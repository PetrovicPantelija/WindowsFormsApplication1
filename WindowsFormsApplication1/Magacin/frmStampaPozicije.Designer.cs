namespace WindowsFormsApplication1.Magacin
{
    partial class frmStampaPozicije
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtBarkodPozicije = new System.Windows.Forms.TextBox();
            this.txtPozicija = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboMaticni = new System.Windows.Forms.ComboBox();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.PozicijaBarkodBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.perftech_BeogradDataSet = new WindowsFormsApplication1.Perftech_BeogradDataSet();
            this.perftechBeogradDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cboPozicija = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboSkladiste = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PozicijaBarkodBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.perftech_BeogradDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.perftechBeogradDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(598, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Štampaj";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtBarkodPozicije
            // 
            this.txtBarkodPozicije.Location = new System.Drawing.Point(341, 132);
            this.txtBarkodPozicije.Name = "txtBarkodPozicije";
            this.txtBarkodPozicije.Size = new System.Drawing.Size(100, 20);
            this.txtBarkodPozicije.TabIndex = 1;
            // 
            // txtPozicija
            // 
            this.txtPozicija.Location = new System.Drawing.Point(341, 105);
            this.txtPozicija.Name = "txtPozicija";
            this.txtPozicija.Size = new System.Drawing.Size(100, 20);
            this.txtPozicija.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(291, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Oznaka";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(294, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Barkod";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(289, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 214;
            this.label5.Text = "Pozicija:";
            // 
            // cboMaticni
            // 
            this.cboMaticni.FormattingEnabled = true;
            this.cboMaticni.Location = new System.Drawing.Point(341, 68);
            this.cboMaticni.Name = "cboMaticni";
            this.cboMaticni.Size = new System.Drawing.Size(159, 21);
            this.cboMaticni.TabIndex = 213;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.PozicijaBarkodBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "WindowsFormsApplication1.Magacin.rptBarkodPozicija.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 173);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(790, 145);
            this.reportViewer1.TabIndex = 215;
            // 
            // PozicijaBarkodBindingSource
            // 
            this.PozicijaBarkodBindingSource.DataMember = "PozicijaBarkod";
            this.PozicijaBarkodBindingSource.DataSource = this.perftech_BeogradDataSet;
            // 
            // perftech_BeogradDataSet
            // 
            this.perftech_BeogradDataSet.DataSetName = "Perftech_BeogradDataSet";
            this.perftech_BeogradDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // perftechBeogradDataSetBindingSource
            // 
            this.perftechBeogradDataSetBindingSource.DataSource = this.perftech_BeogradDataSet;
            this.perftechBeogradDataSetBindingSource.Position = 0;
            // 
            // cboPozicija
            // 
            this.cboPozicija.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboPozicija.FormattingEnabled = true;
            this.cboPozicija.Location = new System.Drawing.Point(11, 121);
            this.cboPozicija.Name = "cboPozicija";
            this.cboPozicija.Size = new System.Drawing.Size(238, 24);
            this.cboPozicija.TabIndex = 251;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 250;
            this.label3.Text = "Pozicija iz:";
            // 
            // cboSkladiste
            // 
            this.cboSkladiste.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboSkladiste.ForeColor = System.Drawing.Color.Black;
            this.cboSkladiste.FormattingEnabled = true;
            this.cboSkladiste.Location = new System.Drawing.Point(11, 65);
            this.cboSkladiste.Name = "cboSkladiste";
            this.cboSkladiste.Size = new System.Drawing.Size(238, 24);
            this.cboSkladiste.TabIndex = 249;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 16);
            this.label4.TabIndex = 248;
            this.label4.Text = "Skladište iz:";
            // 
            // frmStampaPozicije
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(832, 369);
            this.Controls.Add(this.cboPozicija);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboSkladiste);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboMaticni);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPozicija);
            this.Controls.Add(this.txtBarkodPozicije);
            this.Controls.Add(this.button1);
            this.Name = "frmStampaPozicije";
            this.Text = "Štampa barkoda pozicije";
            this.Load += new System.EventHandler(this.frmStampaPozicije_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PozicijaBarkodBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.perftech_BeogradDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.perftechBeogradDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtBarkodPozicije;
        private System.Windows.Forms.TextBox txtPozicija;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboMaticni;
        private Perftech_BeogradDataSet perftech_BeogradDataSet;
        private System.Windows.Forms.BindingSource perftechBeogradDataSetBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource PozicijaBarkodBindingSource;
        private System.Windows.Forms.ComboBox cboPozicija;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboSkladiste;
        private System.Windows.Forms.Label label4;
    }
}