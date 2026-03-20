using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Extract.To.Ltn
{
    public partial class FrmSetDatas : Form
    {
        public string SqlitePath { get; private set; }
        public string MsSqlConnectionString { get; private set; }
        public string TargetTable { get; private set; }

        public FrmSetDatas()
        {
            InitializeComponent();
            this.txtTargetTable.Text = "AceraTable";
        }

        private void btnBrowseSqlite_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos SQLite (*.db;*.sqlite)|*.db;*.sqlite|Todos los archivos (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtSqlitePath.Text = ofd.FileName;
                }
            }
        }

        private void btnTestConn_Click(object sender, EventArgs e)
        {
            // Abrimos el explorador de SQL pasando la configuración actual si existe
            using (var frm = new FrmMsSqlOpen(txtMsSqlConnString.Text, txtTargetTable.Text))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    txtMsSqlConnString.Text = frm.ConnectionString;
                    txtTargetTable.Text = frm.SelectedTable;
                }
            }
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSqlitePath.Text))
            {
                MessageBox.Show("Debe seleccionar un archivo SQLite de origen.", "Faltan Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMsSqlConnString.Text) || string.IsNullOrWhiteSpace(txtTargetTable.Text))
            {
                MessageBox.Show("Debe configurar el destino de SQL Server.", "Faltan Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Exportamos los datos seleccionados
            this.SqlitePath = txtSqlitePath.Text;
            this.MsSqlConnectionString = txtMsSqlConnString.Text;
            this.TargetTable = txtTargetTable.Text;

            // Cerramos con OK para que Program.cs lance la siguiente forma
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
