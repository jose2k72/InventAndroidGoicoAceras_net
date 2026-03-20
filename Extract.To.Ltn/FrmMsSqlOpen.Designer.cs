namespace Extract.To.Ltn
{
    partial class FrmMsSqlOpen
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkWindowsAuth = new System.Windows.Forms.CheckBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbSqlAuth = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRefreshDbs = new System.Windows.Forms.Button();
            this.lstDatabases = new System.Windows.Forms.ListBox();
            this.lstTables = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnNewDb = new System.Windows.Forms.Button();
            this.btnNewTable = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.gbSqlAuth.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkWindowsAuth);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(660, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuración del Servidor";
            // 
            // chkWindowsAuth
            // 
            this.chkWindowsAuth.AutoSize = true;
            this.chkWindowsAuth.Location = new System.Drawing.Point(400, 42);
            this.chkWindowsAuth.Name = "chkWindowsAuth";
            this.chkWindowsAuth.Size = new System.Drawing.Size(125, 17);
            this.chkWindowsAuth.TabIndex = 2;
            this.chkWindowsAuth.Text = "Auth. de Windows";
            this.chkWindowsAuth.UseVisualStyleBackColor = true;
            this.chkWindowsAuth.CheckedChanged += new System.EventHandler(this.chkWindowsAuth_CheckedChanged);
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(10, 40);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(370, 20);
            this.txtServer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Servidor (Dirección/Instancia):";
            // 
            // gbSqlAuth
            // 
            this.gbSqlAuth.Controls.Add(this.txtPassword);
            this.gbSqlAuth.Controls.Add(this.label3);
            this.gbSqlAuth.Controls.Add(this.txtUser);
            this.gbSqlAuth.Controls.Add(this.label2);
            this.gbSqlAuth.Location = new System.Drawing.Point(12, 110);
            this.gbSqlAuth.Name = "gbSqlAuth";
            this.gbSqlAuth.Size = new System.Drawing.Size(660, 80);
            this.gbSqlAuth.TabIndex = 1;
            this.gbSqlAuth.TabStop = false;
            this.gbSqlAuth.Text = "Credenciales SQL Server";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(340, 40);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(304, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(340, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Contraseña:";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(10, 40);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(310, 20);
            this.txtUser.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Usuario:";
            // 
            // btnRefreshDbs
            // 
            this.btnRefreshDbs.Location = new System.Drawing.Point(12, 210);
            this.btnRefreshDbs.Name = "btnRefreshDbs";
            this.btnRefreshDbs.Size = new System.Drawing.Size(180, 24);
            this.btnRefreshDbs.TabIndex = 2;
            this.btnRefreshDbs.Text = "Refrescar Bases de Datos";
            this.btnRefreshDbs.UseVisualStyleBackColor = true;
            this.btnRefreshDbs.Click += new System.EventHandler(this.btnRefreshDbs_Click);
            // 
            // lstDatabases
            // 
            this.lstDatabases.FormattingEnabled = true;
            this.lstDatabases.Location = new System.Drawing.Point(12, 260);
            this.lstDatabases.Name = "lstDatabases";
            this.lstDatabases.Size = new System.Drawing.Size(310, 212);
            this.lstDatabases.TabIndex = 3;
            this.lstDatabases.SelectedIndexChanged += new System.EventHandler(this.lstDatabases_SelectedIndexChanged);
            // 
            // lstTables
            // 
            this.lstTables.FormattingEnabled = true;
            this.lstTables.Location = new System.Drawing.Point(352, 260);
            this.lstTables.Name = "lstTables";
            this.lstTables.Size = new System.Drawing.Size(320, 212);
            this.lstTables.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 244);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Bases de Datos ⬇";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(352, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Tablas Disponibles ⬇";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(462, 530);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(100, 30);
            this.btnAceptar.TabIndex = 9;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(572, 530);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 30);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnNewDb
            // 
            this.btnNewDb.Location = new System.Drawing.Point(12, 478);
            this.btnNewDb.Name = "btnNewDb";
            this.btnNewDb.Size = new System.Drawing.Size(140, 24);
            this.btnNewDb.TabIndex = 7;
            this.btnNewDb.Text = "Nueva Base de Datos...";
            this.btnNewDb.UseVisualStyleBackColor = true;
            this.btnNewDb.Click += new System.EventHandler(this.btnNewDb_Click);
            // 
            // btnNewTable
            // 
            this.btnNewTable.Location = new System.Drawing.Point(352, 478);
            this.btnNewTable.Name = "btnNewTable";
            this.btnNewTable.Size = new System.Drawing.Size(140, 24);
            this.btnNewTable.TabIndex = 8;
            this.btnNewTable.Text = "Nueva Tabla...";
            this.btnNewTable.UseVisualStyleBackColor = true;
            this.btnNewTable.Click += new System.EventHandler(this.btnNewTable_Click);
            // 
            // FrmMsSqlOpen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 570);
            this.Controls.Add(this.btnNewTable);
            this.Controls.Add(this.btnNewDb);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lstTables);
            this.Controls.Add(this.lstDatabases);
            this.Controls.Add(this.btnRefreshDbs);
            this.Controls.Add(this.gbSqlAuth);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMsSqlOpen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Explorador de SQL Server: Seleccionar Base de Datos y Tabla";
            this.Load += new System.EventHandler(this.FrmMsSqlOpen_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbSqlAuth.ResumeLayout(false);
            this.gbSqlAuth.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.CheckBox chkWindowsAuth;
        private System.Windows.Forms.GroupBox gbSqlAuth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnRefreshDbs;
        private System.Windows.Forms.ListBox lstDatabases;
        private System.Windows.Forms.ListBox lstTables;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnNewDb;
        private System.Windows.Forms.Button btnNewTable;
    }
}