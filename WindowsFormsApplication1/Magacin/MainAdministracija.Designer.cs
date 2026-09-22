namespace WindowsFormsApplication1.Magacin
{
    partial class MainAdministracija
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.wEBSPodaciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rkShippingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.korisniciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pravaPristupaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grupeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dodeliGrupuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dodeliFormuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skladistaPravaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wEBSPodaciToolStripMenuItem,
            this.pravaPristupaToolStripMenuItem,
            this.skladistaPravaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 455);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1056, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // wEBSPodaciToolStripMenuItem
            // 
            this.wEBSPodaciToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rkShippingToolStripMenuItem,
            this.korisniciToolStripMenuItem});
            this.wEBSPodaciToolStripMenuItem.Name = "wEBSPodaciToolStripMenuItem";
            this.wEBSPodaciToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.wEBSPodaciToolStripMenuItem.Text = "Šifarnici";
            this.wEBSPodaciToolStripMenuItem.Click += new System.EventHandler(this.wEBSPodaciToolStripMenuItem_Click);
            // 
            // rkShippingToolStripMenuItem
            // 
            this.rkShippingToolStripMenuItem.Name = "rkShippingToolStripMenuItem";
            this.rkShippingToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.rkShippingToolStripMenuItem.Text = "Organizacione jedinice";
            this.rkShippingToolStripMenuItem.Click += new System.EventHandler(this.rkShippingToolStripMenuItem_Click);
            // 
            // korisniciToolStripMenuItem
            // 
            this.korisniciToolStripMenuItem.Name = "korisniciToolStripMenuItem";
            this.korisniciToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.korisniciToolStripMenuItem.Text = "Korisnici";
            this.korisniciToolStripMenuItem.Click += new System.EventHandler(this.korisniciToolStripMenuItem_Click);
            // 
            // pravaPristupaToolStripMenuItem
            // 
            this.pravaPristupaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grupeToolStripMenuItem,
            this.formeToolStripMenuItem,
            this.dodeliGrupuToolStripMenuItem,
            this.dodeliFormuToolStripMenuItem});
            this.pravaPristupaToolStripMenuItem.Name = "pravaPristupaToolStripMenuItem";
            this.pravaPristupaToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.pravaPristupaToolStripMenuItem.Text = "Prava Pristupa";
            // 
            // grupeToolStripMenuItem
            // 
            this.grupeToolStripMenuItem.Name = "grupeToolStripMenuItem";
            this.grupeToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.grupeToolStripMenuItem.Text = "Grupe";
            this.grupeToolStripMenuItem.Click += new System.EventHandler(this.grupeToolStripMenuItem_Click);
            // 
            // formeToolStripMenuItem
            // 
            this.formeToolStripMenuItem.Name = "formeToolStripMenuItem";
            this.formeToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.formeToolStripMenuItem.Text = "Forme";
            this.formeToolStripMenuItem.Click += new System.EventHandler(this.formeToolStripMenuItem_Click);
            // 
            // dodeliGrupuToolStripMenuItem
            // 
            this.dodeliGrupuToolStripMenuItem.Name = "dodeliGrupuToolStripMenuItem";
            this.dodeliGrupuToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.dodeliGrupuToolStripMenuItem.Text = "Dodeli Grupu";
            this.dodeliGrupuToolStripMenuItem.Click += new System.EventHandler(this.dodeliGrupuToolStripMenuItem_Click);
            // 
            // dodeliFormuToolStripMenuItem
            // 
            this.dodeliFormuToolStripMenuItem.Name = "dodeliFormuToolStripMenuItem";
            this.dodeliFormuToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.dodeliFormuToolStripMenuItem.Text = "Dodeli Pravo";
            this.dodeliFormuToolStripMenuItem.Click += new System.EventHandler(this.dodeliFormuToolStripMenuItem_Click);
            // 
            // skladistaPravaToolStripMenuItem
            // 
            this.skladistaPravaToolStripMenuItem.Name = "skladistaPravaToolStripMenuItem";
            this.skladistaPravaToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.skladistaPravaToolStripMenuItem.Text = "Skladista prava";
            this.skladistaPravaToolStripMenuItem.Click += new System.EventHandler(this.skladistaPravaToolStripMenuItem_Click);
            // 
            // MainAdministracija
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1056, 479);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainAdministracija";
            this.Text = "main";
            this.Load += new System.EventHandler(this.MainAdministracija_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem wEBSPodaciToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rkShippingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem korisniciToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pravaPristupaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grupeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dodeliGrupuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dodeliFormuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem skladistaPravaToolStripMenuItem;
    }
}