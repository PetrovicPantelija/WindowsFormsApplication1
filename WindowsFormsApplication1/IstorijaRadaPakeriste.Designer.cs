namespace WindowsFormsApplication1
{
    partial class IstorijaRadaPakeriste
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IstorijaRadaPakeriste));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnPoKOntroloru = new System.Windows.Forms.Button();
            this.cboPaletirao = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(11, 60);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1579, 773);
            this.dataGridView1.TabIndex = 49;
            // 
            // btnPoKOntroloru
            // 
            this.btnPoKOntroloru.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPoKOntroloru.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPoKOntroloru.Location = new System.Drawing.Point(394, 12);
            this.btnPoKOntroloru.Name = "btnPoKOntroloru";
            this.btnPoKOntroloru.Size = new System.Drawing.Size(176, 35);
            this.btnPoKOntroloru.TabIndex = 48;
            this.btnPoKOntroloru.Text = "Pronađi ist. rada";
            this.btnPoKOntroloru.UseVisualStyleBackColor = false;
            this.btnPoKOntroloru.Click += new System.EventHandler(this.btnPoKOntroloru_Click);
            // 
            // cboPaletirao
            // 
            this.cboPaletirao.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboPaletirao.FormattingEnabled = true;
            this.cboPaletirao.Location = new System.Drawing.Point(163, 16);
            this.cboPaletirao.Name = "cboPaletirao";
            this.cboPaletirao.Size = new System.Drawing.Size(196, 28);
            this.cboPaletirao.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(20, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 46;
            this.label1.Text = "Paletar:";
            // 
            // IstorijaRadaPakeriste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1169, 674);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnPoKOntroloru);
            this.Controls.Add(this.cboPaletirao);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IstorijaRadaPakeriste";
            this.Text = "Istorija Rada Pakeriste";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.IstorijaRadaPakeriste_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnPoKOntroloru;
        private System.Windows.Forms.ComboBox cboPaletirao;
        private System.Windows.Forms.Label label1;
    }
}