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
            this.dataGridView1 = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnOsvezi = new FontAwesome.Sharp.IconButton();
            this.btnObeleзи = new FontAwesome.Sharp.IconButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AccessibleName = "Table";
            this.dataGridView1.AllowFiltering = true;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.TabIndex = 242;
            // 
            // btnOsvezi
            // 
            this.btnOsvezi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.btnOsvezi.FlatAppearance.BorderSize = 0;
            this.btnOsvezi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOsvezi.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnOsvezi.IconChar = FontAwesome.Sharp.IconChar.Redo;
            this.btnOsvezi.IconColor = System.Drawing.Color.Black;
            this.btnOsvezi.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnOsvezi.IconSize = 23;
            this.btnOsvezi.Location = new System.Drawing.Point(12, 14);
            this.btnOsvezi.Name = "btnOsvezi";
            this.btnOsvezi.Size = new System.Drawing.Size(128, 30);
            this.btnOsvezi.TabIndex = 298;
            this.btnOsvezi.Text = "Osveži";
            this.btnOsvezi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOsvezi.UseVisualStyleBackColor = false;
            this.btnOsvezi.Click += new System.EventHandler(this.btnOsvezi_Click);
            // 
            // btnObeleзи
            // 
            this.btnObeleзи.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.btnObeleзи.FlatAppearance.BorderSize = 0;
            this.btnObeleзи.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnObeleзи.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnObeleзи.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnObeleзи.IconColor = System.Drawing.Color.Black;
            this.btnObeleзи.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnObeleзи.IconSize = 23;
            this.btnObeleзи.Location = new System.Drawing.Point(166, 14);
            this.btnObeleзи.Name = "btnObeleзи";
            this.btnObeleзи.Size = new System.Drawing.Size(128, 30);
            this.btnObeleзи.TabIndex = 299;
            this.btnObeleзи.Text = "Urađen";
            this.btnObeleзи.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnObeleзи.UseVisualStyleBackColor = false;
            this.btnObeleзи.Click += new System.EventHandler(this.btnObeleзи_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(703, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(137, 54);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 300;
            this.pictureBox2.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox2);
            this.splitContainer1.Panel1.Controls.Add(this.btnOsvezi);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(852, 455);
            this.splitContainer1.SplitterDistance = 58;
            this.splitContainer1.TabIndex = 300;
            // 
            // frmPregledRadnihNaloga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(852, 455);
            this.Controls.Add(this.btnObeleзи);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPregledRadnihNaloga";
            this.Text = "main";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Syncfusion.WinForms.DataGrid.SfDataGrid dataGridView1;
        private FontAwesome.Sharp.IconButton btnOsvezi;
        private FontAwesome.Sharp.IconButton btnObeleзи;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}