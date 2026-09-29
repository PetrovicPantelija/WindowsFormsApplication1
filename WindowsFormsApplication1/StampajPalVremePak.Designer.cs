namespace WindowsFormsApplication1
{
    partial class StampajPalVremePak
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StampajPalVremePak));
            this.TestProcedurePalVremePakBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Dataset6 = new WindowsFormsApplication1.Dataset6();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.cboPaleta = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.cboDatumUtovara = new System.Windows.Forms.ComboBox();
            this.cboVozila = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.TestProcedurePalVremePakTableAdapter = new WindowsFormsApplication1.Dataset6TableAdapters.TestProcedurePalVremePakTableAdapter();
            this.label4 = new System.Windows.Forms.Label();
            this.cb1 = new System.Windows.Forms.CheckBox();
            this.cb2 = new System.Windows.Forms.CheckBox();
            this.cb3 = new System.Windows.Forms.CheckBox();
            this.cb4 = new System.Windows.Forms.CheckBox();
            this.cb5 = new System.Windows.Forms.CheckBox();
            this.cb6 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.TestProcedurePalVremePakBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dataset6)).BeginInit();
            this.SuspendLayout();
            // 
            // TestProcedurePalVremePakBindingSource
            // 
            this.TestProcedurePalVremePakBindingSource.DataMember = "TestProcedurePalVremePak";
            this.TestProcedurePalVremePakBindingSource.DataSource = this.Dataset6;
            // 
            // Dataset6
            // 
            this.Dataset6.DataSetName = "Dataset6";
            this.Dataset6.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(551, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.TabIndex = 60;
            this.label3.Text = "Paleta:";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button3.Location = new System.Drawing.Point(524, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(21, 32);
            this.button3.TabIndex = 59;
            this.button3.Text = "?";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cboPaleta
            // 
            this.cboPaleta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboPaleta.FormattingEnabled = true;
            this.cboPaleta.Location = new System.Drawing.Point(618, 6);
            this.cboPaleta.Name = "cboPaleta";
            this.cboPaleta.Size = new System.Drawing.Size(186, 28);
            this.cboPaleta.TabIndex = 58;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button5.Location = new System.Drawing.Point(257, 8);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(21, 27);
            this.button5.TabIndex = 57;
            this.button5.Text = "?";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // cboDatumUtovara
            // 
            this.cboDatumUtovara.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboDatumUtovara.FormattingEnabled = true;
            this.cboDatumUtovara.Location = new System.Drawing.Point(90, 9);
            this.cboDatumUtovara.Name = "cboDatumUtovara";
            this.cboDatumUtovara.Size = new System.Drawing.Size(161, 28);
            this.cboDatumUtovara.TabIndex = 56;
            // 
            // cboVozila
            // 
            this.cboVozila.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboVozila.FormattingEnabled = true;
            this.cboVozila.Location = new System.Drawing.Point(357, 7);
            this.cboVozila.Name = "cboVozila";
            this.cboVozila.Size = new System.Drawing.Size(161, 28);
            this.cboVozila.TabIndex = 55;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(20, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 20);
            this.label2.TabIndex = 54;
            this.label2.Text = "Datum:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(291, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 53;
            this.label1.Text = "Vozilo:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Silver;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(872, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(256, 37);
            this.button1.TabIndex = 52;
            this.button1.Text = "Osveži izveštaj";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "DataSet6";
            reportDataSource1.Value = this.TestProcedurePalVremePakBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "WindowsFormsApplication1.Report6.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(16, 96);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1186, 478);
            this.reportViewer1.TabIndex = 61;
            // 
            // TestProcedurePalVremePakTableAdapter
            // 
            this.TestProcedurePalVremePakTableAdapter.ClearBeforeFill = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(22, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 20);
            this.label4.TabIndex = 62;
            this.label4.Text = "Sortiraj po:";
            // 
            // cb1
            // 
            this.cb1.AutoSize = true;
            this.cb1.Location = new System.Drawing.Point(119, 58);
            this.cb1.Name = "cb1";
            this.cb1.Size = new System.Drawing.Size(43, 21);
            this.cb1.TabIndex = 63;
            this.cb1.Text = "ID";
            this.cb1.UseVisualStyleBackColor = true;
            // 
            // cb2
            // 
            this.cb2.AutoSize = true;
            this.cb2.Location = new System.Drawing.Point(192, 58);
            this.cb2.Name = "cb2";
            this.cb2.Size = new System.Drawing.Size(122, 21);
            this.cb2.TabIndex = 64;
            this.cb2.Text = "MestoIsporuke";
            this.cb2.UseVisualStyleBackColor = true;
            // 
            // cb3
            // 
            this.cb3.AutoSize = true;
            this.cb3.Location = new System.Drawing.Point(342, 58);
            this.cb3.Name = "cb3";
            this.cb3.Size = new System.Drawing.Size(192, 21);
            this.cb3.TabIndex = 65;
            this.cb3.Text = "Vreme paletiranja rastuće";
            this.cb3.UseVisualStyleBackColor = true;
            // 
            // cb4
            // 
            this.cb4.AutoSize = true;
            this.cb4.Location = new System.Drawing.Point(828, 58);
            this.cb4.Name = "cb4";
            this.cb4.Size = new System.Drawing.Size(107, 21);
            this.cb4.TabIndex = 66;
            this.cb4.Text = "Broj utovara";
            this.cb4.UseVisualStyleBackColor = true;
            // 
            // cb5
            // 
            this.cb5.AutoSize = true;
            this.cb5.Location = new System.Drawing.Point(968, 58);
            this.cb5.Name = "cb5";
            this.cb5.Size = new System.Drawing.Size(109, 21);
            this.cb5.TabIndex = 67;
            this.cb5.Text = "Broj istovara";
            this.cb5.UseVisualStyleBackColor = true;
            // 
            // cb6
            // 
            this.cb6.AutoSize = true;
            this.cb6.Location = new System.Drawing.Point(574, 58);
            this.cb6.Name = "cb6";
            this.cb6.Size = new System.Drawing.Size(211, 21);
            this.cb6.TabIndex = 68;
            this.cb6.Text = "Vreme paletiranja opadajuće";
            this.cb6.UseVisualStyleBackColor = true;
            // 
            // StampajPalVremePak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1214, 586);
            this.Controls.Add(this.cb6);
            this.Controls.Add(this.cb5);
            this.Controls.Add(this.cb4);
            this.Controls.Add(this.cb3);
            this.Controls.Add(this.cb2);
            this.Controls.Add(this.cb1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.cboPaleta);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.cboDatumUtovara);
            this.Controls.Add(this.cboVozila);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Icon = global::WindowsFormsApplication1.Properties.Resources.JelaIkona;
            this.Name = "StampajPalVremePak";
            this.Text = "Štampaj paletu po vremenu pakovanja";
            this.Load += new System.EventHandler(this.StampajPalVremePak_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TestProcedurePalVremePakBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dataset6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox cboPaleta;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox cboDatumUtovara;
        private System.Windows.Forms.ComboBox cboVozila;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource TestProcedurePalVremePakBindingSource;
        private Dataset6 Dataset6;
        private Dataset6TableAdapters.TestProcedurePalVremePakTableAdapter TestProcedurePalVremePakTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cb1;
        private System.Windows.Forms.CheckBox cb2;
        private System.Windows.Forms.CheckBox cb3;
        private System.Windows.Forms.CheckBox cb4;
        private System.Windows.Forms.CheckBox cb5;
        private System.Windows.Forms.CheckBox cb6;
    }
}