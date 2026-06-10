namespace WindowsFormsApplication1.Neto_potrebe
{
    partial class KrojnaLista
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboPlan = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUbaci = new FontAwesome.Sharp.IconButton();
            this.button1 = new FontAwesome.Sharp.IconButton();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPodPlan = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.txtKrojna = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDPlana = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PodPlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDNadredjena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDZapisa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StaraSIf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(550, 503);
            this.panel1.TabIndex = 0;
            // 
            // cboPlan
            // 
            this.cboPlan.FormattingEnabled = true;
            this.cboPlan.Location = new System.Drawing.Point(12, 25);
            this.cboPlan.Name = "cboPlan";
            this.cboPlan.Size = new System.Drawing.Size(278, 21);
            this.cboPlan.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Plan";
            // 
            // btnUbaci
            // 
            this.btnUbaci.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.btnUbaci.FlatAppearance.BorderSize = 0;
            this.btnUbaci.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUbaci.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnUbaci.IconChar = FontAwesome.Sharp.IconChar.Random;
            this.btnUbaci.IconColor = System.Drawing.Color.Black;
            this.btnUbaci.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnUbaci.IconSize = 90;
            this.btnUbaci.Location = new System.Drawing.Point(568, 55);
            this.btnUbaci.Name = "btnUbaci";
            this.btnUbaci.Size = new System.Drawing.Size(74, 503);
            this.btnUbaci.TabIndex = 289;
            this.btnUbaci.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUbaci.UseVisualStyleBackColor = false;
            this.btnUbaci.Click += new System.EventHandler(this.btnUbaci_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.button1.IconChar = FontAwesome.Sharp.IconChar.ExchangeAlt;
            this.button1.IconColor = System.Drawing.Color.Black;
            this.button1.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.button1.IconSize = 22;
            this.button1.Location = new System.Drawing.Point(1070, 39);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.button1.Size = new System.Drawing.Size(294, 25);
            this.button1.TabIndex = 292;
            this.button1.Text = "Formiraj krojnu listu";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(293, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 294;
            this.label10.Text = "Podplan";
            // 
            // txtPodPlan
            // 
            this.txtPodPlan.Location = new System.Drawing.Point(296, 25);
            this.txtPodPlan.Name = "txtPodPlan";
            this.txtPodPlan.Size = new System.Drawing.Size(173, 20);
            this.txtPodPlan.TabIndex = 293;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(24, 69);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(526, 477);
            this.dataGridView1.TabIndex = 3;
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.IDPlana,
            this.PodPlan,
            this.IDNadredjena,
            this.IDZapisa,
            this.MP,
            this.StaraSIf,
            this.Kolicina});
            this.dataGridView2.Location = new System.Drawing.Point(657, 69);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(898, 489);
            this.dataGridView2.TabIndex = 296;
            // 
            // txtKrojna
            // 
            this.txtKrojna.Location = new System.Drawing.Point(757, 43);
            this.txtKrojna.Name = "txtKrojna";
            this.txtKrojna.Size = new System.Drawing.Size(291, 20);
            this.txtKrojna.TabIndex = 297;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(665, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 298;
            this.label2.Text = "Krojna lista naziv";
            // 
            // iconButton1
            // 
            this.iconButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(142)))), ((int)(((byte)(4)))));
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.ExchangeAlt;
            this.iconButton1.IconColor = System.Drawing.Color.Black;
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.iconButton1.IconSize = 22;
            this.iconButton1.Location = new System.Drawing.Point(475, 20);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.iconButton1.Size = new System.Drawing.Size(52, 25);
            this.iconButton1.TabIndex = 299;
            this.iconButton1.Text = "N";
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton1.UseVisualStyleBackColor = false;
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            // 
            // IDPlana
            // 
            this.IDPlana.HeaderText = "IDPlana";
            this.IDPlana.Name = "IDPlana";
            // 
            // PodPlan
            // 
            this.PodPlan.HeaderText = "PodPlan";
            this.PodPlan.Name = "PodPlan";
            // 
            // IDNadredjena
            // 
            this.IDNadredjena.HeaderText = "IDNAdredjena";
            this.IDNadredjena.Name = "IDNadredjena";
            // 
            // IDZapisa
            // 
            this.IDZapisa.HeaderText = "IDZapisa";
            this.IDZapisa.Name = "IDZapisa";
            // 
            // MP
            // 
            this.MP.HeaderText = "MP";
            this.MP.Name = "MP";
            // 
            // StaraSIf
            // 
            this.StaraSIf.HeaderText = "StaraSif";
            this.StaraSIf.Name = "StaraSIf";
            // 
            // Kolicina
            // 
            this.Kolicina.HeaderText = "Kolicina";
            this.Kolicina.Name = "Kolicina";
            // 
            // KrojnaLista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1579, 597);
            this.Controls.Add(this.iconButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKrojna);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtPodPlan);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnUbaci);
            this.Controls.Add(this.cboPlan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "KrojnaLista";
            this.Text = "KrojnaLista";
            this.Load += new System.EventHandler(this.KrojnaLista_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboPlan;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton btnUbaci;
        private FontAwesome.Sharp.IconButton button1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPodPlan;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox txtKrojna;
        private System.Windows.Forms.Label label2;
        private FontAwesome.Sharp.IconButton iconButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDPlana;
        private System.Windows.Forms.DataGridViewTextBoxColumn PodPlan;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDNadredjena;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDZapisa;
        private System.Windows.Forms.DataGridViewTextBoxColumn MP;
        private System.Windows.Forms.DataGridViewTextBoxColumn StaraSIf;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kolicina;
    }
}