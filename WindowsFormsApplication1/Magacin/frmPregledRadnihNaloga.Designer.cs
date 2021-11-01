namespace WindowsFormsApplication1.Magacin
{
    partial class frmPregledRadnihNaloga
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPregledRadnihNaloga));
            this.btnOsvezi = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnObeleзи = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOsvezi
            // 
            this.btnOsvezi.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOsvezi.BackgroundImage")));
            this.btnOsvezi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOsvezi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnOsvezi.ForeColor = System.Drawing.Color.White;
            this.btnOsvezi.Location = new System.Drawing.Point(12, 12);
            this.btnOsvezi.Name = "btnOsvezi";
            this.btnOsvezi.Size = new System.Drawing.Size(260, 25);
            this.btnOsvezi.TabIndex = 241;
            this.btnOsvezi.Text = "Refresh";
            this.btnOsvezi.UseVisualStyleBackColor = true;
            this.btnOsvezi.Click += new System.EventHandler(this.btnOsvezi_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 43);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(828, 400);
            this.dataGridView1.TabIndex = 242;
            // 
            // btnObeleзи
            // 
            this.btnObeleзи.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnObeleзи.BackgroundImage")));
            this.btnObeleзи.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnObeleзи.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnObeleзи.ForeColor = System.Drawing.Color.White;
            this.btnObeleзи.Location = new System.Drawing.Point(580, 12);
            this.btnObeleзи.Name = "btnObeleзи";
            this.btnObeleзи.Size = new System.Drawing.Size(260, 25);
            this.btnObeleзи.TabIndex = 243;
            this.btnObeleзи.Text = "Urađen";
            this.btnObeleзи.UseVisualStyleBackColor = true;
            this.btnObeleзи.Click += new System.EventHandler(this.btnObeleзи_Click);
            // 
            // frmPregledRadnihNaloga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(852, 455);
            this.Controls.Add(this.btnObeleзи);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnOsvezi);
            this.Name = "frmPregledRadnihNaloga";
            this.Text = "main";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOsvezi;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnObeleзи;
    }
}