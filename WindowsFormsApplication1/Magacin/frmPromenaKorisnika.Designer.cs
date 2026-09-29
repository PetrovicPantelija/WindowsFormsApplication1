namespace WindowsFormsApplication1.Magacin
{
    partial class frmPromenaKorisnika
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPromenaKorisnika));
            this.cboKorisnik = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboKorisnikNovi = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboKorisnik
            // 
            this.cboKorisnik.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboKorisnik.ForeColor = System.Drawing.Color.Black;
            this.cboKorisnik.FormattingEnabled = true;
            this.cboKorisnik.Location = new System.Drawing.Point(21, 27);
            this.cboKorisnik.Name = "cboKorisnik";
            this.cboKorisnik.Size = new System.Drawing.Size(262, 24);
            this.cboKorisnik.TabIndex = 257;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(22, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 16);
            this.label1.TabIndex = 256;
            this.label1.Text = "Korisnik trenutni:";
            // 
            // cboKorisnikNovi
            // 
            this.cboKorisnikNovi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboKorisnikNovi.ForeColor = System.Drawing.Color.Black;
            this.cboKorisnikNovi.FormattingEnabled = true;
            this.cboKorisnikNovi.Location = new System.Drawing.Point(21, 87);
            this.cboKorisnikNovi.Name = "cboKorisnikNovi";
            this.cboKorisnikNovi.Size = new System.Drawing.Size(262, 24);
            this.cboKorisnikNovi.TabIndex = 259;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(22, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 16);
            this.label2.TabIndex = 258;
            this.label2.Text = "Korisnik novi:";
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(25, 135);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(258, 25);
            this.button3.TabIndex = 264;
            this.button3.Text = "Promeni korisnika";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // frmPromenaKorisnika
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(340, 190);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.cboKorisnikNovi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboKorisnik);
            this.Controls.Add(this.label1);
            this.Icon = global::WindowsFormsApplication1.Properties.Resources.JelaIkona;
            this.Name = "frmPromenaKorisnika";
            this.Text = "Promena korisnika Uzimanje/Ulaganje";
            this.Load += new System.EventHandler(this.frmPromenaKorisnika_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboKorisnik;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboKorisnikNovi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
    }
}