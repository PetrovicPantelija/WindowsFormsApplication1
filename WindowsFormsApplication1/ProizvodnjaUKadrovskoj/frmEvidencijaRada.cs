using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace WindowsFormsApplication1.ProizvodnjaUKadrovskoj
{
    public partial class frmEvidencijaRada : Form
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        bool status = false;
        int sifraLargo;
        int sektorID = 0;

        public frmEvidencijaRada(int sifraLargo)
        {
            this.sifraLargo = sifraLargo;
            InitializeComponent();
            GetSektor();
            dtDan.Value = DateTime.Now;
        }

        private void dgEvidencija_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgEvidencija.Columns.Contains("ID"))
                dgEvidencija.Columns["ID"].Width = 50;

            if (dgEvidencija.Columns.Contains("BrojSati"))
                dgEvidencija.Columns["BrojSati"].Width = 70;

            if (dgEvidencija.Columns.Contains("Datum"))
                dgEvidencija.Columns["Datum"].Width = 90;
        }
        private void FillComboBoxes()
        {
            var query = "SELECT distinct Naziv,ID FROM TipRada Order By Naziv asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);  

            clbTipaRada.DataSource = ds.Tables[0];
            clbTipaRada.DisplayMember = "Naziv";
            clbTipaRada.ValueMember = "ID";

            var query1 = "SELECT s.Naziv AS Sektor,d.DeSifra AS ID, LTRIM(RTRIM(d.DePriimek)) + ' ' +  LTRIM(RTRIM(d.DeIme)) AS Ime " +
                          "FROM KadroviUProizvodnji kp " +
                          "INNER JOIN Delavci d ON d.deSifra = kp.LargoBroj " +
                          "INNER JOIN Sektor s ON kp.IDSektora = s.ID " +
                          "WHERE deSifStat = 'A' AND kp.IDSektora = " + sektorID +
                          "Order by d.DePriimek,d.DeIme asc";

            var da1 = new SqlDataAdapter(query1, conn);
            var ds1 = new DataSet();
            da1.Fill(ds1);
            clbZaposleni.DataSource = ds1.Tables[0];
            clbZaposleni.DisplayMember = "Ime";
            clbZaposleni.ValueMember = "ID";
        }

        private int GetSektor()
        {
            var query1 = "SELECT IDSektora " +
                         "FROM KadroviUProizvodnji " +
                         "WHERE LargoBroj = " + sifraLargo ;
            SqlConnection conn = new SqlConnection(connect);
            var da1 = new SqlDataAdapter(query1, conn);
            var ds1 = new DataSet();
            da1.Fill(ds1);
            if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
                sektorID = Convert.ToInt32(ds1.Tables[0].Rows[0]["IDSektora"]);
            }
            return sektorID;
        }

        private void clbTipaRada_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked && clbTipaRada.CheckedItems.Count > 0)
            {
                clbTipaRada.ItemCheck -= clbTipaRada_ItemCheck;
                clbTipaRada.SetItemChecked(clbTipaRada.CheckedIndices[0], false);
                clbTipaRada.ItemCheck += clbTipaRada_ItemCheck;
            }
        }

        private void frmEvidencijaRada_Load(object sender, EventArgs e)
        {
            FillComboBoxes();
            dgEvidencija.DataBindingComplete += dgEvidencija_DataBindingComplete;
            FillEvidencija();      
        }

        private void btnSnimi_Click(object sender, EventArgs e)
        {
            int selektovanTipRada = 0;
            int selektovanZaposleni = 0;
            string vecUnetiZaposleni = "";
            int brojVecUnetih = 0;
            InsertEvidencijaRada ks = new InsertEvidencijaRada();
            if (txtBrojSatiRada.Text.Length > 0)
            {
                foreach (int currentIndex in clbTipaRada.CheckedIndices)
                {
                    clbTipaRada.SelectedIndex = currentIndex;
                    if (status == true)
                    {
                        foreach (int currentIndexZ in clbZaposleni.CheckedIndices) 
                        { 
                            clbZaposleni.SelectedIndex = currentIndexZ;
                            clbZaposleni.SetItemChecked(currentIndexZ, false);  
                            var query1 = "SELECT er.LargoBroj, LTRIM(RTRIM(d.DePriimek))+' '+ LTRIM(RTRIM(d.DeIme)) AS ImePrezime " +
                                             "FROM EvidencijaRada er " +
                                             "INNER JOIN Delavci d ON d.deSifra = er.LargoBroj AND d.deSifStat = 'A' " +
                                             "WHERE er.LargoBroj = " + clbZaposleni.SelectedValue + " AND convert(varchar(10), er.Datum, 104) = '" + dtDan.Value.ToString("dd.MM.yyyy") + "'";
                                
                            SqlConnection conn = new SqlConnection(connect); 
                            var da1 = new SqlDataAdapter(query1, conn); 
                            var ds1 = new DataSet();
                            da1.Fill(ds1);  
                            selektovanZaposleni = 1;
                            if (ds1.Tables[0] == null || ds1.Tables[0].Rows.Count == 0)
                            {
                                ks.InsEvidencijaRada(Convert.ToInt32(clbZaposleni.SelectedValue), Convert.ToInt32(clbTipaRada.SelectedValue), (float)Convert.ToDouble(txtBrojSatiRada.Text), dtDan.Value, txtNapomena.Text);
                            }
                            else
                            {
                                if (brojVecUnetih == 0)
                                    vecUnetiZaposleni = vecUnetiZaposleni + " " + ds1.Tables[0].Rows[0]["ImePrezime"].ToString();
                                else
                                    vecUnetiZaposleni = vecUnetiZaposleni + ", " + ds1.Tables[0].Rows[0]["ImePrezime"].ToString();

                                brojVecUnetih++;
                            }
                        }
                        if(brojVecUnetih > 0)
                        MessageBox.Show("Vec ste uneli zaposlenog " + vecUnetiZaposleni + " za ovaj dan!");
                    }
                    else if (status == false && txtSelectedID.Text.Length > 0) 
                    {
                        ks.UpdEvidencijaRada(Convert.ToInt32(txtSelectedID.Text), Convert.ToInt32(clbTipaRada.SelectedValue), (float)Convert.ToDouble(txtBrojSatiRada.Text), txtNapomena.Text);
                    }
                         
                    clbTipaRada.SetItemChecked(currentIndex, false);
                    selektovanTipRada = 1;
                }
                if ((selektovanTipRada == 1 && selektovanZaposleni == 1) || (selektovanTipRada == 1 && status == false))
                {
                    FillEvidencija();
                    status = false;
                    txtBrojSatiRada.Text = "";
                    txtNapomena.Text = "";
                    txtSelectedID.Text = "";
                }
                else if(status == true)
                {
                    MessageBox.Show("Za unos nove evidencije morate označiti zaposlenog i tip rada .");
                }
                else if (status == false)
                {
                    MessageBox.Show("Za ažuriranje vrednosti morate označiti postojeću evdenciju i tip rada.");
                }
            }
            else
            {
                MessageBox.Show("Broj sati je obavezno polje unosa");
            }
        }

        private void FillEvidencija()
        {
            string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string condition = " AND convert(varchar(10), er.Datum, 104) = '" + dtDan.Value.ToString("dd.MM.yyyy") + "'";
            var selectKP = "SELECT  er.ID as idTabele, er.LargoBroj as ID, d.DePriimek AS Prezime, d.DeIme AS Ime, er.BrojSati, tp.Naziv, er.Napomena, " +
                           "CONVERT(varchar(10), er.Datum, 104) as Datum, tp.ID AS TipRadaID " +
                           "FROM EvidencijaRada er " +
                           "INNER JOIN Delavci d ON d.deSifra = er.LargoBroj AND d.deSifStat = 'A' " +
                           "INNER JOIN TipRada tp ON tp.ID = er.TipRadaID " +
                           "INNER JOIN KadroviUProizvodnji kp ON kp.LargoBroj = er.LargoBroj " +
                           "INNER JOIN Sektor s ON kp.IDSektora = s.ID " +
                           "WHERE  s.ID =  " + sektorID + condition +
                           "Order by ID asc";
            SqlConnection conn3 = new SqlConnection(connect);
            var da3 = new SqlDataAdapter(selectKP, conn3);
            var ds3 = new DataSet();
            da3.Fill(ds3);

            dgEvidencija.DataSource = ds3.Tables[0];
            dgEvidencija.ReadOnly = true;
            dgEvidencija.AllowUserToAddRows = false;

           
            if (dgEvidencija.Columns.Contains("TipRadaID"))
            {
                dgEvidencija.Columns["TipRadaID"].Visible = false;
            }
            if (dgEvidencija.Columns.Contains("idTabele"))
            {
                dgEvidencija.Columns["idTabele"].Visible = false;
            }
        }

        private void dtDan_ValueChanged(object sender, EventArgs e)
        {
            FillEvidencija();
        }

        private void btnIzbrisi_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dgEvidencija.Rows)
            {
                InsertEvidencijaRada ks = new InsertEvidencijaRada();
                if (row.Selected)
                {
                    ks.DelEvidencijaRada(Convert.ToInt32(row.Cells["idTabele"].Value.ToString()));
                }
            }
            FillEvidencija();
        }

        private void dgEvidencija_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                foreach (int currentIndexZ in clbZaposleni.CheckedIndices)
                {
                    clbZaposleni.SelectedIndex = currentIndexZ;
                    clbZaposleni.SetItemChecked(currentIndexZ, false);
                }
                txtSelectedID.Text = dgEvidencija.Rows[e.RowIndex].Cells["idTabele"].Value.ToString();
                txtBrojSatiRada.Text = dgEvidencija.Rows[e.RowIndex].Cells["BrojSati"].Value.ToString();
                txtNapomena.Text = dgEvidencija.Rows[e.RowIndex].Cells["Napomena"].Value.ToString();

                foreach (int currentIndex in clbTipaRada.CheckedIndices)
                {
                    clbTipaRada.SelectedIndex = currentIndex;
                    clbTipaRada.SetItemChecked(currentIndex, false);

                }

                int indexOfItem = clbTipaRada.FindStringExact(dgEvidencija.Rows[e.RowIndex].Cells["Naziv"].Value.ToString(), -1);
                if (indexOfItem > -1)
                {
                    clbTipaRada.SetItemChecked(indexOfItem, true);
                    clbTipaRada.SelectedIndex = indexOfItem;
                }
            }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtBrojSatiRada.Text = "";
            txtNapomena.Text = "";
            txtSelectedID.Text = "";
            foreach (int currentIndex in clbTipaRada.CheckedIndices)
            {
                clbTipaRada.SelectedIndex = currentIndex;
                clbTipaRada.SetItemChecked(currentIndex, false);
            }
            foreach (int currentIndexZ in clbZaposleni.CheckedIndices)
            {
                clbZaposleni.SelectedIndex = currentIndexZ;
                clbZaposleni.SetItemChecked(currentIndexZ, false);
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
            {
                for (int i = 0; i < clbZaposleni.Items.Count; i++)
                {
                    clbZaposleni.SetItemChecked(i, chkAll.Checked);
                }
            }
            else if (chkAll.Checked == false) 
            {
                   for (int i = 0; i < clbZaposleni.Items.Count; i++)
                {
                    clbZaposleni.SetItemChecked(i, chkAll.Checked);
                }
            }
        }
   
        private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked && clbTipaRada.CheckedItems.Count > 0)
            {
                clbTipaRada.ItemCheck -= checkedListBox_ItemCheck;
                clbTipaRada.SetItemChecked(clbTipaRada.CheckedIndices[0], false);
                clbTipaRada.ItemCheck += checkedListBox_ItemCheck;
            }
        }
    
    }
}
