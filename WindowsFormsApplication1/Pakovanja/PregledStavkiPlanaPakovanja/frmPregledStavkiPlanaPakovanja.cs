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

namespace WindowsFormsApplication1.Pakovanja.PregledStavkiPlanaPakovanja
{
    public partial class frmPregledStavkiPlanaPakovanja : Form
    {
        private PackingPlanItemOverviewDAO _PackingPlanItemOverview = new PackingPlanItemOverviewDAO();
        private List<PackingPlanItemOverviewModel> PackingPlanItemOverviewList = new List<PackingPlanItemOverviewModel>();

        int id_sup, id_sup_sup;
        int row_selected;
        public frmPregledStavkiPlanaPakovanja(int id, int id_s)
        {
            InitializeComponent();
            setStyle();
            id_sup = id;
            id_sup_sup = id_s;
        }

        private void frmPregledStavkiPlanaPakovanja_Load(object sender, EventArgs e)
        {
            groupBoxAddEdit.Visible = false;
            PackingPlanItemOverviewList = _PackingPlanItemOverview.GetAllByIdSuperior(id_sup);
            // connect the list  to the grid view
            dataGrid.DataSource = PackingPlanItemOverviewList;
            dataGrid.AllowUserToAddRows = false;
            dataGrid.RowHeadersVisible = false;
            dataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (btnSearch.Text == "Search")
            {
                if (string.IsNullOrEmpty(textBoxSearch.Text))
                {
                    PackingPlanItemOverviewList = _PackingPlanItemOverview.GetAllByIdSuperior(id_sup);
                }
                else
                {
                    PackingPlanItemOverviewList = _PackingPlanItemOverview.Search(textBoxSearch.Text, id_sup);
                    btnSearch.Text = "Show All";
                }
                dataGrid.DataSource = PackingPlanItemOverviewList;
            }
            else
            {
                PackingPlanItemOverviewList = _PackingPlanItemOverview.GetAllByIdSuperior(id_sup);
                dataGrid.DataSource = PackingPlanItemOverviewList;
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
                    int resoult = _PackingPlanItemOverview.Delete(id);
                    // MessageBox.Show(resoult + " row(s) is deleted");

                    // Refresh the table
                    PackingPlanItemOverviewList = _PackingPlanItemOverview.GetAllByIdSuperior(id_sup);
                    dataGrid.DataSource = PackingPlanItemOverviewList;
                }
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGrid.Rows.Count != 0)
            {
                groupBoxAddEdit.Visible = true;
                row_selected = dataGrid.CurrentRow.Index;

                txt_id_stavke_plana_pakovanja.Text = dataGrid.Rows[row_selected].Cells[1].Value.ToString();
                txt_rn.Text = dataGrid.Rows[row_selected].Cells[2].Value.ToString();
                txt_status_stavke.Text = (string)dataGrid.Rows[row_selected].Cells[3].Value;
                txt_artikal_paket.Text = dataGrid.Rows[row_selected].Cells[4].Value.ToString();
                txt_količina_za_proizvodnju.Text = dataGrid.Rows[row_selected].Cells[5].Value.ToString();
                txt_artikal_iz_prvog_nivoa_sastavnice.Text = dataGrid.Rows[row_selected].Cells[6].Value.ToString();
                txt_potrebna_količina.Text = dataGrid.Rows[row_selected].Cells[7].Value.ToString();
                txt_trenutna_količina_na_zalihama.Text = dataGrid.Rows[row_selected].Cells[8].Value.ToString();
                txt_rezervisana_količina_po_planu.Text = dataGrid.Rows[row_selected].Cells[9].Value.ToString();
                txt_ukucati_količinu_za_rezervaciju.Text = dataGrid.Rows[row_selected].Cells[10].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = (int)dataGrid.Rows[row_selected].Cells[0].Value;
            PackingPlanItemOverviewModel packingPlanItem = new PackingPlanItemOverviewModel()
            {
                Id = id,
                IdStavkePlanaPakovanja = Convert.ToInt32(txt_id_stavke_plana_pakovanja.Text),
                RN = Convert.ToInt32(txt_rn.Text),
                StatusStavke = txt_status_stavke.Text,
                ArtikalPaket = Convert.ToInt32(txt_artikal_paket.Text),
                KolicinaZaProizvodnju = Convert.ToDecimal(txt_količina_za_proizvodnju.Text),
                ArtikalIzPrvogNivoaSastavnice = Convert.ToInt32(txt_artikal_iz_prvog_nivoa_sastavnice.Text),
                PotrebnaKolicina = Convert.ToDecimal(txt_potrebna_količina.Text),
                TrenutnaKolicinaNaZalihama = Convert.ToDecimal(txt_trenutna_količina_na_zalihama.Text),
                RezervisanaKolicinaPoPlanu = Convert.ToDecimal(txt_rezervisana_količina_po_planu.Text),
                UkucatiKolicinuZaRezervaciju = Convert.ToDecimal(txt_ukucati_količinu_za_rezervaciju.Text)
            };
            int resoult = _PackingPlanItemOverview.Update(packingPlanItem);
            // MessageBox.Show(resoult + " row is updated");
            // Refresh the table
            PackingPlanItemOverviewList = _PackingPlanItemOverview.GetAllByIdSuperior(id_sup);
            dataGrid.DataSource = PackingPlanItemOverviewList;
            groupBoxAddEdit.Visible = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmStavkePlanaPakovanja stavke = new frmStavkePlanaPakovanja(id_sup_sup);
            this.Hide();
            stavke.Show();
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            txt_id_stavke_plana_pakovanja.Text = string.Empty;
            txt_rn.Text = string.Empty;
            txt_status_stavke.Text = string.Empty;
            txt_artikal_paket.Text = string.Empty;
            txt_količina_za_proizvodnju.Text = string.Empty;
            txt_artikal_iz_prvog_nivoa_sastavnice.Text = string.Empty;
            txt_potrebna_količina.Text = string.Empty;
            txt_trenutna_količina_na_zalihama.Text = string.Empty;
            txt_rezervisana_količina_po_planu.Text = string.Empty;
            txt_ukucati_količinu_za_rezervaciju.Text = string.Empty;
            groupBoxAddEdit.Visible = false;
        }

        private void setStyle()
        {
            Color btnBC = Color.FromArgb(225, 143, 4);

            btnSearch.BackColor = btnBC;
            btnEdit.BackColor = btnBC;
            btnUpdate.BackColor = btnBC;
            // btnCancel.BackColor = btnBC;
            // btnDelete.BackColor = btnBC;

            Color btnFC = SystemColors.ControlText;

            btnSearch.ForeColor = btnFC;
            btnEdit.ForeColor = btnFC;
            btnUpdate.ForeColor = btnFC;
            btnCancel.ForeColor = btnFC;
            btnDelete.ForeColor = btnFC;

            Font btnFont = new Font("Segoe UI Semibold", 10, FontStyle.Bold);

            btnSearch.Font = btnFont;
            btnEdit.Font = btnFont;
            btnUpdate.Font = btnFont;
            btnCancel.Font = btnFont;
            btnDelete.Font = btnFont;

            this.BackColor = Color.FromArgb(124, 187, 0);
            this.Font = new Font("Segoe UI", 10);
            this.Size = new Size(1580, 1000);

            dataGrid.BackgroundColor = Color.FromArgb(125, 158, 192);
            dataGrid.Size = new Size(1540, 240);
            dataGrid.DefaultCellStyle.SelectionBackColor = btnBC;
        }

        private void txt_id_stavke_plana_pakovanja_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsInt(e);
        }

        private void txt_rn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsInt(e);
        }

        private void txt_artikal_paket_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsInt(e);
        }

        private void txt_artikal_iz_prvog_nivoa_sastavnice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsInt(e);
        }

        private void txt_količina_za_proizvodnju_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsDecimal(sender, e);
        }

        private void txt_potrebna_količina_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsDecimal(sender, e);
        }

        private void txt_trenutna_količina_na_zalihama_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsDecimal(sender, e);
        }

        private void txt_rezervisana_količina_po_planu_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsDecimal(sender, e);
        }


        private void txt_ukucati_količinu_za_rezervaciju_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsDecimal(sender, e);
        }


    }
}
