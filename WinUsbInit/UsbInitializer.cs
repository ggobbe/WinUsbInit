using System;
using System.IO;
using System.Linq;
using WinUsbInit.Contracts;

namespace WinUsbInit
{
    internal class UsbInitializer : IUsbInitializer
    {
        public DriveInfo FindUsbDrive()
        {
            return
                DriveInfo.GetDrives()
                    .FirstOrDefault(d => d.IsReady && d.VolumeLabel == "" && d.DriveType == DriveType.Removable);
        }

        public void RenameDrive(DriveInfo drive, string label)
        {
            throw new NotImplementedException();
        }
    }
}