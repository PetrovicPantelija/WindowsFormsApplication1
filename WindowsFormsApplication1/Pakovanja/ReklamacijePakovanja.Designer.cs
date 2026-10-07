namespace WindowsFormsApplication1.Pakovanja
{
    partial class ReklamacijePakovanja
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
            this.btnLoad = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.btnPrebaci = new System.Windows.Forms.Button();
            this.btnSpakovano = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chkPocetna = new System.Windows.Forms.CheckBox();
            this.chkObradjena = new System.Windows.Forms.CheckBox();
            this.chkIsporuka = new System.Windows.Forms.CheckBox();
            this.chkZaPakovanje = new System.Windows.Forms.CheckBox();
            this.chkSpakovano = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(154, 30);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Učitaj reklamacije";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(607, 507);
            this.dataGridView1.TabIndex = 1;
            // 
            // dataGridView3
            // 
            this.dataGridView3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView3.Location = new System.Drawing.Point(3, 3);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.Size = new System.Drawing.Size(541, 507);
            this.dataGridView3.TabIndex = 2;
            // 
            // btnPrebaci
            // 
            this.btnPrebaci.Location = new System.Drawing.Point(441, 12);
            this.btnPrebaci.Name = "btnPrebaci";
            this.btnPrebaci.Size = new System.Drawing.Size(154, 30);
            this.btnPrebaci.TabIndex = 3;
            this.btnPrebaci.Text = "Za pakovanje";
            this.btnPrebaci.UseVisualStyleBackColor = true;
            this.btnPrebaci.Click += new System.EventHandler(this.btnZaPakovanje_Click);
            // 
            // btnSpakovano
            // 
            this.btnSpakovano.Location = new System.Drawing.Point(632, 12);
            this.btnSpakovano.Name = "btnSpakovano";
            this.btnSpakovano.Size = new System.Drawing.Size(162, 30);
            this.btnSpakovano.TabIndex = 4;
            this.btnSpakovano.Text = "Proglasi spakovano";
            this.btnSpakovano.UseVisualStyleBackColor = true;
            this.btnSpakovano.Click += new System.EventHandler(this.btnSpakovano_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 63);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView3);
            this.splitContainer1.Size = new System.Drawing.Size(1164, 513);
            this.splitContainer1.SplitterDistance = 613;
            this.splitContainer1.TabIndex = 5;
            //
            // chkPocetna
            //
            this.chkPocetna.AutoSize = true;
            this.chkPocetna.Checked = true;
            this.chkPocetna.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPocetna.Location = new System.Drawing.Point(180, 19);
            this.chkPocetna.Name = "chkPocetna";
            this.chkPocetna.Size = new System.Drawing.Size(64, 17);
            this.chkPocetna.TabIndex = 6;
            this.chkPocetna.Text = "Pocetna";
            this.chkPocetna.UseVisualStyleBackColor = true;
            //
            // chkObradjena
            //
            this.chkObradjena.AutoSize = true;
            this.chkObradjena.Checked = true;
            this.chkObradjena.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkObradjena.Location = new System.Drawing.Point(255, 19);
            this.chkObradjena.Name = "chkObradjena";
            this.chkObradjena.Size = new System.Drawing.Size(74, 17);
            this.chkObradjena.TabIndex = 7;
            this.chkObradjena.Text = "Obradjena";
            this.chkObradjena.UseVisualStyleBackColor = true;
            //
            // chkIsporuka
            //
            this.chkIsporuka.AutoSize = true;
            this.chkIsporuka.Location = new System.Drawing.Point(340, 19);
            this.chkIsporuka.Name = "chkIsporuka";
            this.chkIsporuka.Size = new System.Drawing.Size(66, 17);
            this.chkIsporuka.TabIndex = 8;
            this.chkIsporuka.Text = "Isporuka";
            this.chkIsporuka.UseVisualStyleBackColor = true;
            //
            // chkZaPakovanje
            //
            this.chkZaPakovanje.AutoSize = true;
            this.chkZaPakovanje.Checked = true;
            this.chkZaPakovanje.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkZaPakovanje.Location = new System.Drawing.Point(810, 19);
            this.chkZaPakovanje.Name = "chkZaPakovanje";
            this.chkZaPakovanje.Size = new System.Drawing.Size(88, 17);
            this.chkZaPakovanje.TabIndex = 9;
            this.chkZaPakovanje.Text = "Za pakovanje";
            this.chkZaPakovanje.UseVisualStyleBackColor = true;
            this.chkZaPakovanje.CheckedChanged += new System.EventHandler(this.chkPakovanjeFilter_CheckedChanged);
            //
            // chkSpakovano
            //
            this.chkSpakovano.AutoSize = true;
            this.chkSpakovano.Location = new System.Drawing.Point(910, 19);
            this.chkSpakovano.Name = "chkSpakovano";
            this.chkSpakovano.Size = new System.Drawing.Size(78, 17);
            this.chkSpakovano.TabIndex = 10;
            this.chkSpakovano.Text = "Spakovano";
            this.chkSpakovano.UseVisualStyleBackColor = true;
            this.chkSpakovano.CheckedChanged += new System.EventHandler(this.chkPakovanjeFilter_CheckedChanged);
            //
            // ReklamacijePakovanja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 588);
            this.Controls.Add(this.chkSpakovano);
            this.Controls.Add(this.chkZaPakovanje);
            this.Controls.Add(this.chkIsporuka);
            this.Controls.Add(this.chkObradjena);
            this.Controls.Add(this.chkPocetna);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnSpakovano);
            this.Controls.Add(this.btnPrebaci);
            this.Controls.Add(this.btnLoad);
            this.Icon = global::WindowsFormsApplication1.Properties.Resources.JelaIkona;
            this.Name = "ReklamacijePakovanja";
            this.Text = "ReklamacijePakovanja";
            this.Load += new System.EventHandler(this.ReklamacijePakovanja_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnZaPakovanje;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Button btnPrebaci;
        private System.Windows.Forms.Button btnSpakovano;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox chkPocetna;
        private System.Windows.Forms.CheckBox chkObradjena;
        private System.Windows.Forms.CheckBox chkIsporuka;
        private System.Windows.Forms.CheckBox chkZaPakovanje;
        private System.Windows.Forms.CheckBox chkSpakovano;
    }
}