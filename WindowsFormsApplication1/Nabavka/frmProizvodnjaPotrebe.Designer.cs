namespace WindowsFormsApplication1.Nabavka
{
    partial class frmProizvodnjaPotrebe
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
            this.panelFilter = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblFaktor = new System.Windows.Forms.Label();
            this.txtFaktor = new System.Windows.Forms.TextBox();
            this.btnUmanji = new FontAwesome.Sharp.IconButton();
            this.btnPdf = new FontAwesome.Sharp.IconButton();
            this.btnCsv = new FontAwesome.Sharp.IconButton();
            this.btnExcel = new FontAwesome.Sharp.IconButton();
            this.btnRefresh = new FontAwesome.Sharp.IconButton();
            this.clbPodPlanovi = new System.Windows.Forms.CheckedListBox();
            this.lblPodPlanovi = new System.Windows.Forms.Label();
            this.clbPlanovi = new System.Windows.Forms.CheckedListBox();
            this.lblPlanovi = new System.Windows.Forms.Label();
            this.sfGrid = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.panelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sfGrid)).BeginInit();
            this.SuspendLayout();
            //
            // panelFilter
            //
            this.panelFilter.Controls.Add(this.btnUmanji);
            this.panelFilter.Controls.Add(this.txtFaktor);
            this.panelFilter.Controls.Add(this.lblFaktor);
            this.panelFilter.Controls.Add(this.lblInfo);
            this.panelFilter.Controls.Add(this.btnPdf);
            this.panelFilter.Controls.Add(this.btnCsv);
            this.panelFilter.Controls.Add(this.btnExcel);
            this.panelFilter.Controls.Add(this.btnRefresh);
            this.panelFilter.Controls.Add(this.clbPodPlanovi);
            this.panelFilter.Controls.Add(this.lblPodPlanovi);
            this.panelFilter.Controls.Add(this.clbPlanovi);
            this.panelFilter.Controls.Add(this.lblPlanovi);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 0);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1100, 200);
            this.panelFilter.TabIndex = 0;
            //
            // lblInfo
            //
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInfo.Location = new System.Drawing.Point(800, 36);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 15);
            this.lblInfo.TabIndex = 8;
            //
            // lblFaktor
            //
            this.lblFaktor.AutoSize = true;
            this.lblFaktor.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFaktor.Location = new System.Drawing.Point(797, 62);
            this.lblFaktor.Name = "lblFaktor";
            this.lblFaktor.Size = new System.Drawing.Size(140, 15);
            this.lblFaktor.TabIndex = 9;
            this.lblFaktor.Text = "Faktor umanjenja tabli";
            //
            // txtFaktor
            //
            this.txtFaktor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFaktor.Location = new System.Drawing.Point(800, 80);
            this.txtFaktor.Name = "txtFaktor";
            this.txtFaktor.Size = new System.Drawing.Size(90, 25);
            this.txtFaktor.TabIndex = 10;
            this.txtFaktor.Text = "5,796";
            this.txtFaktor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // btnUmanji
            //
            this.btnUmanji.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnUmanji.IconChar = FontAwesome.Sharp.IconChar.Divide;
            this.btnUmanji.IconColor = System.Drawing.Color.DarkOrange;
            this.btnUmanji.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnUmanji.IconSize = 20;
            this.btnUmanji.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUmanji.Location = new System.Drawing.Point(898, 76);
            this.btnUmanji.Name = "btnUmanji";
            this.btnUmanji.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnUmanji.Size = new System.Drawing.Size(200, 32);
            this.btnUmanji.TabIndex = 11;
            this.btnUmanji.Text = "Umanji količine table";
            this.btnUmanji.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUmanji.UseVisualStyleBackColor = true;
            this.btnUmanji.Click += new System.EventHandler(this.btnUmanji_Click);
            //
            // btnPdf
            //
            this.btnPdf.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnPdf.IconChar = FontAwesome.Sharp.IconChar.FilePdf;
            this.btnPdf.IconColor = System.Drawing.Color.Firebrick;
            this.btnPdf.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPdf.IconSize = 22;
            this.btnPdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPdf.Location = new System.Drawing.Point(650, 152);
            this.btnPdf.Name = "btnPdf";
            this.btnPdf.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnPdf.Size = new System.Drawing.Size(130, 32);
            this.btnPdf.TabIndex = 7;
            this.btnPdf.Text = "PDF";
            this.btnPdf.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPdf.UseVisualStyleBackColor = true;
            this.btnPdf.Click += new System.EventHandler(this.btnPdf_Click);
            //
            // btnCsv
            //
            this.btnCsv.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCsv.IconChar = FontAwesome.Sharp.IconChar.FileCsv;
            this.btnCsv.IconColor = System.Drawing.Color.SteelBlue;
            this.btnCsv.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCsv.IconSize = 22;
            this.btnCsv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCsv.Location = new System.Drawing.Point(650, 114);
            this.btnCsv.Name = "btnCsv";
            this.btnCsv.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnCsv.Size = new System.Drawing.Size(130, 32);
            this.btnCsv.TabIndex = 6;
            this.btnCsv.Text = "CSV";
            this.btnCsv.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCsv.UseVisualStyleBackColor = true;
            this.btnCsv.Click += new System.EventHandler(this.btnCsv_Click);
            //
            // btnExcel
            //
            this.btnExcel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnExcel.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            this.btnExcel.IconColor = System.Drawing.Color.ForestGreen;
            this.btnExcel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExcel.IconSize = 22;
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(650, 76);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnExcel.Size = new System.Drawing.Size(130, 32);
            this.btnExcel.TabIndex = 5;
            this.btnExcel.Text = "Excel";
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            //
            // btnRefresh
            //
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.IconChar = FontAwesome.Sharp.IconChar.SyncAlt;
            this.btnRefresh.IconColor = System.Drawing.Color.Black;
            this.btnRefresh.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRefresh.IconSize = 22;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(650, 28);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnRefresh.Size = new System.Drawing.Size(130, 32);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            //
            // clbPodPlanovi
            //
            this.clbPodPlanovi.CheckOnClick = true;
            this.clbPodPlanovi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.clbPodPlanovi.FormattingEnabled = true;
            this.clbPodPlanovi.IntegralHeight = false;
            this.clbPodPlanovi.Location = new System.Drawing.Point(380, 28);
            this.clbPodPlanovi.Name = "clbPodPlanovi";
            this.clbPodPlanovi.Size = new System.Drawing.Size(260, 156);
            this.clbPodPlanovi.TabIndex = 3;
            this.clbPodPlanovi.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbPodPlanovi_ItemCheck);
            //
            // lblPodPlanovi
            //
            this.lblPodPlanovi.AutoSize = true;
            this.lblPodPlanovi.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPodPlanovi.Location = new System.Drawing.Point(377, 8);
            this.lblPodPlanovi.Name = "lblPodPlanovi";
            this.lblPodPlanovi.Size = new System.Drawing.Size(70, 15);
            this.lblPodPlanovi.TabIndex = 2;
            this.lblPodPlanovi.Text = "Podplanovi";
            //
            // clbPlanovi
            //
            this.clbPlanovi.CheckOnClick = true;
            this.clbPlanovi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.clbPlanovi.FormattingEnabled = true;
            this.clbPlanovi.IntegralHeight = false;
            this.clbPlanovi.Location = new System.Drawing.Point(10, 28);
            this.clbPlanovi.Name = "clbPlanovi";
            this.clbPlanovi.Size = new System.Drawing.Size(360, 156);
            this.clbPlanovi.TabIndex = 1;
            this.clbPlanovi.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbPlanovi_ItemCheck);
            //
            // lblPlanovi
            //
            this.lblPlanovi.AutoSize = true;
            this.lblPlanovi.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPlanovi.Location = new System.Drawing.Point(7, 8);
            this.lblPlanovi.Name = "lblPlanovi";
            this.lblPlanovi.Size = new System.Drawing.Size(50, 15);
            this.lblPlanovi.TabIndex = 0;
            this.lblPlanovi.Text = "Planovi";
            //
            // sfGrid
            //
            this.sfGrid.AccessibleName = "Table";
            this.sfGrid.AllowEditing = false;
            this.sfGrid.AllowFiltering = true;
            this.sfGrid.AllowResizingColumns = true;
            this.sfGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sfGrid.Location = new System.Drawing.Point(0, 200);
            this.sfGrid.Name = "sfGrid";
            this.sfGrid.Size = new System.Drawing.Size(1100, 450);
            this.sfGrid.TabIndex = 1;
            this.sfGrid.Text = "sfGrid";
            this.sfGrid.AutoGeneratingColumn += new Syncfusion.WinForms.DataGrid.Events.AutoGeneratingColumnEventHandler(this.sfGrid_AutoGeneratingColumn);
            //
            // frmProizvodnjaPotrebe
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.Controls.Add(this.sfGrid);
            this.Controls.Add(this.panelFilter);
            this.Name = "frmProizvodnjaPotrebe";
            this.Text = "Proizvodnja - potrebe materijala";
            this.Load += new System.EventHandler(this.frmProizvodnjaPotrebe_Load);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sfGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Label lblPlanovi;
        private System.Windows.Forms.CheckedListBox clbPlanovi;
        private System.Windows.Forms.Label lblPodPlanovi;
        private System.Windows.Forms.CheckedListBox clbPodPlanovi;
        private FontAwesome.Sharp.IconButton btnRefresh;
        private FontAwesome.Sharp.IconButton btnExcel;
        private FontAwesome.Sharp.IconButton btnCsv;
        private FontAwesome.Sharp.IconButton btnPdf;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblFaktor;
        private System.Windows.Forms.TextBox txtFaktor;
        private FontAwesome.Sharp.IconButton btnUmanji;
        private Syncfusion.WinForms.DataGrid.SfDataGrid sfGrid;
    }
}
