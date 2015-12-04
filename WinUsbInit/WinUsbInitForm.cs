using System;
using System.Diagnostics;
using System.Windows.Forms;
using WinUsbInit.Contracts;

namespace WinUsbInit
{
    public partial class WinUsbInitForm : Form
    {
        private readonly IConfig _config;

        private readonly DeviceArrivalListener _deviceArrivalListener;
        private readonly IUsbInitializer _usbInitializer;

        public WinUsbInitForm()
        {
            InitializeComponent();
            _usbInitializer = new UsbInitializer();
            _config = new Config();

            WriteLog("Starting new devices listener...");
            _deviceArrivalListener = new DeviceArrivalListener(this);
        }

        public void DeviceInserted()
        {
            // Search drive with default label
            var searchLabel = _config.GetInitialVolumeLabel();
            WriteLog($"Device inserted, searching for drive with label '{searchLabel}'");
            var drive = _usbInitializer.FindUsbDrive(searchLabel);
            if (drive == null)
            {
                WriteLog($"No device found with label '{searchLabel}'.");
                WriteLog("Please insert another USB drive to initialize...");
                return;
            }

            // Change drive label
            var newLabel = _config.GetNewVolumeLabel();
            WriteLog($"Changing label of drive {drive.Name} to '{newLabel}'");
            _usbInitializer.RenameDrive(drive, newLabel);

            // Copy files from source directory
            var sourceDir = _config.GetSourceFilesDir();
            WriteLog($"Copying files to drive {drive.Name}");
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var copiedCount = _usbInitializer.CopyFiles(sourceDir, drive);
            stopWatch.Stop();
            WriteLog($"{copiedCount} file(s) copied to {drive.Name} in {stopWatch.Elapsed}");

            WriteLog($"Removing drive {drive.Name} safely");
            var removed = _usbInitializer.EjectDrive(drive);
            WriteLog(removed
                ? "Please insert another USB drive to initialize..."
                : $"Error whilst removing drive {drive.Name}...");
        }

        private void WriteLog(string msg)
        {
            var time = DateTime.Now.ToLongTimeString();
            var log = $"{time}: {msg}";
            outputBox.AppendText($"{log}\n");
        }
    }
}