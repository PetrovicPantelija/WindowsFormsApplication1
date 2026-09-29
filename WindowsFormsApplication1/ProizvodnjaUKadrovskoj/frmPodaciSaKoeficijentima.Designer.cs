namespace WindowsFormsApplication1.ProizvodnjaUKadrovskoj
{
    partial class frmPodaciSaKoeficijentima
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPodaciSaKoeficijentima));
            this.dgEvidencija = new System.Windows.Forms.DataGridView();
            this.dtDan = new System.Windows.Forms.DateTimePicker();
            this.btnPrikazi = new System.Windows.Forms.Button();
            this.btnDetaljanPrikaz = new System.Windows.Forms.Button();
            this.plDetalji = new System.Windows.Forms.Panel();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.cboMesec = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txt_Koeficijent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txtSelectedLargoBroj = new System.Windows.Forms.TextBox();
            this.lblSektor = new System.Windows.Forms.Label();
            this.cboSektor = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgEvidencija)).BeginInit();
            this.plDetalji.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
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
            this.dgEvidencija.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgEvidencija_CellClick);
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
            this.btnPrikazi.Location = new System.Drawing.Point(207, 48);
            this.btnPrikazi.Name = "btnPrikazi";
            this.btnPrikazi.Size = new System.Drawing.Size(75, 30);
            this.btnPrikazi.TabIndex = 437;
            this.btnPrikazi.Text = "Prikazi";
            this.btnPrikazi.UseVisualStyleBackColor = true;
            this.btnPrikazi.Click += new System.EventHandler(this.dtDan_ValueChanged);
            // 
            // btnDetaljanPrikaz
            // 
            this.btnDetaljanPrikaz.Location = new System.Drawing.Point(627, 48);
            this.btnDetaljanPrikaz.Name = "btnDetaljanPrikaz";
            this.btnDetaljanPrikaz.Size = new System.Drawing.Size(244, 30);
            this.btnDetaljanPrikaz.TabIndex = 438;
            this.btnDetaljanPrikaz.Text = "Detaljan prikaz koeficijenata";
            this.btnDetaljanPrikaz.UseVisualStyleBackColor = true;
            this.btnDetaljanPrikaz.Click += new System.EventHandler(this.btnDetaljanPrikaz_Click);
            // 
            // plDetalji
            // 
            this.plDetalji.Controls.Add(this.txt_ID);
            this.plDetalji.Controls.Add(this.btnClose);
            this.plDetalji.Controls.Add(this.cboMesec);
            this.plDetalji.Controls.Add(this.label3);
            this.plDetalji.Controls.Add(this.dataGridView1);
            this.plDetalji.Controls.Add(this.txt_Koeficijent);
            this.plDetalji.Controls.Add(this.label2);
            this.plDetalji.Controls.Add(this.label1);
            this.plDetalji.Controls.Add(this.toolStrip1);
            this.plDetalji.Location = new System.Drawing.Point(436, 79);
            this.plDetalji.Name = "plDetalji";
            this.plDetalji.Size = new System.Drawing.Size(1143, 600);
            this.plDetalji.TabIndex = 439;
            this.plDetalji.Visible = false;
            // 
            // txt_ID
            // 
            this.txt_ID.Location = new System.Drawing.Point(26, 77);
            this.txt_ID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.ReadOnly = true;
            this.txt_ID.Size = new System.Drawing.Size(117, 22);
            this.txt_ID.TabIndex = 455;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(26, 355);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(172, 30);
            this.btnClose.TabIndex = 454;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cboMesec
            // 
            this.cboMesec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.cboMesec.BackColor = System.Drawing.SystemColors.Window;
            this.cboMesec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMesec.FormattingEnabled = true;
            this.cboMesec.Location = new System.Drawing.Point(26, 241);
            this.cboMesec.Margin = new System.Windows.Forms.Padding(4);
            this.cboMesec.Name = "cboMesec";
            this.cboMesec.Size = new System.Drawing.Size(169, 24);
            this.cboMesec.TabIndex = 453;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 452;
            this.label3.Text = "MESEC";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(233, 41);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(898, 548);
            this.dataGridView1.TabIndex = 451;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // txt_Koeficijent
            // 
            this.txt_Koeficijent.Location = new System.Drawing.Point(26, 158);
            this.txt_Koeficijent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Koeficijent.Name = "txt_Koeficijent";
            this.txt_Koeficijent.Size = new System.Drawing.Size(113, 22);
            this.txt_Koeficijent.TabIndex = 449;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 20);
            this.label2.TabIndex = 447;
            this.label2.Text = "KOEFICIJENT";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 20);
            this.label1.TabIndex = 448;
            this.label1.Text = "ID";
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
            this.toolStrip1.Size = new System.Drawing.Size(1143, 27);
            this.toolStrip1.TabIndex = 446;
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
            // txtSelectedLargoBroj
            // 
            this.txtSelectedLargoBroj.Location = new System.Drawing.Point(893, 51);
            this.txtSelectedLargoBroj.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSelectedLargoBroj.Name = "txtSelectedLargoBroj";
            this.txtSelectedLargoBroj.Size = new System.Drawing.Size(117, 22);
            this.txtSelectedLargoBroj.TabIndex = 451;
            this.txtSelectedLargoBroj.Visible = false;
            // 
            // lblSektor
            // 
            this.lblSektor.AutoSize = true;
            this.lblSektor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSektor.Location = new System.Drawing.Point(322, 50);
            this.lblSektor.Name = "lblSektor";
            this.lblSektor.Size = new System.Drawing.Size(54, 20);
            this.lblSektor.TabIndex = 453;
            this.lblSektor.Text = "Sektor:";
            // 
            // cboSektor
            // 
            this.cboSektor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSektor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboSektor.FormattingEnabled = true;
            this.cboSektor.Location = new System.Drawing.Point(404, 49);
            this.cboSektor.Name = "cboSektor";
            this.cboSektor.Size = new System.Drawing.Size(172, 28);
            this.cboSektor.TabIndex = 452;
            this.cboSektor.SelectedIndexChanged += new System.EventHandler(this.cboSektor_SelectedIndexChanged);
            // 
            // frmPodaciSaKoeficijentima
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 683);
            this.Controls.Add(this.lblSektor);
            this.Controls.Add(this.cboSektor);
            this.Controls.Add(this.txtSelectedLargoBroj);
            this.Controls.Add(this.plDetalji);
            this.Controls.Add(this.btnDetaljanPrikaz);
            this.Controls.Add(this.btnPrikazi);
            this.Controls.Add(this.dtDan);
            this.Controls.Add(this.dgEvidencija);
            this.Icon = global::WindowsFormsApplication1.Properties.Resources.JelaIkona;
            this.Name = "frmPodaciSaKoeficijentima";
            this.Text = "frmPodaciSaKoeficijentima";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPodaciSaKoeficijentima_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgEvidencija)).EndInit();
            this.plDetalji.ResumeLayout(false);
            this.plDetalji.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgEvidencija;
        private System.Windows.Forms.DateTimePicker dtDan;
        private System.Windows.Forms.Button btnPrikazi;
        private System.Windows.Forms.Button btnDetaljanPrikaz;
        private System.Windows.Forms.Panel plDetalji;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_Koeficijent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboMesec;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtSelectedLargoBroj;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Label lblSektor;
        private System.Windows.Forms.ComboBox cboSektor;
    }
}