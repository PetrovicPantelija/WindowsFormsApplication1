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
using System.IO;

namespace WindowsFormsApplication1.Magacin
{
    public partial class frmPlanImportNeto : Form
    {
        int usao = 0;
        string KorisnikM = "";
        int ReadNabavkaM = 0;
        public static string code = "frmPlanImportNeto";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        public frmPlanImportNeto()
        {
            InitializeComponent();
            IdGrupe();
            IdForme();
            PravoPristupa();
        }

        public frmPlanImportNeto(string Korisnik, int ReadNabavka)
        {
            IdGrupe();
            IdForme();
            PravoPristupa();
            InitializeComponent();
            KorisnikM = Korisnik;
            ReadNabavkaM = ReadNabavka;
            if (ReadNabavkaM == 1)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button8.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button8.Enabled = true;
            }
            
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
        private void frmPlanImportNeto_Load(object sender, EventArgs e)
        {
            var select2 = " Select SkSifra, (Cast(SkSifra as nvarchar(3)) + '-' + SkNaziv) as SkNaziv from Sklad order by SkSifra";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboSkladiste1.DataSource = ds2.Tables[0];
            cboSkladiste1.DisplayMember = "SkNaziv";
            cboSkladiste1.ValueMember = "SkSifra";


            //Planovi
            var select = " select Distinct PlanNaziv from PlanStavkeNeto";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            txtPlan.DataSource = ds.Tables[0];
            txtPlan.DisplayMember = "PlanNaziv";
            txtPlan.ValueMember = "PlanNaziv";

            RefreshDataGridSkladista();
        }
       
        private void BindData(string filePath)
        {
            DataTable dt = new DataTable();
            string[] lines = System.IO.File.ReadAllLines(filePath);
            if (lines.Length > 0)
            {
                //first line to create header
                string firstLine = lines[0];
                string[] headerLabels = firstLine.Split(';');
                foreach (string headerWord in headerLabels)
                {
                    dt.Columns.Add(new DataColumn(headerWord));
                }
                //For Data
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] dataWords = lines[i].Split(';');
                    DataRow dr = dt.NewRow();
                    int columnIndex = 0;
                    foreach (string headerWord in headerLabels)
                    {
                        dr[headerWord] = dataWords[columnIndex++];
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }

        }

        private void btnImeFajla_Click(object sender, EventArgs e)
        {
            string PictureFolder = txtPutanja.Text;
            ofd1.InitialDirectory = PictureFolder;
            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                txtPutanja.Text = fbd.SelectedPath.ToString() + ofd1.FileName;
            }
            usao = 0;
            BindData(txtPutanja.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertPlan isporuka = new InsertPlan();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row != null && row.Cells[0].Value != null)
                {
                    isporuka.InsertPlanStavkeNeto(txtPlan.Text, Convert.ToInt32(row.Cells[0].Value.ToString()), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(),  Convert.ToDouble(row.Cells[3].Value.ToString()), Convert.ToInt32(row.Cells[4].Value.ToString()),row.Cells[5].Value.ToString(),row.Cells[6].Value.ToString(),row.Cells[7].Value.ToString(),row.Cells[8].Value.ToString(),0,0);
                }
            }
            MessageBox.Show("Formiran je plan");
            //RefreshDataGrid();
        }

        private void RefreshDataGridSkladista()
        {
            var select = " select SkSifra, SkNaziv from sklad ";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];
            
            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "Šifra";
            dataGridView2.Columns[0].Width = 60;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Naziv";
            dataGridView2.Columns[1].Width = 150;
            /*
            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Skladište 1";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Skladište 2";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Stanje";
            dataGridView1.Columns[4].Width = 60;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Isporuka 1";
            dataGridView1.Columns[5].Width = 60;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Raspoloživo 1";
            dataGridView1.Columns[6].Width = 60;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Isporuka 2";
            dataGridView1.Columns[7].Width = 60;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Raspoloživo 2";
            dataGridView1.Columns[8].Width = 60;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Isporuka 3";
            dataGridView1.Columns[9].Width = 60;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Raspoloživo 3";
            dataGridView1.Columns[10].Width = 60;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Isporuka 4";
            dataGridView1.Columns[11].Width = 60;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Raspoloživo 4";
            dataGridView1.Columns[12].Width = 60;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Isporuka 5";
            dataGridView1.Columns[13].Width = 60;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Raspoloživo 5";
            dataGridView1.Columns[14].Width = 60;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Nedostaje";
            dataGridView1.Columns[15].Width = 70;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Izdato RN";
            dataGridView1.Columns[16].Width = 70;

            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Potrebno izdati RN";
            dataGridView1.Columns[17].Width = 70;
            */
            PromeniBoju();
        }
        
        private void RefreshDataGrid()
        {
            var select = " SELECT distinct [ID],[PlanNaziv],[MPSifra],[MPStaraSif] " +
             " ,[JM] ,[Kolicina] ,[RedniBroj],[Grupa] " +
             " ,[Napomena] ,[LokSkSifra] ,[LokOpis],[Termin] " +
             " ,[MAXTermin]  FROM [Jela_test].[dbo].[PlanStavkeNeto] where [LokSkSifra] = '" +
             + Convert.ToInt32(cboSkladiste1.SelectedValue) + " ' and PlanStavkeNeto.[PlanNaziv] = '" + txtPlan.Text + "'" + " and  PlanStavke.[PlanNaziv]  = '" + txtPlan.Text + "'";
            
            

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            /*
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "MP";
            dataGridView1.Columns[0].Width = 60;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Mp kod";
            dataGridView1.Columns[1].Width = 150;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Skladište 1";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Skladište 2";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Stanje";
            dataGridView1.Columns[4].Width = 60;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Isporuka 1";
            dataGridView1.Columns[5].Width = 60;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Raspoloživo 1";
            dataGridView1.Columns[6].Width = 60;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Isporuka 2";
            dataGridView1.Columns[7].Width = 60;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Raspoloživo 2";
            dataGridView1.Columns[8].Width = 60;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Isporuka 3";
            dataGridView1.Columns[9].Width = 60;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Raspoloživo 3";
            dataGridView1.Columns[10].Width = 60;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Isporuka 4";
            dataGridView1.Columns[11].Width = 60;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Raspoloživo 4";
            dataGridView1.Columns[12].Width = 60;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Isporuka 5";
            dataGridView1.Columns[13].Width = 60;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Raspoloživo 5";
            dataGridView1.Columns[14].Width = 60;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Nedostaje";
            dataGridView1.Columns[15].Width = 70;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Izdato RN";
            dataGridView1.Columns[16].Width = 70;

            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Potrebno izdati RN";
            dataGridView1.Columns[17].Width = 70;
            */
            PromeniBoju();
        }

        private void RefreshDataGridRazlika()
        {
            usao = 1;
            var select = "  SELECT distinct PlanStavkeNeto.[ID],PlanStavkeNeto.[PlanNaziv], " +
            " PlanStavkeNeto.[MPSifra],PlanStavkeNeto.[RN],PlanStavkeNeto.[StatusRN], PlanStavkeNeto.[MPStaraSif] " +
            "  ,PlanStavkeNeto.[JM] ,PlanStavkeNeto.[Kolicina] ,PlanStavkeNeto.[RedniBroj], " +
            "  PlanStavkeNeto.[Grupa] " +
            "  ,PlanStavkeNeto.[Napomena] ,PlanStavkeNeto.[LokSkSifra] , " +
            "  PlanStavkeNeto.[LokOpis],PlanStavkeNeto.[Termin] " +
            "  ,PlanStavkeNeto.[MAXTermin], " +
            "  PlanStavkeNeto.[TerminUvoz],PlanStavkeNeto.[TerminMaxPocetni] , " +
            "  PlanStavkeNeto.[DatumZadnjePromene]  " +
            "  FROM PlanStavkeNeto " +
            "  inner join PlanStavke on PlanStavkeNeto.MPSifra = PlanStavke.MPSifra " +
            "  where PlanStavkeNeto.[LokSkSifra] = '" + Convert.ToInt32(cboSkladiste1.SelectedValue) + "' and PlanStavkeNeto.[PlanNaziv] = '" + txtPlan.Text + "'" + " and  PlanStavke.[PlanNaziv]  = '" + txtPlan.Text + "'" +
            "  order by PlanStavkeNeto.[DatumZadnjePromene] desc ";
            

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
            dataGridView1.Columns[0].Width = 60;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Plan";
            dataGridView1.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Mp sifra";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "RN";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "RN Status";
            dataGridView1.Columns[4].Width = 60;


            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Mp Kod";
            dataGridView1.Columns[5].Width = 100;

          


            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "JM";
            dataGridView1.Columns[6].Width = 40;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Količina neto";
            dataGridView1.Columns[7].Width = 60;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "RB";
            dataGridView1.Columns[8].Width = 30;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Grupa";
            dataGridView1.Columns[9].Width = 40;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Napomena";
            dataGridView1.Columns[10].Width = 120;

         

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Skladište";
            dataGridView1.Columns[11].Width = 60;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Lokacija";
            dataGridView1.Columns[12].Width = 80;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Tekući Termin";
            dataGridView1.Columns[13].Width = 60;
            dataGridView1.Columns[13].Visible = false;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Tekući MAX termin";
            dataGridView1.Columns[14].Width = 60;

     
            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Početni termin";
            dataGridView1.Columns[15].Width = 60;
            dataGridView1.Columns[15].Visible = false;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Max početni termin";
            dataGridView1.Columns[16].Width = 60;

            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Datum zadnje promene";
            dataGridView1.Columns[17].Width = 70;

            PromeniBoju();
        }

        private void RefreshDataGridPLan()
        {
            usao = 1;


  var select = "  SELECT distinct PlanStavkeNeto.[ID],PlanStavkeNeto.[PlanNaziv], " +
 " PlanStavkeNeto.[MPSifra],PlanStavkeNeto.[RN],PlanStavkeNeto.[StatusRN],PlanStavkeNeto.[MPStaraSif] " +
 "  ,PlanStavkeNeto.[JM] ,PlanStavkeNeto.[Kolicina] ,PlanStavkeNeto.[RedniBroj], " +
 "  PlanStavkeNeto.[Grupa] " +
"  ,PlanStavkeNeto.[Napomena] ,PlanStavkeNeto.[LokSkSifra] , " +
"  PlanStavkeNeto.[LokOpis],PlanStavkeNeto.[Termin] " +
"  ,PlanStavkeNeto.[MAXTermin], " +
"  PlanStavkeNeto.[TerminUvoz],PlanStavkeNeto.[TerminMaxPocetni] , " +
"  PlanStavkeNeto.[DatumZadnjePromene]  " +
"  FROM PlanStavkeNeto " +
"  inner join PlanStavke on PlanStavkeNeto.MPSifra = PlanStavke.MPSifra " +
"  where PlanStavkeNeto.[PlanNaziv] = '" + txtPlan.Text + "'" + " and  PlanStavke.[PlanNaziv]  = '" + txtPlan.Text + "'" +
"  order by PlanStavkeNeto.[DatumZadnjePromene] desc ";
            
         

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
            dataGridView1.Columns[0].Width = 60;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Plan";
            dataGridView1.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Mp sifra";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "RN";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "RN Status";
            dataGridView1.Columns[4].Width = 60;


            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Mp Kod";
            dataGridView1.Columns[5].Width = 100;




            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "JM";
            dataGridView1.Columns[6].Width = 40;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Količina neto";
            dataGridView1.Columns[7].Width = 60;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "RB";
            dataGridView1.Columns[8].Width = 30;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Grupa";
            dataGridView1.Columns[9].Width = 40;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Napomena";
            dataGridView1.Columns[10].Width = 120;



            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Skladište";
            dataGridView1.Columns[11].Width = 60;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Lokacija";
            dataGridView1.Columns[12].Width = 80;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Tekući Termin";
            dataGridView1.Columns[13].Width = 60;
            dataGridView1.Columns[13].Visible = false;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Tekući MAX termin";
            dataGridView1.Columns[14].Width = 60;


            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Početni termin";
            dataGridView1.Columns[15].Width = 60;
            dataGridView1.Columns[15].Visible = false;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Max početni termin";
            dataGridView1.Columns[16].Width = 60;

            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Datum zadnje promene";
            dataGridView1.Columns[17].Width = 70;


            PromeniBoju();
        }
       
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (usao == 1)
            {
                int rowindex = e.RowIndex;

                if (dataGridView1.Rows[rowindex].Cells[14].Value.ToString() != dataGridView1.Rows[rowindex].Cells[16].Value.ToString())
                {
                    dataGridView1.Rows[rowindex].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void PromeniBoju()
        {
          /*
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var pom = row.Cells[11].Value.ToString();
                var pom2 = row.Cells[13].Value.ToString();
                if (pom != pom2)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                    row.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
                }
                dataGridView1.Refresh();
            }
        */
        }
        
        private void RefreshDataGridPLanRazlika()
        {
            var select = " SELECT Distinct PlanStavkeNeto.[ID],PlanStavkeNeto.[PlanNaziv],PlanStavkeNeto.[MPSifra],PlanStavkeNeto.[RN],PlanStavkeNeto.[StatusRN],PlanStavkeNeto.[MPStaraSif] " +
          " ,PlanStavkeNeto.[JM] ,PlanStavkeNeto.[Kolicina] ,PlanStavkeNeto.[RedniBroj],PlanStavkeNeto.[Grupa] " +
          " ,PlanStavkeNeto.[Napomena] ,PlanStavkeNeto.[LokSkSifra] ,PlanStavkeNeto.[LokOpis],PlanStavkeNeto.[Termin] " +
          " ,PlanStavkeNeto.[MAXTermin], [TerminUvoz],[TerminMaxPocetni] ,[DatumZadnjePromene] " +
            " FROM [Jela_test].[dbo].[PlanStavkeNeto] inner join PlanStavke on PlanStavke.MpSifra = PlanStavkeNeto.MpSifra " +
          " where PlanStavkeNeto.[PlanNaziv] = '" + txtPlan.Text + "'";

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
            dataGridView1.Columns[0].Width = 60;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Plan";
            dataGridView1.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Mp sifra";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "RN";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "RN Status";
            dataGridView1.Columns[4].Width = 60;


            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Mp Kod";
            dataGridView1.Columns[5].Width = 100;




            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "JM";
            dataGridView1.Columns[6].Width = 40;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Količina neto";
            dataGridView1.Columns[7].Width = 60;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "RB";
            dataGridView1.Columns[8].Width = 30;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Grupa";
            dataGridView1.Columns[9].Width = 40;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Napomena";
            dataGridView1.Columns[10].Width = 120;



            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Skladište";
            dataGridView1.Columns[11].Width = 60;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Lokacija";
            dataGridView1.Columns[12].Width = 80;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Tekući Termin";
            dataGridView1.Columns[13].Width = 60;
            dataGridView1.Columns[13].Visible = false;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Tekući MAX termin";
            dataGridView1.Columns[14].Width = 60;


            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Početni termin";
            dataGridView1.Columns[15].Width = 60;
            dataGridView1.Columns[15].Visible = false;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Max početni termin";
            dataGridView1.Columns[16].Width = 60;

            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Datum zadnje promene";
            dataGridView1.Columns[17].Width = 70;

            PromeniBoju();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InsertPlan isporuka = new InsertPlan();

                    isporuka.UpdateTermin2(txtPlan.Text);

            MessageBox.Show("Promenjen je termin");
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertPlan isporuka = new InsertPlan();
            isporuka.UpdateMAXTermin2(txtPlan.Text);
          //  RefreshDataGrid();
            }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RefreshDataGridPLan();
            UkupneKolicine();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            RefreshDataGridRazlika();
            UkupneKolicine();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RefreshDataGridPLanRazlika();
            UkupneKolicine();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            double SelectedKolicina = 0;
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra.Text = row.Cells[0].Value.ToString();
                        SelectedKolicina = SelectedKolicina + Convert.ToDouble(row.Cells[7].Value.ToString());
                     
                    }
                }

                txtSelektovanaKoličina.Text = SelectedKolicina.ToString();
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        InsertNetoPotrebe ins = new InsertNetoPotrebe();
                        ins.InsertNPNapomena(Convert.ToInt32(row.Cells[0].Value.ToString()), txtNapomena.Text);
                       // ins.UpdateTerminiNapomena(Convert.ToInt32(row.Cells[0].Value.ToString()), txtNapomena.Text);
                        // txtPutanja.Text = row.Cells[2].Value.ToString();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela promena napomene");
            }
            
            
        }

        private void txtSifra_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNapomena_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            InsertPlan rn = new InsertPlan();
            rn.UpdatePlanStavkeNetoRN(txtPlan.Text);
            MessageBox.Show("Prebačeni su brojevi RN");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            InsertPlan rn = new InsertPlan();
            rn.UpdatePlanStavkeNetoRNStatus(txtPlan.Text);
            MessageBox.Show("Statusi ažurirani");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            String Skladista = "";
            int prvi = 0;
          
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        if (prvi == 0)
                        { 
                        Skladista = row.Cells[0].Value.ToString();
                        prvi = prvi + 1;
                        }
                        else
                        {
                            Skladista = Skladista + "," + row.Cells[0].Value.ToString();
                        }
                    }

                }

                var select = " SELECT distinct [PlanStavkeNeto].[ID],[PlanStavkeNeto].[PlanNaziv],[PlanStavkeNeto].[MPSifra]," +
           " PlanStavkeNeto.[RN],PlanStavkeNeto.[StatusRN], " +
                   " [PlanStavkeNeto].[MPStaraSif] " +
             " ,[PlanStavkeNeto].[JM] ,[PlanStavkeNeto].[Kolicina] ,[PlanStavkeNeto].[RedniBroj],[PlanStavkeNeto].[Grupa] " +
             " ,[PlanStavkeNeto].[Napomena] ,[PlanStavkeNeto].[LokSkSifra] ,[PlanStavkeNeto].[LokOpis],[PlanStavkeNeto].[Termin] " +
             " ,[PlanStavkeNeto].[MAXTermin]  " +
             "  ,PlanStavkeNeto.[TerminUvoz],PlanStavkeNeto.[TerminMaxPocetni] , " +
             "  PlanStavkeNeto.[DatumZadnjePromene] " +
            " FROM [Jela_test].[dbo].[PlanStavkeNeto] " +
            "  inner join PlanStavke on PlanStavkeNeto.MPSifra = PlanStavke.MPSifra " +
                " where [PlanStavkeNeto].[LokSkSifra] IN (" + Skladista + ") and " +
            "  PlanStavkeNeto.[PlanNaziv] = '" + txtPlan.Text + "'" + " and  PlanStavke.[PlanNaziv]  = '" + txtPlan.Text + "'" +
              "  order by PlanStavkeNeto.[DatumZadnjePromene] desc ";;
            //Panta

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
            dataGridView1.Columns[0].Width = 60;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Plan";
            dataGridView1.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Mp sifra";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "RN";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "RN Status";
            dataGridView1.Columns[4].Width = 60;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Mp Kod";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "JM";
            dataGridView1.Columns[6].Width = 40;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Količina neto";
            dataGridView1.Columns[7].Width = 60;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "RB";
            dataGridView1.Columns[8].Width = 30;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Grupa";
            dataGridView1.Columns[9].Width = 40;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Napomena";
            dataGridView1.Columns[10].Width = 120;



            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Skladište";
            dataGridView1.Columns[11].Width = 60;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Lokacija";
            dataGridView1.Columns[12].Width = 80;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Tekući Termin";
            dataGridView1.Columns[13].Width = 60;
            dataGridView1.Columns[13].Visible = false;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Tekući MAX termin";
            dataGridView1.Columns[14].Width = 60;


            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Početni termin";
            dataGridView1.Columns[15].Width = 60;
            dataGridView1.Columns[15].Visible = false;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Max početni termin";
            dataGridView1.Columns[16].Width = 60;

            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Datum zadnje promene";
            dataGridView1.Columns[17].Width = 70;

            UkupneKolicine();

        
        }

        private void UkupneKolicine()
        {
            double UkupnaKolicina = 0;
            double SelectovanaKolicina = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                      if (row.Selected == true)
                      {
                        SelectovanaKolicina = SelectovanaKolicina +  Convert.ToDouble(row.Cells[7].Value.ToString());
                      }

                      UkupnaKolicina = UkupnaKolicina + Convert.ToDouble(row.Cells[7].Value.ToString());
                }

            txtUkupnaKolicina.Text = UkupnaKolicina.ToString();
            txtSelektovanaKoličina.Text = SelectovanaKolicina.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var select = " SELECT distinct [ID],[PlanNaziv],[MPSifra],PlanStavkeNeto.[RN],PlanStavkeNeto.[StatusRN],[MPStaraSif] " +
          " ,[JM] ,[Kolicina] ,[RedniBroj],[Grupa] " +
          " ,[Napomena] ,[LokSkSifra] ,[LokOpis],[Termin] " +
          " ,[MAXTermin]  " +
            "  ,PlanStavkeNeto.[TerminUvoz],PlanStavkeNeto.[TerminMaxPocetni] , " +
            "  PlanStavkeNeto.[DatumZadnjePromene] " +
           " FROM [Jela_test].[dbo].[PlanStavkeNeto] where MpSifra = " + txtMPSifra.Text.Trim() + 
           "  and PlanStavkeNeto.[PlanNaziv] = '" + txtPlan.Text + "'";


         

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            UkupneKolicine();
            }

        private void button13_Click(object sender, EventArgs e)
        {
              if (dataGridView1.Rows.Count > 0)  
    {  
        SaveFileDialog sfd = new SaveFileDialog();  
        sfd.Filter = "CSV (*.csv)|*.csv";  
        sfd.FileName = "Output.csv";  
        bool fileError = false;  
        if (sfd.ShowDialog() == DialogResult.OK)  
        {  
            if (File.Exists(sfd.FileName))  
            {  
                try  
                {  
                    File.Delete(sfd.FileName);  
                }  
                catch (IOException ex)  
                {  
                    fileError = true;  
                    MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);  
                }  
            }  
            if (!fileError)  
            {  
                try  
                {  
                    int columnCount = dataGridView1.Columns.Count;  
                    string columnNames = "";  
                    string[] outputCsv = new string[dataGridView1.Rows.Count + 1];  
                    for (int i = 0; i < columnCount; i++)  
                    {  
                        columnNames += dataGridView1.Columns[i].HeaderText.ToString() + ";";  
                    }  
                    outputCsv[0] += columnNames;  
                              
                    for (int i = 1; (i - 1) < dataGridView1.Rows.Count; i++)  
                    {  
                        for (int j = 0; j < columnCount; j++)  
                        {  
                            outputCsv[i] += dataGridView1.Rows[i - 1].Cells[j].Value.ToString() + ";";  
                        }  
                    }  
  
                    File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);  
                    MessageBox.Show("Data Exported Successfully !!!", "Info");  
                }  
                catch(Exception ex)  
                {  
                    MessageBox.Show("Error :" + ex.Message);  
                }  
            }  
        }  
    }  
    else  
    {  
        MessageBox.Show("No Record To Export !!!", "Info");  
    }  
} 
        }
        }

