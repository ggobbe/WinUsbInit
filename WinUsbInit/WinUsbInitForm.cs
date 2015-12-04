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

            AddLog("Starting new devices listener...");
            _deviceArrivalListener = new DeviceArrivalListener(this);
        }

        public void DeviceInserted()
        {
            // Search drive with default label
            var searchLabel = _config.GetInitialVolumeLabel();
            AddLog($"Device inserted, searching for drive with label '{searchLabel}'");
            var drive = _usbInitializer.FindUsbDrive(searchLabel);
            if (drive == null)
            {
                AddLog("No drive found!");
                return;
            }
            AddLog($"Found drive {drive.Name}");

            // Change drive label
            var newLabel = _config.GetNewVolumeLabel();
            AddLog($"Changing label of drive {drive.Name} to '{newLabel}'");
            _usbInitializer.RenameDrive(drive, newLabel);

            // Copy files from source directory
            var sourceDir = _config.GetSourceFilesDir();
            AddLog($"Copying files to drive {drive.Name}");
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var copiedCount = _usbInitializer.CopyFiles(sourceDir, drive);
            stopWatch.Stop();
            AddLog($"{copiedCount} file(s) copied to {drive.Name} in {stopWatch.Elapsed}");
        }

        private void AddLog(string msg)
        {
            var time = DateTime.Now.ToLongTimeString();
            var log = $"{time}: {msg}";
            outputBox.AppendText($"{log}\n");
        }
    }
}