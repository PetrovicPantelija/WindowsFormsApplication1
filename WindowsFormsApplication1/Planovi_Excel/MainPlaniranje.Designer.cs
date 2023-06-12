
namespace WindowsFormsApplication1.Planovi_Excel
{
    partial class MainPlaniranje
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPlaniranje));
            this.panel2 = new System.Windows.Forms.Panel();
            this.IconButton = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSifarnici = new FontAwesome.Sharp.IconButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Kalkulator = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconButton)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(83)))), ((int)(((byte)(87)))));
            this.panel2.Controls.Add(this.IconButton);
            this.panel2.Location = new System.Drawing.Point(11, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(751, 55);
            this.panel2.TabIndex = 4;
            // 
            // IconButton
            // 
            this.IconButton.Image = ((System.Drawing.Image)(resources.GetObject("IconButton.Image")));
            this.IconButton.Location = new System.Drawing.Point(319, 11);
            this.IconButton.Name = "IconButton";
            this.IconButton.Size = new System.Drawing.Size(114, 37);
            this.IconButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.IconButton.TabIndex = 2;
            this.IconButton.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(83)))), ((int)(((byte)(87)))));
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnSifarnici);
            this.panel1.Location = new System.Drawing.Point(11, 61);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(236, 280);
            this.panel1.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(7, 160);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 19);
            this.label6.TabIndex = 22;
            this.label6.Text = "• Izlaznost";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(7, 130);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 19);
            this.label5.TabIndex = 21;
            this.label5.Text = "• Podešavanje";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(7, 99);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 19);
            this.label4.TabIndex = 20;
            this.label4.Text = "• Pripadnost";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(7, 68);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 19);
            this.label8.TabIndex = 19;
            this.label8.Text = "• Programi";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(7, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 19);
            this.label2.TabIndex = 18;
            this.label2.Text = "• Kolekcije";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnSifarnici
            // 
            this.btnSifarnici.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSifarnici.FlatAppearance.BorderSize = 0;
            this.btnSifarnici.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSifarnici.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSifarnici.ForeColor = System.Drawing.Color.White;
            this.btnSifarnici.IconChar = FontAwesome.Sharp.IconChar.Archive;
            this.btnSifarnici.IconColor = System.Drawing.Color.White;
            this.btnSifarnici.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSifarnici.IconSize = 24;
            this.btnSifarnici.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSifarnici.Location = new System.Drawing.Point(0, 0);
            this.btnSifarnici.Name = "btnSifarnici";
            this.btnSifarnici.Padding = new System.Windows.Forms.Padding(15, 0, 16, 0);
            this.btnSifarnici.Size = new System.Drawing.Size(236, 34);
            this.btnSifarnici.TabIndex = 16;
            this.btnSifarnici.Text = "Šifarnici";
            this.btnSifarnici.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSifarnici.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSifarnici.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(83)))), ((int)(((byte)(87)))));
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.btn_Kalkulator);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(265, 61);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(236, 280);
            this.panel3.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(12, 99);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 19);
            this.label7.TabIndex = 22;
            this.label7.Text = "• Plan detaljno";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 68);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 19);
            this.label3.TabIndex = 21;
            this.label3.Text = "• Prodaja neisporuceni";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // btn_Kalkulator
            // 
            this.btn_Kalkulator.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Kalkulator.FlatAppearance.BorderSize = 0;
            this.btn_Kalkulator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Kalkulator.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Kalkulator.ForeColor = System.Drawing.Color.White;
            this.btn_Kalkulator.IconChar = FontAwesome.Sharp.IconChar.Calculator;
            this.btn_Kalkulator.IconColor = System.Drawing.Color.White;
            this.btn_Kalkulator.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_Kalkulator.IconSize = 24;
            this.btn_Kalkulator.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Kalkulator.Location = new System.Drawing.Point(0, 0);
            this.btn_Kalkulator.Name = "btn_Kalkulator";
            this.btn_Kalkulator.Padding = new System.Windows.Forms.Padding(15, 0, 16, 0);
            this.btn_Kalkulator.Size = new System.Drawing.Size(236, 34);
            this.btn_Kalkulator.TabIndex = 20;
            this.btn_Kalkulator.Text = "PLaner";
            this.btn_Kalkulator.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Kalkulator.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_Kalkulator.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 19);
            this.label1.TabIndex = 19;
            this.label1.Text = "• Plan aktiviranje";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // MainPlaniranje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(83)))), ((int)(((byte)(87)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "MainPlaniranje";
            this.Text = "MainPlaniranje";
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.IconButton)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox IconButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private FontAwesome.Sharp.IconButton btnSifarnici;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private FontAwesome.Sharp.IconButton btn_Kalkulator;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
    }
}