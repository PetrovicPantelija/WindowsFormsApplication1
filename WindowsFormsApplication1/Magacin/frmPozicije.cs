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

namespace WindowsFormsApplication1.Magacin
{
    public partial class frmPozicije : Form
    {
        bool status = false;
        public frmPozicije()
        {
            InitializeComponent();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Enabled = false;
            txtSifra.Text = "";
            txtRed.Text = "";
            txtVertikala.Text = "";
            txtCelija.Text = "";
            txtLokSifr.Text = "";
            txtOpis.Text = "";
           // txtMarka.Text = "";
        }

        private void RefreshDataGRid()
        {

            var select = "  select LokZapSt, LokSkSifra, sklad.SkNaziv, LokSifra, Red,Vertikala,Celija,Sirina,LokOpis,Aktivna,  MaxTezina from Lokac " +
            " inner join Sklad on Sklad.SkSifra = Lokac.LokSkSifra " +
            " where sklad.SkSifra = " + Convert.ToInt32(cboSkladiste.SelectedValue);

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "Zapis";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Skladište sifra";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Skladište naziv";
            dataGridView1.Columns[2].Width = 150;


            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Šifra lokacije";
            dataGridView1.Columns[3].Width = 50;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Red";
            dataGridView1.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Vertikala";
            dataGridView1.Columns[5].Width = 50;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Ćelija";
            dataGridView1.Columns[6].Width = 50;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Širina";
            dataGridView1.Columns[7].Width = 50;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Opis";
            dataGridView1.Columns[8].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Aktivna";
            dataGridView1.Columns[9].Width = 50;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Max Težina";
            dataGridView1.Columns[10].Width = 50;

        }

        private void RefreshDataGRidPoRedu()
        {

            var select = "  select LokZapSt, LokSkSifra, sklad.SkNaziv, LokSifra, Red,Vertikala,Celija,Sirina,LokOpis,Aktivna,  MaxTezina from Lokac " +
            " inner join Sklad on Sklad.SkSifra = Lokac.LokSkSifra " +
            " where sklad.SkSifra = " + Convert.ToInt32(cboSkladiste.SelectedValue) + " and Red = " + txtRed.Text.Trim();

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "Zapis";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Skladište sifra";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Skladište naziv";
            dataGridView1.Columns[2].Width = 150;


            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Šifra lokacije";
            dataGridView1.Columns[3].Width = 50;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Red";
            dataGridView1.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Vertikala";
            dataGridView1.Columns[5].Width = 50;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Ćelija";
            dataGridView1.Columns[6].Width = 50;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Širina";
            dataGridView1.Columns[7].Width = 50;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Opis";
            dataGridView1.Columns[8].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Aktivna";
            dataGridView1.Columns[9].Width = 50;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Max Težina";
            dataGridView1.Columns[10].Width = 50;

        }

        private void frmPozicije_Load(object sender, EventArgs e)
        {
             //Skladista
            var select3 = " select SkSifra , (Cast(SkSifra as nvarchar(6))  + '-' + SkNaziv) as Naziv from Sklad order by SkSifra";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboSkladiste.DataSource = ds3.Tables[0];
            cboSkladiste.DisplayMember = "Naziv";
            cboSkladiste.ValueMember = "SkSifra";

            var select4 = " select MpSifra , (Cast(MpSifra as nvarchar(6))  + '-' + MpStaraSif) as Naziv from MaticniPodatki order by MpSifra";
            var s_connection4 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboMaticniPodatak.DataSource = ds4.Tables[0];
            cboMaticniPodatak.DisplayMember = "Naziv";
            cboMaticniPodatak.ValueMember = "MpSifra";
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int Akt = 0;
          if (chkAktivna.Checked == true)
          {
          Akt = 1;
          }
            else
	            {
            Akt = 0;
	            }

            if (status == true)
            {
                InsertPozicije ins = new InsertPozicije();
                ins.InsPozicije(Convert.ToInt32(cboSkladiste.SelectedValue), txtOpis.Text, txtRed.Text, txtVertikala.Text, txtCelija.Text, Convert.ToDouble(txtSirina.Value),  Akt, Convert.ToDouble(txtMaxTezina.Value), Convert.ToInt32(cboMaticniPodatak.SelectedValue));
                status = false;
            }
            else
            {
                InsertPozicije upd = new InsertPozicije();
                //Update
                upd.UpdPozicije(Convert.ToInt32(txtSifra.Text), Convert.ToInt32(cboSkladiste.SelectedValue), txtOpis.Text, txtRed.Text, txtVertikala.Text, txtCelija.Text, Convert.ToDouble(txtSirina.Value), Akt, Convert.ToDouble(txtMaxTezina.Value), Convert.ToInt32(cboMaticniPodatak.SelectedValue));
            }
            RefreshDataGRid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertPozicije ins = new InsertPozicije();
            ins.DeletePozicije(Convert.ToInt32(txtSifra.Text));
            status = false;
            RefreshDataGRid();
        }

        private void VratiPodatke(string ID)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select LokZapSt, LokSkSifra, sklad.SkNaziv, LokSifra, Red,Vertikala,Celija,Sirina,LokOpis,Aktivna, MaxTezina, MaticniPodatak from Lokac " +
            " inner join Sklad on Sklad.SkSifra = Lokac.LokSkSifra where LokZapSt =" + txtSifra.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cboSkladiste.SelectedValue = Convert.ToInt32(dr["LokSkSifra"].ToString());
                txtLokSifr.Text = dr["LokSifra"].ToString();
                txtRed.Text = dr["Red"].ToString();
                txtVertikala.Text = dr["Vertikala"].ToString();
                txtCelija.Text = dr["Celija"].ToString();
                txtSirina.Value = Convert.ToDecimal(dr["Sirina"].ToString());
                txtMaxTezina.Value = Convert.ToDecimal(dr["MaxTezina"].ToString());
                if (dr["Aktivna"].ToString() == "1")
                {
                    chkAktivna.Checked = true;
                }
                else
                {

                    chkAktivna.Checked = false;
                }
                cboMaticniPodatak.SelectedValue = Convert.ToInt32(dr["MaticniPodatak"].ToString());
                txtOpis.Text = dr["LokOpis"].ToString();
            }

            con.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra.Text = row.Cells[0].Value.ToString();
                        VratiPodatke(txtSifra.Text);
                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void txtRed_Leave(object sender, EventArgs e)
        {
            txtOpis.Text = txtRed.Text + '.' + txtVertikala.Text + '.' + txtCelija.Text;
        }

        private void txtVertikala_Leave(object sender, EventArgs e)
        {
            txtOpis.Text = txtRed.Text + '.' + txtVertikala.Text + '.' + txtCelija.Text;
        }

        private void txtCelija_Leave(object sender, EventArgs e)
        {
            txtOpis.Text = txtRed.Text + '.' + txtVertikala.Text + '.' + txtCelija.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGRid();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        InsertPozicije poz = new InsertPozicije();
                        poz.UpdAktivna(Convert.ToInt32(row.Cells[0].Value.ToString()));
                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
             try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        InsertPozicije poz = new InsertPozicije();
                        poz.UpdNeAktivna(Convert.ToInt32(row.Cells[0].Value.ToString()));
                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }

        }

        private void btnOsvezi_Click(object sender, EventArgs e)
        {
            RefreshDataGRidPoRedu();
        }
    }
}
