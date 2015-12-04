using System.IO;

namespace WinUsbInit.Contracts
{
    internal interface IUsbInitializer
    {
        DriveInfo FindUsbDrive(string volumeLabel);
        void RenameDrive(DriveInfo drive, string label);
        int CopyFiles(string sourceDirectory, DriveInfo drive);
        bool EjectDrive(DriveInfo drive);
    }
}