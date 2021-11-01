namespace WindowsFormsApplication1
{
    partial class frmPrebacivanjeMeđPrenosaRN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrebacivanjeMeđPrenosaRN));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.txtKolicinaRN = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.txtKoeficijent = new System.Windows.Forms.NumericUpDown();
            this.button5 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBrRn = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnOsvezi = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.btnUbaci = new System.Windows.Forms.Button();
            this.cboSkladisteU = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboSkladiste = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.txtIzdaje = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBrojMSP = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.cboLokacijaU = new System.Windows.Forms.ComboBox();
            this.dataGridView5 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNapomena = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button6 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBN = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.button4 = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.button1 = new System.Windows.Forms.Button();
            this.MP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MPKod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SkladisteIz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SkladisteU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KOL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OsnovnoU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LokacijaU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Napomena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LokacijaIz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtKolicinaRN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKoeficijent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtKolicinaRN);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtKoeficijent);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtBrRn);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.btnOsvezi);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1268, 193);
            this.panel1.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(4, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 16);
            this.label10.TabIndex = 260;
            this.label10.Text = "Količina RN:";
            // 
            // txtKolicinaRN
            // 
            this.txtKolicinaRN.DecimalPlaces = 2;
            this.txtKolicinaRN.Enabled = false;
            this.txtKolicinaRN.Location = new System.Drawing.Point(101, 100);
            this.txtKolicinaRN.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtKolicinaRN.Name = "txtKolicinaRN";
            this.txtKolicinaRN.Size = new System.Drawing.Size(82, 20);
            this.txtKolicinaRN.TabIndex = 259;
            this.txtKolicinaRN.Value = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(4, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 16);
            this.label7.TabIndex = 258;
            this.label7.Text = "Količina:";
            // 
            // txtKoeficijent
            // 
            this.txtKoeficijent.DecimalPlaces = 2;
            this.txtKoeficijent.Location = new System.Drawing.Point(101, 126);
            this.txtKoeficijent.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtKoeficijent.Name = "txtKoeficijent";
            this.txtKoeficijent.Size = new System.Drawing.Size(82, 20);
            this.txtKoeficijent.TabIndex = 257;
            this.txtKoeficijent.Value = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            this.txtKoeficijent.Leave += new System.EventHandler(this.txtKoeficijent_Leave);
            // 
            // button5
            // 
            this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(129, 68);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(54, 25);
            this.button5.TabIndex = 256;
            this.button5.Text = "Nađi";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(4, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 16);
            this.label6.TabIndex = 255;
            this.label6.Text = "Radni Nalog broj:";
            // 
            // txtBrRn
            // 
            this.txtBrRn.Location = new System.Drawing.Point(7, 71);
            this.txtBrRn.Name = "txtBrRn";
            this.txtBrRn.Size = new System.Drawing.Size(116, 20);
            this.txtBrRn.TabIndex = 254;
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(3, 152);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(180, 25);
            this.button2.TabIndex = 239;
            this.button2.Text = "Povuci stavke RN sel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnOsvezi
            // 
            this.btnOsvezi.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOsvezi.BackgroundImage")));
            this.btnOsvezi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOsvezi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnOsvezi.ForeColor = System.Drawing.Color.White;
            this.btnOsvezi.Location = new System.Drawing.Point(3, 3);
            this.btnOsvezi.Name = "btnOsvezi";
            this.btnOsvezi.Size = new System.Drawing.Size(180, 25);
            this.btnOsvezi.TabIndex = 237;
            this.btnOsvezi.Text = "Prikaži LA,KZ RN";
            this.btnOsvezi.UseVisualStyleBackColor = true;
            this.btnOsvezi.Click += new System.EventHandler(this.btnOsvezi_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(189, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1066, 187);
            this.dataGridView1.TabIndex = 236;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.Controls.Add(this.dataGridView2);
            this.panel2.Location = new System.Drawing.Point(9, 220);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(492, 479);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(4, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(485, 473);
            this.dataGridView2.TabIndex = 237;
            this.dataGridView2.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.dataGridView3);
            this.panel3.Location = new System.Drawing.Point(720, 220);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(621, 479);
            this.panel3.TabIndex = 2;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView3.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MP,
            this.MPKod,
            this.JM,
            this.SkladisteIz,
            this.SkladisteU,
            this.KOL,
            this.RN,
            this.OsnovnoU,
            this.LokacijaU,
            this.Napomena,
            this.LokacijaIz});
            this.dataGridView3.Location = new System.Drawing.Point(14, 9);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(594, 467);
            this.dataGridView3.TabIndex = 238;
            // 
            // btnUbaci
            // 
            this.btnUbaci.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUbaci.BackgroundImage")));
            this.btnUbaci.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUbaci.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnUbaci.ForeColor = System.Drawing.Color.White;
            this.btnUbaci.Location = new System.Drawing.Point(511, 520);
            this.btnUbaci.Name = "btnUbaci";
            this.btnUbaci.Size = new System.Drawing.Size(182, 45);
            this.btnUbaci.TabIndex = 247;
            this.btnUbaci.Text = ">>>>";
            this.btnUbaci.UseVisualStyleBackColor = true;
            this.btnUbaci.Click += new System.EventHandler(this.btnUbaci_Click);
            // 
            // cboSkladisteU
            // 
            this.cboSkladisteU.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboSkladisteU.FormattingEnabled = true;
            this.cboSkladisteU.Location = new System.Drawing.Point(510, 405);
            this.cboSkladisteU.Name = "cboSkladisteU";
            this.cboSkladisteU.Size = new System.Drawing.Size(182, 24);
            this.cboSkladisteU.TabIndex = 246;
            this.cboSkladisteU.Leave += new System.EventHandler(this.cboSkladisteU_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(507, 386);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 245;
            this.label3.Text = "Skladiste u:";
            // 
            // cboSkladiste
            // 
            this.cboSkladiste.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboSkladiste.ForeColor = System.Drawing.Color.Black;
            this.cboSkladiste.FormattingEnabled = true;
            this.cboSkladiste.Location = new System.Drawing.Point(670, 196);
            this.cboSkladiste.Name = "cboSkladiste";
            this.cboSkladiste.Size = new System.Drawing.Size(44, 24);
            this.cboSkladiste.TabIndex = 244;
            this.cboSkladiste.Visible = false;
            this.cboSkladiste.SelectedIndexChanged += new System.EventHandler(this.cboSkladiste_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(579, 199);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 243;
            this.label1.Text = "Skladište iz:";
            this.label1.Visible = false;
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(510, 571);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(182, 45);
            this.button3.TabIndex = 248;
            this.button3.Text = "Formiraj međuskladišni prenos";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtIzdaje
            // 
            this.txtIzdaje.Enabled = false;
            this.txtIzdaje.Location = new System.Drawing.Point(517, 680);
            this.txtIzdaje.Name = "txtIzdaje";
            this.txtIzdaje.Size = new System.Drawing.Size(178, 20);
            this.txtIzdaje.TabIndex = 249;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(518, 662);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 16);
            this.label2.TabIndex = 250;
            this.label2.Text = "Izdaje:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(518, 619);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 16);
            this.label4.TabIndex = 252;
            this.label4.Text = "Broj MSP:";
            // 
            // txtBrojMSP
            // 
            this.txtBrojMSP.Location = new System.Drawing.Point(517, 638);
            this.txtBrojMSP.Name = "txtBrojMSP";
            this.txtBrojMSP.Size = new System.Drawing.Size(178, 20);
            this.txtBrojMSP.TabIndex = 251;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1355, 731);
            this.tabControl1.TabIndex = 253;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.cboLokacijaU);
            this.tabPage1.Controls.Add(this.dataGridView5);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.txtNapomena);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.txtBrojMSP);
            this.tabPage1.Controls.Add(this.cboSkladiste);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtIzdaje);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.btnUbaci);
            this.tabPage1.Controls.Add(this.cboSkladisteU);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1347, 705);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Formiranje";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(507, 432);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 16);
            this.label9.TabIndex = 255;
            this.label9.Text = "Lokacija u:";
            // 
            // cboLokacijaU
            // 
            this.cboLokacijaU.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboLokacijaU.FormattingEnabled = true;
            this.cboLokacijaU.Location = new System.Drawing.Point(510, 451);
            this.cboLokacijaU.Name = "cboLokacijaU";
            this.cboLokacijaU.Size = new System.Drawing.Size(182, 24);
            this.cboLokacijaU.TabIndex = 256;
            // 
            // dataGridView5
            // 
            this.dataGridView5.AllowUserToAddRows = false;
            this.dataGridView5.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView5.Location = new System.Drawing.Point(507, 223);
            this.dataGridView5.Name = "dataGridView5";
            this.dataGridView5.Size = new System.Drawing.Size(207, 160);
            this.dataGridView5.TabIndex = 238;
            this.dataGridView5.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView5_CellLeave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(508, 477);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 16);
            this.label5.TabIndex = 254;
            this.label5.Text = "Napomena:";
            // 
            // txtNapomena
            // 
            this.txtNapomena.Location = new System.Drawing.Point(510, 496);
            this.txtNapomena.Name = "txtNapomena";
            this.txtNapomena.Size = new System.Drawing.Size(178, 20);
            this.txtNapomena.TabIndex = 253;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.txtBN);
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.reportViewer1);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1347, 705);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Štampa nalepnica";
            // 
            // button6
            // 
            this.button6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button6.BackgroundImage")));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(379, 6);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(177, 25);
            this.button6.TabIndex = 259;
            this.button6.Text = "Promeni broj nalepnica";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(204, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 16);
            this.label8.TabIndex = 258;
            this.label8.Text = "Broj nalepnica:";
            // 
            // txtBN
            // 
            this.txtBN.Location = new System.Drawing.Point(315, 9);
            this.txtBN.Name = "txtBN";
            this.txtBN.Size = new System.Drawing.Size(52, 20);
            this.txtBN.TabIndex = 257;
            this.txtBN.Text = "1";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.dataGridView4);
            this.panel4.Location = new System.Drawing.Point(6, 37);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1248, 181);
            this.panel4.TabIndex = 243;
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView4.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(3, 3);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.Size = new System.Drawing.Size(1228, 175);
            this.dataGridView4.TabIndex = 238;
            // 
            // button4
            // 
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(567, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(180, 25);
            this.button4.TabIndex = 242;
            this.button4.Text = "Prikaži nalepnice";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "DataSetReklamacija";
            reportDataSource1.Value = null;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "WindowsFormsApplication1.rptReklNal.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(6, 235);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1268, 366);
            this.reportViewer1.TabIndex = 241;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(180, 25);
            this.button1.TabIndex = 240;
            this.button1.Text = "Napravi nalepnice";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MP
            // 
            this.MP.FillWeight = 50F;
            this.MP.HeaderText = "MP";
            this.MP.Name = "MP";
            this.MP.Width = 50;
            // 
            // MPKod
            // 
            this.MPKod.HeaderText = "Kod";
            this.MPKod.Name = "MPKod";
            // 
            // JM
            // 
            this.JM.FillWeight = 20F;
            this.JM.HeaderText = "JM";
            this.JM.Name = "JM";
            this.JM.Width = 20;
            // 
            // SkladisteIz
            // 
            this.SkladisteIz.FillWeight = 40F;
            this.SkladisteIz.HeaderText = "SkladisteIz";
            this.SkladisteIz.Name = "SkladisteIz";
            this.SkladisteIz.Width = 40;
            // 
            // SkladisteU
            // 
            this.SkladisteU.FillWeight = 40F;
            this.SkladisteU.HeaderText = "LokacijaIz";
            this.SkladisteU.Name = "SkladisteU";
            this.SkladisteU.Width = 40;
            // 
            // KOL
            // 
            this.KOL.FillWeight = 40F;
            this.KOL.HeaderText = "Kolicina";
            this.KOL.Name = "KOL";
            this.KOL.Width = 40;
            // 
            // RN
            // 
            this.RN.FillWeight = 1F;
            this.RN.HeaderText = "RN";
            this.RN.Name = "RN";
            this.RN.Visible = false;
            this.RN.Width = 5;
            // 
            // OsnovnoU
            // 
            this.OsnovnoU.FillWeight = 40F;
            this.OsnovnoU.HeaderText = "SkladisteU";
            this.OsnovnoU.Name = "OsnovnoU";
            this.OsnovnoU.Width = 40;
            // 
            // LokacijaU
            // 
            this.LokacijaU.FillWeight = 40F;
            this.LokacijaU.HeaderText = "LokacijaU";
            this.LokacijaU.Name = "LokacijaU";
            this.LokacijaU.Width = 40;
            // 
            // Napomena
            // 
            this.Napomena.FillWeight = 80F;
            this.Napomena.HeaderText = "Napomena";
            this.Napomena.Name = "Napomena";
            this.Napomena.Width = 80;
            // 
            // LokacijaIz
            // 
            this.LokacijaIz.FillWeight = 80F;
            this.LokacijaIz.HeaderText = "LokacijaUOpis";
            this.LokacijaIz.Name = "LokacijaIz";
            this.LokacijaIz.Width = 80;
            // 
            // frmPrebacivanjeMeđPrenosaRN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmPrebacivanjeMeđPrenosaRN";
            this.Text = "Formiranje Međuskladisnog prenosa iz RN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPrebacivanjeMeđPrenosaRN_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtKolicinaRN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKoeficijent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnOsvezi;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Button btnUbaci;
        private System.Windows.Forms.ComboBox cboSkladisteU;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboSkladiste;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtIzdaje;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBrojMSP;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNapomena;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBrRn;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBN;
        private System.Windows.Forms.DataGridView dataGridView5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown txtKoeficijent;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboLokacijaU;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown txtKolicinaRN;
        private System.Windows.Forms.DataGridViewTextBoxColumn MP;
        private System.Windows.Forms.DataGridViewTextBoxColumn MPKod;
        private System.Windows.Forms.DataGridViewTextBoxColumn JM;
        private System.Windows.Forms.DataGridViewTextBoxColumn SkladisteIz;
        private System.Windows.Forms.DataGridViewTextBoxColumn SkladisteU;
        private System.Windows.Forms.DataGridViewTextBoxColumn KOL;
        private System.Windows.Forms.DataGridViewTextBoxColumn RN;
        private System.Windows.Forms.DataGridViewTextBoxColumn OsnovnoU;
        private System.Windows.Forms.DataGridViewTextBoxColumn LokacijaU;
        private System.Windows.Forms.DataGridViewTextBoxColumn Napomena;
        private System.Windows.Forms.DataGridViewTextBoxColumn LokacijaIz;
    }
}