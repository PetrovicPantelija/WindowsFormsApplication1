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



namespace WindowsFormsApplication1.Magacin
{
    public partial class frmOtpremnicaUvozPocetna : Form
    {
        public frmOtpremnicaUvozPocetna()
        {
            InitializeComponent();
        }

        private void frmOtpremnicaUvoz_Load(object sender, EventArgs e)
        {
            var select = " Select Distinct DatumIstovara, Cast(SUBSTRING(DatumIstovara, 1, Charindex('.',DatumIstovara) -1) as int)  as  Dan, Cast(Replace(SUBSTRING(DatumIstovara, Charindex('.',DatumIstovara)+1, Len(DatumIstovara)-12 -1 -( (Charindex('.',DatumIstovara) ))),'.','') as int) as  Mesec  From RkShippingPocetna where Stanje = 1  order by  Mesec, Dan";
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
            RefreshDataGrid();
            button2.Enabled = false;
        }

        private void RefreshDataGrid()
        {
            var select2 = "SELECT [LargoPakID],LargoNaziv,sum([KolicinaOrg]) as Kolicina,1,  PaketName, RkShippingItemPakPocetna.ProizvodGTIN, [RkShippingPocetna].BrojIstovara as BrojUtovara,1  FROM [dbo].[RkShippingItemPakPocetna] inner join RkShippingPocetna on RkShippingPocetna.ShippingID=RkShippingItemPakPocetna.ShipingIdz " +
" where (PaketName NOT  LIKE 'RP%' and PaketName NOT  LIKE 'SOKLA%') and RkShippingPocetna.DatumIstovara = '" + cboDatumUtovara.Text + "'" +
" group by [LargoPakID], LargoNaziv,RkShippingItemPakPocetna.ProizvodGTIN,PaketName, [RkShippingPocetna].BrojIstovara " +
" union " +
" SELECT [LargoPakID],LargoNaziv,[KolicinaOrg] as Kolicina, 1, PaketName, RkShippingItemPakPocetna.ProizvodGTIN, [RkShippingPocetna].BrojIstovara as BrojUtovara, rb  FROM [dbo].[RkShippingItemPakPocetna] inner join RkShippingPocetna on RkShippingPocetna.ShippingID=RkShippingItemPakPocetna.ShipingIdz " +
" where (PaketName LIKE 'RP%' or PaketName LIKE 'SOKLA%') and RkShippingPocetna.DatumIstovara = '" + cboDatumUtovara.Text + "'";

            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            //   textBox1.Text = select2;

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds2.Tables[0];
            // [ShipingIDz],Paleta, [PaketName],[Kolicina],[LargoPakID],[Pakerista],[Kontrolisao], Tezina
            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "LargoID";
            dataGridView3.Columns[0].Width = 80;

            DataGridViewColumn column1 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "Largo naziv";
            dataGridView3.Columns[1].Width = 150;

            DataGridViewColumn column2 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "Kolicina";
            dataGridView3.Columns[2].Width = 40;

            DataGridViewColumn column3 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "ID";
            dataGridView3.Columns[3].Width = 40;

            DataGridViewColumn column4 = dataGridView3.Columns[4];
            dataGridView3.Columns[4].HeaderText = "Ime paketa";
            dataGridView3.Columns[4].Width = 180;


            DataGridViewColumn column5 = dataGridView3.Columns[5];
            dataGridView3.Columns[5].HeaderText = "Proizvod GTIN";
            dataGridView3.Columns[5].Width = 150;

            DataGridViewColumn column6 = dataGridView3.Columns[6];
            dataGridView3.Columns[6].HeaderText = "Broj utovara";
            dataGridView3.Columns[6].Width = 80;


            // DodajKoloneDataGrid2();


        }
        private void DodajKoloneDataGrid2()
        {

            //
            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "LargoID";
            dataGridView2.Columns[0].Width = 40;

            DataGridViewColumn column1 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Largo naziv";
            dataGridView2.Columns[1].Width = 180;

            DataGridViewColumn column2 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Kolicina";
            dataGridView2.Columns[2].Width = 40;

            DataGridViewColumn column3 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "ID";
            dataGridView2.Columns[3].Width = 40;

            DataGridViewColumn column4 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "Ime paketa";
            dataGridView2.Columns[4].Width = 180;


            DataGridViewColumn column5 = dataGridView2.Columns[5];
            dataGridView2.Columns[5].HeaderText = "Proizvod GTIN";
            dataGridView2.Columns[5].Width = 180;


        }

        private void RefreshDataGridNeaktivni()
        {
            var select2 = "SELECT Distinct [LargoPakID],LargoNaziv,PaketName, RkShippingItemPakPocetna.ProizvodGTIN ,MaticniPodatki.MpStatus " +
 " FROM [dbo].[RkShippingItemPakPocetna] inner join RkShippingPocetna on RkShippingPocetna.ShippingID=RkShippingItemPakPocetna.ShipingIdz " +
 " inner join MaticniPodatki on  [RkShippingItemPakPocetna].LargoPakID = MaticniPodatki.MpSifra " +
 " where MaticniPodatki.MpStatus = 1 and RkShippingPocetna.DatumIstovara = '" + cboDatumUtovara.Text + "'";

            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);



            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds2.Tables[0];
            // [ShipingIDz],Paleta, [PaketName],[Kolicina],[LargoPakID],[Pakerista],[Kontrolisao], Tezina
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "LargoID";
            dataGridView1.Columns[0].Width = 80;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "LArgo naziv";
            dataGridView1.Columns[1].Width = 280;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Paket";
            dataGridView1.Columns[2].Width = 280;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Proizvod GTIN";
            dataGridView1.Columns[3].Width = 280;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Aktivan";
            dataGridView1.Columns[4].Width = 100;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertIsporuka isporuka = new InsertIsporuka();
            isporuka.InsertDobavnicaPocetna(cboDatumUtovara.Text);
            progressBar1.Maximum = dataGridView3.Rows.Count;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row != null && row.Cells[0].Value != null)
                {
                    isporuka.InsertDobavnicaPostavPocetna(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToDecimal(row.Cells[2].Value.ToString()), Convert.ToInt32(row.Cells[3].Value.ToString()), row.Cells[5].Value.ToString());
                    progressBar1.Value = progressBar1.Value + 1;
                }
            }
            MessageBox.Show("Formirana je otpremnica");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshDataGridNeaktivni();
            int counter = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                counter = counter + 1;
                button2.Enabled = false;
                button6.Enabled = false;
            }
            if (counter == 0)
            {
                button2.Enabled = true;
                button6.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button6.Enabled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //to copy the rows you need to have created the columns:
            for (int i = 0; i < dataGridView3.SelectedRows.Count; i++)
            {
                int index = dataGridView2.Rows.Add();
                dataGridView2.Rows[index].Cells[0].Value = dataGridView3.SelectedRows[i].Cells[0].Value.ToString();
                dataGridView2.Rows[index].Cells[1].Value = dataGridView3.SelectedRows[i].Cells[1].Value.ToString();
                dataGridView2.Rows[index].Cells[2].Value = dataGridView3.SelectedRows[i].Cells[2].Value.ToString();
                dataGridView2.Rows[index].Cells[3].Value = dataGridView3.SelectedRows[i].Cells[3].Value.ToString();
                dataGridView2.Rows[index].Cells[4].Value = dataGridView3.SelectedRows[i].Cells[4].Value.ToString();
                dataGridView2.Rows[index].Cells[5].Value = dataGridView3.SelectedRows[i].Cells[5].Value.ToString();

            }
            foreach (DataGridViewRow selRow in dataGridView3.SelectedRows.OfType<DataGridViewRow>().ToArray())
            {
                dataGridView3.Rows.Remove(selRow);
                // toDataGridView.Rows.Add(selRow);
            }

            /*
                        for (int selRow = dataGridView3.Rows.Count - 1; selRow >= 0; selRow--)
                        {
                            dataGridView2.Rows.Add(selRow);
                            dataGridView3.Rows.Remove(selRow);
                        }
             * */
        }

        private void button6_Click(object sender, EventArgs e)
        {
            InsertIsporuka isporuka = new InsertIsporuka();
            isporuka.InsertDobavnicaPocetna(cboDatumUtovara.Text);
            progressBar1.Maximum = dataGridView2.Rows.Count;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row != null && row.Cells[0].Value != null)
                {
                    isporuka.InsertDobavnicaPostavPocetna(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToDecimal(row.Cells[2].Value.ToString()), Convert.ToInt32(row.Cells[3].Value.ToString()), row.Cells[5].Value.ToString());
                    progressBar1.Value = progressBar1.Value + 1;
                }
            }
            MessageBox.Show("Formirana je otpremnica");
        }

        private void cboDatumUtovara_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

