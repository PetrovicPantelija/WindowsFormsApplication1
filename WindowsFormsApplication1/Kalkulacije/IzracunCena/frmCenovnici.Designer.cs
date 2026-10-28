namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    partial class frmCenovnici
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlDugmad = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPrikaz = new System.Windows.Forms.Label();
            this.cboPrikaz = new System.Windows.Forms.ComboBox();
            this.btnOsvezi = new System.Windows.Forms.Button();
            this.btnOtvori = new System.Windows.Forms.Button();
            this.btnIzvoz = new System.Windows.Forms.Button();
            this.btnKopiraj = new System.Windows.Forms.Button();
            this.grid = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.pnlDugmad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            //
            // pnlDugmad
            //
            this.pnlDugmad.BackColor = System.Drawing.Color.FromArgb(248, 247, 244);
            this.pnlDugmad.Controls.Add(this.lblPrikaz);
            this.pnlDugmad.Controls.Add(this.cboPrikaz);
            this.pnlDugmad.Controls.Add(this.btnOsvezi);
            this.pnlDugmad.Controls.Add(this.btnOtvori);
            this.pnlDugmad.Controls.Add(this.btnIzvoz);
            this.pnlDugmad.Controls.Add(this.btnKopiraj);
            this.pnlDugmad.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDugmad.Location = new System.Drawing.Point(0, 0);
            this.pnlDugmad.Name = "pnlDugmad";
            this.pnlDugmad.Padding = new System.Windows.Forms.Padding(8, 6, 8, 4);
            this.pnlDugmad.Size = new System.Drawing.Size(1100, 46);
            this.pnlDugmad.TabIndex = 0;
            //
            // lblPrikaz
            //
            this.lblPrikaz.AutoSize = true;
            this.lblPrikaz.Margin = new System.Windows.Forms.Padding(3, 10, 0, 0);
            this.lblPrikaz.Name = "lblPrikaz";
            this.lblPrikaz.Text = "Prikaži:";
            //
            // cboPrikaz
            //
            this.cboPrikaz.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrikaz.Items.AddRange(new object[] { "Važeći cenovnici", "Svi cenovnici" });
            this.cboPrikaz.Margin = new System.Windows.Forms.Padding(4, 6, 3, 3);
            this.cboPrikaz.Name = "cboPrikaz";
            this.cboPrikaz.Size = new System.Drawing.Size(150, 23);
            this.cboPrikaz.TabIndex = 0;
            this.cboPrikaz.SelectedIndexChanged += new System.EventHandler(this.cboPrikaz_SelectedIndexChanged);
            //
            // btnOsvezi
            //
            this.btnOsvezi.Name = "btnOsvezi";
            this.btnOsvezi.Size = new System.Drawing.Size(80, 30);
            this.btnOsvezi.TabIndex = 1;
            this.btnOsvezi.Text = "Osveži";
            this.btnOsvezi.Click += new System.EventHandler(this.btnOsvezi_Click);
            //
            // btnOtvori
            //
            this.btnOtvori.Margin = new System.Windows.Forms.Padding(18, 3, 3, 3);
            this.btnOtvori.Name = "btnOtvori";
            this.btnOtvori.Size = new System.Drawing.Size(90, 30);
            this.btnOtvori.TabIndex = 2;
            this.btnOtvori.Tag = "glavno";
            this.btnOtvori.Text = "Otvori";
            this.btnOtvori.Click += new System.EventHandler(this.btnOtvori_Click);
            //
            // btnIzvoz
            //
            this.btnIzvoz.Name = "btnIzvoz";
            this.btnIzvoz.Size = new System.Drawing.Size(110, 30);
            this.btnIzvoz.TabIndex = 3;
            this.btnIzvoz.Text = "Izvoz u Excel";
            this.btnIzvoz.Click += new System.EventHandler(this.btnIzvoz_Click);
            //
            // btnKopiraj
            //
            this.btnKopiraj.Name = "btnKopiraj";
            this.btnKopiraj.Size = new System.Drawing.Size(190, 30);
            this.btnKopiraj.TabIndex = 4;
            this.btnKopiraj.Text = "Kopiraj u novu radnu kopiju";
            this.btnKopiraj.Click += new System.EventHandler(this.btnKopiraj_Click);
            //
            // grid
            //
            this.grid.AccessibleName = "Table";
            this.grid.AllowEditing = false;
            this.grid.AllowFiltering = true;
            this.grid.AutoGenerateColumns = false;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 46);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(1100, 514);
            this.grid.TabIndex = 1;
            this.grid.CellDoubleClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.grid_CellDoubleClick);
            //
            // frmCenovnici
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 560);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.pnlDugmad);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmCenovnici";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pregled cenovnika";
            this.pnlDugmad.ResumeLayout(false);
            this.pnlDugmad.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlDugmad;
        private System.Windows.Forms.Label lblPrikaz;
        private System.Windows.Forms.ComboBox cboPrikaz;
        private System.Windows.Forms.Button btnOsvezi;
        private System.Windows.Forms.Button btnOtvori;
        private System.Windows.Forms.Button btnIzvoz;
        private System.Windows.Forms.Button btnKopiraj;
        private Syncfusion.WinForms.DataGrid.SfDataGrid grid;
    }
}
