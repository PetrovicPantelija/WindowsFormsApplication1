using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    partial class frmRedosledPakovanja
    {
        private IContainer components = null;

        private DataGridView dataGridView1;
        private TextBox txtRedosled;
        private TextBox txtMpSifra;
        private TextBox txtMpCode;
        private TextBox txtKolicina;
        private TextBox txtMpNadredjeni;
        private Button btnUvozExcel;
        private Button btnDodaj;
        private Button btnPrebaci;
        private Button btnObrisi;
        private Label lblRedosled;
        private Label lblMpSifra;
        private Label lblMpCode;
        private Label lblKolicina;
        private Label lblMpNadredjeni;
        private Label label1;

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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtRedosled = new System.Windows.Forms.TextBox();
            this.txtMpSifra = new System.Windows.Forms.TextBox();
            this.txtMpCode = new System.Windows.Forms.TextBox();
            this.txtKolicina = new System.Windows.Forms.TextBox();
            this.txtMpNadredjeni = new System.Windows.Forms.TextBox();
            this.btnUvozExcel = new System.Windows.Forms.Button();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.btnPrebaci = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.lblRedosled = new System.Windows.Forms.Label();
            this.lblMpSifra = new System.Windows.Forms.Label();
            this.lblMpCode = new System.Windows.Forms.Label();
            this.lblKolicina = new System.Windows.Forms.Label();
            this.lblMpNadredjeni = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 159);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(760, 291);
            this.dataGridView1.TabIndex = 13;
            // 
            // txtRedosled
            // 
            this.txtRedosled.Location = new System.Drawing.Point(101, 12);
            this.txtRedosled.Name = "txtRedosled";
            this.txtRedosled.Size = new System.Drawing.Size(140, 20);
            this.txtRedosled.TabIndex = 1;
            // 
            // txtMpSifra
            // 
            this.txtMpSifra.Location = new System.Drawing.Point(101, 42);
            this.txtMpSifra.Name = "txtMpSifra";
            this.txtMpSifra.Size = new System.Drawing.Size(120, 20);
            this.txtMpSifra.TabIndex = 3;
            // 
            // txtMpCode
            // 
            this.txtMpCode.Location = new System.Drawing.Point(101, 71);
            this.txtMpCode.Name = "txtMpCode";
            this.txtMpCode.Size = new System.Drawing.Size(193, 20);
            this.txtMpCode.TabIndex = 5;
            // 
            // txtKolicina
            // 
            this.txtKolicina.Location = new System.Drawing.Point(103, 133);
            this.txtKolicina.Name = "txtKolicina";
            this.txtKolicina.Size = new System.Drawing.Size(140, 20);
            this.txtKolicina.TabIndex = 7;
            // 
            // txtMpNadredjeni
            // 
            this.txtMpNadredjeni.Location = new System.Drawing.Point(101, 99);
            this.txtMpNadredjeni.Name = "txtMpNadredjeni";
            this.txtMpNadredjeni.Size = new System.Drawing.Size(129, 20);
            this.txtMpNadredjeni.TabIndex = 9;
            // 
            // btnUvozExcel
            // 
            this.btnUvozExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUvozExcel.Location = new System.Drawing.Point(343, 12);
            this.btnUvozExcel.Name = "btnUvozExcel";
            this.btnUvozExcel.Size = new System.Drawing.Size(120, 25);
            this.btnUvozExcel.TabIndex = 10;
            this.btnUvozExcel.Text = "Uvoz iz Excela";
            this.btnUvozExcel.UseVisualStyleBackColor = true;
            this.btnUvozExcel.Click += new System.EventHandler(this.btnUvozExcel_Click);
            // 
            // btnDodaj
            // 
            this.btnDodaj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDodaj.Location = new System.Drawing.Point(479, 12);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(120, 25);
            this.btnDodaj.TabIndex = 12;
            this.btnDodaj.Text = "Dodaj";
            this.btnDodaj.UseVisualStyleBackColor = true;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // btnPrebaci
            // 
            this.btnPrebaci.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrebaci.Location = new System.Drawing.Point(605, 12);
            this.btnPrebaci.Name = "btnPrebaci";
            this.btnPrebaci.Size = new System.Drawing.Size(110, 25);
            this.btnPrebaci.TabIndex = 11;
            this.btnPrebaci.Text = "Prebaci u Bazu";
            this.btnPrebaci.UseVisualStyleBackColor = true;
            this.btnPrebaci.Click += new System.EventHandler(this.btnPrebaci_Click);
            // 
            // btnObrisi
            // 
            this.btnObrisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObrisi.Location = new System.Drawing.Point(605, 43);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(110, 25);
            this.btnObrisi.TabIndex = 14;
            this.btnObrisi.Text = "Obriši po MpSifra/MpNd";
            this.btnObrisi.UseVisualStyleBackColor = true;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // lblRedosled
            // 
            this.lblRedosled.AutoSize = true;
            this.lblRedosled.Location = new System.Drawing.Point(12, 15);
            this.lblRedosled.Name = "lblRedosled";
            this.lblRedosled.Size = new System.Drawing.Size(55, 13);
            this.lblRedosled.TabIndex = 0;
            this.lblRedosled.Text = "Redosled:";
            // 
            // lblMpSifra
            // 
            this.lblMpSifra.AutoSize = true;
            this.lblMpSifra.Location = new System.Drawing.Point(13, 42);
            this.lblMpSifra.Name = "lblMpSifra";
            this.lblMpSifra.Size = new System.Drawing.Size(46, 13);
            this.lblMpSifra.TabIndex = 2;
            this.lblMpSifra.Text = "MpSifra:";
            // 
            // lblMpCode
            // 
            this.lblMpCode.AutoSize = true;
            this.lblMpCode.Location = new System.Drawing.Point(9, 71);
            this.lblMpCode.Name = "lblMpCode";
            this.lblMpCode.Size = new System.Drawing.Size(50, 13);
            this.lblMpCode.TabIndex = 4;
            this.lblMpCode.Text = "MpCode:";
            // 
            // lblKolicina
            // 
            this.lblKolicina.AutoSize = true;
            this.lblKolicina.Location = new System.Drawing.Point(9, 131);
            this.lblKolicina.Name = "lblKolicina";
            this.lblKolicina.Size = new System.Drawing.Size(40, 13);
            this.lblKolicina.TabIndex = 6;
            this.lblKolicina.Text = "MpKol:";
            // 
            // lblMpNadredjeni
            // 
            this.lblMpNadredjeni.AutoSize = true;
            this.lblMpNadredjeni.Location = new System.Drawing.Point(9, 102);
            this.lblMpNadredjeni.Name = "lblMpNadredjeni";
            this.lblMpNadredjeni.Size = new System.Drawing.Size(76, 13);
            this.lblMpNadredjeni.TabIndex = 8;
            this.lblMpNadredjeni.Text = "MpNadredjeni:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(294, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(478, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Potrebna polja Excel: Redosled, MpSifra, MpCode, Kolicina, MpNdredjen(Na koji se " +
    "odnose podaci)i";
            // 
            // frmRedosledPakovanja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.btnUvozExcel);
            this.Controls.Add(this.btnPrebaci);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.txtMpNadredjeni);
            this.Controls.Add(this.lblMpNadredjeni);
            this.Controls.Add(this.txtKolicina);
            this.Controls.Add(this.lblKolicina);
            this.Controls.Add(this.txtMpCode);
            this.Controls.Add(this.lblMpCode);
            this.Controls.Add(this.txtMpSifra);
            this.Controls.Add(this.lblMpSifra);
            this.Controls.Add(this.txtRedosled);
            this.Controls.Add(this.lblRedosled);
            this.Name = "frmRedosledPakovanja";
            this.Text = "Redosled pakovanja";
            this.Load += new System.EventHandler(this.frmRedosledPakovanja_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}