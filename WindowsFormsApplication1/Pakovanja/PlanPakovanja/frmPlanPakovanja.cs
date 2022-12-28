using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.Pakovanja.StavkePlanaPakovanja;

namespace WindowsFormsApplication1.Pakovanja.PlanPakovanja
{
    public partial class frmPlanPakovanja : Form // Number 1
    {
        private PackingPlanDAO _PackingPlan = new PackingPlanDAO();

        private List<PackingPlanModel> PackingPlanList = new List<PackingPlanModel>();

        int row_selected;
        public frmPlanPakovanja()
        {
            InitializeComponent();
            setStyle();
        }
        private void frmPlanPakovanja_Load(object sender, EventArgs e)
        {
            groupBoxAddEdit.Visible = false;
            PackingPlanList = _PackingPlan.GetAll();

            // connect the list  to the grid view
            dataGrid.DataSource = PackingPlanList;

        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (btnSearch.Text == "Search")
            {
                if (string.IsNullOrEmpty(textBoxSearch.Text))
                {
                    PackingPlanList = _PackingPlan.GetAll();
                }
                else
                {
                    PackingPlanList = _PackingPlan.Search(textBoxSearch.Text);
                    btnSearch.Text = "Show All";
                }
                dataGrid.DataSource = PackingPlanList;
            }
            else
            {
                PackingPlanList = _PackingPlan.GetAll();
                dataGrid.DataSource = PackingPlanList;
                btnSearch.Text = "Search";
                textBoxSearch.Text = string.Empty;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid.Rows.Count != 0)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this item?",
                     "Confirm Delete!",
                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    row_selected = dataGrid.CurrentRow.Index;
                    int id = (int)dataGrid.Rows[row_selected].Cells[0].Value;
                    int resoult = _PackingPlan.Delete(id);
                    //  MessageBox.Show(resoult + " row(s) is deleted");

                    // Refresh the table
                    PackingPlanList = _PackingPlan.GetAll();
                    dataGrid.DataSource = PackingPlanList;
                }
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            groupBoxAddEdit.Visible = true;
            groupBoxAddEdit.Text = "Dodaj Plan Pakovanja";
            btnAddUpdate.Text = "Add";
            datum_od.Value = DateTime.Now;
            datum_do.Value = DateTime.Now;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGrid.Rows.Count != 0)
            {
                groupBoxAddEdit.Visible = true;
                groupBoxAddEdit.Text = "Izmeni Plan Pakovanja";
                btnAddUpdate.Text = "Update";
                row_selected = dataGrid.CurrentRow.Index;
                txt_naziv.Text = (string)dataGrid.Rows[row_selected].Cells[1].Value;
                datum_od.Value = (DateTime)dataGrid.Rows[row_selected].Cells[2].Value;
                datum_do.Value = (DateTime)dataGrid.Rows[row_selected].Cells[3].Value;
                txt_napomena.Text = (string)dataGrid.Rows[row_selected].Cells[4].Value;
            }
        }

        private void btnAddUpdate_Click(object sender, EventArgs e)
        {
            if (btnAddUpdate.Text == "Add")
            {
                PackingPlanModel packingPlan = new PackingPlanModel()
                {
                    Naziv = txt_naziv.Text,
                    DatumOd = datum_od.Value,
                    DatumDo = datum_do.Value,
                    Napomena = txt_napomena.Text
                };
                int resoult = _PackingPlan.Insert(packingPlan);
                // MessageBox.Show(resoult + " row(s) is added");
                // Refresh the table
                PackingPlanList = _PackingPlan.GetAll();
                dataGrid.DataSource = PackingPlanList;
                groupBoxAddEdit.Visible = false;
            }
            else
            {
                int id = (int)dataGrid.Rows[row_selected].Cells[0].Value;
                PackingPlanModel packingPlan = new PackingPlanModel()
                {
                    Id = id,
                    Naziv = txt_naziv.Text,
                    DatumOd = datum_od.Value,
                    DatumDo = datum_do.Value,
                    Napomena = txt_napomena.Text
                };
                int resoult = _PackingPlan.Update(packingPlan);
                // MessageBox.Show(resoult + " row is updated");
                // Refresh the table
                PackingPlanList = _PackingPlan.GetAll();
                dataGrid.DataSource = PackingPlanList;
                groupBoxAddEdit.Visible = false;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            groupBoxAddEdit.Visible = false;
            row_selected = dataGrid.CurrentRow.Index;
            txt_naziv.Text = string.Empty;
            datum_od.Value = DateTime.Now;
            datum_do.Value = DateTime.Now;
            txt_napomena.Text = string.Empty;
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            if (dataGrid.Rows.Count > 0)
            {
                row_selected = dataGrid.CurrentRow.Index;
                frmStavkePlanaPakovanja stavke = new frmStavkePlanaPakovanja((int)dataGrid.Rows[row_selected].Cells[0].Value);
                this.Hide();
                stavke.Show();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //Povratak na stranu po želji
        }

        private void setStyle()
        {
            Color btnBC = Color.FromArgb(225, 143, 4);

            btnSearch.BackColor = btnBC;
            btnAddItem.BackColor = btnBC;
            btnEdit.BackColor = btnBC;
            btnDetails.BackColor = btnBC;
            btnBack.BackColor = btnBC;
            btnAddUpdate.BackColor = btnBC;
            // btnCancel.BackColor = btnBC;
            // btnDelete.BackColor = btnBC;

            Color btnFC = SystemColors.ControlText;

            btnSearch.ForeColor = btnFC;
            btnAddItem.ForeColor = btnFC;
            btnEdit.ForeColor = btnFC;
            btnAddUpdate.ForeColor = btnFC;
            btnDetails.ForeColor = btnFC;
            btnCancel.ForeColor = btnFC;
            btnBack.ForeColor = btnFC;
            btnDelete.ForeColor = btnFC;

            Font btnFont = new Font("Segoe UI Semibold", 10, FontStyle.Bold);

            btnSearch.Font = btnFont;
            btnAddItem.Font = btnFont;
            btnEdit.Font = btnFont;
            btnAddUpdate.Font = btnFont;
            btnDetails.Font = btnFont;
            btnCancel.Font = btnFont;
            btnBack.Font = btnFont;
            btnDelete.Font = btnFont;

            this.BackColor = Color.FromArgb(124, 187, 0);
            this.Font = new Font("Segoe UI", 10);
            this.Size = new Size(1600, 1000);

            dataGrid.BackgroundColor = Color.FromArgb(125, 158, 192);
            dataGrid.Size = new Size(850, 250);
            dataGrid.DefaultCellStyle.SelectionBackColor = btnBC;
            dataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGrid.AllowUserToAddRows = false;
            dataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGrid.RowHeadersVisible = false;
        }

    }
}
