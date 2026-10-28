namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    partial class frmPripremaCenovnika
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
            this.pnlZaglavlje = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.btnNova = new System.Windows.Forms.Button();
            this.cboRadna = new System.Windows.Forms.ComboBox();
            this.lblRadna = new System.Windows.Forms.Label();
            this.pnlObavestenje = new System.Windows.Forms.Panel();
            this.btnPreuzmi = new System.Windows.Forms.Button();
            this.lblObavestenje = new System.Windows.Forms.Label();
            this.pnlDugmad = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSacuvaj = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.lblRabat = new System.Windows.Forms.Label();
            this.txtRabat = new System.Windows.Forms.TextBox();
            this.lblAkcija = new System.Windows.Forms.Label();
            this.txtAkcija = new System.Windows.Forms.TextBox();
            this.lblPovecanje = new System.Windows.Forms.Label();
            this.txtPovecanje = new System.Windows.Forms.TextBox();
            this.btnPrimeni = new System.Windows.Forms.Button();
            this.btnIzvoz = new System.Windows.Forms.Button();
            this.chkEUR = new System.Windows.Forms.CheckBox();
            this.btnProglasi = new System.Windows.Forms.Button();
            this.uc = new WindowsFormsApplication1.Kalkulacije.IzracunCena.ucCenovnik();
            this.status = new System.Windows.Forms.StatusStrip();
            this.lblPrikazano = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblIzmene = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblKorisnik = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlZaglavlje.SuspendLayout();
            this.pnlObavestenje.SuspendLayout();
            this.pnlDugmad.SuspendLayout();
            this.status.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlZaglavlje
            //
            this.pnlZaglavlje.BackColor = System.Drawing.Color.FromArgb(248, 247, 244);
            this.pnlZaglavlje.Controls.Add(this.lblInfo);
            this.pnlZaglavlje.Controls.Add(this.btnObrisi);
            this.pnlZaglavlje.Controls.Add(this.btnNova);
            this.pnlZaglavlje.Controls.Add(this.cboRadna);
            this.pnlZaglavlje.Controls.Add(this.lblRadna);
            this.pnlZaglavlje.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlZaglavlje.Location = new System.Drawing.Point(0, 0);
            this.pnlZaglavlje.Name = "pnlZaglavlje";
            this.pnlZaglavlje.Size = new System.Drawing.Size(1384, 70);
            this.pnlZaglavlje.TabIndex = 0;
            //
            // lblInfo
            //
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.lblInfo.Location = new System.Drawing.Point(12, 44);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(10, 15);
            this.lblInfo.TabIndex = 4;
            this.lblInfo.Text = "-";
            //
            // btnObrisi
            //
            this.btnObrisi.Location = new System.Drawing.Point(642, 8);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(130, 28);
            this.btnObrisi.TabIndex = 3;
            this.btnObrisi.Text = "Obriši radnu kopiju";
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            //
            // btnNova
            //
            this.btnNova.Location = new System.Drawing.Point(476, 8);
            this.btnNova.Name = "btnNova";
            this.btnNova.Size = new System.Drawing.Size(160, 28);
            this.btnNova.TabIndex = 2;
            this.btnNova.Text = "Nova radna kopija...";
            this.btnNova.Click += new System.EventHandler(this.btnNova_Click);
            //
            // cboRadna
            //
            this.cboRadna.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRadna.Location = new System.Drawing.Point(100, 11);
            this.cboRadna.Name = "cboRadna";
            this.cboRadna.Size = new System.Drawing.Size(360, 23);
            this.cboRadna.TabIndex = 1;
            this.cboRadna.SelectionChangeCommitted += new System.EventHandler(this.cboRadna_SelectionChangeCommitted);
            //
            // lblRadna
            //
            this.lblRadna.AutoSize = true;
            this.lblRadna.Location = new System.Drawing.Point(12, 14);
            this.lblRadna.Name = "lblRadna";
            this.lblRadna.Size = new System.Drawing.Size(82, 15);
            this.lblRadna.TabIndex = 0;
            this.lblRadna.Text = "Radna kopija:";
            //
            // pnlObavestenje
            //
            this.pnlObavestenje.BackColor = System.Drawing.Color.FromArgb(255, 240, 180);
            this.pnlObavestenje.Controls.Add(this.btnPreuzmi);
            this.pnlObavestenje.Controls.Add(this.lblObavestenje);
            this.pnlObavestenje.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlObavestenje.Location = new System.Drawing.Point(0, 70);
            this.pnlObavestenje.Name = "pnlObavestenje";
            this.pnlObavestenje.Size = new System.Drawing.Size(1384, 38);
            this.pnlObavestenje.TabIndex = 1;
            this.pnlObavestenje.Visible = false;
            //
            // btnPreuzmi
            //
            this.btnPreuzmi.Location = new System.Drawing.Point(560, 5);
            this.btnPreuzmi.Name = "btnPreuzmi";
            this.btnPreuzmi.Size = new System.Drawing.Size(130, 28);
            this.btnPreuzmi.TabIndex = 1;
            this.btnPreuzmi.Tag = "glavno";
            this.btnPreuzmi.Text = "Preuzmi izmene";
            this.btnPreuzmi.Click += new System.EventHandler(this.btnPreuzmi_Click);
            //
            // lblObavestenje
            //
            this.lblObavestenje.AutoSize = true;
            this.lblObavestenje.Location = new System.Drawing.Point(12, 11);
            this.lblObavestenje.Name = "lblObavestenje";
            this.lblObavestenje.Size = new System.Drawing.Size(530, 15);
            this.lblObavestenje.TabIndex = 0;
            this.lblObavestenje.Text = "Opšti cenovnik je promenjen posle kopiranja. Preuzmite troškove i VP za Code koje niste menjali.";
            //
            // pnlDugmad
            //
            this.pnlDugmad.BackColor = System.Drawing.Color.FromArgb(248, 247, 244);
            this.pnlDugmad.Controls.Add(this.btnSacuvaj);
            this.pnlDugmad.Controls.Add(this.btnOdustani);
            this.pnlDugmad.Controls.Add(this.lblRabat);
            this.pnlDugmad.Controls.Add(this.txtRabat);
            this.pnlDugmad.Controls.Add(this.lblAkcija);
            this.pnlDugmad.Controls.Add(this.txtAkcija);
            this.pnlDugmad.Controls.Add(this.lblPovecanje);
            this.pnlDugmad.Controls.Add(this.txtPovecanje);
            this.pnlDugmad.Controls.Add(this.btnPrimeni);
            this.pnlDugmad.Controls.Add(this.btnIzvoz);
            this.pnlDugmad.Controls.Add(this.chkEUR);
            this.pnlDugmad.Controls.Add(this.btnProglasi);
            this.pnlDugmad.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDugmad.Location = new System.Drawing.Point(0, 108);
            this.pnlDugmad.Name = "pnlDugmad";
            this.pnlDugmad.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.pnlDugmad.Size = new System.Drawing.Size(1384, 42);
            this.pnlDugmad.TabIndex = 2;
            //
            // btnSacuvaj
            //
            this.btnSacuvaj.Name = "btnSacuvaj";
            this.btnSacuvaj.Size = new System.Drawing.Size(90, 30);
            this.btnSacuvaj.TabIndex = 0;
            this.btnSacuvaj.Tag = "glavno";
            this.btnSacuvaj.Text = "Sačuvaj";
            this.btnSacuvaj.Click += new System.EventHandler(this.btnSacuvaj_Click);
            //
            // btnOdustani
            //
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(90, 30);
            this.btnOdustani.TabIndex = 1;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            //
            // lblRabat
            //
            this.lblRabat.AutoSize = true;
            this.lblRabat.Margin = new System.Windows.Forms.Padding(24, 10, 0, 0);
            this.lblRabat.Name = "lblRabat";
            this.lblRabat.Text = "Rabat %";
            //
            // txtRabat
            //
            this.txtRabat.Margin = new System.Windows.Forms.Padding(4, 6, 3, 3);
            this.txtRabat.Name = "txtRabat";
            this.txtRabat.Size = new System.Drawing.Size(55, 23);
            this.txtRabat.TabIndex = 2;
            //
            // lblAkcija
            //
            this.lblAkcija.AutoSize = true;
            this.lblAkcija.Margin = new System.Windows.Forms.Padding(8, 10, 0, 0);
            this.lblAkcija.Name = "lblAkcija";
            this.lblAkcija.Text = "Akcija %";
            //
            // txtAkcija
            //
            this.txtAkcija.Margin = new System.Windows.Forms.Padding(4, 6, 3, 3);
            this.txtAkcija.Name = "txtAkcija";
            this.txtAkcija.Size = new System.Drawing.Size(55, 23);
            this.txtAkcija.TabIndex = 3;
            //
            // lblPovecanje
            //
            this.lblPovecanje.AutoSize = true;
            this.lblPovecanje.Margin = new System.Windows.Forms.Padding(8, 10, 0, 0);
            this.lblPovecanje.Name = "lblPovecanje";
            this.lblPovecanje.Text = "% povećanja VP";
            //
            // txtPovecanje
            //
            this.txtPovecanje.Margin = new System.Windows.Forms.Padding(4, 6, 3, 3);
            this.txtPovecanje.Name = "txtPovecanje";
            this.txtPovecanje.Size = new System.Drawing.Size(55, 23);
            this.txtPovecanje.TabIndex = 4;
            //
            // btnPrimeni
            //
            this.btnPrimeni.Name = "btnPrimeni";
            this.btnPrimeni.Size = new System.Drawing.Size(150, 30);
            this.btnPrimeni.TabIndex = 5;
            this.btnPrimeni.Text = "Primeni na prikazane";
            this.btnPrimeni.Click += new System.EventHandler(this.btnPrimeni_Click);
            //
            // btnIzvoz
            //
            this.btnIzvoz.Margin = new System.Windows.Forms.Padding(24, 3, 3, 3);
            this.btnIzvoz.Name = "btnIzvoz";
            this.btnIzvoz.Size = new System.Drawing.Size(110, 30);
            this.btnIzvoz.TabIndex = 6;
            this.btnIzvoz.Text = "Izvoz u Excel";
            this.btnIzvoz.Click += new System.EventHandler(this.btnIzvoz_Click);
            //
            // chkEUR
            //
            this.chkEUR.AutoSize = true;
            this.chkEUR.Margin = new System.Windows.Forms.Padding(8, 9, 3, 3);
            this.chkEUR.Name = "chkEUR";
            this.chkEUR.TabIndex = 7;
            this.chkEUR.Text = "Prikaži u €";
            this.chkEUR.CheckedChanged += new System.EventHandler(this.chkEUR_CheckedChanged);
            //
            // btnProglasi
            //
            this.btnProglasi.Margin = new System.Windows.Forms.Padding(24, 3, 3, 3);
            this.btnProglasi.Name = "btnProglasi";
            this.btnProglasi.Size = new System.Drawing.Size(140, 30);
            this.btnProglasi.TabIndex = 8;
            this.btnProglasi.Tag = "glavno";
            this.btnProglasi.Text = "Proglasi važećim...";
            this.btnProglasi.Click += new System.EventHandler(this.btnProglasi_Click);
            //
            // uc
            //
            this.uc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc.Location = new System.Drawing.Point(0, 150);
            this.uc.Name = "uc";
            this.uc.Size = new System.Drawing.Size(1384, 589);
            this.uc.TabIndex = 3;
            //
            // status
            //
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblPrikazano,
            this.lblIzmene,
            this.lblKorisnik});
            this.status.Location = new System.Drawing.Point(0, 739);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(1384, 22);
            this.status.TabIndex = 4;
            //
            // lblPrikazano
            //
            this.lblPrikazano.Name = "lblPrikazano";
            this.lblPrikazano.Text = "Prikazano: -";
            //
            // lblIzmene
            //
            this.lblIzmene.Margin = new System.Windows.Forms.Padding(20, 3, 0, 2);
            this.lblIzmene.Name = "lblIzmene";
            this.lblIzmene.Text = "Nesačuvanih izmena: 0";
            //
            // lblKorisnik
            //
            this.lblKorisnik.Margin = new System.Windows.Forms.Padding(20, 3, 0, 2);
            this.lblKorisnik.Name = "lblKorisnik";
            this.lblKorisnik.Text = "Korisnik: -";
            //
            // frmPripremaCenovnika
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1384, 761);
            this.Controls.Add(this.uc);
            this.Controls.Add(this.pnlDugmad);
            this.Controls.Add(this.pnlObavestenje);
            this.Controls.Add(this.pnlZaglavlje);
            this.Controls.Add(this.status);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmPripremaCenovnika";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Priprema cenovnika";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPripremaCenovnika_FormClosing);
            this.pnlZaglavlje.ResumeLayout(false);
            this.pnlZaglavlje.PerformLayout();
            this.pnlObavestenje.ResumeLayout(false);
            this.pnlObavestenje.PerformLayout();
            this.pnlDugmad.ResumeLayout(false);
            this.pnlDugmad.PerformLayout();
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlZaglavlje;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.Button btnNova;
        private System.Windows.Forms.ComboBox cboRadna;
        private System.Windows.Forms.Label lblRadna;
        private System.Windows.Forms.Panel pnlObavestenje;
        private System.Windows.Forms.Button btnPreuzmi;
        private System.Windows.Forms.Label lblObavestenje;
        private System.Windows.Forms.FlowLayoutPanel pnlDugmad;
        private System.Windows.Forms.Button btnSacuvaj;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Label lblRabat;
        private System.Windows.Forms.TextBox txtRabat;
        private System.Windows.Forms.Label lblAkcija;
        private System.Windows.Forms.TextBox txtAkcija;
        private System.Windows.Forms.Label lblPovecanje;
        private System.Windows.Forms.TextBox txtPovecanje;
        private System.Windows.Forms.Button btnPrimeni;
        private System.Windows.Forms.Button btnIzvoz;
        private System.Windows.Forms.CheckBox chkEUR;
        private System.Windows.Forms.Button btnProglasi;
        private ucCenovnik uc;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel lblPrikazano;
        private System.Windows.Forms.ToolStripStatusLabel lblIzmene;
        private System.Windows.Forms.ToolStripStatusLabel lblKorisnik;
    }
}
