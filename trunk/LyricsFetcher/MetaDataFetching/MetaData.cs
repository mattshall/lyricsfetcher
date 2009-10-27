using System;
using System.Collections.Generic;
using System.Text;

namespace LyricsFetcher
{
    public class MetaData
    {
        /// <summary>
        /// Gets or sets the song that this metadata is for
        /// </summary>
        public Song Song { get; set; }

        /// <summary>
        /// Gets the unique identifier for this track
        /// </summary>
        public string Puid { get; set; }

        /// <summary>
        /// Gets the title of the track
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets the artist of the track
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// Gets the genre of the track
        /// </summary>
        public string Genre { get; set; }

        public MetaDataStatus Status { get; set; }

        public bool IsFinished {
            get {
                return (int)this.Status >= (int)MetaDataStatus.Failed;
            }
        }

        public bool IsDataUnchanged {
            get {
                return this.Status == MetaDataStatus.Success &&
                    this.Song != null &&
                    this.Song.Title == this.Title &&
                    this.Song.Artist == this.Artist &&
                    this.Song.Genre == this.Genre;
            }
        }
    }

    public class MetaDataEventArgs : EventArgs
    {
        public MetaDataEventArgs(MetaData data) {
            this.MetaData = data;
        }
        public MetaData MetaData { get; private set; }
    }


    public enum MetaDataStatus
    {
        None = 0,

        Waiting = 0x100,

        InProgress = 0x200,
        InProgress_Converting,
        InProgress_Fingerprinting,
        InProgress_Fetching,

        Failed = 0x300,
        Failed_MissingFile,
        Failed_UnknownFileType,
        Failed_ConverterFailed,
        Failed_Lookup,
        Failed_NoData,

        Success = 0x400,
        Cancelled = 0x500,
        Accepted = 0x600,
        Rejected = 0x700
    }
}
