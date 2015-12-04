using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinUsbInit
{
    public partial class WinUsbInitForm : Form
    {
        private DeviceArrivalListener _deviceArrivalListener;

        public WinUsbInitForm()
        {
            InitializeComponent();
            AddLog("Starting listener...");
            _deviceArrivalListener = new DeviceArrivalListener(this);
            AddLog("Listener started!");
        }

        public void DeviceInserted()
        {
            AddLog("Device inserted");
        }

        public void AddLog(string msg)
        {
            var time = DateTime.Now.ToLongTimeString();
            var log = $"{time} {msg}";
            outputBox.Text += $"{log}\n";
        }
    }
}
