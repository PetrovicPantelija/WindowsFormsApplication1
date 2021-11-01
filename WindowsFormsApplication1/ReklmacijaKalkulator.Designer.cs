namespace WindowsFormsApplication1
{
    partial class ReklmacijaKalkulator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReklmacijaKalkulator));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.label2 = new System.Windows.Forms.Label();
            this.cboMaticni = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboVarijanta = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnOsvezi = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRSR = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRINO = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtROT = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.txtCenaD = new System.Windows.Forms.TextBox();
            this.txtCenaM = new System.Windows.Forms.TextBox();
            this.txtFiksni = new System.Windows.Forms.TextBox();
            this.txtCenaDD = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtProizvodna = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRSRStavka = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtRINOStavka = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtROTStavka = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtStavkaSifra = new System.Windows.Forms.TextBox();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.chkPoNazivu = new System.Windows.Forms.CheckBox();
            this.txtKod = new System.Windows.Forms.TextBox();
            this.txtStavkaKod = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.button13 = new System.Windows.Forms.Button();
            this.txtBrZapisa = new System.Windows.Forms.TextBox();
            this.txtKalk = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 243;
            this.label2.Text = "MP naziv:";
            // 
            // cboMaticni
            // 
            this.cboMaticni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboMaticni.FormattingEnabled = true;
            this.cboMaticni.Location = new System.Drawing.Point(83, 15);
            this.cboMaticni.Name = "cboMaticni";
            this.cboMaticni.Size = new System.Drawing.Size(191, 24);
            this.cboMaticni.TabIndex = 242;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 245;
            this.label1.Text = "Varijanta:";
            // 
            // cboVarijanta
            // 
            this.cboVarijanta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboVarijanta.FormattingEnabled = true;
            this.cboVarijanta.Location = new System.Drawing.Point(82, 70);
            this.cboVarijanta.Name = "cboVarijanta";
            this.cboVarijanta.Size = new System.Drawing.Size(191, 24);
            this.cboVarijanta.TabIndex = 244;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1067, 309);
            this.dataGridView1.TabIndex = 248;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // btnOsvezi
            // 
            this.btnOsvezi.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOsvezi.BackgroundImage")));
            this.btnOsvezi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOsvezi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnOsvezi.ForeColor = System.Drawing.Color.White;
            this.btnOsvezi.Location = new System.Drawing.Point(340, 14);
            this.btnOsvezi.Name = "btnOsvezi";
            this.btnOsvezi.Size = new System.Drawing.Size(38, 25);
            this.btnOsvezi.TabIndex = 249;
            this.btnOsvezi.Text = "?";
            this.btnOsvezi.UseVisualStyleBackColor = true;
            this.btnOsvezi.Click += new System.EventHandler(this.btnOsvezi_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(277, 70);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(38, 25);
            this.button1.TabIndex = 250;
            this.button1.Text = "?";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(413, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 16);
            this.label8.TabIndex = 255;
            this.label8.Text = "RSR:";
            // 
            // txtRSR
            // 
            this.txtRSR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtRSR.Location = new System.Drawing.Point(500, 78);
            this.txtRSR.Name = "txtRSR";
            this.txtRSR.Size = new System.Drawing.Size(110, 20);
            this.txtRSR.TabIndex = 256;
            this.txtRSR.Text = "0";
            this.txtRSR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(413, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 16);
            this.label7.TabIndex = 253;
            this.label7.Text = "RINO:";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // txtRINO
            // 
            this.txtRINO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtRINO.Location = new System.Drawing.Point(500, 47);
            this.txtRINO.Name = "txtRINO";
            this.txtRINO.Size = new System.Drawing.Size(110, 20);
            this.txtRINO.TabIndex = 254;
            this.txtRINO.Text = "0";
            this.txtRINO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(413, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 251;
            this.label6.Text = "ROT:";
            // 
            // txtROT
            // 
            this.txtROT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtROT.Location = new System.Drawing.Point(500, 13);
            this.txtROT.Name = "txtROT";
            this.txtROT.Size = new System.Drawing.Size(110, 20);
            this.txtROT.TabIndex = 252;
            this.txtROT.Text = "0";
            this.txtROT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(694, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(259, 25);
            this.button2.TabIndex = 257;
            this.button2.Text = "Postavi ne prikazivanje MP";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(694, 41);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(259, 25);
            this.button3.TabIndex = 258;
            this.button3.Text = "Postavi ne prikazivanje MP uvek";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(694, 72);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(259, 25);
            this.button5.TabIndex = 260;
            this.button5.Text = "Postavi ne prikazivanje Grane uvek";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(321, 70);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(38, 25);
            this.button4.TabIndex = 261;
            this.button4.Text = "S";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button6
            // 
            this.button6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button6.BackgroundImage")));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(795, 103);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(158, 25);
            this.button6.TabIndex = 262;
            this.button6.Text = "Ažurirj cene";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // txtCenaD
            // 
            this.txtCenaD.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtCenaD.Location = new System.Drawing.Point(18, 135);
            this.txtCenaD.Name = "txtCenaD";
            this.txtCenaD.Size = new System.Drawing.Size(86, 20);
            this.txtCenaD.TabIndex = 263;
            // 
            // txtCenaM
            // 
            this.txtCenaM.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtCenaM.Location = new System.Drawing.Point(110, 135);
            this.txtCenaM.Name = "txtCenaM";
            this.txtCenaM.Size = new System.Drawing.Size(79, 20);
            this.txtCenaM.TabIndex = 264;
            // 
            // txtFiksni
            // 
            this.txtFiksni.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtFiksni.Location = new System.Drawing.Point(195, 135);
            this.txtFiksni.Name = "txtFiksni";
            this.txtFiksni.Size = new System.Drawing.Size(76, 20);
            this.txtFiksni.TabIndex = 265;
            // 
            // txtCenaDD
            // 
            this.txtCenaDD.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtCenaDD.Location = new System.Drawing.Point(277, 135);
            this.txtCenaDD.Name = "txtCenaDD";
            this.txtCenaDD.Size = new System.Drawing.Size(79, 20);
            this.txtCenaDD.TabIndex = 266;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(15, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 16);
            this.label3.TabIndex = 267;
            this.label3.Text = "CenaD:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(110, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 16);
            this.label4.TabIndex = 268;
            this.label4.Text = "CenaM:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(192, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 16);
            this.label5.TabIndex = 269;
            this.label5.Text = "Fiksni:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(277, 116);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 16);
            this.label9.TabIndex = 270;
            this.label9.Text = "CenaDD:";
            // 
            // txtProizvodna
            // 
            this.txtProizvodna.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtProizvodna.Location = new System.Drawing.Point(500, 135);
            this.txtProizvodna.Name = "txtProizvodna";
            this.txtProizvodna.Size = new System.Drawing.Size(110, 20);
            this.txtProizvodna.TabIndex = 271;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(413, 135);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 16);
            this.label10.TabIndex = 272;
            this.label10.Text = "Proizvodna:";
            // 
            // button7
            // 
            this.button7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button7.BackgroundImage")));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Location = new System.Drawing.Point(616, 11);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(56, 25);
            this.button7.TabIndex = 273;
            this.button7.Text = "UC";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button8.BackgroundImage")));
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Location = new System.Drawing.Point(616, 43);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(56, 25);
            this.button8.TabIndex = 274;
            this.button8.Text = "UC";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button9.BackgroundImage")));
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Location = new System.Drawing.Point(616, 75);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(56, 25);
            this.button9.TabIndex = 275;
            this.button9.Text = "UC";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtStavkaKod);
            this.groupBox1.Controls.Add(this.button12);
            this.groupBox1.Controls.Add(this.button11);
            this.groupBox1.Controls.Add(this.button10);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtStavkaSifra);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtRSRStavka);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtRINOStavka);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtROTStavka);
            this.groupBox1.Location = new System.Drawing.Point(22, 165);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1049, 92);
            this.groupBox1.TabIndex = 276;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stavka";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(633, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 16);
            this.label11.TabIndex = 261;
            this.label11.Text = "RSR:";
            // 
            // txtRSRStavka
            // 
            this.txtRSRStavka.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtRSRStavka.Location = new System.Drawing.Point(679, 28);
            this.txtRSRStavka.Name = "txtRSRStavka";
            this.txtRSRStavka.Size = new System.Drawing.Size(110, 20);
            this.txtRSRStavka.TabIndex = 262;
            this.txtRSRStavka.Text = "0";
            this.txtRSRStavka.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(419, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 16);
            this.label12.TabIndex = 259;
            this.label12.Text = "RINO:";
            // 
            // txtRINOStavka
            // 
            this.txtRINOStavka.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtRINOStavka.Location = new System.Drawing.Point(469, 28);
            this.txtRINOStavka.Name = "txtRINOStavka";
            this.txtRINOStavka.Size = new System.Drawing.Size(110, 20);
            this.txtRINOStavka.TabIndex = 260;
            this.txtRINOStavka.Text = "0";
            this.txtRINOStavka.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(190, 29);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 16);
            this.label13.TabIndex = 257;
            this.label13.Text = "ROT:";
            // 
            // txtROTStavka
            // 
            this.txtROTStavka.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtROTStavka.ForeColor = System.Drawing.Color.Black;
            this.txtROTStavka.Location = new System.Drawing.Point(236, 29);
            this.txtROTStavka.Name = "txtROTStavka";
            this.txtROTStavka.Size = new System.Drawing.Size(110, 20);
            this.txtROTStavka.TabIndex = 258;
            this.txtROTStavka.Text = "0";
            this.txtROTStavka.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(13, 27);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(58, 16);
            this.label14.TabIndex = 272;
            this.label14.Text = "ŠifraMP:";
            // 
            // txtStavkaSifra
            // 
            this.txtStavkaSifra.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtStavkaSifra.Location = new System.Drawing.Point(95, 28);
            this.txtStavkaSifra.Name = "txtStavkaSifra";
            this.txtStavkaSifra.Size = new System.Drawing.Size(79, 20);
            this.txtStavkaSifra.TabIndex = 271;
            // 
            // button10
            // 
            this.button10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button10.BackgroundImage")));
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button10.ForeColor = System.Drawing.Color.White;
            this.button10.Location = new System.Drawing.Point(352, 26);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(38, 25);
            this.button10.TabIndex = 274;
            this.button10.Text = "UC";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button11.BackgroundImage")));
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button11.ForeColor = System.Drawing.Color.White;
            this.button11.Location = new System.Drawing.Point(585, 26);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(38, 25);
            this.button11.TabIndex = 275;
            this.button11.Text = "UC";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button12.BackgroundImage")));
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button12.ForeColor = System.Drawing.Color.White;
            this.button12.Location = new System.Drawing.Point(795, 26);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(38, 25);
            this.button12.TabIndex = 276;
            this.button12.Text = "UC";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // chkPoNazivu
            // 
            this.chkPoNazivu.AutoSize = true;
            this.chkPoNazivu.Location = new System.Drawing.Point(281, 18);
            this.chkPoNazivu.Name = "chkPoNazivu";
            this.chkPoNazivu.Size = new System.Drawing.Size(53, 17);
            this.chkPoNazivu.TabIndex = 277;
            this.chkPoNazivu.Text = "Naziv";
            this.chkPoNazivu.UseVisualStyleBackColor = true;
            this.chkPoNazivu.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txtKod
            // 
            this.txtKod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtKod.Location = new System.Drawing.Point(83, 41);
            this.txtKod.Name = "txtKod";
            this.txtKod.Size = new System.Drawing.Size(188, 20);
            this.txtKod.TabIndex = 278;
            // 
            // txtStavkaKod
            // 
            this.txtStavkaKod.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtStavkaKod.Location = new System.Drawing.Point(16, 48);
            this.txtStavkaKod.Name = "txtStavkaKod";
            this.txtStavkaKod.Size = new System.Drawing.Size(158, 20);
            this.txtStavkaKod.TabIndex = 279;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(15, 41);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(57, 16);
            this.label15.TabIndex = 279;
            this.label15.Text = "MP kod:";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(18, 263);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1087, 347);
            this.tabControl1.TabIndex = 280;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1079, 321);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Stavke";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.reportViewer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(869, 321);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Izvestaj";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "DataSetReklamacija";
            reportDataSource1.Value = null;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "WindowsFormsApplication1.rptReklNal2.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(9, 6);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(854, 309);
            this.reportViewer1.TabIndex = 1;
            // 
            // button13
            // 
            this.button13.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button13.BackgroundImage")));
            this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button13.ForeColor = System.Drawing.Color.White;
            this.button13.Location = new System.Drawing.Point(694, 134);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(259, 25);
            this.button13.TabIndex = 281;
            this.button13.Text = "Štampaj";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // txtBrZapisa
            // 
            this.txtBrZapisa.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtBrZapisa.Location = new System.Drawing.Point(680, 106);
            this.txtBrZapisa.Name = "txtBrZapisa";
            this.txtBrZapisa.Size = new System.Drawing.Size(68, 20);
            this.txtBrZapisa.TabIndex = 282;
            this.txtBrZapisa.Text = "1";
            // 
            // txtKalk
            // 
            this.txtKalk.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtKalk.Location = new System.Drawing.Point(754, 106);
            this.txtKalk.Name = "txtKalk";
            this.txtKalk.Size = new System.Drawing.Size(35, 20);
            this.txtKalk.TabIndex = 283;
            this.txtKalk.Text = "0";
            // 
            // ReklmacijaKalkulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1117, 622);
            this.Controls.Add(this.txtKalk);
            this.Controls.Add(this.txtBrZapisa);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtKod);
            this.Controls.Add(this.chkPoNazivu);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtProizvodna);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCenaDD);
            this.Controls.Add(this.txtFiksni);
            this.Controls.Add(this.txtCenaM);
            this.Controls.Add(this.txtCenaD);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtRSR);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtRINO);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtROT);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOsvezi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboVarijanta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboMaticni);
            this.Name = "ReklmacijaKalkulator";
            this.Text = "Kalkulator reklamacije";
            this.Load += new System.EventHandler(this.ReklmacijaKalkulator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboMaticni;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboVarijanta;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnOsvezi;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtRSR;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRINO;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtROT;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox txtCenaD;
        private System.Windows.Forms.TextBox txtCenaM;
        private System.Windows.Forms.TextBox txtFiksni;
        private System.Windows.Forms.TextBox txtCenaDD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtProizvodna;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtStavkaSifra;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRSRStavka;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtRINOStavka;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtROTStavka;
        private System.Windows.Forms.CheckBox chkPoNazivu;
        private System.Windows.Forms.TextBox txtKod;
        private System.Windows.Forms.TextBox txtStavkaKod;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.TextBox txtBrZapisa;
        private System.Windows.Forms.TextBox txtKalk;
    }
}