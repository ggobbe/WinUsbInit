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
            var label = _config.GetInitialVolumeLabel();
            AddLog($"Device inserted, searching for drive with label '{label}'...");
            var drive = _usbInitializer.FindUsbDrive(label);
            if (drive == null)
            {
                AddLog("No drive found!");
                return;
            }
            AddLog($"Drive found: {drive.Name}");
        }

        private void AddLog(string msg)
        {
            var time = DateTime.Now.ToLongTimeString();
            var log = $"{time}: {msg}";
            outputBox.AppendText($"{log}\n");
        }
    }
}