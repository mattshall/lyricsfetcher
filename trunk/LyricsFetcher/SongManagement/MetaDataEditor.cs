/*
 * This class uses Windows Media SDK calls to read and write metadata to media files.
 * 
 * This editor only works on file types that Windows Media subsystem can handle
 * e.g. wma and mp3. Other formats are dependent on the installed codecs. 
 * 
 * Author: Phillip Piper
 * Date: 2009-03-12 10:28 PM
 *
 * CHANGE LOG:
 * 2009-03-12 JPP   Initial Version
 * 
 * TO DO:
 * - Add method to get all fields (and values?)
 */

using System;
using System.Runtime.InteropServices;
using System.Text;
using WMFSDKWrapper;

namespace LyricsFetcher
{
    /// <summary>
    /// Instances of this class get or set the metadata on a given file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class only supports string metadata. This is not a real limitation since
    /// this is exactly what WMP ocx does, plus it seems that all ID3 tags are stored 
    /// as strings anyway, even date and year fields.
    /// </para>
    /// <para>
    /// It only works on file types that Windows Media subsystem can handle, e.g. wma and mp3.
    /// Support for other formats depends on the capabilities of the codecs installed.
    /// Being able to play a particular format does NOT guarantee the ability to read/write metadata.
    /// </para>
    /// <para>
    /// All metadata is taken and written to the file as a whole (stream 0).
    /// </para>
    /// <para>
    /// Most media players (like WMP and iTunes) only read the tags when they first import
    /// a track. Changes made to tags after the initial import will not change the information
    /// within the music library. Lyrics are a notable exception to this rule, since they are
    /// not stored within the library, only within the media file itself.
    /// </para>
    /// </remarks>
    public class MetaDataEditor : IDisposable
    {
        #region Life and death

        /// <summary>
        /// Open an editor on the given file.
        /// </summary>
        /// <param name="fileName">The path to the file to open</param>
        /// <remarks>This will throw an exception if the format of the file is unknown.</remarks>
        public MetaDataEditor(string fileName) {
            WMFSDKFunctions.WMCreateEditor(out this.editor);
            this.headerInfo = (IWMHeaderInfo3)this.editor;
            this.editor.Open(fileName);
        }

        /// <summary>
        /// Dispose of this resource
        /// </summary>
        void IDisposable.Dispose() {
            this.Close();
        }

        /// <summary>
        /// Close this editor, committing any changes to disk.
        /// </summary>
        /// <remarks>The editor cannot be further used after this is called.</remarks>
        public void Close() {
            if (this.editor != null) {
                this.editor.Flush();
                this.editor.Close(); // is this redundant?
                this.editor = null;
            }
        }

        #endregion

        #region Tags

        /// <summary>
        /// Return the value of the given tag
        /// </summary>
        /// <param name="fieldName">The name of the field to fetch</param>
        /// <returns>The value of the field or an empty string if the tag wasn't present</returns>
        /// <remarks>This only works on field of type string. Fields of other types are returned as
        /// empty strings.</remarks>
        public string GetFieldValue(string fieldName) {
            WMT_ATTR_DATATYPE fieldType;
            ushort fieldValueLength = 0;

            // How big the value going to be?
            try {
                this.headerInfo.GetAttributeByName(ref this.streamNumber, fieldName, out fieldType, null, ref fieldValueLength);
            }
            catch (COMException ex) {
                // If there was no such tag, we return an empty string
                if ((ex.ErrorCode & 0x7FFFFFFF) == 0x400D07F0)
                    return String.Empty;
                else
                    throw ex;
            }

            // We only handle strings
            if (fieldType != WMT_ATTR_DATATYPE.WMT_TYPE_STRING)
                return String.Empty;

            // If there is no value in the field, return an empty string
            if (fieldValueLength == 0)
                return String.Empty;

            // Allocate a StringBuilder with the required storage space
            StringBuilder fieldValue = new StringBuilder(fieldValueLength);

            // Get the actual field value (when fieldValueLength > 0, this gets the value)
            this.headerInfo.GetAttributeByName(ref this.streamNumber,
                fieldName, out fieldType, fieldValue, ref fieldValueLength);

            return fieldValue.ToString();
        }

        /// <summary>
        /// Set the value of the given field.
        /// </summary>
        /// <param name="fieldName">The name of the field to set</param>
        /// <param name="value">The value to set</param>
        /// <remarks>
        /// <para>If the given field already has multiple values, only the first value will be updated.</para>
        /// </remarks>
        public void SetFieldValue(string fieldName, string value) {

            // How many fields with that name does the file already have?
            ushort language = 0;
            ushort arrayLength = 0;
            this.headerInfo.GetAttributeIndices(this.streamNumber, fieldName, ref language, null, ref arrayLength);

            // Use a StringBuilder as our buffer. We have to make sure the string is null-terminated
            StringBuilder buffer = new StringBuilder(value);
            if (value.Length == 0 || value[value.Length-1] != '\0')
                buffer.Append('\0');

            // If there is no previous field with that name, we add a new one. 
            // Otherwise we modify the first existing one.
            if (arrayLength == 0) {
                ushort fieldIndex;
                this.headerInfo.AddAttribute(this.streamNumber, fieldName, out fieldIndex,
                    WMT_ATTR_DATATYPE.WMT_TYPE_STRING, language, buffer, (uint)buffer.Length * 2);
            } else {
                // Get the indices of the field with that name
                ushort[] indices = new ushort[arrayLength];
                this.headerInfo.GetAttributeIndices(this.streamNumber, fieldName, ref language, indices, ref arrayLength);
                
                // Update the first field
                this.headerInfo.ModifyAttribute(this.streamNumber, indices[0],
                    WMT_ATTR_DATATYPE.WMT_TYPE_STRING, language, buffer, (uint)buffer.Length * 2);
            }
        }

        #endregion

        #region Private variables

        private IWMMetadataEditor editor;
        private IWMHeaderInfo3 headerInfo;
        private ushort streamNumber = 0;

        #endregion
    }
}
