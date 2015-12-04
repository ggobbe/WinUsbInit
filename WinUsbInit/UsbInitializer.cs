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
                        d =>
                            d.IsReady && d.VolumeLabel.Equals(volumeLabel, StringComparison.InvariantCultureIgnoreCase) &&
                            d.DriveType == DriveType.Removable);
        }

        public void RenameDrive(DriveInfo drive, string label)
        {
            drive.VolumeLabel = label;
        }
    }
}