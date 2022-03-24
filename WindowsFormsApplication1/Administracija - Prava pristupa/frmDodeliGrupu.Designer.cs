
namespace WindowsFormsApplication1.Administracija___Prava_pristupa
{
    partial class frmDodeliGrupu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDodeliGrupu));
            this.label2 = new System.Windows.Forms.Label();
            this.combo_Korisnik = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.combo_Grupa = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.IconButton = new System.Windows.Forms.PictureBox();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconButton)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 206;
            this.label2.Text = "Korisnik";
            // 
            // combo_Korisnik
            // 
            this.combo_Korisnik.FormattingEnabled = true;
            this.combo_Korisnik.Location = new System.Drawing.Point(12, 140);
            this.combo_Korisnik.Name = "combo_Korisnik";
            this.combo_Korisnik.Size = new System.Drawing.Size(313, 24);
            this.combo_Korisnik.TabIndex = 209;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 206;
            this.label3.Text = "Grupa";
            // 
            // combo_Grupa
            // 
            this.combo_Grupa.FormattingEnabled = true;
            this.combo_Grupa.Location = new System.Drawing.Point(12, 222);
            this.combo_Grupa.Name = "combo_Grupa";
            this.combo_Grupa.Size = new System.Drawing.Size(313, 24);
            this.combo_Grupa.TabIndex = 209;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 275);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 41);
            this.button1.TabIndex = 211;
            this.button1.Text = "Dodeli Grupu";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(151, 275);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(174, 41);
            this.button2.TabIndex = 211;
            this.button2.Text = "Pretrazi po korisniku";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(74, 352);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(149, 41);
            this.button3.TabIndex = 211;
            this.button3.Text = "Ukloni iz grupe";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(385, 32);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(693, 706);
            this.dataGridView1.TabIndex = 212;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // IconButton
            // 
            this.IconButton.Image = ((System.Drawing.Image)(resources.GetObject("IconButton.Image")));
            this.IconButton.Location = new System.Drawing.Point(74, 460);
            this.IconButton.Margin = new System.Windows.Forms.Padding(4);
            this.IconButton.Name = "IconButton";
            this.IconButton.Size = new System.Drawing.Size(152, 45);
            this.IconButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.IconButton.TabIndex = 214;
            this.IconButton.TabStop = false;
            // 
            // txt_ID
            // 
            this.txt_ID.Location = new System.Drawing.Point(12, 65);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(100, 22);
            this.txt_ID.TabIndex = 216;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 20);
            this.label1.TabIndex = 215;
            this.label1.Text = "ID";
            // 
            // frmDodeliGrupu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(83)))), ((int)(((byte)(87)))));
            this.ClientSize = new System.Drawing.Size(1098, 750);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IconButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.combo_Grupa);
            this.Controls.Add(this.combo_Korisnik);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "frmDodeliGrupu";
            this.Text = "frmDodeliGrupu";
            this.Load += new System.EventHandler(this.frmDodeliGrupu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox combo_Korisnik;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox combo_Grupa;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox IconButton;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Label label1;
    }
}