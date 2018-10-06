using System;
using System.Collections.Generic;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GearBoxGUI(args));
        }
    }
}
