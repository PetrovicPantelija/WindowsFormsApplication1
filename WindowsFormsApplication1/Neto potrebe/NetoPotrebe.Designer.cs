namespace WindowsFormsApplication1.Neto_potrebe
{
    partial class NetoPotrebe
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
            this.label1 = new System.Windows.Forms.Label();
            this.cboPlan = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.cboGrupa = new System.Windows.Forms.ComboBox();
            this.cboPodgrupa = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.txtNovaKolicina = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.cboMaterijal1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboMaterijal2 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboDebljina = new System.Windows.Forms.ComboBox();
            this.cboTehnologija1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboTehnologija2 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkGrupa = new System.Windows.Forms.CheckBox();
            this.chkMaterijal1 = new System.Windows.Forms.CheckBox();
            this.chkMaterijal2 = new System.Windows.Forms.CheckBox();
            this.chkDebljina = new System.Windows.Forms.CheckBox();
            this.chkTehnologija1 = new System.Windows.Forms.CheckBox();
            this.chkTehnologija2 = new System.Windows.Forms.CheckBox();
            this.button6 = new System.Windows.Forms.Button();
            this.txtPodPlan = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.chkRazvrstani = new System.Windows.Forms.CheckBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.chkNerazvrstani = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Plan";
            // 
            // cboPlan
            // 
            this.cboPlan.FormattingEnabled = true;
            this.cboPlan.Location = new System.Drawing.Point(12, 25);
            this.cboPlan.Name = "cboPlan";
            this.cboPlan.Size = new System.Drawing.Size(278, 21);
            this.cboPlan.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 230);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1299, 349);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(365, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Grupa";
            // 
            // cboGrupa
            // 
            this.cboGrupa.FormattingEnabled = true;
            this.cboGrupa.Location = new System.Drawing.Point(368, 25);
            this.cboGrupa.Name = "cboGrupa";
            this.cboGrupa.Size = new System.Drawing.Size(281, 21);
            this.cboGrupa.TabIndex = 4;
            this.cboGrupa.SelectionChangeCommitted += new System.EventHandler(this.cboGrupa_SelectionChangeCommitted);
            // 
            // cboPodgrupa
            // 
            this.cboPodgrupa.FormattingEnabled = true;
            this.cboPodgrupa.Location = new System.Drawing.Point(770, 25);
            this.cboPodgrupa.Name = "cboPodgrupa";
            this.cboPodgrupa.Size = new System.Drawing.Size(259, 21);
            this.cboPodgrupa.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(767, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Podgrupa";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(11, 57);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 33);
            this.button2.TabIndex = 6;
            this.button2.Text = "Prikaži plan";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(532, 57);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 33);
            this.button1.TabIndex = 7;
            this.button1.Text = "Plan + Grupa";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.button3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.button3.Location = new System.Drawing.Point(837, 57);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(192, 33);
            this.button3.TabIndex = 8;
            this.button3.Text = "Plan + Grupa + Podgrupa";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.button4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.button4.Location = new System.Drawing.Point(184, 57);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(106, 33);
            this.button4.TabIndex = 9;
            this.button4.Text = "Obriši NP";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtNovaKolicina
            // 
            this.txtNovaKolicina.Location = new System.Drawing.Point(1135, 25);
            this.txtNovaKolicina.Name = "txtNovaKolicina";
            this.txtNovaKolicina.Size = new System.Drawing.Size(139, 20);
            this.txtNovaKolicina.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(1132, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Nova kolicina";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.button5.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.button5.Location = new System.Drawing.Point(1133, 57);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(139, 33);
            this.button5.TabIndex = 12;
            this.button5.Text = "Promeni kolicinu";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // cboMaterijal1
            // 
            this.cboMaterijal1.FormattingEnabled = true;
            this.cboMaterijal1.Location = new System.Drawing.Point(17, 135);
            this.cboMaterijal1.Name = "cboMaterijal1";
            this.cboMaterijal1.Size = new System.Drawing.Size(119, 21);
            this.cboMaterijal1.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(14, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Materijal 1";
            // 
            // cboMaterijal2
            // 
            this.cboMaterijal2.FormattingEnabled = true;
            this.cboMaterijal2.Location = new System.Drawing.Point(157, 135);
            this.cboMaterijal2.Name = "cboMaterijal2";
            this.cboMaterijal2.Size = new System.Drawing.Size(119, 21);
            this.cboMaterijal2.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(154, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 15);
            this.label6.TabIndex = 15;
            this.label6.Text = "Materijal 2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(293, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 15);
            this.label7.TabIndex = 18;
            this.label7.Text = "Debljina";
            // 
            // cboDebljina
            // 
            this.cboDebljina.FormattingEnabled = true;
            this.cboDebljina.Location = new System.Drawing.Point(296, 135);
            this.cboDebljina.Name = "cboDebljina";
            this.cboDebljina.Size = new System.Drawing.Size(119, 21);
            this.cboDebljina.TabIndex = 19;
            // 
            // cboTehnologija1
            // 
            this.cboTehnologija1.FormattingEnabled = true;
            this.cboTehnologija1.Location = new System.Drawing.Point(437, 135);
            this.cboTehnologija1.Name = "cboTehnologija1";
            this.cboTehnologija1.Size = new System.Drawing.Size(119, 21);
            this.cboTehnologija1.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(434, 116);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 15);
            this.label8.TabIndex = 20;
            this.label8.Text = "Tehnologija 1";
            // 
            // cboTehnologija2
            // 
            this.cboTehnologija2.FormattingEnabled = true;
            this.cboTehnologija2.Location = new System.Drawing.Point(577, 135);
            this.cboTehnologija2.Name = "cboTehnologija2";
            this.cboTehnologija2.Size = new System.Drawing.Size(119, 21);
            this.cboTehnologija2.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(574, 117);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 15);
            this.label9.TabIndex = 22;
            this.label9.Text = "Tehnologija 2";
            // 
            // chkGrupa
            // 
            this.chkGrupa.AutoSize = true;
            this.chkGrupa.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.chkGrupa.Location = new System.Drawing.Point(656, 27);
            this.chkGrupa.Name = "chkGrupa";
            this.chkGrupa.Size = new System.Drawing.Size(62, 19);
            this.chkGrupa.TabIndex = 24;
            this.chkGrupa.Text = "Ukljuci";
            this.chkGrupa.UseVisualStyleBackColor = true;
            // 
            // chkMaterijal1
            // 
            this.chkMaterijal1.AutoSize = true;
            this.chkMaterijal1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.chkMaterijal1.Location = new System.Drawing.Point(78, 162);
            this.chkMaterijal1.Name = "chkMaterijal1";
            this.chkMaterijal1.Size = new System.Drawing.Size(62, 19);
            this.chkMaterijal1.TabIndex = 25;
            this.chkMaterijal1.Text = "Ukljuci";
            this.chkMaterijal1.UseVisualStyleBackColor = true;
            // 
            // chkMaterijal2
            // 
            this.chkMaterijal2.AutoSize = true;
            this.chkMaterijal2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.chkMaterijal2.Location = new System.Drawing.Point(218, 162);
            this.chkMaterijal2.Name = "chkMaterijal2";
            this.chkMaterijal2.Size = new System.Drawing.Size(62, 19);
            this.chkMaterijal2.TabIndex = 26;
            this.chkMaterijal2.Text = "Ukljuci";
            this.chkMaterijal2.UseVisualStyleBackColor = true;
            // 
            // chkDebljina
            // 
            this.chkDebljina.AutoSize = true;
            this.chkDebljina.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.chkDebljina.Location = new System.Drawing.Point(357, 162);
            this.chkDebljina.Name = "chkDebljina";
            this.chkDebljina.Size = new System.Drawing.Size(62, 19);
            this.chkDebljina.TabIndex = 27;
            this.chkDebljina.Text = "Ukljuci";
            this.chkDebljina.UseVisualStyleBackColor = true;
            // 
            // chkTehnologija1
            // 
            this.chkTehnologija1.AutoSize = true;
            this.chkTehnologija1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.chkTehnologija1.Location = new System.Drawing.Point(498, 162);
            this.chkTehnologija1.Name = "chkTehnologija1";
            this.chkTehnologija1.Size = new System.Drawing.Size(62, 19);
            this.chkTehnologija1.TabIndex = 28;
            this.chkTehnologija1.Text = "Ukljuci";
            this.chkTehnologija1.UseVisualStyleBackColor = true;
            // 
            // chkTehnologija2
            // 
            this.chkTehnologija2.AutoSize = true;
            this.chkTehnologija2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.chkTehnologija2.Location = new System.Drawing.Point(638, 162);
            this.chkTehnologija2.Name = "chkTehnologija2";
            this.chkTehnologija2.Size = new System.Drawing.Size(62, 19);
            this.chkTehnologija2.TabIndex = 29;
            this.chkTehnologija2.Text = "Ukljuci";
            this.chkTehnologija2.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.button6.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.button6.Location = new System.Drawing.Point(713, 128);
            this.button6.Margin = new System.Windows.Forms.Padding(2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(170, 33);
            this.button6.TabIndex = 30;
            this.button6.Text = "Pretraga po osobinama";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // txtPodPlan
            // 
            this.txtPodPlan.Location = new System.Drawing.Point(931, 116);
            this.txtPodPlan.Name = "txtPodPlan";
            this.txtPodPlan.Size = new System.Drawing.Size(173, 20);
            this.txtPodPlan.TabIndex = 31;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(928, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(93, 15);
            this.label10.TabIndex = 32;
            this.label10.Text = "Podplan - grupa";
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.button7.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.button7.Location = new System.Drawing.Point(1109, 115);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(163, 33);
            this.button7.TabIndex = 33;
            this.button7.Text = "Primeni podplan";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // chkRazvrstani
            // 
            this.chkRazvrstani.AutoSize = true;
            this.chkRazvrstani.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.chkRazvrstani.Location = new System.Drawing.Point(931, 185);
            this.chkRazvrstani.Name = "chkRazvrstani";
            this.chkRazvrstani.Size = new System.Drawing.Size(80, 19);
            this.chkRazvrstani.TabIndex = 34;
            this.chkRazvrstani.Text = "Razvrstani";
            this.chkRazvrstani.UseVisualStyleBackColor = true;
            this.chkRazvrstani.CheckStateChanged += new System.EventHandler(this.chkRazvrstani_CheckStateChanged);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.button8.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.button8.Location = new System.Drawing.Point(1111, 192);
            this.button8.Margin = new System.Windows.Forms.Padding(2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(161, 33);
            this.button8.TabIndex = 35;
            this.button8.Text = "Formiraj krojnu listu";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.button9.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.button9.Location = new System.Drawing.Point(1111, 152);
            this.button9.Margin = new System.Windows.Forms.Padding(2);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(163, 33);
            this.button9.TabIndex = 36;
            this.button9.Text = "Pregled podplan";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // chkNerazvrstani
            // 
            this.chkNerazvrstani.AutoSize = true;
            this.chkNerazvrstani.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.chkNerazvrstani.Location = new System.Drawing.Point(931, 208);
            this.chkNerazvrstani.Name = "chkNerazvrstani";
            this.chkNerazvrstani.Size = new System.Drawing.Size(92, 19);
            this.chkNerazvrstani.TabIndex = 34;
            this.chkNerazvrstani.Text = "Nerazvrstani";
            this.chkNerazvrstani.UseVisualStyleBackColor = true;
            this.chkNerazvrstani.CheckStateChanged += new System.EventHandler(this.chkNerazvrstani_CheckStateChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(931, 158);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(173, 21);
            this.comboBox1.TabIndex = 37;
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(928, 139);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 15);
            this.label11.TabIndex = 32;
            this.label11.Text = "Svi podplanovi";
            // 
            // NetoPotrebe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1323, 591);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.chkNerazvrstani);
            this.Controls.Add(this.chkRazvrstani);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtPodPlan);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.chkTehnologija2);
            this.Controls.Add(this.chkTehnologija1);
            this.Controls.Add(this.chkDebljina);
            this.Controls.Add(this.chkMaterijal2);
            this.Controls.Add(this.chkMaterijal1);
            this.Controls.Add(this.chkGrupa);
            this.Controls.Add(this.cboTehnologija2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cboTehnologija1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboDebljina);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboMaterijal2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboMaterijal1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNovaKolicina);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.cboPodgrupa);
            this.Controls.Add(this.cboGrupa);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cboPlan);
            this.Controls.Add(this.label1);
            this.Name = "NetoPotrebe";
            this.Text = "NetoPotrebe";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.NetoPotrebe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPlan;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboGrupa;
        private System.Windows.Forms.ComboBox cboPodgrupa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtNovaKolicina;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox cboMaterijal1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboMaterijal2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboDebljina;
        private System.Windows.Forms.ComboBox cboTehnologija1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboTehnologija2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkGrupa;
        private System.Windows.Forms.CheckBox chkMaterijal1;
        private System.Windows.Forms.CheckBox chkMaterijal2;
        private System.Windows.Forms.CheckBox chkDebljina;
        private System.Windows.Forms.CheckBox chkTehnologija1;
        private System.Windows.Forms.CheckBox chkTehnologija2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox txtPodPlan;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.CheckBox chkRazvrstani;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.CheckBox chkNerazvrstani;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label11;
    }
}