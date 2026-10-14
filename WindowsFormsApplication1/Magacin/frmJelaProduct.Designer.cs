using Syncfusion.WinForms.DataGrid;

namespace WindowsFormsApplication1.Magacin
{
    partial class frmJelaProduct
    {
        private System.ComponentModel.IContainer components = null;
        private SfDataGrid sfGrid;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnUnos;
        private System.Windows.Forms.Button btnIzmena;
        private System.Windows.Forms.Button btnDeaktiviranje;
        private System.Windows.Forms.Button btnImportExcel;
        private System.Windows.Forms.CheckBox chkNeaktivni;
        private System.Windows.Forms.Button btnExportExcel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.sfGrid = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnUnos = new System.Windows.Forms.Button();
            this.btnIzmena = new System.Windows.Forms.Button();
            this.btnDeaktiviranje = new System.Windows.Forms.Button();
            this.btnImportExcel = new System.Windows.Forms.Button();
            this.chkNeaktivni = new System.Windows.Forms.CheckBox();
            this.btnExportExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sfGrid)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlTop
            //
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 44;
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Controls.Add(this.btnUnos);
            this.pnlTop.Controls.Add(this.btnIzmena);
            this.pnlTop.Controls.Add(this.btnDeaktiviranje);
            this.pnlTop.Controls.Add(this.btnImportExcel);
            this.pnlTop.Controls.Add(this.chkNeaktivni);
            this.pnlTop.Controls.Add(this.btnExportExcel);
            //
            // btnUnos   (Tag se koristi za gasenje dugmadi po pravima: upis / izmena / brisanje)
            //
            this.btnUnos.Location = new System.Drawing.Point(8, 8);
            this.btnUnos.Size = new System.Drawing.Size(100, 28);
            this.btnUnos.Name = "btnUnos";
            this.btnUnos.Tag = "upis";
            this.btnUnos.Text = "Unos";
            this.btnUnos.Click += new System.EventHandler(this.btnUnos_Click);
            //
            // btnIzmena
            //
            this.btnIzmena.Location = new System.Drawing.Point(116, 8);
            this.btnIzmena.Size = new System.Drawing.Size(100, 28);
            this.btnIzmena.Name = "btnIzmena";
            this.btnIzmena.Tag = "izmena";
            this.btnIzmena.Text = "Izmena";
            this.btnIzmena.Click += new System.EventHandler(this.btnIzmena_Click);
            //
            // btnDeaktiviranje
            //
            this.btnDeaktiviranje.Location = new System.Drawing.Point(224, 8);
            this.btnDeaktiviranje.Size = new System.Drawing.Size(120, 28);
            this.btnDeaktiviranje.Name = "btnDeaktiviranje";
            this.btnDeaktiviranje.Tag = "brisanje";
            this.btnDeaktiviranje.Text = "Deaktiviranje";
            this.btnDeaktiviranje.Click += new System.EventHandler(this.btnDeaktiviranje_Click);
            //
            // btnImportExcel
            //
            this.btnImportExcel.Location = new System.Drawing.Point(360, 8);
            this.btnImportExcel.Size = new System.Drawing.Size(140, 28);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Tag = "upis";
            this.btnImportExcel.Text = "Import from Excel";
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            //
            // chkNeaktivni
            //
            this.chkNeaktivni.AutoSize = true;
            this.chkNeaktivni.Location = new System.Drawing.Point(520, 13);
            this.chkNeaktivni.Name = "chkNeaktivni";
            this.chkNeaktivni.Text = "Prikaži i neaktivne";
            this.chkNeaktivni.CheckedChanged += new System.EventHandler(this.chkNeaktivni_CheckedChanged);
            //
            // btnExportExcel
            //
            this.btnExportExcel.Location = new System.Drawing.Point(680, 8);
            this.btnExportExcel.Size = new System.Drawing.Size(140, 28);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Text = "Export to Excel";
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            //
            // sfGrid
            //
            this.sfGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sfGrid.Name = "sfGrid";
            this.sfGrid.TabIndex = 1;
            this.sfGrid.AllowEditing = false;
            this.sfGrid.AllowDeleting = false;
            this.sfGrid.AllowSorting = true;
            this.sfGrid.AllowFiltering = true;
            this.sfGrid.AllowResizingColumns = true;
            this.sfGrid.AutoGenerateColumns = true;
            this.sfGrid.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            this.sfGrid.ShowRowHeader = false;
            this.sfGrid.AutoGeneratingColumn += new Syncfusion.WinForms.DataGrid.Events.AutoGeneratingColumnEventHandler(this.sfGrid_AutoGeneratingColumn);
            //
            // frmJelaProduct
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.sfGrid);
            this.Controls.Add(this.pnlTop);
            this.Name = "frmJelaProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Jela product";
            this.Load += new System.EventHandler(this.frmJelaProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sfGrid)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
