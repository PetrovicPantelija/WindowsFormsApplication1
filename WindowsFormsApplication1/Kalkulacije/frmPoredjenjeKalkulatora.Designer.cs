
namespace WindowsFormsApplication1.Kalkulacije
{
    partial class frmPoredjenjeKalkulatora
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
            this.combo_Zaglavlje = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_Rabat = new System.Windows.Forms.Button();
            this.combo_Zaglavlje1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // combo_Zaglavlje
            // 
            this.combo_Zaglavlje.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.combo_Zaglavlje.FormattingEnabled = true;
            this.combo_Zaglavlje.Location = new System.Drawing.Point(193, 29);
            this.combo_Zaglavlje.Margin = new System.Windows.Forms.Padding(2);
            this.combo_Zaglavlje.Name = "combo_Zaglavlje";
            this.combo_Zaglavlje.Size = new System.Drawing.Size(164, 21);
            this.combo_Zaglavlje.TabIndex = 205;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 204;
            this.label3.Text = "Uporedi sa";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(400, 54);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(716, 383);
            this.dataGridView1.TabIndex = 206;
            // 
            // btn_Rabat
            // 
            this.btn_Rabat.Location = new System.Drawing.Point(400, 27);
            this.btn_Rabat.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Rabat.Name = "btn_Rabat";
            this.btn_Rabat.Size = new System.Drawing.Size(191, 23);
            this.btn_Rabat.TabIndex = 207;
            this.btn_Rabat.Text = "Poredi sve";
            this.btn_Rabat.UseVisualStyleBackColor = true;
            this.btn_Rabat.Click += new System.EventHandler(this.btn_Rabat_Click);
            // 
            // combo_Zaglavlje1
            // 
            this.combo_Zaglavlje1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.combo_Zaglavlje1.FormattingEnabled = true;
            this.combo_Zaglavlje1.Location = new System.Drawing.Point(11, 29);
            this.combo_Zaglavlje1.Margin = new System.Windows.Forms.Padding(2);
            this.combo_Zaglavlje1.Name = "combo_Zaglavlje1";
            this.combo_Zaglavlje1.Size = new System.Drawing.Size(164, 21);
            this.combo_Zaglavlje1.TabIndex = 208;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 209;
            this.label1.Text = "Kalkulator";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(193, 54);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(166, 360);
            this.dataGridView2.TabIndex = 210;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(364, 54);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 360);
            this.button2.TabIndex = 212;
            this.button2.Text = "POREDI";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(8, 54);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.Size = new System.Drawing.Size(181, 360);
            this.dataGridView3.TabIndex = 211;
            // 
            // frmPoredjenjeKalkulatora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1127, 450);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combo_Zaglavlje1);
            this.Controls.Add(this.btn_Rabat);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.combo_Zaglavlje);
            this.Controls.Add(this.label3);
            this.Icon = global::WindowsFormsApplication1.Properties.Resources.JelaIkona;
            this.Name = "frmPoredjenjeKalkulatora";
            this.Text = "Poredjenje kalkulacije";
            this.Load += new System.EventHandler(this.frmPoredjenjeKalkulatora_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox combo_Zaglavlje;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_Rabat;
        private System.Windows.Forms.ComboBox combo_Zaglavlje1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView3;
    }
}