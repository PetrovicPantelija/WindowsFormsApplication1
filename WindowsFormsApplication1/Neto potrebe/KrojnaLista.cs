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
    public partial class KrojnaLista : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
       int tmpPlanID  = 0;
        string tmpPodPlan = ""; 
        public KrojnaLista()
        {
            InitializeComponent();
        }

        public KrojnaLista(int PlanID, string PodPlan)
        {
            InitializeComponent();
            tmpPlanID = PlanID;
            tmpPodPlan = PodPlan;
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

        private void FillGV()
        {
            var query = @"select NetoPotrebeIgor.ID, NetoPotrebeIGor.IDPlana, Podplan, NetoPotrebeIgor.IDNadredjena,NetoPotrebeIgor.IDZapisa,Mp,MpStaraSif,Kolicina
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
left join MDFGrupe on MdfGrupe.ID = MaticniPodatki.MDFGrupa 
left join MdfPodgrupe on MAticniPodatki.MDFPodgrupa = MDFPodgrupe.ID
where LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " and NetoPotrebeIgor.PodPlan  Like ('%" + txtPodPlan.Text + "%') order by NetoPotrebeIgor.id asc";
            
            
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
            dataGridView1.Columns[1].HeaderText = "IDPlana";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Podplan";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "IDNadredjena";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "IDZapisa";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "MP";
            dataGridView1.Columns[5].Width = 180;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Stara sif";
            dataGridView1.Columns[6].Width = 180;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Kolicina";
            dataGridView1.Columns[7].Width = 100;

        }
        private void KrojnaLista_Load(object sender, EventArgs e)
        {
            FillPlanovi();
            txtPodPlan.Text = tmpPodPlan;
            cboPlan.SelectedValue = tmpPlanID;
            FillGV();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

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
                dataGridView2.Rows[index].Cells[6].Value = dataGridView1.SelectedRows[i].Cells[6].Value.ToString();
                dataGridView2.Rows[index].Cells[7].Value = dataGridView1.SelectedRows[i].Cells[7].Value.ToString();
                // NetoPotrebeIgor.ID,Podplan, NetoPotrebeIgor.IDNadredjena,NetoPotrebeIgor.IDZapisa,Mp,MpStaraSif,Kolicina, Sklad,skNaziv as Skladiste,Lokac,LokOpis,
                //MdfGrupe.Naziv as MDFGrupa,  MDFPodgrupe.Naziv as Podgrupa, MaticniPodatki.Materijal1,MaticniPodatki.Materijal2,MaticniPodatki.Debljina,MaticniPodatki.Tehnologija1,MaticniPodatki.Tehnologija2
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertKrojnaLista isporuka = new InsertKrojnaLista();
           
         
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row != null && row.Cells[0].Value != null)
                {
                    isporuka.InsertKrojna(Convert.ToInt32(row.Cells[5].Value.ToString()),Convert.ToInt32(row.Cells[4].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), Convert.ToDouble(row.Cells[7].Value.ToString()), txtKrojna.Text);
                   
                }
            }
           
            MessageBox.Show("Formirana je krojna lista");
        }
    }
}
