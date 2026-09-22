using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
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
using static WindowsFormsApplication1.Planovi_Excel.PripadnostKP;

namespace WindowsFormsApplication1.Neto_potrebe
{
    public partial class NetoPotrebe : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        //komentar1
        public static string code = "NetoPotrebe";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";

        public NetoPotrebe()
        {
            InitializeComponent();
        
        }

        private void NetoPotrebe_Load(object sender, EventArgs e)
        {
            IdGrupe();
            IdForme();
            PravoPristupa();
            if (Pravo == false)
            {
                this.Close();
                return;
            }

            FillGV();
            FillGrupe();
            FillMAterijal1();
            FillMAterijal2();
            FillDebljina();
            FillTehnologija1();
            FillTehnologija2();
            FillPlanovi();
            FillSkladiste();
        }
        public string IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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
        private void FillGV()
        {

            string wheredodatak = "";

            if (chkRazvrstani.Checked == true)
            {
                wheredodatak += " and NetoPotrebeIgor.PodPlan <> '" + "" + "'";
            }
        


                var query = @"select NetoPotrebeIgor.ID,NetoPotrebeIgor.Datum as Datum,NetoPotrebeIgor.IDNadredjena,NetoPotrebeIgor.IDZapisa,Mp,MpStaraSif,Sklad,skNaziv as Skladiste,Lokac,LokOpis,Kolicina,
MdfGrupe.Naziv as MDFGrupa,  MDFPodgrupe.Naziv as Podgrupa, MaticniPodatkiIgor.Materijal1,MaticniPodatkiIgor.Materijal2,MaticniPodatkiIgor.Debljina,MaticniPodatkiIgor.Tehnologija1,MaticniPodatkiIgor.Tehnologija2, Podplan,
MaticniPodatkiIgor.MdfGrupa as GrupaID,MaticniPodatkiIgor.MdfPodgrupa as PodgrupaID,  NetoPotrebeIGor.RB
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
left join MaticniPodatkiIgor on MAticniPodatkiIgor.MPSifra=MaticniPodatki.MpSifra
left join MDFGrupe on MdfGrupe.ID = MaticniPodatkiIgor.MDFGrupa 
left join MdfPodgrupe on MAticniPodatkiIgor.MDFPodgrupa = MDFPodgrupe.ID
where LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + wheredodatak + " order by NetoPotrebeIgor.Rb, NetoPotrebeIgor.id asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns["GrupaID"].Visible = false;
            dataGridView1.Columns["PodgrupaID"].Visible = false;

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Datum";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Nadredjeni";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Zapis";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "MP";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Kod";
            dataGridView1.Columns[5].Width = 180;

        }

        int filterGrupa = 0;
        private void FillGVGrupa()
        {
           
            var query = @"select NetoPotrebeIgor.ID,NetoPotrebeIgor.Datum as Datum,NetoPotrebeIgor.IDNadredjena,
IDZapisa,Mp,MpStaraSif,Sklad,skNaziv,Lokac,LokOpis,Kolicina, MdfGrupe.Naziv as MDFGrupa,  
MDFPodgrupe.Naziv as Podgrupa, MaticniPodatkiIgor.Materijal1,MaticniPodatkiIgor.Materijal2,MaticniPodatkiIgor.Debljina,
MaticniPodatkiIgor.Tehnologija1,MaticniPodatkiIgor.Tehnologija2, Podplan,  NetoPotrebeIGor.RB
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
left join MaticniPodatkiIgor on MAticniPodatkiIgor.MPSifra=MaticniPodatki.MpSifra
left join mdfgrupe on mdfgrupe.ID = MaticniPodatkiIgor.mdfgrupa
left join mdfpodgrupe on mdfpodgrupe.ID = MaticniPodatkiIgor.mdfpodgrupa";

var where = " where Rtrim(NetoPotrebeIgor.Podplan) = '' and LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " and MaticniPodatkiIgor.MdfGrupa = " + Convert.ToInt32(cboGrupa.SelectedValue) + " order by RB, MpStaraSif asc";

            if (chkRazvrstani.Checked == true)
            {
                where = " where NetoPotrebeIgor.Podplan <> '' and LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " and MaticniPodatkiIgor.MdfGrupa = " + Convert.ToInt32(cboGrupa.SelectedValue) + " order by RB, MpStaraSif asc";
                query = query + where;
                //MessageBox.Show(query.ToString());
            }
            else
            {
                query = query + where;
              //  MessageBox.Show(query.ToString());

            }
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Datum";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Nadredjeni";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Zapis";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "MP";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Kod";
            dataGridView1.Columns[5].Width = 180;
        }

        private void FillGVOsobina()
        {
            string wheredodatak = "";

            if (chkMaterijal1.Checked == true)
            {
                wheredodatak += " and MaticniPodatkiIgor.Materijal1 = '" + cboMaterijal1.Text.ToString() + "'";
            }
            if (chkMaterijal2.Checked == true)
            {
                wheredodatak += " and MaticniPodatkiIgor.Materijal2 = '" + cboMaterijal2.Text.ToString() + "'";
            }
            if (chkDebljina.Checked == true)
            {
                wheredodatak += " and MaticniPodatkiIgor.Debljina = '" + cboDebljina.Text.ToString() + "'";
            }
            if (chkTehnologija1.Checked == true)
            {
                wheredodatak += " and MaticniPodatkiIgor.Tehnologija1 = '" + cboTehnologija1.Text.ToString() + "'";
            }
            if (chkTehnologija2.Checked == true)
            {
                wheredodatak += " and MaticniPodatkiIgor.Tehnologija2 = '" + cboTehnologija2.Text.ToString() + "'";
            }
            if (chkGrupa.Checked == true)
            {
                wheredodatak += " and MaticniPodatkiIgor.MdfGrupa = " + Convert.ToInt32(cboGrupa.SelectedValue);
            }
            if (chkSkladiste.Checked == true)
            {
                wheredodatak += " and NetoPotrebeIGor.Sklad= " + Convert.ToInt32(cboSkladiste.SelectedValue);
            }
            if (chkRazvrstani.Checked == true)
            {
                wheredodatak += " and NetoPotrebeIgor.PodPlan <> '" + "" + "'";
            }
            else
            {
                wheredodatak += " and NetoPotrebeIgor.PodPlan = '" + "" + "'";
            }

                var query = "select NetoPotrebeIgor.ID,NetoPotrebeIgor.Datum as Datum,NetoPotrebeIgor.IDNadredjena,IDZapisa,Mp,MpStaraSif,Sklad,skNaziv,Lokac,LokOpis,Kolicina, " +
    " MdfGrupe.Naziv as MDFGrupa,  MDFPodgrupe.Naziv as Podgrupa, MAticniPodatkiIgor.Materijal1, " +
    " MAticniPodatkiIgor.Materijal2,MAticniPodatkiIgor.Debljina,MAticniPodatkiIgor.Tehnologija1,MAticniPodatkiIgor.Tehnologija2, PodPlan,  NetoPotrebeIGor.RB " +
    " From NetoPotrebeIgor " +
    " inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra " +
    " inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra " +
    " inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra  " +
    " left join MaticniPodatkiIgor on MAticniPodatkiIgor.MPSifra = MaticniPodatki.MpSifra " +
    " left join mdfgrupe on mdfgrupe.ID = MAticniPodatkiIgor.mdfgrupa " +
    " left join mdfpodgrupe on mdfpodgrupe.ID = MAticniPodatkiIgor.mdfpodgrupa " +
    "  where LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + wheredodatak + " order by RB, MpStaraSif asc";
           
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Datum";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Nadredjeni";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Zapis";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "MP";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Kod";
            dataGridView1.Columns[5].Width = 180;
        }

        private void FillGVGrupaPodgrupa()
        {
            var query = @"select ID,NetoPotrebeIgor.Datum as Datum,IDNadredjena,IDZapisa,Mp,MpStaraSif,Sklad,skNaziv,Lokac,LokOpis,Kolicina,
 MdfGrupe.Naziv as MDFGrupa,  MDFPodgrupe.Naziv as Podgrupa, MAticniPodatkiIgor.Materijal1, 
MAticniPodatkiIgor.Materijal2,MAticniPodatkiIgor.Debljina,MAticniPodatkiIgor.Tehnologija1,MAticniPodatkiIgor.Tehnologija2, podplan 
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
left join MaticniPodatkiIgor on MAticniPodatkiIgor.MPSifra = MaticniPodatki.MpSifra 
left join mdfgrupe on mdfgrupe.ID = MAticniPodatkiIgor.mdfgrupa  +
left join mdfpodgrupe on mdfpodgrupe.ID = MAticniPodatkiIgor.mdfpodgrupa +
where LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " and MAticniPodatkiIgor.MdfGrupa = " + Convert.ToInt32(cboGrupa.SelectedValue) + " and MAticniPodatkiIgor.MdfPodgrupa = " + Convert.ToInt32(cboPodgrupa.SelectedValue) + " order by Rb, id asc ";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Datum";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Nadredjeni";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Zapis";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "MP";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Kod";
            dataGridView1.Columns[5].Width = 180;
        }
        private void FillGrupe()
        {
            var query = "Select ID,Naziv From MdfGrupe order by ID asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboGrupa.DataSource = ds.Tables[0];
            cboGrupa.DisplayMember = "Naziv";
            cboGrupa.ValueMember = "ID";


        }

        private void FillSkladiste()
        {
            var query = "Select SkSifra,(Cast(SkSifra as nvarchar(20)) +' - '+ RTrim(SkNaziv)) as SkNaziv From Sklad order by SkSifra asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboSkladiste.DataSource = ds.Tables[0];
            cboSkladiste.DisplayMember = "SkNaziv";
            cboSkladiste.ValueMember = "SkSifra";


        }
        private void FillMAterijal1()
        {
            var query = "Select distinct Materijali1 From MdfMAterijal1 order by Materijali1 asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboMaterijal1.DataSource = ds.Tables[0];
            cboMaterijal1.DisplayMember = "Materijali1";
            cboMaterijal1.ValueMember = "Materijali1";
        }

        private void FillMAterijal2()
        {
            var query = "Select distinct Materijali2 From MdfMAterijal2 order by Materijali2 asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboMaterijal2.DataSource = ds.Tables[0];
            cboMaterijal2.DisplayMember = "Materijali2";
            cboMaterijal2.ValueMember = "Materijali2";
        }

        private void FillDebljina()
        {
            var query = "Select distinct Debljina From MaticniPodatkiIgor order by Debljina asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboDebljina.DataSource = ds.Tables[0];
            cboDebljina.DisplayMember = "Debljina";
            cboDebljina.ValueMember = "Debljina";
        }

        private void FillTehnologija1()
        {
            var query = "Select distinct Tehnologija1 From MdfTehnologija1 order by Tehnologija1 asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboTehnologija1.DataSource = ds.Tables[0];
            cboTehnologija1.DisplayMember = "Tehnologija1";
            cboTehnologija1.ValueMember = "Tehnologija1";
        }

        private void FillTehnologija2()
        {
            var query = "Select distinct Tehnologija2 From MdfTehnologija2 order by Tehnologija2 asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboTehnologija2.DataSource = ds.Tables[0];
            cboTehnologija2.DisplayMember = "Tehnologija2";
            cboTehnologija2.ValueMember = "Tehnologija2";
        }

        private void FillPlanovi()
        {
            var query = "Select ID,Naziv From PlanArhiv order by ID asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboPlan.DataSource = ds.Tables[0];
            cboPlan.DisplayMember = "Naziv";
            cboPlan.ValueMember = "ID";
        }

        private void FillPodgrupe()
        {
            var query = "Select ID,Naziv From MDFPodgrupe Where IDNadredjena=" + Convert.ToInt32(cboGrupa.SelectedValue) + " order by ID asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboPodgrupa.DataSource = ds.Tables[0];
            cboPodgrupa.DisplayMember = "Naziv";
            cboPodgrupa.ValueMember = "ID";
        }

        private void FillComboPodplan()
        {
            var query = "select distinct Podplan From NetoPotrebeIgor where PodPlan is not null and IDPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " order by Podplan asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "Podplan";
            comboBox1.ValueMember = "Podplan";
        }

        private void cboGrupa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillPodgrupe();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            filterGrupa = 0;
            FillGV();
            FillComboPodplan();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filterGrupa = 1;
            FillGVGrupa();
            FillComboPodplan();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FillGVGrupaPodgrupa();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult potvrda = MessageBox.Show(
               "Da li ste sigurni da želite da obrišete NP?",
                "Brisanje NP",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (potvrda != DialogResult.Yes)
            {
                return;

            }
            else
            {

                InsertVeza iv = new InsertVeza();

                iv.ObrisiNetoPotrebe(Convert.ToInt32(cboPlan.SelectedValue));
                FillGV();
            }
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InsertVeza MspNal = new InsertVeza();
          
            //Skupiti sve parametre

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                { 
                 MspNal.PromeniKolicinu(Convert.ToInt32(row.Cells[0].Value.ToString()),
                    Convert.ToDouble(txtNovaKolicina.Text));
                }    
               
            }
           // FillGV();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FillGVOsobina();
            FillComboPodplan();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            InsertVeza MspNal = new InsertVeza();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    MspNal.DefinisiPodplan(Convert.ToInt32(row.Cells[0].Value.ToString()),txtPodPlan.Text);
                      // Convert.ToDouble(txtNovaKolicina.Text));
                }

            }
            FillGV();
            FillComboPodplan();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            KrojnaLista kl = new KrojnaLista(Convert.ToInt32(cboPlan.SelectedValue), txtPodPlan.Text);
            kl.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            PregledPodPlan pp = new PregledPodPlan(Convert.ToInt32(cboPlan.SelectedValue), txtPodPlan.Text);
            pp.Show();
        }

        private void chkRazvrstani_CheckStateChanged(object sender, EventArgs e)
        {
          
            if (filterGrupa == 0)
            {
                FillGV();
            }
            if (filterGrupa == 1)
            {
                FillGVGrupa();
            }
        }
        private int? DgvValue(object value)
        {
            if (value == null || value == DBNull.Value)
                return null;

            if (int.TryParse(value.ToString(), out int result))
                return result;

            return null;
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var row = dataGridView1.SelectedRows[0];

                if (filterGrupa == 0)
                {
                    var m1 = DgvValue(row.Cells["Materijal1"].Value);
                    var m2 = DgvValue(row.Cells["Materijal2"].Value);
                    var d = DgvValue(row.Cells["Debljina"].Value);
                    var t1 = DgvValue(row.Cells["Tehnologija1"].Value);
                    var t2 = DgvValue(row.Cells["Tehnologija2"].Value);
                    var g = DgvValue(row.Cells["MdfGrupa"].Value);
                    var pg = DgvValue(row.Cells["PodPlan"].Value);

                    if (m1.HasValue) cboMaterijal1.SelectedValue = m1.Value;
                    if (m2.HasValue) cboMaterijal2.SelectedValue = m2.Value;
                    if (d.HasValue) cboDebljina.SelectedValue = d.Value;
                    if (t1.HasValue) cboTehnologija1.SelectedValue = t1.Value;
                    if (t2.HasValue) cboTehnologija2.SelectedValue = t2.Value;
                    if (g.HasValue)
                    {
                        cboGrupa.SelectedValue = g.Value;
                        FillPodgrupe();

                        if (pg.HasValue) cboPodgrupa.SelectedValue = pg.Value;
                    }

                    txtPodPlan.Text = row.Cells["Podplan"].Value?.ToString() ?? "";
                }

            }
        }

        private void chkNerazvrstani_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkNerazvrstani.Checked == false)
            {
                FillGV();
            }
            else
            {
                var query = @"select NetoPotrebeIgor.ID,NetoPotrebeIgor.Datum as Datum,NetoPotrebeIgor.IDNadredjena,NetoPotrebeIgor.IDZapisa,Mp,MpStaraSif,Sklad,skNaziv as Skladiste,Lokac,LokOpis,Kolicina,
MdfGrupe.Naziv as MDFGrupa,  MDFPodgrupe.Naziv as Podgrupa, MaticniPodatkiIgor.Materijal1,MaticniPodatkiIgor.Materijal2,MaticniPodatkiIgor.Debljina,MaticniPodatkiIgor.Tehnologija1,MaticniPodatkiIgor.Tehnologija2, Podplan,
MaticniPodatkiIgor.MdfGrupa as GrupaID,MaticniPodatkiIgor.MdfPodgrupa as PodgrupaID , NetoPotrebeIGor.RB
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
left join MaticniPodatkiIgor on MAticniPodatkiIgor.MPSifra = MaticniPodatki.MpSifra 
left join MDFGrupe on MdfGrupe.ID = MaticniPodatkiIgor.MDFGrupa 
left join MdfPodgrupe on MAticniPodatkiIgor.MDFPodgrupa = MDFPodgrupe.ID
where LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " and Podplan='' order by RB, NetoPotrebeIgor.id asc";
                SqlConnection conn = new SqlConnection(connect);
                var da = new SqlDataAdapter(query, conn);
                var ds = new DataSet();
                da.Fill(ds);
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];

                dataGridView1.Columns["GrupaID"].Visible = false;
                dataGridView1.Columns["PodgrupaID"].Visible = false;

                DataGridViewColumn column = dataGridView1.Columns[0];
                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[0].Width = 50;

                DataGridViewColumn column2 = dataGridView1.Columns[1];
                dataGridView1.Columns[1].HeaderText = "Datum";
                dataGridView1.Columns[1].Width = 60;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Nadredjeni";
                dataGridView1.Columns[2].Width = 60;

                DataGridViewColumn column4 = dataGridView1.Columns[3];
                dataGridView1.Columns[3].HeaderText = "Zapis";
                dataGridView1.Columns[3].Width = 60;

                DataGridViewColumn column5 = dataGridView1.Columns[4];
                dataGridView1.Columns[4].HeaderText = "MP";
                dataGridView1.Columns[4].Width = 80;

                DataGridViewColumn column6 = dataGridView1.Columns[5];
                dataGridView1.Columns[5].HeaderText = "Kod";
                dataGridView1.Columns[5].Width = 180;
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtPodPlan.Text = comboBox1.SelectedValue.ToString();

            var query = @"select NetoPotrebeIgor.ID,NetoPotrebeIgor.Datum as Datum,NetoPotrebeIgor.IDNadredjena,NetoPotrebeIgor.IDZapisa,Mp,MpStaraSif,Sklad,skNaziv as Skladiste,Lokac,LokOpis,Kolicina,
MdfGrupe.Naziv as MDFGrupa,  MDFPodgrupe.Naziv as Podgrupa, MaticniPodatkiIgor.Materijal1,MaticniPodatkiIgor.Materijal2,MaticniPodatkiIgor.Debljina,MaticniPodatkiIgor.Tehnologija1,MaticniPodatkiIgor.Tehnologija2, Podplan,
MaticniPodatkiIgor.MdfGrupa as GrupaID,MaticniPodatkiIgor.MdfPodgrupa as PodgrupaID, NetoPotrebeIgor.rb 
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
inner join MaticniPodatkiIgor on MAticniPodatki.MpSifra=MaticniPodatkiIgor.MpSifra
left join MDFGrupe on MdfGrupe.ID = MaticniPodatkiIgor.MDFGrupa 
left join MdfPodgrupe on MaticniPodatkiIgor.MDFPodgrupa = MDFPodgrupe.ID
where LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " and Podplan='"+txtPodPlan.Text+ "' order by NetoPotrebeIgor.rb, NetoPotrebeIgor.id asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns["GrupaID"].Visible = false;
            dataGridView1.Columns["PodgrupaID"].Visible = false;

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Datum";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Nadredjeni";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Zapis";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "MP";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Kod";
            dataGridView1.Columns[5].Width = 180;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Planovi_Excel.PoveziRN prn = new Planovi_Excel.PoveziRN();
            prn.Show();
        }

        private void chkRazvrstani_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRazvrstani.Checked == true)
            { 
            chkNerazvrstani.Checked = false;
            }
               
                   

            if (chkRazvrstani.Checked == false)
            {
                chkNerazvrstani.Checked = true;
            }
        }

        private void chkNerazvrstani_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNerazvrstani.Checked == true)
            {
                chkRazvrstani.Checked = false;
            }



            if (chkNerazvrstani.Checked == false)
            {
                chkRazvrstani.Checked = true;
            }
        }
    }
}
