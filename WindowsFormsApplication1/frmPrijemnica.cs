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
//using ClosedXML.Excel;

using Microsoft.Reporting.WinForms;

namespace WindowsFormsApplication1
{
    public partial class frmPrijemnica : Form
    {
        int usao = 0;
        public static string code = "frmPrijemnica";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        public frmPrijemnica()
        {
            InitializeComponent();
            IdGrupe();
            IdForme();
            PravoPristupa();
        }
        public int IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            //Sifarnici.frmLogovanje frm = new Sifarnici.frmLogovanje();         
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idGrupe = Convert.ToInt32(dr["IdGrupe"].ToString());
            }
            conn.Close();
            return idGrupe;
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
            string query = "Select * From GrupaForma Where IdGrupe=" + idGrupe + " and IdForme=" + idForme;
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
        private void frmPrijemnica_Load(object sender, EventArgs e)
        {
          
        
        }

        private void cboPrijemnica_Leave(object sender, EventArgs e)
        {
            
        }

        private void RefreshDataGrid1()
            {
           
            var select = " select Distinct NPrPPreZap as ID, NPrPSifra as Sifra, MaticniPodatki.MpStaraSif ,NPrPEM as JM, NPrPKolPre as KOL, kolicina2, NPrPSifSkl as Skladiste, " +
               " NPrPLokac  as Lokacija, Lokac.LokOpis from NPreP " +
                " inner join MAticniPodatki on MaticniPodatki.MpSifra = nprep.nPrPSifra " +
                 "    inner Join Lokac on Lokac.LokSifra = nprep. NPrPLokac " +
               " where NPrPStPre = " + Convert.ToInt32(cboPrijemnica.SelectedValue);
           

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
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 30;
                
            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Sifra";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Kod";
            dataGridView1.Columns[2].Width = 130;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "JM";
            dataGridView1.Columns[3].Width = 30;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Kol";
            dataGridView1.Columns[4].Width = 50;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Kol uneta";
            dataGridView1.Columns[5].Width = 50;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Sklad";
            dataGridView1.Columns[6].Width = 50;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Lokac";
            dataGridView1.Columns[7].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Lokac opis";
            dataGridView1.Columns[8].Width = 100;
            
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                decimal pomizmerena = 0;
                if (row.Cells[5].Value.ToString() == "")
                {
                    pomizmerena = 0;
                }
                else if (row.Cells[5].Value.ToString() == "null")
                {
                   pomizmerena = 0; 
                }
                else
                {
                pomizmerena = Convert.ToDecimal(row.Cells[5].Value.ToString());
                }

               decimal Kolicina = Convert.ToDecimal(row.Cells[4].Value.ToString());
             //  decimal Izmerena = Convert.ToDecimal(row.Cells[5].Value.ToString());
               decimal Razlika = Kolicina - pomizmerena;
                if (Razlika  == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.SelectionBackColor = Color.Green;
                }
                else if (Razlika>0)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.SelectionBackColor = Color.Red;
                }
                else if (Razlika < 0)
                {
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                    row.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
                }
            }
             
            dataGridView1.Refresh();

        }

        private void btnPrihvatiIzmene_Click(object sender, EventArgs e)
        {
           
        }

        private void frmPrijemnica_Load_1(object sender, EventArgs e)
        {
            var select2 = " select NprStPre from NPre where NPrStatus = 'OD' order by NPrStPre";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);

            cboPrijemnica.DataSource = ds2.Tables[0];
            cboPrijemnica.DisplayMember = "NprStPre";
            cboPrijemnica.ValueMember = "NprStPre";

            usao = 1;
        }

        private void cboPrijemnica_Leave_1(object sender, EventArgs e)
        {
           // RefreshDataGrid1();
        }

        private void btnPrihvatiIzmene_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    InsertPrijemnica ins = new InsertPrijemnica();
                    ins.UpdatePrijemnicaKOlicina(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToDouble(row.Cells[5].Value.ToString()));

                }
            }

            MessageBox.Show("Završena je izmena stavki");
        }

        private void cboPrijemnica_SelectedIndexChanged(object sender, EventArgs e)
        {
           if (usao == 1)
            { 
            RefreshDataGrid1();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row2 in dataGridView1.Rows)
            {
                if (row2.Selected)
                {
                    InsertPrijemnica ins = new InsertPrijemnica();
                    ins.UpdateKolicina2Prijemnica(Convert.ToInt32(row2.Cells[0].Value.ToString()));
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var select2 = " select NprStPre from NPre where NPrStatus = 'OD' order by NPrStPre";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);

            cboPrijemnica.DataSource = ds2.Tables[0];
            cboPrijemnica.DisplayMember = "NprStPre";
            cboPrijemnica.ValueMember = "NprStPre";

            usao = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (usao == 1)
            {
                RefreshDataGrid1();
            }
        }
        }
    }


