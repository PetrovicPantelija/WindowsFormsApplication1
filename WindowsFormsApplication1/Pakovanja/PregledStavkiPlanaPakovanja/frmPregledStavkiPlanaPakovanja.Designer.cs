
namespace WindowsFormsApplication1.Pakovanja.PregledStavkiPlanaPakovanja
{
    partial class frmPregledStavkiPlanaPakovanja
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
            this.btnBack = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.groupBoxAddEdit = new System.Windows.Forms.GroupBox();
            this.txt_ukucati_količinu_za_rezervaciju = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_rezervisana_količina_po_planu = new System.Windows.Forms.TextBox();
            this.txt_trenutna_količina_na_zalihama = new System.Windows.Forms.TextBox();
            this.txt_potrebna_količina = new System.Windows.Forms.TextBox();
            this.txt_artikal_iz_prvog_nivoa_sastavnice = new System.Windows.Forms.TextBox();
            this.txt_količina_za_proizvodnju = new System.Windows.Forms.TextBox();
            this.txt_artikal_paket = new System.Windows.Forms.TextBox();
            this.txt_status_stavke = new System.Windows.Forms.TextBox();
            this.txt_rn = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_id_stavke_plana_pakovanja = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.groupBoxAddEdit.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.SlateGray;
            this.btnBack.Location = new System.Drawing.Point(11, 12);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 24);
            this.btnBack.TabIndex = 72;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(282, 12);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 24);
            this.btnEdit.TabIndex = 70;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.IndianRed;
            this.btnDelete.Location = new System.Drawing.Point(362, 12);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 24);
            this.btnDelete.TabIndex = 69;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(482, 12);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 24);
            this.btnSearch.TabIndex = 67;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(158)))), ((int)(((byte)(192)))));
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(11, 41);
            this.dataGrid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersWidth = 51;
            this.dataGrid.RowTemplate.Height = 24;
            this.dataGrid.Size = new System.Drawing.Size(1155, 203);
            this.dataGrid.TabIndex = 66;
            // 
            // groupBoxAddEdit
            // 
            this.groupBoxAddEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAddEdit.Controls.Add(this.txt_ukucati_količinu_za_rezervaciju);
            this.groupBoxAddEdit.Controls.Add(this.label1);
            this.groupBoxAddEdit.Controls.Add(this.txt_rezervisana_količina_po_planu);
            this.groupBoxAddEdit.Controls.Add(this.txt_trenutna_količina_na_zalihama);
            this.groupBoxAddEdit.Controls.Add(this.txt_potrebna_količina);
            this.groupBoxAddEdit.Controls.Add(this.txt_artikal_iz_prvog_nivoa_sastavnice);
            this.groupBoxAddEdit.Controls.Add(this.txt_količina_za_proizvodnju);
            this.groupBoxAddEdit.Controls.Add(this.txt_artikal_paket);
            this.groupBoxAddEdit.Controls.Add(this.txt_status_stavke);
            this.groupBoxAddEdit.Controls.Add(this.txt_rn);
            this.groupBoxAddEdit.Controls.Add(this.label14);
            this.groupBoxAddEdit.Controls.Add(this.label13);
            this.groupBoxAddEdit.Controls.Add(this.label12);
            this.groupBoxAddEdit.Controls.Add(this.label10);
            this.groupBoxAddEdit.Controls.Add(this.label9);
            this.groupBoxAddEdit.Controls.Add(this.txt_id_stavke_plana_pakovanja);
            this.groupBoxAddEdit.Controls.Add(this.label4);
            this.groupBoxAddEdit.Controls.Add(this.label6);
            this.groupBoxAddEdit.Controls.Add(this.label7);
            this.groupBoxAddEdit.Controls.Add(this.label8);
            this.groupBoxAddEdit.Controls.Add(this.btnCancel);
            this.groupBoxAddEdit.Controls.Add(this.btnUpdate);
            this.groupBoxAddEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAddEdit.Location = new System.Drawing.Point(11, 248);
            this.groupBoxAddEdit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBoxAddEdit.Name = "groupBoxAddEdit";
            this.groupBoxAddEdit.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBoxAddEdit.Size = new System.Drawing.Size(682, 238);
            this.groupBoxAddEdit.TabIndex = 74;
            this.groupBoxAddEdit.TabStop = false;
            this.groupBoxAddEdit.Text = "Izmeni Stavke Za Pregled Plana Pakovanja";
            // 
            // txt_ukucati_količinu_za_rezervaciju
            // 
            this.txt_ukucati_količinu_za_rezervaciju.Location = new System.Drawing.Point(518, 158);
            this.txt_ukucati_količinu_za_rezervaciju.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_ukucati_količinu_za_rezervaciju.Multiline = true;
            this.txt_ukucati_količinu_za_rezervaciju.Name = "txt_ukucati_količinu_za_rezervaciju";
            this.txt_ukucati_količinu_za_rezervaciju.Size = new System.Drawing.Size(156, 24);
            this.txt_ukucati_količinu_za_rezervaciju.TabIndex = 90;
            this.txt_ukucati_količinu_za_rezervaciju.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_ukucati_količinu_za_rezervaciju_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(335, 160);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 15);
            this.label1.TabIndex = 91;
            this.label1.Text = "Ukucati Količinu Za Rezervaciju";
            // 
            // txt_rezervisana_količina_po_planu
            // 
            this.txt_rezervisana_količina_po_planu.Location = new System.Drawing.Point(518, 128);
            this.txt_rezervisana_količina_po_planu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_rezervisana_količina_po_planu.Multiline = true;
            this.txt_rezervisana_količina_po_planu.Name = "txt_rezervisana_količina_po_planu";
            this.txt_rezervisana_količina_po_planu.Size = new System.Drawing.Size(156, 24);
            this.txt_rezervisana_količina_po_planu.TabIndex = 89;
            this.txt_rezervisana_količina_po_planu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_rezervisana_količina_po_planu_KeyPress);
            // 
            // txt_trenutna_količina_na_zalihama
            // 
            this.txt_trenutna_količina_na_zalihama.Location = new System.Drawing.Point(518, 98);
            this.txt_trenutna_količina_na_zalihama.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_trenutna_količina_na_zalihama.Multiline = true;
            this.txt_trenutna_količina_na_zalihama.Name = "txt_trenutna_količina_na_zalihama";
            this.txt_trenutna_količina_na_zalihama.Size = new System.Drawing.Size(156, 24);
            this.txt_trenutna_količina_na_zalihama.TabIndex = 88;
            this.txt_trenutna_količina_na_zalihama.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_trenutna_količina_na_zalihama_KeyPress);
            // 
            // txt_potrebna_količina
            // 
            this.txt_potrebna_količina.Location = new System.Drawing.Point(518, 67);
            this.txt_potrebna_količina.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_potrebna_količina.Multiline = true;
            this.txt_potrebna_količina.Name = "txt_potrebna_količina";
            this.txt_potrebna_količina.Size = new System.Drawing.Size(156, 24);
            this.txt_potrebna_količina.TabIndex = 86;
            this.txt_potrebna_količina.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_potrebna_količina_KeyPress);
            // 
            // txt_artikal_iz_prvog_nivoa_sastavnice
            // 
            this.txt_artikal_iz_prvog_nivoa_sastavnice.Location = new System.Drawing.Point(518, 36);
            this.txt_artikal_iz_prvog_nivoa_sastavnice.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_artikal_iz_prvog_nivoa_sastavnice.Multiline = true;
            this.txt_artikal_iz_prvog_nivoa_sastavnice.Name = "txt_artikal_iz_prvog_nivoa_sastavnice";
            this.txt_artikal_iz_prvog_nivoa_sastavnice.Size = new System.Drawing.Size(156, 24);
            this.txt_artikal_iz_prvog_nivoa_sastavnice.TabIndex = 87;
            this.txt_artikal_iz_prvog_nivoa_sastavnice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_artikal_iz_prvog_nivoa_sastavnice_KeyPress);
            // 
            // txt_količina_za_proizvodnju
            // 
            this.txt_količina_za_proizvodnju.Location = new System.Drawing.Point(148, 158);
            this.txt_količina_za_proizvodnju.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_količina_za_proizvodnju.Multiline = true;
            this.txt_količina_za_proizvodnju.Name = "txt_količina_za_proizvodnju";
            this.txt_količina_za_proizvodnju.Size = new System.Drawing.Size(156, 24);
            this.txt_količina_za_proizvodnju.TabIndex = 85;
            this.txt_količina_za_proizvodnju.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_količina_za_proizvodnju_KeyPress);
            // 
            // txt_artikal_paket
            // 
            this.txt_artikal_paket.Location = new System.Drawing.Point(148, 128);
            this.txt_artikal_paket.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_artikal_paket.Multiline = true;
            this.txt_artikal_paket.Name = "txt_artikal_paket";
            this.txt_artikal_paket.Size = new System.Drawing.Size(156, 24);
            this.txt_artikal_paket.TabIndex = 84;
            this.txt_artikal_paket.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_artikal_paket_KeyPress);
            // 
            // txt_status_stavke
            // 
            this.txt_status_stavke.Location = new System.Drawing.Point(148, 98);
            this.txt_status_stavke.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_status_stavke.Multiline = true;
            this.txt_status_stavke.Name = "txt_status_stavke";
            this.txt_status_stavke.Size = new System.Drawing.Size(156, 24);
            this.txt_status_stavke.TabIndex = 77;
            // 
            // txt_rn
            // 
            this.txt_rn.Location = new System.Drawing.Point(148, 66);
            this.txt_rn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_rn.Multiline = true;
            this.txt_rn.Name = "txt_rn";
            this.txt_rn.Size = new System.Drawing.Size(156, 24);
            this.txt_rn.TabIndex = 83;
            this.txt_rn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_rn_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(10, 160);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(131, 15);
            this.label14.TabIndex = 82;
            this.label14.Text = "Količina Za Proizvodnju";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(335, 68);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 15);
            this.label13.TabIndex = 81;
            this.label13.Text = "Potrebna Količina";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(335, 38);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(178, 15);
            this.label12.TabIndex = 80;
            this.label12.Text = "Artikal Iz Prvog Nivoa Sastavnice";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(335, 128);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(163, 15);
            this.label10.TabIndex = 78;
            this.label10.Text = "Rezervisana Količina Po Planu";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(335, 100);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(168, 15);
            this.label9.TabIndex = 77;
            this.label9.Text = "Trenutna Količina Na Zalihama";
            // 
            // txt_id_stavke_plana_pakovanja
            // 
            this.txt_id_stavke_plana_pakovanja.Location = new System.Drawing.Point(148, 36);
            this.txt_id_stavke_plana_pakovanja.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_id_stavke_plana_pakovanja.Multiline = true;
            this.txt_id_stavke_plana_pakovanja.Name = "txt_id_stavke_plana_pakovanja";
            this.txt_id_stavke_plana_pakovanja.Size = new System.Drawing.Size(156, 24);
            this.txt_id_stavke_plana_pakovanja.TabIndex = 76;
            this.txt_id_stavke_plana_pakovanja.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_id_stavke_plana_pakovanja_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 128);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 15);
            this.label4.TabIndex = 72;
            this.label4.Text = "Artikal Paket";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(10, 100);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 15);
            this.label6.TabIndex = 71;
            this.label6.Text = "Status RN";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(10, 68);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 15);
            this.label7.TabIndex = 70;
            this.label7.Text = "RN";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(10, 38);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 15);
            this.label8.TabIndex = 69;
            this.label8.Text = "Id Stavke Plana Pakovanja";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.SlateGray;
            this.btnCancel.Location = new System.Drawing.Point(80, 201);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 64;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(227, 201);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 24);
            this.btnUpdate.TabIndex = 63;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(562, 15);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(89, 20);
            this.textBoxSearch.TabIndex = 75;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(698, 249);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(468, 382);
            this.panel1.TabIndex = 76;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(462, 148);
            this.dataGridView1.TabIndex = 0;
            // 
            // frmPregledStavkiPlanaPakovanja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 643);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.groupBoxAddEdit);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dataGrid);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Icon = global::WindowsFormsApplication1.Properties.Resources.JelaIkona;
            this.Name = "frmPregledStavkiPlanaPakovanja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Izmeni Stavke Za Pregled Plana Pakovanja";
            this.Load += new System.EventHandler(this.frmPregledStavkiPlanaPakovanja_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.groupBoxAddEdit.ResumeLayout(false);
            this.groupBoxAddEdit.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.GroupBox groupBoxAddEdit;
        private System.Windows.Forms.TextBox txt_rezervisana_količina_po_planu;
        private System.Windows.Forms.TextBox txt_trenutna_količina_na_zalihama;
        private System.Windows.Forms.TextBox txt_potrebna_količina;
        private System.Windows.Forms.TextBox txt_artikal_iz_prvog_nivoa_sastavnice;
        private System.Windows.Forms.TextBox txt_količina_za_proizvodnju;
        private System.Windows.Forms.TextBox txt_artikal_paket;
        private System.Windows.Forms.TextBox txt_status_stavke;
        private System.Windows.Forms.TextBox txt_rn;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_id_stavke_plana_pakovanja;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txt_ukucati_količinu_za_rezervaciju;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}