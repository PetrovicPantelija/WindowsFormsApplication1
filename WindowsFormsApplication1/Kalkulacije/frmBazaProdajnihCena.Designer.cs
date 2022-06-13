
namespace WindowsFormsApplication1.Kalkulacije
{
    partial class frmBazaProdajnihCena
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBazaProdajnihCena));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_mpSifra = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_mpNaziv = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Cena = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txt_Slika = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_Kod = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_Opis = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_Vrste = new System.Windows.Forms.TextBox();
            this.txt_Visina = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_Duzina = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_Sirina = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_Kg = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_Koleto = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_M3 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtKalkulator = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.toolStripButton2,
            this.toolStripButton3});
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
            this.tsDelete.Text = "toolStripButton1";
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
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(97, 24);
            this.toolStripButton1.Text = "Ažuriraj VP cenu";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(96, 24);
            this.toolStripButton2.Text = "Štampaj katalog";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(83, 24);
            this.toolStripButton3.Text = "Prenesi u CSV";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(432, 92);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 201;
            this.label1.Text = "Visina";
            // 
            // txt_ID
            // 
            this.txt_ID.Location = new System.Drawing.Point(12, 60);
            this.txt_ID.Margin = new System.Windows.Forms.Padding(2);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(72, 20);
            this.txt_ID.TabIndex = 202;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(324, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 203;
            this.label2.Text = "MP Sifra";
            // 
            // txt_mpSifra
            // 
            this.txt_mpSifra.Location = new System.Drawing.Point(327, 60);
            this.txt_mpSifra.Margin = new System.Windows.Forms.Padding(2);
            this.txt_mpSifra.Name = "txt_mpSifra";
            this.txt_mpSifra.Size = new System.Drawing.Size(72, 20);
            this.txt_mpSifra.TabIndex = 204;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(405, 57);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 24);
            this.button1.TabIndex = 205;
            this.button1.Text = "Nadji";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(472, 45);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 206;
            this.label3.Text = "MP Naziv";
            // 
            // txt_mpNaziv
            // 
            this.txt_mpNaziv.BackColor = System.Drawing.Color.Yellow;
            this.txt_mpNaziv.Enabled = false;
            this.txt_mpNaziv.Location = new System.Drawing.Point(475, 60);
            this.txt_mpNaziv.Margin = new System.Windows.Forms.Padding(2);
            this.txt_mpNaziv.Name = "txt_mpNaziv";
            this.txt_mpNaziv.Size = new System.Drawing.Size(129, 20);
            this.txt_mpNaziv.TabIndex = 207;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(109, 59);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(204, 21);
            this.comboBox1.TabIndex = 208;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(106, 45);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 201;
            this.label4.Text = "Kalkulator za cene";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(765, 115);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 206;
            this.label5.Text = "Cena";
            // 
            // txt_Cena
            // 
            this.txt_Cena.BackColor = System.Drawing.Color.Yellow;
            this.txt_Cena.Enabled = false;
            this.txt_Cena.Location = new System.Drawing.Point(768, 133);
            this.txt_Cena.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Cena.Name = "txt_Cena";
            this.txt_Cena.Size = new System.Drawing.Size(94, 20);
            this.txt_Cena.TabIndex = 207;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 164);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1005, 353);
            this.dataGridView1.TabIndex = 209;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // txt_Slika
            // 
            this.txt_Slika.Location = new System.Drawing.Point(623, 59);
            this.txt_Slika.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Slika.Name = "txt_Slika";
            this.txt_Slika.Size = new System.Drawing.Size(217, 20);
            this.txt_Slika.TabIndex = 202;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(620, 45);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 201;
            this.label6.Text = "Slika";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(844, 56);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(62, 24);
            this.button2.TabIndex = 205;
            this.button2.Text = "Nadji";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 86);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 203;
            this.label7.Text = "Kod";
            // 
            // txt_Kod
            // 
            this.txt_Kod.Location = new System.Drawing.Point(11, 101);
            this.txt_Kod.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Kod.Name = "txt_Kod";
            this.txt_Kod.Size = new System.Drawing.Size(72, 20);
            this.txt_Kod.TabIndex = 204;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(106, 83);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 201;
            this.label8.Text = "Opis proizvodnje";
            // 
            // txt_Opis
            // 
            this.txt_Opis.Location = new System.Drawing.Point(109, 99);
            this.txt_Opis.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Opis.Multiline = true;
            this.txt_Opis.Name = "txt_Opis";
            this.txt_Opis.Size = new System.Drawing.Size(138, 57);
            this.txt_Opis.TabIndex = 202;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(269, 83);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 201;
            this.label9.Text = "Vrste Materijala";
            // 
            // txt_Vrste
            // 
            this.txt_Vrste.Location = new System.Drawing.Point(272, 99);
            this.txt_Vrste.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Vrste.Multiline = true;
            this.txt_Vrste.Name = "txt_Vrste";
            this.txt_Vrste.Size = new System.Drawing.Size(138, 57);
            this.txt_Vrste.TabIndex = 202;
            // 
            // txt_Visina
            // 
            this.txt_Visina.Location = new System.Drawing.Point(471, 92);
            this.txt_Visina.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Visina.Name = "txt_Visina";
            this.txt_Visina.Size = new System.Drawing.Size(94, 20);
            this.txt_Visina.TabIndex = 202;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(427, 140);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 201;
            this.label10.Text = "Duzina";
            // 
            // txt_Duzina
            // 
            this.txt_Duzina.Location = new System.Drawing.Point(471, 140);
            this.txt_Duzina.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Duzina.Name = "txt_Duzina";
            this.txt_Duzina.Size = new System.Drawing.Size(94, 20);
            this.txt_Duzina.TabIndex = 202;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(434, 115);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 13);
            this.label11.TabIndex = 201;
            this.label11.Text = "Sirina";
            // 
            // txt_Sirina
            // 
            this.txt_Sirina.Location = new System.Drawing.Point(471, 116);
            this.txt_Sirina.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Sirina.Name = "txt_Sirina";
            this.txt_Sirina.Size = new System.Drawing.Size(94, 20);
            this.txt_Sirina.TabIndex = 202;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(582, 92);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 13);
            this.label12.TabIndex = 201;
            this.label12.Text = "KG";
            // 
            // txt_Kg
            // 
            this.txt_Kg.Location = new System.Drawing.Point(623, 92);
            this.txt_Kg.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Kg.Name = "txt_Kg";
            this.txt_Kg.Size = new System.Drawing.Size(94, 20);
            this.txt_Kg.TabIndex = 202;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(582, 116);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 13);
            this.label13.TabIndex = 201;
            this.label13.Text = "Koleto";
            // 
            // txt_Koleto
            // 
            this.txt_Koleto.Location = new System.Drawing.Point(623, 116);
            this.txt_Koleto.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Koleto.Name = "txt_Koleto";
            this.txt_Koleto.Size = new System.Drawing.Size(94, 20);
            this.txt_Koleto.TabIndex = 202;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(582, 140);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(22, 13);
            this.label14.TabIndex = 201;
            this.label14.Text = "M3";
            // 
            // txt_M3
            // 
            this.txt_M3.Location = new System.Drawing.Point(623, 140);
            this.txt_M3.Margin = new System.Windows.Forms.Padding(2);
            this.txt_M3.Name = "txt_M3";
            this.txt_M3.Size = new System.Drawing.Size(94, 20);
            this.txt_M3.TabIndex = 202;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 45);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 13);
            this.label15.TabIndex = 201;
            this.label15.Text = "ID stavke";
            // 
            // txtKalkulator
            // 
            this.txtKalkulator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtKalkulator.Location = new System.Drawing.Point(918, 130);
            this.txtKalkulator.Margin = new System.Windows.Forms.Padding(2);
            this.txtKalkulator.Name = "txtKalkulator";
            this.txtKalkulator.Size = new System.Drawing.Size(99, 20);
            this.txtKalkulator.TabIndex = 211;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(915, 115);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(102, 13);
            this.label16.TabIndex = 210;
            this.label16.Text = "Kalkulator azuriranja";
            // 
            // frmBazaProdajnihCena
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 526);
            this.Controls.Add(this.txtKalkulator);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.txt_Cena);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_mpNaziv);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_Kod);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_mpSifra);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_Vrste);
            this.Controls.Add(this.txt_Opis);
            this.Controls.Add(this.txt_Slika);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txt_M3);
            this.Controls.Add(this.txt_Koleto);
            this.Controls.Add(this.txt_Kg);
            this.Controls.Add(this.txt_Duzina);
            this.Controls.Add(this.txt_Sirina);
            this.Controls.Add(this.txt_Visina);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmBazaProdajnihCena";
            this.Text = "Katalog prodajnih cena";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_mpSifra;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_mpNaziv;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_Cena;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_Slika;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_Kod;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_Opis;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_Vrste;
        private System.Windows.Forms.TextBox txt_Visina;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_Duzina;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_Sirina;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_Kg;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_Koleto;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_M3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.TextBox txtKalkulator;
        private System.Windows.Forms.Label label16;
    }
}