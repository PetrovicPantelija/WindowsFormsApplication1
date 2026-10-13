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
using System.Net;
using System.Net.Mail;

using Microsoft.Reporting.WinForms;

namespace WindowsFormsApplication1.Magacin
{
    public partial class frmPlanMedjuskladisni : Form
    {
        bool usao = false;

        public static string code = "frmPlanMedjuskladisni";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public frmPlanMedjuskladisni()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            IdGrupe();
            IdForme();
            PravoPristupa();
        }
        public string IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            //Sifarnici.frmLogovanje frm = new Sifarnici.frmLogovanje();         
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
        private void frmPlanMedjuskladisni_Load(object sender, EventArgs e)
        {
            var select2 = "Select SkSifra, (Cast(SkSifra as nvarchar(3)) + '-' + SkNaziv) as SkNaziv from Sklad order by SkSifra";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboSkladisteIzbor.DataSource = ds2.Tables[0];
            cboSkladisteIzbor.DisplayMember = "SkNaziv";
            cboSkladisteIzbor.ValueMember = "SkSifra";

            var select3 = "Select SkSifra, (Cast(SkSifra as nvarchar(3)) + '-' + SkNaziv) as SkNaziv from Sklad order by SkSifra";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboSkladiste.DataSource = ds3.Tables[0];
            cboSkladiste.DisplayMember = "SkNaziv";
            cboSkladiste.ValueMember = "SkSifra";


            var select5 = " select MpSifra , MpStaraSif from MaticniPodatki order by MpStaraSif";
            var s_connection5 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection5 = new SqlConnection(s_connection5);
            var c5 = new SqlConnection(s_connection5);
            var dataAdapter5 = new SqlDataAdapter(select5, c5);

            var commandBuilder5 = new SqlCommandBuilder(dataAdapter5);
            var ds5 = new DataSet();
            dataAdapter5.Fill(ds5);
            cboMaticni.DataSource = ds5.Tables[0];
            cboMaticni.DisplayMember = "MpStaraSif";
            cboMaticni.ValueMember = "MpSifra";

            var select6 = "  select VpSifra, (Cast(VpSifra as nvarchar(5)) + ' - ' + VpOpis) as VpOpis from VrstePr";
            var s_connection6 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection6 = new SqlConnection(s_connection6);
            var c6 = new SqlConnection(s_connection6);
            var dataAdapter6 = new SqlDataAdapter(select6, c6);

            var commandBuilder6 = new SqlCommandBuilder(dataAdapter6);
            var ds6 = new DataSet();
            dataAdapter6.Fill(ds6);
            cboVPPrijemno.DataSource = ds6.Tables[0];
            cboVPPrijemno.DisplayMember = "VpOpis";
            cboVPPrijemno.ValueMember = "VpSifra";

            var select7 = "  select VpSifra, (Cast(VpSifra as nvarchar(5)) + ' - ' + VpOpis) as VpOpis from VrstePr";
            var s_connection7 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection7 = new SqlConnection(s_connection7);
            var c7 = new SqlConnection(s_connection7);
            var dataAdapter7 = new SqlDataAdapter(select7, c7);

            var commandBuilder7 = new SqlCommandBuilder(dataAdapter7);
            var ds7 = new DataSet();
            dataAdapter7.Fill(ds7);
            cboVPIzdajno.DataSource = ds7.Tables[0];
            cboVPIzdajno.DisplayMember = "VpOpis";
            cboVPIzdajno.ValueMember = "VpSifra";

            usao = true;
        }

        private void btnOsvezi_Click(object sender, EventArgs e)
        {
            var select = "  select   Lokac.Red, PrLokacija as Lokacija, MaticniPodatki.MpSifra, MaticniPodatki.MpStaraSif as Naziv, 0 as Kolicina" +
            " from Promet " +
            " inner Join Lokac on Promet.PrLokacija = Lokac.LokSifra "+
             " inner Join MaticniPodatki on MaticniPodatki.MpSifra = Lokac.LokSifra " +
           " Where Promet.PrSifSklad =  " + Convert.ToInt32(cboSkladisteIzbor.SelectedValue) + "  group " +
            " by Promet.PrSifSklad, Promet.PrLokacija  having sum(Promet.PrPrevKol - Promet.PrIzdKol) = 0 ";
      
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            //string value = dataGridView2.Rows[0].Cells[0].Value.ToString();
           /*
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "Šifra";
            dataGridView1.Columns[0].Width = 40;
            * */
        }

        private void cboSkladiste_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (usao == true)
            {
                var select = " select LokZapSt, LokSifra from Lokac where LokSkSifra = " + Convert.ToInt32(cboSkladiste.SelectedValue);
                var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                var c = new SqlConnection(s_connection);
                var dataAdapter = new SqlDataAdapter(select, c);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                cboPozicija.DataSource = ds.Tables[0];
                cboPozicija.DisplayMember = "LokSifra";
                cboPozicija.ValueMember = "LokSifra";
            }
        }

        private void btnUbaci_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                int index = dataGridView2.Rows.Add();
                dataGridView2.Rows[index].Cells[0].Value = cboSkladisteIzbor.SelectedValue;
                dataGridView2.Rows[index].Cells[1].Value = dataGridView1.SelectedRows[i].Cells[1].Value.ToString();
                dataGridView2.Rows[index].Cells[2].Value = cboSkladiste.SelectedValue;
                dataGridView2.Rows[index].Cells[3].Value = cboPozicija.SelectedValue;
                dataGridView2.Rows[index].Cells[4].Value = cboMaticni.SelectedValue;
                dataGridView2.Rows[index].Cells[5].Value = Convert.ToDouble(txtKolicina.Value);

            }
            foreach (DataGridViewRow selRow in dataGridView1.SelectedRows.OfType<DataGridViewRow>().ToArray())
            {
                dataGridView1.Rows.Remove(selRow);
                // toDataGridView.Rows.Add(selRow);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertMedjuskladisni isporuka = new InsertMedjuskladisni();
            isporuka.InsertMedju2(Convert.ToInt32(txtOperater.Text));
            progressBar1.Maximum = dataGridView2.Rows.Count;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row != null && row.Cells[0].Value != null)
                {
                    isporuka.InsertMedjuPostav2(Convert.ToInt32(row.Cells[2].Value.ToString()), Convert.ToDecimal(txtKolicina.Text), Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(cboSkladiste.SelectedValue), row.Cells[1].Value.ToString(), cboPozicija.SelectedValue.ToString(),cboVPIzdajno.SelectedValue.ToString(), cboVPPrijemno.SelectedValue.ToString());
                    progressBar1.Value = progressBar1.Value + 1;
                }
            }
            MessageBox.Show("Formiran je Međuskladišni prenos");
        }
    }
}
