using DocumentFormat.OpenXml.Wordprocessing;
using Irony;
using System.Windows.Forms;

namespace WindowsFormsApplication1.ProizvodnjaUKadrovskoj
{
    partial class frmEvidencijaRada
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEvidencijaRada));
            this.lblDatum = new System.Windows.Forms.Label();
            this.lblZaposleni = new System.Windows.Forms.Label();
            this.txtBrojSatiRada = new System.Windows.Forms.TextBox();
            this.lblBrSati = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgEvidencija = new System.Windows.Forms.DataGridView();
            this.txtNapomena = new System.Windows.Forms.TextBox();
            this.lblNapomena = new System.Windows.Forms.Label();
            this.fPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.dtDan = new System.Windows.Forms.DateTimePicker();
            this.clbZaposleni = new System.Windows.Forms.CheckedListBox();
            this.lblTipRada = new System.Windows.Forms.Label();
            this.clbTipaRada = new System.Windows.Forms.CheckedListBox();
            this.txtSelectedID = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.chkAll = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgEvidencija)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDatum
            // 
            this.lblDatum.AutoSize = true;
            this.lblDatum.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDatum.Location = new System.Drawing.Point(360, 51);
            this.lblDatum.Name = "lblDatum";
            this.lblDatum.Size = new System.Drawing.Size(57, 20);
            this.lblDatum.TabIndex = 166;
            this.lblDatum.Text = "Datum:";
            // 
            // lblZaposleni
            // 
            this.lblZaposleni.AutoSize = true;
            this.lblZaposleni.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblZaposleni.Location = new System.Drawing.Point(23, 122);
            this.lblZaposleni.Name = "lblZaposleni";
            this.lblZaposleni.Size = new System.Drawing.Size(77, 20);
            this.lblZaposleni.TabIndex = 172;
            this.lblZaposleni.Text = "Zaposleni:";
            // 
            // txtBrojSatiRada
            // 
            this.txtBrojSatiRada.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBrojSatiRada.Location = new System.Drawing.Point(358, 157);
            this.txtBrojSatiRada.Name = "txtBrojSatiRada";
            this.txtBrojSatiRada.Size = new System.Drawing.Size(126, 27);
            this.txtBrojSatiRada.TabIndex = 174;
            // 
            // lblBrSati
            // 
            this.lblBrSati.AutoSize = true;
            this.lblBrSati.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBrSati.Location = new System.Drawing.Point(358, 130);
            this.lblBrSati.Name = "lblBrSati";
            this.lblBrSati.Size = new System.Drawing.Size(100, 20);
            this.lblBrSati.TabIndex = 175;
            this.lblBrSati.Text = "Broj sati rada:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(864, -25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 23);
            this.label1.TabIndex = 190;
            this.label1.Text = "Kadrovi u sektorima:";
            // 
            // dgEvidencija
            // 
            this.dgEvidencija.AllowDrop = true;
            this.dgEvidencija.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgEvidencija.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgEvidencija.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEvidencija.Location = new System.Drawing.Point(658, 51);
            this.dgEvidencija.Margin = new System.Windows.Forms.Padding(4);
            this.dgEvidencija.Name = "dgEvidencija";
            this.dgEvidencija.RowHeadersWidth = 51;
            this.dgEvidencija.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgEvidencija.Size = new System.Drawing.Size(1131, 697);
            this.dgEvidencija.TabIndex = 189;
            this.dgEvidencija.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgEvidencija_CellClick);
            // 
            // txtNapomena
            // 
            this.txtNapomena.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNapomena.Location = new System.Drawing.Point(358, 224);
            this.txtNapomena.Multiline = true;
            this.txtNapomena.Name = "txtNapomena";
            this.txtNapomena.Size = new System.Drawing.Size(262, 86);
            this.txtNapomena.TabIndex = 432;
            // 
            // lblNapomena
            // 
            this.lblNapomena.AutoSize = true;
            this.lblNapomena.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNapomena.Location = new System.Drawing.Point(358, 197);
            this.lblNapomena.Name = "lblNapomena";
            this.lblNapomena.Size = new System.Drawing.Size(86, 20);
            this.lblNapomena.TabIndex = 433;
            this.lblNapomena.Text = "Napomena:";
            // 
            // fPanel
            // 
            this.fPanel.Location = new System.Drawing.Point(0, 0);
            this.fPanel.Name = "fPanel";
            this.fPanel.Size = new System.Drawing.Size(200, 300);
            this.fPanel.TabIndex = 0;
            // 
            // dtDan
            // 
            this.dtDan.CustomFormat = "dd.MM.yyyy";
            this.dtDan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtDan.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDan.Location = new System.Drawing.Point(363, 78);
            this.dtDan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtDan.Name = "dtDan";
            this.dtDan.Size = new System.Drawing.Size(121, 27);
            this.dtDan.TabIndex = 435;
            this.dtDan.ValueChanged += new System.EventHandler(this.dtDan_ValueChanged);
            // 
            // clbZaposleni
            // 
            this.clbZaposleni.AllowDrop = true;
            this.clbZaposleni.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.clbZaposleni.Location = new System.Drawing.Point(24, 157);
            this.clbZaposleni.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.clbZaposleni.Name = "clbZaposleni";
            this.clbZaposleni.Size = new System.Drawing.Size(294, 584);
            this.clbZaposleni.TabIndex = 439;
            // 
            // lblTipRada
            // 
            this.lblTipRada.AutoSize = true;
            this.lblTipRada.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTipRada.Location = new System.Drawing.Point(358, 323);
            this.lblTipRada.Name = "lblTipRada";
            this.lblTipRada.Size = new System.Drawing.Size(67, 20);
            this.lblTipRada.TabIndex = 440;
            this.lblTipRada.Text = "Tip rada:";
            // 
            // clbTipaRada
            // 
            this.clbTipaRada.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.clbTipaRada.FormattingEnabled = true;
            this.clbTipaRada.HorizontalScrollbar = true;
            this.clbTipaRada.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.clbTipaRada.Location = new System.Drawing.Point(356, 350);
            this.clbTipaRada.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.clbTipaRada.MultiColumn = true;
            this.clbTipaRada.Name = "clbTipaRada";
            this.clbTipaRada.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.clbTipaRada.Size = new System.Drawing.Size(264, 244);
            this.clbTipaRada.TabIndex = 176;
            this.clbTipaRada.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbTipaRada_ItemCheck);
            // 
            // txtSelectedID
            // 
            this.txtSelectedID.Enabled = false;
            this.txtSelectedID.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSelectedID.Location = new System.Drawing.Point(24, 78);
            this.txtSelectedID.Name = "txtSelectedID";
            this.txtSelectedID.Size = new System.Drawing.Size(100, 27);
            this.txtSelectedID.TabIndex = 441;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Enabled = false;
            this.lblID.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblID.Location = new System.Drawing.Point(23, 51);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(24, 20);
            this.lblID.TabIndex = 442;
            this.lblID.Text = "ID";
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
            this.toolStrip1.Size = new System.Drawing.Size(1802, 27);
            this.toolStrip1.TabIndex = 445;
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
            this.tsSave.Click += new System.EventHandler(this.btnSnimi_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsDelete.Image")));
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(29, 24);
            this.tsDelete.Text = "toolStripButton1";
            this.tsDelete.Click += new System.EventHandler(this.btnIzbrisi_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkAll.Location = new System.Drawing.Point(146, 122);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(117, 24);
            this.chkAll.TabIndex = 446;
            this.chkAll.Text = "Svi zaposleni";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // frmEvidencijaRada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1802, 762);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.txtSelectedID);
            this.Controls.Add(this.lblTipRada);
            this.Controls.Add(this.clbZaposleni);
            this.Controls.Add(this.dtDan);
            this.Controls.Add(this.lblNapomena);
            this.Controls.Add(this.txtNapomena);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgEvidencija);
            this.Controls.Add(this.clbTipaRada);
            this.Controls.Add(this.lblBrSati);
            this.Controls.Add(this.txtBrojSatiRada);
            this.Controls.Add(this.lblZaposleni);
            this.Controls.Add(this.lblDatum);
            this.Icon = global::WindowsFormsApplication1.Properties.Resources.JelaIkona;
            this.Name = "frmEvidencijaRada";
            this.Text = "frmEvidencijaRada";
            this.Load += new System.EventHandler(this.frmEvidencijaRada_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgEvidencija)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblDatum;
        private System.Windows.Forms.Label lblZaposleni;
        private System.Windows.Forms.TextBox txtBrojSatiRada;
        private System.Windows.Forms.Label lblBrSati;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgEvidencija;
        private System.Windows.Forms.TextBox txtNapomena;
        private System.Windows.Forms.Label lblNapomena;
        private FlowLayoutPanel fPanel;
        private DateTimePicker dtDan;
        private CheckedListBox clbZaposleni;
        private Label lblTipRada;
        private CheckedListBox clbTipaRada;
        private TextBox txtSelectedID;
        private Label lblID;
        private ToolStrip toolStrip1;
        private ToolStripButton tsNew;
        private ToolStripButton tsSave;
        private ToolStripButton tsDelete;
        private ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.CheckBox chkAll;
    }
}