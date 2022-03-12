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

namespace WindowsFormsApplication1.Kalkulacije
{
    public partial class frmZaglavlja : Form
    {
        public static int IdK;
        int idNovi;
        bool status = false;
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public frmZaglavlja()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            txt_ID.Enabled = false;
        }
        private void FillGV()
        {
            var query = "Select Kalkulator.ID,DatumVazenja,Kalkulator.Naziv,Kurs,VpCenovnik,KalkulacioniCenovnici.Naziv as CenovnikNaziv,Kalkulator.Zavrsen " +
                "From Kalkulator " +
                "Inner join KalkulacioniCenovnici on Kalkulator.VpCenovnik=KalkulacioniCenovnici.ID";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void FillCombo()
        {
            var query = " Select * from KalkulacioniCenovnici";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            combo_VP.DataSource = ds.Tables[0];
            combo_VP.DisplayMember = "Naziv";
            combo_VP.ValueMember = "ID";
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            tsNew.Checked = true;
            tsNew.Enabled = false;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            
            if (status == true)
            {
                Kalkulacije.InsertKalkulator kalk = new InsertKalkulator();
                kalk.InsKalkulator(Convert.ToDateTime(dateTimePicker1.Value), txt_Naziv.Text.ToString().TrimEnd(), Convert.ToDecimal(txt_Kurs.Text), Convert.ToInt32(combo_VP.SelectedValue),0);
                status = false;

                tsNew.Checked = false;
                tsNew.Enabled = true;

                var query = "select top 1 ID from Kalkulator order by ID desc";
                SqlConnection conn = new SqlConnection(connect);
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    idNovi = Convert.ToInt32(dr["ID"].ToString());
                }
                conn.Close();

                Kalkulacije.frmNoviKalkulator novi = new frmNoviKalkulator(idNovi,txt_Naziv.Text.ToString().TrimEnd());
                novi.Show();

            }
            else
            {
                Kalkulacije.InsertKalkulator kalk = new InsertKalkulator();
                kalk.UpdKalkulator(Convert.ToInt32(txt_ID.Text), Convert.ToDateTime(dateTimePicker1.Value), txt_Naziv.Text.ToString().TrimEnd(), Convert.ToDecimal(txt_Kurs.Text),
                    Convert.ToInt32(combo_VP.SelectedValue),0);
            }
            FillGV();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            Kalkulacije.InsertKalkulator kalk = new InsertKalkulator();
            kalk.DelKalkulator(Convert.ToInt32(txt_ID.Text));
            FillGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_ID.Text == "")
            {
                MessageBox.Show("Izaberite cenovnik");
            }
            else
            {
                Kalkulacije.frmKalkulatorStavke stavke = new frmKalkulatorStavke(Convert.ToInt32(txt_ID.Text),txt_Naziv.Text);
                stavke.Show();
            }
        }

        private void combo_VP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txt_ID.Text =row.Cells[0].Value.ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(row.Cells[1].Value.ToString());
                        txt_Naziv.Text =row.Cells[2].Value.ToString();
                        txt_Kurs.Text =row.Cells[3].Value.ToString();
                        combo_VP.Text =row.Cells[5].Value.ToString();
                    }
                }
            }
            catch { }
        }
    }
}
