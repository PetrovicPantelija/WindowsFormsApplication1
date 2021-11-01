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

namespace WindowsFormsApplication1
{
    public partial class frmReklamacijaUvoz : Form
    {
        public frmReklamacijaUvoz()
        {
            InitializeComponent();
        }

        private void frmReklamacijaUvoz_Load(object sender, EventArgs e)
        {
            var select = " Select Distinct DatumIstovara, Cast(SUBSTRING(DatumIstovara, 1, Charindex('.',DatumIstovara) -1) as int)  as  Dan, Cast(Replace(SUBSTRING(DatumIstovara, Charindex('.',DatumIstovara)+1, Len(DatumIstovara)-12 -1 -( (Charindex('.',DatumIstovara) ))),'.','') as int) as  Mesec  From RkShipping where Stanje = 1  order by  Mesec, Dan";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboDatumUtovara.DataSource = ds.Tables[0];
            cboDatumUtovara.DisplayMember = "DatumIstovara";
            cboDatumUtovara.ValueMember = "DatumIstovara";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refreshdatagridview();
        }

        private void refreshdatagridview()
        {
            var select = " SELECT Paleta, RkShipping.BrojIstovara as BrojUtovara, RkShipping.BrojUtovara as BrojIsporuke , MestoIsporuke, RkShippingItemRec.ShippingItemRecID , RkShippingItemRec.Zapisnik AS Zapisnik, RkShippingItemRec.SifraReklamacije as SifLargo , RkShippingItemREC.NazivReklamacije,  RkShippingItemREC.Opis,KolicinaOrg,  Kolicina,   0 as Paletirano, Paletar as Paletar, VremePakovanja as VremePaletiranja, RkShippingItemRec.IsporucenoKupcu as Kontrolisano, Kontrolor as KOntrolor ,  RkShippingItemRec.VremeKontrole FROM [dbo].RkShippingItemRec inner join RkShipping on [dbo].RkShippingItemRec.ShipingIDz = RkShipping.[ShippingID] inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where  RkShipping.DatumIstovara = '" + cboDatumUtovara.Text + "'";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];
        

            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "Paleta";
            dataGridView3.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "Broj utovara";
            dataGridView3.Columns[1].Width = 45;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "Broj isporuke";
            dataGridView3.Columns[2].Width = 45;


            DataGridViewColumn column4 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "Mesto isporuke";
            dataGridView3.Columns[3].Width = 190;

            DataGridViewColumn column5 = dataGridView3.Columns[4];
            dataGridView3.Columns[4].HeaderText = "Reklamacija ID";
            dataGridView3.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView3.Columns[5];
            dataGridView3.Columns[5].HeaderText = "Zapisnik";
            dataGridView3.Columns[5].Width = 80;



            DataGridViewColumn column7 = dataGridView3.Columns[6];
            dataGridView3.Columns[6].HeaderText = "Šifra reklamacije";
            dataGridView3.Columns[6].Width = 195;


            DataGridViewColumn column8 = dataGridView3.Columns[7];
            dataGridView3.Columns[7].HeaderText = "Naziv Reklamacije";
            dataGridView3.Columns[7].Width = 90;


            DataGridViewColumn column9 = dataGridView3.Columns[8];
            dataGridView3.Columns[8].HeaderText = "Opis";
            dataGridView3.Columns[8].Width = 80;


            DataGridViewColumn column10 = dataGridView3.Columns[9];
            dataGridView3.Columns[9].HeaderText = "Količ. org";
            dataGridView3.Columns[9].Width = 40;


            DataGridViewColumn column11 = dataGridView3.Columns[10];
            dataGridView3.Columns[10].HeaderText = "Količina";
            dataGridView3.Columns[10].Width = 45;

            DataGridViewColumn column12 = dataGridView3.Columns[11];
            dataGridView3.Columns[11].HeaderText = "Pal. Ček";
            dataGridView3.Columns[11].Width = 35;

            DataGridViewColumn column13 = dataGridView3.Columns[12];
            dataGridView3.Columns[12].HeaderText = "Paletar";
            dataGridView3.Columns[12].Width = 150;

            DataGridViewColumn column14 = dataGridView3.Columns[13];
            dataGridView3.Columns[13].HeaderText = "Vreme pal.";
            dataGridView3.Columns[13].Width = 100;

            DataGridViewColumn column15 = dataGridView3.Columns[14];
            dataGridView3.Columns[14].HeaderText = "Kont. ček";
            dataGridView3.Columns[14].Width = 35;

            DataGridViewColumn column16 = dataGridView3.Columns[15];
            dataGridView3.Columns[15].HeaderText = "Kontr.";
            dataGridView3.Columns[15].Width = 150;

            DataGridViewColumn column17 = dataGridView3.Columns[16];
            dataGridView3.Columns[16].HeaderText = "Vreme kont";
            dataGridView3.Columns[16].Width = 100;
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertIsporuka isporuka = new InsertIsporuka();
            isporuka.InsertReklamacija(cboDatumUtovara.Text);
            progressBar1.Maximum = dataGridView3.Rows.Count;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected == true)
                {
                    if (row != null && row.Cells[0].Value != null)
                    {
                        //Provera
                        isporuka.InsertReklamacijaPostav(row.Cells[6].Value.ToString(), Convert.ToDecimal(row.Cells[9].Value.ToString()), Convert.ToInt32(row.Cells[4].Value.ToString()), row.Cells[5].Value.ToString(), row.Cells[7].Value.ToString());
                        progressBar1.Value = progressBar1.Value + 1;
                    }
                }
            }
            MessageBox.Show("Formirana je reklamacija");

        }
    }
}
