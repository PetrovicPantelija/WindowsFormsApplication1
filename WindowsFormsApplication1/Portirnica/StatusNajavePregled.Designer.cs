
namespace WindowsFormsApplication1.Portirnica
{
    partial class StatusNajavePregled
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
            this.najavljeniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.primljeniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spremniZaIzlazakToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.izašliToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sviToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.najavljeniToolStripMenuItem,
            this.primljeniToolStripMenuItem,
            this.spremniZaIzlazakToolStripMenuItem,
            this.izašliToolStripMenuItem,
            this.sviToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(948, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // najavljeniToolStripMenuItem
            // 
            this.najavljeniToolStripMenuItem.Name = "najavljeniToolStripMenuItem";
            this.najavljeniToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.najavljeniToolStripMenuItem.Text = "Najavljeni";
            this.najavljeniToolStripMenuItem.Click += new System.EventHandler(this.najavljeniToolStripMenuItem_Click);
            // 
            // primljeniToolStripMenuItem
            // 
            this.primljeniToolStripMenuItem.Name = "primljeniToolStripMenuItem";
            this.primljeniToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.primljeniToolStripMenuItem.Text = "Primljeni";
            this.primljeniToolStripMenuItem.Click += new System.EventHandler(this.primljeniToolStripMenuItem_Click);
            // 
            // spremniZaIzlazakToolStripMenuItem
            // 
            this.spremniZaIzlazakToolStripMenuItem.Name = "spremniZaIzlazakToolStripMenuItem";
            this.spremniZaIzlazakToolStripMenuItem.Size = new System.Drawing.Size(114, 20);
            this.spremniZaIzlazakToolStripMenuItem.Text = "Spremni za izlazak";
            this.spremniZaIzlazakToolStripMenuItem.Click += new System.EventHandler(this.spremniZaIzlazakToolStripMenuItem_Click);
            // 
            // izašliToolStripMenuItem
            // 
            this.izašliToolStripMenuItem.Name = "izašliToolStripMenuItem";
            this.izašliToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.izašliToolStripMenuItem.Text = "Izašli";
            this.izašliToolStripMenuItem.Click += new System.EventHandler(this.izašliToolStripMenuItem_Click);
            // 
            // sviToolStripMenuItem
            // 
            this.sviToolStripMenuItem.Name = "sviToolStripMenuItem";
            this.sviToolStripMenuItem.Size = new System.Drawing.Size(34, 20);
            this.sviToolStripMenuItem.Text = "Svi";
            this.sviToolStripMenuItem.Click += new System.EventHandler(this.sviToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 27);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(924, 402);
            this.dataGridView1.TabIndex = 62;
            // 
            // StatusNajavePregled
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Icon = global::WindowsFormsApplication1.Properties.Resources.JelaIkona;
            this.Name = "StatusNajavePregled";
            this.Text = "StatusNajavePregled";
            this.Load += new System.EventHandler(this.StatusNajavePregled_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem najavljeniToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem primljeniToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spremniZaIzlazakToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem izašliToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sviToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}