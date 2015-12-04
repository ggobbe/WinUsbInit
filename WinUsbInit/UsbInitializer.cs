using System;
using System.Diagnostics;
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

        public int CopyFiles(string sourceDirectory, DriveInfo drive)
        {
            var files = Directory.GetFiles(sourceDirectory);
            foreach (var file in files)
            {
                var fileName = file.Substring(sourceDirectory.Length + 1);
                File.Copy(file, Path.Combine(drive.Name, fileName), true);
            }
            return files.Length;
        }

        public bool EjectDrive(DriveInfo drive)
        {
            var process = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    FileName = @"Utils\RemoveDrive.exe",
                    Arguments = $"{drive.Name[0]} -L",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Minimized
                }
            };
            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return output.Contains("success");
        }
    }
}