namespace WindowsFormsApplication1
{
    partial class frmPrebacivanjeMedjuskladisniPrenos
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrebacivanjeMedjuskladisniPrenos));
            this.cboSkladisteIzbor = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.PrSifSklad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrLokacija = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrSifMp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MpStaraSif = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MpNaziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MpDoNaziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MPSifEnoteMere1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboPozicija = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboSkladiste = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txtKolicina = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new FontAwesome.Sharp.IconButton();
            this.btnOsvezi = new FontAwesome.Sharp.IconButton();
            this.btnUbaci = new FontAwesome.Sharp.IconButton();
            this.cboPozicijaIz = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBrojMSP = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIzdaje = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBN = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.SuspendLayout();
            // 
            // cboSkladisteIzbor
            // 
            this.cboSkladisteIzbor.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboSkladisteIzbor.FormattingEnabled = true;
            this.cboSkladisteIzbor.Location = new System.Drawing.Point(19, 42);
            this.cboSkladisteIzbor.Name = "cboSkladisteIzbor";
            this.cboSkladisteIzbor.Size = new System.Drawing.Size(225, 24);
            this.cboSkladisteIzbor.TabIndex = 233;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(19, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 17);
            this.label5.TabIndex = 232;
            this.label5.Text = "Skladište:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 168);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(549, 525);
            this.dataGridView1.TabIndex = 235;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
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
            this.dataGridView2.Location = new System.Drawing.Point(716, 76);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.Size = new System.Drawing.Size(618, 617);
            this.dataGridView2.TabIndex = 243;
            // 
            // PrSifSklad
            // 
            this.PrSifSklad.HeaderText = "PrSifSklad";
            this.PrSifSklad.MinimumWidth = 6;
            this.PrSifSklad.Name = "PrSifSklad";
            this.PrSifSklad.Width = 125;
            // 
            // PrLokacija
            // 
            this.PrLokacija.HeaderText = "PrLokacija";
            this.PrLokacija.MinimumWidth = 6;
            this.PrLokacija.Name = "PrLokacija";
            this.PrLokacija.Width = 125;
            // 
            // PrSifMp
            // 
            this.PrSifMp.HeaderText = "PrSifMp";
            this.PrSifMp.MinimumWidth = 6;
            this.PrSifMp.Name = "PrSifMp";
            this.PrSifMp.Width = 125;
            // 
            // MpStaraSif
            // 
            this.MpStaraSif.HeaderText = "MpStaraSif";
            this.MpStaraSif.MinimumWidth = 6;
            this.MpStaraSif.Name = "MpStaraSif";
            this.MpStaraSif.Width = 125;
            // 
            // MpNaziv
            // 
            this.MpNaziv.HeaderText = "MpNaziv";
            this.MpNaziv.MinimumWidth = 6;
            this.MpNaziv.Name = "MpNaziv";
            this.MpNaziv.Width = 125;
            // 
            // MpDoNaziv
            // 
            this.MpDoNaziv.HeaderText = "MpDoNaziv";
            this.MpDoNaziv.MinimumWidth = 6;
            this.MpDoNaziv.Name = "MpDoNaziv";
            this.MpDoNaziv.Width = 125;
            // 
            // Kolicina
            // 
            this.Kolicina.HeaderText = "Kolicina";
            this.Kolicina.MinimumWidth = 6;
            this.Kolicina.Name = "Kolicina";
            this.Kolicina.Width = 125;
            // 
            // MPSifEnoteMere1
            // 
            this.MPSifEnoteMere1.HeaderText = "MPSifEnoteMere1";
            this.MPSifEnoteMere1.MinimumWidth = 6;
            this.MPSifEnoteMere1.Name = "MPSifEnoteMere1";
            this.MPSifEnoteMere1.Width = 125;
            // 
            // cboPozicija
            // 
            this.cboPozicija.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboPozicija.FormattingEnabled = true;
            this.cboPozicija.Location = new System.Drawing.Point(323, 93);
            this.cboPozicija.Name = "cboPozicija";
            this.cboPozicija.Size = new System.Drawing.Size(239, 24);
            this.cboPozicija.TabIndex = 239;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(324, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 238;
            this.label3.Text = "Lokacija u:";
            // 
            // cboSkladiste
            // 
            this.cboSkladiste.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboSkladiste.ForeColor = System.Drawing.Color.Black;
            this.cboSkladiste.FormattingEnabled = true;
            this.cboSkladiste.Location = new System.Drawing.Point(323, 39);
            this.cboSkladiste.Name = "cboSkladiste";
            this.cboSkladiste.Size = new System.Drawing.Size(239, 24);
            this.cboSkladiste.TabIndex = 237;
            this.cboSkladiste.SelectedIndexChanged += new System.EventHandler(this.cboSkladiste_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(324, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 236;
            this.label1.Text = "Skladište u:";
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.progressBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.progressBar1.Location = new System.Drawing.Point(717, 51);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(390, 19);
            this.progressBar1.TabIndex = 245;
            // 
            // txtKolicina
            // 
            this.txtKolicina.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtKolicina.Location = new System.Drawing.Point(323, 140);
            this.txtKolicina.Name = "txtKolicina";
            this.txtKolicina.Size = new System.Drawing.Size(106, 22);
            this.txtKolicina.TabIndex = 246;
            this.txtKolicina.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(324, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 17);
            this.label2.TabIndex = 247;
            this.label2.Text = "Kolicina za prenos:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.button1.IconChar = FontAwesome.Sharp.IconChar.ExchangeAlt;
            this.button1.IconColor = System.Drawing.Color.Black;
            this.button1.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.button1.IconSize = 22;
            this.button1.Location = new System.Drawing.Point(716, 21);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.button1.Size = new System.Drawing.Size(390, 25);
            this.button1.TabIndex = 284;
            this.button1.Text = "Formiraj međuskladišni prenos";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnOsvezi
            // 
            this.btnOsvezi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.btnOsvezi.FlatAppearance.BorderSize = 0;
            this.btnOsvezi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOsvezi.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnOsvezi.IconChar = FontAwesome.Sharp.IconChar.Redo;
            this.btnOsvezi.IconColor = System.Drawing.Color.Black;
            this.btnOsvezi.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnOsvezi.IconSize = 23;
            this.btnOsvezi.Location = new System.Drawing.Point(248, 86);
            this.btnOsvezi.Name = "btnOsvezi";
            this.btnOsvezi.Size = new System.Drawing.Size(59, 25);
            this.btnOsvezi.TabIndex = 287;
            this.btnOsvezi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOsvezi.UseVisualStyleBackColor = false;
            this.btnOsvezi.Click += new System.EventHandler(this.btnOsvezi_Click);
            // 
            // btnUbaci
            // 
            this.btnUbaci.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.btnUbaci.FlatAppearance.BorderSize = 0;
            this.btnUbaci.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUbaci.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnUbaci.IconChar = FontAwesome.Sharp.IconChar.Random;
            this.btnUbaci.IconColor = System.Drawing.Color.Black;
            this.btnUbaci.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnUbaci.IconSize = 90;
            this.btnUbaci.Location = new System.Drawing.Point(577, 168);
            this.btnUbaci.Name = "btnUbaci";
            this.btnUbaci.Size = new System.Drawing.Size(121, 459);
            this.btnUbaci.TabIndex = 288;
            this.btnUbaci.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUbaci.UseVisualStyleBackColor = false;
            this.btnUbaci.Click += new System.EventHandler(this.btnUbaci_Click);
            // 
            // cboPozicijaIz
            // 
            this.cboPozicijaIz.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboPozicijaIz.FormattingEnabled = true;
            this.cboPozicijaIz.Location = new System.Drawing.Point(18, 87);
            this.cboPozicijaIz.Name = "cboPozicijaIz";
            this.cboPozicijaIz.Size = new System.Drawing.Size(224, 24);
            this.cboPozicijaIz.TabIndex = 290;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(19, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 289;
            this.label4.Text = "Lokacija u:";
            // 
            // txtBrojMSP
            // 
            this.txtBrojMSP.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtBrojMSP.Location = new System.Drawing.Point(1125, 32);
            this.txtBrojMSP.Name = "txtBrojMSP";
            this.txtBrojMSP.Size = new System.Drawing.Size(106, 22);
            this.txtBrojMSP.TabIndex = 291;
            this.txtBrojMSP.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(1122, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 17);
            this.label6.TabIndex = 292;
            this.label6.Text = "MSP:";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1357, 725);
            this.tabControl1.TabIndex = 293;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.txtIzdaje);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.txtBrojMSP);
            this.tabPage1.Controls.Add(this.cboSkladisteIzbor);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.cboPozicijaIz);
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.dataGridView2);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.cboSkladiste);
            this.tabPage1.Controls.Add(this.btnUbaci);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.btnOsvezi);
            this.tabPage1.Controls.Add(this.cboPozicija);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtKolicina);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1349, 699);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Formiranje";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(1234, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 17);
            this.label7.TabIndex = 294;
            this.label7.Text = "Korisnik:";
            // 
            // txtIzdaje
            // 
            this.txtIzdaje.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtIzdaje.Location = new System.Drawing.Point(1237, 32);
            this.txtIzdaje.Name = "txtIzdaje";
            this.txtIzdaje.Size = new System.Drawing.Size(106, 22);
            this.txtIzdaje.TabIndex = 293;
            this.txtIzdaje.Text = " ";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.reportViewer1);
            this.tabPage2.Controls.Add(this.dataGridView4);
            this.tabPage2.Controls.Add(this.button7);
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.txtBN);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1349, 699);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Štampanje";
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
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "WindowsFormsApplication1.rptReklNal.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(9, 288);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1327, 394);
            this.reportViewer1.TabIndex = 268;
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView4.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(9, 59);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.Size = new System.Drawing.Size(1327, 239);
            this.dataGridView4.TabIndex = 267;
            // 
            // button7
            // 
            this.button7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button7.BackgroundImage")));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Location = new System.Drawing.Point(290, 16);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(254, 25);
            this.button7.TabIndex = 266;
            this.button7.Text = "Napravi nalepnice RN";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click_1);
            // 
            // button6
            // 
            this.button6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button6.BackgroundImage")));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(861, 16);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(177, 25);
            this.button6.TabIndex = 265;
            this.button6.Text = "Promeni broj nalepnica";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(686, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 16);
            this.label8.TabIndex = 264;
            this.label8.Text = "Broj nalepnica:";
            // 
            // txtBN
            // 
            this.txtBN.Location = new System.Drawing.Point(797, 19);
            this.txtBN.Name = "txtBN";
            this.txtBN.Size = new System.Drawing.Size(52, 20);
            this.txtBN.TabIndex = 263;
            this.txtBN.Text = "1";
            // 
            // button4
            // 
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(1049, 16);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(180, 25);
            this.button4.TabIndex = 262;
            this.button4.Text = "Prikaži nalepnice";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(9, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(254, 25);
            this.button2.TabIndex = 261;
            this.button2.Text = "Napravi nalepnice ARTIKAL";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmPrebacivanjeMedjuskladisniPrenos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1397, 749);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPrebacivanjeMedjuskladisniPrenos";
            this.Text = "Međuskladišni prenos prebacivanje";
            this.Load += new System.EventHandler(this.frmPrebacivanjeMedjuskladisniPrenos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cboSkladisteIzbor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ComboBox cboPozicija;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboSkladiste;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txtKolicina;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrSifSklad;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrLokacija;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrSifMp;
        private System.Windows.Forms.DataGridViewTextBoxColumn MpStaraSif;
        private System.Windows.Forms.DataGridViewTextBoxColumn MpNaziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn MpDoNaziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn MPSifEnoteMere1;
        private FontAwesome.Sharp.IconButton button1;
        private FontAwesome.Sharp.IconButton btnOsvezi;
        private FontAwesome.Sharp.IconButton btnUbaci;
        private System.Windows.Forms.ComboBox cboPozicijaIz;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBrojMSP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBN;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView4;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtIzdaje;
    }
}