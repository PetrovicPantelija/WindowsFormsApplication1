
namespace WindowsFormsApplication1
{
    partial class StampajPalZavrseno
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource9 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource10 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.TestProcedurePalTableAdapter = new WindowsFormsApplication1.DataSet2TableAdapters.TestProcedurePalTableAdapter();
            this.cb3 = new System.Windows.Forms.CheckBox();
            this.cb2 = new System.Windows.Forms.CheckBox();
            this.cb1 = new System.Windows.Forms.CheckBox();
            this.TestProcedureTableAdapter = new WindowsFormsApplication1.MyDataSetTableAdapters.TestProcedureTableAdapter();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.TestProcedurePalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSet2 = new WindowsFormsApplication1.DataSet2();
            this.TestProcedureBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MyDataSet = new WindowsFormsApplication1.MyDataSet();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.cboPaleta = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.cboDatumUtovara = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cboVozila = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.TestProcedurePalBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TestProcedureBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MyDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // TestProcedurePalTableAdapter
            // 
            this.TestProcedurePalTableAdapter.ClearBeforeFill = true;
            // 
            // cb3
            // 
            this.cb3.AutoSize = true;
            this.cb3.Location = new System.Drawing.Point(321, 40);
            this.cb3.Margin = new System.Windows.Forms.Padding(2);
            this.cb3.Name = "cb3";
            this.cb3.Size = new System.Drawing.Size(160, 17);
            this.cb3.TabIndex = 81;
            this.cb3.Text = "Vreme paletiranja opadajuće";
            this.cb3.UseVisualStyleBackColor = true;
            // 
            // cb2
            // 
            this.cb2.AutoSize = true;
            this.cb2.Location = new System.Drawing.Point(134, 38);
            this.cb2.Margin = new System.Windows.Forms.Padding(2);
            this.cb2.Name = "cb2";
            this.cb2.Size = new System.Drawing.Size(145, 17);
            this.cb2.TabIndex = 80;
            this.cb2.Text = "Vreme paletiranja rastuće";
            this.cb2.UseVisualStyleBackColor = true;
            // 
            // cb1
            // 
            this.cb1.AutoSize = true;
            this.cb1.Location = new System.Drawing.Point(18, 40);
            this.cb1.Margin = new System.Windows.Forms.Padding(2);
            this.cb1.Name = "cb1";
            this.cb1.Size = new System.Drawing.Size(59, 17);
            this.cb1.TabIndex = 79;
            this.cb1.Text = "Normal";
            this.cb1.UseVisualStyleBackColor = true;
            // 
            // TestProcedureTableAdapter
            // 
            this.TestProcedureTableAdapter.ClearBeforeFill = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource9.Name = "DataSet2";
            reportDataSource9.Value = this.TestProcedurePalBindingSource;
            reportDataSource10.Name = "DataSet1";
            reportDataSource10.Value = this.TestProcedureBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource9);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource10);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "WindowsFormsApplication1.Report2.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(7, 61);
            this.reportViewer1.Margin = new System.Windows.Forms.Padding(2);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1041, 677);
            this.reportViewer1.TabIndex = 82;
            // 
            // TestProcedurePalBindingSource
            // 
            this.TestProcedurePalBindingSource.DataMember = "TestProcedurePal";
            this.TestProcedurePalBindingSource.DataSource = this.DataSet2;
            // 
            // DataSet2
            // 
            this.DataSet2.DataSetName = "DataSet2";
            this.DataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // TestProcedureBindingSource
            // 
            this.TestProcedureBindingSource.DataMember = "TestProcedure";
            this.TestProcedureBindingSource.DataSource = this.MyDataSet;
            // 
            // MyDataSet
            // 
            this.MyDataSet.DataSetName = "MyDataSet";
            this.MyDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(437, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 17);
            this.label3.TabIndex = 78;
            this.label3.Text = "Paleta:";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button3.Location = new System.Drawing.Point(397, 9);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(16, 26);
            this.button3.TabIndex = 77;
            this.button3.Text = "?";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cboPaleta
            // 
            this.cboPaleta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboPaleta.FormattingEnabled = true;
            this.cboPaleta.Location = new System.Drawing.Point(487, 11);
            this.cboPaleta.Margin = new System.Windows.Forms.Padding(2);
            this.cboPaleta.Name = "cboPaleta";
            this.cboPaleta.Size = new System.Drawing.Size(140, 25);
            this.cboPaleta.TabIndex = 76;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button5.Location = new System.Drawing.Point(191, 11);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(16, 22);
            this.button5.TabIndex = 75;
            this.button5.Text = "?";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // cboDatumUtovara
            // 
            this.cboDatumUtovara.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboDatumUtovara.FormattingEnabled = true;
            this.cboDatumUtovara.Location = new System.Drawing.Point(66, 10);
            this.cboDatumUtovara.Margin = new System.Windows.Forms.Padding(2);
            this.cboDatumUtovara.Name = "cboDatumUtovara";
            this.cboDatumUtovara.Size = new System.Drawing.Size(122, 25);
            this.cboDatumUtovara.TabIndex = 74;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(11, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 17);
            this.label2.TabIndex = 72;
            this.label2.Text = "Datum:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(211, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 71;
            this.label1.Text = "Vozilo:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Silver;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(677, 7);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 30);
            this.button1.TabIndex = 70;
            this.button1.Text = "Osveži izveštaj";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cboVozila
            // 
            this.cboVozila.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboVozila.FormattingEnabled = true;
            this.cboVozila.Location = new System.Drawing.Point(272, 10);
            this.cboVozila.Margin = new System.Windows.Forms.Padding(2);
            this.cboVozila.Name = "cboVozila";
            this.cboVozila.Size = new System.Drawing.Size(122, 25);
            this.cboVozila.TabIndex = 73;
            // 
            // StampajPalZavrseno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1059, 749);
            this.Controls.Add(this.cb3);
            this.Controls.Add(this.cb2);
            this.Controls.Add(this.cb1);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.cboPaleta);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.cboDatumUtovara);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cboVozila);
            this.Name = "StampajPalZavrseno";
            this.Text = "Štampaj paletu iz završenog";
            this.Load += new System.EventHandler(this.StampajPalZavrseno_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TestProcedurePalBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TestProcedureBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MyDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataSet2TableAdapters.TestProcedurePalTableAdapter TestProcedurePalTableAdapter;
        private System.Windows.Forms.CheckBox cb3;
        private System.Windows.Forms.CheckBox cb2;
        private System.Windows.Forms.CheckBox cb1;
        private MyDataSetTableAdapters.TestProcedureTableAdapter TestProcedureTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource TestProcedurePalBindingSource;
        private DataSet2 DataSet2;
        private System.Windows.Forms.BindingSource TestProcedureBindingSource;
        private MyDataSet MyDataSet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox cboPaleta;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox cboDatumUtovara;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cboVozila;
    }
}