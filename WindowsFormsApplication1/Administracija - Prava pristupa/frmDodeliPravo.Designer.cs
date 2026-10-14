
namespace WindowsFormsApplication1.Administracija___Prava_pristupa
{
    partial class frmDodeliPravo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDodeliPravo));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.combo_Grupa = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.combo_Forma = new System.Windows.Forms.ComboBox();
            this.cb_Upis = new System.Windows.Forms.CheckBox();
            this.cb_Izmena = new System.Windows.Forms.CheckBox();
            this.cb_Brisanje = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.dataGridView2 = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(1156, 27);
            this.toolStrip1.TabIndex = 203;
            this.toolStrip1.Text = "Osveži";
            // 
            // tsNew
            // 
            this.tsNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNew.Image = ((System.Drawing.Image)(resources.GetObject("tsNew.Image")));
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(24, 24);
            this.tsNew.Text = "Novi";
            this.tsNew.Click += new System.EventHandler(this.tsNew_Click);
            // 
            // tsSave
            // 
            this.tsSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSave.Image = ((System.Drawing.Image)(resources.GetObject("tsSave.Image")));
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(24, 24);
            this.tsSave.Text = "tsSave";
            this.tsSave.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsDelete.Image")));
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(24, 24);
            this.tsDelete.Text = "toolStripButton1";
            this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // txt_ID
            // 
            this.txt_ID.Location = new System.Drawing.Point(9, 58);
            this.txt_ID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(76, 20);
            this.txt_ID.TabIndex = 209;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(9, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 17);
            this.label1.TabIndex = 208;
            this.label1.Text = "ID";
            // 
            // combo_Grupa
            // 
            this.combo_Grupa.FormattingEnabled = true;
            this.combo_Grupa.Location = new System.Drawing.Point(124, 58);
            this.combo_Grupa.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.combo_Grupa.Name = "combo_Grupa";
            this.combo_Grupa.Size = new System.Drawing.Size(175, 21);
            this.combo_Grupa.TabIndex = 211;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(121, 36);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 17);
            this.label3.TabIndex = 210;
            this.label3.Text = "Grupa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(316, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 210;
            this.label2.Text = "Forma";
            // 
            // combo_Forma
            // 
            this.combo_Forma.FormattingEnabled = true;
            this.combo_Forma.Location = new System.Drawing.Point(319, 57);
            this.combo_Forma.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.combo_Forma.Name = "combo_Forma";
            this.combo_Forma.Size = new System.Drawing.Size(242, 21);
            this.combo_Forma.TabIndex = 211;
            // 
            // cb_Upis
            // 
            this.cb_Upis.AutoSize = true;
            this.cb_Upis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Upis.ForeColor = System.Drawing.Color.Black;
            this.cb_Upis.Location = new System.Drawing.Point(580, 58);
            this.cb_Upis.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cb_Upis.Name = "cb_Upis";
            this.cb_Upis.Size = new System.Drawing.Size(55, 21);
            this.cb_Upis.TabIndex = 212;
            this.cb_Upis.Text = "Upis";
            this.cb_Upis.UseVisualStyleBackColor = true;
            // 
            // cb_Izmena
            // 
            this.cb_Izmena.AutoSize = true;
            this.cb_Izmena.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Izmena.ForeColor = System.Drawing.Color.Black;
            this.cb_Izmena.Location = new System.Drawing.Point(674, 58);
            this.cb_Izmena.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cb_Izmena.Name = "cb_Izmena";
            this.cb_Izmena.Size = new System.Drawing.Size(72, 21);
            this.cb_Izmena.TabIndex = 212;
            this.cb_Izmena.Text = "Izmena";
            this.cb_Izmena.UseVisualStyleBackColor = true;
            // 
            // cb_Brisanje
            // 
            this.cb_Brisanje.AutoSize = true;
            this.cb_Brisanje.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Brisanje.ForeColor = System.Drawing.Color.Black;
            this.cb_Brisanje.Location = new System.Drawing.Point(770, 58);
            this.cb_Brisanje.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cb_Brisanje.Name = "cb_Brisanje";
            this.cb_Brisanje.Size = new System.Drawing.Size(78, 21);
            this.cb_Brisanje.TabIndex = 212;
            this.cb_Brisanje.Text = "Brisanje";
            this.cb_Brisanje.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AccessibleName = "Table";
            this.dataGridView1.AllowFiltering = true;
            this.dataGridView1.AllowEditing = false;
            this.dataGridView1.AllowResizingColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.Location = new System.Drawing.Point(13, 42);
            this.dataGridView1.Size = new System.Drawing.Size(578, 461);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.TabIndex = 213;
            this.dataGridView1.SelectionChanged += new Syncfusion.WinForms.DataGrid.Events.SelectionChangedEventHandler(this.dataGridView1_SelectionChanged);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AccessibleName = "Table";
            this.dataGridView2.AllowFiltering = true;
            this.dataGridView2.AllowEditing = false;
            this.dataGridView2.AllowResizingColumns = true;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.Location = new System.Drawing.Point(11, 42);
            this.dataGridView2.Size = new System.Drawing.Size(489, 461);
            this.dataGridView2.SelectionMode = Syncfusion.WinForms.DataGrid.Enums.GridSelectionMode.Multiple;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.TabIndex = 214;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(8, 5);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 215;
            this.label4.Text = "Sve forme";
            // 
            // btnDodaj
            // 
            this.btnDodaj.Location = new System.Drawing.Point(305, 5);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(155, 32);
            this.btnDodaj.TabIndex = 216;
            this.btnDodaj.Text = "Dodeli forme grupi";
            this.btnDodaj.UseVisualStyleBackColor = true;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(10, 12);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 17);
            this.label5.TabIndex = 217;
            this.label5.Text = "Sve forme dodeljene grupi";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(14, 108);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView2);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.btnDodaj);
            this.splitContainer1.Size = new System.Drawing.Size(1120, 519);
            this.splitContainer1.SplitterDistance = 605;
            this.splitContainer1.TabIndex = 218;
            // 
            // frmDodeliPravo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1156, 639);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cb_Brisanje);
            this.Controls.Add(this.cb_Izmena);
            this.Controls.Add(this.cb_Upis);
            this.Controls.Add(this.combo_Forma);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.combo_Grupa);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Icon = global::WindowsFormsApplication1.Properties.Resources.JelaIkona;
            this.Name = "frmDodeliPravo";
            this.Text = "Dodela prava";
            this.Load += new System.EventHandler(this.frmDodeliPravo_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combo_Grupa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox combo_Forma;
        private System.Windows.Forms.CheckBox cb_Upis;
        private System.Windows.Forms.CheckBox cb_Izmena;
        private System.Windows.Forms.CheckBox cb_Brisanje;
        private Syncfusion.WinForms.DataGrid.SfDataGrid dataGridView1;
        private Syncfusion.WinForms.DataGrid.SfDataGrid dataGridView2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}