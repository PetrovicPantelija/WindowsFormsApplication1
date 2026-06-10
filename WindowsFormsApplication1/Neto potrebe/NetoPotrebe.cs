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

namespace WindowsFormsApplication1.Neto_potrebe
{
    public partial class NetoPotrebe : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        //komentar1
        public NetoPotrebe()
        {
            InitializeComponent();
        }

        private void NetoPotrebe_Load(object sender, EventArgs e)
        {
            FillGV();
            FillGrupe();
            FillMAterijal1();
            FillMAterijal2();
            FillDebljina();
            FillTehnologija1();
            FillTehnologija2();
            FillPlanovi();
        }
        private void FillGV()
        {

            string wheredodatak = "";

            if (chkRazvrstani.Checked == true)
            {
                wheredodatak += " and NetoPotrebeIgor.PodPlan <> '" + "" + "'";
            }

            var query = @"select NetoPotrebeIgor.ID,NetoPotrebeIgor.Datum as Datum,NetoPotrebeIgor.IDNadredjena,NetoPotrebeIgor.IDZapisa,Mp,MpStaraSif,Sklad,skNaziv as Skladiste,Lokac,LokOpis,Kolicina,
MdfGrupe.Naziv as MDFGrupa,  MDFPodgrupe.Naziv as Podgrupa, MaticniPodatki.Materijal1,MaticniPodatki.Materijal2,MaticniPodatki.Debljina,MaticniPodatki.Tehnologija1,MaticniPodatki.Tehnologija2, Podplan 
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
left join MDFGrupe on MdfGrupe.ID = MaticniPodatki.MDFGrupa 
left join MdfPodgrupe on MAticniPodatki.MDFPodgrupa = MDFPodgrupe.ID
where LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + wheredodatak +  " order by NetoPotrebeIgor.id asc";
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

        private void FillGVGrupa()
        {
            var query = @"select NetoPotrebeIgor.ID,NetoPotrebeIgor.Datum as Datum,NetoPotrebeIgor.IDNadredjena,IDZapisa,Mp,MpStaraSif,Sklad,skNaziv,Lokac,LokOpis,Kolicina, MdfGrupe.Naziv as MDFGrupa,  MDFPodgrupe.Naziv as Podgrupa, MaticniPodatki.Materijal1,MaticniPodatki.Materijal2,MaticniPodatki.Debljina,MaticniPodatki.Tehnologija1,MaticniPodatki.Tehnologija2, Podplan
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
left join mdfgrupe on mdfgrupe.ID = MaticniPodatki.mdfgrupa
left join mdfpodgrupe on mdfpodgrupe.ID = MaticniPodatki.mdfpodgrupa 
where LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " and MaticniPodatki.MdfGrupa = " + Convert.ToInt32(cboGrupa.SelectedValue) + " order by MpStaraSif asc";
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
                wheredodatak += " and MaticniPodatki.Materijal1 = '" + cboMaterijal1.Text.ToString() + "'";
            }
            if (chkMaterijal2.Checked == true)
            {
                wheredodatak += " and MaticniPodatki.Materijal2 = '" + cboMaterijal2.Text.ToString() + "'";
            }
            if (chkDebljina.Checked == true)
            {
                wheredodatak += " and MaticniPodatki.Debljina = '" + cboDebljina.Text.ToString() + "'";
            }
            if (chkTehnologija1.Checked == true)
            {
                wheredodatak += " and MaticniPodatki.Tehnologija1 = '" + cboTehnologija1.Text.ToString() + "'";
            }
            if (chkTehnologija2.Checked == true)
            {
                wheredodatak += " and MaticniPodatki.Tehnologija2 = '" + cboTehnologija2.Text.ToString() + "'";
            }
            if (chkGrupa.Checked == true)
            {
                wheredodatak += " and MaticniPodatki.MdfGrupa = " + Convert.ToInt32(cboGrupa.SelectedValue);
            }

            var query = "select NetoPotrebeIgor.ID,NetoPotrebeIgor.Datum as Datum,NetoPotrebeIgor.IDNadredjena,IDZapisa,Mp,MpStaraSif,Sklad,skNaziv,Lokac,LokOpis,Kolicina, " +
" MdfGrupe.Naziv as MDFGrupa,  MDFPodgrupe.Naziv as Podgrupa, MaticniPodatki.Materijal1, " +
" MaticniPodatki.Materijal2,MaticniPodatki.Debljina,MaticniPodatki.Tehnologija1,MaticniPodatki.Tehnologija2, PodPlan " +
" From NetoPotrebeIgor " +
" inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra " +
" inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra " +
" inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra  " +
" left join mdfgrupe on mdfgrupe.ID = MaticniPodatki.mdfgrupa " +
" left join mdfpodgrupe on mdfpodgrupe.ID = MaticniPodatki.mdfpodgrupa " +
"  where LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + wheredodatak + " order by MpStaraSif asc";
           
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
 MdfGrupe.Naziv as MDFGrupa,  MDFPodgrupe.Naziv as Podgrupa, MaticniPodatki.Materijal1, 
MaticniPodatki.Materijal2,MaticniPodatki.Debljina,MaticniPodatki.Tehnologija1,MaticniPodatki.Tehnologija2, podplan 
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
left join mdfgrupe on mdfgrupe.ID = MaticniPodatki.mdfgrupa  +
left join mdfpodgrupe on mdfpodgrupe.ID = MaticniPodatki.mdfpodgrupa +
where LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " and MaticniPodatki.MdfGrupa = " + Convert.ToInt32(cboGrupa.SelectedValue) + " and MaticniPodatki.MdfPodgrupa = " + Convert.ToInt32(cboPodgrupa.SelectedValue) + " order by id asc ";
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

        private void FillMAterijal1()
        {
            var query = "Select distinct Materijal1 From MaticniPodatki order by Materijal1 asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboMaterijal1.DataSource = ds.Tables[0];
            cboMaterijal1.DisplayMember = "Materijal1";
            cboMaterijal1.ValueMember = "Materijal1";
        }

        private void FillMAterijal2()
        {
            var query = "Select distinct Materijal2 From MaticniPodatki order by Materijal2 asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboMaterijal2.DataSource = ds.Tables[0];
            cboMaterijal2.DisplayMember = "Materijal2";
            cboMaterijal2.ValueMember = "Materijal2";
        }

        private void FillDebljina()
        {
            var query = "Select distinct Debljina From MaticniPodatki order by Debljina asc";
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
            var query = "Select distinct Tehnologija1 From MaticniPodatki order by Tehnologija1 asc";
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
            var query = "Select distinct Tehnologija2 From MaticniPodatki order by Tehnologija2 asc";
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

        private void cboGrupa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillPodgrupe();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FillGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillGVGrupa();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FillGVGrupaPodgrupa();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InsertVeza iv = new InsertVeza();
           
                iv.ObrisiNetoPotrebe(Convert.ToInt32(cboPlan.SelectedValue));
                FillGV();
           
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
    }
}
