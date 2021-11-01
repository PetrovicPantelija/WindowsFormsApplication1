namespace WindowsFormsApplication1
{
    partial class IstorijaRadaKontrolor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IstorijaRadaKontrolor));
            this.btnPoKOntroloru = new System.Windows.Forms.Button();
            this.cboKontrolisao = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPoKOntroloru
            // 
            this.btnPoKOntroloru.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPoKOntroloru.Location = new System.Drawing.Point(371, 3);
            this.btnPoKOntroloru.Name = "btnPoKOntroloru";
            this.btnPoKOntroloru.Size = new System.Drawing.Size(176, 35);
            this.btnPoKOntroloru.TabIndex = 44;
            this.btnPoKOntroloru.Text = "Pronađi ist. rada";
            this.btnPoKOntroloru.UseVisualStyleBackColor = true;
            this.btnPoKOntroloru.Click += new System.EventHandler(this.btnPoKOntroloru_Click);
            // 
            // cboKontrolisao
            // 
            this.cboKontrolisao.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboKontrolisao.FormattingEnabled = true;
            this.cboKontrolisao.Location = new System.Drawing.Point(169, 7);
            this.cboKontrolisao.Name = "cboKontrolisao";
            this.cboKontrolisao.Size = new System.Drawing.Size(196, 28);
            this.cboKontrolisao.TabIndex = 42;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(26, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 20);
            this.label1.TabIndex = 40;
            this.label1.Text = "Kontrolisao:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(17, 51);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1579, 773);
            this.dataGridView1.TabIndex = 45;
            // 
            // IstorijaRadaKontrolor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1608, 836);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnPoKOntroloru);
            this.Controls.Add(this.cboKontrolisao);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IstorijaRadaKontrolor";
            this.Text = "Istorija Rada Kontrolora";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.IstorijaRadaKontrolor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPoKOntroloru;
        private System.Windows.Forms.ComboBox cboKontrolisao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}