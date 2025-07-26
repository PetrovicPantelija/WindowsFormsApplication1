namespace WindowsFormsApplication1.Barcode
{
    partial class Pregled
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.pravaSkladišteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prijemniceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statistikaPrijemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.promenaLokacijeMPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trebovanjaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 48);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(440, 664);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Prijemnice";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(455, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Stavke prijemnice";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(458, 48);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(1273, 664);
            this.dataGridView2.TabIndex = 2;
            this.dataGridView2.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView2_RowPrePaint);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.DarkOrange;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pravaSkladišteToolStripMenuItem,
            this.prijemniceToolStripMenuItem,
            this.statistikaPrijemaToolStripMenuItem,
            this.promenaLokacijeMPToolStripMenuItem,
            this.trebovanjaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1862, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // pravaSkladišteToolStripMenuItem
            // 
            this.pravaSkladišteToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pravaSkladišteToolStripMenuItem.ForeColor = System.Drawing.Color.ForestGreen;
            this.pravaSkladišteToolStripMenuItem.Name = "pravaSkladišteToolStripMenuItem";
            this.pravaSkladišteToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.pravaSkladišteToolStripMenuItem.Text = "Prava-Skladište";
            this.pravaSkladišteToolStripMenuItem.Click += new System.EventHandler(this.pravaSkladišteToolStripMenuItem_Click);
            // 
            // prijemniceToolStripMenuItem
            // 
            this.prijemniceToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prijemniceToolStripMenuItem.ForeColor = System.Drawing.Color.ForestGreen;
            this.prijemniceToolStripMenuItem.Name = "prijemniceToolStripMenuItem";
            this.prijemniceToolStripMenuItem.Size = new System.Drawing.Size(90, 24);
            this.prijemniceToolStripMenuItem.Text = "Prijemnice";
            this.prijemniceToolStripMenuItem.Click += new System.EventHandler(this.prijemniceToolStripMenuItem_Click);
            // 
            // statistikaPrijemaToolStripMenuItem
            // 
            this.statistikaPrijemaToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statistikaPrijemaToolStripMenuItem.ForeColor = System.Drawing.Color.ForestGreen;
            this.statistikaPrijemaToolStripMenuItem.Name = "statistikaPrijemaToolStripMenuItem";
            this.statistikaPrijemaToolStripMenuItem.Size = new System.Drawing.Size(136, 24);
            this.statistikaPrijemaToolStripMenuItem.Text = "Statistika prijema";
            this.statistikaPrijemaToolStripMenuItem.Click += new System.EventHandler(this.statistikaPrijemaToolStripMenuItem_Click);
            // 
            // promenaLokacijeMPToolStripMenuItem
            // 
            this.promenaLokacijeMPToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.promenaLokacijeMPToolStripMenuItem.ForeColor = System.Drawing.Color.ForestGreen;
            this.promenaLokacijeMPToolStripMenuItem.Name = "promenaLokacijeMPToolStripMenuItem";
            this.promenaLokacijeMPToolStripMenuItem.Size = new System.Drawing.Size(160, 24);
            this.promenaLokacijeMPToolStripMenuItem.Text = "Promena lokacije MP";
            this.promenaLokacijeMPToolStripMenuItem.Click += new System.EventHandler(this.promenaLokacijeMPToolStripMenuItem_Click);
            // 
            // trebovanjaToolStripMenuItem
            // 
            this.trebovanjaToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.trebovanjaToolStripMenuItem.ForeColor = System.Drawing.Color.ForestGreen;
            this.trebovanjaToolStripMenuItem.Name = "trebovanjaToolStripMenuItem";
            this.trebovanjaToolStripMenuItem.Size = new System.Drawing.Size(94, 24);
            this.trebovanjaToolStripMenuItem.Text = "Trebovanja";
            this.trebovanjaToolStripMenuItem.Click += new System.EventHandler(this.trebovanjaToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 300000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.ForestGreen;
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(1737, 187);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 50);
            this.button1.TabIndex = 4;
            this.button1.Text = "Potvrdi količine";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.Orange;
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Location = new System.Drawing.Point(1737, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 50);
            this.button2.TabIndex = 4;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.Red;
            this.button3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button3.Location = new System.Drawing.Point(1737, 116);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 50);
            this.button3.TabIndex = 4;
            this.button3.Text = "Ponovi prijem";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Pregled
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1862, 724);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Pregled";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pregled";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Click += new System.EventHandler(this.Pregled_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem prijemniceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pravaSkladišteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statistikaPrijemaToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem promenaLokacijeMPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trebovanjaToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}