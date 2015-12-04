using System.Configuration;
using WinUsbInit.Contracts;

namespace WinUsbInit
{
    internal class Config : IConfig
    {
        private const string InitialVolumeLabel = "InitialVolumeLabel";
        private const string NewVolumeLabel = "NewVolumeLabel";
        private const string SourceFilesDir = "SourceFilesDir";

        public string GetInitialVolumeLabel()
        {
            return ConfigurationManager.AppSettings[InitialVolumeLabel];
        }

        public string GetNewVolumeLabel()
        {
            return ConfigurationManager.AppSettings[NewVolumeLabel];
        }

        public string GetSourceFilesDir()
        {
            return ConfigurationManager.AppSettings[SourceFilesDir];
        }
    }
}