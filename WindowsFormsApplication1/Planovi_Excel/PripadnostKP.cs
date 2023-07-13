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

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class PripadnostKP : Form
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public PripadnostKP()
        {
            InitializeComponent();
        }
        private void PripadnostKP_Load(object sender, EventArgs e)
        {
            FillGV();
            FillCombo();
        }
        private void FillGV()
        {
            var query = "select pk.ID,RTrim(Kolekcije.Naziv) as [Kolekcija],RTrim(Program.Naziv) as [Program],pk.MpSifra,RTrim(MpNaziv) as [Naziv],TipPaketa,Koeficijent " +
                "From PripadnostKolProgram pk " +
                "Inner join Kolekcije on pk.IdKolekcije = Kolekcije.ID " +
                "Inner join Program on pk.IDPrograma = Program.ID " +
                "inner join MaticniPodatki on pk.MpSifra = MaticniPodatki.MpSifra order by pk.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.Columns[0].Width = 50;



            var query2 = "Select MpSifra,MpStaraSif as [Kod],RTrim(MpNaziv) as [Naziv],MpOsnLokacija as [Osnovna lokacija] From MaticniPodatki Where MpStatus=0 order by MpSifra asc";
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds2.Tables[0];
            dataGridView1.Columns[0].Width = 50;
        }
        private void FillCombo()
        {
            var query = "Select ID,Naziv from Program";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboProgram.DataSource = ds.Tables[0];
            cboProgram.DisplayMember = "Naziv";
            cboProgram.ValueMember = "ID";

            var query2 = "Select ID,Naziv from Kolekcije";
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            cboKolekcija.DataSource = ds2.Tables[0];
            cboKolekcija.DisplayMember = "Naziv";
            cboKolekcija.ValueMember = "ID";
        }
        int MpSifra;
        int boja;
        private void Dodela()
        {
            if (checkBox1.Checked == true)
            {
                boja = 1;
            }
            else { boja = 0; }
            InsertPlanovi ins = new InsertPlanovi();
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        MpSifra = Convert.ToInt32(row.Cells[0].Value.ToString());
                        ins.InsertPripadnostKP(Convert.ToInt32(cboKolekcija.SelectedValue), Convert.ToInt32(cboProgram.SelectedValue), MpSifra, boja, Convert.ToDecimal(textBox1.Text.ToString().TrimEnd()));
                    }
                }
                ObrisiDuplirano();
                FillGV();
            }
            catch
            { }
        }
        private void ObrisiDuplirano()
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand("With cte as (Select ID,IDKolekcije,IDPrograma,MpSifra,TipPaketa,ROW_NUMBER() Over(Partition by IDKolekcije,IDPrograma,MpSifra,TipPaketa " +
                "order by IDKolekcije,IDPrograma,MpSifra,TipPaketa) row_num From PripadnostKolProgram) " +
                "delete from cte Where row_num>1;", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dodela();
        }
        int ID;
        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                boja = 1;
            }
            else { boja = 0; }
            InsertPlanovi ins = new InsertPlanovi();
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                        cboKolekcija.SelectedValue = row.Cells[1].Value.ToString();
                        cboProgram.SelectedValue = row.Cells[2].Value.ToString();
                        boja = Convert.ToInt32(row.Cells[5].Value.ToString());
                        if (boja == 1)
                        {
                            checkBox1.Checked = true;
                        }
                        else { checkBox1.Checked = false; }
                        textBox1.Text = row.Cells[6].Value.ToString();

                        ins.UpdatePripadnostKP(ID, Convert.ToInt32(cboKolekcija.SelectedValue), Convert.ToInt32(cboProgram.SelectedValue), Convert.ToInt32(row.Cells[3].Value), boja, Convert.ToDecimal(textBox1.Text.ToString()));
                    }
                }
                ObrisiDuplirano();
                FillGV();
            }
            catch
            { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                        InsertPlanovi ins = new InsertPlanovi();
                        ins.DeletePripadnostKP(ID);
                    }
                }
                FillGV();
            }
            catch
            { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Planovi_Excel.Izlaznost frm = new Izlaznost();
            frm.Show();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (Convert.ToInt32(row.Cells["MpSifra"].Value.ToString()) == Convert.ToInt32(textBox2.Text.ToString()))
                    {
                        row.Selected = true;
                        dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;
                    }
                }
            }
        }
    }
}
