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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
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
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 207);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1299, 372);
            this.dataGridView1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(365, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
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
            this.label3.Location = new System.Drawing.Point(767, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Podgrupa";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 57);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 33);
            this.button2.TabIndex = 6;
            this.button2.Text = "Ceo plan";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(532, 57);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 33);
            this.button1.TabIndex = 7;
            this.button1.Text = "Plan + Grupa";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(890, 57);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(139, 33);
            this.button3.TabIndex = 8;
            this.button3.Text = "Plan + Grupa + Podgrupa";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(184, 57);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(106, 33);
            this.button4.TabIndex = 9;
            this.button4.Text = "Obriši NP";
            this.button4.UseVisualStyleBackColor = true;
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
            this.label4.Location = new System.Drawing.Point(1171, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Nova kolicina";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1143, 57);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(139, 33);
            this.button5.TabIndex = 12;
            this.button5.Text = "Promeni kolicinu";
            this.button5.UseVisualStyleBackColor = true;
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
            this.label5.Location = new System.Drawing.Point(14, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
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
            this.label6.Location = new System.Drawing.Point(154, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Materijal 2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(293, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
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
            this.label8.Location = new System.Drawing.Point(434, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
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
            this.label9.Location = new System.Drawing.Point(574, 119);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Tehnologija 2";
            // 
            // chkGrupa
            // 
            this.chkGrupa.AutoSize = true;
            this.chkGrupa.Location = new System.Drawing.Point(656, 27);
            this.chkGrupa.Name = "chkGrupa";
            this.chkGrupa.Size = new System.Drawing.Size(58, 17);
            this.chkGrupa.TabIndex = 24;
            this.chkGrupa.Text = "Ukljuci";
            this.chkGrupa.UseVisualStyleBackColor = true;
            // 
            // chkMaterijal1
            // 
            this.chkMaterijal1.AutoSize = true;
            this.chkMaterijal1.Location = new System.Drawing.Point(78, 162);
            this.chkMaterijal1.Name = "chkMaterijal1";
            this.chkMaterijal1.Size = new System.Drawing.Size(58, 17);
            this.chkMaterijal1.TabIndex = 25;
            this.chkMaterijal1.Text = "Ukljuci";
            this.chkMaterijal1.UseVisualStyleBackColor = true;
            // 
            // chkMaterijal2
            // 
            this.chkMaterijal2.AutoSize = true;
            this.chkMaterijal2.Location = new System.Drawing.Point(218, 162);
            this.chkMaterijal2.Name = "chkMaterijal2";
            this.chkMaterijal2.Size = new System.Drawing.Size(58, 17);
            this.chkMaterijal2.TabIndex = 26;
            this.chkMaterijal2.Text = "Ukljuci";
            this.chkMaterijal2.UseVisualStyleBackColor = true;
            // 
            // chkDebljina
            // 
            this.chkDebljina.AutoSize = true;
            this.chkDebljina.Location = new System.Drawing.Point(357, 162);
            this.chkDebljina.Name = "chkDebljina";
            this.chkDebljina.Size = new System.Drawing.Size(58, 17);
            this.chkDebljina.TabIndex = 27;
            this.chkDebljina.Text = "Ukljuci";
            this.chkDebljina.UseVisualStyleBackColor = true;
            // 
            // chkTehnologija1
            // 
            this.chkTehnologija1.AutoSize = true;
            this.chkTehnologija1.Location = new System.Drawing.Point(498, 162);
            this.chkTehnologija1.Name = "chkTehnologija1";
            this.chkTehnologija1.Size = new System.Drawing.Size(58, 17);
            this.chkTehnologija1.TabIndex = 28;
            this.chkTehnologija1.Text = "Ukljuci";
            this.chkTehnologija1.UseVisualStyleBackColor = true;
            // 
            // chkTehnologija2
            // 
            this.chkTehnologija2.AutoSize = true;
            this.chkTehnologija2.Location = new System.Drawing.Point(638, 162);
            this.chkTehnologija2.Name = "chkTehnologija2";
            this.chkTehnologija2.Size = new System.Drawing.Size(58, 17);
            this.chkTehnologija2.TabIndex = 29;
            this.chkTehnologija2.Text = "Ukljuci";
            this.chkTehnologija2.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(724, 128);
            this.button6.Margin = new System.Windows.Forms.Padding(2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(163, 33);
            this.button6.TabIndex = 30;
            this.button6.Text = "Pretraga po osobinama";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // NetoPotrebe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1323, 591);
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
    }
}