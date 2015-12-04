using System;
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

            AddLog("Starting listener...");
            _deviceArrivalListener = new DeviceArrivalListener(this);
            AddLog("Listener started!");
        }

        public void DeviceInserted()
        {
            var searchLabel = _config.GetInitialVolumeLabel();
            AddLog($"Device inserted, searching for drive with label '{searchLabel}'");
            var drive = _usbInitializer.FindUsbDrive(searchLabel);
            if (drive == null)
            {
                AddLog("No drive found!");
                return;
            }

            var newLabel = _config.GetNewVolumeLabel();
            AddLog($"Changing drive {drive.Name} label to '{newLabel}'");
            _usbInitializer.RenameDrive(drive, newLabel);
        }

        private void AddLog(string msg)
        {
            var time = DateTime.Now.ToLongTimeString();
            var log = $"{time}: {msg}";
            outputBox.AppendText($"{log}\n");
        }
    }
}