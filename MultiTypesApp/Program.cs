using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiTypesApp
{

    static class Program
    {

        #region PInvoke Methods (We will discuss this later! :))
        [DllImport("kernel32", SetLastError = true)]
        private static extern bool AttachConsole(int dwProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool FreeConsole();

        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();
        #endregion
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            string mode = args.Length > 0 ? args[0] : "gui";
            if (mode == "gui")
            {
                // Open windows form as our GUI
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else if (mode == "console")
            {
                // Get a pointer to the forground window.  The idea here is that
                // IF the user is starting our application from an existing console
                // shell, that shell will be the uppermost window.  We'll get it
                // and attach to it
                IntPtr ptr = GetForegroundWindow();
                // We will insert the curent thread ID into this var
                int u;
                // Get the process ID, using our pointer
                GetWindowThreadProcessId(ptr, out u);
                // Get process element using the process ID
                Process process = Process.GetProcessById(u);

                if (process.ProcessName == "cmd" || process.ProcessName == "powershell")
                {
                    // Use Current shell as our GUI
                    AttachConsole(process.Id);
                    //we have a console to attach to ..
                    Console.WriteLine("hello. It looks like you started me from an existing console (or powershell).");
                    // if we want to close and go back to CMD we can use
                    SendKeys.SendWait("{ENTER}");
                }
                else
                {
                    //Open New console shell as our GUI.
                    AllocConsole();

                    Console.WriteLine(@"hello. It looks like you double clicked me to start AND you want console mode.  Here's a new console.");
                    Console.WriteLine("press any key to continue ...");
                    Console.ReadLine();
                }
                FreeConsole();
            }
        }
    }
}
