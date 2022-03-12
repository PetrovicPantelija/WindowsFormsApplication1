
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
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1450, 27);
            this.toolStrip1.TabIndex = 200;
            this.toolStrip1.Text = "Osveži";
            // 
            // tsNew
            // 
            this.tsNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNew.Image = ((System.Drawing.Image)(resources.GetObject("tsNew.Image")));
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(29, 24);
            this.tsNew.Text = "Novi";
            this.tsNew.Click += new System.EventHandler(this.tsNew_Click);
            // 
            // tsSave
            // 
            this.tsSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSave.Image = ((System.Drawing.Image)(resources.GetObject("tsSave.Image")));
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(29, 24);
            this.tsSave.Text = "tsSave";
            this.tsSave.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsDelete.Image")));
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(29, 24);
            this.tsDelete.Text = "toolStripButton1";
            this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(566, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 201;
            this.label1.Text = "Visina";
            // 
            // txt_ID
            // 
            this.txt_ID.Location = new System.Drawing.Point(12, 76);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(95, 22);
            this.txt_ID.TabIndex = 202;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(449, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 203;
            this.label2.Text = "MP Sifra";
            // 
            // txt_mpSifra
            // 
            this.txt_mpSifra.Location = new System.Drawing.Point(452, 74);
            this.txt_mpSifra.Name = "txt_mpSifra";
            this.txt_mpSifra.Size = new System.Drawing.Size(95, 22);
            this.txt_mpSifra.TabIndex = 204;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(553, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 29);
            this.button1.TabIndex = 205;
            this.button1.Text = "Nadji";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(660, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 17);
            this.label3.TabIndex = 206;
            this.label3.Text = "MP Naziv";
            // 
            // txt_mpNaziv
            // 
            this.txt_mpNaziv.BackColor = System.Drawing.Color.Yellow;
            this.txt_mpNaziv.Enabled = false;
            this.txt_mpNaziv.Location = new System.Drawing.Point(663, 76);
            this.txt_mpNaziv.Name = "txt_mpNaziv";
            this.txt_mpNaziv.Size = new System.Drawing.Size(171, 22);
            this.txt_mpNaziv.TabIndex = 207;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(145, 74);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(271, 24);
            this.comboBox1.TabIndex = 208;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(142, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 17);
            this.label4.TabIndex = 201;
            this.label4.Text = "Kalkulator";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(865, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 17);
            this.label5.TabIndex = 206;
            this.label5.Text = "Cena";
            // 
            // txt_Cena
            // 
            this.txt_Cena.BackColor = System.Drawing.Color.Yellow;
            this.txt_Cena.Enabled = false;
            this.txt_Cena.Location = new System.Drawing.Point(868, 76);
            this.txt_Cena.Name = "txt_Cena";
            this.txt_Cena.Size = new System.Drawing.Size(124, 22);
            this.txt_Cena.TabIndex = 207;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(8, 210);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1403, 426);
            this.dataGridView1.TabIndex = 209;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // txt_Slika
            // 
            this.txt_Slika.Location = new System.Drawing.Point(1023, 76);
            this.txt_Slika.Name = "txt_Slika";
            this.txt_Slika.Size = new System.Drawing.Size(288, 22);
            this.txt_Slika.TabIndex = 202;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1020, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 17);
            this.label6.TabIndex = 201;
            this.label6.Text = "Slika";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1328, 76);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 29);
            this.button2.TabIndex = 205;
            this.button2.Text = "Nadji";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 17);
            this.label7.TabIndex = 203;
            this.label7.Text = "Kod";
            // 
            // txt_Kod
            // 
            this.txt_Kod.Location = new System.Drawing.Point(12, 142);
            this.txt_Kod.Name = "txt_Kod";
            this.txt_Kod.Size = new System.Drawing.Size(95, 22);
            this.txt_Kod.TabIndex = 204;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(142, 122);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 17);
            this.label8.TabIndex = 201;
            this.label8.Text = "Opis proizvodnje";
            // 
            // txt_Opis
            // 
            this.txt_Opis.Location = new System.Drawing.Point(145, 142);
            this.txt_Opis.Name = "txt_Opis";
            this.txt_Opis.Size = new System.Drawing.Size(183, 22);
            this.txt_Opis.TabIndex = 202;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(359, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 17);
            this.label9.TabIndex = 201;
            this.label9.Text = "Vrste Materijala";
            // 
            // txt_Vrste
            // 
            this.txt_Vrste.Location = new System.Drawing.Point(362, 142);
            this.txt_Vrste.Name = "txt_Vrste";
            this.txt_Vrste.Size = new System.Drawing.Size(183, 22);
            this.txt_Vrste.TabIndex = 202;
            // 
            // txt_Visina
            // 
            this.txt_Visina.Location = new System.Drawing.Point(566, 142);
            this.txt_Visina.Name = "txt_Visina";
            this.txt_Visina.Size = new System.Drawing.Size(124, 22);
            this.txt_Visina.TabIndex = 202;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(848, 122);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 17);
            this.label10.TabIndex = 201;
            this.label10.Text = "Duzina";
            // 
            // txt_Duzina
            // 
            this.txt_Duzina.Location = new System.Drawing.Point(851, 142);
            this.txt_Duzina.Name = "txt_Duzina";
            this.txt_Duzina.Size = new System.Drawing.Size(124, 22);
            this.txt_Duzina.TabIndex = 202;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(711, 122);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 17);
            this.label11.TabIndex = 201;
            this.label11.Text = "Sirina";
            // 
            // txt_Sirina
            // 
            this.txt_Sirina.Location = new System.Drawing.Point(714, 142);
            this.txt_Sirina.Name = "txt_Sirina";
            this.txt_Sirina.Size = new System.Drawing.Size(124, 22);
            this.txt_Sirina.TabIndex = 202;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(991, 122);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(28, 17);
            this.label12.TabIndex = 201;
            this.label12.Text = "KG";
            // 
            // txt_Kg
            // 
            this.txt_Kg.Location = new System.Drawing.Point(994, 142);
            this.txt_Kg.Name = "txt_Kg";
            this.txt_Kg.Size = new System.Drawing.Size(124, 22);
            this.txt_Kg.TabIndex = 202;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1142, 122);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 17);
            this.label13.TabIndex = 201;
            this.label13.Text = "Koleto";
            // 
            // txt_Koleto
            // 
            this.txt_Koleto.Location = new System.Drawing.Point(1145, 142);
            this.txt_Koleto.Name = "txt_Koleto";
            this.txt_Koleto.Size = new System.Drawing.Size(124, 22);
            this.txt_Koleto.TabIndex = 202;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1284, 122);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(27, 17);
            this.label14.TabIndex = 201;
            this.label14.Text = "M3";
            // 
            // txt_M3
            // 
            this.txt_M3.Location = new System.Drawing.Point(1287, 142);
            this.txt_M3.Name = "txt_M3";
            this.txt_M3.Size = new System.Drawing.Size(124, 22);
            this.txt_M3.TabIndex = 202;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 56);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(21, 17);
            this.label15.TabIndex = 201;
            this.label15.Text = "ID";
            // 
            // frmBazaProdajnihCena
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1450, 648);
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
            this.Name = "frmBazaProdajnihCena";
            this.Text = "frmBazaProdajnihCena";
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
    }
}