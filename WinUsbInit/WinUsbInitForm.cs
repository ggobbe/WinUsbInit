using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinUsbInit.Contracts;

namespace WinUsbInit
{
    public partial class WinUsbInitForm : Form
    {
        private readonly IConfig _config;
        private readonly DeviceArrivalListener _deviceArrivalListener;
        private readonly Color _errorColor = Color.Red;
        private readonly Color _infoColor = Color.Aqua;
        private readonly IUsbInitializer _usbInitializer;


        public WinUsbInitForm()
        {
            InitializeComponent();
            _usbInitializer = new UsbInitializer();
            _config = new Config();

            WriteLog("Starting new devices listener...");
            _deviceArrivalListener = new DeviceArrivalListener(this);
        }

        public async Task DeviceInserted()
        {
            // Search drive with default label
            var searchLabel = _config.GetInitialVolumeLabel();
            WriteLog($"Device inserted, searching for drive with label '{searchLabel}'");
            var drive = _usbInitializer.FindUsbDrive(searchLabel);
            if (drive == null)
            {
                WriteLog($"No device found with label '{searchLabel}'.", _errorColor);
                WriteLog("Please insert another USB drive to initialize...", _infoColor);
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
            var copiedCount = await Task.Run(() => _usbInitializer.CopyFiles(sourceDir, drive));
            stopWatch.Stop();
            WriteLog($"{copiedCount} file(s) copied to {drive.Name} in {stopWatch.Elapsed}");

            // Remove drive
            WriteLog($"Removing drive {drive.Name} safely");
            var removed = await Task.Run(() => _usbInitializer.EjectDrive(drive));
            for (var i = 0; i < 4; i++)
            {
                Console.Beep(800,500);
            }
            WriteLog(removed
                ? "Please insert another USB drive to initialize..."
                : $"Error whilst removing drive {drive.Name}...", removed ? _infoColor : _errorColor);
            
            
       
    }

        private void WriteLog(string msg, Color? color = null)
        {
            var length = outputBox.TextLength;
            var time = DateTime.Now.ToLongTimeString();
            var log = $"{time}: {msg}";
            outputBox.AppendText($"{log}\n");

            if (color.HasValue)
            {
                outputBox.SelectionStart = length;
                outputBox.SelectionLength = log.Length;
                outputBox.SelectionColor = color.Value;
                outputBox.SelectionLength = 0;
                outputBox.SelectionStart = outputBox.TextLength;
            }
        }
    }
}