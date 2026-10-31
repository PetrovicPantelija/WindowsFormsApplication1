namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    partial class frmOpstiCenovnik
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
            this.numPDV = new System.Windows.Forms.NumericUpDown();
            this.lblPDV = new System.Windows.Forms.Label();
            this.numKurs = new System.Windows.Forms.NumericUpDown();
            this.lblKurs = new System.Windows.Forms.Label();
            this.cboCenovnik = new System.Windows.Forms.ComboBox();
            this.lblCenovnik = new System.Windows.Forms.Label();
            this.pnlDugmad = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSacuvaj = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnAzurirajTroskove = new System.Windows.Forms.Button();
            this.btnProglasiTroskove = new System.Windows.Forms.Button();
            this.cboPredlog = new System.Windows.Forms.ComboBox();
            this.btnPredlog = new System.Windows.Forms.Button();
            this.btnIzvoz = new System.Windows.Forms.Button();
            this.chkEUR = new System.Windows.Forms.CheckBox();
            this.btnPoveziSlike = new System.Windows.Forms.Button();
            this.btnIzvestajSlika = new System.Windows.Forms.Button();
            this.uc = new WindowsFormsApplication1.Kalkulacije.IzracunCena.ucCenovnik();
            this.status = new System.Windows.Forms.StatusStrip();
            this.lblPrikazano = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblBezTroska = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblIzmene = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblKorisnik = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlZaglavlje.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKurs)).BeginInit();
            this.pnlDugmad.SuspendLayout();
            this.status.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlZaglavlje
            //
            this.pnlZaglavlje.BackColor = System.Drawing.Color.FromArgb(248, 247, 244);
            this.pnlZaglavlje.Controls.Add(this.lblInfo);
            this.pnlZaglavlje.Controls.Add(this.numPDV);
            this.pnlZaglavlje.Controls.Add(this.lblPDV);
            this.pnlZaglavlje.Controls.Add(this.numKurs);
            this.pnlZaglavlje.Controls.Add(this.lblKurs);
            this.pnlZaglavlje.Controls.Add(this.cboCenovnik);
            this.pnlZaglavlje.Controls.Add(this.lblCenovnik);
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
            this.lblInfo.TabIndex = 6;
            this.lblInfo.Text = "-";
            //
            // numPDV
            //
            this.numPDV.DecimalPlaces = 2;
            this.numPDV.Location = new System.Drawing.Point(620, 11);
            this.numPDV.Name = "numPDV";
            this.numPDV.Size = new System.Drawing.Size(70, 23);
            this.numPDV.TabIndex = 5;
            this.numPDV.Value = new decimal(new int[] { 20, 0, 0, 0 });
            this.numPDV.ValueChanged += new System.EventHandler(this.numPDV_ValueChanged);
            //
            // lblPDV
            //
            this.lblPDV.AutoSize = true;
            this.lblPDV.Location = new System.Drawing.Point(566, 14);
            this.lblPDV.Name = "lblPDV";
            this.lblPDV.Size = new System.Drawing.Size(48, 15);
            this.lblPDV.TabIndex = 4;
            this.lblPDV.Text = "PDV %:";
            //
            // numKurs
            //
            this.numKurs.DecimalPlaces = 2;
            this.numKurs.Location = new System.Drawing.Point(470, 11);
            this.numKurs.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            this.numKurs.Minimum = new decimal(new int[] { 50, 0, 0, 0 });
            this.numKurs.Name = "numKurs";
            this.numKurs.Size = new System.Drawing.Size(80, 23);
            this.numKurs.TabIndex = 3;
            this.numKurs.Value = new decimal(new int[] { 11793, 0, 0, 131072 });
            this.numKurs.ValueChanged += new System.EventHandler(this.numKurs_ValueChanged);
            //
            // lblKurs
            //
            this.lblKurs.AutoSize = true;
            this.lblKurs.Location = new System.Drawing.Point(430, 14);
            this.lblKurs.Name = "lblKurs";
            this.lblKurs.Size = new System.Drawing.Size(34, 15);
            this.lblKurs.TabIndex = 2;
            this.lblKurs.Text = "Kurs:";
            //
            // cboCenovnik
            //
            this.cboCenovnik.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCenovnik.Location = new System.Drawing.Point(84, 11);
            this.cboCenovnik.Name = "cboCenovnik";
            this.cboCenovnik.Size = new System.Drawing.Size(320, 23);
            this.cboCenovnik.TabIndex = 1;
            this.cboCenovnik.SelectionChangeCommitted += new System.EventHandler(this.cboCenovnik_SelectionChangeCommitted);
            //
            // lblCenovnik
            //
            this.lblCenovnik.AutoSize = true;
            this.lblCenovnik.Location = new System.Drawing.Point(12, 14);
            this.lblCenovnik.Name = "lblCenovnik";
            this.lblCenovnik.Size = new System.Drawing.Size(66, 15);
            this.lblCenovnik.TabIndex = 0;
            this.lblCenovnik.Text = "Cenovnik:";
            //
            // pnlDugmad
            //
            this.pnlDugmad.BackColor = System.Drawing.Color.FromArgb(248, 247, 244);
            this.pnlDugmad.Controls.Add(this.btnSacuvaj);
            this.pnlDugmad.Controls.Add(this.btnOdustani);
            this.pnlDugmad.Controls.Add(this.btnAzurirajTroskove);
            this.pnlDugmad.Controls.Add(this.btnProglasiTroskove);
            this.pnlDugmad.Controls.Add(this.cboPredlog);
            this.pnlDugmad.Controls.Add(this.btnPredlog);
            this.pnlDugmad.Controls.Add(this.btnIzvoz);
            this.pnlDugmad.Controls.Add(this.chkEUR);
            this.pnlDugmad.Controls.Add(this.btnPoveziSlike);
            this.pnlDugmad.Controls.Add(this.btnIzvestajSlika);
            this.pnlDugmad.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDugmad.Location = new System.Drawing.Point(0, 70);
            this.pnlDugmad.Name = "pnlDugmad";
            this.pnlDugmad.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.pnlDugmad.Size = new System.Drawing.Size(1384, 42);
            this.pnlDugmad.TabIndex = 1;
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
            // btnAzurirajTroskove
            //
            this.btnAzurirajTroskove.Margin = new System.Windows.Forms.Padding(18, 3, 3, 3);
            this.btnAzurirajTroskove.Name = "btnAzurirajTroskove";
            this.btnAzurirajTroskove.Size = new System.Drawing.Size(130, 30);
            this.btnAzurirajTroskove.TabIndex = 2;
            this.btnAzurirajTroskove.Text = "Ažuriraj troškove";
            this.btnAzurirajTroskove.Click += new System.EventHandler(this.btnAzurirajTroskove_Click);
            //
            // btnProglasiTroskove
            //
            this.btnProglasiTroskove.Name = "btnProglasiTroskove";
            this.btnProglasiTroskove.Size = new System.Drawing.Size(165, 30);
            this.btnProglasiTroskove.TabIndex = 3;
            this.btnProglasiTroskove.Text = "Proglasi troškove novim";
            this.btnProglasiTroskove.Click += new System.EventHandler(this.btnProglasiTroskove_Click);
            //
            // cboPredlog
            //
            this.cboPredlog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPredlog.Items.AddRange(new object[] { "VP prati trošak", "VP za staru maržu" });
            this.cboPredlog.Margin = new System.Windows.Forms.Padding(18, 6, 3, 3);
            this.cboPredlog.Name = "cboPredlog";
            this.cboPredlog.Size = new System.Drawing.Size(140, 23);
            this.cboPredlog.TabIndex = 4;
            //
            // btnPredlog
            //
            this.btnPredlog.Name = "btnPredlog";
            this.btnPredlog.Size = new System.Drawing.Size(120, 30);
            this.btnPredlog.TabIndex = 5;
            this.btnPredlog.Text = "Primeni predlog";
            this.btnPredlog.Click += new System.EventHandler(this.btnPredlog_Click);
            //
            // btnIzvoz
            //
            this.btnIzvoz.Margin = new System.Windows.Forms.Padding(18, 3, 3, 3);
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
            this.chkEUR.Size = new System.Drawing.Size(86, 19);
            this.chkEUR.TabIndex = 7;
            this.chkEUR.Text = "Devizni (€)";
            this.chkEUR.CheckedChanged += new System.EventHandler(this.chkEUR_CheckedChanged);
            //
            // btnPoveziSlike
            //
            this.btnPoveziSlike.Margin = new System.Windows.Forms.Padding(18, 3, 3, 3);
            this.btnPoveziSlike.Name = "btnPoveziSlike";
            this.btnPoveziSlike.Size = new System.Drawing.Size(105, 30);
            this.btnPoveziSlike.TabIndex = 8;
            this.btnPoveziSlike.Text = "Poveži slike";
            this.btnPoveziSlike.Click += new System.EventHandler(this.btnPoveziSlike_Click);
            //
            // btnIzvestajSlika
            //
            this.btnIzvestajSlika.Name = "btnIzvestajSlika";
            this.btnIzvestajSlika.Size = new System.Drawing.Size(110, 30);
            this.btnIzvestajSlika.TabIndex = 9;
            this.btnIzvestajSlika.Text = "Izveštaj slika";
            this.btnIzvestajSlika.Click += new System.EventHandler(this.btnIzvestajSlika_Click);
            //
            // uc
            //
            this.uc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc.Location = new System.Drawing.Point(0, 112);
            this.uc.Name = "uc";
            this.uc.Size = new System.Drawing.Size(1384, 627);
            this.uc.TabIndex = 2;
            //
            // status
            //
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblPrikazano,
            this.lblBezTroska,
            this.lblIzmene,
            this.lblKorisnik});
            this.status.Location = new System.Drawing.Point(0, 739);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(1384, 22);
            this.status.TabIndex = 3;
            //
            // lblPrikazano
            //
            this.lblPrikazano.Name = "lblPrikazano";
            this.lblPrikazano.Size = new System.Drawing.Size(70, 17);
            this.lblPrikazano.Text = "Prikazano: -";
            //
            // lblBezTroska
            //
            this.lblBezTroska.Margin = new System.Windows.Forms.Padding(20, 3, 0, 2);
            this.lblBezTroska.Name = "lblBezTroska";
            this.lblBezTroska.Size = new System.Drawing.Size(80, 17);
            this.lblBezTroska.Text = "Bez troška: -";
            //
            // lblIzmene
            //
            this.lblIzmene.Margin = new System.Windows.Forms.Padding(20, 3, 0, 2);
            this.lblIzmene.Name = "lblIzmene";
            this.lblIzmene.Size = new System.Drawing.Size(120, 17);
            this.lblIzmene.Text = "Nesačuvanih izmena: 0";
            //
            // lblKorisnik
            //
            this.lblKorisnik.Margin = new System.Windows.Forms.Padding(20, 3, 0, 2);
            this.lblKorisnik.Name = "lblKorisnik";
            this.lblKorisnik.Size = new System.Drawing.Size(60, 17);
            this.lblKorisnik.Text = "Korisnik: -";
            //
            // frmOpstiCenovnik
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1384, 761);
            this.Controls.Add(this.uc);
            this.Controls.Add(this.pnlDugmad);
            this.Controls.Add(this.pnlZaglavlje);
            this.Controls.Add(this.status);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmOpstiCenovnik";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opšti cenovnik";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOpstiCenovnik_FormClosing);
            this.pnlZaglavlje.ResumeLayout(false);
            this.pnlZaglavlje.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKurs)).EndInit();
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
        private System.Windows.Forms.NumericUpDown numPDV;
        private System.Windows.Forms.Label lblPDV;
        private System.Windows.Forms.NumericUpDown numKurs;
        private System.Windows.Forms.Label lblKurs;
        private System.Windows.Forms.ComboBox cboCenovnik;
        private System.Windows.Forms.Label lblCenovnik;
        private System.Windows.Forms.FlowLayoutPanel pnlDugmad;
        private System.Windows.Forms.Button btnSacuvaj;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnAzurirajTroskove;
        private System.Windows.Forms.Button btnProglasiTroskove;
        private System.Windows.Forms.ComboBox cboPredlog;
        private System.Windows.Forms.Button btnPredlog;
        private System.Windows.Forms.Button btnIzvoz;
        private System.Windows.Forms.CheckBox chkEUR;
        private System.Windows.Forms.Button btnPoveziSlike;
        private System.Windows.Forms.Button btnIzvestajSlika;
        private ucCenovnik uc;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel lblPrikazano;
        private System.Windows.Forms.ToolStripStatusLabel lblBezTroska;
        private System.Windows.Forms.ToolStripStatusLabel lblIzmene;
        private System.Windows.Forms.ToolStripStatusLabel lblKorisnik;
    }
}
