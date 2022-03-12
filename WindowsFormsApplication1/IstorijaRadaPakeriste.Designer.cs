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
            this.cboPaletirao = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPoKOntroloru = new FontAwesome.Sharp.IconButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(158)))), ((int)(((byte)(192)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(23, 90);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1184, 628);
            this.dataGridView1.TabIndex = 49;
            // 
            // cboPaletirao
            // 
            this.cboPaletirao.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboPaletirao.FormattingEnabled = true;
            this.cboPaletirao.Location = new System.Drawing.Point(23, 39);
            this.cboPaletirao.Margin = new System.Windows.Forms.Padding(2);
            this.cboPaletirao.Name = "cboPaletirao";
            this.cboPaletirao.Size = new System.Drawing.Size(162, 25);
            this.cboPaletirao.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(23, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 19);
            this.label1.TabIndex = 46;
            this.label1.Text = "Paletar:";
            // 
            // btnPoKOntroloru
            // 
            this.btnPoKOntroloru.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.btnPoKOntroloru.FlatAppearance.BorderSize = 0;
            this.btnPoKOntroloru.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPoKOntroloru.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnPoKOntroloru.IconChar = FontAwesome.Sharp.IconChar.FileAlt;
            this.btnPoKOntroloru.IconColor = System.Drawing.Color.Black;
            this.btnPoKOntroloru.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPoKOntroloru.IconSize = 22;
            this.btnPoKOntroloru.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPoKOntroloru.Location = new System.Drawing.Point(195, 39);
            this.btnPoKOntroloru.Name = "btnPoKOntroloru";
            this.btnPoKOntroloru.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnPoKOntroloru.Size = new System.Drawing.Size(183, 25);
            this.btnPoKOntroloru.TabIndex = 161;
            this.btnPoKOntroloru.Text = "Pronađi istoriju rada";
            this.btnPoKOntroloru.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPoKOntroloru.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPoKOntroloru.UseVisualStyleBackColor = false;
            this.btnPoKOntroloru.Click += new System.EventHandler(this.btnPoKOntroloru_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(1070, 39);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(137, 34);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 162;
            this.pictureBox2.TabStop = false;
            // 
            // IstorijaRadaPakeriste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1230, 743);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnPoKOntroloru);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cboPaletirao);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "IstorijaRadaPakeriste";
            this.Text = "Istorija Rada Pakeriste";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.IstorijaRadaPakeriste_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cboPaletirao;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton btnPoKOntroloru;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}