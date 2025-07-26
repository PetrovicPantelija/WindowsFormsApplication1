namespace WindowsFormsApplication1
{
    partial class frmPrijemnica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrijemnica));
            this.cboPrijemnica = new System.Windows.Forms.ComboBox();
            this.btnPrihvatiIzmene = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cboPrijemnica
            // 
            this.cboPrijemnica.FormattingEnabled = true;
            this.cboPrijemnica.Location = new System.Drawing.Point(109, 14);
            this.cboPrijemnica.Name = "cboPrijemnica";
            this.cboPrijemnica.Size = new System.Drawing.Size(161, 21);
            this.cboPrijemnica.TabIndex = 39;
            this.cboPrijemnica.SelectedIndexChanged += new System.EventHandler(this.cboPrijemnica_SelectedIndexChanged);
            this.cboPrijemnica.Leave += new System.EventHandler(this.cboPrijemnica_Leave_1);
            // 
            // btnPrihvatiIzmene
            // 
            this.btnPrihvatiIzmene.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrihvatiIzmene.BackgroundImage")));
            this.btnPrihvatiIzmene.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrihvatiIzmene.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPrihvatiIzmene.ForeColor = System.Drawing.Color.White;
            this.btnPrihvatiIzmene.Location = new System.Drawing.Point(614, 10);
            this.btnPrihvatiIzmene.Name = "btnPrihvatiIzmene";
            this.btnPrihvatiIzmene.Size = new System.Drawing.Size(253, 25);
            this.btnPrihvatiIzmene.TabIndex = 245;
            this.btnPrihvatiIzmene.Text = "Prihvati Izmene";
            this.btnPrihvatiIzmene.UseVisualStyleBackColor = true;
            this.btnPrihvatiIzmene.Click += new System.EventHandler(this.btnPrihvatiIzmene_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 246;
            this.label1.Text = "Prijemnica:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 41);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1129, 527);
            this.dataGridView1.TabIndex = 247;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(382, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(226, 25);
            this.button1.TabIndex = 248;
            this.button1.Text = "Postavi Količinu 2 na nulu";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(276, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(31, 25);
            this.button2.TabIndex = 249;
            this.button2.Text = "R";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(313, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(35, 25);
            this.button3.TabIndex = 250;
            this.button3.Text = "RS";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(873, 10);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(145, 25);
            this.button4.TabIndex = 245;
            this.button4.Text = "Otvori ponovo";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(1024, 10);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(120, 25);
            this.button5.TabIndex = 245;
            this.button5.Text = "Ponisti prijem";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btnPrihvatiIzmene_Click_1);
            // 
            // frmPrijemnica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1167, 580);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnPrihvatiIzmene);
            this.Controls.Add(this.cboPrijemnica);
            this.Name = "frmPrijemnica";
            this.Text = "Prijemnica količine";
            this.Load += new System.EventHandler(this.frmPrijemnica_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboPrijemnica;
        private System.Windows.Forms.Button btnPrihvatiIzmene;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}