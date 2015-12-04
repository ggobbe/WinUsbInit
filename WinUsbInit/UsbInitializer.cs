using System;
using System.IO;
using System.Linq;
using WinUsbInit.Contracts;

namespace WinUsbInit
{
    internal class UsbInitializer : IUsbInitializer
    {
        public DriveInfo FindUsbDrive(string volumeLabel)
        {
            return
                DriveInfo.GetDrives()
                    .SingleOrDefault(
                        d => d.IsReady && d.VolumeLabel == volumeLabel && d.DriveType == DriveType.Removable);
        }

        public void RenameDrive(DriveInfo drive, string label)
        {
            throw new NotImplementedException();
        }
    }
}