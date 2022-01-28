namespace WindowsFormsApplication1.Meniji
{
    partial class frmIstorija
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIstorija));
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnsVrstaRobe = new FontAwesome.Sharp.IconButton();
            this.btnsZaposleni = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(796, 556);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(218, 65);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 146;
            this.pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnsVrstaRobe);
            this.panel1.Controls.Add(this.btnsZaposleni);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(296, 633);
            this.panel1.TabIndex = 147;
            // 
            // btnsVrstaRobe
            // 
            this.btnsVrstaRobe.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnsVrstaRobe.FlatAppearance.BorderSize = 0;
            this.btnsVrstaRobe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsVrstaRobe.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnsVrstaRobe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(54)))), ((int)(((byte)(56)))));
            this.btnsVrstaRobe.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnsVrstaRobe.IconColor = System.Drawing.Color.Black;
            this.btnsVrstaRobe.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnsVrstaRobe.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnsVrstaRobe.Location = new System.Drawing.Point(0, 40);
            this.btnsVrstaRobe.Name = "btnsVrstaRobe";
            this.btnsVrstaRobe.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.btnsVrstaRobe.Size = new System.Drawing.Size(296, 40);
            this.btnsVrstaRobe.TabIndex = 13;
            this.btnsVrstaRobe.Text = "Istorija rada - Paletar";
            this.btnsVrstaRobe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnsVrstaRobe.UseVisualStyleBackColor = true;
            this.btnsVrstaRobe.Click += new System.EventHandler(this.btnsVrstaRobe_Click);
            // 
            // btnsZaposleni
            // 
            this.btnsZaposleni.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnsZaposleni.FlatAppearance.BorderSize = 0;
            this.btnsZaposleni.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsZaposleni.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnsZaposleni.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(54)))), ((int)(((byte)(56)))));
            this.btnsZaposleni.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnsZaposleni.IconColor = System.Drawing.Color.Black;
            this.btnsZaposleni.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnsZaposleni.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnsZaposleni.Location = new System.Drawing.Point(0, 0);
            this.btnsZaposleni.Name = "btnsZaposleni";
            this.btnsZaposleni.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.btnsZaposleni.Size = new System.Drawing.Size(296, 40);
            this.btnsZaposleni.TabIndex = 10;
            this.btnsZaposleni.Text = "Istorija rada - Kontrolor";
            this.btnsZaposleni.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnsZaposleni.UseVisualStyleBackColor = true;
            this.btnsZaposleni.Click += new System.EventHandler(this.btnsZaposleni_Click);
            // 
            // frmIstorija
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1026, 633);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmIstorija";
            this.Text = "frmIstorija";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton btnsVrstaRobe;
        private FontAwesome.Sharp.IconButton btnsZaposleni;
    }
}