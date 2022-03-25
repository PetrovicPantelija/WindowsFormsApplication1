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
    public partial class frmPlanImport : Form
    {
        int RefreshStatus = 0;
        public static string code = "frmPlanImport";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        // Refresh status 0-Usao 1-Sve 2-Plan 3- Skladiste

        int row1 = 0;
        int col1 = 0;
        
        int usao = 0;
        string KorisnikM = "";
        int ReadNabavkaM = 0;
        public frmPlanImport()
        {
            InitializeComponent();
        }

        public frmPlanImport(string Korisnik, int ReadNabavka)
        {
            InitializeComponent();
            KorisnikM = Korisnik;
            ReadNabavkaM = ReadNabavka;
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
        private void frmPlanImport_Load(object sender, EventArgs e)
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


            var select = " select Distinct PlanNaziv from PlanStavke";
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
            if (ReadNabavkaM == 1)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button8.Enabled = false;
                button5.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button8.Enabled = true;
                button5.Enabled = true;
            }
        }

        private void RefreshDataGridSve()
        {
            usao = 1;
            var select = " SELECT [ID] ,[PlanNaziv] ,[MPSifra], RN, StatusRN " +
     " ,[MPStaraSif] ,[LokSkSifra] ,[LokOpis]      ,[Kolicina]       ,[Termin]       ,[Datum1] " +
     " ,[Termin2]      ,[Datum2], Napomena, Zuta  FROM [Jela_test].[dbo].[PlanStavke] where [PlanNaziv] = '" + txtPlan.Text + "' order by Datum2 desc";

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
            dataGridView1.Columns[1].HeaderText = "Plan Naziv";
            dataGridView1.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Mp šifra";
            dataGridView1.Columns[2].Width = 60;


            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "RN";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "RN Status";
            dataGridView1.Columns[4].Width = 60;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Mp kod";
            dataGridView1.Columns[5].Width = 100;


            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Skladište";
            dataGridView1.Columns[6].Width = 60;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Skladište opis";
            dataGridView1.Columns[7].Width = 160;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Količina";
            dataGridView1.Columns[8].Width = 60;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Termin 2";
            dataGridView1.Columns[9].Width = 60;


            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Datum 2";
            dataGridView1.Columns[10].Width = 100;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Termin 1";
            dataGridView1.Columns[11].Width = 60;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Datum 1";
            dataGridView1.Columns[12].Width = 100;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Napomena";
            dataGridView1.Columns[13].Width = 120;


            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Zuta";
            dataGridView1.Columns[14].Width = 40;

          
           
        }

        private void RefreshDataGrid()
        {
            usao = 1;
            var select = " SELECT [ID] ,[PlanNaziv] ,[MPSifra], RN, StatusRN " +
     " ,[MPStaraSif] ,[LokSkSifra] ,[LokOpis]      ,[Kolicina]       ,[Termin]       ,[Datum1] " +
     " ,[Termin2]      ,[Datum2], Napomena,Zuta   FROM [Jela_test].[dbo].[PlanStavke] where [LokSkSifra] = " + Convert.ToInt32(cboSkladiste1.SelectedValue) + " and [PlanNaziv] = '" + txtPlan.Text + "' order by Datum2 desc";

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
            dataGridView1.Columns[1].HeaderText = "Plan Naziv";
            dataGridView1.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Mp šifra";
            dataGridView1.Columns[2].Width = 60;


            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "RN";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "RN Status";
            dataGridView1.Columns[4].Width = 60;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Mp kod";
            dataGridView1.Columns[5].Width = 100;


            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Skladište";
            dataGridView1.Columns[6].Width = 60;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Skladište opis";
            dataGridView1.Columns[7].Width = 160;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Količina";
            dataGridView1.Columns[8].Width = 60;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Termin 2";
            dataGridView1.Columns[9].Width = 60;


            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Datum 2";
            dataGridView1.Columns[10].Width = 100;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Termin 1";
            dataGridView1.Columns[11].Width = 60;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Datum 1";
            dataGridView1.Columns[12].Width = 100;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Napomena";
            dataGridView1.Columns[13].Width = 120;


            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Zuta";
            dataGridView1.Columns[14].Width = 40;

        }

        private void RefreshDataGridPlan()
        {
            usao = 1;
            var select = " SELECT [ID] ,[PlanNaziv] ,[MPSifra], RN, StatusRN " +
     " ,[MPStaraSif] ,[LokSkSifra] ,[LokOpis]      ,[Kolicina]       ,[Termin]       ,[Datum1] " +
     " ,[Termin2]      ,[Datum2], Napomena, Zuta   FROM [Jela_test].[dbo].[PlanStavke] where  [PlanNaziv] = '" + txtPlan.Text + "' order by Datum2 desc";

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
            dataGridView1.Columns[1].HeaderText = "Plan Naziv";
            dataGridView1.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Mp šifra";
            dataGridView1.Columns[2].Width = 60;


            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "RN";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "RN Status";
            dataGridView1.Columns[4].Width = 60;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Mp kod";
            dataGridView1.Columns[5].Width = 100;


            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Skladište";
            dataGridView1.Columns[6].Width = 60;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Skladište opis";
            dataGridView1.Columns[7].Width = 160;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Količina";
            dataGridView1.Columns[8].Width = 60;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Termin 2";
            dataGridView1.Columns[9].Width = 60;


            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Datum 2";
            dataGridView1.Columns[10].Width = 100;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Termin 1";
            dataGridView1.Columns[11].Width = 60;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Datum 1";
            dataGridView1.Columns[12].Width = 100;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Napomena";
            dataGridView1.Columns[13].Width = 120;


            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Zuta";
            dataGridView1.Columns[14].Width = 40;

      
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            RefreshDataGrid();
            RefreshStatus = 3;
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

        private void button1_Click(object sender, EventArgs e)
        {
            InsertPlan isporuka = new InsertPlan();
           
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
               // if (row.Selected == true)
               // {

                    if (row != null && row.Cells[1].Value != null)
                    {
                        int terminpom = 0;
                        if (row.Cells[6].Value.ToString().Trim() == "" )
                        { terminpom = 0;}
                        else  
                        { terminpom = Convert.ToInt32(row.Cells[6].Value);}
                        //Provera
                        //Provera
                       // Šif.Mp;Naziv MP;Količ.;Sklad.;Lokacija;TERMIN REALIZACIJE
                      //  MessageBox.Show(txtPutanja.Text + ' ' + Convert.ToInt32(row.Cells[0].Value.ToString()) + ' ' + row.Cells[1].Value.ToString() + ' ' + row.Cells[3].Value.ToString() + ' ' + row.Cells[4].Value.ToString() + ' ' + Convert.ToDouble(row.Cells[2].Value.ToString()) + ' ' + Convert.ToInt32(row.Cells[5].Value.ToString()) + ' ' + Convert.ToInt32(row.Cells[5].Value.ToString()) + ' ' + DateTime.Now + terminpom);

                        isporuka.InsertPlanStavke(txtPlan.Text, Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[2].Value.ToString(), row.Cells[4].Value.ToString(), row.Cells[5].Value.ToString(), Convert.ToDouble(row.Cells[3].Value.ToString()), terminpom, DateTime.Now, terminpom, DateTime.Now, row.Cells[0].Value.ToString());
                       // progressBar1.Value = progressBar1.Value + 1;
                    }
              //  }
            }
            MessageBox.Show("Formiran je plan");
           // RefreshDataGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertPlan isporuka = new InsertPlan();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {

                    if (row != null && row.Cells[0].Value != null)
                    {

                        isporuka.UpdatePlanStavke(Convert.ToInt32(row.Cells[0].Value.ToString()),Convert.ToInt32(txtNoviTermin.Text), DateTime.Now);
                        isporuka.UpdatePlanStavkeNeto(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(txtNoviTermin.Text), txtPlan.Text);
                        // progressBar1.Value = progressBar1.Value + 1;
                    }
                    //  }
                }
            }
            MessageBox.Show("Izmenjen plan");
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnOsvezi_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
          
            RefreshDataGridPlan();
            RefreshStatus = 2;
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
                       // ins.UpdateTerminiZuta(Convert.ToInt32(row.Cells[0].Value.ToString()));
                        ins.UpdateTerminiNapomena(Convert.ToInt32(row.Cells[0].Value.ToString()), txtNapomena.Text);
                        // txtPutanja.Text = row.Cells[2].Value.ToString();
                    }
                }

                row1 = (this.dataGridView1.CurrentCell.RowIndex);
                col1 = (this.dataGridView1.CurrentCell.ColumnIndex);

                if (RefreshStatus == 2)
                {
                    RefreshDataGridPlan();
                    this.dataGridView1.CurrentCell = this.dataGridView1[col1, row1];
                }
                else if (RefreshStatus == 3)
                {
                    RefreshDataGrid();
                    this.dataGridView1.CurrentCell = this.dataGridView1[col1, row1];
                }
               
            }
            catch
            {
                MessageBox.Show("Nije uspela promena napomene");
            }

            /*
            InsertNetoPotrebe ins = new InsertNetoPotrebe();

            ins.UpdateTerminiNapomena(Convert.ToInt32(txtSifra.Text), txtNapomena.Text);
             */
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        InsertNetoPotrebe ins = new InsertNetoPotrebe();
                        ins.UpdateTerminiZuta(Convert.ToInt32(row.Cells[0].Value.ToString()));
                        row1 = (this.dataGridView1.CurrentCell.RowIndex);
                        col1 = (this.dataGridView1.CurrentCell.ColumnIndex);
                       
                        this.dataGridView1.CurrentCell = this.dataGridView1[col1, row1];
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                      

                        // txtPutanja.Text = row.Cells[2].Value.ToString();
                    }
                }
              

            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
            
            
            
           
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (usao == 1)
            { 
                    int rowindex = e.RowIndex;
            
                    if (dataGridView1.Rows[rowindex].Cells[14].Value.ToString() == "1")
                    {
                        dataGridView1.Rows[rowindex].DefaultCellStyle.BackColor = Color.Yellow;
                    }
            }
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
                        // txtPutanja.Text = row.Cells[2].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        InsertNetoPotrebe ins = new InsertNetoPotrebe();
                        ins.UpdateTerminiBela(Convert.ToInt32(row.Cells[0].Value.ToString()));
                        row1 = (this.dataGridView1.CurrentCell.RowIndex);
                        col1 = (this.dataGridView1.CurrentCell.ColumnIndex);
                        this.dataGridView1.CurrentCell = this.dataGridView1[col1, row1];
                        row.DefaultCellStyle.BackColor = Color.White;
                        
                    }
                }
               
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button7_Click(object sender, EventArgs e)
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
            
            
            //Panta
        }
        
    }


