using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinUsbInit
{
    class DeviceArrivalListener : NativeWindow
    {
        // Constant values was found in the "windows.h" header file.
        private const int WM_DEVICECHANGE = 0x219;
        private const int DBT_DEVICEARRIVAL = 0x8000;

        private readonly WinUsbInitForm _parent;

        public DeviceArrivalListener(WinUsbInitForm parent)
        {
            parent.HandleCreated += OnHandleCreated;
            parent.HandleDestroyed += OnHandleDestroyed;
            _parent = parent;
        }

        // Listen for the control's window creation and then hook into it.
        internal void OnHandleCreated(object sender, EventArgs e)
        {
            // Window is now created, assign handle to NativeWindow.
            AssignHandle(((WinUsbInitForm)sender).Handle);
        }

        internal void OnHandleDestroyed(object sender, EventArgs e)
        {
            // Window was destroyed, release hook.
            ReleaseHandle();
        }

        protected override void WndProc(ref Message m)
        {
            // Listen for operating system messages
            if (m.Msg == WM_DEVICECHANGE && (int) m.WParam == DBT_DEVICEARRIVAL)
            {
                // Notify the form that this message was received.
                _parent.DeviceInserted();
            }
            base.WndProc(ref m);
        }
    }
}
