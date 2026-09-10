using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Neto_potrebe
{
    partial class frmTehnologija1
    {
        private IContainer components = null;

        private DataGridView dataGridView1;
        private TextBox txtNaziv;
        private Button btnDodaj;
        private Button btnObrisi;
        private Label lblNaziv;

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
            this.components = new Container();
            this.dataGridView1 = new DataGridView();
            this.txtNaziv = new TextBox();
            this.btnDodaj = new Button();
            this.btnObrisi = new Button();
            this.lblNaziv = new Label();
            ((ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new Size(560, 320);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // lblNaziv
            // 
            this.lblNaziv.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.lblNaziv.AutoSize = true;
            this.lblNaziv.Location = new Point(12, 345);
            this.lblNaziv.Name = "lblNaziv";
            this.lblNaziv.Size = new Size(37, 13);
            this.lblNaziv.TabIndex = 1;
            this.lblNaziv.Text = "Naziv:";
            // 
            // txtNaziv
            // 
            this.txtNaziv.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.txtNaziv.Location = new Point(55, 342);
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new Size(355, 20);
            this.txtNaziv.TabIndex = 2;
            // 
            // btnDodaj
            // 
            this.btnDodaj.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.btnDodaj.Location = new Point(426, 340);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new Size(70, 23);
            this.btnDodaj.TabIndex = 3;
            this.btnDodaj.Text = "Dodaj";
            this.btnDodaj.UseVisualStyleBackColor = true;
            this.btnDodaj.Click += new EventHandler(this.btnDodaj_Click);
            // 
            // btnObrisi
            // 
            this.btnObrisi.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.btnObrisi.Location = new Point(502, 340);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new Size(70, 23);
            this.btnObrisi.TabIndex = 4;
            this.btnObrisi.Text = "Obriši";
            this.btnObrisi.UseVisualStyleBackColor = true;
            this.btnObrisi.Click += new EventHandler(this.btnObrisi_Click);
            // 
            // frmTehnologija1
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(584, 381);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.lblNaziv);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmTehnologija1";
            this.Text = "Tehnologija1";
            this.Load += new EventHandler(this.frmTehnologija1_Load);
            ((ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}