namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    partial class RealizacijaPlanaPakovanja
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
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Rezervisi = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cboSkladiste = new System.Windows.Forms.ComboBox();
            this.btnSvi = new System.Windows.Forms.Button();
            this.btnPretrazi = new System.Windows.Forms.Button();
            this.btnNalogOkov = new System.Windows.Forms.Button();
            this.btnNalogAmbalaza = new System.Windows.Forms.Button();
            this.btnNalogPanel = new System.Windows.Forms.Button();
            this.btnRezervisiZbirno = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtArtikal = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnPotvrda = new System.Windows.Forms.Button();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.btnNalogStiropor = new System.Windows.Forms.Button();
            this.btnCSV = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.listMagacin = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.btnKompletirano = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 30);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1143, 304);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Stavke plana pakovanja";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(12, 372);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(1096, 465);
            this.dataGridView2.TabIndex = 2;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(1223, 205);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(604, 632);
            this.dataGridView3.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(1220, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Potrebno za proizvodnju";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(12, 351);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Artikli prvog nivoa sastavnice";
            // 
            // btn_Rezervisi
            // 
            this.btn_Rezervisi.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_Rezervisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Rezervisi.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Rezervisi.Location = new System.Drawing.Point(1114, 574);
            this.btn_Rezervisi.Name = "btn_Rezervisi";
            this.btn_Rezervisi.Size = new System.Drawing.Size(103, 49);
            this.btn_Rezervisi.TabIndex = 6;
            this.btn_Rezervisi.Text = "Rezerviši pojedinačno";
            this.btn_Rezervisi.UseVisualStyleBackColor = false;
            this.btn_Rezervisi.Click += new System.EventHandler(this.btn_Rezervisi_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(1114, 427);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(103, 32);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(1218, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Pretraži po skladištu";
            // 
            // cboSkladiste
            // 
            this.cboSkladiste.FormattingEnabled = true;
            this.cboSkladiste.Location = new System.Drawing.Point(1221, 176);
            this.cboSkladiste.Name = "cboSkladiste";
            this.cboSkladiste.Size = new System.Drawing.Size(247, 21);
            this.cboSkladiste.TabIndex = 9;
            // 
            // btnSvi
            // 
            this.btnSvi.Location = new System.Drawing.Point(1554, 175);
            this.btnSvi.Name = "btnSvi";
            this.btnSvi.Size = new System.Drawing.Size(72, 24);
            this.btnSvi.TabIndex = 10;
            this.btnSvi.Text = "Svi";
            this.btnSvi.UseVisualStyleBackColor = true;
            this.btnSvi.Click += new System.EventHandler(this.btnSvi_Click);
            // 
            // btnPretrazi
            // 
            this.btnPretrazi.Location = new System.Drawing.Point(1474, 175);
            this.btnPretrazi.Name = "btnPretrazi";
            this.btnPretrazi.Size = new System.Drawing.Size(74, 24);
            this.btnPretrazi.TabIndex = 11;
            this.btnPretrazi.Text = "Pretraži";
            this.btnPretrazi.UseVisualStyleBackColor = true;
            this.btnPretrazi.Click += new System.EventHandler(this.btnPretrazi_Click);
            // 
            // btnNalogOkov
            // 
            this.btnNalogOkov.BackColor = System.Drawing.Color.DarkOrange;
            this.btnNalogOkov.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNalogOkov.ForeColor = System.Drawing.Color.White;
            this.btnNalogOkov.Location = new System.Drawing.Point(1640, 12);
            this.btnNalogOkov.Name = "btnNalogOkov";
            this.btnNalogOkov.Size = new System.Drawing.Size(187, 27);
            this.btnNalogOkov.TabIndex = 12;
            this.btnNalogOkov.Text = "Nalog za pripremu okova";
            this.btnNalogOkov.UseVisualStyleBackColor = false;
            this.btnNalogOkov.Click += new System.EventHandler(this.btnNalogOkov_Click);
            // 
            // btnNalogAmbalaza
            // 
            this.btnNalogAmbalaza.BackColor = System.Drawing.Color.DarkOrange;
            this.btnNalogAmbalaza.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNalogAmbalaza.ForeColor = System.Drawing.Color.White;
            this.btnNalogAmbalaza.Location = new System.Drawing.Point(1640, 45);
            this.btnNalogAmbalaza.Name = "btnNalogAmbalaza";
            this.btnNalogAmbalaza.Size = new System.Drawing.Size(187, 27);
            this.btnNalogAmbalaza.TabIndex = 12;
            this.btnNalogAmbalaza.Text = "Nalog za pripremu ambalaže";
            this.btnNalogAmbalaza.UseVisualStyleBackColor = false;
            this.btnNalogAmbalaza.Click += new System.EventHandler(this.btnNalogAmbalaza_Click);
            // 
            // btnNalogPanel
            // 
            this.btnNalogPanel.BackColor = System.Drawing.Color.DarkOrange;
            this.btnNalogPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNalogPanel.ForeColor = System.Drawing.Color.White;
            this.btnNalogPanel.Location = new System.Drawing.Point(1640, 111);
            this.btnNalogPanel.Name = "btnNalogPanel";
            this.btnNalogPanel.Size = new System.Drawing.Size(187, 27);
            this.btnNalogPanel.TabIndex = 12;
            this.btnNalogPanel.Text = "Nalog za izradu panela\r\n";
            this.btnNalogPanel.UseVisualStyleBackColor = false;
            this.btnNalogPanel.Click += new System.EventHandler(this.btnNalogPanel_Click);
            // 
            // btnRezervisiZbirno
            // 
            this.btnRezervisiZbirno.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnRezervisiZbirno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRezervisiZbirno.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRezervisiZbirno.Location = new System.Drawing.Point(1114, 519);
            this.btnRezervisiZbirno.Name = "btnRezervisiZbirno";
            this.btnRezervisiZbirno.Size = new System.Drawing.Size(103, 49);
            this.btnRezervisiZbirno.TabIndex = 14;
            this.btnRezervisiZbirno.Text = "Rezerviši po rasporedu";
            this.btnRezervisiZbirno.UseVisualStyleBackColor = false;
            this.btnRezervisiZbirno.Click += new System.EventHandler(this.btnRezervisiZbirno_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtArtikal);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnPotvrda);
            this.groupBox1.Controls.Add(this.dataGridView4);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(1223, 244);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(604, 403);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // txtArtikal
            // 
            this.txtArtikal.Location = new System.Drawing.Point(291, 30);
            this.txtArtikal.Name = "txtArtikal";
            this.txtArtikal.Size = new System.Drawing.Size(100, 22);
            this.txtArtikal.TabIndex = 4;
            this.txtArtikal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtArtikal_KeyPress);
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button2.Location = new System.Drawing.Point(507, 21);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 40);
            this.button2.TabIndex = 3;
            this.button2.Text = "Nazad";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(298, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "Pronađi artikal";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(205, 16);
            this.label5.TabIndex = 2;
            this.label5.Text = "Raspored pakovanja i realizacije";
            // 
            // btnPotvrda
            // 
            this.btnPotvrda.BackColor = System.Drawing.Color.DarkOrange;
            this.btnPotvrda.Location = new System.Drawing.Point(410, 21);
            this.btnPotvrda.Name = "btnPotvrda";
            this.btnPotvrda.Size = new System.Drawing.Size(91, 40);
            this.btnPotvrda.TabIndex = 1;
            this.btnPotvrda.Text = "Potvrdi rezervacije";
            this.btnPotvrda.UseVisualStyleBackColor = false;
            this.btnPotvrda.Click += new System.EventHandler(this.btnPotvrda_Click);
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(6, 67);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.Size = new System.Drawing.Size(592, 330);
            this.dataGridView4.TabIndex = 0;
            // 
            // btnNalogStiropor
            // 
            this.btnNalogStiropor.BackColor = System.Drawing.Color.DarkOrange;
            this.btnNalogStiropor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNalogStiropor.ForeColor = System.Drawing.Color.White;
            this.btnNalogStiropor.Location = new System.Drawing.Point(1640, 78);
            this.btnNalogStiropor.Name = "btnNalogStiropor";
            this.btnNalogStiropor.Size = new System.Drawing.Size(187, 27);
            this.btnNalogStiropor.TabIndex = 12;
            this.btnNalogStiropor.Text = "Nalog za pripremu stiropora";
            this.btnNalogStiropor.UseVisualStyleBackColor = false;
            this.btnNalogStiropor.Click += new System.EventHandler(this.btnNalogStiropor_Click);
            // 
            // btnCSV
            // 
            this.btnCSV.BackColor = System.Drawing.Color.LimeGreen;
            this.btnCSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCSV.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCSV.Location = new System.Drawing.Point(1730, 172);
            this.btnCSV.Name = "btnCSV";
            this.btnCSV.Size = new System.Drawing.Size(97, 27);
            this.btnCSV.TabIndex = 16;
            this.btnCSV.Text = "Export CSV";
            this.btnCSV.UseVisualStyleBackColor = false;
            this.btnCSV.Click += new System.EventHandler(this.btnCSV_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(1223, 33);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(310, 84);
            this.checkedListBox1.TabIndex = 17;
            // 
            // listMagacin
            // 
            this.listMagacin.BackColor = System.Drawing.Color.Moccasin;
            this.listMagacin.Location = new System.Drawing.Point(1539, 78);
            this.listMagacin.Name = "listMagacin";
            this.listMagacin.Size = new System.Drawing.Size(75, 42);
            this.listMagacin.TabIndex = 18;
            this.listMagacin.Text = "Potvrdi magacin";
            this.listMagacin.UseVisualStyleBackColor = false;
            this.listMagacin.Click += new System.EventHandler(this.listMagacin_Click);
            // 
            // btnObrisi
            // 
            this.btnObrisi.BackColor = System.Drawing.Color.Red;
            this.btnObrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnObrisi.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnObrisi.Location = new System.Drawing.Point(1114, 372);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(103, 49);
            this.btnObrisi.TabIndex = 19;
            this.btnObrisi.Text = "Obriši";
            this.btnObrisi.UseVisualStyleBackColor = false;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // btnKompletirano
            // 
            this.btnKompletirano.BackColor = System.Drawing.Color.Yellow;
            this.btnKompletirano.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKompletirano.ForeColor = System.Drawing.Color.Black;
            this.btnKompletirano.Location = new System.Drawing.Point(1640, 144);
            this.btnKompletirano.Name = "btnKompletirano";
            this.btnKompletirano.Size = new System.Drawing.Size(187, 25);
            this.btnKompletirano.TabIndex = 12;
            this.btnKompletirano.Text = "Nalog za kompletirane";
            this.btnKompletirano.UseVisualStyleBackColor = false;
            this.btnKompletirano.Click += new System.EventHandler(this.btnKompletirano_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Yellow;
            this.button1.Location = new System.Drawing.Point(1161, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 42);
            this.button1.TabIndex = 20;
            this.button1.Text = "Komplet";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // RealizacijaPlanaPakovanja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.ClientSize = new System.Drawing.Size(1839, 849);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.listMagacin);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.btnCSV);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRezervisiZbirno);
            this.Controls.Add(this.btnKompletirano);
            this.Controls.Add(this.btnNalogPanel);
            this.Controls.Add(this.btnNalogStiropor);
            this.Controls.Add(this.btnNalogAmbalaza);
            this.Controls.Add(this.btnNalogOkov);
            this.Controls.Add(this.btnPretrazi);
            this.Controls.Add(this.btnSvi);
            this.Controls.Add(this.cboSkladiste);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btn_Rezervisi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Icon = global::WindowsFormsApplication1.Properties.Resources.JelaIkona;
            this.Name = "RealizacijaPlanaPakovanja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RealizacijaPlanaPakovanja";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RealizacijaPlanaPakovanja_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Rezervisi;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboSkladiste;
        private System.Windows.Forms.Button btnSvi;
        private System.Windows.Forms.Button btnPretrazi;
        private System.Windows.Forms.Button btnNalogOkov;
        private System.Windows.Forms.Button btnNalogAmbalaza;
        private System.Windows.Forms.Button btnNalogPanel;
        private System.Windows.Forms.Button btnRezervisiZbirno;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPotvrda;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnNalogStiropor;
        private System.Windows.Forms.Button btnCSV;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button listMagacin;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.TextBox txtArtikal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnKompletirano;
        private System.Windows.Forms.Button button1;
    }
}