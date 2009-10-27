﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LyricsFetcher.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        /// <summary>
        /// The list of genres for which song lyrics will never be fetched
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Configuration.SettingsDescriptionAttribute("The list of genres for which song lyrics will never be fetched")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>Books &amp; Spoken</string>
  <string>Classical</string>
  <string>Instrumental</string>
  <string>Podcast</string>
  <string>Sermon</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection IgnoredGenres {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["IgnoredGenres"]));
            }
        }
        
        /// <summary>
        /// The URL that is invoked when the Search button is pressed. {0} is the title, {1} is the artist.
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Configuration.SettingsDescriptionAttribute("The URL that is invoked when the Search button is pressed. {0} is the title, {1} " +
            "is the artist.")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://www.google.com/search?q=\'{0}\'+\'{1}\'+%2Blyrics")]
        public string SearchQuery {
            get {
                return ((string)(this["SearchQuery"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>MPEG-4 video file</string>
  <string>Protected MPEG-4 video file</string>
  <string>Protected QuickTime movie file</string>
  <string>QuickTime movie file</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection IgnoredKinds {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["IgnoredKinds"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool UseFastITunesLoader {
            get {
                return ((bool)(this["UseFastITunesLoader"]));
            }
        }
        
        /// <summary>
        /// How are artist names arranged? &apos;FirstLast&apos; is normal &apos;John Smith&apos;; &apos;LastFirst&apos; means &apos;Smith, John&apos;
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Configuration.SettingsDescriptionAttribute("How are artist names arranged? \'FirstLast\' is normal \'John Smith\'; \'LastFirst\' me" +
            "ans \'Smith, John\'")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("FirstLast")]
        public string ArtistNameFormat {
            get {
                return ((string)(this["ArtistNameFormat"]));
            }
        }
        
        /// <summary>
        /// The names of the lyrics source that will be used to look for lyrics in the order that they will be checked
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Configuration.SettingsDescriptionAttribute("The names of the lyrics source that will be used to look for lyrics in the order " +
            "that they will be checked")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3." +
            "org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <s" +
            "tring>LyricsPlugin</string>\r\n  <string>LyrDb</string>\r\n</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection LyricsSources {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["LyricsSources"]));
            }
        }
    }
}
