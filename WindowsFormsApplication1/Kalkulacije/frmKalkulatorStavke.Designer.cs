
namespace WindowsFormsApplication1.Kalkulacije
{
    partial class frmKalkulatorStavke
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKalkulatorStavke));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_mpSifra = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.combo_Zaglavlje = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Trosak = new System.Windows.Forms.TextBox();
            this.txt_Avans = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Provizija = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_Transport = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_VpCena = new System.Windows.Forms.TextBox();
            this.txt_Rabat = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_Marza = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_Akcija = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_MpCena = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_Mp2 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_Rabat = new System.Windows.Forms.Button();
            this.btn_Akcija = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.tsSave,
            this.tsDelete,
            this.toolStripSeparator1,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1028, 27);
            this.toolStrip1.TabIndex = 200;
            this.toolStrip1.Text = "Osveži";
            // 
            // tsNew
            // 
            this.tsNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNew.Image = ((System.Drawing.Image)(resources.GetObject("tsNew.Image")));
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(24, 24);
            this.tsNew.Text = "Novi";
            this.tsNew.Click += new System.EventHandler(this.tsNew_Click);
            // 
            // tsSave
            // 
            this.tsSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSave.Image = ((System.Drawing.Image)(resources.GetObject("tsSave.Image")));
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(24, 24);
            this.tsSave.Text = "tsSave";
            this.tsSave.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsDelete.Image")));
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(24, 24);
            this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(125, 24);
            this.toolStripButton1.Text = "Azuriraj nove troskove";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(99, 24);
            this.toolStripButton2.Text = "Proglasi konacan";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // txt_ID
            // 
            this.txt_ID.Location = new System.Drawing.Point(36, 32);
            this.txt_ID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(61, 20);
            this.txt_ID.TabIndex = 201;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 202;
            this.label1.Text = "ID";
            // 
            // txt_mpSifra
            // 
            this.txt_mpSifra.Location = new System.Drawing.Point(12, 96);
            this.txt_mpSifra.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_mpSifra.Name = "txt_mpSifra";
            this.txt_mpSifra.Size = new System.Drawing.Size(60, 20);
            this.txt_mpSifra.TabIndex = 201;
            this.txt_mpSifra.Leave += new System.EventHandler(this.txt_mpSifra_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 67);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 202;
            this.label2.Text = "MpSifra";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(97, 67);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 202;
            this.label3.Text = "Zaglavlje";
            // 
            // combo_Zaglavlje
            // 
            this.combo_Zaglavlje.FormattingEnabled = true;
            this.combo_Zaglavlje.Location = new System.Drawing.Point(88, 94);
            this.combo_Zaglavlje.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.combo_Zaglavlje.Name = "combo_Zaglavlje";
            this.combo_Zaglavlje.Size = new System.Drawing.Size(164, 21);
            this.combo_Zaglavlje.TabIndex = 203;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(266, 67);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 202;
            this.label4.Text = "Trosak proizvodnje";
            // 
            // txt_Trosak
            // 
            this.txt_Trosak.BackColor = System.Drawing.Color.Yellow;
            this.txt_Trosak.Location = new System.Drawing.Point(266, 96);
            this.txt_Trosak.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_Trosak.Name = "txt_Trosak";
            this.txt_Trosak.Size = new System.Drawing.Size(98, 20);
            this.txt_Trosak.TabIndex = 201;
            // 
            // txt_Avans
            // 
            this.txt_Avans.Location = new System.Drawing.Point(9, 150);
            this.txt_Avans.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_Avans.Name = "txt_Avans";
            this.txt_Avans.Size = new System.Drawing.Size(98, 20);
            this.txt_Avans.TabIndex = 201;
            this.txt_Avans.Leave += new System.EventHandler(this.txt_Avans_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 134);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 202;
            this.label5.Text = "Avans";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(134, 134);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 202;
            this.label6.Text = "Provizija";
            // 
            // txt_Provizija
            // 
            this.txt_Provizija.Location = new System.Drawing.Point(136, 150);
            this.txt_Provizija.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_Provizija.Name = "txt_Provizija";
            this.txt_Provizija.Size = new System.Drawing.Size(98, 20);
            this.txt_Provizija.TabIndex = 201;
            this.txt_Provizija.Leave += new System.EventHandler(this.txt_Provizija_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(274, 134);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 202;
            this.label7.Text = "Transport";
            // 
            // txt_Transport
            // 
            this.txt_Transport.Location = new System.Drawing.Point(267, 150);
            this.txt_Transport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_Transport.Name = "txt_Transport";
            this.txt_Transport.Size = new System.Drawing.Size(98, 20);
            this.txt_Transport.TabIndex = 201;
            this.txt_Transport.Leave += new System.EventHandler(this.txt_Transport_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(56, 232);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 202;
            this.label8.Text = "VP CENA";
            // 
            // txt_VpCena
            // 
            this.txt_VpCena.BackColor = System.Drawing.Color.Yellow;
            this.txt_VpCena.Location = new System.Drawing.Point(9, 248);
            this.txt_VpCena.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_VpCena.Name = "txt_VpCena";
            this.txt_VpCena.Size = new System.Drawing.Size(98, 20);
            this.txt_VpCena.TabIndex = 201;
            // 
            // txt_Rabat
            // 
            this.txt_Rabat.Location = new System.Drawing.Point(9, 202);
            this.txt_Rabat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_Rabat.Name = "txt_Rabat";
            this.txt_Rabat.Size = new System.Drawing.Size(98, 20);
            this.txt_Rabat.TabIndex = 201;
            this.txt_Rabat.Leave += new System.EventHandler(this.txt_Rabat_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 185);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 13);
            this.label9.TabIndex = 202;
            this.label9.Text = "Rabat";
            // 
            // txt_Marza
            // 
            this.txt_Marza.Location = new System.Drawing.Point(136, 202);
            this.txt_Marza.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_Marza.Name = "txt_Marza";
            this.txt_Marza.Size = new System.Drawing.Size(98, 20);
            this.txt_Marza.TabIndex = 201;
            this.txt_Marza.Leave += new System.EventHandler(this.txt_Marza_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(134, 185);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 202;
            this.label10.Text = "Marža";
            // 
            // txt_Akcija
            // 
            this.txt_Akcija.Location = new System.Drawing.Point(266, 202);
            this.txt_Akcija.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_Akcija.Name = "txt_Akcija";
            this.txt_Akcija.Size = new System.Drawing.Size(98, 20);
            this.txt_Akcija.TabIndex = 201;
            this.txt_Akcija.Leave += new System.EventHandler(this.txt_Akcija_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(274, 185);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 202;
            this.label11.Text = "Akcija";
            // 
            // txt_MpCena
            // 
            this.txt_MpCena.BackColor = System.Drawing.Color.Yellow;
            this.txt_MpCena.Location = new System.Drawing.Point(136, 248);
            this.txt_MpCena.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_MpCena.Name = "txt_MpCena";
            this.txt_MpCena.Size = new System.Drawing.Size(98, 20);
            this.txt_MpCena.TabIndex = 201;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(182, 232);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 13);
            this.label12.TabIndex = 202;
            this.label12.Text = "MP CENA";
            // 
            // txt_Mp2
            // 
            this.txt_Mp2.BackColor = System.Drawing.Color.Yellow;
            this.txt_Mp2.Location = new System.Drawing.Point(266, 248);
            this.txt_Mp2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_Mp2.Name = "txt_Mp2";
            this.txt_Mp2.Size = new System.Drawing.Size(98, 20);
            this.txt_Mp2.TabIndex = 201;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(272, 232);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(94, 13);
            this.label13.TabIndex = 202;
            this.label13.Text = "MP CENA Veznog";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(412, 29);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(605, 578);
            this.dataGridView1.TabIndex = 204;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // btn_Rabat
            // 
            this.btn_Rabat.Location = new System.Drawing.Point(9, 284);
            this.btn_Rabat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Rabat.Name = "btn_Rabat";
            this.btn_Rabat.Size = new System.Drawing.Size(114, 37);
            this.btn_Rabat.TabIndex = 205;
            this.btn_Rabat.Text = "Primeni rabat";
            this.btn_Rabat.UseVisualStyleBackColor = true;
            this.btn_Rabat.Click += new System.EventHandler(this.btn_Rabat_Click);
            // 
            // btn_Akcija
            // 
            this.btn_Akcija.Location = new System.Drawing.Point(136, 284);
            this.btn_Akcija.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Akcija.Name = "btn_Akcija";
            this.btn_Akcija.Size = new System.Drawing.Size(114, 37);
            this.btn_Akcija.TabIndex = 205;
            this.btn_Akcija.Text = "Primeni Akciju";
            this.btn_Akcija.UseVisualStyleBackColor = true;
            this.btn_Akcija.Click += new System.EventHandler(this.btn_Akcija_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(266, 284);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 37);
            this.button1.TabIndex = 205;
            this.button1.Text = "Primeni troskovni cenovnik";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(5, 18);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(138, 263);
            this.dataGridView2.TabIndex = 206;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(147, 18);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.Size = new System.Drawing.Size(175, 263);
            this.dataGridView3.TabIndex = 207;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(326, 18);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(58, 263);
            this.button2.TabIndex = 208;
            this.button2.Text = "Prikaži po programu i kolekciji";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView2);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.dataGridView3);
            this.groupBox1.Location = new System.Drawing.Point(9, 326);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(389, 286);
            this.groupBox1.TabIndex = 209;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Po programu i kolekciji";
            // 
            // frmKalkulatorStavke
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 624);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_Akcija);
            this.Controls.Add(this.btn_Rabat);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.combo_Zaglavlje);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_mpSifra);
            this.Controls.Add(this.txt_Trosak);
            this.Controls.Add(this.txt_Mp2);
            this.Controls.Add(this.txt_MpCena);
            this.Controls.Add(this.txt_VpCena);
            this.Controls.Add(this.txt_Transport);
            this.Controls.Add(this.txt_Provizija);
            this.Controls.Add(this.txt_Akcija);
            this.Controls.Add(this.txt_Marza);
            this.Controls.Add(this.txt_Rabat);
            this.Controls.Add(this.txt_Avans);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmKalkulatorStavke";
            this.Text = "frmKalkulatorStavke";
            this.Load += new System.EventHandler(this.frmKalkulatorStavke_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_mpSifra;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox combo_Zaglavlje;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Trosak;
        private System.Windows.Forms.TextBox txt_Avans;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_Provizija;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_Transport;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_VpCena;
        private System.Windows.Forms.TextBox txt_Rabat;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_Marza;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_Akcija;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_MpCena;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_Mp2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_Rabat;
        private System.Windows.Forms.Button btn_Akcija;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}