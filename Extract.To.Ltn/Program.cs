using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Extract.To.Ltn
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var frmSet = new FrmSetDatas())
            {
                if (frmSet.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new FrmProcess(frmSet.SqlitePath, frmSet.MsSqlConnectionString, frmSet.TargetTable));
                }
            }
        }
    }
}
