using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;

using Microsoft.Reporting.WinForms;

namespace WindowsFormsApplication1
{
    public partial class ObradaReklamacije : Form
    {
        public ObradaReklamacije()
        {
            InitializeComponent();
        }

        private void ObradaReklamacije_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet2.ReklamacijaNalepnica' table. You can move, or remove it, as needed.
          //  this.ReklamacijaNalepnicaTableAdapter.Fill(this.DataSet2.ReklamacijaNalepnica);
            // TODO: This line of code loads data into the 'DataSet2.ReklamacijaNalepnica' table. You can move, or remove it, as needed.
           // this.ReklamacijaNalepnicaTableAdapter.Fill(this.DataSet2.ReklamacijaNalepnica);
            var select = " Select Distinct DatumUtovara, Cast(SUBSTRING(DatumUtovara, 1, Charindex('.',DatumUtovara) -1) as int)  as  Dan, Cast(Replace(SUBSTRING(DatumUtovara, Charindex('.',DatumUtovara)+1, Len(DatumUtovara)-12 -1 -( (Charindex('.',DatumUtovara) ))),'.','') as int) as  Mesec  From RkShipping where Stanje = 1  order by  Mesec, Dan";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboDatumUtovara.DataSource = ds.Tables[0];
            cboDatumUtovara.DisplayMember = "DatumUtovara";
            cboDatumUtovara.ValueMember = "DatumUtovara";

          //  this.reportViewer1.RefreshReport();
          //  this.reportViewer1.RefreshReport();
          //  this.reportViewer2.RefreshReport();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ZAMENA
            //Select Distinct Vozilo From RkShipping where Stanje = 1
            var select = " Select Distinct RTrim(Vozilo) as Vozilo From RkShipping where Stanje = 1 and DatumUtovara = '" + cboDatumUtovara.Text + "'";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboVozila.DataSource = ds.Tables[0];
            cboVozila.DisplayMember = "Vozilo";
            cboVozila.ValueMember = "Vozilo";
        }

        private void btnPozovi_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {

            var select = " Select RkShippingItemRec.ShippingItemRecID, RkShippingItemRec.ShipingIdz,  RkShipping.ShippingNo as BarkodIstovara, RkShipping.BrojIstovara as BrojUtovara,RkShipping.BrojUtovara as BrojIstovara, RkShippingItemRec.SifraElementa as Element, Saloni.MestoIsporuke, RkShippingItemRec.SifraReklamacije, RkShippingItemRec.NazivReklamacije, RkShippingItemRec.KolicinaOrg, RkShippingItemRec.Zapisnik,  RkShippingItemRec.Paleta, RkShippingItemRec.Paket, RkShippingItemRec.BrojNalepnica, RkShippingItemRec.Tezina, RkShippingItemRec.LargoDimenzija, RkShippingItemRec.SifraReklamacijeLargo,  RkShippingItemRec.BrojKoleta  FROM RkShippingItemRec inner join RkShipping on  RkShipping.ShippingID = RkShippingItemRec.ShipingIdz  inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
           
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];
            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "Isporuka ID";
            dataGridView3.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "Isporuka";
            dataGridView3.Columns[2].Width = 140;


            DataGridViewColumn column4 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "Br. utovara";
            dataGridView3.Columns[3].Width = 45;

            DataGridViewColumn column5 = dataGridView3.Columns[4];
            dataGridView3.Columns[4].HeaderText = "Br. istovara";
            dataGridView3.Columns[4].Width = 45;

            DataGridViewColumn column6 = dataGridView3.Columns[5];
            dataGridView3.Columns[5].HeaderText = "Element";
            dataGridView3.Columns[5].Width = 170;

            DataGridViewColumn column7 = dataGridView3.Columns[6];
            dataGridView3.Columns[6].HeaderText = "Mesto isporuke";
            dataGridView3.Columns[6].Width = 100;

            DataGridViewColumn column8 = dataGridView3.Columns[7];
            dataGridView3.Columns[7].HeaderText = "Šifra reklamacije(Largo kod)";
            dataGridView3.Columns[7].Width = 200;


            DataGridViewColumn column9 = dataGridView3.Columns[8];
            dataGridView3.Columns[8].HeaderText = "Naziv reklamacije";
            dataGridView3.Columns[8].Width = 140;

            DataGridViewColumn column10 = dataGridView3.Columns[9];
            dataGridView3.Columns[9].HeaderText = "Količina org.";
            dataGridView3.Columns[9].Width = 40;


            DataGridViewColumn column11 = dataGridView3.Columns[10];
            dataGridView3.Columns[10].HeaderText = "Zapisnik";
            dataGridView3.Columns[10].Width = 70;

            DataGridViewColumn column12 = dataGridView3.Columns[11];
            dataGridView3.Columns[11].HeaderText = "Paleta";
            dataGridView3.Columns[11].Width = 40;

            DataGridViewColumn column13 = dataGridView3.Columns[12];
            dataGridView3.Columns[12].HeaderText = "Paket";
            dataGridView3.Columns[12].DefaultCellStyle.BackColor = Color.LightYellow;
            dataGridView3.Columns[12].Width = 40;

            DataGridViewColumn column14 = dataGridView3.Columns[13];
            dataGridView3.Columns[13].HeaderText = "Br. nalepnica";
            dataGridView3.Columns[13].DefaultCellStyle.BackColor = Color.LightGreen;
            dataGridView3.Columns[13].Width = 40;

            DataGridViewColumn column15 = dataGridView3.Columns[14];
            dataGridView3.Columns[14].HeaderText = "Težina(KG)";
            dataGridView3.Columns[14].Width = 50;

            DataGridViewColumn column16 = dataGridView3.Columns[15];
            dataGridView3.Columns[15].HeaderText = "Largo Dimenzija";
            dataGridView3.Columns[15].Width = 100;

            DataGridViewColumn column17 = dataGridView3.Columns[16];
            dataGridView3.Columns[16].HeaderText = "Šifra reklamacije Largo";
            dataGridView3.Columns[16].Width = 70;

            DataGridViewColumn column18 = dataGridView3.Columns[17];
            dataGridView3.Columns[17].HeaderText = "Broj koleta";
            dataGridView3.Columns[17].Width = 70;


            dataGridView3.Refresh();        
        
        
        
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {

                    if (row.Selected)
                    {
                        var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

                        SqlConnection connection = new SqlConnection(s_connection);
                        SqlCommand command = new SqlCommand("SELECT * FROM Korisnici", connection);
                        command.Connection.Open();

                        string querystr = "UPDATE RkShippingItemRec SET Paket='" + cboPaket.Text + "' WHERE ShippingItemRecId ='" + row.Cells[0].Value.ToString() + "'";
                        SqlCommand query = new SqlCommand(querystr, connection);
                        query.ExecuteNonQuery();

                      

                        command.Connection.Close();

                    }
                }
                RefreshDataGrid();
              

            }
            catch
            {
                MessageBox.Show("Nije uspela promena stavki");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {

                    if (row.Selected)
                    {
                        var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

                        SqlConnection connection = new SqlConnection(s_connection);
                        SqlCommand command = new SqlCommand("SELECT * FROM Korisnici", connection);
                        command.Connection.Open();

                        string querystr = "UPDATE RkShippingItemRec SET BrojNalepnica='" + txtBrojNalepnica.Text + "' WHERE ShippingItemRecId ='" + row.Cells[0].Value.ToString() + "'";
                        SqlCommand query = new SqlCommand(querystr, connection);
                        query.ExecuteNonQuery();



                        command.Connection.Close();

                    }
                }
                RefreshDataGrid();


            }
            catch
            {
                MessageBox.Show("Nije uspela promena stavki");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            Barcodes.Barcode39 b = new Barcodes.Barcode39();
         
            double Cena = 0;
           
            InsertReklamacijaNalepnica ReklNal = new InsertReklamacijaNalepnica();
            ReklNal.DeleteReklNal();
            //Skupiti sve parametre

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
               
                
              
                if (row.Selected == true)
                {
                    int BrojNalepnica = 0;
                    for (int i = 0; i < Convert.ToInt32(row.Cells[13].Value.ToString()); i++)
                    {
                        if (BrojNalepnica < Convert.ToInt32(row.Cells[13].Value.ToString()))
                        {
                            BrojNalepnica = BrojNalepnica + 1;

                            ReklNal.InsertReklNal(Convert.ToInt32(row.Cells[0].Value.ToString()),
                                Convert.ToInt32(row.Cells[1].Value.ToString())
                                , row.Cells[2].Value.ToString(),
                                row.Cells[5].Value.ToString(), 
                                row.Cells[6].Value.ToString(), 
                                row.Cells[7].Value.ToString()
                                , Convert.ToDouble(row.Cells[9].Value.ToString()),
                                row.Cells[10].Value.ToString(),
                                row.Cells[8].Value.ToString()
                                , Convert.ToInt32(row.Cells[9].Value.ToString()), 
                                b.GenerateBarcodeImage(row.Cells[0].Value.ToString()));
                            // Convert.ToInt32(row.Cells[7].Value.ToString()), 1, txtMestoTroska.Text, Convert.ToDateTime(row.Cells[4].Value.ToString()), Convert.ToDateTime(row.Cells[5].Value.ToString()), Convert.ToInt32(1), Cena, "");
                        } 
                        
                    }
                   
                }
               // progressBar1.Value = progressBar1.Value + 1;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
          



            DataSet2TableAdapters.ReklamacijaNalepnicaTableAdapter ta = new DataSet2TableAdapters.ReklamacijaNalepnicaTableAdapter();
            DataSet2.ReklamacijaNalepnicaDataTable dt = new DataSet2.ReklamacijaNalepnicaDataTable();
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSetReklamacija";
            rds.Value = dt;

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptReklNal.rdlc";
     
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataSet2TableAdapters.ReklamacijaNalepnicaTableAdapter ta = new DataSet2TableAdapters.ReklamacijaNalepnicaTableAdapter();
            DataSet2.ReklamacijaNalepnicaDataTable dt = new DataSet2.ReklamacijaNalepnicaDataTable();
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSetReklamacija";
            rds.Value = dt;

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptReklNal2.rdlc";

            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataSet2TableAdapters.ReklamacijaNalepnicaTableAdapter ta = new DataSet2TableAdapters.ReklamacijaNalepnicaTableAdapter();
            DataSet2.ReklamacijaNalepnicaDataTable dt = new DataSet2.ReklamacijaNalepnicaDataTable();
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSetReklamacija";
            rds.Value = dt;

            reportViewer3.LocalReport.DataSources.Clear();
            reportViewer3.LocalReport.ReportPath = "rptReklNal3.rdlc";

            reportViewer3.LocalReport.DataSources.Add(rds);
            reportViewer3.RefreshReport();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DataSet2TableAdapters.ReklamacijaNalepnicaTableAdapter ta = new DataSet2TableAdapters.ReklamacijaNalepnicaTableAdapter();
            DataSet2.ReklamacijaNalepnicaDataTable dt = new DataSet2.ReklamacijaNalepnicaDataTable();
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSetReklamacija";
            rds.Value = dt;

            reportViewer4.LocalReport.DataSources.Clear();
            reportViewer4.LocalReport.ReportPath = "rptReklNal4.rdlc";

            reportViewer4.LocalReport.DataSources.Add(rds);
            reportViewer4.RefreshReport();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {

                    if (row.Selected)
                    {
                        var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

                        SqlConnection connection = new SqlConnection(s_connection);
                        SqlCommand command = new SqlCommand("SELECT * FROM Korisnici", connection);
                        command.Connection.Open();

                        string querystr = "UPDATE RkShippingItemRec SET BrojKoleta='" + txtBrojKoleta.Text + "' WHERE ShippingItemRecId ='" + row.Cells[0].Value.ToString() + "'";
                        SqlCommand query = new SqlCommand(querystr, connection);
                        query.ExecuteNonQuery();



                        command.Connection.Close();

                    }
                }
                RefreshDataGrid();


            }
            catch
            {
                MessageBox.Show("Nije uspela promena stavki");
            }
        }
    }
}
