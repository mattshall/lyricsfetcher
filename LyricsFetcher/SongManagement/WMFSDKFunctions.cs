//*****************************************************************************
//
// Microsoft Windows Media
// Copyright ( C) Microsoft Corporation. All rights reserved.
//
// FileName:            WMFSDKFunctions.cs
//
// Abstract:            Wrapper used by managed-code samples.
//
//*****************************************************************************

// CHANGE LOG:
// 2009-03-12   JPP   Changed to only read and write strings

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WMFSDKWrapper
{
    public class WMFSDKFunctions
    {
        [DllImport("WMVCore.dll", EntryPoint = "WMCreateEditor", SetLastError = true,
             CharSet = CharSet.Unicode, ExactSpelling = true,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int WMCreateEditor(
            [Out, MarshalAs(UnmanagedType.Interface)]	out IWMMetadataEditor ppMetadataEditor);

        public WMFSDKFunctions() {
            //
            // TODO: Add constructor logic here
            //
        }
    }

    [Guid("96406BD9-2B2B-11d3-B36B-00C04F6108FF"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMMetadataEditor
    {
        int Open([In, MarshalAs(UnmanagedType.LPWStr)] string pwszFilename);
        int Close();
        int Flush();

    }

    [Guid("15CC68E3-27CC-4ecd-B222-3F5D02D80BD5"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMHeaderInfo3
    {
        int GetAttributeCount(
            [In]									ushort wStreamNum,
            [Out]									out ushort pcAttributes);

        int GetAttributeByIndex(
            [In]									ushort wIndex,
            [Out, In]								ref ushort pwStreamNum,
            [Out, MarshalAs(UnmanagedType.LPWStr)]	string pwszName,
            [Out, In]								ref ushort pcchNameLen,
            [Out]									out WMT_ATTR_DATATYPE pType,
            //[Out, MarshalAs(UnmanagedType.LPArray)]	byte[] pValue,
            [Out, MarshalAs(UnmanagedType.LPWStr)]	StringBuilder pValue,
            [Out, In]								ref ushort pcbLength);

        int GetAttributeByName(
            [Out, In]								ref ushort pwStreamNum,
            [Out, MarshalAs(UnmanagedType.LPWStr)]	string pszName,
            [Out]									out WMT_ATTR_DATATYPE pType,
            //[Out, MarshalAs(UnmanagedType.LPArray)]	byte[] pValue,
            [Out, MarshalAs(UnmanagedType.LPWStr)]	StringBuilder pValue,
            [Out, In]								ref ushort pcbLength);

        int SetAttribute(
            [In]									ushort wStreamNum,
            [In, MarshalAs(UnmanagedType.LPWStr)]	string pszName,
            [In]									WMT_ATTR_DATATYPE Type,
            [In, MarshalAs(UnmanagedType.LPArray)]	byte[] pValue,
            [In]									ushort cbLength);

        int GetMarkerCount(
            [Out]									out ushort pcMarkers);

        int GetMarker(
            [In]									ushort wIndex,
            [Out, MarshalAs(UnmanagedType.LPWStr)]	string pwszMarkerName,
            [Out, In]								ref ushort pcchMarkerNameLen,
            [Out]									out ulong pcnsMarkerTime);

        int AddMarker(
            [In, MarshalAs(UnmanagedType.LPWStr)]	string pwszMarkerName,
            [In]									ulong cnsMarkerTime);

        int RemoveMarker(
            [In]									ushort wIndex);

        int GetScriptCount(
            [Out]									out ushort pcScripts);

        int GetScript(
            [In]									ushort wIndex,
            [Out, MarshalAs(UnmanagedType.LPWStr)]	string pwszType,
            [Out, In]								ref ushort pcchTypeLen,
            [Out, MarshalAs(UnmanagedType.LPWStr)]	string pwszCommand,
            [Out, In]								ref ushort pcchCommandLen,
            [Out]									out ulong pcnsScriptTime);

        int AddScript(
            [In, MarshalAs(UnmanagedType.LPWStr)]	string pwszType,
            [In, MarshalAs(UnmanagedType.LPWStr)]	string pwszCommand,
            [In]									ulong cnsScriptTime);

        int RemoveScript(
            [In]									ushort wIndex);

        int GetCodecInfoCount(
            [Out]									out uint pcCodecInfos);

        int GetCodecInfo(
            [In]									uint wIndex,
            [Out, In]								ref ushort pcchName,
            [Out, MarshalAs(UnmanagedType.LPWStr)]	string pwszName,
            [Out, In]								ref ushort pcchDescription,
            [Out, MarshalAs(UnmanagedType.LPWStr)]	string pwszDescription,
            [Out]									out WMT_CODEC_INFO_TYPE pCodecType,
            [Out, In]								ref ushort pcbCodecInfo,
            [Out, MarshalAs(UnmanagedType.LPArray)]	byte[] pbCodecInfo);

        int GetAttributeCountEx(
            [In]									ushort wStreamNum,
            [Out]									out ushort pcAttributes);

        int GetAttributeIndices(
            [In]									ushort wStreamNum,
            [In, MarshalAs(UnmanagedType.LPWStr)]	string pwszName,
            [In]									ref ushort pwLangIndex,
            [Out, MarshalAs(UnmanagedType.LPArray)] ushort[] pwIndices,
            [Out, In]								ref ushort pwCount);

        int GetAttributeByIndexEx(
            [In]									ushort wStreamNum,
            [In]									ushort wIndex,
            [Out, MarshalAs(UnmanagedType.LPWStr)]	string pwszName,
            [Out, In]								ref ushort pwNameLen,
            [Out]									out WMT_ATTR_DATATYPE pType,
            [Out]									out ushort pwLangIndex,
            //[Out, MarshalAs(UnmanagedType.LPArray)]	byte[] pValue,
            [Out, MarshalAs(UnmanagedType.LPWStr)]	StringBuilder pValue,
            [Out, In]								ref uint pdwDataLength);

        int ModifyAttribute(
            [In]									ushort wStreamNum,
            [In]									ushort wIndex,
            [In]									WMT_ATTR_DATATYPE Type,
            [In]									ushort wLangIndex,
            //[In, MarshalAs(UnmanagedType.LPArray)]	byte[] pValue,
            [In, MarshalAs(UnmanagedType.LPWStr)]	StringBuilder pValue,
            [In]									uint dwLength);

        int AddAttribute(
            [In]									ushort wStreamNum,
            [In, MarshalAs(UnmanagedType.LPWStr)]	string pszName,
            [Out]									out ushort pwIndex,
            [In]									WMT_ATTR_DATATYPE Type,
            [In]									ushort wLangIndex,
            //[In, MarshalAs(UnmanagedType.LPArray)]	byte[] pValue,
            [In, MarshalAs(UnmanagedType.LPWStr)]	StringBuilder pValue,
            [In]									uint dwLength);

        int DeleteAttribute(
            [In]									ushort wStreamNum,
            [In]									ushort wIndex);

        int AddCodecInfo(
            [In, MarshalAs(UnmanagedType.LPWStr)]	string pszName,
            [In, MarshalAs(UnmanagedType.LPWStr)]	string pwszDescription,
            [In]									WMT_CODEC_INFO_TYPE codecType,
            [In]									ushort cbCodecInfo,
            [In, MarshalAs(UnmanagedType.LPArray)]	byte[] pbCodecInfo);
    }

    public enum WMT_ATTR_DATATYPE
    {
        WMT_TYPE_DWORD = 0,
        WMT_TYPE_STRING = 1,
        WMT_TYPE_BINARY = 2,
        WMT_TYPE_BOOL = 3,
        WMT_TYPE_QWORD = 4,
        WMT_TYPE_WORD = 5,
        WMT_TYPE_GUID = 6,
    }

    public enum WMT_CODEC_INFO_TYPE
    {
        WMT_CODECINFO_AUDIO = 0,
        WMT_CODECINFO_VIDEO = 1,
        WMT_CODECINFO_UNKNOWN = 0xffffff
    }
}
