namespace WindowsFormsApplication1.ProizvodnjaUKadrovskoj
{
    partial class frmPodaciOZaposlenima
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
            this.dgEvidencija = new System.Windows.Forms.DataGridView();
            this.dtDan = new System.Windows.Forms.DateTimePicker();
            this.btnPrikazi = new System.Windows.Forms.Button();
            this.cboSektor = new System.Windows.Forms.ComboBox();
            this.lblSektor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgEvidencija)).BeginInit();
            this.SuspendLayout();
            // 
            // dgEvidencija
            // 
            this.dgEvidencija.AllowUserToAddRows = false;
            this.dgEvidencija.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgEvidencija.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEvidencija.Location = new System.Drawing.Point(13, 102);
            this.dgEvidencija.Name = "dgEvidencija";
            this.dgEvidencija.RowHeadersWidth = 51;
            this.dgEvidencija.RowTemplate.Height = 24;
            this.dgEvidencija.Size = new System.Drawing.Size(1159, 462);
            this.dgEvidencija.TabIndex = 1;
            this.dgEvidencija.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgEvidencija_CellFormatting);
            // 
            // dtDan
            // 
            this.dtDan.CustomFormat = "MM.yyyy";
            this.dtDan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtDan.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDan.Location = new System.Drawing.Point(26, 49);
            this.dtDan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtDan.Name = "dtDan";
            this.dtDan.ShowUpDown = true;
            this.dtDan.Size = new System.Drawing.Size(151, 27);
            this.dtDan.TabIndex = 436;
            // 
            // btnPrikazi
            // 
            this.btnPrikazi.Location = new System.Drawing.Point(207, 49);
            this.btnPrikazi.Name = "btnPrikazi";
            this.btnPrikazi.Size = new System.Drawing.Size(75, 30);
            this.btnPrikazi.TabIndex = 437;
            this.btnPrikazi.Text = "Prikazi";
            this.btnPrikazi.UseVisualStyleBackColor = true;
            this.btnPrikazi.Click += new System.EventHandler(this.dtDan_ValueChanged);
            // 
            // cboSektor
            // 
            this.cboSektor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSektor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboSektor.FormattingEnabled = true;
            this.cboSektor.Location = new System.Drawing.Point(417, 52);
            this.cboSektor.Name = "cboSektor";
            this.cboSektor.Size = new System.Drawing.Size(172, 28);
            this.cboSektor.TabIndex = 438;
            this.cboSektor.SelectedIndexChanged += new System.EventHandler(this.cboSektor_SelectedIndexChanged);
            // 
            // lblSektor
            // 
            this.lblSektor.AutoSize = true;
            this.lblSektor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSektor.Location = new System.Drawing.Point(336, 53);
            this.lblSektor.Name = "lblSektor";
            this.lblSektor.Size = new System.Drawing.Size(54, 20);
            this.lblSektor.TabIndex = 439;
            this.lblSektor.Text = "Sektor:";
            // 
            // frmPodaciOZaposlenima
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 683);
            this.Controls.Add(this.lblSektor);
            this.Controls.Add(this.cboSektor);
            this.Controls.Add(this.btnPrikazi);
            this.Controls.Add(this.dtDan);
            this.Controls.Add(this.dgEvidencija);
            this.Name = "frmPodaciOZaposlenima";
            this.Text = "frmPodaciOZaposlenima";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPodaciOZaposlenima_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgEvidencija)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgEvidencija;
        private System.Windows.Forms.DateTimePicker dtDan;
        private System.Windows.Forms.Button btnPrikazi;
        private System.Windows.Forms.ComboBox cboSektor;
        private System.Windows.Forms.Label lblSektor;
    }
}