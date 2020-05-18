using Carbon.Helpers.Mvvm;

using System;
using System.IO;
using System.Windows.Forms;

namespace Carbon.UI
{
    public class OptionsViewModel : BaseViewModel
    {
        private string configurationPath;

        public string ConfigurationPath
        {
            get => configurationPath;
            set
            {
                configurationPath = value;
                RaisePropertyChangedEvent("ConfigurationPath");
            }
        }


        public bool UseBrowserCache
        {
            get => GeneralSettings.Default.UseBrowserCache;
            set
            {
                GeneralSettings.Default.UseBrowserCache = value;
                GeneralSettings.Default.Save();
            }
        }

        public bool IncludeTrivia
        {
            get => GeneralSettings.Default.IncludeTrivia;
            set
            {
                GeneralSettings.Default.IncludeTrivia = value;
                GeneralSettings.Default.Save();
            }
        }



        public DelegateCommand SetConfigFileButtonClickedCommand { get; private set; }

        public OptionsViewModel()
        {
            SetConfigFileButtonClickedCommand = new DelegateCommand(SetConfigFileButtonClicked);




        }

        private void SetConfigFileButtonClicked()
        {
            var fileContent = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog.Filter = "json file (*.json)|*.json";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    //Get the path of specified file
                    ConfigurationPath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }
        }
    }
}
