namespace WindowsFormsApplication1.Magacin
{
    partial class frmProsecnaIzlaznost
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProsecnaIzlaznost));
            this.btnOsvezi = new System.Windows.Forms.Button();
            this.cboSkladisteIzbor = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpDatum1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dateDatum2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKoeficijent = new System.Windows.Forms.TextBox();
            this.cboKlijent = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkAktivna = new System.Windows.Forms.CheckBox();
            this.dtpDatum3 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpDatum4 = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOsvezi
            // 
            this.btnOsvezi.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOsvezi.BackgroundImage")));
            this.btnOsvezi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOsvezi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnOsvezi.ForeColor = System.Drawing.Color.White;
            this.btnOsvezi.Location = new System.Drawing.Point(24, 67);
            this.btnOsvezi.Name = "btnOsvezi";
            this.btnOsvezi.Size = new System.Drawing.Size(260, 25);
            this.btnOsvezi.TabIndex = 276;
            this.btnOsvezi.Text = "?";
            this.btnOsvezi.UseVisualStyleBackColor = true;
            // 
            // cboSkladisteIzbor
            // 
            this.cboSkladisteIzbor.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboSkladisteIzbor.FormattingEnabled = true;
            this.cboSkladisteIzbor.Location = new System.Drawing.Point(24, 37);
            this.cboSkladisteIzbor.Name = "cboSkladisteIzbor";
            this.cboSkladisteIzbor.Size = new System.Drawing.Size(260, 24);
            this.cboSkladisteIzbor.TabIndex = 275;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(21, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(167, 16);
            this.label5.TabIndex = 274;
            this.label5.Text = "Podrzumevano skladište:";
            // 
            // dtpDatum1
            // 
            this.dtpDatum1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum1.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpDatum1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatum1.Location = new System.Drawing.Point(312, 37);
            this.dtpDatum1.Name = "dtpDatum1";
            this.dtpDatum1.ShowUpDown = true;
            this.dtpDatum1.Size = new System.Drawing.Size(128, 22);
            this.dtpDatum1.TabIndex = 280;
            this.dtpDatum1.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(309, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 279;
            this.label1.Text = "Datum od";
            // 
            // dateDatum2
            // 
            this.dateDatum2.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dateDatum2.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dateDatum2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dateDatum2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateDatum2.Location = new System.Drawing.Point(468, 37);
            this.dateDatum2.Name = "dateDatum2";
            this.dateDatum2.ShowUpDown = true;
            this.dateDatum2.Size = new System.Drawing.Size(128, 22);
            this.dateDatum2.TabIndex = 282;
            this.dateDatum2.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(465, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 281;
            this.label2.Text = "Datum do";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(614, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 284;
            this.label3.Text = "Koeficijent:";
            // 
            // txtKoeficijent
            // 
            this.txtKoeficijent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtKoeficijent.Location = new System.Drawing.Point(617, 37);
            this.txtKoeficijent.Name = "txtKoeficijent";
            this.txtKoeficijent.Size = new System.Drawing.Size(134, 22);
            this.txtKoeficijent.TabIndex = 283;
            this.txtKoeficijent.Text = "1";
            // 
            // cboKlijent
            // 
            this.cboKlijent.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboKlijent.FormattingEnabled = true;
            this.cboKlijent.Location = new System.Drawing.Point(802, 37);
            this.cboKlijent.Name = "cboKlijent";
            this.cboKlijent.Size = new System.Drawing.Size(260, 24);
            this.cboKlijent.TabIndex = 286;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(799, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 16);
            this.label4.TabIndex = 285;
            this.label4.Text = "Klijent:";
            // 
            // chkAktivna
            // 
            this.chkAktivna.AutoSize = true;
            this.chkAktivna.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkAktivna.ForeColor = System.Drawing.Color.White;
            this.chkAktivna.Location = new System.Drawing.Point(312, 73);
            this.chkAktivna.Name = "chkAktivna";
            this.chkAktivna.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkAktivna.Size = new System.Drawing.Size(80, 19);
            this.chkAktivna.TabIndex = 287;
            this.chkAktivna.Text = "Svi klijenti";
            this.chkAktivna.UseVisualStyleBackColor = true;
            // 
            // dtpDatum3
            // 
            this.dtpDatum3.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum3.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpDatum3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatum3.Location = new System.Drawing.Point(312, 125);
            this.dtpDatum3.Name = "dtpDatum3";
            this.dtpDatum3.ShowUpDown = true;
            this.dtpDatum3.Size = new System.Drawing.Size(128, 22);
            this.dtpDatum3.TabIndex = 289;
            this.dtpDatum3.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(309, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 16);
            this.label6.TabIndex = 288;
            this.label6.Text = "Datum od izlaznost";
            // 
            // dtpDatum4
            // 
            this.dtpDatum4.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum4.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpDatum4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum4.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatum4.Location = new System.Drawing.Point(468, 125);
            this.dtpDatum4.Name = "dtpDatum4";
            this.dtpDatum4.ShowUpDown = true;
            this.dtpDatum4.Size = new System.Drawing.Size(128, 22);
            this.dtpDatum4.TabIndex = 291;
            this.dtpDatum4.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.ForeColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(465, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 16);
            this.label7.TabIndex = 290;
            this.label7.Text = "Datum do izlaznost";
            // 
            // frmProsecnaIzlaznost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1263, 713);
            this.Controls.Add(this.dtpDatum4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpDatum3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chkAktivna);
            this.Controls.Add(this.cboKlijent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtKoeficijent);
            this.Controls.Add(this.dateDatum2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpDatum1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOsvezi);
            this.Controls.Add(this.cboSkladisteIzbor);
            this.Controls.Add(this.label5);
            this.Name = "frmProsecnaIzlaznost";
            this.Text = "Prosečna izlaznost";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOsvezi;
        private System.Windows.Forms.ComboBox cboSkladisteIzbor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpDatum1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateDatum2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKoeficijent;
        private System.Windows.Forms.ComboBox cboKlijent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkAktivna;
        private System.Windows.Forms.DateTimePicker dtpDatum3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpDatum4;
        private System.Windows.Forms.Label label7;
    }
}