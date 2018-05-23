using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SaledServices
{
    public static class Program
    {
        public static Form parentForm = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            parentForm = new MainForm();

            

            Application.Run(parentForm);
        }
    }
}
