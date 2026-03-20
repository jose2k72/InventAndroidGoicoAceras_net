using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using Dapper;
using Microsoft.VisualBasic;

namespace Extract.To.Ltn
{
    public partial class FrmMsSqlOpen : Form
    {
        public string ConnectionString { get; private set; }
        public string SelectedTable { get; private set; }

        private string _currentConnectionString = string.Empty;
        public List<string> CreatedDatabases { get; } = new List<string>();
        public List<KeyValuePair<string, string>> CreatedTables { get; } = new List<KeyValuePair<string, string>>();

        private string _initialTable = string.Empty;
        private string _initialCatalog = string.Empty;

        public FrmMsSqlOpen(string initialConnString = null, string initialTable = null)
        {
            InitializeComponent();
            _initialTable = initialTable ?? string.Empty;

            if (!string.IsNullOrEmpty(initialConnString))
            {
                InitializeFormFromConnString(initialConnString);
            }
        }

        private void FrmMsSqlOpen_Load(object sender, EventArgs e)
        {
            this.FormFixedNoGrow();
            chkWindowsAuth_CheckedChanged(null, null);

            // Iniciar deshabilitados hasta que haya conexión efectiva
            btnNewDb.Enabled = false;
            btnNewTable.Enabled = false;
            
            // Si el constructor recibió una cadena de conexión, refrescamos automáticamente
            if (!string.IsNullOrWhiteSpace(txtServer.Text))
            {
                RefreshDatabases(_initialCatalog);
            }
        }

        private void chkWindowsAuth_CheckedChanged(object sender, EventArgs e)
        {
            gbSqlAuth.Enabled = !chkWindowsAuth.Checked;
        }

        private void btnRefreshDbs_Click(object sender, EventArgs e)
        {
            RefreshDatabases();
        }

        private void InitializeFormFromConnString(string connString)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connString);
                txtServer.Text = builder.DataSource;
                _initialCatalog = builder.InitialCatalog;
                
                if (builder.IntegratedSecurity)
                {
                    chkWindowsAuth.Checked = true;
                    txtUser.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                }
                else
                {
                    chkWindowsAuth.Checked = false;
                    txtUser.Text = builder.UserID;
                    txtPassword.Text = builder.Password;
                }

                // Si ya tenemos servidor y/o base de datos desde la cadena, habilitamos proactivamente los botones
                if (!string.IsNullOrWhiteSpace(txtServer.Text))
                {
                    btnNewDb.Enabled = true;
                }

                if (!string.IsNullOrWhiteSpace(_initialCatalog))
                {
                    btnNewTable.Enabled = true;
                }
            }
            catch { /* Ignorar si la cadena de conexión es inválida */ }
        }

        private void RefreshDatabases(string selectTarget = null)
        {
            if (string.IsNullOrWhiteSpace(txtServer.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre del servidor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si ya existe una conexión activa, realizamos limpieza de lo creado antes de continuar
            if (!string.IsNullOrEmpty(_currentConnectionString))
            {
                PerformCleanup();
                _currentConnectionString = string.Empty;
            }

            string masterConnStr = BuildConnectionString("master");
            lstDatabases.Items.Clear();
            lstTables.Items.Clear();

            try
            {
                using (SqlConnection conn = new SqlConnection(masterConnStr))
                {
                    conn.Open();
                    var databases = conn.Query<string>("SELECT name FROM sys.databases WHERE database_id > 4 ORDER BY name");

                    if (databases.Any())
                    {
                        lstDatabases.Items.AddRange(databases.ToArray());
                        
                        if (!string.IsNullOrEmpty(selectTarget))
                        {
                            int index = lstDatabases.FindStringExact(selectTarget);
                            if (index != -1) lstDatabases.SelectedIndex = index;
                        }

                        // Almacenamos que hay una conexión establecida con éxito
                        _currentConnectionString = masterConnStr;
                        btnNewDb.Enabled = true; // Habilitar creación de DB
                    }
                }
            }
            catch (Exception ex)
            {
                btnNewDb.Enabled = false;
                btnNewTable.Enabled = false;
                MessageBox.Show($"Error al cargar bases de datos: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se cambia el catálogo, si tenemos una tabla inicial pendiente, la usamos
            RefreshTables(_initialTable);
            
            // Una vez usada la tabla inicial por primera vez, la limpiamos para no interferir con futuras selecciones manuales
            _initialTable = string.Empty;
        }

        private void RefreshTables(string selectTarget = null)
        {
            if (lstDatabases.SelectedItem == null) 
            {
                btnNewTable.Enabled = false;
                return;
            }

            string dbName = lstDatabases.SelectedItem.ToString();
            string dbConnStr = BuildConnectionString(dbName);
            lstTables.Items.Clear();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbConnStr))
                {
                    conn.Open();
                    var tables = conn.Query<string>("SELECT name FROM sys.tables ORDER BY name");
                    lstTables.Items.AddRange(tables.ToArray());

                    if (!string.IsNullOrEmpty(selectTarget))
                    {
                        int index = lstTables.FindStringExact(selectTarget);
                        if (index != -1) lstTables.SelectedIndex = index;
                    }

                    btnNewTable.Enabled = true; // Habilitar creación de tablas en esta DB
                }
            }
            catch (Exception ex)
            {
                btnNewTable.Enabled = false;
                MessageBox.Show($"Error al cargar tablas de {dbName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNewDb_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtServer.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre del servidor para crear una Base de Datos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string dbName = Interaction.InputBox("Nombre de la nueva Base de Datos:", "Nueva DB", "NuevaDB_" + DateTime.Now.ToString("yyyyMMdd"));
            
            if (string.IsNullOrWhiteSpace(dbName))
            {
                MessageBox.Show("La creación de la base de datos ha sido cancelada o el nombre ingresado está vacío.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string masterConnStr = BuildConnectionString("master");
                using (SqlConnection conn = new SqlConnection(masterConnStr))
                {
                    conn.Open();
                    conn.Execute($"CREATE DATABASE [{dbName}]");
                }
                
                RefreshDatabases(dbName);
                CreatedDatabases.Add(dbName); // Tracking database creation
                MessageBox.Show($"Base de datos '{dbName}' creada con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNewTable_Click(object sender, EventArgs e)
        {
            if (lstDatabases.SelectedItem == null)
            {
                MessageBox.Show("Primero seleccione una base de datos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tableName = Interaction.InputBox("Nombre de la nueva tabla (se creará vacía):", "Nueva Tabla", "NuevaTabla_" + DateTime.Now.ToString("HHmmss"));
            
            if (string.IsNullOrWhiteSpace(tableName))
            {
                MessageBox.Show("La creación de la tabla ha sido cancelada o el nombre ingresado está vacío.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string dbName = lstDatabases.SelectedItem.ToString();
                string dbConnStr = BuildConnectionString(dbName);

                using (SqlConnection conn = new SqlConnection(dbConnStr))
                {
                    conn.Open();
                    // Crear una tabla mock solo con un ID
                    string sql = $@"
                    CREATE TABLE [{tableName}] (
                        [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1)
                    )";
                    conn.Execute(sql);
                }

                CreatedTables.Add(new KeyValuePair<string, string>(dbName, tableName)); // Tracking table creation
                RefreshTables(tableName);
                MessageBox.Show($"Tabla '{tableName}' creada satisfactoriamente en '{dbName}'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear la tabla: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string BuildConnectionString(string initialCatalog = null)
        {
            string db = initialCatalog ?? (lstDatabases.SelectedItem?.ToString() ?? "master");

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = txtServer.Text,
                InitialCatalog = db,
                TrustServerCertificate = true,
                MultipleActiveResultSets = true,
                ConnectTimeout = 10
            };

            if (chkWindowsAuth.Checked)
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.UserID = txtUser.Text;
                builder.Password = txtPassword.Text;
                builder.IntegratedSecurity = false;
            }

            return builder.ToString();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (lstDatabases.SelectedItem == null || lstTables.SelectedItem == null)
            {
                MessageBox.Show("Para continuar, debe seleccionar tanto una Base de Datos como una Tabla de destino.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string currentDb = lstDatabases.SelectedItem.ToString();
            string currentTable = lstTables.SelectedItem?.ToString() ?? string.Empty;

            // Antes de aceptar, limpiamos todo lo creado EXCEPTO lo que se seleccionó formalmente
            // Si lo seleccionado está en las listas de seguimiento, NO se borra del servidor.
            PerformCleanup(currentDb, currentTable);

            this.ConnectionString = BuildConnectionString(currentDb);
            this.SelectedTable = currentTable;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // En cancelar, limpiamos TODO lo creado sin excepciones
            PerformCleanup();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void PerformCleanup(string excludeDb = null, string excludeTable = null)
        {
            try
            {
                foreach (var tableEntry in CreatedTables)
                {
                    if (excludeDb != null && excludeTable != null &&
                        tableEntry.Key.Equals(excludeDb, StringComparison.OrdinalIgnoreCase) && 
                        tableEntry.Value.Equals(excludeTable, StringComparison.OrdinalIgnoreCase))
                    {
                        continue; 
                    }

                    try
                    {
                        string dbConnStr = BuildConnectionString(tableEntry.Key);
                        using (SqlConnection conn = new SqlConnection(dbConnStr))
                        {
                            conn.Open();
                            conn.Execute($"DROP TABLE IF EXISTS [{tableEntry.Value}]");
                        }
                    }
                    catch { }
                }

                string masterConnStr = BuildConnectionString("master");
                using (SqlConnection conn = new SqlConnection(masterConnStr))
                {
                    conn.Open();
                    foreach (var dbName in CreatedDatabases)
                    {
                        if (dbName.Equals(excludeDb, StringComparison.OrdinalIgnoreCase)) continue;

                        try
                        {
                            conn.Execute($"ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [{dbName}]");
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }
    }
}
