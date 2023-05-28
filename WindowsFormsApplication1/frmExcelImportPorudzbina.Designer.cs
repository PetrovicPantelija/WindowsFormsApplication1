namespace WindowsFormsApplication1
{
    partial class frmExcelImportPorudzbina
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExcelImportPorudzbina));
            this.txtPutanja = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ofd1 = new System.Windows.Forms.OpenFileDialog();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.cboDatumUtovara = new System.Windows.Forms.DateTimePicker();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnImeFajla = new FontAwesome.Sharp.IconButton();
            this.button1 = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPutanja
            // 
            this.txtPutanja.Location = new System.Drawing.Point(31, 48);
            this.txtPutanja.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPutanja.Name = "txtPutanja";
            this.txtPutanja.Size = new System.Drawing.Size(557, 22);
            this.txtPutanja.TabIndex = 13;
            this.txtPutanja.TextChanged += new System.EventHandler(this.txtPutanja_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 23);
            this.label1.TabIndex = 12;
            this.label1.Text = "Ulazni folder / Custumer input file";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // ofd1
            // 
            this.ofd1.FileName = "openFileDialog1";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(246)))));
            this.progressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.progressBar.Location = new System.Drawing.Point(31, 150);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1088, 28);
            this.progressBar.TabIndex = 42;
            this.progressBar.Click += new System.EventHandler(this.progressBar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(31, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 23);
            this.label2.TabIndex = 43;
            this.label2.Text = "Datum:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // cboDatumUtovara
            // 
            this.cboDatumUtovara.CustomFormat = "dd.MM.yyyy";
            this.cboDatumUtovara.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cboDatumUtovara.Location = new System.Drawing.Point(31, 110);
            this.cboDatumUtovara.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboDatumUtovara.Name = "cboDatumUtovara";
            this.cboDatumUtovara.Size = new System.Drawing.Size(224, 22);
            this.cboDatumUtovara.TabIndex = 44;
            this.cboDatumUtovara.ValueChanged += new System.EventHandler(this.cboDatumUtovara_ValueChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(965, 5);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(183, 42);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 165;
            this.pictureBox2.TabStop = false;
            // 
            // btnImeFajla
            // 
            this.btnImeFajla.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.btnImeFajla.FlatAppearance.BorderSize = 0;
            this.btnImeFajla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImeFajla.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnImeFajla.IconChar = FontAwesome.Sharp.IconChar.FileAlt;
            this.btnImeFajla.IconColor = System.Drawing.Color.Black;
            this.btnImeFajla.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnImeFajla.IconSize = 23;
            this.btnImeFajla.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImeFajla.Location = new System.Drawing.Point(603, 46);
            this.btnImeFajla.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnImeFajla.Name = "btnImeFajla";
            this.btnImeFajla.Size = new System.Drawing.Size(136, 31);
            this.btnImeFajla.TabIndex = 167;
            this.btnImeFajla.Text = "Ime fajla";
            this.btnImeFajla.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImeFajla.UseVisualStyleBackColor = false;
            this.btnImeFajla.Click += new System.EventHandler(this.btnImeFajla_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.button1.IconChar = FontAwesome.Sharp.IconChar.CalendarCheck;
            this.button1.IconColor = System.Drawing.Color.Black;
            this.button1.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.button1.IconSize = 23;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(268, 107);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 31);
            this.button1.TabIndex = 168;
            this.button1.Text = "Obradi";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmExcelImportPorudzbina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1151, 199);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.cboDatumUtovara);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.txtPutanja);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnImeFajla);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmExcelImportPorudzbina";
            this.Text = "Excel import MP";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtPutanja;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog ofd1;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker cboDatumUtovara;
        private System.Windows.Forms.PictureBox pictureBox2;
        private FontAwesome.Sharp.IconButton btnImeFajla;
        private FontAwesome.Sharp.IconButton button1;
    }
}