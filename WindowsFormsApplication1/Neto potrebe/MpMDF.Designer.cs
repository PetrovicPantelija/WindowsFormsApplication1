namespace WindowsFormsApplication1.Neto_potrebe
{
    partial class MpMDF
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
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cboGrupa = new System.Windows.Forms.ComboBox();
            this.cboPodgrupa = new System.Windows.Forms.ComboBox();
            this.btnDodeli = new System.Windows.Forms.Button();
            this.chkGrupa = new System.Windows.Forms.CheckBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSearch2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMaterijal1 = new System.Windows.Forms.TextBox();
            this.txtMaterijal2 = new System.Windows.Forms.TextBox();
            this.txtDebljina = new System.Windows.Forms.TextBox();
            this.txtTehnologija1 = new System.Windows.Forms.TextBox();
            this.txtTehnologija2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSearchMP = new System.Windows.Forms.TextBox();
            this.btnObrisi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Grupa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Podgrupa";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(336, 49);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1009, 497);
            this.dataGridView1.TabIndex = 1;
            // 
            // cboGrupa
            // 
            this.cboGrupa.FormattingEnabled = true;
            this.cboGrupa.Location = new System.Drawing.Point(12, 37);
            this.cboGrupa.Name = "cboGrupa";
            this.cboGrupa.Size = new System.Drawing.Size(261, 21);
            this.cboGrupa.TabIndex = 2;
            this.cboGrupa.SelectionChangeCommitted += new System.EventHandler(this.cboGrupa_SelectionChangeCommitted);
            this.cboGrupa.SelectedValueChanged += new System.EventHandler(this.cboGrupa_SelectedValueChanged);
            // 
            // cboPodgrupa
            // 
            this.cboPodgrupa.FormattingEnabled = true;
            this.cboPodgrupa.Location = new System.Drawing.Point(12, 89);
            this.cboPodgrupa.Name = "cboPodgrupa";
            this.cboPodgrupa.Size = new System.Drawing.Size(261, 21);
            this.cboPodgrupa.TabIndex = 2;
            // 
            // btnDodeli
            // 
            this.btnDodeli.Location = new System.Drawing.Point(11, 392);
            this.btnDodeli.Name = "btnDodeli";
            this.btnDodeli.Size = new System.Drawing.Size(173, 39);
            this.btnDodeli.TabIndex = 3;
            this.btnDodeli.Text = "Dodeli";
            this.btnDodeli.UseVisualStyleBackColor = true;
            this.btnDodeli.Click += new System.EventHandler(this.btnDodeli_Click);
            // 
            // chkGrupa
            // 
            this.chkGrupa.AutoSize = true;
            this.chkGrupa.Location = new System.Drawing.Point(1087, 20);
            this.chkGrupa.Name = "chkGrupa";
            this.chkGrupa.Size = new System.Drawing.Size(73, 17);
            this.chkGrupa.TabIndex = 4;
            this.chkGrupa.Text = "Ima grupu";
            this.chkGrupa.UseVisualStyleBackColor = true;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(635, 16);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(173, 20);
            this.textBoxSearch.TabIndex = 77;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(566, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 78;
            this.label3.Text = "Kod like";
            // 
            // textBoxSearch2
            // 
            this.textBoxSearch2.Location = new System.Drawing.Point(821, 16);
            this.textBoxSearch2.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSearch2.Name = "textBoxSearch2";
            this.textBoxSearch2.Size = new System.Drawing.Size(172, 20);
            this.textBoxSearch2.TabIndex = 79;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(998, 13);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 24);
            this.button1.TabIndex = 80;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 507);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(174, 39);
            this.button2.TabIndex = 81;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 338);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 91;
            this.label9.Text = "Tehnologija 2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 290);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
            this.label8.TabIndex = 89;
            this.label8.Text = "Tehnologija 1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 242);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 87;
            this.label7.Text = "Debljina";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 85;
            this.label6.Text = "Materijal 2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 83;
            this.label5.Text = "Materijal 1";
            // 
            // txtMaterijal1
            // 
            this.txtMaterijal1.Location = new System.Drawing.Point(11, 164);
            this.txtMaterijal1.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaterijal1.Name = "txtMaterijal1";
            this.txtMaterijal1.Size = new System.Drawing.Size(173, 20);
            this.txtMaterijal1.TabIndex = 93;
            // 
            // txtMaterijal2
            // 
            this.txtMaterijal2.Location = new System.Drawing.Point(12, 212);
            this.txtMaterijal2.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaterijal2.Name = "txtMaterijal2";
            this.txtMaterijal2.Size = new System.Drawing.Size(173, 20);
            this.txtMaterijal2.TabIndex = 94;
            // 
            // txtDebljina
            // 
            this.txtDebljina.Location = new System.Drawing.Point(11, 261);
            this.txtDebljina.Margin = new System.Windows.Forms.Padding(2);
            this.txtDebljina.Name = "txtDebljina";
            this.txtDebljina.Size = new System.Drawing.Size(173, 20);
            this.txtDebljina.TabIndex = 95;
            // 
            // txtTehnologija1
            // 
            this.txtTehnologija1.Location = new System.Drawing.Point(11, 309);
            this.txtTehnologija1.Margin = new System.Windows.Forms.Padding(2);
            this.txtTehnologija1.Name = "txtTehnologija1";
            this.txtTehnologija1.Size = new System.Drawing.Size(173, 20);
            this.txtTehnologija1.TabIndex = 96;
            // 
            // txtTehnologija2
            // 
            this.txtTehnologija2.Location = new System.Drawing.Point(11, 357);
            this.txtTehnologija2.Margin = new System.Windows.Forms.Padding(2);
            this.txtTehnologija2.Name = "txtTehnologija2";
            this.txtTehnologija2.Size = new System.Drawing.Size(173, 20);
            this.txtTehnologija2.TabIndex = 97;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(333, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 78;
            this.label4.Text = "Kod like";
            // 
            // textBoxSearchMP
            // 
            this.textBoxSearchMP.Location = new System.Drawing.Point(383, 18);
            this.textBoxSearchMP.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSearchMP.Name = "textBoxSearchMP";
            this.textBoxSearchMP.Size = new System.Drawing.Size(172, 20);
            this.textBoxSearchMP.TabIndex = 79;
            // 
            // btnObrisi
            // 
            this.btnObrisi.Location = new System.Drawing.Point(10, 447);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(175, 39);
            this.btnObrisi.TabIndex = 3;
            this.btnObrisi.Text = "Obriši";
            this.btnObrisi.UseVisualStyleBackColor = true;
            this.btnObrisi.Click += new System.EventHandler(this.btnDodeli_Click);
            // 
            // MpMDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1345, 570);
            this.Controls.Add(this.txtTehnologija2);
            this.Controls.Add(this.txtTehnologija1);
            this.Controls.Add(this.txtDebljina);
            this.Controls.Add(this.txtMaterijal2);
            this.Controls.Add(this.txtMaterijal1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxSearchMP);
            this.Controls.Add(this.textBoxSearch2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.chkGrupa);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.btnDodeli);
            this.Controls.Add(this.cboPodgrupa);
            this.Controls.Add(this.cboGrupa);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MpMDF";
            this.Text = "MpMDF";
            this.Load += new System.EventHandler(this.MpMDF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cboGrupa;
        private System.Windows.Forms.ComboBox cboPodgrupa;
        private System.Windows.Forms.Button btnDodeli;
        private System.Windows.Forms.CheckBox chkGrupa;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSearch2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMaterijal1;
        private System.Windows.Forms.TextBox txtMaterijal2;
        private System.Windows.Forms.TextBox txtDebljina;
        private System.Windows.Forms.TextBox txtTehnologija1;
        private System.Windows.Forms.TextBox txtTehnologija2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxSearchMP;
        private System.Windows.Forms.Button btnObrisi;
    }
}