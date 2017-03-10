using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardDataReader
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
            //Application.Run(new Form1());
            CardDataStore store = new CardDataStore();
            Clipboard.SetText(store.DownloadPictures(@"data\CardData.xml"));
            MessageBox.Show("OK");
        }
    }
}
