using MyVoice.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyVoice.ViewModel
{
    class MainVM
    {

        // singleton stuff
        private static MainVM mInstance;

        private MainVM() { }
        public static MainVM Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new MainVM();
                    mInstance.Init();
                }
                return mInstance;
            }
        }
        //////////////////////
        private const string UserSettingsFilename = "settings.xml";
        private string _DefaultSettingsPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\Settings\\" + UserSettingsFilename;
        private string _UserSettingsPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\Settings\\UserSettings\\" + UserSettingsFilename;

        private readonly Phrases mPhrases = new Phrases();


        private void Init()
        {
            LoadSettings();
            LoadList();
        }

        public void LoadList()
        {
            mPhrases.LoadList(FilePath, FolderPath);
        }

        public AppSettings Settings { get; private set; }

          
        private void LoadSettings()
        {
            // if default settings exist
            if (File.Exists(_UserSettingsPath))
                this.Settings = AppSettings.Read(_UserSettingsPath);
            else
                this.Settings = AppSettings.Read(_DefaultSettingsPath);
        }

        public void SaveUserSettings()
        {
            Settings.Save(_UserSettingsPath);
        }

        public string FilePath
        {
            get
            {
                string recent = Settings.FilePath;
                if (recent == null || recent.Length == 0 || !File.Exists(recent))
                {
                    recent = "";
                    FilePath = recent;
                }
                return recent;
            }
            set
            {
                Settings.FilePath = value;
                SaveUserSettings();
                mPhrases.LoadList(Settings.FilePath, Settings.FolderPath);
                return;
            }
        }

        public string FolderPath
        {
            get
            {
                string recent = Settings.FolderPath;
                if (recent == null || recent.Length == 0 || !Directory.Exists(recent))
                {
                    recent = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    FolderPath = recent;
                }
                return recent;
            }
            set
            {
                Settings.FolderPath = value;
                SaveUserSettings();
                mPhrases.LoadList(Settings.FilePath, Settings.FolderPath);
            }
        }


        public List<Phrase> PhrasesList
        {
            get
            {
                return mPhrases.List;
            }
        }
        
    }
}
