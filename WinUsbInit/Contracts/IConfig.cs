namespace WinUsbInit.Contracts
{
    internal interface IConfig
    {
        string GetVolumeLabel();
        string GetSourceFilesDir();
    }
}