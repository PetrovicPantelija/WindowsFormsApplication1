using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.Pakovanja.PackingPlanItem;
using WindowsFormsApplication1.Pakovanja.PlanPakovanja;
using WindowsFormsApplication1.Pakovanja.PregledStavkiPlanaPakovanja;

namespace WindowsFormsApplication1.Pakovanja.StavkePlanaPakovanja
{
    public partial class frmStavkePlanaPakovanja : Form
    {
        private PackingPlanItemDAO _PackingPlanItem = new PackingPlanItemDAO();
        private PackingPlanItemOverviewDAO _PackingPlanItemOverview = new PackingPlanItemOverviewDAO();

        private List<PackingPlanItemModel> PackingPlanItemList = new List<PackingPlanItemModel>();

        int id_sup;
        int row_selected;

        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public frmStavkePlanaPakovanja(int id)
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            setStyle();
            id_sup = id;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmPlanPakovanja plan = new frmPlanPakovanja();
            this.Hide();
            plan.Show();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            if (dataGrid.Rows.Count > 0)
            {
                row_selected = dataGrid.CurrentRow.Index;
                frmPregledStavkiPlanaPakovanja pregled = new frmPregledStavkiPlanaPakovanja((int)dataGrid.Rows[row_selected].Cells[0].Value, id_sup);
                this.Hide();
                pregled.Show();
            }
        }
        private void frmStavkePlanaPakovanja_Load(object sender, EventArgs e)
        {
            groupBoxAddEdit.Visible = false;
            InsertBox.Visible = false;
            PackingPlanItemList = _PackingPlanItem.SearchByIdSuperior(id_sup);
            // connect the list  to the grid view
            dataGrid.DataSource = PackingPlanItemList;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (btnSearch.Text == "Search")
            {
                if (string.IsNullOrEmpty(textBoxSearch.Text))
                {
                    PackingPlanItemList = _PackingPlanItem.SearchByIdSuperior(id_sup);
                }
                else
                {
                    PackingPlanItemList = _PackingPlanItem.Search(textBoxSearch.Text, id_sup);
                    btnSearch.Text = "Show All";
                }
                dataGrid.DataSource = PackingPlanItemList;
            }
            else
            {
                PackingPlanItemList = _PackingPlanItem.SearchByIdSuperior(id_sup);
                dataGrid.DataSource = PackingPlanItemList;
                btnSearch.Text = "Search";
                textBoxSearch.Text = string.Empty;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid.Rows.Count != 0)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this item?",
                "Confirm Delete!", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    row_selected = dataGrid.CurrentRow.Index;
                    int id = (int)dataGrid.Rows[row_selected].Cells[0].Value;
                    int resoult = _PackingPlanItem.Delete(id);
                    // MessageBox.Show(resoult + " row(s) is deleted");

                    // Refresh the table
                    PackingPlanItemList = _PackingPlanItem.SearchByIdSuperior(id_sup);
                    dataGrid.DataSource = PackingPlanItemList;
                }
            }

        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            groupBoxAddEdit.Visible = false;
            InsertBox.Visible = true;
            panelDNP.Visible = false;
            datum_od.Value = DateTime.Now.AddYears(-3);
            datum_do.Value = DateTime.Now;
            SqlConnection conn = new SqlConnection(connect);

            var query = "select ID,Naziv from PlanArhiv order by ID desc";
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboPlanProizvodnje.DataSource = ds.Tables[0];
            cboPlanProizvodnje.DisplayMember = "Naziv";
            cboPlanProizvodnje.ValueMember = "ID";

            List<string> items = _PackingPlanItem.GetAllVrsteDN();
            foreach (var item in items)
            {
                comboBox.Items.Add(item);
            }
            comboBox.SelectedIndex = 4;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(cboPlanProizvodnje.SelectedValue);
            string DNType = comboBox.Text.ToString().Substring(0, comboBox.Text.ToString().IndexOf(' '));
            panelDNP.Visible = true;
            dataGrid1.DataSource = _PackingPlanItem.GetAllByPlanProizvodnje(id,DNType);
            /*
            string DNType = comboBox.Text.ToString().Substring(0, comboBox.Text.ToString().IndexOf(' '));

            panelDNP.Visible = true;
            // connect the list  to the grid view
            dataGrid1.DataSource = _PackingPlanItem.GetAllByDateDNP(DNType, datum_od.Value, datum_do.Value);
            txt_UnesiteTermin.Text = "0";*/

            var query = "select * from DelNalogProizvodni Where DNpSifVrDok='PDN' and DNpSifStat='LA' or DnPSifStat='PL' or DnpSifStat='OD' and (Datum Between DATEADD(m, -3, GETDATE()) and GETDATE()) and IdPlana is not null and IDPlana<>"+Convert.ToInt32(cboPlanProizvodnje.SelectedValue)+"  order by DNpStZapisa asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.BackgroundColor = Color.Red;

            dataGrid1.ClearSelection();

        }

        private void btnInsertStavke_Click(object sender, EventArgs e)
        {
            int row;
            string sif_zapisa;
            int sif;
            int ter;
            int resoult;

            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.Selected)
                {
                    dataGrid1.ClearSelection();

                    sif_zapisa = r.Cells[2].Value.ToString();
                    sif = Int32.Parse(sif_zapisa);
                    ter = Int32.Parse(txt_UnesiteTermin.Text);
                    txt_UnesiteTermin.Text = "0";
                    resoult = _PackingPlanItem.InsertFromTable(id_sup, sif, ter);
                    if (resoult == 1)
                    {
                        // Refresh the table
                        PackingPlanItemList = _PackingPlanItem.SearchByIdSuperior(id_sup);
                        dataGrid.DataSource = PackingPlanItemList;


                        // Popunjavanje tabele 3
                        PackingPlanItemModel item = PackingPlanItemList.LastOrDefault();
                        resoult = _PackingPlanItemOverview.InsertFromTable(item.Id, item.KolicinaZaProizvodnju, item.Artikal);
                        //  MessageBox.Show(resoult + " row(s) is added");
                    }
                    dataGridView1.ClearSelection();
                    dataGridView1.CurrentCell = null;
                }
                else
                {
                    row = dataGrid1.CurrentRow.Index;
                    dataGrid1.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(225, 143, 4);
                    sif_zapisa = dataGrid1.Rows[row].Cells[2].Value.ToString();
                    sif = Int32.Parse(sif_zapisa);
                    ter = Int32.Parse(txt_UnesiteTermin.Text);
                    txt_UnesiteTermin.Text = "0";
                    resoult = _PackingPlanItem.InsertFromTable(id_sup, sif, ter);
                    // MessageBox.Show(resoult + " row(s) is added");
                    if (resoult == 1)
                    {
                        // Refresh the table
                        PackingPlanItemList = _PackingPlanItem.SearchByIdSuperior(id_sup);
                        dataGrid.DataSource = PackingPlanItemList;


                        // Popunjavanje tabele 3
                        PackingPlanItemModel item = PackingPlanItemList.LastOrDefault();
                        resoult = _PackingPlanItemOverview.InsertFromTable(item.Id, item.KolicinaZaProizvodnju, item.Artikal);
                        //  MessageBox.Show(resoult + " row(s) is added");
                    }
                }

            }
        }

        private void btnCancelAdd_Click(object sender, EventArgs e)
        {
            panelDNP.Visible = false;
            InsertBox.Visible = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGrid.Rows.Count != 0)
            {
                InsertBox.Visible = false;
                groupBoxAddEdit.Visible = true;
                groupBoxAddEdit.Text = "Izmeni Stavke Plana Pakovanja";
                btnUpdate.Text = "Update";
                row_selected = dataGrid.CurrentRow.Index;
                txt_id_plana_pakovanja.Text = dataGrid.Rows[row_selected].Cells[1].Value.ToString();
                txt_broj_rn.Text = dataGrid.Rows[row_selected].Cells[2].Value.ToString();
                txt_status_rn.Text = (string)dataGrid.Rows[row_selected].Cells[3].Value;
                txt_vrsta_rn.Text = dataGrid.Rows[row_selected].Cells[4].Value.ToString();
                txt_artikal.Text = dataGrid.Rows[row_selected].Cells[5].Value.ToString();
                txt_kod.Text = (string)dataGrid.Rows[row_selected].Cells[6].Value;
                txt_naziv.Text = (string)dataGrid.Rows[row_selected].Cells[7].Value;
                txt_kolicina.Text = dataGrid.Rows[row_selected].Cells[8].Value.ToString();
                datum.Value = (DateTime)dataGrid.Rows[row_selected].Cells[9].Value;
                txt_termin.Text = dataGrid.Rows[row_selected].Cells[10].Value.ToString();
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = (int)dataGrid.Rows[row_selected].Cells[0].Value;
            PackingPlanItemModel packingPlanItem = new PackingPlanItemModel()
            {
                Id = id,
                IdPlanaPakovanja = Convert.ToInt32(txt_id_plana_pakovanja.Text),
                RnBroj = Convert.ToInt32(txt_broj_rn.Text),
                RnStatus = txt_status_rn.Text,
                RnVrsta = Convert.ToInt32(txt_vrsta_rn.Text),
                Artikal = Convert.ToInt32(txt_artikal.Text),
                Kod = txt_kod.Text,
                Naziv = txt_naziv.Text,
                KolicinaZaProizvodnju = Convert.ToDecimal(txt_kolicina.Text),
                KrajnjiDatum = datum.Value,
                Termin = Convert.ToInt32(txt_termin.Text)
            };
            int resoult = _PackingPlanItem.Update(packingPlanItem);
            // MessageBox.Show(resoult + " row is updated");
            // Refresh the table
            PackingPlanItemList = _PackingPlanItem.SearchByIdSuperior(id_sup);
            dataGrid.DataSource = PackingPlanItemList;
            groupBoxAddEdit.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txt_id_plana_pakovanja.Text = string.Empty;
            txt_broj_rn.Text = string.Empty;
            txt_status_rn.Text = string.Empty;
            txt_vrsta_rn.Text = string.Empty;
            txt_artikal.Text = string.Empty;
            txt_kod.Text = string.Empty;
            txt_naziv.Text = string.Empty;
            txt_kolicina.Text = string.Empty;
            datum.Value = DateTime.Now;
            txt_termin.Text = string.Empty;
            groupBoxAddEdit.Visible = false;
        }

        private void setStyle()
        {
            Color btnBC = Color.FromArgb(225, 143, 4);


            btnSearch.BackColor = btnBC;
            btnAddItem.BackColor = btnBC;
            btnEdit.BackColor = btnBC;
            btnDetails.BackColor = btnBC;
            btnBack.BackColor = btnBC;
            btnUpdate.BackColor = btnBC;
            btnApply.BackColor = btnBC;
            btnCancelAdd.BackColor = btnBC;
            btnInsertStavke.BackColor = btnBC;
            // btnCancel.BackColor = btnBC;
            // btnDelete.BackColor = btnBC;

            Color btnFC = SystemColors.ControlText;

            btnSearch.ForeColor = btnFC;
            btnAddItem.ForeColor = btnFC;
            btnEdit.ForeColor = btnFC;
            btnUpdate.ForeColor = btnFC;
            btnDetails.ForeColor = btnFC;
            btnCancel.ForeColor = btnFC;
            btnBack.ForeColor = btnFC;
            btnApply.ForeColor = btnFC;
            btnInsertStavke.ForeColor = btnFC;
            btnCancelAdd.ForeColor = btnFC;
            btnDelete.ForeColor = btnFC;

            Font btnFont = new Font("Segoe UI Semibold", 10, FontStyle.Bold);

            btnSearch.Font = btnFont;
            btnAddItem.Font = btnFont;
            btnEdit.Font = btnFont;
            btnUpdate.Font = btnFont;
            btnDetails.Font = btnFont;
            btnCancel.Font = btnFont;
            btnCancelAdd.Font = btnFont;
            btnBack.Font = btnFont;
            btnApply.Font = btnFont;
            btnInsertStavke.Font = btnFont;
            btnDelete.Font = btnFont;

            this.BackColor = Color.FromArgb(124, 187, 0);
            this.Font = new Font("Segoe UI", 10);
            //this.Size = new Size(1600, 1000);

            dataGrid.BackgroundColor = Color.FromArgb(125, 158, 192);
            //dataGrid.Size = new Size(1540, 250);
            dataGrid.DefaultCellStyle.SelectionBackColor = btnBC;
            dataGrid.AllowUserToAddRows = false;
            dataGrid.RowHeadersVisible = false;
            dataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGrid1.BackgroundColor = Color.FromArgb(125, 158, 192);
            //dataGrid1.Size = new Size(1000, 250);
            dataGrid1.DefaultCellStyle.SelectionBackColor = btnBC;
            dataGrid1.AllowUserToAddRows = false;
            dataGrid1.RowHeadersVisible = false;
            dataGrid1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGrid1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
/*
            int i = dataGridView1.ColumnCount;
            for (int j = 0; j < i; j++)
            {
                dataGridView1.Columns[j].DefaultCellStyle.BackColor = Color.FromArgb(41, 244, 159);
            }

            //dataGridView1.BackgroundColor = Color.Red;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.MediumVioletRed;*/
        }

        private void txt_UnesiteTermin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsInt(e);
        }

        private void txt_id_plana_pakovanja_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsInt(e);
        }

        private void txt_broj_rn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsInt(e);
        }

        private void txt_vrsta_rn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsInt(e);
        }

        private void txt_artikal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsInt(e);
        }

        private void txt_kolicina_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsDecimal(sender, e);
        }

        private void txt_termin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsInt(e);
        }

    }
}
