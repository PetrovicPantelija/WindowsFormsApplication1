namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    partial class ucCenovnik
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitLevo = new System.Windows.Forms.SplitContainer();
            this.trvProgrami = new System.Windows.Forms.TreeView();
            this.pnlStabloDugmad = new System.Windows.Forms.Panel();
            this.btnNista = new System.Windows.Forms.Button();
            this.btnSve = new System.Windows.Forms.Button();
            this.lblStablo = new System.Windows.Forms.Label();
            this.splitDesno = new System.Windows.Forms.SplitContainer();
            this.grid = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.cmsGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmDodeliSliku = new System.Windows.Forms.ToolStripMenuItem();
            this.picSlika = new System.Windows.Forms.PictureBox();
            this.lblSlika = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitLevo)).BeginInit();
            this.splitLevo.Panel1.SuspendLayout();
            this.splitLevo.Panel2.SuspendLayout();
            this.splitLevo.SuspendLayout();
            this.pnlStabloDugmad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitDesno)).BeginInit();
            this.splitDesno.Panel1.SuspendLayout();
            this.splitDesno.Panel2.SuspendLayout();
            this.splitDesno.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.cmsGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSlika)).BeginInit();
            this.SuspendLayout();
            //
            // splitLevo
            //
            this.splitLevo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitLevo.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitLevo.Location = new System.Drawing.Point(0, 0);
            this.splitLevo.Name = "splitLevo";
            //
            // splitLevo.Panel1
            //
            this.splitLevo.Panel1.Controls.Add(this.trvProgrami);
            this.splitLevo.Panel1.Controls.Add(this.pnlStabloDugmad);
            this.splitLevo.Panel1.Controls.Add(this.lblStablo);
            //
            // splitLevo.Panel2
            //
            this.splitLevo.Panel2.Controls.Add(this.splitDesno);
            this.splitLevo.Size = new System.Drawing.Size(1200, 600);
            this.splitLevo.SplitterDistance = 240;
            this.splitLevo.TabIndex = 0;
            //
            // trvProgrami
            //
            this.trvProgrami.BackColor = System.Drawing.Color.FromArgb(248, 247, 244);
            this.trvProgrami.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvProgrami.CheckBoxes = true;
            this.trvProgrami.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvProgrami.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.trvProgrami.HideSelection = false;
            this.trvProgrami.Location = new System.Drawing.Point(0, 26);
            this.trvProgrami.Name = "trvProgrami";
            this.trvProgrami.Size = new System.Drawing.Size(240, 538);
            this.trvProgrami.TabIndex = 1;
            this.trvProgrami.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvProgrami_AfterCheck);
            //
            // pnlStabloDugmad
            //
            this.pnlStabloDugmad.Controls.Add(this.btnNista);
            this.pnlStabloDugmad.Controls.Add(this.btnSve);
            this.pnlStabloDugmad.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStabloDugmad.Location = new System.Drawing.Point(0, 564);
            this.pnlStabloDugmad.Name = "pnlStabloDugmad";
            this.pnlStabloDugmad.Size = new System.Drawing.Size(240, 36);
            this.pnlStabloDugmad.TabIndex = 2;
            //
            // btnNista
            //
            this.btnNista.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNista.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNista.Location = new System.Drawing.Point(84, 5);
            this.btnNista.Name = "btnNista";
            this.btnNista.Size = new System.Drawing.Size(72, 26);
            this.btnNista.TabIndex = 1;
            this.btnNista.Text = "Ništa";
            this.btnNista.UseVisualStyleBackColor = true;
            this.btnNista.Click += new System.EventHandler(this.btnNista_Click);
            //
            // btnSve
            //
            this.btnSve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSve.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSve.Location = new System.Drawing.Point(6, 5);
            this.btnSve.Name = "btnSve";
            this.btnSve.Size = new System.Drawing.Size(72, 26);
            this.btnSve.TabIndex = 0;
            this.btnSve.Text = "Sve";
            this.btnSve.UseVisualStyleBackColor = true;
            this.btnSve.Click += new System.EventHandler(this.btnSve_Click);
            //
            // lblStablo
            //
            this.lblStablo.BackColor = System.Drawing.Color.FromArgb(58, 77, 57);
            this.lblStablo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStablo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.lblStablo.ForeColor = System.Drawing.Color.White;
            this.lblStablo.Location = new System.Drawing.Point(0, 0);
            this.lblStablo.Name = "lblStablo";
            this.lblStablo.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblStablo.Size = new System.Drawing.Size(240, 26);
            this.lblStablo.TabIndex = 0;
            this.lblStablo.Text = "Program / Kolekcija";
            this.lblStablo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // splitDesno
            //
            this.splitDesno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitDesno.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitDesno.Location = new System.Drawing.Point(0, 0);
            this.splitDesno.Name = "splitDesno";
            //
            // splitDesno.Panel1
            //
            this.splitDesno.Panel1.Controls.Add(this.grid);
            //
            // splitDesno.Panel2
            //
            this.splitDesno.Panel2.BackColor = System.Drawing.Color.FromArgb(248, 247, 244);
            this.splitDesno.Panel2.Controls.Add(this.picSlika);
            this.splitDesno.Panel2.Controls.Add(this.lblSlika);
            this.splitDesno.Size = new System.Drawing.Size(956, 600);
            this.splitDesno.SplitterDistance = 726;
            this.splitDesno.TabIndex = 0;
            //
            // grid
            //
            this.grid.AccessibleName = "Table";
            this.grid.AllowFiltering = true;
            this.grid.AutoGenerateColumns = false;
            this.grid.ContextMenuStrip = this.cmsGrid;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(726, 600);
            this.grid.TabIndex = 0;
            //
            // cmsGrid
            //
            this.cmsGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmDodeliSliku});
            this.cmsGrid.Name = "cmsGrid";
            this.cmsGrid.Size = new System.Drawing.Size(160, 26);
            //
            // tsmDodeliSliku
            //
            this.tsmDodeliSliku.Name = "tsmDodeliSliku";
            this.tsmDodeliSliku.Size = new System.Drawing.Size(159, 22);
            this.tsmDodeliSliku.Text = "Dodeli sliku...";
            this.tsmDodeliSliku.Click += new System.EventHandler(this.tsmDodeliSliku_Click);
            //
            // picSlika
            //
            this.picSlika.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picSlika.Location = new System.Drawing.Point(0, 26);
            this.picSlika.Name = "picSlika";
            this.picSlika.Size = new System.Drawing.Size(226, 574);
            this.picSlika.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSlika.TabIndex = 1;
            this.picSlika.TabStop = false;
            //
            // lblSlika
            //
            this.lblSlika.BackColor = System.Drawing.Color.FromArgb(58, 77, 57);
            this.lblSlika.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSlika.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.lblSlika.ForeColor = System.Drawing.Color.White;
            this.lblSlika.Location = new System.Drawing.Point(0, 0);
            this.lblSlika.Name = "lblSlika";
            this.lblSlika.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblSlika.Size = new System.Drawing.Size(226, 26);
            this.lblSlika.TabIndex = 0;
            this.lblSlika.Text = "Slika";
            this.lblSlika.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // ucCenovnik
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitLevo);
            this.Name = "ucCenovnik";
            this.Size = new System.Drawing.Size(1200, 600);
            this.splitLevo.Panel1.ResumeLayout(false);
            this.splitLevo.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitLevo)).EndInit();
            this.splitLevo.ResumeLayout(false);
            this.pnlStabloDugmad.ResumeLayout(false);
            this.splitDesno.Panel1.ResumeLayout(false);
            this.splitDesno.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitDesno)).EndInit();
            this.splitDesno.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.cmsGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSlika)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitLevo;
        private System.Windows.Forms.TreeView trvProgrami;
        private System.Windows.Forms.Panel pnlStabloDugmad;
        private System.Windows.Forms.Button btnNista;
        private System.Windows.Forms.Button btnSve;
        private System.Windows.Forms.Label lblStablo;
        private System.Windows.Forms.SplitContainer splitDesno;
        private Syncfusion.WinForms.DataGrid.SfDataGrid grid;
        private System.Windows.Forms.ContextMenuStrip cmsGrid;
        private System.Windows.Forms.ToolStripMenuItem tsmDodeliSliku;
        private System.Windows.Forms.PictureBox picSlika;
        private System.Windows.Forms.Label lblSlika;
    }
}
