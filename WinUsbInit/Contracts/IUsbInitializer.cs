using System.IO;

namespace WinUsbInit.Contracts
{
    internal interface IUsbInitializer
    {
        DriveInfo FindUsbDrive();
        void RenameDrive(DriveInfo drive, string label);
    }
}