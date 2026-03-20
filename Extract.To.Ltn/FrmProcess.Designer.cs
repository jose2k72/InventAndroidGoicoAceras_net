namespace Extract.To.Ltn
{
    partial class FrmProcess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLogInfo = new System.Windows.Forms.RichTextBox();
            this.txtLogWarn = new System.Windows.Forms.RichTextBox();
            this.txtLogErr = new System.Windows.Forms.RichTextBox();
            this.btnExportInfo = new System.Windows.Forms.Button();
            this.btnExportWarn = new System.Windows.Forms.Button();
            this.btnExportErr = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnParar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtLogInfo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnExportInfo, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtLogWarn, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnExportWarn, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtLogErr, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.btnExportErr, 0, 8);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(960, 488);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Log de Ejecución";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.DarkOrange;
            this.label2.Location = new System.Drawing.Point(3, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Warnings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(3, 318);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Errores";
            // 
            // txtLogInfo
            // 
            this.txtLogInfo.BackColor = System.Drawing.Color.Black;
            this.txtLogInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogInfo.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.txtLogInfo.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtLogInfo.Location = new System.Drawing.Point(3, 23);
            this.txtLogInfo.Name = "txtLogInfo";
            this.txtLogInfo.ReadOnly = true;
            this.txtLogInfo.Size = new System.Drawing.Size(954, 103);
            this.txtLogInfo.TabIndex = 1;
            this.txtLogInfo.Text = "";
            // 
            // txtLogWarn
            // 
            this.txtLogWarn.BackColor = System.Drawing.Color.Black;
            this.txtLogWarn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogWarn.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.txtLogWarn.ForeColor = System.Drawing.Color.Orange;
            this.txtLogWarn.Location = new System.Drawing.Point(3, 182);
            this.txtLogWarn.Name = "txtLogWarn";
            this.txtLogWarn.ReadOnly = true;
            this.txtLogWarn.Size = new System.Drawing.Size(954, 103);
            this.txtLogWarn.TabIndex = 4;
            this.txtLogWarn.Text = "";
            // 
            // txtLogErr
            // 
            this.txtLogErr.BackColor = System.Drawing.Color.Black;
            this.txtLogErr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogErr.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.txtLogErr.ForeColor = System.Drawing.Color.LightCoral;
            this.txtLogErr.Location = new System.Drawing.Point(3, 341);
            this.txtLogErr.Name = "txtLogErr";
            this.txtLogErr.ReadOnly = true;
            this.txtLogErr.Size = new System.Drawing.Size(954, 103);
            this.txtLogErr.TabIndex = 7;
            this.txtLogErr.Text = "";
            // 
            // btnExportInfo
            // 
            this.btnExportInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportInfo.Enabled = false;
            this.btnExportInfo.Location = new System.Drawing.Point(807, 132);
            this.btnExportInfo.Name = "btnExportInfo";
            this.btnExportInfo.Size = new System.Drawing.Size(150, 24);
            this.btnExportInfo.TabIndex = 2;
            this.btnExportInfo.Text = "Exportar Log...";
            this.btnExportInfo.UseVisualStyleBackColor = true;
            this.btnExportInfo.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnExportWarn
            // 
            this.btnExportWarn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportWarn.Enabled = false;
            this.btnExportWarn.Location = new System.Drawing.Point(807, 291);
            this.btnExportWarn.Name = "btnExportWarn";
            this.btnExportWarn.Size = new System.Drawing.Size(150, 24);
            this.btnExportWarn.TabIndex = 5;
            this.btnExportWarn.Text = "Exportar Warnings...";
            this.btnExportWarn.UseVisualStyleBackColor = true;
            this.btnExportWarn.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnExportErr
            // 
            this.btnExportErr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportErr.Enabled = false;
            this.btnExportErr.Location = new System.Drawing.Point(807, 450);
            this.btnExportErr.Name = "btnExportErr";
            this.btnExportErr.Size = new System.Drawing.Size(150, 24);
            this.btnExportErr.TabIndex = 8;
            this.btnExportErr.Text = "Exportar Errores...";
            this.btnExportErr.UseVisualStyleBackColor = true;
            this.btnExportErr.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnParar);
            this.panel1.Controls.Add(this.btnCerrar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 506);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 55);
            this.panel1.TabIndex = 1;
            // 
            // btnParar
            // 
            this.btnParar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnParar.Location = new System.Drawing.Point(12, 10);
            this.btnParar.Name = "btnParar";
            this.btnParar.Size = new System.Drawing.Size(120, 35);
            this.btnParar.TabIndex = 0;
            this.btnParar.Text = "Parar Proceso";
            this.btnParar.UseVisualStyleBackColor = true;
            this.btnParar.Click += new System.EventHandler(this.btnParar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Enabled = false;
            this.btnCerrar.Location = new System.Drawing.Point(852, 10);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(120, 35);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // FrmProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmProcess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proceso de Extracción y Migración";
            this.Load += new System.EventHandler(this.FrmProcess_Load);
            this.Shown += new System.EventHandler(this.FrmProcess_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox txtLogInfo;
        private System.Windows.Forms.RichTextBox txtLogWarn;
        private System.Windows.Forms.RichTextBox txtLogErr;
        private System.Windows.Forms.Button btnExportInfo;
        private System.Windows.Forms.Button btnExportWarn;
        private System.Windows.Forms.Button btnExportErr;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnParar;
        private System.Windows.Forms.Button btnCerrar;
    }
}

