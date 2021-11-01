namespace WindowsFormsApplication1
{
    partial class frmRaspolozivoStanje
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRaspolozivoStanje));
            this.dtpVremeStanje = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDatum1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDatum22 = new System.Windows.Forms.DateTimePicker();
            this.dtpDatum2 = new System.Windows.Forms.Label();
            this.dtpDatum3 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDatum4 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDatum5 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOsvezi = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cboSkladiste2 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboSkladiste1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpVremeStanje
            // 
            this.dtpVremeStanje.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpVremeStanje.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpVremeStanje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpVremeStanje.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpVremeStanje.Location = new System.Drawing.Point(31, 38);
            this.dtpVremeStanje.Name = "dtpVremeStanje";
            this.dtpVremeStanje.ShowUpDown = true;
            this.dtpVremeStanje.Size = new System.Drawing.Size(128, 22);
            this.dtpVremeStanje.TabIndex = 207;
            this.dtpVremeStanje.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(28, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 16);
            this.label2.TabIndex = 206;
            this.label2.Text = "Stanje na dan";
            // 
            // dtpDatum1
            // 
            this.dtpDatum1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum1.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpDatum1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatum1.Location = new System.Drawing.Point(204, 38);
            this.dtpDatum1.Name = "dtpDatum1";
            this.dtpDatum1.ShowUpDown = true;
            this.dtpDatum1.Size = new System.Drawing.Size(128, 22);
            this.dtpDatum1.TabIndex = 209;
            this.dtpDatum1.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(201, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 16);
            this.label1.TabIndex = 208;
            this.label1.Text = "Planirana isporuka 1";
            // 
            // dtpDatum22
            // 
            this.dtpDatum22.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum22.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpDatum22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum22.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatum22.Location = new System.Drawing.Point(362, 38);
            this.dtpDatum22.Name = "dtpDatum22";
            this.dtpDatum22.ShowUpDown = true;
            this.dtpDatum22.Size = new System.Drawing.Size(128, 22);
            this.dtpDatum22.TabIndex = 211;
            this.dtpDatum22.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // dtpDatum2
            // 
            this.dtpDatum2.AutoSize = true;
            this.dtpDatum2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum2.ForeColor = System.Drawing.SystemColors.Control;
            this.dtpDatum2.Location = new System.Drawing.Point(359, 19);
            this.dtpDatum2.Name = "dtpDatum2";
            this.dtpDatum2.Size = new System.Drawing.Size(130, 16);
            this.dtpDatum2.TabIndex = 210;
            this.dtpDatum2.Text = "Planirana isporuka 2";
            // 
            // dtpDatum3
            // 
            this.dtpDatum3.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum3.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpDatum3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatum3.Location = new System.Drawing.Point(518, 38);
            this.dtpDatum3.Name = "dtpDatum3";
            this.dtpDatum3.ShowUpDown = true;
            this.dtpDatum3.Size = new System.Drawing.Size(128, 22);
            this.dtpDatum3.TabIndex = 213;
            this.dtpDatum3.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(515, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 16);
            this.label3.TabIndex = 212;
            this.label3.Text = "Planirana isporuka 3";
            // 
            // dtpDatum4
            // 
            this.dtpDatum4.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum4.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpDatum4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum4.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatum4.Location = new System.Drawing.Point(669, 38);
            this.dtpDatum4.Name = "dtpDatum4";
            this.dtpDatum4.ShowUpDown = true;
            this.dtpDatum4.Size = new System.Drawing.Size(128, 22);
            this.dtpDatum4.TabIndex = 215;
            this.dtpDatum4.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(666, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 16);
            this.label4.TabIndex = 214;
            this.label4.Text = "Planirana isporuka 4";
            // 
            // dtpDatum5
            // 
            this.dtpDatum5.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum5.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpDatum5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum5.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatum5.Location = new System.Drawing.Point(818, 38);
            this.dtpDatum5.Name = "dtpDatum5";
            this.dtpDatum5.ShowUpDown = true;
            this.dtpDatum5.Size = new System.Drawing.Size(128, 22);
            this.dtpDatum5.TabIndex = 217;
            this.dtpDatum5.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(815, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 16);
            this.label5.TabIndex = 216;
            this.label5.Text = "Planirana isporuka 5";
            // 
            // btnOsvezi
            // 
            this.btnOsvezi.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOsvezi.BackgroundImage")));
            this.btnOsvezi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOsvezi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnOsvezi.ForeColor = System.Drawing.Color.White;
            this.btnOsvezi.Location = new System.Drawing.Point(754, 66);
            this.btnOsvezi.Name = "btnOsvezi";
            this.btnOsvezi.Size = new System.Drawing.Size(191, 57);
            this.btnOsvezi.TabIndex = 243;
            this.btnOsvezi.Text = "Osveži";
            this.btnOsvezi.UseVisualStyleBackColor = true;
            this.btnOsvezi.Click += new System.EventHandler(this.btnOsvezi_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(31, 136);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1059, 418);
            this.dataGridView1.TabIndex = 272;
            // 
            // cboSkladiste2
            // 
            this.cboSkladiste2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboSkladiste2.FormattingEnabled = true;
            this.cboSkladiste2.Location = new System.Drawing.Point(477, 99);
            this.cboSkladiste2.Name = "cboSkladiste2";
            this.cboSkladiste2.Size = new System.Drawing.Size(260, 24);
            this.cboSkladiste2.TabIndex = 281;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(390, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 16);
            this.label7.TabIndex = 280;
            this.label7.Text = "Skladište 2:";
            // 
            // cboSkladiste1
            // 
            this.cboSkladiste1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboSkladiste1.FormattingEnabled = true;
            this.cboSkladiste1.Location = new System.Drawing.Point(104, 102);
            this.cboSkladiste1.Name = "cboSkladiste1";
            this.cboSkladiste1.Size = new System.Drawing.Size(260, 24);
            this.cboSkladiste1.TabIndex = 278;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(28, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 16);
            this.label8.TabIndex = 277;
            this.label8.Text = "Skladište:";
            // 
            // frmRaspolozivoStanje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1102, 566);
            this.Controls.Add(this.cboSkladiste2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboSkladiste1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnOsvezi);
            this.Controls.Add(this.dtpDatum5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpDatum4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpDatum3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpDatum22);
            this.Controls.Add(this.dtpDatum2);
            this.Controls.Add(this.dtpDatum1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpVremeStanje);
            this.Controls.Add(this.label2);
            this.Name = "frmRaspolozivoStanje";
            this.Text = "Pregled rezervacija po isporukama";
            this.Load += new System.EventHandler(this.frmRaspolozivoStanje_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpVremeStanje;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDatum1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDatum22;
        private System.Windows.Forms.Label dtpDatum2;
        private System.Windows.Forms.DateTimePicker dtpDatum3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDatum4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDatum5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOsvezi;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cboSkladiste2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboSkladiste1;
        private System.Windows.Forms.Label label8;
    }
}