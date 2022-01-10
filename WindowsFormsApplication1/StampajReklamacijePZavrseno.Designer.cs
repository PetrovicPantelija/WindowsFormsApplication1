
namespace WindowsFormsApplication1
{
    partial class StampajReklamacijePZavrseno
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.TestProcedurePalSveRBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSet7 = new WindowsFormsApplication1.DataSet7();
            this.button5 = new System.Windows.Forms.Button();
            this.cboDatumUtovara = new System.Windows.Forms.ComboBox();
            this.cboVozila = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.TestProcedurePalSveRTableAdapter = new WindowsFormsApplication1.DataSet7TableAdapters.TestProcedurePalSveRTableAdapter();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TestProcedurePalSveRBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet7)).BeginInit();
            this.SuspendLayout();
            // 
            // TestProcedurePalSveRBindingSource
            // 
            this.TestProcedurePalSveRBindingSource.DataMember = "TestProcedurePalSveR";
            this.TestProcedurePalSveRBindingSource.DataSource = this.DataSet7;
            // 
            // DataSet7
            // 
            this.DataSet7.DataSetName = "DataSet7";
            this.DataSet7.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button5.Location = new System.Drawing.Point(269, 6);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(16, 22);
            this.button5.TabIndex = 66;
            this.button5.Text = "?";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // cboDatumUtovara
            // 
            this.cboDatumUtovara.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboDatumUtovara.FormattingEnabled = true;
            this.cboDatumUtovara.Location = new System.Drawing.Point(117, 7);
            this.cboDatumUtovara.Margin = new System.Windows.Forms.Padding(2);
            this.cboDatumUtovara.Name = "cboDatumUtovara";
            this.cboDatumUtovara.Size = new System.Drawing.Size(150, 25);
            this.cboDatumUtovara.TabIndex = 65;
            // 
            // cboVozila
            // 
            this.cboVozila.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboVozila.FormattingEnabled = true;
            this.cboVozila.Location = new System.Drawing.Point(348, 10);
            this.cboVozila.Margin = new System.Windows.Forms.Padding(2);
            this.cboVozila.Name = "cboVozila";
            this.cboVozila.Size = new System.Drawing.Size(141, 25);
            this.cboVozila.TabIndex = 64;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(11, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 17);
            this.label2.TabIndex = 62;
            this.label2.Text = "Datum istovara:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(299, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 61;
            this.label1.Text = "Vozilo:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Silver;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(515, 6);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(182, 30);
            this.button1.TabIndex = 60;
            this.button1.Text = "Osveži izveštaj";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TestProcedurePalSveRTableAdapter
            // 
            this.TestProcedurePalSveRTableAdapter.ClearBeforeFill = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource5.Name = "DataSet7";
            reportDataSource5.Value = this.TestProcedurePalSveRBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "WindowsFormsApplication1.Report7.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(16, 46);
            this.reportViewer1.Margin = new System.Windows.Forms.Padding(2);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1066, 533);
            this.reportViewer1.TabIndex = 67;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(11, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 17);
            this.label3.TabIndex = 63;
            this.label3.Text = "Datum istovara:";
            // 
            // StampajReklamacijePZavrseno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1113, 737);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.cboDatumUtovara);
            this.Controls.Add(this.cboVozila);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.label3);
            this.Name = "StampajReklamacijePZavrseno";
            this.Text = "Štampaj reklamacije iz Završenog";
            this.Load += new System.EventHandler(this.StampajReklamacijePZavrseno_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TestProcedurePalSveRBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet7)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource TestProcedurePalSveRBindingSource;
        private DataSet7 DataSet7;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox cboDatumUtovara;
        private System.Windows.Forms.ComboBox cboVozila;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private DataSet7TableAdapters.TestProcedurePalSveRTableAdapter TestProcedurePalSveRTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Label label3;
    }
}