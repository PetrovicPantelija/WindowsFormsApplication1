using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class frmPrikazPorudzbinaWebs : Form
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public frmPrikazPorudzbinaWebs()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connString =
          ConfigurationManager.AppSettings["webs"];

            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                //Panta E fakture
                // OleDbCommand cmd = new OleDbCommand("SELECT * FROM RkShipping where ExportLargo = false and RkShipping.VoziloIDz  = (Select Min(VoziloID) from TrVozila where TrVozila.RegistarskiBroj = '" + cboVozila.Text + "') and DatumIstovara =  CDate('" + cboDatumUtovara.Text + "')", conn);

                OleDbCommand cmd = new OleDbCommand("Select * from RkShippingExportPorudzbina", conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(dataTableRes);
                dataGridView1.DataSource = dataTableRes;

                /*DataGridViewColumn column = dataGridView1.Columns[0];
                dataGridView1.Columns[0].HeaderText = "Isporuka ID";
                dataGridView1.Columns[0].Width = 50;

                DataGridViewColumn column2 = dataGridView1.Columns[1];
                dataGridView1.Columns[1].HeaderText = "Barkod isporuke";
                dataGridView1.Columns[1].Width = 150;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Datum utovara";
                dataGridView1.Columns[2].Width = 100;

                DataGridViewColumn column4 = dataGridView1.Columns[3];
                dataGridView1.Columns[3].HeaderText = "Datum istovara";
                dataGridView1.Columns[3].Width = 100;

                DataGridViewColumn column5 = dataGridView1.Columns[4];
                dataGridView1.Columns[4].HeaderText = "Broj utovara";
                dataGridView1.Columns[4].Width = 70;

                DataGridViewColumn column6 = dataGridView1.Columns[5];
                dataGridView1.Columns[5].HeaderText = "Broj istovara";
                dataGridView1.Columns[5].Width = 70;

                DataGridViewColumn column7 = dataGridView1.Columns[6];
                dataGridView1.Columns[6].HeaderText = "Broj palete";
                dataGridView1.Columns[6].Width = 70;

                DataGridViewColumn column8 = dataGridView1.Columns[7];
                dataGridView1.Columns[7].HeaderText = "Kupac";
                dataGridView1.Columns[7].Width = 70;

                DataGridViewColumn column9 = dataGridView1.Columns[8];
                dataGridView1.Columns[8].HeaderText = "Salon";
                dataGridView1.Columns[8].Width = 70;

                DataGridViewColumn column10 = dataGridView1.Columns[9];
                dataGridView1.Columns[9].HeaderText = "Vozilo";
                dataGridView1.Columns[9].Width = 70;

                DataGridViewColumn column11 = dataGridView1.Columns[10];
                dataGridView1.Columns[10].HeaderText = "Priključno vozilo";
                dataGridView1.Columns[10].Width = 70;

                DataGridViewColumn column12 = dataGridView1.Columns[11];
                dataGridView1.Columns[11].HeaderText = "Broj paketa";
                dataGridView1.Columns[11].Width = 70;

                DataGridViewColumn column13 = dataGridView1.Columns[12];
                dataGridView1.Columns[12].HeaderText = "Štampa";
                dataGridView1.Columns[12].Width = 70;

                DataGridViewColumn column14 = dataGridView1.Columns[13];
                dataGridView1.Columns[13].HeaderText = "Export Largo";
                dataGridView1.Columns[13].Width = 70;

                DataGridViewColumn column15 = dataGridView1.Columns[14];
                dataGridView1.Columns[14].HeaderText = "Export Largo - Može";
                dataGridView1.Columns[14].Width = 70;
                */
                conn.Close();
                DialogResult dialogResult=MessageBox.Show("Da li želite da presnimite trenutne vrednosti?", "Novi podaci", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    UbaciRkShiipingPorudzbina();
                    var select = "select distinct rk.*,CAST(SUM((MaticniPodatki.MpBrutoTeza*rk.Kolicina))as decimal(18,2))as Tezina " +
                        "From RkShippingExportPorudzbine rk " +
                        "inner join MaticniPodatki on rk.LargoPaketID = MaticniPodatki.MpSifra " +
                        "group by ID,LargoProizvodID,LargoProizvodNaziv,LargoPaketID,LargoPaketNaziv,rk.Kolicina,Relacija,RelacijaNaziv,RelacijaDatum " +
                        "order by ID asc";
                    SqlConnection conn2 = new SqlConnection(connect);
                    var da2 = new SqlDataAdapter(select, conn2);
                    var ds2 = new DataSet();
                    da2.Fill(ds2);
                    dataGridView2.ReadOnly = true;
                    dataGridView2.DataSource = ds2.Tables[0];

                    var query = "select distinct Relacija,RelacijaNaziv from RkShippingExportPorudzbine order by RelacijaNaziv asc";
                    var da = new SqlDataAdapter(query, conn2);
                    var ds = new DataSet();
                    da.Fill(ds);
                    cboRelacija.DataSource = ds.Tables[0];
                    cboRelacija.DisplayMember = "RelacijaNaziv";
                    cboRelacija.ValueMember = "Relacija";
                }
                else if (dialogResult == DialogResult.No)
                {
                    var select = "select distinct rk.*,CAST(SUM((MaticniPodatki.MpBrutoTeza*rk.Kolicina))as decimal(18,2))as Tezina " +
                        "From RkShippingExportPorudzbine rk " +
                        "inner join MaticniPodatki on rk.LargoPaketID = MaticniPodatki.MpSifra " +
                        "group by ID,LargoProizvodID,LargoProizvodNaziv,LargoPaketID,LargoPaketNaziv,rk.Kolicina,Relacija,RelacijaNaziv,RelacijaDatum " +
                        "order by ID asc";
                    SqlConnection conn2 = new SqlConnection(connect);
                    var da2 = new SqlDataAdapter(select, conn2);
                    var ds2 = new DataSet();
                    da2.Fill(ds2);
                    dataGridView2.ReadOnly = true;
                    dataGridView2.DataSource = ds2.Tables[0];

                    var query = "select distinct RelacijaNaziv from RkShippingExportPorudzbine order by RelacijaNaziv asc";
                    var da = new SqlDataAdapter(query, conn2);
                    var ds = new DataSet();
                    da.Fill(ds);
                    cboRelacija.DataSource = ds.Tables[0];
                    cboRelacija.DisplayMember = "RelacijaNaziv";
                    cboRelacija.ValueMember = "RelacijaNaziv";
                }
                else { return; }
            }
        }
        private void UbaciRkShiipingPorudzbina()
        {
            WindowsFormsApplication1.Planovi_Excel.InsertPorudzbine ins = new WindowsFormsApplication1.Planovi_Excel.InsertPorudzbine();
            string rPom = " ";
            string rPom2 = " ";
            string rPom3 = " ";
            try
            {
                ins.DeleteRkStavkePorudzbine();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    if (row.Cells[6].Value.ToString() == "")
                    {
                        rPom = " ";
                    }
                    else
                    {
                        rPom = row.Cells[6].Value.ToString();
                    }


                    if (row.Cells[7].Value.ToString() == "")
                    {
                        rPom2 = " ";
                    }
                    else
                    {
                        rPom2 = row.Cells[7].Value.ToString();
                    }

                    if (row.Cells[8].Value.ToString() == "")
                    {
                        rPom3 = " ";
                    }
                    else
                    {
                        rPom3 = row.Cells[8].Value.ToString();
                    }

                    ins.InsertRKStavkePOrudzbine(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[2].Value.ToString(), Convert.ToInt32(row.Cells[3].Value.ToString()), row.Cells[4].Value.ToString(), Convert.ToInt32(row.Cells[5].Value.ToString()), rPom, rPom2, rPom3);

                }
            }
            catch
            {

            }

        }
        private void iconButton1_Click(object sender, EventArgs e)
        {

            dtpVremeOd.Value = DateTime.Now;

                InsertPorudzbina porudzbina = new InsertPorudzbina();

            porudzbina.InsertPor(dtpVremeOd.Text);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // porudzbina.InsertPorudzbinaPostav(Convert.ToInt32(LargoPaketID), Convert.ToDecimal(Kolicina));
                porudzbina.InsertPorudzbinaPostav(Convert.ToInt32(row.Cells[3].Value.ToString()), Convert.ToInt32(row.Cells[5].Value.ToString()));
               
            }
            MessageBox.Show("Zavrsen uvoz porudzbine");

          
            string NazivBeleske = DateTime.Now.ToString() + '-' + "Porudzbina";
            MessageBox.Show(NazivBeleske);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // porudzbina.InsertPorudzbinaPostav(Convert.ToInt32(LargoPaketID), Convert.ToDecimal(Kolicina));
                porudzbina.InsertBeleskaPostav(NazivBeleske, Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[2].Value.ToString(), Convert.ToInt32(row.Cells[3].Value.ToString()), row.Cells[4].Value.ToString(), Convert.ToDouble(row.Cells[5].Value.ToString()));
               
            }
            MessageBox.Show("Zavrsen uvoz porudzbine");

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var select = "select rk.*,CAST(SUM((MaticniPodatki.MpBrutoTeza*rk.Kolicina))as decimal(18,2))as Tezina " +
                        "From RkShippingExportPorudzbine rk " +
                        "inner join MaticniPodatki on rk.LargoPaketID = MaticniPodatki.MpSifra " +
                        "Where RelacijaNaziv='" + cboRelacija.SelectedValue.ToString() + "' " +
                        "group by ID,LargoProizvodID,LargoProizvodNaziv,LargoPaketID,LargoPaketNaziv,rk.Kolicina,Relacija,RelacijaNaziv,RelacijaDatum " +
                        "order by ID asc";
            SqlConnection conn2 = new SqlConnection(connect);
            var da2 = new SqlDataAdapter(select, conn2);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds2.Tables[0];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var select = "select rk.*,CAST(SUM((MaticniPodatki.MpBrutoTeza*rk.Kolicina))as decimal(18,2))as Tezina " +
                        "From RkShippingExportPorudzbine rk " +
                        "inner join MaticniPodatki on rk.LargoPaketID = MaticniPodatki.MpSifra " +
                        "group by ID,LargoProizvodID,LargoProizvodNaziv,LargoPaketID,LargoPaketNaziv,rk.Kolicina,Relacija,RelacijaNaziv,RelacijaDatum " +
                        "order by ID asc";
            SqlConnection conn2 = new SqlConnection(connect);
            var da2 = new SqlDataAdapter(select, conn2);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds2.Tables[0];

            dataGridView2.Columns[0].Width = 60;
            dataGridView2.Columns[1].Width = 80;
            dataGridView2.Columns[3].Width = 80;
            dataGridView2.Columns[5].Width = 50;
            dataGridView2.Columns[6].Width = 50;

            var query = "select distinct RelacijaNaziv from RkShippingExportPorudzbine order by RelacijaNaziv asc";
            var da = new SqlDataAdapter(query, conn2);
            var ds = new DataSet();
            da.Fill(ds);
            cboRelacija.DataSource = ds.Tables[0];
            cboRelacija.DisplayMember = "RelacijaNaziv";
            cboRelacija.ValueMember = "RelacijaNaziv";

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
           /* var select = "Select ID,LargoProizvodID,LargoProizvodNaziv,LargoPaketID,LargoPaketNaziv,Kolicina,Relacija,RelacijaNaziv,RelacijaDatum," +
                        "Cast((MaticniPodatki.MpTeza * Kolicina)as decimal(18,2)) as Tezina  " +
                        "from RkShippingExportPorudzbine inner join MaticniPodatki on RkShippingExportPorudzbine.LargoProizvodID=MaticniPodatki.MpSifra Where RelacijaNaziv='" + cboRelacija.SelectedValue.ToString()+"' and RelacijaDatum='"+Convert.ToDateTime(dtpVremeOd.Value)+"'";
            SqlConnection conn2 = new SqlConnection(connect);
            var da2 = new SqlDataAdapter(select, conn2);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds2.Tables[0];*/
        }

        private void btnIzvestaj_Click(object sender, EventArgs e)
        {
            IzvestajNeisporuceni frm = new IzvestajNeisporuceni();
            frm.Show();
        }

        private void frmPrikazPorudzbinaWebs_Load(object sender, EventArgs e)
        {
            
        }
    }
}