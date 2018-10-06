using System;
using System.Threading;
using System.Windows.Forms;

namespace GearBox
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Restrict to show only one GearBox window
            Mutex m = new Mutex(false, "GearBoxGUI", out bool canCreateWindow);

            if (!canCreateWindow)
            {
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GearBoxGUI(args));
        }
    }
}
