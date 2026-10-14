using Syncfusion.WinForms.DataGrid;

namespace WindowsFormsApplication1.Planovi_Excel
{
    partial class frmProdajaGP
    {
        private System.ComponentModel.IContainer components = null;
        private SfDataGrid sfGrid;
        private System.Windows.Forms.Panel pnlLevo;
        private System.Windows.Forms.Label lblKolekcija;
        private System.Windows.Forms.ListBox lbKolekcija;
        private System.Windows.Forms.Label lblProgram;
        private System.Windows.Forms.ListBox lbProgram;
        private System.Windows.Forms.Button btnPrikazi;
        private System.Windows.Forms.Button btnPonisti;
        private System.Windows.Forms.CheckBox chkSveKolekcije;
        private System.Windows.Forms.CheckBox chkSvePrograme;
        private System.Windows.Forms.Button btnExcel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.sfGrid = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.pnlLevo = new System.Windows.Forms.Panel();
            this.lblKolekcija = new System.Windows.Forms.Label();
            this.lbKolekcija = new System.Windows.Forms.ListBox();
            this.lblProgram = new System.Windows.Forms.Label();
            this.lbProgram = new System.Windows.Forms.ListBox();
            this.btnPrikazi = new System.Windows.Forms.Button();
            this.btnPonisti = new System.Windows.Forms.Button();
            this.chkSveKolekcije = new System.Windows.Forms.CheckBox();
            this.chkSvePrograme = new System.Windows.Forms.CheckBox();
            this.btnExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sfGrid)).BeginInit();
            this.pnlLevo.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlLevo
            //
            this.pnlLevo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLevo.Size = new System.Drawing.Size(260, 720);   // visina = forma, da Anchor dole radi
            this.pnlLevo.Name = "pnlLevo";
            this.pnlLevo.Controls.Add(this.lblKolekcija);
            this.pnlLevo.Controls.Add(this.lbKolekcija);
            this.pnlLevo.Controls.Add(this.lblProgram);
            this.pnlLevo.Controls.Add(this.lbProgram);
            this.pnlLevo.Controls.Add(this.btnPrikazi);
            this.pnlLevo.Controls.Add(this.btnPonisti);
            this.pnlLevo.Controls.Add(this.chkSveKolekcije);
            this.pnlLevo.Controls.Add(this.chkSvePrograme);
            this.pnlLevo.Controls.Add(this.btnExcel);
            //
            // lblKolekcija
            //
            this.lblKolekcija.AutoSize = true;
            this.lblKolekcija.Location = new System.Drawing.Point(8, 8);
            this.lblKolekcija.Name = "lblKolekcija";
            this.lblKolekcija.Text = "Kolekcija";
            //
            // lbKolekcija   (NazivRobneGrupe; vise izbora sa Ctrl/Shift)
            //
            this.lbKolekcija.Location = new System.Drawing.Point(8, 28);
            this.lbKolekcija.Size = new System.Drawing.Size(244, 260);
            this.lbKolekcija.Name = "lbKolekcija";
            this.lbKolekcija.IntegralHeight = false;
            this.lbKolekcija.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbKolekcija.SelectedIndexChanged += new System.EventHandler(this.lbKolekcija_SelectedIndexChanged);
            //
            // lblProgram
            //
            this.lblProgram.AutoSize = true;
            this.lblProgram.Location = new System.Drawing.Point(8, 296);
            this.lblProgram.Name = "lblProgram";
            this.lblProgram.Text = "Program";
            //
            // lbProgram   (NazivGrupe; vise izbora sa Ctrl/Shift)
            //
            this.lbProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lbProgram.Location = new System.Drawing.Point(8, 316);
            this.lbProgram.Size = new System.Drawing.Size(244, 316);
            this.lbProgram.Name = "lbProgram";
            this.lbProgram.IntegralHeight = false;
            this.lbProgram.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbProgram.SelectedIndexChanged += new System.EventHandler(this.lbProgram_SelectedIndexChanged);
            //
            // btnPrikazi
            //
            this.btnPrikazi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrikazi.Location = new System.Drawing.Point(8, 644);
            this.btnPrikazi.Size = new System.Drawing.Size(118, 30);
            this.btnPrikazi.Name = "btnPrikazi";
            this.btnPrikazi.Text = "Prikaži";
            this.btnPrikazi.Click += new System.EventHandler(this.btnPrikazi_Click);
            //
            // btnPonisti
            //
            this.btnPonisti.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPonisti.Location = new System.Drawing.Point(134, 644);
            this.btnPonisti.Size = new System.Drawing.Size(118, 30);
            this.btnPonisti.Name = "btnPonisti";
            this.btnPonisti.Text = "Poništi filter";
            this.btnPonisti.Click += new System.EventHandler(this.btnPonisti_Click);
            //
            // chkSveKolekcije
            //
            this.chkSveKolekcije.AutoSize = true;
            this.chkSveKolekcije.Location = new System.Drawing.Point(160, 6);
            this.chkSveKolekcije.Name = "chkSveKolekcije";
            this.chkSveKolekcije.Text = "Izaberi sve";
            this.chkSveKolekcije.CheckedChanged += new System.EventHandler(this.chkSveKolekcije_CheckedChanged);
            //
            // chkSvePrograme
            //
            this.chkSvePrograme.AutoSize = true;
            this.chkSvePrograme.Location = new System.Drawing.Point(160, 294);
            this.chkSvePrograme.Name = "chkSvePrograme";
            this.chkSvePrograme.Text = "Izaberi sve";
            this.chkSvePrograme.CheckedChanged += new System.EventHandler(this.chkSvePrograme_CheckedChanged);
            //
            // btnExcel
            //
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExcel.Location = new System.Drawing.Point(8, 680);
            this.btnExcel.Size = new System.Drawing.Size(244, 30);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Text = "Izvoz u Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
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
            // frmProdajaGP
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.sfGrid);
            this.Controls.Add(this.pnlLevo);
            this.Name = "frmProdajaGP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prodaja GP";
            this.Load += new System.EventHandler(this.frmProdajaGP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sfGrid)).EndInit();
            this.pnlLevo.ResumeLayout(false);
            this.pnlLevo.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
