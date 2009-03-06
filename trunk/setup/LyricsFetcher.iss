[Files]
Source: ..\LyricsFetcher\bin\Release\LyricsFetcher.exe; DestDir: {app}
Source: ..\LyricsFetcher\bin\Release\Interop.iTunesLib.dll; DestDir: {app}
Source: ..\LyricsFetcher\bin\Release\LyricsFetcher.XmlSerializers.dll; DestDir: {app}
Source: ..\LyricsFetcher\bin\Release\ObjectListView.dll; DestDir: {app}
Source: ..\LyricsFetcher\bin\Release\Interop.WMPLib.dll; DestDir: {app}
Source: ..\LyricsFetcher\bin\Release\LyricsFetcher.exe.config; DestDir: {app}
Source: Readme.rtf; DestDir: {app}
Source: ..\LyricsFetcher\bin\Release\Interop.ITDETECTORLib.dll; DestDir: {app}
[Dirs]
Name: {app}\
[Setup]
VersionInfoVersion=0.5
VersionInfoCompany=Bright Ideas Software
VersionInfoDescription=LyricsFetch installer
VersionInfoTextVersion=the text version
VersionInfoCopyright=(c) 2009 All Right Reserved
AppCopyright=Copyright © 2009 Bright Ideas Software All Rights Reserved
AppName=LyricsFetcher
AppVerName=LyricsFetcher v0.5
PrivilegesRequired=none
DefaultDirName={pf}\Bright Ideas Software\LyricsFetcher\
ShowLanguageDialog=no
OutputBaseFilename=LyricsFetcherSetup-0.5
DefaultGroupName=LyricsFetcher
AppID={{A556A5AD-2A0D-48ED-A8E8-EA524CA0D366}
LicenseFile=..\COPYING.txt
OutputDir=..\..\releases
[Icons]
Name: {group}\LyricsFetcher; Filename: {app}\LyricsFetcher.exe; IconFilename: {app}\LyricsFetcher.exe
Name: {group}\Uninstall LyricsFetcher; Filename: {uninstallexe}
Name: {group}\Readme; Filename: {app}\Readme.rtf
[Run]
Filename: {app}\LyricsFetcher.exe; Description: Run LyricsFetcher now; Flags: postinstall nowait skipifsilent
Filename: {app}\Readme.rtf; Description: Open ReadMe; Flags: postinstall unchecked shellexec skipifsilent nowait
