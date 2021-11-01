namespace WindowsFormsApplication1.Magacin
{
    partial class frmPregledIzdavanja
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPregledIzdavanja));
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnOsvezi = new System.Windows.Forms.Button();
            this.cboSkladisteIzbor = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtOperater = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboVPIzdajno = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.PrSifSklad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrLokacija = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrSifMp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MpStaraSif = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MpNaziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MpDoNaziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MPSifEnoteMere1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnUbaci = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(48, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 244;
            this.label1.Text = "Izdavanja:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(48, 113);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(404, 260);
            this.dataGridView1.TabIndex = 243;
            // 
            // btnOsvezi
            // 
            this.btnOsvezi.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOsvezi.BackgroundImage")));
            this.btnOsvezi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOsvezi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnOsvezi.ForeColor = System.Drawing.Color.White;
            this.btnOsvezi.Location = new System.Drawing.Point(48, 62);
            this.btnOsvezi.Name = "btnOsvezi";
            this.btnOsvezi.Size = new System.Drawing.Size(260, 25);
            this.btnOsvezi.TabIndex = 242;
            this.btnOsvezi.Text = "?";
            this.btnOsvezi.UseVisualStyleBackColor = true;
            this.btnOsvezi.Click += new System.EventHandler(this.btnOsvezi_Click);
            // 
            // cboSkladisteIzbor
            // 
            this.cboSkladisteIzbor.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboSkladisteIzbor.FormattingEnabled = true;
            this.cboSkladisteIzbor.Location = new System.Drawing.Point(48, 32);
            this.cboSkladisteIzbor.Name = "cboSkladisteIzbor";
            this.cboSkladisteIzbor.Size = new System.Drawing.Size(260, 24);
            this.cboSkladisteIzbor.TabIndex = 241;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(48, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 240;
            this.label5.Text = "Skladište:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(449, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 16);
            this.label9.TabIndex = 265;
            this.label9.Text = "Operater šifra:";
            // 
            // txtOperater
            // 
            this.txtOperater.Location = new System.Drawing.Point(547, 43);
            this.txtOperater.Name = "txtOperater";
            this.txtOperater.Size = new System.Drawing.Size(143, 20);
            this.txtOperater.TabIndex = 266;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(449, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 16);
            this.label7.TabIndex = 264;
            this.label7.Text = "VP Izdajno:";
            // 
            // cboVPIzdajno
            // 
            this.cboVPIzdajno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboVPIzdajno.FormattingEnabled = true;
            this.cboVPIzdajno.Location = new System.Drawing.Point(547, 11);
            this.cboVPIzdajno.Name = "cboVPIzdajno";
            this.cboVPIzdajno.Size = new System.Drawing.Size(143, 24);
            this.cboVPIzdajno.TabIndex = 263;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(547, 68);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(570, 19);
            this.progressBar1.TabIndex = 262;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(714, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(403, 25);
            this.button1.TabIndex = 261;
            this.button1.Text = "Formiraj izdavnje / trebovanje";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PrSifSklad,
            this.PrLokacija,
            this.PrSifMp,
            this.MpStaraSif,
            this.MpNaziv,
            this.MpDoNaziv,
            this.Kolicina,
            this.MPSifEnoteMere1});
            this.dataGridView2.Location = new System.Drawing.Point(547, 94);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(643, 279);
            this.dataGridView2.TabIndex = 260;
            // 
            // PrSifSklad
            // 
            this.PrSifSklad.HeaderText = "PrSifSklad";
            this.PrSifSklad.Name = "PrSifSklad";
            // 
            // PrLokacija
            // 
            this.PrLokacija.HeaderText = "PrLokacija";
            this.PrLokacija.Name = "PrLokacija";
            // 
            // PrSifMp
            // 
            this.PrSifMp.HeaderText = "PrSifMp";
            this.PrSifMp.Name = "PrSifMp";
            // 
            // MpStaraSif
            // 
            this.MpStaraSif.HeaderText = "MpStaraSif";
            this.MpStaraSif.Name = "MpStaraSif";
            // 
            // MpNaziv
            // 
            this.MpNaziv.HeaderText = "MpNaziv";
            this.MpNaziv.Name = "MpNaziv";
            // 
            // MpDoNaziv
            // 
            this.MpDoNaziv.HeaderText = "MpDoNaziv";
            this.MpDoNaziv.Name = "MpDoNaziv";
            // 
            // Kolicina
            // 
            this.Kolicina.HeaderText = "Kolicina";
            this.Kolicina.Name = "Kolicina";
            // 
            // MPSifEnoteMere1
            // 
            this.MPSifEnoteMere1.HeaderText = "MPSifEnoteMere1";
            this.MPSifEnoteMere1.Name = "MPSifEnoteMere1";
            // 
            // btnUbaci
            // 
            this.btnUbaci.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUbaci.BackgroundImage")));
            this.btnUbaci.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUbaci.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnUbaci.ForeColor = System.Drawing.Color.White;
            this.btnUbaci.Location = new System.Drawing.Point(458, 113);
            this.btnUbaci.Name = "btnUbaci";
            this.btnUbaci.Size = new System.Drawing.Size(83, 260);
            this.btnUbaci.TabIndex = 267;
            this.btnUbaci.Text = ">>>>";
            this.btnUbaci.UseVisualStyleBackColor = true;
            this.btnUbaci.Click += new System.EventHandler(this.btnUbaci_Click);
            // 
            // frmPregledIzdavanja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1206, 385);
            this.Controls.Add(this.btnUbaci);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtOperater);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboVPIzdajno);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnOsvezi);
            this.Controls.Add(this.cboSkladisteIzbor);
            this.Controls.Add(this.label5);
            this.Name = "frmPregledIzdavanja";
            this.Text = "Izdatnice / Trebovanja";
            this.Load += new System.EventHandler(this.frmPregledIzdavanja_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnOsvezi;
        private System.Windows.Forms.ComboBox cboSkladisteIzbor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtOperater;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboVPIzdajno;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrSifSklad;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrLokacija;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrSifMp;
        private System.Windows.Forms.DataGridViewTextBoxColumn MpStaraSif;
        private System.Windows.Forms.DataGridViewTextBoxColumn MpNaziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn MpDoNaziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn MPSifEnoteMere1;
        private System.Windows.Forms.Button btnUbaci;
    }
}