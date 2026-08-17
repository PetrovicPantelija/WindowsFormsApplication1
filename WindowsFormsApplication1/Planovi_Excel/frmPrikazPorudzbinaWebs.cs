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
               
                OleDbCommand cmd = new OleDbCommand("Select * from RkShippingExportPorudzbina", conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(dataTableRes);
                dataGridView1.DataSource = dataTableRes;

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

                    ins.InsertRKStavkePOrudzbine(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[2].Value.ToString(), Convert.ToInt32(row.Cells[3].Value.ToString()), row.Cells[4].Value.ToString(), Convert.ToDecimal(row.Cells[5].Value.ToString()), rPom, rPom2, rPom3);

                }
            }
            catch
            {

            }

        }

        int VratiPostojiPorudzbina()
        {
            int Najveci = 0;
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select count(*) as Broj from Narocilo where NaVpisalDat = cAST(gETdATE() as Date)", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Najveci = Convert.ToInt32(dr["Broj"].ToString());
             
                return Najveci;
            }
            return 0;
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (VratiPostojiPorudzbina() > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Vec postoji porudzbina sa danasnjim datumom, da li zelite da je izbrisete pa ucitate ponovo?", "Novi podaci", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    WindowsFormsApplication1.Planovi_Excel.InsertPorudzbine ins = new WindowsFormsApplication1.Planovi_Excel.InsertPorudzbine();
                   try
                    {
                        UbaciRkShiipingPorudzbina();
                        MessageBox.Show("1");
                    } 
                    catch {  }

                    try { UnesiPorudzbinu(); }
                    catch { }
                   

                    // ins.DeleteRkStavkePorudzbine();
                    // UnesiPorudzbinu();
                }
              
            }
            else
            {
                UnesiPorudzbinu();
            }
            SqlConnection conn2 = new SqlConnection(connect);
            var query = "select distinct RelacijaNaziv from RkShippingExportPorudzbine order by RelacijaNaziv asc";
            var da = new SqlDataAdapter(query, conn2);
            var ds = new DataSet();
            da.Fill(ds);
            cboRelacija.DataSource = ds.Tables[0];
            cboRelacija.DisplayMember = "RelacijaNaziv";
            cboRelacija.ValueMember = "RelacijaNaziv";

            var select = "select distinct rk.*,CAST(SUM((MaticniPodatki.MpBrutoTeza*rk.Kolicina))as decimal(18,2))as Tezina " +
                  "From RkShippingExportPorudzbine rk " +
                  "inner join MaticniPodatki on rk.LargoPaketID = MaticniPodatki.MpSifra " +
                  "group by ID,LargoProizvodID,LargoProizvodNaziv,LargoPaketID,LargoPaketNaziv,rk.Kolicina,Relacija,RelacijaNaziv,RelacijaDatum " +
                  "order by ID asc";
           
            var da2 = new SqlDataAdapter(select, conn2);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds2.Tables[0];

           



        }
        private void UnesiPorudzbinu()
        {
            dtpVremeOd.Value = DateTime.Now;

            InsertPorudzbina porudzbina = new InsertPorudzbina();

            porudzbina.InsertPor(dtpVremeOd.Text);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                porudzbina.InsertPorudzbinaPostav(Convert.ToInt32(row.Cells[3].Value.ToString()), Convert.ToDecimal(row.Cells[5].Value.ToString()));

            }
            MessageBox.Show("Zavrsen uvoz porudzbine");


            string NazivBeleske = DateTime.Now.ToString() + '-' + "Porudzbina";
            MessageBox.Show(NazivBeleske);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
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