using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WinForms;

namespace WindowsFormsApplication1
{
    public partial class frmPrebacivanjeMedjuskladisniPrenos : Form
    {
        bool usao = false;
        public static string code = "frmPrebacivanjeMedjuskladisniPrenos";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;

        string niz = "";
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        int prenSkladiste = 0;
        int prenSkladisteU = 0;
        public frmPrebacivanjeMedjuskladisniPrenos()
        {
            InitializeComponent();
          //  IdGrupe();
           // IdForme();
           // PravoPristupa();
        }

        public frmPrebacivanjeMedjuskladisniPrenos(int SkladistaIz, int SkladisteU,string korisnik)
        {
            InitializeComponent();
            //  IdGrupe();
            // IdForme();
            // PravoPristupa();
            Kor = korisnik;
            txtIzdaje.Text = Kor;
            prenSkladiste = SkladistaIz;
            prenSkladisteU = SkladisteU;
        }
        public string IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            //Sifarnici.frmLogovanje frm = new Sifarnici.frmLogovanje();         
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            int count = 0;

            while (dr.Read())
            {
                if (count == 0)
                {
                    niz = dr["IdGrupe"].ToString();
                    count++;
                }
                else
                {
                    niz = niz + "," + dr["IdGrupe"].ToString();
                    count++;
                }

            }
            conn.Close();
            return niz;
        }
        private int IdForme()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select IdForme from Forme where Rtrim(Code)=" + "'" + code + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idForme = Convert.ToInt32(dr["IdForme"].ToString());
            }
            conn.Close();
            return idForme;
        }

        private void PravoPristupa()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select * From GrupaForma Where IdGrupe in (" + niz + ") and IdForme=" + idForme;
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nemate prava za pristup ovoj formi", code);
                Pravo = false;
            }
            else
            {
                Pravo = true;
                while (reader.Read())
                {
                    insert = Convert.ToBoolean(reader["Upis"]);
                    if (insert == false)
                    {
                        //tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                        //tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                        //tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
        }
        private void frmPrebacivanjeMedjuskladisniPrenos_Load(object sender, EventArgs e)
        {
            var select2 = " Select SkSifra, (Cast(SkSifra as nvarchar(3)) + '-' + SkNaziv) as SkNaziv from Sklad where SkSifra = " + prenSkladiste + " order by SkSifra";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboSkladisteIzbor.DataSource = ds2.Tables[0];
            cboSkladisteIzbor.DisplayMember = "SkNaziv";
            cboSkladisteIzbor.ValueMember = "SkSifra";

            var select3 = " Select SkSifra, (Cast(SkSifra as nvarchar(3)) + '-' + SkNaziv) as SkNaziv from Sklad where SkSifra = " + prenSkladisteU + " order by SkSifra";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection2);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboSkladiste.DataSource = ds3.Tables[0];
            cboSkladiste.DisplayMember = "SkNaziv";
            cboSkladiste.ValueMember = "SkSifra";

            var select = " select LokSifra,  Rtrim(LokOpis) as Opis from Lokac where LokSkSifra = " + prenSkladiste;
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboPozicijaIz.DataSource = ds.Tables[0];
            cboPozicijaIz.DisplayMember = "Opis";
            cboPozicijaIz.ValueMember = "LokSifra";


            var select4 = " select LokSifra, Rtrim(LokOpis) as Opis from Lokac where LokSkSifra = " + prenSkladisteU;
            var s_connectionU = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnectionU = new SqlConnection(s_connectionU);
            var cu = new SqlConnection(s_connectionU);
            var dataAdapterU = new SqlDataAdapter(select4, cu);

            var commandBuilderU = new SqlCommandBuilder(dataAdapterU);
            var dsU = new DataSet();
            dataAdapterU.Fill(dsU);
            cboPozicija.DataSource = dsU.Tables[0];
            cboPozicija.DisplayMember = "Opis";
            cboPozicija.ValueMember = "LokSifra";

            usao = true;
        }

        private void btnOsvezi_Click(object sender, EventArgs e)
        {
           /* select    PrSifSklad['Šif.skl.'], PrLokacija['Lok.'],    PrSifMp['Šif.Mp.'], MpStaraSif['Kod'],    MpNaziv['Ime MP'], MpDoNaziv['Duži naziv MP'],    sum(PrPrevKol - PrIzdKol)['Stv.kol'],    (select sum(ReKolDvig) from Rezervacije where ReSifMp = PrSifMP and ReSifSklad = PrSifSklad and ReLokacija = PrLokacija and ReBrisati = 0)['Rezervacije'],    MPSifEnoteMere1['JM']    from Promet    join MaticniPodatki on MPSifra = PrSifMP Where PrSifSklad =  4 and PrStZapisaVez = 0  group by PrSifSklad, PrLokacija, PrSifM
, MpStaraSif, MpNaziv, MpDoNaziv, MPSifEnoteMere1 having sum(PrPrevKol - PrIzdKol) <> 0 */
            var select = "  select    PrSifSklad, PrLokacija, " +   
           " PrSifMp, MpStaraSif,    MpNaziv, MpDoNaziv, " +  
           " sum(PrPrevKol - PrIzdKol) as Kolicina, " +
           " MPSifEnoteMere1    from Promet   " +
           " join MaticniPodatki on MPSifra = PrSifMP Where PrLokacija = " + Convert.ToInt32(cboPozicijaIz.SelectedValue) + "And PrSifSklad = " + Convert.ToInt32(cboSkladisteIzbor.SelectedValue)  + " and PrStZapisaVez = 0  group by PrSifSklad, PrLokacija" +
           "   ,PrSifMP, MpStaraSif, MpNaziv, MpDoNaziv, MPSifEnoteMere1 having sum(PrPrevKol - PrIzdKol) <> 0 ";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            //string value = dataGridView2.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "Šifra";
            dataGridView1.Columns[0].Width = 40;

        }

        private void btnUbaci_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                int index = dataGridView2.Rows.Add();
                dataGridView2.Rows[index].Cells[0].Value = dataGridView1.SelectedRows[i].Cells[0].Value.ToString();
                dataGridView2.Rows[index].Cells[1].Value = dataGridView1.SelectedRows[i].Cells[1].Value.ToString();
                dataGridView2.Rows[index].Cells[2].Value = dataGridView1.SelectedRows[i].Cells[2].Value.ToString();
                dataGridView2.Rows[index].Cells[3].Value = dataGridView1.SelectedRows[i].Cells[3].Value.ToString();
                dataGridView2.Rows[index].Cells[4].Value = dataGridView1.SelectedRows[i].Cells[4].Value.ToString();
                dataGridView2.Rows[index].Cells[5].Value = dataGridView1.SelectedRows[i].Cells[5].Value.ToString();

            }
            foreach (DataGridViewRow selRow in dataGridView1.SelectedRows.OfType<DataGridViewRow>().ToArray())
            {
                dataGridView1.Rows.Remove(selRow);
                // toDataGridView.Rows.Add(selRow);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertMedjuskladisni isporuka = new InsertMedjuskladisni();
            isporuka.InsertMedju("Prebacivanje MP", "");
            progressBar1.Maximum = dataGridView2.Rows.Count;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row != null && row.Cells[0].Value != null)
                {
                    isporuka.InsertMedjuPostav(Convert.ToInt32(row.Cells[2].Value.ToString()), Convert.ToDecimal(txtKolicina.Text), Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(cboSkladiste.SelectedValue),row.Cells[1].Value.ToString(), cboPozicija.SelectedValue.ToString());
                    progressBar1.Value = progressBar1.Value + 1;
                }
            }
            VratiMSP();
            MessageBox.Show("Formiran je Međuskladišni prenos");
        }

        private void VratiMSP()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select MAX(PrTStDok) as PrTStDok from PrometTrans where PrTTipPrTrans = 'MS'", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtBrojMSP.Text = dr["PrTStDok"].ToString();
            }

            con.Close();
        }


        private void cboSkladiste_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (usao == true)
            {
                var select = " select LokZapSt, Rtrim(LokSifra) + '/' + Rtrim(Opis) from Lokac where LokSkSifra = " + Convert.ToInt32(cboSkladiste.SelectedValue);
                var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                var c = new SqlConnection(s_connection);
                var dataAdapter = new SqlDataAdapter(select, c);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                cboPozicija.DataSource = ds.Tables[0];
                cboPozicija.DisplayMember = "LokSifra";
                cboPozicija.ValueMember = "LokSifra";
            }
            */
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void VratiMSPGrid()
        {
            var select = "   Select PrTpSifSklad as SkladisteIz, " +
                    " PrTpLok as LokIz, BrRN, PrTpSifMp as MP,PrTpStaraSifMp as Kod, " +
                    " PrTpEnotaMere as JM, PrTpPrevKol as Kol, " +
                    " PrTpSifSkladpr as SkladisteU, " +
                    " PrTpLokpr as LokU, PrTpOpomba, PrTpStZapisa as Zapis " +
                    " from PrometTransPos " +
                    " where PrTpStDokumenta = " + Convert.ToInt32(txtBrojMSP.Text) + " and PrTpSifVrDok = 'MSP' ";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView4.ReadOnly = true;
            dataGridView4.DataSource = ds.Tables[0];

            //string value = dataGridView2.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView4.Columns[0];
            dataGridView4.Columns[0].HeaderText = "Sifra";
            dataGridView4.Columns[0].Width = 40;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            VratiMSPGrid();



            Barcodes.Barcode39 b = new Barcodes.Barcode39();

            Magacin.InsertMSPNalepnica MspNal = new Magacin.InsertMSPNalepnica();
            MspNal.DeleteNalepnice();
            //Skupiti sve parametre

            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                MspNal.InsMspNalepnica(Convert.ToInt32(txtBrojMSP.Text),
                    Convert.ToInt32(row.Cells[2].Value.ToString())
                    , row.Cells[4].Value.ToString()
                    , row.Cells[7].Value.ToString()
                   , row.Cells[0].Value.ToString()
                   , Convert.ToInt32(row.Cells[3].Value.ToString())
                    , txtIzdaje.Text
                    , Convert.ToDouble(row.Cells[6].Value.ToString())
                   , row.Cells[9].Value.ToString()
                   , b.GenerateBarcodeImage(row.Cells[3].Value.ToString()),
                   b.GenerateBarcodeImage(txtBrojMSP.Text));
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Magacin.InsertMSPNalepnica MspNal = new Magacin.InsertMSPNalepnica();

            try
            {
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Selected)
                    {
                        MessageBox.Show(row.Cells[3].Value.ToString());
                        MspNal.UpdMspNalepnica(Convert.ToInt32(row.Cells[3].Value.ToString()), Convert.ToInt32(txtBN.Text));
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija ili promena");
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Perftech_BeogradDataSet2TableAdapters.MSPNalepniceTableAdapter ta = new Perftech_BeogradDataSet2TableAdapters.MSPNalepniceTableAdapter();
            Perftech_BeogradDataSet2.MSPNalepniceDataTable dt = new Perftech_BeogradDataSet2.MSPNalepniceDataTable();
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptMSPNal.rdlc";

            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }
    }
}
