using System.Configuration;
using WinUsbInit.Contracts;

namespace WinUsbInit
{
    internal class Config : IConfig
    {
        private const string VolumeLabel = "VolumeLabel";
        private const string SourceFilesDir = "SourceFilesDir";

        public string GetVolumeLabel()
        {
            return ConfigurationManager.AppSettings[VolumeLabel];
        }

        public string GetSourceFilesDir()
        {
            return ConfigurationManager.AppSettings[SourceFilesDir];
        }
    }
}