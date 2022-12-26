namespace WindowsFormsApplication1
{
    partial class ObradaIsporuke
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObradaIsporuke));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboVozila = new System.Windows.Forms.ComboBox();
            this.cboDatumUtovara = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Tezina = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCeoNalog = new System.Windows.Forms.TextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboUtovar = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPakovanja = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.cboPaleta = new System.Windows.Forms.ComboBox();
            this.cboVozaci = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button3 = new FontAwesome.Sharp.IconButton();
            this.btnPozovi = new FontAwesome.Sharp.IconButton();
            this.button2 = new FontAwesome.Sharp.IconButton();
            this.button1 = new FontAwesome.Sharp.IconButton();
            this.button4 = new FontAwesome.Sharp.IconButton();
            this.btnPostaviPaletu = new FontAwesome.Sharp.IconButton();
            this.button5 = new FontAwesome.Sharp.IconButton();
            this.button6 = new FontAwesome.Sharp.IconButton();
            this.button7 = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 23);
            this.label1.TabIndex = 16;
            this.label1.Text = "Vozilo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 23);
            this.label2.TabIndex = 17;
            this.label2.Text = "Datum Utovara:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 23);
            this.label3.TabIndex = 23;
            this.label3.Text = "Br palete:";
            // 
            // cboVozila
            // 
            this.cboVozila.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboVozila.FormattingEnabled = true;
            this.cboVozila.Location = new System.Drawing.Point(31, 110);
            this.cboVozila.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboVozila.Name = "cboVozila";
            this.cboVozila.Size = new System.Drawing.Size(215, 28);
            this.cboVozila.TabIndex = 24;
            this.cboVozila.SelectedIndexChanged += new System.EventHandler(this.cboVozila_SelectedIndexChanged);
            // 
            // cboDatumUtovara
            // 
            this.cboDatumUtovara.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboDatumUtovara.FormattingEnabled = true;
            this.cboDatumUtovara.Location = new System.Drawing.Point(31, 48);
            this.cboDatumUtovara.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboDatumUtovara.Name = "cboDatumUtovara";
            this.cboDatumUtovara.Size = new System.Drawing.Size(215, 28);
            this.cboDatumUtovara.TabIndex = 25;
            this.cboDatumUtovara.SelectedIndexChanged += new System.EventHandler(this.cboDatumUtovara_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(653, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 23);
            this.label4.TabIndex = 26;
            this.label4.Text = "Težina sumarno:";
            // 
            // Tezina
            // 
            this.Tezina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(200)))), ((int)(((byte)(0)))));
            this.Tezina.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Tezina.Location = new System.Drawing.Point(653, 44);
            this.Tezina.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Tezina.Name = "Tezina";
            this.Tezina.Size = new System.Drawing.Size(215, 34);
            this.Tezina.TabIndex = 27;
            this.Tezina.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(653, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 23);
            this.label5.TabIndex = 28;
            this.label5.Text = "Težina ceo nalog:";
            // 
            // txtCeoNalog
            // 
            this.txtCeoNalog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(200)))), ((int)(((byte)(0)))));
            this.txtCeoNalog.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtCeoNalog.Location = new System.Drawing.Point(653, 106);
            this.txtCeoNalog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCeoNalog.Name = "txtCeoNalog";
            this.txtCeoNalog.Size = new System.Drawing.Size(215, 34);
            this.txtCeoNalog.TabIndex = 29;
            this.txtCeoNalog.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCeoNalog.TextChanged += new System.EventHandler(this.txtCeoNalog_TextChanged);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(158)))), ((int)(((byte)(192)))));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(1124, 49);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(301, 220);
            this.dataGridView2.TabIndex = 30;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(1119, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(162, 23);
            this.label6.TabIndex = 31;
            this.label6.Text = "Težina po paletama:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(31, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 23);
            this.label7.TabIndex = 32;
            this.label7.Text = "Broj utovara:";
            // 
            // cboUtovar
            // 
            this.cboUtovar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboUtovar.FormattingEnabled = true;
            this.cboUtovar.Location = new System.Drawing.Point(31, 171);
            this.cboUtovar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboUtovar.Name = "cboUtovar";
            this.cboUtovar.Size = new System.Drawing.Size(215, 28);
            this.cboUtovar.TabIndex = 33;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(653, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(145, 23);
            this.label8.TabIndex = 36;
            this.label8.Text = "Težina Pakovanja:";
            // 
            // txtPakovanja
            // 
            this.txtPakovanja.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(200)))), ((int)(((byte)(0)))));
            this.txtPakovanja.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPakovanja.Location = new System.Drawing.Point(653, 167);
            this.txtPakovanja.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPakovanja.Name = "txtPakovanja";
            this.txtPakovanja.Size = new System.Drawing.Size(215, 34);
            this.txtPakovanja.TabIndex = 37;
            this.txtPakovanja.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tabControl1.Location = new System.Drawing.Point(15, 270);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1423, 363);
            this.tabControl1.TabIndex = 39;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(1415, 333);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Paketi";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(158)))), ((int)(((byte)(192)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(5, 5);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1401, 321);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView3);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(1415, 333);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Reklamacije";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView3.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(158)))), ((int)(((byte)(192)))));
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(3, 5);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.Size = new System.Drawing.Size(1403, 321);
            this.dataGridView3.TabIndex = 3;
            this.dataGridView3.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // cboPaleta
            // 
            this.cboPaleta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboPaleta.FormattingEnabled = true;
            this.cboPaleta.Location = new System.Drawing.Point(31, 233);
            this.cboPaleta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboPaleta.Name = "cboPaleta";
            this.cboPaleta.Size = new System.Drawing.Size(215, 28);
            this.cboPaleta.TabIndex = 41;
            // 
            // cboVozaci
            // 
            this.cboVozaci.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboVozaci.FormattingEnabled = true;
            this.cboVozaci.Location = new System.Drawing.Point(653, 230);
            this.cboVozaci.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboVozaci.Name = "cboVozaci";
            this.cboVozaci.Size = new System.Drawing.Size(215, 28);
            this.cboVozaci.TabIndex = 45;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(653, 207);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 23);
            this.label9.TabIndex = 44;
            this.label9.Text = "Vozači:";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.button3.IconChar = FontAwesome.Sharp.IconChar.IdBadge;
            this.button3.IconColor = System.Drawing.Color.Black;
            this.button3.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.button3.IconSize = 23;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(260, 48);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.button3.Size = new System.Drawing.Size(100, 31);
            this.button3.TabIndex = 152;
            this.button3.Text = "DI";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnPozovi
            // 
            this.btnPozovi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.btnPozovi.FlatAppearance.BorderSize = 0;
            this.btnPozovi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPozovi.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnPozovi.IconChar = FontAwesome.Sharp.IconChar.List;
            this.btnPozovi.IconColor = System.Drawing.Color.Black;
            this.btnPozovi.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPozovi.IconSize = 23;
            this.btnPozovi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPozovi.Location = new System.Drawing.Point(260, 109);
            this.btnPozovi.Margin = new System.Windows.Forms.Padding(4);
            this.btnPozovi.Name = "btnPozovi";
            this.btnPozovi.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnPozovi.Size = new System.Drawing.Size(208, 31);
            this.btnPozovi.TabIndex = 153;
            this.btnPozovi.Text = "Pozovi stavke";
            this.btnPozovi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPozovi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPozovi.UseVisualStyleBackColor = false;
            this.btnPozovi.Click += new System.EventHandler(this.btnPozovi_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.button2.IconChar = FontAwesome.Sharp.IconChar.ArrowAltCircleDown;
            this.button2.IconColor = System.Drawing.Color.Black;
            this.button2.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.button2.IconSize = 23;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(260, 171);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.button2.Size = new System.Drawing.Size(100, 31);
            this.button2.TabIndex = 155;
            this.button2.Text = "Poz";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.button1.IconChar = FontAwesome.Sharp.IconChar.ArrowAltCircleDown;
            this.button1.IconColor = System.Drawing.Color.Black;
            this.button1.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.button1.IconSize = 23;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(368, 171);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.button1.Size = new System.Drawing.Size(129, 31);
            this.button1.TabIndex = 154;
            this.button1.Text = "Pozovi";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.button4.IconChar = FontAwesome.Sharp.IconChar.ListAlt;
            this.button4.IconColor = System.Drawing.Color.Black;
            this.button4.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.button4.IconSize = 23;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(397, 233);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.button4.Size = new System.Drawing.Size(223, 31);
            this.button4.TabIndex = 157;
            this.button4.Text = "Postavi paletu rekl";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnPostaviPaletu
            // 
            this.btnPostaviPaletu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.btnPostaviPaletu.FlatAppearance.BorderSize = 0;
            this.btnPostaviPaletu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPostaviPaletu.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnPostaviPaletu.IconChar = FontAwesome.Sharp.IconChar.ListAlt;
            this.btnPostaviPaletu.IconColor = System.Drawing.Color.Black;
            this.btnPostaviPaletu.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPostaviPaletu.IconSize = 23;
            this.btnPostaviPaletu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPostaviPaletu.Location = new System.Drawing.Point(260, 233);
            this.btnPostaviPaletu.Margin = new System.Windows.Forms.Padding(4);
            this.btnPostaviPaletu.Name = "btnPostaviPaletu";
            this.btnPostaviPaletu.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnPostaviPaletu.Size = new System.Drawing.Size(129, 31);
            this.btnPostaviPaletu.TabIndex = 156;
            this.btnPostaviPaletu.Text = "Postavi";
            this.btnPostaviPaletu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPostaviPaletu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPostaviPaletu.UseVisualStyleBackColor = false;
            this.btnPostaviPaletu.Click += new System.EventHandler(this.button1_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.button5.IconChar = FontAwesome.Sharp.IconChar.FileExport;
            this.button5.IconColor = System.Drawing.Color.Black;
            this.button5.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.button5.IconSize = 23;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(883, 108);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.button5.Size = new System.Drawing.Size(235, 31);
            this.button5.TabIndex = 160;
            this.button5.Text = "Vrati paletu u Webs";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.button6.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            this.button6.IconColor = System.Drawing.Color.Black;
            this.button6.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.button6.IconSize = 23;
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(883, 170);
            this.button6.Margin = new System.Windows.Forms.Padding(4);
            this.button6.Name = "button6";
            this.button6.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.button6.Size = new System.Drawing.Size(235, 31);
            this.button6.TabIndex = 159;
            this.button6.Text = "Izbriši reklamaciju";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.button7.IconChar = FontAwesome.Sharp.IconChar.Truck;
            this.button7.IconColor = System.Drawing.Color.Black;
            this.button7.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.button7.IconSize = 23;
            this.button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.Location = new System.Drawing.Point(883, 230);
            this.button7.Margin = new System.Windows.Forms.Padding(4);
            this.button7.Name = "button7";
            this.button7.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.button7.Size = new System.Drawing.Size(235, 31);
            this.button7.TabIndex = 158;
            this.button7.Text = "Postavi Vozača";
            this.button7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // ObradaIsporuke
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1440, 661);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnPostaviPaletu);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnPozovi);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.cboVozaci);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cboPaleta);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtPakovanja);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboUtovar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.txtCeoNalog);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Tezina);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboDatumUtovara);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboVozila);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ObradaIsporuke";
            this.Text = "Dodela paleta";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ObradaIsporuke_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboVozila;
        private System.Windows.Forms.ComboBox cboDatumUtovara;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Tezina;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCeoNalog;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboUtovar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPakovanja;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cboPaleta;
        private System.Windows.Forms.ComboBox cboVozaci;
        private System.Windows.Forms.Label label9;
        private FontAwesome.Sharp.IconButton button3;
        private FontAwesome.Sharp.IconButton btnPozovi;
        private FontAwesome.Sharp.IconButton button2;
        private FontAwesome.Sharp.IconButton button1;
        private FontAwesome.Sharp.IconButton button4;
        private FontAwesome.Sharp.IconButton btnPostaviPaletu;
        private FontAwesome.Sharp.IconButton button5;
        private FontAwesome.Sharp.IconButton button6;
        private FontAwesome.Sharp.IconButton button7;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView3;
    }
}