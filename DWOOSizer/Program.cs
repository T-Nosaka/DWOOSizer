using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DWOOSizer
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm ap = new TabMainForm();

            //初期ロードがある場合
            if (args.Length > 0)
            {
                ap.LoadFile(args[0]);
            }

            Application.Run(ap);
        }
    }
}
