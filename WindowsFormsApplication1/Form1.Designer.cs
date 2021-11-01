namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtDatumIsporuke = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblVozila = new System.Windows.Forms.Label();
            this.txtVozilo = new System.Windows.Forms.TextBox();
            this.btnPoDatumuIVozilu = new System.Windows.Forms.Button();
            this.btnInsertNakNalog = new System.Windows.Forms.Button();
            this.btnSastavnica = new System.Windows.Forms.Button();
            this.btnIzvuciStavku = new System.Windows.Forms.Button();
            this.txtSifraProizvoda = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
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
            this.dataGridView1.Location = new System.Drawing.Point(12, 121);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1377, 745);
            this.dataGridView1.TabIndex = 1;
            // 
            // txtDatumIsporuke
            // 
            this.txtDatumIsporuke.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtDatumIsporuke.Location = new System.Drawing.Point(241, 9);
            this.txtDatumIsporuke.Name = "txtDatumIsporuke";
            this.txtDatumIsporuke.Size = new System.Drawing.Size(278, 27);
            this.txtDatumIsporuke.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Datum isporuke";
            // 
            // lblVozila
            // 
            this.lblVozila.AutoSize = true;
            this.lblVozila.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblVozila.Location = new System.Drawing.Point(12, 42);
            this.lblVozila.Name = "lblVozila";
            this.lblVozila.Size = new System.Drawing.Size(55, 20);
            this.lblVozila.TabIndex = 4;
            this.lblVozila.Text = "Vozilo";
            // 
            // txtVozilo
            // 
            this.txtVozilo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtVozilo.Location = new System.Drawing.Point(241, 42);
            this.txtVozilo.Name = "txtVozilo";
            this.txtVozilo.Size = new System.Drawing.Size(278, 27);
            this.txtVozilo.TabIndex = 5;
            // 
            // btnPoDatumuIVozilu
            // 
            this.btnPoDatumuIVozilu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPoDatumuIVozilu.Location = new System.Drawing.Point(547, 12);
            this.btnPoDatumuIVozilu.Name = "btnPoDatumuIVozilu";
            this.btnPoDatumuIVozilu.Size = new System.Drawing.Size(305, 39);
            this.btnPoDatumuIVozilu.TabIndex = 6;
            this.btnPoDatumuIVozilu.Text = "Prikaži pregled po vozilu na datum";
            this.btnPoDatumuIVozilu.UseVisualStyleBackColor = true;
            this.btnPoDatumuIVozilu.Click += new System.EventHandler(this.btnPoDatumuIVozilu_Click);
            // 
            // btnInsertNakNalog
            // 
            this.btnInsertNakNalog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnInsertNakNalog.Location = new System.Drawing.Point(858, 12);
            this.btnInsertNakNalog.Name = "btnInsertNakNalog";
            this.btnInsertNakNalog.Size = new System.Drawing.Size(284, 41);
            this.btnInsertNakNalog.TabIndex = 8;
            this.btnInsertNakNalog.Text = "Prebaci nalog za utovar";
            this.btnInsertNakNalog.UseVisualStyleBackColor = true;
            this.btnInsertNakNalog.Click += new System.EventHandler(this.btnInsertNakNalog_Click);
            // 
            // btnSastavnica
            // 
            this.btnSastavnica.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSastavnica.Location = new System.Drawing.Point(547, 57);
            this.btnSastavnica.Name = "btnSastavnica";
            this.btnSastavnica.Size = new System.Drawing.Size(305, 38);
            this.btnSastavnica.TabIndex = 9;
            this.btnSastavnica.Text = "Prenesi sastavnicu";
            this.btnSastavnica.UseVisualStyleBackColor = true;
            this.btnSastavnica.Click += new System.EventHandler(this.btnSastavnica_Click);
            // 
            // btnIzvuciStavku
            // 
            this.btnIzvuciStavku.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIzvuciStavku.Location = new System.Drawing.Point(858, 59);
            this.btnIzvuciStavku.Name = "btnIzvuciStavku";
            this.btnIzvuciStavku.Size = new System.Drawing.Size(284, 38);
            this.btnIzvuciStavku.TabIndex = 10;
            this.btnIzvuciStavku.Text = "Izvuci jednu stavku";
            this.btnIzvuciStavku.UseVisualStyleBackColor = true;
            this.btnIzvuciStavku.Click += new System.EventHandler(this.btnIzvuciStavku_Click);
            // 
            // txtSifraProizvoda
            // 
            this.txtSifraProizvoda.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSifraProizvoda.Location = new System.Drawing.Point(241, 75);
            this.txtSifraProizvoda.Name = "txtSifraProizvoda";
            this.txtSifraProizvoda.Size = new System.Drawing.Size(278, 27);
            this.txtSifraProizvoda.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(12, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Proizvod";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(1148, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(241, 39);
            this.button1.TabIndex = 13;
            this.button1.Text = "Tanja Unos";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button2.Location = new System.Drawing.Point(1148, 54);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(241, 29);
            this.button2.TabIndex = 14;
            this.button2.Text = "Tanja Pregled";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button3.Location = new System.Drawing.Point(1148, 89);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(241, 29);
            this.button3.TabIndex = 15;
            this.button3.Text = "Tanja Brisanje";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1401, 878);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSifraProizvoda);
            this.Controls.Add(this.btnIzvuciStavku);
            this.Controls.Add(this.btnSastavnica);
            this.Controls.Add(this.btnInsertNakNalog);
            this.Controls.Add(this.btnPoDatumuIVozilu);
            this.Controls.Add(this.txtVozilo);
            this.Controls.Add(this.lblVozila);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDatumIsporuke);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Nalozi za utovar";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtDatumIsporuke;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblVozila;
        private System.Windows.Forms.TextBox txtVozilo;
        private System.Windows.Forms.Button btnPoDatumuIVozilu;
        private System.Windows.Forms.Button btnInsertNakNalog;
        private System.Windows.Forms.Button btnSastavnica;
        private System.Windows.Forms.Button btnIzvuciStavku;
        private System.Windows.Forms.TextBox txtSifraProizvoda;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}