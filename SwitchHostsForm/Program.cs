using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SwitchHostsForm;
using System.Threading;
using System.Configuration;
using System.Drawing;

namespace SwitchHostsForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static Mutex mutex = new Mutex(true, "{8F6F0BC4-B9A1-45fd-A8CF-72F04E6BDE8F}");
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                MainForm main = new MainForm();
                Application.Run(main);                
                mutex.ReleaseMutex();
            } else {
                MessageBox.Show("Only one instance at a time");
            }
        }
    }
}
