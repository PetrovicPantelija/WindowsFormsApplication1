namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    partial class StavkePlanaPakovanjaNovi
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
            this.btnNazad = new System.Windows.Forms.Button();
            this.btnPretrazi = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtRnDo = new System.Windows.Forms.TextBox();
            this.txtRNod = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.btnPretraziRN = new System.Windows.Forms.Button();
            this.btnPretrazipoDatumu = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.cboKolekcija = new System.Windows.Forms.ComboBox();
            this.cboVrstaRN = new System.Windows.Forms.ComboBox();
            this.cboPlan = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.txtMP = new System.Windows.Forms.TextBox();
            this.btnMP = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDodeli = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.btnIzmeni = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.txtArtikal = new System.Windows.Forms.TextBox();
            this.txtKolicina = new System.Windows.Forms.TextBox();
            this.txtTermin = new System.Windows.Forms.TextBox();
            this.txtVrstaRN = new System.Windows.Forms.TextBox();
            this.txtStatusRN = new System.Windows.Forms.TextBox();
            this.txtRN = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtPlanPakovanja = new System.Windows.Forms.TextBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.btnRealizacija = new System.Windows.Forms.Button();
            this.btnKolicine = new System.Windows.Forms.Button();
            this.btnIzvestaj = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(534, 49);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1021, 269);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // btnNazad
            // 
            this.btnNazad.BackColor = System.Drawing.Color.CadetBlue;
            this.btnNazad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNazad.Location = new System.Drawing.Point(534, 12);
            this.btnNazad.Name = "btnNazad";
            this.btnNazad.Size = new System.Drawing.Size(70, 31);
            this.btnNazad.TabIndex = 1;
            this.btnNazad.Text = "Nazad";
            this.btnNazad.UseVisualStyleBackColor = false;
            this.btnNazad.Click += new System.EventHandler(this.btnNazad_Click);
            // 
            // btnPretrazi
            // 
            this.btnPretrazi.Location = new System.Drawing.Point(133, 260);
            this.btnPretrazi.Name = "btnPretrazi";
            this.btnPretrazi.Size = new System.Drawing.Size(95, 40);
            this.btnPretrazi.TabIndex = 2;
            this.btnPretrazi.Text = "Pretraži po kolekciji";
            this.btnPretrazi.UseVisualStyleBackColor = true;
            this.btnPretrazi.Click += new System.EventHandler(this.btnPretrazi_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkOrange;
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtRnDo);
            this.groupBox1.Controls.Add(this.txtRNod);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnPretraziRN);
            this.groupBox1.Controls.Add(this.btnPretrazipoDatumu);
            this.groupBox1.Controls.Add(this.btnPretrazi);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.cboKolekcija);
            this.groupBox1.Controls.Add(this.cboVrstaRN);
            this.groupBox1.Controls.Add(this.cboPlan);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(504, 306);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dodaj stavke";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(348, 140);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(61, 16);
            this.label17.TabIndex = 5;
            this.label17.Text = "Vrsta RN";
            // 
            // txtRnDo
            // 
            this.txtRnDo.Location = new System.Drawing.Point(63, 226);
            this.txtRnDo.Name = "txtRnDo";
            this.txtRnDo.Size = new System.Drawing.Size(86, 22);
            this.txtRnDo.TabIndex = 4;
            // 
            // txtRNod
            // 
            this.txtRNod.Location = new System.Drawing.Point(63, 194);
            this.txtRNod.Name = "txtRNod";
            this.txtRNod.Size = new System.Drawing.Size(86, 22);
            this.txtRNod.TabIndex = 4;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 229);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(51, 16);
            this.label16.TabIndex = 3;
            this.label16.Text = "RN Do:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 197);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 16);
            this.label15.TabIndex = 3;
            this.label15.Text = "RN Od:";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(150, 161);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(120, 22);
            this.dateTimePicker2.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.YellowGreen;
            this.button1.Location = new System.Drawing.Point(6, 260);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 40);
            this.button1.TabIndex = 2;
            this.button1.Text = "Pretraži po planu";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnPretraziRN
            // 
            this.btnPretraziRN.Location = new System.Drawing.Point(391, 260);
            this.btnPretraziRN.Name = "btnPretraziRN";
            this.btnPretraziRN.Size = new System.Drawing.Size(95, 40);
            this.btnPretraziRN.TabIndex = 2;
            this.btnPretraziRN.Text = "Pretraži po RN";
            this.btnPretraziRN.UseVisualStyleBackColor = true;
            this.btnPretraziRN.Click += new System.EventHandler(this.btnPretraziRN_Click);
            // 
            // btnPretrazipoDatumu
            // 
            this.btnPretrazipoDatumu.Location = new System.Drawing.Point(261, 260);
            this.btnPretrazipoDatumu.Name = "btnPretrazipoDatumu";
            this.btnPretrazipoDatumu.Size = new System.Drawing.Size(95, 40);
            this.btnPretrazipoDatumu.TabIndex = 2;
            this.btnPretrazipoDatumu.Text = "Pretraži po datumu";
            this.btnPretrazipoDatumu.UseVisualStyleBackColor = true;
            this.btnPretrazipoDatumu.Click += new System.EventHandler(this.btnPretrazipoDatumu_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(6, 161);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(120, 22);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // cboKolekcija
            // 
            this.cboKolekcija.FormattingEnabled = true;
            this.cboKolekcija.Location = new System.Drawing.Point(6, 104);
            this.cboKolekcija.Name = "cboKolekcija";
            this.cboKolekcija.Size = new System.Drawing.Size(290, 24);
            this.cboKolekcija.TabIndex = 1;
            // 
            // cboVrstaRN
            // 
            this.cboVrstaRN.FormattingEnabled = true;
            this.cboVrstaRN.Location = new System.Drawing.Point(284, 159);
            this.cboVrstaRN.Name = "cboVrstaRN";
            this.cboVrstaRN.Size = new System.Drawing.Size(202, 24);
            this.cboVrstaRN.TabIndex = 1;
            this.cboVrstaRN.SelectionChangeCommitted += new System.EventHandler(this.cboPlan_SelectionChangeCommitted);
            this.cboVrstaRN.SelectedValueChanged += new System.EventHandler(this.cboPlan_SelectedValueChanged);
            // 
            // cboPlan
            // 
            this.cboPlan.FormattingEnabled = true;
            this.cboPlan.Location = new System.Drawing.Point(6, 45);
            this.cboPlan.Name = "cboPlan";
            this.cboPlan.Size = new System.Drawing.Size(290, 24);
            this.cboPlan.TabIndex = 1;
            this.cboPlan.SelectionChangeCommitted += new System.EventHandler(this.cboPlan_SelectionChangeCommitted);
            this.cboPlan.SelectedValueChanged += new System.EventHandler(this.cboPlan_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(147, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Datum Do";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Datum Od";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Kolekcija";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Plan proizvodnje";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(12, 365);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(1543, 321);
            this.dataGridView2.TabIndex = 4;
            this.dataGridView2.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView2_RowPrePaint);
            this.dataGridView2.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
            // 
            // txtMP
            // 
            this.txtMP.Location = new System.Drawing.Point(827, 18);
            this.txtMP.Name = "txtMP";
            this.txtMP.Size = new System.Drawing.Size(116, 20);
            this.txtMP.TabIndex = 5;
            // 
            // btnMP
            // 
            this.btnMP.Location = new System.Drawing.Point(949, 14);
            this.btnMP.Name = "btnMP";
            this.btnMP.Size = new System.Drawing.Size(94, 26);
            this.btnMP.TabIndex = 2;
            this.btnMP.Text = "Pretraži MP";
            this.btnMP.UseVisualStyleBackColor = true;
            this.btnMP.Click += new System.EventHandler(this.btnMP_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(1049, 14);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(94, 26);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDodeli
            // 
            this.btnDodeli.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDodeli.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDodeli.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDodeli.Location = new System.Drawing.Point(758, 324);
            this.btnDodeli.Name = "btnDodeli";
            this.btnDodeli.Size = new System.Drawing.Size(123, 35);
            this.btnDodeli.TabIndex = 6;
            this.btnDodeli.Text = "Dodeli planu";
            this.btnDodeli.UseVisualStyleBackColor = false;
            this.btnDodeli.Click += new System.EventHandler(this.btnDodeli_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dateTimePicker3);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.btnIzmeni);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btnOdustani);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtNaziv);
            this.groupBox2.Controls.Add(this.txtArtikal);
            this.groupBox2.Controls.Add(this.txtKolicina);
            this.groupBox2.Controls.Add(this.txtTermin);
            this.groupBox2.Controls.Add(this.txtVrstaRN);
            this.groupBox2.Controls.Add(this.txtStatusRN);
            this.groupBox2.Controls.Add(this.txtRN);
            this.groupBox2.Controls.Add(this.txtID);
            this.groupBox2.Controls.Add(this.txtPlanPakovanja);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox2.Location = new System.Drawing.Point(374, 389);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(612, 232);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Izmeni stavku";
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker3.Location = new System.Drawing.Point(380, 144);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(189, 22);
            this.dateTimePicker3.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(413, 93);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 32);
            this.label12.TabIndex = 1;
            this.label12.Text = "Količina za \r\nproizvodnju";
            // 
            // btnIzmeni
            // 
            this.btnIzmeni.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnIzmeni.Location = new System.Drawing.Point(416, 185);
            this.btnIzmeni.Name = "btnIzmeni";
            this.btnIzmeni.Size = new System.Drawing.Size(70, 31);
            this.btnIzmeni.TabIndex = 2;
            this.btnIzmeni.Text = "Potvrdi";
            this.btnIzmeni.UseVisualStyleBackColor = false;
            this.btnIzmeni.Click += new System.EventHandler(this.btnIzmeni_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(269, 140);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(105, 32);
            this.label13.TabIndex = 1;
            this.label13.Text = "Planirani krajnji \r\ndatum završetka";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(269, 103);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 16);
            this.label11.TabIndex = 1;
            this.label11.Text = "Termin";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(269, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 16);
            this.label10.TabIndex = 1;
            this.label10.Text = "Naziv";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 188);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 16);
            this.label9.TabIndex = 1;
            this.label9.Text = "Artikal";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 16);
            this.label8.TabIndex = 1;
            this.label8.Text = "Vrsta RN";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "Status RN";
            // 
            // btnOdustani
            // 
            this.btnOdustani.BackColor = System.Drawing.Color.CadetBlue;
            this.btnOdustani.Location = new System.Drawing.Point(499, 185);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(70, 31);
            this.btnOdustani.TabIndex = 1;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.UseVisualStyleBackColor = false;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Broj RN";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(42, 32);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(20, 16);
            this.label14.TabIndex = 1;
            this.label14.Text = "ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Plan pakovanja";
            // 
            // txtNaziv
            // 
            this.txtNaziv.Enabled = false;
            this.txtNaziv.Location = new System.Drawing.Point(319, 26);
            this.txtNaziv.Multiline = true;
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(250, 53);
            this.txtNaziv.TabIndex = 0;
            // 
            // txtArtikal
            // 
            this.txtArtikal.Enabled = false;
            this.txtArtikal.Location = new System.Drawing.Point(112, 185);
            this.txtArtikal.Name = "txtArtikal";
            this.txtArtikal.Size = new System.Drawing.Size(100, 22);
            this.txtArtikal.TabIndex = 0;
            // 
            // txtKolicina
            // 
            this.txtKolicina.Location = new System.Drawing.Point(495, 100);
            this.txtKolicina.Name = "txtKolicina";
            this.txtKolicina.Size = new System.Drawing.Size(74, 22);
            this.txtKolicina.TabIndex = 0;
            // 
            // txtTermin
            // 
            this.txtTermin.Location = new System.Drawing.Point(319, 100);
            this.txtTermin.Name = "txtTermin";
            this.txtTermin.Size = new System.Drawing.Size(74, 22);
            this.txtTermin.TabIndex = 0;
            // 
            // txtVrstaRN
            // 
            this.txtVrstaRN.Enabled = false;
            this.txtVrstaRN.Location = new System.Drawing.Point(112, 155);
            this.txtVrstaRN.Name = "txtVrstaRN";
            this.txtVrstaRN.Size = new System.Drawing.Size(100, 22);
            this.txtVrstaRN.TabIndex = 0;
            // 
            // txtStatusRN
            // 
            this.txtStatusRN.Enabled = false;
            this.txtStatusRN.Location = new System.Drawing.Point(112, 123);
            this.txtStatusRN.Name = "txtStatusRN";
            this.txtStatusRN.Size = new System.Drawing.Size(100, 22);
            this.txtStatusRN.TabIndex = 0;
            // 
            // txtRN
            // 
            this.txtRN.Enabled = false;
            this.txtRN.Location = new System.Drawing.Point(112, 90);
            this.txtRN.Name = "txtRN";
            this.txtRN.Size = new System.Drawing.Size(100, 22);
            this.txtRN.TabIndex = 0;
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(112, 26);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(100, 22);
            this.txtID.TabIndex = 0;
            // 
            // txtPlanPakovanja
            // 
            this.txtPlanPakovanja.Enabled = false;
            this.txtPlanPakovanja.Location = new System.Drawing.Point(112, 57);
            this.txtPlanPakovanja.Name = "txtPlanPakovanja";
            this.txtPlanPakovanja.Size = new System.Drawing.Size(100, 22);
            this.txtPlanPakovanja.TabIndex = 0;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.SteelBlue;
            this.btnEdit.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnEdit.Location = new System.Drawing.Point(1369, 12);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(93, 31);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Izmeni stavku";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnObrisi
            // 
            this.btnObrisi.BackColor = System.Drawing.Color.OrangeRed;
            this.btnObrisi.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnObrisi.Location = new System.Drawing.Point(1468, 12);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(87, 31);
            this.btnObrisi.TabIndex = 1;
            this.btnObrisi.Text = "Obriši stavku";
            this.btnObrisi.UseVisualStyleBackColor = false;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // btnRealizacija
            // 
            this.btnRealizacija.BackColor = System.Drawing.Color.DarkOrange;
            this.btnRealizacija.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRealizacija.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRealizacija.Location = new System.Drawing.Point(612, 12);
            this.btnRealizacija.Name = "btnRealizacija";
            this.btnRealizacija.Size = new System.Drawing.Size(80, 31);
            this.btnRealizacija.TabIndex = 8;
            this.btnRealizacija.Text = "Realizacija";
            this.btnRealizacija.UseVisualStyleBackColor = false;
            this.btnRealizacija.Click += new System.EventHandler(this.btnRealizacija_Click);
            // 
            // btnKolicine
            // 
            this.btnKolicine.BackColor = System.Drawing.Color.LimeGreen;
            this.btnKolicine.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnKolicine.Location = new System.Drawing.Point(1270, 12);
            this.btnKolicine.Name = "btnKolicine";
            this.btnKolicine.Size = new System.Drawing.Size(93, 31);
            this.btnKolicine.TabIndex = 9;
            this.btnKolicine.Text = "Potvrdi količinu";
            this.btnKolicine.UseVisualStyleBackColor = false;
            this.btnKolicine.Click += new System.EventHandler(this.btnKolicine_Click);
            // 
            // btnIzvestaj
            // 
            this.btnIzvestaj.Location = new System.Drawing.Point(698, 12);
            this.btnIzvestaj.Name = "btnIzvestaj";
            this.btnIzvestaj.Size = new System.Drawing.Size(87, 31);
            this.btnIzvestaj.TabIndex = 10;
            this.btnIzvestaj.Text = "Izveštaj";
            this.btnIzvestaj.UseVisualStyleBackColor = true;
            this.btnIzvestaj.Click += new System.EventHandler(this.btnIzvestaj_Click);
            // 
            // StavkePlanaPakovanjaNovi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.ClientSize = new System.Drawing.Size(1567, 698);
            this.Controls.Add(this.btnIzvestaj);
            this.Controls.Add(this.btnKolicine);
            this.Controls.Add(this.btnRealizacija);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnDodeli);
            this.Controls.Add(this.txtMP);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnMP);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnNazad);
            this.Controls.Add(this.dataGridView1);
            this.Name = "StavkePlanaPakovanjaNovi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StavkePlanaPakovanjaNovi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.StavkePlanaPakovanjaNovi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnNazad;
        private System.Windows.Forms.Button btnPretrazi;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboKolekcija;
        private System.Windows.Forms.ComboBox cboPlan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtMP;
        private System.Windows.Forms.Button btnMP;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnDodeli;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPlanPakovanja;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtVrstaRN;
        private System.Windows.Forms.TextBox txtStatusRN;
        private System.Windows.Forms.TextBox txtRN;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtArtikal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.TextBox txtKolicina;
        private System.Windows.Forms.TextBox txtTermin;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnIzmeni;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.Button btnPretraziRN;
        private System.Windows.Forms.Button btnPretrazipoDatumu;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtRnDo;
        private System.Windows.Forms.TextBox txtRNod;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnRealizacija;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cboVrstaRN;
        private System.Windows.Forms.Button btnKolicine;
        private System.Windows.Forms.Button btnIzvestaj;
    }
}