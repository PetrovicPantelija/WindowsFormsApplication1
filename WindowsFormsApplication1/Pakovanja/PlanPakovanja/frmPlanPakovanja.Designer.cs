
namespace WindowsFormsApplication1.Pakovanja.PlanPakovanja
{
    partial class frmPlanPakovanja
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
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBoxAddEdit = new System.Windows.Forms.GroupBox();
            this.txt_naziv = new System.Windows.Forms.TextBox();
            this.datum_do = new System.Windows.Forms.DateTimePicker();
            this.datum_od = new System.Windows.Forms.DateTimePicker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddUpdate = new System.Windows.Forms.Button();
            this.txt_napomena = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnDetails = new System.Windows.Forms.Button();
            this.btnKonacan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.groupBoxAddEdit.SuspendLayout();
            this.SuspendLayout();
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
            this.dataGrid.Size = new System.Drawing.Size(924, 203);
            this.dataGrid.TabIndex = 48;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(482, 12);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 24);
            this.btnSearch.TabIndex = 49;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(561, 15);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(89, 20);
            this.textBoxSearch.TabIndex = 50;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.IndianRed;
            this.btnDelete.Location = new System.Drawing.Point(362, 12);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 24);
            this.btnDelete.TabIndex = 51;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(282, 12);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 24);
            this.btnEdit.TabIndex = 52;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(202, 12);
            this.btnAddItem.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(75, 24);
            this.btnAddItem.TabIndex = 53;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // groupBoxAddEdit
            // 
            this.groupBoxAddEdit.Controls.Add(this.txt_naziv);
            this.groupBoxAddEdit.Controls.Add(this.datum_do);
            this.groupBoxAddEdit.Controls.Add(this.datum_od);
            this.groupBoxAddEdit.Controls.Add(this.btnCancel);
            this.groupBoxAddEdit.Controls.Add(this.btnAddUpdate);
            this.groupBoxAddEdit.Controls.Add(this.txt_napomena);
            this.groupBoxAddEdit.Controls.Add(this.label4);
            this.groupBoxAddEdit.Controls.Add(this.label3);
            this.groupBoxAddEdit.Controls.Add(this.label2);
            this.groupBoxAddEdit.Controls.Add(this.label1);
            this.groupBoxAddEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAddEdit.Location = new System.Drawing.Point(11, 248);
            this.groupBoxAddEdit.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxAddEdit.Name = "groupBoxAddEdit";
            this.groupBoxAddEdit.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxAddEdit.Size = new System.Drawing.Size(347, 238);
            this.groupBoxAddEdit.TabIndex = 54;
            this.groupBoxAddEdit.TabStop = false;
            this.groupBoxAddEdit.Text = "Dodaj Plan Pakovanja";
            // 
            // txt_naziv
            // 
            this.txt_naziv.Location = new System.Drawing.Point(80, 23);
            this.txt_naziv.Margin = new System.Windows.Forms.Padding(2);
            this.txt_naziv.Multiline = true;
            this.txt_naziv.Name = "txt_naziv";
            this.txt_naziv.Size = new System.Drawing.Size(252, 24);
            this.txt_naziv.TabIndex = 68;
            // 
            // datum_do
            // 
            this.datum_do.CustomFormat = "dd-MM-yyyy HH:mm";
            this.datum_do.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datum_do.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datum_do.Location = new System.Drawing.Point(80, 102);
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
            this.datum_od.Location = new System.Drawing.Point(80, 62);
            this.datum_od.Margin = new System.Windows.Forms.Padding(2);
            this.datum_od.Name = "datum_od";
            this.datum_od.Size = new System.Drawing.Size(252, 23);
            this.datum_od.TabIndex = 66;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.SlateGray;
            this.btnCancel.Location = new System.Drawing.Point(80, 197);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 64;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddUpdate
            // 
            this.btnAddUpdate.Location = new System.Drawing.Point(256, 197);
            this.btnAddUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddUpdate.Name = "btnAddUpdate";
            this.btnAddUpdate.Size = new System.Drawing.Size(75, 24);
            this.btnAddUpdate.TabIndex = 63;
            this.btnAddUpdate.Text = "Add";
            this.btnAddUpdate.UseVisualStyleBackColor = true;
            this.btnAddUpdate.Click += new System.EventHandler(this.btnAddUpdate_Click);
            // 
            // txt_napomena
            // 
            this.txt_napomena.Location = new System.Drawing.Point(80, 141);
            this.txt_napomena.Margin = new System.Windows.Forms.Padding(2);
            this.txt_napomena.Multiline = true;
            this.txt_napomena.Name = "txt_napomena";
            this.txt_napomena.Size = new System.Drawing.Size(252, 53);
            this.txt_napomena.TabIndex = 62;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 143);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 15);
            this.label4.TabIndex = 61;
            this.label4.Text = "Napomena";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 104);
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
            this.label2.Location = new System.Drawing.Point(4, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.TabIndex = 57;
            this.label2.Text = "Datum Od";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 55;
            this.label1.Text = "Naziv";
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.SlateGray;
            this.btnBack.Location = new System.Drawing.Point(11, 12);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 24);
            this.btnBack.TabIndex = 55;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnDetails
            // 
            this.btnDetails.Location = new System.Drawing.Point(91, 12);
            this.btnDetails.Margin = new System.Windows.Forms.Padding(2);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(75, 24);
            this.btnDetails.TabIndex = 56;
            this.btnDetails.Text = "Details";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // btnKonacan
            // 
            this.btnKonacan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnKonacan.Location = new System.Drawing.Point(827, 15);
            this.btnKonacan.Name = "btnKonacan";
            this.btnKonacan.Size = new System.Drawing.Size(108, 24);
            this.btnKonacan.TabIndex = 57;
            this.btnKonacan.Text = "Proglasi konačan";
            this.btnKonacan.UseVisualStyleBackColor = false;
            this.btnKonacan.Click += new System.EventHandler(this.btnKonacan_Click);
            // 
            // frmPlanPakovanja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 782);
            this.Controls.Add(this.btnKonacan);
            this.Controls.Add(this.btnDetails);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.groupBoxAddEdit);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dataGrid);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmPlanPakovanja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plan Pakovanja";
            this.Load += new System.EventHandler(this.frmPlanPakovanja_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.groupBoxAddEdit.ResumeLayout(false);
            this.groupBoxAddEdit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAddItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBoxAddEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker datum_do;
        private System.Windows.Forms.DateTimePicker datum_od;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddUpdate;
        private System.Windows.Forms.TextBox txt_napomena;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.TextBox txt_naziv;
        private System.Windows.Forms.Button btnKonacan;
    }
}