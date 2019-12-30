using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PacketTextConverter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string[] buf = Environment.GetCommandLineArgs(); //пкс
            List<string> cline = new List<string>();

            foreach (string s in buf)
            {
                cline.Add(s.ToLowerInvariant());
            }

            if (cline.Contains("/np")) //не портабельный режим
            {
                //данные в C:\Users\<пользователь>\AppData\Local\BatchTextConverter\
                CommonFunctions.SettingsPath = CommonFunctions.AddSlash(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)) +
                    "BatchTextConverter\\";                
            }
            else
            {
                //данные в папке с экзешником
                CommonFunctions.SettingsPath = CommonFunctions.AddSlash(Application.StartupPath);                

            }

            if (cline.Contains("/?") || cline.Contains("/h") ||
                cline.Contains("/help") || cline.Contains("-help") ||
                cline.Contains("--help") || cline.Contains("-h"))
            {
                frmReadme fReadme = new frmReadme();
                fReadme.ShowDialog();
                return;
            }            

            Application.Run(new frmMain());
        }
    }
}
