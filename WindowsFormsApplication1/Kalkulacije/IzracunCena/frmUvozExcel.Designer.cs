namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    partial class frmUvozExcel
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
            this.pnlGore = new System.Windows.Forms.Panel();
            this.lblExcelNapomena = new System.Windows.Forms.Label();
            this.btnProcitaj = new System.Windows.Forms.Button();
            this.btnIzaberi = new System.Windows.Forms.Button();
            this.txtFajl = new System.Windows.Forms.TextBox();
            this.lblFajl = new System.Windows.Forms.Label();
            this.numPDV = new System.Windows.Forms.NumericUpDown();
            this.lblPDV = new System.Windows.Forms.Label();
            this.numKurs = new System.Windows.Forms.NumericUpDown();
            this.lblKurs = new System.Windows.Forms.Label();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.lblNaziv = new System.Windows.Forms.Label();
            this.cboValuta = new System.Windows.Forms.ComboBox();
            this.lblValuta = new System.Windows.Forms.Label();
            this.tabPregled = new System.Windows.Forms.TabControl();
            this.tabSheetovi = new System.Windows.Forms.TabPage();
            this.dgvSheetovi = new System.Windows.Forms.DataGridView();
            this.tabProblemi = new System.Windows.Forms.TabPage();
            this.dgvProblemi = new System.Windows.Forms.DataGridView();
            this.pnlDole = new System.Windows.Forms.Panel();
            this.lblSazetak = new System.Windows.Forms.Label();
            this.btnZatvori = new System.Windows.Forms.Button();
            this.btnNapravi = new System.Windows.Forms.Button();
            this.pnlGore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKurs)).BeginInit();
            this.tabPregled.SuspendLayout();
            this.tabSheetovi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSheetovi)).BeginInit();
            this.tabProblemi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProblemi)).BeginInit();
            this.pnlDole.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlGore
            //
            this.pnlGore.BackColor = System.Drawing.Color.FromArgb(248, 247, 244);
            this.pnlGore.Controls.Add(this.lblExcelNapomena);
            this.pnlGore.Controls.Add(this.btnProcitaj);
            this.pnlGore.Controls.Add(this.btnIzaberi);
            this.pnlGore.Controls.Add(this.txtFajl);
            this.pnlGore.Controls.Add(this.lblFajl);
            this.pnlGore.Controls.Add(this.numPDV);
            this.pnlGore.Controls.Add(this.lblPDV);
            this.pnlGore.Controls.Add(this.numKurs);
            this.pnlGore.Controls.Add(this.lblKurs);
            this.pnlGore.Controls.Add(this.txtNaziv);
            this.pnlGore.Controls.Add(this.lblNaziv);
            this.pnlGore.Controls.Add(this.cboValuta);
            this.pnlGore.Controls.Add(this.lblValuta);
            this.pnlGore.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGore.Location = new System.Drawing.Point(0, 0);
            this.pnlGore.Name = "pnlGore";
            this.pnlGore.Size = new System.Drawing.Size(984, 110);
            this.pnlGore.TabIndex = 0;
            //
            // lblExcelNapomena
            //
            this.lblExcelNapomena.AutoSize = true;
            this.lblExcelNapomena.ForeColor = System.Drawing.Color.Gray;
            this.lblExcelNapomena.Location = new System.Drawing.Point(92, 82);
            this.lblExcelNapomena.Name = "lblExcelNapomena";
            this.lblExcelNapomena.Text = "Bez fajla: svi proizvodi, VP iz jela_products, troškovi iz KalkCenik; rabati i akcije se unose kasnije.";
            //
            // btnProcitaj
            //
            this.btnProcitaj.Location = new System.Drawing.Point(708, 50);
            this.btnProcitaj.Name = "btnProcitaj";
            this.btnProcitaj.Size = new System.Drawing.Size(100, 28);
            this.btnProcitaj.TabIndex = 7;
            this.btnProcitaj.Text = "Pročitaj";
            this.btnProcitaj.Click += new System.EventHandler(this.btnProcitaj_Click);
            //
            // btnIzaberi
            //
            this.btnIzaberi.Location = new System.Drawing.Point(602, 50);
            this.btnIzaberi.Name = "btnIzaberi";
            this.btnIzaberi.Size = new System.Drawing.Size(100, 28);
            this.btnIzaberi.TabIndex = 6;
            this.btnIzaberi.Text = "Izaberi...";
            this.btnIzaberi.Click += new System.EventHandler(this.btnIzaberi_Click);
            //
            // txtFajl
            //
            this.txtFajl.Location = new System.Drawing.Point(92, 53);
            this.txtFajl.Name = "txtFajl";
            this.txtFajl.ReadOnly = true;
            this.txtFajl.Size = new System.Drawing.Size(500, 23);
            this.txtFajl.TabIndex = 5;
            //
            // lblFajl
            //
            this.lblFajl.AutoSize = true;
            this.lblFajl.Location = new System.Drawing.Point(12, 56);
            this.lblFajl.Name = "lblFajl";
            this.lblFajl.Text = "Excel fajl:";
            //
            // numPDV
            //
            this.numPDV.DecimalPlaces = 2;
            this.numPDV.Location = new System.Drawing.Point(738, 14);
            this.numPDV.Name = "numPDV";
            this.numPDV.Size = new System.Drawing.Size(70, 23);
            this.numPDV.TabIndex = 4;
            this.numPDV.Value = new decimal(new int[] { 20, 0, 0, 0 });
            //
            // lblPDV
            //
            this.lblPDV.AutoSize = true;
            this.lblPDV.Location = new System.Drawing.Point(684, 17);
            this.lblPDV.Name = "lblPDV";
            this.lblPDV.Text = "PDV %:";
            //
            // numKurs
            //
            this.numKurs.DecimalPlaces = 2;
            this.numKurs.Location = new System.Drawing.Point(592, 14);
            this.numKurs.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            this.numKurs.Minimum = new decimal(new int[] { 50, 0, 0, 0 });
            this.numKurs.Name = "numKurs";
            this.numKurs.Size = new System.Drawing.Size(80, 23);
            this.numKurs.TabIndex = 3;
            this.numKurs.Value = new decimal(new int[] { 11793, 0, 0, 131072 });
            //
            // lblKurs
            //
            this.lblKurs.AutoSize = true;
            this.lblKurs.Location = new System.Drawing.Point(552, 17);
            this.lblKurs.Name = "lblKurs";
            this.lblKurs.Text = "Kurs:";
            //
            // txtNaziv
            //
            this.txtNaziv.Location = new System.Drawing.Point(250, 14);
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(290, 23);
            this.txtNaziv.TabIndex = 2;
            //
            // lblNaziv
            //
            this.lblNaziv.AutoSize = true;
            this.lblNaziv.Location = new System.Drawing.Point(200, 17);
            this.lblNaziv.Name = "lblNaziv";
            this.lblNaziv.Text = "Naziv:";
            //
            // cboValuta
            //
            this.cboValuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboValuta.Items.AddRange(new object[] { "RSD", "EUR" });
            this.cboValuta.Location = new System.Drawing.Point(92, 14);
            this.cboValuta.Name = "cboValuta";
            this.cboValuta.Size = new System.Drawing.Size(90, 23);
            this.cboValuta.TabIndex = 1;
            this.cboValuta.SelectedIndexChanged += new System.EventHandler(this.cboValuta_SelectedIndexChanged);
            //
            // lblValuta
            //
            this.lblValuta.AutoSize = true;
            this.lblValuta.Location = new System.Drawing.Point(12, 17);
            this.lblValuta.Name = "lblValuta";
            this.lblValuta.Text = "Valuta:";
            //
            // tabPregled
            //
            this.tabPregled.Controls.Add(this.tabSheetovi);
            this.tabPregled.Controls.Add(this.tabProblemi);
            this.tabPregled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPregled.Location = new System.Drawing.Point(0, 110);
            this.tabPregled.Name = "tabPregled";
            this.tabPregled.SelectedIndex = 0;
            this.tabPregled.Size = new System.Drawing.Size(984, 391);
            this.tabPregled.TabIndex = 1;
            //
            // tabSheetovi
            //
            this.tabSheetovi.Controls.Add(this.dgvSheetovi);
            this.tabSheetovi.Name = "tabSheetovi";
            this.tabSheetovi.Text = "Sheetovi";
            //
            // dgvSheetovi
            //
            this.dgvSheetovi.AllowUserToAddRows = false;
            this.dgvSheetovi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvSheetovi.BackgroundColor = System.Drawing.Color.White;
            this.dgvSheetovi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSheetovi.Name = "dgvSheetovi";
            this.dgvSheetovi.ReadOnly = true;
            this.dgvSheetovi.TabIndex = 0;
            //
            // tabProblemi
            //
            this.tabProblemi.Controls.Add(this.dgvProblemi);
            this.tabProblemi.Name = "tabProblemi";
            this.tabProblemi.Text = "Problemi";
            //
            // dgvProblemi
            //
            this.dgvProblemi.AllowUserToAddRows = false;
            this.dgvProblemi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvProblemi.BackgroundColor = System.Drawing.Color.White;
            this.dgvProblemi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProblemi.Name = "dgvProblemi";
            this.dgvProblemi.ReadOnly = true;
            this.dgvProblemi.TabIndex = 0;
            //
            // pnlDole
            //
            this.pnlDole.BackColor = System.Drawing.Color.FromArgb(248, 247, 244);
            this.pnlDole.Controls.Add(this.lblSazetak);
            this.pnlDole.Controls.Add(this.btnZatvori);
            this.pnlDole.Controls.Add(this.btnNapravi);
            this.pnlDole.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDole.Location = new System.Drawing.Point(0, 501);
            this.pnlDole.Name = "pnlDole";
            this.pnlDole.Size = new System.Drawing.Size(984, 60);
            this.pnlDole.TabIndex = 2;
            //
            // lblSazetak
            //
            this.lblSazetak.Location = new System.Drawing.Point(12, 8);
            this.lblSazetak.Name = "lblSazetak";
            this.lblSazetak.Size = new System.Drawing.Size(620, 44);
            this.lblSazetak.Text = "Izaberite valutu i naziv. Excel fajl nije obavezan.";
            //
            // btnZatvori
            //
            this.btnZatvori.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZatvori.Location = new System.Drawing.Point(872, 14);
            this.btnZatvori.Name = "btnZatvori";
            this.btnZatvori.Size = new System.Drawing.Size(100, 32);
            this.btnZatvori.TabIndex = 1;
            this.btnZatvori.Text = "Zatvori";
            this.btnZatvori.Click += new System.EventHandler(this.btnZatvori_Click);
            //
            // btnNapravi
            //
            this.btnNapravi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNapravi.Location = new System.Drawing.Point(686, 14);
            this.btnNapravi.Name = "btnNapravi";
            this.btnNapravi.Size = new System.Drawing.Size(180, 32);
            this.btnNapravi.TabIndex = 0;
            this.btnNapravi.Tag = "glavno";
            this.btnNapravi.Text = "Napravi opšti cenovnik";
            this.btnNapravi.Click += new System.EventHandler(this.btnNapravi_Click);
            //
            // frmUvozExcel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.tabPregled);
            this.Controls.Add(this.pnlDole);
            this.Controls.Add(this.pnlGore);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmUvozExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Novi opšti cenovnik / uvoz iz Excela";
            this.pnlGore.ResumeLayout(false);
            this.pnlGore.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKurs)).EndInit();
            this.tabPregled.ResumeLayout(false);
            this.tabSheetovi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSheetovi)).EndInit();
            this.tabProblemi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProblemi)).EndInit();
            this.pnlDole.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlGore;
        private System.Windows.Forms.Label lblExcelNapomena;
        private System.Windows.Forms.Button btnProcitaj;
        private System.Windows.Forms.Button btnIzaberi;
        private System.Windows.Forms.TextBox txtFajl;
        private System.Windows.Forms.Label lblFajl;
        private System.Windows.Forms.NumericUpDown numPDV;
        private System.Windows.Forms.Label lblPDV;
        private System.Windows.Forms.NumericUpDown numKurs;
        private System.Windows.Forms.Label lblKurs;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Label lblNaziv;
        private System.Windows.Forms.ComboBox cboValuta;
        private System.Windows.Forms.Label lblValuta;
        private System.Windows.Forms.TabControl tabPregled;
        private System.Windows.Forms.TabPage tabSheetovi;
        private System.Windows.Forms.DataGridView dgvSheetovi;
        private System.Windows.Forms.TabPage tabProblemi;
        private System.Windows.Forms.DataGridView dgvProblemi;
        private System.Windows.Forms.Panel pnlDole;
        private System.Windows.Forms.Label lblSazetak;
        private System.Windows.Forms.Button btnZatvori;
        private System.Windows.Forms.Button btnNapravi;
    }
}
