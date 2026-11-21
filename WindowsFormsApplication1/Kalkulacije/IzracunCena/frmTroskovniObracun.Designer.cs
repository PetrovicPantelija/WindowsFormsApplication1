namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    partial class frmTroskovniObracun
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
            this.pnlDugmad = new System.Windows.Forms.FlowLayoutPanel();
            this.btnObradi = new System.Windows.Forms.Button();
            this.btnOsvezi = new System.Windows.Forms.Button();
            this.btnIzvozCsv = new System.Windows.Forms.Button();
            this.btnIzvozExcel = new System.Windows.Forms.Button();
            this.btnPreskoceni = new System.Windows.Forms.Button();
            this.pnlFajl = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblNapredak = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.btnIzaberi = new System.Windows.Forms.Button();
            this.txtPutanja = new System.Windows.Forms.TextBox();
            this.lblFajl = new System.Windows.Forms.Label();
            this.grid = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.status = new System.Windows.Forms.StatusStrip();
            this.lblPrikazano = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblIzabrano = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblKorisnik = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblObrada = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlDugmad.SuspendLayout();
            this.pnlFajl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.status.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlDugmad
            //
            this.pnlDugmad.BackColor = System.Drawing.Color.FromArgb(248, 247, 244);
            this.pnlDugmad.Controls.Add(this.btnObradi);
            this.pnlDugmad.Controls.Add(this.btnOsvezi);
            this.pnlDugmad.Controls.Add(this.btnIzvozCsv);
            this.pnlDugmad.Controls.Add(this.btnIzvozExcel);
            this.pnlDugmad.Controls.Add(this.btnPreskoceni);
            this.pnlDugmad.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDugmad.Location = new System.Drawing.Point(0, 0);
            this.pnlDugmad.Name = "pnlDugmad";
            this.pnlDugmad.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.pnlDugmad.Size = new System.Drawing.Size(1200, 42);
            this.pnlDugmad.TabIndex = 0;
            //
            // btnObradi
            //
            this.btnObradi.Name = "btnObradi";
            this.btnObradi.Size = new System.Drawing.Size(90, 30);
            this.btnObradi.TabIndex = 0;
            this.btnObradi.Tag = "glavno";
            this.btnObradi.Text = "Obradi";
            this.btnObradi.Click += new System.EventHandler(this.btnObradi_Click);
            //
            // btnOsvezi
            //
            this.btnOsvezi.Name = "btnOsvezi";
            this.btnOsvezi.Size = new System.Drawing.Size(90, 30);
            this.btnOsvezi.TabIndex = 1;
            this.btnOsvezi.Text = "Osveži";
            this.btnOsvezi.Click += new System.EventHandler(this.btnOsvezi_Click);
            //
            // btnIzvozCsv
            //
            this.btnIzvozCsv.Margin = new System.Windows.Forms.Padding(18, 3, 3, 3);
            this.btnIzvozCsv.Name = "btnIzvozCsv";
            this.btnIzvozCsv.Size = new System.Drawing.Size(100, 30);
            this.btnIzvozCsv.TabIndex = 2;
            this.btnIzvozCsv.Text = "Izvoz CSV";
            this.btnIzvozCsv.Click += new System.EventHandler(this.btnIzvozCsv_Click);
            //
            // btnIzvozExcel
            //
            this.btnIzvozExcel.Name = "btnIzvozExcel";
            this.btnIzvozExcel.Size = new System.Drawing.Size(110, 30);
            this.btnIzvozExcel.TabIndex = 3;
            this.btnIzvozExcel.Text = "Izvoz u Excel";
            this.btnIzvozExcel.Click += new System.EventHandler(this.btnIzvozExcel_Click);
            //
            // btnPreskoceni
            //
            this.btnPreskoceni.Enabled = false;
            this.btnPreskoceni.Margin = new System.Windows.Forms.Padding(18, 3, 3, 3);
            this.btnPreskoceni.Name = "btnPreskoceni";
            this.btnPreskoceni.Size = new System.Drawing.Size(150, 30);
            this.btnPreskoceni.TabIndex = 4;
            this.btnPreskoceni.Text = "Preskočeni redovi";
            this.btnPreskoceni.Click += new System.EventHandler(this.btnPreskoceni_Click);
            //
            // pnlFajl
            //
            this.pnlFajl.BackColor = System.Drawing.Color.FromArgb(248, 247, 244);
            this.pnlFajl.Controls.Add(this.lblInfo);
            this.pnlFajl.Controls.Add(this.lblNapredak);
            this.pnlFajl.Controls.Add(this.progress);
            this.pnlFajl.Controls.Add(this.btnIzaberi);
            this.pnlFajl.Controls.Add(this.txtPutanja);
            this.pnlFajl.Controls.Add(this.lblFajl);
            this.pnlFajl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFajl.Location = new System.Drawing.Point(0, 42);
            this.pnlFajl.Name = "pnlFajl";
            this.pnlFajl.Size = new System.Drawing.Size(1200, 70);
            this.pnlFajl.TabIndex = 1;
            //
            // lblInfo
            //
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.lblInfo.Location = new System.Drawing.Point(12, 44);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(10, 15);
            this.lblInfo.TabIndex = 5;
            this.lblInfo.Text = "Prvi red fajla je zaglavlje. Kolona 1 = ID paketa (LargoPaketID), kolona 2 = količina (prazno = 1).";
            //
            // lblNapredak
            //
            this.lblNapredak.AutoSize = true;
            this.lblNapredak.Location = new System.Drawing.Point(972, 14);
            this.lblNapredak.Name = "lblNapredak";
            this.lblNapredak.Size = new System.Drawing.Size(0, 15);
            this.lblNapredak.TabIndex = 4;
            //
            // progress
            //
            this.progress.Location = new System.Drawing.Point(744, 11);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(220, 23);
            this.progress.TabIndex = 3;
            this.progress.Visible = false;
            //
            // btnIzaberi
            //
            this.btnIzaberi.Location = new System.Drawing.Point(618, 8);
            this.btnIzaberi.Name = "btnIzaberi";
            this.btnIzaberi.Size = new System.Drawing.Size(110, 28);
            this.btnIzaberi.TabIndex = 2;
            this.btnIzaberi.Text = "Izaberi fajl...";
            this.btnIzaberi.Click += new System.EventHandler(this.btnIzaberi_Click);
            //
            // txtPutanja
            //
            this.txtPutanja.Location = new System.Drawing.Point(50, 11);
            this.txtPutanja.Name = "txtPutanja";
            this.txtPutanja.Size = new System.Drawing.Size(560, 23);
            this.txtPutanja.TabIndex = 1;
            //
            // lblFajl
            //
            this.lblFajl.AutoSize = true;
            this.lblFajl.Location = new System.Drawing.Point(12, 14);
            this.lblFajl.Name = "lblFajl";
            this.lblFajl.Size = new System.Drawing.Size(29, 15);
            this.lblFajl.TabIndex = 0;
            this.lblFajl.Text = "Fajl:";
            //
            // grid
            //
            this.grid.AccessibleName = "Table";
            this.grid.AllowFiltering = true;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grid.Location = new System.Drawing.Point(0, 112);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(1200, 586);
            this.grid.TabIndex = 2;
            //
            // status
            //
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblPrikazano,
            this.lblIzabrano,
            this.lblKorisnik,
            this.lblObrada});
            this.status.Location = new System.Drawing.Point(0, 698);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(1200, 22);
            this.status.TabIndex = 3;
            //
            // lblPrikazano
            //
            this.lblPrikazano.Name = "lblPrikazano";
            this.lblPrikazano.Size = new System.Drawing.Size(70, 17);
            this.lblPrikazano.Text = "Prikazano: -";
            //
            // lblIzabrano
            //
            this.lblIzabrano.Margin = new System.Windows.Forms.Padding(20, 3, 0, 2);
            this.lblIzabrano.Name = "lblIzabrano";
            this.lblIzabrano.Size = new System.Drawing.Size(70, 17);
            this.lblIzabrano.Text = "Izabrano: 0";
            //
            // lblKorisnik
            //
            this.lblKorisnik.Margin = new System.Windows.Forms.Padding(20, 3, 0, 2);
            this.lblKorisnik.Name = "lblKorisnik";
            this.lblKorisnik.Size = new System.Drawing.Size(60, 17);
            this.lblKorisnik.Text = "Korisnik: -";
            //
            // lblObrada
            //
            this.lblObrada.Margin = new System.Windows.Forms.Padding(20, 3, 0, 2);
            this.lblObrada.Name = "lblObrada";
            this.lblObrada.Size = new System.Drawing.Size(120, 17);
            this.lblObrada.Text = "Poslednja obrada: -";
            //
            // frmTroskovniObracun
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 720);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.pnlFajl);
            this.Controls.Add(this.pnlDugmad);
            this.Controls.Add(this.status);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(900, 400);
            this.Name = "frmTroskovniObracun";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Troškovni obračun";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTroskovniObracun_FormClosing);
            this.pnlDugmad.ResumeLayout(false);
            this.pnlFajl.ResumeLayout(false);
            this.pnlFajl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlDugmad;
        private System.Windows.Forms.Button btnObradi;
        private System.Windows.Forms.Button btnOsvezi;
        private System.Windows.Forms.Button btnIzvozCsv;
        private System.Windows.Forms.Button btnIzvozExcel;
        private System.Windows.Forms.Button btnPreskoceni;
        private System.Windows.Forms.Panel pnlFajl;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblNapredak;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Button btnIzaberi;
        private System.Windows.Forms.TextBox txtPutanja;
        private System.Windows.Forms.Label lblFajl;
        private Syncfusion.WinForms.DataGrid.SfDataGrid grid;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel lblPrikazano;
        private System.Windows.Forms.ToolStripStatusLabel lblIzabrano;
        private System.Windows.Forms.ToolStripStatusLabel lblKorisnik;
        private System.Windows.Forms.ToolStripStatusLabel lblObrada;
    }
}
