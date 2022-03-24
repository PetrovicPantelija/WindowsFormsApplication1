
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.toolStrip1.Size = new System.Drawing.Size(1145, 27);
            this.toolStrip1.TabIndex = 203;
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
            // txt_ID
            // 
            this.txt_ID.Location = new System.Drawing.Point(12, 72);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(100, 22);
            this.txt_ID.TabIndex = 209;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 20);
            this.label1.TabIndex = 208;
            this.label1.Text = "ID";
            // 
            // combo_Grupa
            // 
            this.combo_Grupa.FormattingEnabled = true;
            this.combo_Grupa.Location = new System.Drawing.Point(165, 72);
            this.combo_Grupa.Name = "combo_Grupa";
            this.combo_Grupa.Size = new System.Drawing.Size(191, 24);
            this.combo_Grupa.TabIndex = 211;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(161, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 210;
            this.label3.Text = "Grupa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(391, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 20);
            this.label2.TabIndex = 210;
            this.label2.Text = "Forma";
            // 
            // combo_Forma
            // 
            this.combo_Forma.FormattingEnabled = true;
            this.combo_Forma.Location = new System.Drawing.Point(395, 72);
            this.combo_Forma.Name = "combo_Forma";
            this.combo_Forma.Size = new System.Drawing.Size(204, 24);
            this.combo_Forma.TabIndex = 211;
            // 
            // cb_Upis
            // 
            this.cb_Upis.AutoSize = true;
            this.cb_Upis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Upis.ForeColor = System.Drawing.Color.White;
            this.cb_Upis.Location = new System.Drawing.Point(631, 72);
            this.cb_Upis.Name = "cb_Upis";
            this.cb_Upis.Size = new System.Drawing.Size(69, 24);
            this.cb_Upis.TabIndex = 212;
            this.cb_Upis.Text = "Upis";
            this.cb_Upis.UseVisualStyleBackColor = true;
            // 
            // cb_Izmena
            // 
            this.cb_Izmena.AutoSize = true;
            this.cb_Izmena.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Izmena.ForeColor = System.Drawing.Color.White;
            this.cb_Izmena.Location = new System.Drawing.Point(757, 72);
            this.cb_Izmena.Name = "cb_Izmena";
            this.cb_Izmena.Size = new System.Drawing.Size(91, 24);
            this.cb_Izmena.TabIndex = 212;
            this.cb_Izmena.Text = "Izmena";
            this.cb_Izmena.UseVisualStyleBackColor = true;
            // 
            // cb_Brisanje
            // 
            this.cb_Brisanje.AutoSize = true;
            this.cb_Brisanje.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Brisanje.ForeColor = System.Drawing.Color.White;
            this.cb_Brisanje.Location = new System.Drawing.Point(885, 72);
            this.cb_Brisanje.Name = "cb_Brisanje";
            this.cb_Brisanje.Size = new System.Drawing.Size(101, 24);
            this.cb_Brisanje.TabIndex = 212;
            this.cb_Brisanje.Text = "Brisanje";
            this.cb_Brisanje.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(14, 120);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1119, 435);
            this.dataGridView1.TabIndex = 213;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // frmDodeliPravo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(83)))), ((int)(((byte)(87)))));
            this.ClientSize = new System.Drawing.Size(1145, 567);
            this.Controls.Add(this.dataGridView1);
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
            this.Name = "frmDodeliPravo";
            this.Text = "frmDodeliPravo";
            this.Load += new System.EventHandler(this.frmDodeliPravo_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}