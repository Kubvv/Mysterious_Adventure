namespace RambleJungle.Base.Tools
{
    using System.Collections;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Runtime.Versioning;

    public class Monitor
    {
        #region Dll imports

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        private static extern bool GetMonitorInfo (HandleRef hmonitor, [In, Out] MonitorInfoEx info);

        [DllImport("user32.dll", ExactSpelling = true)]
        [ResourceExposure(ResourceScope.None)]
        private static extern bool EnumDisplayMonitors (HandleRef hdc, IntPtr rcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

        private delegate bool MonitorEnumProc (IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        private struct MonitorRect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        private class MonitorInfoEx
        {
            internal int cbSize = Marshal.SizeOf(typeof(MonitorInfoEx));
            internal MonitorRect rcMonitor = new();
            internal MonitorRect rcWork = new();
            internal int dwFlags = 0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            internal char[] szDevice = new char[32];
        }

        private const int MonitorinfofPrimary = 0x00000001;

        #endregion

        public static HandleRef NullHandleRef { get; } = new(null, IntPtr.Zero);

        public Rectangle Bounds { get; private set; }
        public Rectangle WorkingArea { get; private set; }
        public string Name { get; private set; }

        public bool IsPrimary { get; private set; }

        private Monitor(IntPtr monitor, IntPtr _)
        {
            MonitorInfoEx? info = new();
            GetMonitorInfo(new HandleRef(null, monitor), info);
            Bounds = new Rectangle(info.rcMonitor.left, info.rcMonitor.top,
                info.rcMonitor.right - info.rcMonitor.left, info.rcMonitor.bottom - info.rcMonitor.top);
            WorkingArea = new Rectangle(info.rcWork.left, info.rcWork.top,
                info.rcWork.right - info.rcWork.left, info.rcWork.bottom - info.rcWork.top);
            IsPrimary = ((info.dwFlags & MonitorinfofPrimary) != 0);
            Name = new string(info.szDevice).TrimEnd((char)0);
        }

        public static IEnumerable<Monitor> AllMonitors
        {
            get
            {
                MonitorEnumCallback? closure = new();
                MonitorEnumProc? proc = new(closure.Callback);
                EnumDisplayMonitors(NullHandleRef, IntPtr.Zero, proc, IntPtr.Zero);
                return closure.Monitors.Cast<Monitor>();
            }
        }

        public static Monitor BiggestMonitor()
        {
            Monitor result = AllMonitors.First();
            foreach (Monitor monitor in AllMonitors)
            {
                if (monitor.WorkingArea.Width * monitor.WorkingArea.Height > result.WorkingArea.Width * result.WorkingArea.Height)
                {
                    result = monitor;
                }
            }
            return result;
        }

        private class MonitorEnumCallback
        {
            public ArrayList Monitors { get; private set; }

            public MonitorEnumCallback()
            {
                Monitors = new ArrayList();
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
            public bool Callback(IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lparam)
            {
                Monitors.Add(new Monitor(monitor, hdc));
                return true;
            }
        }
    }
}
