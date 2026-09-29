
namespace WindowsFormsApplication1.Pakovanja.StavkePlanaPakovanja
{
    partial class frmStavkePlanaPakovanja
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
            this.btnDetails = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.InsertBox = new System.Windows.Forms.GroupBox();
            this.panelDNP = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_UnesiteTermin = new System.Windows.Forms.TextBox();
            this.btnInsertStavke = new System.Windows.Forms.Button();
            this.dataGrid1 = new System.Windows.Forms.DataGridView();
            this.cboPlanProizvodnje = new System.Windows.Forms.ComboBox();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.datum_do = new System.Windows.Forms.DateTimePicker();
            this.datum_od = new System.Windows.Forms.DateTimePicker();
            this.btnCancelAdd = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxAddEdit = new System.Windows.Forms.GroupBox();
            this.txt_kolicina = new System.Windows.Forms.TextBox();
            this.txt_termin = new System.Windows.Forms.TextBox();
            this.txt_naziv = new System.Windows.Forms.TextBox();
            this.txt_kod = new System.Windows.Forms.TextBox();
            this.txt_artikal = new System.Windows.Forms.TextBox();
            this.txt_vrsta_rn = new System.Windows.Forms.TextBox();
            this.txt_status_rn = new System.Windows.Forms.TextBox();
            this.txt_broj_rn = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.datum = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_id_plana_pakovanja = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.InsertBox.SuspendLayout();
            this.panelDNP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.groupBoxAddEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDetails
            // 
            this.btnDetails.Location = new System.Drawing.Point(91, 12);
            this.btnDetails.Margin = new System.Windows.Forms.Padding(2);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(75, 24);
            this.btnDetails.TabIndex = 65;
            this.btnDetails.Text = "Details";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.SlateGray;
            this.btnBack.Location = new System.Drawing.Point(11, 12);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 24);
            this.btnBack.TabIndex = 64;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(202, 12);
            this.btnAddItem.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(75, 24);
            this.btnAddItem.TabIndex = 62;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(282, 12);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 24);
            this.btnEdit.TabIndex = 61;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.IndianRed;
            this.btnDelete.Location = new System.Drawing.Point(362, 12);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 24);
            this.btnDelete.TabIndex = 60;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(482, 12);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 24);
            this.btnSearch.TabIndex = 58;
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
            this.dataGrid.Margin = new System.Windows.Forms.Padding(2);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersWidth = 51;
            this.dataGrid.RowTemplate.Height = 24;
            this.dataGrid.Size = new System.Drawing.Size(1292, 203);
            this.dataGrid.TabIndex = 57;
            // 
            // InsertBox
            // 
            this.InsertBox.Controls.Add(this.panelDNP);
            this.InsertBox.Controls.Add(this.cboPlanProizvodnje);
            this.InsertBox.Controls.Add(this.comboBox);
            this.InsertBox.Controls.Add(this.datum_do);
            this.InsertBox.Controls.Add(this.datum_od);
            this.InsertBox.Controls.Add(this.btnCancelAdd);
            this.InsertBox.Controls.Add(this.btnApply);
            this.InsertBox.Controls.Add(this.label3);
            this.InsertBox.Controls.Add(this.label2);
            this.InsertBox.Controls.Add(this.label15);
            this.InsertBox.Controls.Add(this.label1);
            this.InsertBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InsertBox.Location = new System.Drawing.Point(11, 248);
            this.InsertBox.Margin = new System.Windows.Forms.Padding(2);
            this.InsertBox.Name = "InsertBox";
            this.InsertBox.Padding = new System.Windows.Forms.Padding(2);
            this.InsertBox.Size = new System.Drawing.Size(1459, 405);
            this.InsertBox.TabIndex = 66;
            this.InsertBox.TabStop = false;
            this.InsertBox.Text = "Dodaj Stavke Plana Pakovanja";
            // 
            // panelDNP
            // 
            this.panelDNP.Controls.Add(this.dataGridView1);
            this.panelDNP.Controls.Add(this.label5);
            this.panelDNP.Controls.Add(this.txt_UnesiteTermin);
            this.panelDNP.Controls.Add(this.btnInsertStavke);
            this.panelDNP.Controls.Add(this.dataGrid1);
            this.panelDNP.Location = new System.Drawing.Point(271, 13);
            this.panelDNP.Margin = new System.Windows.Forms.Padding(2);
            this.panelDNP.Name = "panelDNP";
            this.panelDNP.Size = new System.Drawing.Size(1184, 388);
            this.panelDNP.TabIndex = 69;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 233);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1178, 152);
            this.dataGridView1.TabIndex = 74;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(2, 5);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 15);
            this.label5.TabIndex = 72;
            this.label5.Text = "Unesite termin";
            // 
            // txt_UnesiteTermin
            // 
            this.txt_UnesiteTermin.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_UnesiteTermin.Location = new System.Drawing.Point(2, 22);
            this.txt_UnesiteTermin.Margin = new System.Windows.Forms.Padding(2);
            this.txt_UnesiteTermin.Name = "txt_UnesiteTermin";
            this.txt_UnesiteTermin.Size = new System.Drawing.Size(126, 22);
            this.txt_UnesiteTermin.TabIndex = 71;
            this.txt_UnesiteTermin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_UnesiteTermin_KeyPress);
            // 
            // btnInsertStavke
            // 
            this.btnInsertStavke.Location = new System.Drawing.Point(132, 7);
            this.btnInsertStavke.Margin = new System.Windows.Forms.Padding(2);
            this.btnInsertStavke.Name = "btnInsertStavke";
            this.btnInsertStavke.Size = new System.Drawing.Size(83, 38);
            this.btnInsertStavke.TabIndex = 70;
            this.btnInsertStavke.Text = "Dodaj Stavku";
            this.btnInsertStavke.UseVisualStyleBackColor = true;
            this.btnInsertStavke.Click += new System.EventHandler(this.btnInsertStavke_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.AllowUserToAddRows = false;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(158)))), ((int)(((byte)(192)))));
            this.dataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid1.Location = new System.Drawing.Point(2, 49);
            this.dataGrid1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.RowHeadersWidth = 51;
            this.dataGrid1.RowTemplate.Height = 24;
            this.dataGrid1.Size = new System.Drawing.Size(1180, 179);
            this.dataGrid1.TabIndex = 69;
            // 
            // cboPlanProizvodnje
            // 
            this.cboPlanProizvodnje.FormattingEnabled = true;
            this.cboPlanProizvodnje.Location = new System.Drawing.Point(4, 49);
            this.cboPlanProizvodnje.Margin = new System.Windows.Forms.Padding(2);
            this.cboPlanProizvodnje.Name = "cboPlanProizvodnje";
            this.cboPlanProizvodnje.Size = new System.Drawing.Size(253, 23);
            this.cboPlanProizvodnje.TabIndex = 73;
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(4, 97);
            this.comboBox.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(253, 23);
            this.comboBox.TabIndex = 73;
            // 
            // datum_do
            // 
            this.datum_do.CustomFormat = "dd-MM-yyyy HH:mm";
            this.datum_do.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datum_do.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datum_do.Location = new System.Drawing.Point(5, 206);
            this.datum_do.Margin = new System.Windows.Forms.Padding(2);
            this.datum_do.Name = "datum_do";
            this.datum_do.Size = new System.Drawing.Size(252, 23);
            this.datum_do.TabIndex = 67;
            // 
            // datum_od
            // 
            this.datum_od.CustomFormat = "dd-MM-yyyy HH:mm";
            this.datum_od.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datum_od.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datum_od.Location = new System.Drawing.Point(5, 150);
            this.datum_od.Margin = new System.Windows.Forms.Padding(2);
            this.datum_od.Name = "datum_od";
            this.datum_od.Size = new System.Drawing.Size(252, 23);
            this.datum_od.TabIndex = 66;
            // 
            // btnCancelAdd
            // 
            this.btnCancelAdd.BackColor = System.Drawing.Color.SlateGray;
            this.btnCancelAdd.Location = new System.Drawing.Point(5, 249);
            this.btnCancelAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelAdd.Name = "btnCancelAdd";
            this.btnCancelAdd.Size = new System.Drawing.Size(75, 24);
            this.btnCancelAdd.TabIndex = 64;
            this.btnCancelAdd.Text = "Cancel";
            this.btnCancelAdd.UseVisualStyleBackColor = false;
            this.btnCancelAdd.Click += new System.EventHandler(this.btnCancelAdd_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(182, 249);
            this.btnApply.Margin = new System.Windows.Forms.Padding(2);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 24);
            this.btnApply.TabIndex = 63;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 187);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 15);
            this.label3.TabIndex = 59;
            this.label3.Text = "Datum Do";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 133);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.TabIndex = 57;
            this.label2.Text = "Datum Od";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(10, 32);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 15);
            this.label15.TabIndex = 55;
            this.label15.Text = "Plan proizvodnje";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 80);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 55;
            this.label1.Text = "Vrsta naloga";
            // 
            // groupBoxAddEdit
            // 
            this.groupBoxAddEdit.Controls.Add(this.txt_kolicina);
            this.groupBoxAddEdit.Controls.Add(this.txt_termin);
            this.groupBoxAddEdit.Controls.Add(this.txt_naziv);
            this.groupBoxAddEdit.Controls.Add(this.txt_kod);
            this.groupBoxAddEdit.Controls.Add(this.txt_artikal);
            this.groupBoxAddEdit.Controls.Add(this.txt_vrsta_rn);
            this.groupBoxAddEdit.Controls.Add(this.txt_status_rn);
            this.groupBoxAddEdit.Controls.Add(this.txt_broj_rn);
            this.groupBoxAddEdit.Controls.Add(this.label14);
            this.groupBoxAddEdit.Controls.Add(this.datum);
            this.groupBoxAddEdit.Controls.Add(this.label13);
            this.groupBoxAddEdit.Controls.Add(this.label12);
            this.groupBoxAddEdit.Controls.Add(this.label11);
            this.groupBoxAddEdit.Controls.Add(this.label10);
            this.groupBoxAddEdit.Controls.Add(this.label9);
            this.groupBoxAddEdit.Controls.Add(this.txt_id_plana_pakovanja);
            this.groupBoxAddEdit.Controls.Add(this.label4);
            this.groupBoxAddEdit.Controls.Add(this.label6);
            this.groupBoxAddEdit.Controls.Add(this.label7);
            this.groupBoxAddEdit.Controls.Add(this.label8);
            this.groupBoxAddEdit.Controls.Add(this.btnCancel);
            this.groupBoxAddEdit.Controls.Add(this.btnUpdate);
            this.groupBoxAddEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAddEdit.Location = new System.Drawing.Point(674, 17);
            this.groupBoxAddEdit.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxAddEdit.Name = "groupBoxAddEdit";
            this.groupBoxAddEdit.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxAddEdit.Size = new System.Drawing.Size(629, 227);
            this.groupBoxAddEdit.TabIndex = 67;
            this.groupBoxAddEdit.TabStop = false;
            this.groupBoxAddEdit.Text = "Izmeni Stavke Plana Pakovanja";
            // 
            // txt_kolicina
            // 
            this.txt_kolicina.Location = new System.Drawing.Point(424, 128);
            this.txt_kolicina.Margin = new System.Windows.Forms.Padding(2);
            this.txt_kolicina.Multiline = true;
            this.txt_kolicina.Name = "txt_kolicina";
            this.txt_kolicina.Size = new System.Drawing.Size(156, 24);
            this.txt_kolicina.TabIndex = 89;
            this.txt_kolicina.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_kolicina_KeyPress);
            // 
            // txt_termin
            // 
            this.txt_termin.Location = new System.Drawing.Point(424, 98);
            this.txt_termin.Margin = new System.Windows.Forms.Padding(2);
            this.txt_termin.Multiline = true;
            this.txt_termin.Name = "txt_termin";
            this.txt_termin.Size = new System.Drawing.Size(156, 24);
            this.txt_termin.TabIndex = 88;
            this.txt_termin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_termin_KeyPress);
            // 
            // txt_naziv
            // 
            this.txt_naziv.Location = new System.Drawing.Point(424, 67);
            this.txt_naziv.Margin = new System.Windows.Forms.Padding(2);
            this.txt_naziv.Multiline = true;
            this.txt_naziv.Name = "txt_naziv";
            this.txt_naziv.Size = new System.Drawing.Size(156, 24);
            this.txt_naziv.TabIndex = 86;
            // 
            // txt_kod
            // 
            this.txt_kod.Location = new System.Drawing.Point(424, 36);
            this.txt_kod.Margin = new System.Windows.Forms.Padding(2);
            this.txt_kod.Multiline = true;
            this.txt_kod.Name = "txt_kod";
            this.txt_kod.Size = new System.Drawing.Size(156, 24);
            this.txt_kod.TabIndex = 87;
            // 
            // txt_artikal
            // 
            this.txt_artikal.Location = new System.Drawing.Point(119, 158);
            this.txt_artikal.Margin = new System.Windows.Forms.Padding(2);
            this.txt_artikal.Multiline = true;
            this.txt_artikal.Name = "txt_artikal";
            this.txt_artikal.Size = new System.Drawing.Size(156, 24);
            this.txt_artikal.TabIndex = 85;
            this.txt_artikal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_artikal_KeyPress);
            // 
            // txt_vrsta_rn
            // 
            this.txt_vrsta_rn.Location = new System.Drawing.Point(119, 128);
            this.txt_vrsta_rn.Margin = new System.Windows.Forms.Padding(2);
            this.txt_vrsta_rn.Multiline = true;
            this.txt_vrsta_rn.Name = "txt_vrsta_rn";
            this.txt_vrsta_rn.Size = new System.Drawing.Size(156, 24);
            this.txt_vrsta_rn.TabIndex = 84;
            this.txt_vrsta_rn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_vrsta_rn_KeyPress);
            // 
            // txt_status_rn
            // 
            this.txt_status_rn.Location = new System.Drawing.Point(119, 98);
            this.txt_status_rn.Margin = new System.Windows.Forms.Padding(2);
            this.txt_status_rn.Multiline = true;
            this.txt_status_rn.Name = "txt_status_rn";
            this.txt_status_rn.Size = new System.Drawing.Size(156, 24);
            this.txt_status_rn.TabIndex = 77;
            // 
            // txt_broj_rn
            // 
            this.txt_broj_rn.Location = new System.Drawing.Point(119, 66);
            this.txt_broj_rn.Margin = new System.Windows.Forms.Padding(2);
            this.txt_broj_rn.Multiline = true;
            this.txt_broj_rn.Name = "txt_broj_rn";
            this.txt_broj_rn.Size = new System.Drawing.Size(156, 24);
            this.txt_broj_rn.TabIndex = 83;
            this.txt_broj_rn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_broj_rn_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(10, 160);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 15);
            this.label14.TabIndex = 82;
            this.label14.Text = "Artikal";
            // 
            // datum
            // 
            this.datum.CustomFormat = "dd-MM-yyyy HH:mm";
            this.datum.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datum.Location = new System.Drawing.Point(344, 193);
            this.datum.Margin = new System.Windows.Forms.Padding(2);
            this.datum.Name = "datum";
            this.datum.Size = new System.Drawing.Size(170, 23);
            this.datum.TabIndex = 74;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(286, 68);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 15);
            this.label13.TabIndex = 81;
            this.label13.Text = "Naziv";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(286, 38);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(28, 15);
            this.label12.TabIndex = 80;
            this.label12.Text = "Kod";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(341, 175);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(178, 15);
            this.label11.TabIndex = 79;
            this.label11.Text = "Planirani Krajni Datum Završetka";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(286, 128);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(131, 15);
            this.label10.TabIndex = 78;
            this.label10.Text = "Količina Za Proizvodnju";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(286, 100);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 15);
            this.label9.TabIndex = 77;
            this.label9.Text = "Termin";
            // 
            // txt_id_plana_pakovanja
            // 
            this.txt_id_plana_pakovanja.Location = new System.Drawing.Point(119, 36);
            this.txt_id_plana_pakovanja.Margin = new System.Windows.Forms.Padding(2);
            this.txt_id_plana_pakovanja.Multiline = true;
            this.txt_id_plana_pakovanja.Name = "txt_id_plana_pakovanja";
            this.txt_id_plana_pakovanja.Size = new System.Drawing.Size(156, 24);
            this.txt_id_plana_pakovanja.TabIndex = 76;
            this.txt_id_plana_pakovanja.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_id_plana_pakovanja_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 128);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 72;
            this.label4.Text = "Vrsta RN";
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
            this.label7.Size = new System.Drawing.Size(47, 15);
            this.label7.TabIndex = 70;
            this.label7.Text = "Broj RN";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(10, 38);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 15);
            this.label8.TabIndex = 69;
            this.label8.Text = "Id Plana Pakovanja";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.SlateGray;
            this.btnCancel.Location = new System.Drawing.Point(80, 201);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 64;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(199, 201);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2);
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
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(89, 20);
            this.textBoxSearch.TabIndex = 68;
            // 
            // frmStavkePlanaPakovanja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1481, 664);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.groupBoxAddEdit);
            this.Controls.Add(this.InsertBox);
            this.Controls.Add(this.btnDetails);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dataGrid);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Icon = global::WindowsFormsApplication1.Properties.Resources.JelaIkona;
            this.Name = "frmStavkePlanaPakovanja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stavke Plana Pakovanja";
            this.Load += new System.EventHandler(this.frmStavkePlanaPakovanja_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.InsertBox.ResumeLayout(false);
            this.InsertBox.PerformLayout();
            this.panelDNP.ResumeLayout(false);
            this.panelDNP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.groupBoxAddEdit.ResumeLayout(false);
            this.groupBoxAddEdit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.GroupBox InsertBox;
        private System.Windows.Forms.DateTimePicker datum_do;
        private System.Windows.Forms.DateTimePicker datum_od;
        private System.Windows.Forms.Button btnCancelAdd;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelDNP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_UnesiteTermin;
        private System.Windows.Forms.DataGridView dataGrid1;
        private System.Windows.Forms.Button btnInsertStavke;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBoxAddEdit;
        private System.Windows.Forms.TextBox txt_id_plana_pakovanja;
        private System.Windows.Forms.DateTimePicker datum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_kolicina;
        private System.Windows.Forms.TextBox txt_termin;
        private System.Windows.Forms.TextBox txt_naziv;
        private System.Windows.Forms.TextBox txt_kod;
        private System.Windows.Forms.TextBox txt_artikal;
        private System.Windows.Forms.TextBox txt_vrsta_rn;
        private System.Windows.Forms.TextBox txt_status_rn;
        private System.Windows.Forms.TextBox txt_broj_rn;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.ComboBox cboPlanProizvodnje;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}