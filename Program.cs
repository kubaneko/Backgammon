using System;
using System.Windows.Forms;

namespace Backgammon
{
    static class Program
    {
        public static Form1 Form1
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
