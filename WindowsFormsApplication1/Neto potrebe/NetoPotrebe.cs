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
            FillPlanovi();
        }
        private void FillGV()
        {
            var query = @"select NetoPotrebeIgor.ID,NetoPotrebeIgor.Datum as Datum,NetoPotrebeIgor.IDNadredjena,NetoPotrebeIgor.IDZapisa,Mp,MpStaraSif,Sklad,skNaziv as Skladiste,Lokac,LokOpis,Kolicina,
MdfGrupe.Naziv as MDFGrupa,  MDFPodgrupe.Naziv as Podgrupa 
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
left join MDFGrupe on MdfGrupe.ID = MaticniPodatki.MDFGrupa 
left join MdfPodgrupe on MAticniPodatki.MDFPodgrupa = MDFPodgrupe.ID
where LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue)+ " order by NetoPotrebeIgor.id asc";
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
            var query = @"select ID,NetoPotrebeIgor.Datum as Datum,IDNadredjena,IDZapisa,Mp,MpStaraSif,Sklad,skNaziv,Lokac,LokOpis,Kolicina
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
where LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " and MaticniPodatki.MdfGrupa = " + Convert.ToInt32(cboGrupa.SelectedValue) + " order by id asc";
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
            var query = @"select ID,NetoPotrebeIgor.Datum as Datum,IDNadredjena,IDZapisa,Mp,MpStaraSif,Sklad,skNaziv,Lokac,LokOpis,Kolicina
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
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
    }
}
