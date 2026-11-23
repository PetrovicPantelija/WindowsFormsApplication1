
namespace WindowsFormsApplication1.Kalkulacije
{
    partial class frmTroskoviIzracun
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
            this.pnlDugmad = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new FontAwesome.Sharp.IconButton();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.btnSelektujSve = new FontAwesome.Sharp.IconButton();
            this.pnlFajl = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnImeFajla = new FontAwesome.Sharp.IconButton();
            this.txtPutanja = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ofd1 = new System.Windows.Forms.OpenFileDialog();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.dataGridView1 = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.pnlDugmad.SuspendLayout();
            this.pnlFajl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            //
            // pnlDugmad
            //
            this.pnlDugmad.BackColor = System.Drawing.Color.FromArgb(248, 247, 244);
            this.pnlDugmad.Controls.Add(this.button1);
            this.pnlDugmad.Controls.Add(this.iconButton1);
            this.pnlDugmad.Controls.Add(this.btnSelektujSve);
            this.pnlDugmad.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDugmad.Location = new System.Drawing.Point(0, 0);
            this.pnlDugmad.Name = "pnlDugmad";
            this.pnlDugmad.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.pnlDugmad.Size = new System.Drawing.Size(900, 42);
            this.pnlDugmad.TabIndex = 0;
            //
            // button1
            //
            this.button1.IconChar = FontAwesome.Sharp.IconChar.CalendarCheck;
            this.button1.IconColor = System.Drawing.Color.White;
            this.button1.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.button1.IconSize = 18;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 30);
            this.button1.TabIndex = 0;
            this.button1.Tag = "glavno";
            this.button1.Text = "Obradi";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            //
            // iconButton1
            //
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.CalendarCheck;
            this.iconButton1.IconColor = System.Drawing.Color.White;
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.iconButton1.IconSize = 18;
            this.iconButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Size = new System.Drawing.Size(120, 30);
            this.iconButton1.TabIndex = 1;
            this.iconButton1.Tag = "glavno";
            this.iconButton1.Text = "Obradi csv";
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click);
            //
            // btnSelektujSve
            //
            this.btnSelektujSve.IconChar = FontAwesome.Sharp.IconChar.CheckDouble;
            this.btnSelektujSve.IconColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.btnSelektujSve.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnSelektujSve.IconSize = 18;
            this.btnSelektujSve.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelektujSve.Margin = new System.Windows.Forms.Padding(18, 3, 3, 3);
            this.btnSelektujSve.Name = "btnSelektujSve";
            this.btnSelektujSve.Size = new System.Drawing.Size(125, 30);
            this.btnSelektujSve.TabIndex = 2;
            this.btnSelektujSve.Text = "Selektuj sve";
            this.btnSelektujSve.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelektujSve.Click += new System.EventHandler(this.btnSelektujSve_Click);
            //
            // pnlFajl
            //
            this.pnlFajl.BackColor = System.Drawing.Color.FromArgb(248, 247, 244);
            this.pnlFajl.Controls.Add(this.progressBar);
            this.pnlFajl.Controls.Add(this.btnImeFajla);
            this.pnlFajl.Controls.Add(this.txtPutanja);
            this.pnlFajl.Controls.Add(this.label1);
            this.pnlFajl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFajl.Location = new System.Drawing.Point(0, 42);
            this.pnlFajl.Name = "pnlFajl";
            this.pnlFajl.Size = new System.Drawing.Size(900, 66);
            this.pnlFajl.TabIndex = 1;
            //
            // progressBar
            //
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 42);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(876, 14);
            this.progressBar.TabIndex = 3;
            //
            // btnImeFajla
            //
            this.btnImeFajla.IconChar = FontAwesome.Sharp.IconChar.FileAlt;
            this.btnImeFajla.IconColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.btnImeFajla.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnImeFajla.IconSize = 18;
            this.btnImeFajla.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImeFajla.Location = new System.Drawing.Point(658, 7);
            this.btnImeFajla.Name = "btnImeFajla";
            this.btnImeFajla.Size = new System.Drawing.Size(110, 28);
            this.btnImeFajla.TabIndex = 2;
            this.btnImeFajla.Text = "Ime fajla";
            this.btnImeFajla.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImeFajla.Click += new System.EventHandler(this.btnImeFajla_Click);
            //
            // txtPutanja
            //
            this.txtPutanja.Location = new System.Drawing.Point(90, 10);
            this.txtPutanja.Name = "txtPutanja";
            this.txtPutanja.Size = new System.Drawing.Size(560, 23);
            this.txtPutanja.TabIndex = 1;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ulazni fajl:";
            //
            // ofd1
            //
            this.ofd1.FileName = "openFileDialog1";
            //
            // dataGridView1
            //
            this.dataGridView1.AccessibleName = "Table";
            this.dataGridView1.AllowFiltering = true;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dataGridView1.Location = new System.Drawing.Point(0, 108);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(900, 492);
            this.dataGridView1.TabIndex = 2;
            //
            // frmTroskoviIzracun
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.pnlFajl);
            this.Controls.Add(this.pnlDugmad);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = global::WindowsFormsApplication1.Properties.Resources.JelaIkona;
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "frmTroskoviIzracun";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Troškovni obračun";
            this.Load += new System.EventHandler(this.frmTroskoviIzracun_Load);
            this.pnlDugmad.ResumeLayout(false);
            this.pnlFajl.ResumeLayout(false);
            this.pnlFajl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlDugmad;
        private FontAwesome.Sharp.IconButton button1;
        private FontAwesome.Sharp.IconButton iconButton1;
        private FontAwesome.Sharp.IconButton btnSelektujSve;
        private System.Windows.Forms.Panel pnlFajl;
        private System.Windows.Forms.ProgressBar progressBar;
        private FontAwesome.Sharp.IconButton btnImeFajla;
        private System.Windows.Forms.TextBox txtPutanja;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog ofd1;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private Syncfusion.WinForms.DataGrid.SfDataGrid dataGridView1;
    }
}
