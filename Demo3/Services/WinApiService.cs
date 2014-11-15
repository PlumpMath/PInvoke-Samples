using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo3.Services
{
    public struct KeyboardHookStruct
    {
        public int vkCode;
        public int scanCode;
        public int flags;
        public int time;
        public int dwExtraInfo;
    }

    public enum ProcessorArchitecture
    {
        X86 = 0,
        X64 = 9,
        @Arm = -1,
        Itanium = 6,
        Unknown = 0xFFFF,
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct SystemInfo
    {
        public ProcessorArchitecture Processor;
        public uint PageSize;
        public uint MinimumApplicationAddress;
        public uint MaximumApplicationAddress;
        public uint ActiveProcessorMask;
        public uint NumberOfProcessors;
        public uint ProcessorType;
        public uint AllocationGranularity;
        public uint ProcessorLevel;
        public uint ProcessorRevision;
    }

    public class WinApiService
    {
        private IntPtr _hookHandle;

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)] //
        public static extern bool SendMessage(IntPtr hWnd, int Msg, int wParam, StringBuilder lParam);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wparam, int lparam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, string lParam);

		[DllImport("user32.dll")]
		static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookProc callback, IntPtr hInstance, uint threadId);

		/// <summary>
		/// Unhooks the windows hook.
		/// </summary>
		/// <param name="hInstance">The hook handle that was returned from SetWindowsHookEx</param>
		/// <returns>True if successful, false otherwise</returns>
		[DllImport("user32.dll")]
		static extern bool UnhookWindowsHookEx(IntPtr hInstance);

		[DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref KeyboardHookStruct lParam);

		[DllImport("kernel32.dll")]
		static extern IntPtr LoadLibrary(string lpFileName);

		public delegate int KeyboardHookProc(int code, int wParam, ref KeyboardHookStruct lParam);

        [DllImport("kernel32")]
        static extern void GetSystemInfo(ref SystemInfo syetemInfo); 


        const int WM_GETTEXT = 0x000D;
        const int WM_GETTEXTLENGTH = 0x000E;
        const int WM_SETTEXT = 0xC;
		const int WH_KEYBOARD_LL = 13;
		const int WM_KEYDOWN = 0x100;
		const int WM_KEYUP = 0x101;
		const int WM_SYSKEYDOWN = 0x104;

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

        public void StartKeyboardHook()
        {
            var user32 = LoadLibrary("User32");
            _hookHandle = SetWindowsHookEx(WH_KEYBOARD_LL, Hook, user32, 0);
        }    

        public void StopKeyboardHook()
        {
            UnhookWindowsHookEx(_hookHandle);
        }    

        public int Hook(int code, int wParam, ref KeyboardHookStruct lParam)
        {
            if (code >= 0)
            {

                Keys key = (Keys)lParam.vkCode;

                    KeyEventArgs kea = new KeyEventArgs(key);                              
                    if (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN)
                    {
                        KeyDown(this, kea);
                    }
      
                    if (kea.Handled)
                        return 1;
            }
            return CallNextHookEx(_hookHandle, code, wParam, ref lParam);
        }

        public event KeyEventHandler KeyDown;

        public SystemInfo GetSystemInfo()
        {
            var info = new SystemInfo();
            GetSystemInfo(ref info);
            return info;
        }
    }
}                                       


