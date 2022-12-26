namespace WindowsFormsApplication1
{
    partial class Reklamacije
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
            this.btnInsertNakNalog = new System.Windows.Forms.Button();
            this.btnPoDatumuIVozilu = new System.Windows.Forms.Button();
            this.txtVozilo = new System.Windows.Forms.TextBox();
            this.lblVozila = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDatumIsporuke = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInsertNakNalog
            // 
            this.btnInsertNakNalog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnInsertNakNalog.Location = new System.Drawing.Point(881, 12);
            this.btnInsertNakNalog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnInsertNakNalog.Name = "btnInsertNakNalog";
            this.btnInsertNakNalog.Size = new System.Drawing.Size(320, 41);
            this.btnInsertNakNalog.TabIndex = 14;
            this.btnInsertNakNalog.Text = "Prebaci nalog za utovar";
            this.btnInsertNakNalog.UseVisualStyleBackColor = true;
            this.btnInsertNakNalog.Click += new System.EventHandler(this.btnInsertNakNalog_Click);
            // 
            // btnPoDatumuIVozilu
            // 
            this.btnPoDatumuIVozilu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPoDatumuIVozilu.Location = new System.Drawing.Point(539, 12);
            this.btnPoDatumuIVozilu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPoDatumuIVozilu.Name = "btnPoDatumuIVozilu";
            this.btnPoDatumuIVozilu.Size = new System.Drawing.Size(320, 39);
            this.btnPoDatumuIVozilu.TabIndex = 13;
            this.btnPoDatumuIVozilu.Text = "Prikaži pregled po vozilu na datum";
            this.btnPoDatumuIVozilu.UseVisualStyleBackColor = true;
            this.btnPoDatumuIVozilu.Click += new System.EventHandler(this.btnPoDatumuIVozilu_Click);
            // 
            // txtVozilo
            // 
            this.txtVozilo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtVozilo.Location = new System.Drawing.Point(233, 50);
            this.txtVozilo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtVozilo.Name = "txtVozilo";
            this.txtVozilo.Size = new System.Drawing.Size(279, 27);
            this.txtVozilo.TabIndex = 12;
            // 
            // lblVozila
            // 
            this.lblVozila.AutoSize = true;
            this.lblVozila.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblVozila.Location = new System.Drawing.Point(4, 50);
            this.lblVozila.Name = "lblVozila";
            this.lblVozila.Size = new System.Drawing.Size(55, 20);
            this.lblVozila.TabIndex = 11;
            this.lblVozila.Text = "Vozilo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(1, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Datum utovara";
            // 
            // txtDatumIsporuke
            // 
            this.txtDatumIsporuke.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtDatumIsporuke.Location = new System.Drawing.Point(233, 9);
            this.txtDatumIsporuke.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDatumIsporuke.Name = "txtDatumIsporuke";
            this.txtDatumIsporuke.Size = new System.Drawing.Size(279, 27);
            this.txtDatumIsporuke.TabIndex = 9;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(8, 108);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1193, 425);
            this.dataGridView1.TabIndex = 15;
            // 
            // Reklamacije
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1257, 546);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnInsertNakNalog);
            this.Controls.Add(this.btnPoDatumuIVozilu);
            this.Controls.Add(this.txtVozilo);
            this.Controls.Add(this.lblVozila);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDatumIsporuke);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Reklamacije";
            this.Text = "Reklamacije";
            this.Load += new System.EventHandler(this.Reklamacije_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInsertNakNalog;
        private System.Windows.Forms.Button btnPoDatumuIVozilu;
        private System.Windows.Forms.TextBox txtVozilo;
        private System.Windows.Forms.Label lblVozila;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDatumIsporuke;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}