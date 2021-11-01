namespace WindowsFormsApplication1.Magacin
{
    partial class frmPlanMedjuskladisni
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlanMedjuskladisni));
            this.btnOsvezi = new System.Windows.Forms.Button();
            this.cboSkladisteIzbor = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.cboMaticni = new System.Windows.Forms.ComboBox();
            this.txtPotrebnaKolicina = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.cboPozicija = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboSkladiste = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtKolicina = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.btnUbaci = new System.Windows.Forms.Button();
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
            this.label7 = new System.Windows.Forms.Label();
            this.cboVPIzdajno = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboVPPrijemno = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtOperater = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPotrebnaKolicina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKolicina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOsvezi
            // 
            this.btnOsvezi.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOsvezi.BackgroundImage")));
            this.btnOsvezi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOsvezi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnOsvezi.ForeColor = System.Drawing.Color.White;
            this.btnOsvezi.Location = new System.Drawing.Point(22, 69);
            this.btnOsvezi.Name = "btnOsvezi";
            this.btnOsvezi.Size = new System.Drawing.Size(260, 25);
            this.btnOsvezi.TabIndex = 237;
            this.btnOsvezi.Text = "Pretraži prazne";
            this.btnOsvezi.UseVisualStyleBackColor = true;
            this.btnOsvezi.Click += new System.EventHandler(this.btnOsvezi_Click);
            // 
            // cboSkladisteIzbor
            // 
            this.cboSkladisteIzbor.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboSkladisteIzbor.FormattingEnabled = true;
            this.cboSkladisteIzbor.Location = new System.Drawing.Point(22, 39);
            this.cboSkladisteIzbor.Name = "cboSkladisteIzbor";
            this.cboSkladisteIzbor.Size = new System.Drawing.Size(260, 24);
            this.cboSkladisteIzbor.TabIndex = 236;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(22, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 235;
            this.label5.Text = "Skladište:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(22, 170);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(260, 506);
            this.dataGridView1.TabIndex = 238;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(297, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 241;
            this.label2.Text = "MP naziv:";
            // 
            // cboMaticni
            // 
            this.cboMaticni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboMaticni.FormattingEnabled = true;
            this.cboMaticni.Location = new System.Drawing.Point(368, 38);
            this.cboMaticni.Name = "cboMaticni";
            this.cboMaticni.Size = new System.Drawing.Size(159, 24);
            this.cboMaticni.TabIndex = 240;
            // 
            // txtPotrebnaKolicina
            // 
            this.txtPotrebnaKolicina.DecimalPlaces = 2;
            this.txtPotrebnaKolicina.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPotrebnaKolicina.Location = new System.Drawing.Point(414, 79);
            this.txtPotrebnaKolicina.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtPotrebnaKolicina.Name = "txtPotrebnaKolicina";
            this.txtPotrebnaKolicina.Size = new System.Drawing.Size(113, 22);
            this.txtPotrebnaKolicina.TabIndex = 242;
            this.txtPotrebnaKolicina.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(290, 81);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(118, 16);
            this.label13.TabIndex = 243;
            this.label13.Text = "Potrebna količina.:";
            // 
            // cboPozicija
            // 
            this.cboPozicija.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboPozicija.FormattingEnabled = true;
            this.cboPozicija.Location = new System.Drawing.Point(289, 196);
            this.cboPozicija.Name = "cboPozicija";
            this.cboPozicija.Size = new System.Drawing.Size(238, 24);
            this.cboPozicija.TabIndex = 247;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(290, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 246;
            this.label3.Text = "Pozicija iz:";
            // 
            // cboSkladiste
            // 
            this.cboSkladiste.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboSkladiste.ForeColor = System.Drawing.Color.Black;
            this.cboSkladiste.FormattingEnabled = true;
            this.cboSkladiste.Location = new System.Drawing.Point(289, 140);
            this.cboSkladiste.Name = "cboSkladiste";
            this.cboSkladiste.Size = new System.Drawing.Size(238, 24);
            this.cboSkladiste.TabIndex = 245;
            this.cboSkladiste.SelectedIndexChanged += new System.EventHandler(this.cboSkladiste_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(290, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 16);
            this.label4.TabIndex = 244;
            this.label4.Text = "Skladište iz:";
            // 
            // txtKolicina
            // 
            this.txtKolicina.DecimalPlaces = 2;
            this.txtKolicina.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtKolicina.Location = new System.Drawing.Point(414, 250);
            this.txtKolicina.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtKolicina.Name = "txtKolicina";
            this.txtKolicina.Size = new System.Drawing.Size(113, 22);
            this.txtKolicina.TabIndex = 248;
            this.txtKolicina.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(290, 252);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 16);
            this.label6.TabIndex = 249;
            this.label6.Text = "Količina.:";
            // 
            // btnUbaci
            // 
            this.btnUbaci.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUbaci.BackgroundImage")));
            this.btnUbaci.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUbaci.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnUbaci.ForeColor = System.Drawing.Color.White;
            this.btnUbaci.Location = new System.Drawing.Point(293, 286);
            this.btnUbaci.Name = "btnUbaci";
            this.btnUbaci.Size = new System.Drawing.Size(210, 390);
            this.btnUbaci.TabIndex = 250;
            this.btnUbaci.Text = ">>>>";
            this.btnUbaci.UseVisualStyleBackColor = true;
            this.btnUbaci.Click += new System.EventHandler(this.btnUbaci_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(532, 98);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(677, 19);
            this.progressBar1.TabIndex = 253;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(819, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(390, 25);
            this.button1.TabIndex = 252;
            this.button1.Text = "Formiraj međuskladišni prenos";
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
            this.dataGridView2.Location = new System.Drawing.Point(531, 122);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(688, 554);
            this.dataGridView2.TabIndex = 251;
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(552, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 16);
            this.label7.TabIndex = 255;
            this.label7.Text = "VP Izdajno:";
            // 
            // cboVPIzdajno
            // 
            this.cboVPIzdajno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboVPIzdajno.FormattingEnabled = true;
            this.cboVPIzdajno.Location = new System.Drawing.Point(643, 41);
            this.cboVPIzdajno.Name = "cboVPIzdajno";
            this.cboVPIzdajno.Size = new System.Drawing.Size(159, 24);
            this.cboVPIzdajno.TabIndex = 254;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(552, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 16);
            this.label8.TabIndex = 257;
            this.label8.Text = "VP Prijemno:";
            // 
            // cboVPPrijemno
            // 
            this.cboVPPrijemno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboVPPrijemno.FormattingEnabled = true;
            this.cboVPPrijemno.Location = new System.Drawing.Point(643, 68);
            this.cboVPPrijemno.Name = "cboVPPrijemno";
            this.cboVPPrijemno.Size = new System.Drawing.Size(159, 24);
            this.cboVPPrijemno.TabIndex = 256;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(818, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 16);
            this.label9.TabIndex = 258;
            this.label9.Text = "Operater šifra:";
            // 
            // txtOperater
            // 
            this.txtOperater.Location = new System.Drawing.Point(917, 38);
            this.txtOperater.Name = "txtOperater";
            this.txtOperater.Size = new System.Drawing.Size(119, 20);
            this.txtOperater.TabIndex = 259;
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(22, 100);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(260, 25);
            this.button2.TabIndex = 260;
            this.button2.Text = "Pretraži prazne po redu";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(22, 131);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(260, 25);
            this.button3.TabIndex = 261;
            this.button3.Text = "Pretraži prazne po MP";
            this.button3.UseVisualStyleBackColor = true;
          //  this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // frmPlanMedjuskladisni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1231, 688);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtOperater);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboVPPrijemno);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboVPIzdajno);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.btnUbaci);
            this.Controls.Add(this.txtKolicina);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboPozicija);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboSkladiste);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPotrebnaKolicina);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboMaticni);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnOsvezi);
            this.Controls.Add(this.cboSkladisteIzbor);
            this.Controls.Add(this.label5);
            this.Name = "frmPlanMedjuskladisni";
            this.Text = "Plan medjuskladisnog prenosa";
            this.Load += new System.EventHandler(this.frmPlanMedjuskladisni_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPotrebnaKolicina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKolicina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOsvezi;
        private System.Windows.Forms.ComboBox cboSkladisteIzbor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboMaticni;
        private System.Windows.Forms.NumericUpDown txtPotrebnaKolicina;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cboPozicija;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboSkladiste;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txtKolicina;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnUbaci;
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboVPIzdajno;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboVPPrijemno;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtOperater;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}