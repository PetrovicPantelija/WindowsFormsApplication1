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
using WindowsFormsApplication1.Kalkulacije;

namespace WindowsFormsApplication1.Neto_potrebe
{
    public partial class MpMDF : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        //komentar1
        public MpMDF()
        {
            InitializeComponent();
        }

        private void MpMDF_Load(object sender, EventArgs e)
        {
            FillGV();
            FillGrupe();
        }
        private void FillGV()
        {
            string PrviAnd = "";
            string DrugiAnd = "";
            string TreciEnd = "";
            if (textBoxSearch.Text != "")
            {
                PrviAnd = " and  MpStaraSif like '%" + textBoxSearch.Text + "%' ";
            }
            if (textBoxSearch2.Text != "")
            {
                DrugiAnd = " and  MpStaraSif like '%" + textBoxSearch2.Text + "%' ";
            }
            if (chkGrupa.Checked == true)
            {
                TreciEnd = " and mdfGrupa <> ''  order by MpStaraSif asc";
            }
            string MpAnd = "";
            if (textBoxSearchMP.Text != "")
            {
                MpAnd = " and MaticniPodatki.MpSifra =" + Convert.ToInt32(textBoxSearchMP.Text);
            }



            var query = " Select  MAticniPodatki.MpSifra as Sifra, MpStaraSif as Kod, MpNaziv as NAziv, MpSifEnoteMere1 as JM, MpOsnSklad, MpOsnLokacija, MpStatus,  " +
                " MdfGrupe.Naziv as MDFGrupa,  MDFPodgrupe.Naziv as Podgrupa, MAticniPodatkiIgor.Materijal1, MAticniPodatkiIgor.Materijal2, MAticniPodatkiIgor.Debljina," +
                " MAticniPodatkiIgor.Tehnologija1, MAticniPodatkiIgor.Tehnologija2,ISNull(MdfGrupe.ID,0) as GrupaID,ISNull(MdfPodgrupe.ID,0) as PodgrupaID  from MaticniPodatki " +
                " left join MaticniPodatkiIgor on MaticniPodatkiIgor.MpSifra = MaticniPOdatki.MpSifra " +
                " Left join MDFGrupe on MdfGrupe.ID = MaticniPodatkiIgor.MDFGrupa " +
                " Left join MdfPodgrupe on MAticniPodatkiIgor.MDFPodgrupa = MDFPodgrupe.ID where MpStatus = 0 ";
            query = query + MpAnd+ PrviAnd + DrugiAnd + TreciEnd;
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];


            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "MP sifra";
            dataGridView1.Columns[0].Width = 80;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Kod";
            dataGridView1.Columns[1].Width = 180;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Naziv";
            dataGridView1.Columns[2].Width = 160;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "JM";
            dataGridView1.Columns[3].Width = 40;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Sklad";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Lokac";
            dataGridView1.Columns[5].Width = 80;

            dataGridView1.Columns["GrupaID"].Visible = false;
            dataGridView1.Columns["PodgrupaID"].Visible = false;
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

        private void cboGrupa_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void cboGrupa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillPodgrupe();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillGV();
        }

        private void btnDodeli_Click(object sender, EventArgs e)
        {
            int SifraMp;
            int grupa;
            int podgrupa;
            
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {

                    SifraMp = Convert.ToInt32(row.Cells[0].Value.ToString());
                    grupa = Convert.ToInt32(row.Cells["GrupaID"].Value.ToString());
                    podgrupa = Convert.ToInt32(row.Cells["PodgrupaID"].Value.ToString());
                    
                    InsertVeza program = new InsertVeza();
                    program.InsProgram(Convert.ToInt32(SifraMp), Convert.ToInt32(cboGrupa.SelectedValue), Convert.ToInt32(cboPodgrupa.SelectedValue), txtMaterijal1.Text, txtMaterijal2.Text, txtDebljina.Text, txtTehnologija1.Text, txtTehnologija2.Text);
                }
            }
            FillGV();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FillGV();
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            int SifraMp;
            int grupa;
            int podgrupa;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {

                    SifraMp = Convert.ToInt32(row.Cells[0].Value.ToString());
                    grupa = Convert.ToInt32(row.Cells["GrupaID"].Value.ToString());
                    podgrupa = Convert.ToInt32(row.Cells["PodgrupaID"].Value.ToString());

                    InsertVeza program = new InsertVeza();
                    program.DelProgram(Convert.ToInt32(SifraMp));
                }
            }
            FillGV();
        }

        private void btnPromeni_Click(object sender, EventArgs e)
        {
            int SifraMp;
            int grupa;
            int podgrupa;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {

                    SifraMp = Convert.ToInt32(row.Cells[0].Value.ToString());
                    grupa = Convert.ToInt32(row.Cells["GrupaID"].Value.ToString());
                    podgrupa = Convert.ToInt32(row.Cells["PodgrupaID"].Value.ToString());

                    InsertVeza program = new InsertVeza();
                    program.UpdProgram(Convert.ToInt32(SifraMp), Convert.ToInt32(cboGrupa.SelectedValue), Convert.ToInt32(cboPodgrupa.SelectedValue), txtMaterijal1.Text, txtMaterijal2.Text, txtDebljina.Text, txtTehnologija1.Text, txtTehnologija2.Text);
                }
            }
            FillGV();
        }
    }
}
