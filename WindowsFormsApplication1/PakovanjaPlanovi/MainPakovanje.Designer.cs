namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    partial class MainPakovanje
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPakovanje));
            this.PanelMenu = new System.Windows.Forms.Panel();
            this.btnReklamacije = new FontAwesome.Sharp.IconButton();
            this.btnNamensko = new FontAwesome.Sharp.IconButton();
            this.btnRedovno = new FontAwesome.Sharp.IconButton();
            this.btnEmp = new FontAwesome.Sharp.IconButton();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.IconButton = new System.Windows.Forms.PictureBox();
            this.PanelMenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconButton)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelMenu
            // 
            this.PanelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(54)))), ((int)(((byte)(56)))));
            this.PanelMenu.Controls.Add(this.btnReklamacije);
            this.PanelMenu.Controls.Add(this.btnNamensko);
            this.PanelMenu.Controls.Add(this.btnRedovno);
            this.PanelMenu.Controls.Add(this.btnEmp);
            this.PanelMenu.Controls.Add(this.panelLogo);
            this.PanelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelMenu.Location = new System.Drawing.Point(0, 0);
            this.PanelMenu.Name = "PanelMenu";
            this.PanelMenu.Size = new System.Drawing.Size(590, 472);
            this.PanelMenu.TabIndex = 1;
            // 
            // btnReklamacije
            // 
            this.btnReklamacije.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReklamacije.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnReklamacije.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReklamacije.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReklamacije.ForeColor = System.Drawing.Color.White;
            this.btnReklamacije.IconChar = FontAwesome.Sharp.IconChar.Warehouse;
            this.btnReklamacije.IconColor = System.Drawing.Color.White;
            this.btnReklamacije.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnReklamacije.IconSize = 24;
            this.btnReklamacije.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReklamacije.Location = new System.Drawing.Point(0, 280);
            this.btnReklamacije.Name = "btnReklamacije";
            this.btnReklamacije.Padding = new System.Windows.Forms.Padding(215, 0, 16, 0);
            this.btnReklamacije.Size = new System.Drawing.Size(590, 81);
            this.btnReklamacije.TabIndex = 17;
            this.btnReklamacije.Text = "Reklamacije";
            this.btnReklamacije.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReklamacije.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReklamacije.UseVisualStyleBackColor = true;
            this.btnReklamacije.Click += new System.EventHandler(this.btnReklamacije_Click);
            // 
            // btnNamensko
            // 
            this.btnNamensko.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNamensko.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnNamensko.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNamensko.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNamensko.ForeColor = System.Drawing.Color.White;
            this.btnNamensko.IconChar = FontAwesome.Sharp.IconChar.ListAlt;
            this.btnNamensko.IconColor = System.Drawing.Color.White;
            this.btnNamensko.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNamensko.IconSize = 24;
            this.btnNamensko.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNamensko.Location = new System.Drawing.Point(0, 207);
            this.btnNamensko.Name = "btnNamensko";
            this.btnNamensko.Padding = new System.Windows.Forms.Padding(195, 0, 16, 0);
            this.btnNamensko.Size = new System.Drawing.Size(590, 73);
            this.btnNamensko.TabIndex = 13;
            this.btnNamensko.Text = "Namensko pakovanje";
            this.btnNamensko.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNamensko.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNamensko.UseVisualStyleBackColor = true;
            this.btnNamensko.Click += new System.EventHandler(this.btnNamensko_Click);
            // 
            // btnRedovno
            // 
            this.btnRedovno.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRedovno.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.btnRedovno.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnRedovno.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRedovno.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedovno.ForeColor = System.Drawing.Color.White;
            this.btnRedovno.IconChar = FontAwesome.Sharp.IconChar.NetworkWired;
            this.btnRedovno.IconColor = System.Drawing.Color.White;
            this.btnRedovno.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRedovno.IconSize = 24;
            this.btnRedovno.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRedovno.Location = new System.Drawing.Point(0, 140);
            this.btnRedovno.Name = "btnRedovno";
            this.btnRedovno.Padding = new System.Windows.Forms.Padding(200, 0, 16, 0);
            this.btnRedovno.Size = new System.Drawing.Size(590, 67);
            this.btnRedovno.TabIndex = 5;
            this.btnRedovno.Text = "Redovno pakovanje";
            this.btnRedovno.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRedovno.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRedovno.UseVisualStyleBackColor = true;
            this.btnRedovno.Click += new System.EventHandler(this.btnRedovno_Click);
            // 
            // btnEmp
            // 
            this.btnEmp.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEmp.Enabled = false;
            this.btnEmp.FlatAppearance.BorderSize = 0;
            this.btnEmp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmp.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmp.ForeColor = System.Drawing.Color.White;
            this.btnEmp.IconChar = FontAwesome.Sharp.IconChar.FileAlt;
            this.btnEmp.IconColor = System.Drawing.Color.Transparent;
            this.btnEmp.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEmp.IconSize = 22;
            this.btnEmp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEmp.Location = new System.Drawing.Point(0, 50);
            this.btnEmp.Name = "btnEmp";
            this.btnEmp.Padding = new System.Windows.Forms.Padding(15, 0, 16, 0);
            this.btnEmp.Size = new System.Drawing.Size(590, 90);
            this.btnEmp.TabIndex = 2;
            this.btnEmp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEmp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEmp.UseVisualStyleBackColor = true;
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(83)))), ((int)(((byte)(87)))));
            this.panelLogo.Controls.Add(this.IconButton);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(590, 50);
            this.panelLogo.TabIndex = 0;
            // 
            // IconButton
            // 
            this.IconButton.Image = ((System.Drawing.Image)(resources.GetObject("IconButton.Image")));
            this.IconButton.Location = new System.Drawing.Point(36, 9);
            this.IconButton.Name = "IconButton";
            this.IconButton.Size = new System.Drawing.Size(109, 32);
            this.IconButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.IconButton.TabIndex = 1;
            this.IconButton.TabStop = false;
            // 
            // MainPakovanje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 472);
            this.Controls.Add(this.PanelMenu);
            this.Name = "MainPakovanje";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainPakovanje";
            this.PanelMenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.IconButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelMenu;
        private FontAwesome.Sharp.IconButton btnReklamacije;
        private FontAwesome.Sharp.IconButton btnNamensko;
        private FontAwesome.Sharp.IconButton btnRedovno;
        private FontAwesome.Sharp.IconButton btnEmp;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.PictureBox IconButton;
    }
}