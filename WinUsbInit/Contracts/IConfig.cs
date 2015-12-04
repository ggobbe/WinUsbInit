namespace WinUsbInit.Contracts
{
    internal interface IConfig
    {
        string GetInitialVolumeLabel();
        string GetNewVolumeLabel();
        string GetSourceFilesDir();
    }
}