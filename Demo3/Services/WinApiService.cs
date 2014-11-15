using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Demo3.Services
{

    public class WinApiService
    {
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)] //
        public static extern bool SendMessage(IntPtr hWnd, int Msg, int wParam, StringBuilder lParam);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wparam, int lparam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, string lParam);

        const int WM_GETTEXT = 0x000D;
        const int WM_GETTEXTLENGTH = 0x000E;
        const int WM_SETTEXT = 0xC;

        public string GetTextOfProcess(Process process)
        {
            try
            {
                if (process != null)
                {
                    var childHandle = FindWindowEx(process.MainWindowHandle, IntPtr.Zero, "Edit", null);
                    var size = SendMessage(childHandle, WM_GETTEXTLENGTH, 0, 0).ToInt32();
                    if (size > 0)
                    {
                        var content = new StringBuilder(size + 1);

                        SendMessage(childHandle, WM_GETTEXT, content.Capacity, content);
                        return content.ToString();
                    }
                }
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public bool SendNewMessage(Process process, string newMessage)
        {
            try
            {
                if (process != null)
                {
                    var childHandle = FindWindowEx(process.MainWindowHandle, IntPtr.Zero, "Edit", null);
                    SendMessage(childHandle, WM_SETTEXT, 0, newMessage);

                    return true;

                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
