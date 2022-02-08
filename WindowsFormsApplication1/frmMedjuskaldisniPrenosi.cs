using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace WindowsFormsApplication1
{
    public partial class frmMedjuskaldisniPrenosi : Form
    {
        int usao = 0;
        public frmMedjuskaldisniPrenosi()
        {
            InitializeComponent();
        }

        private void frmMedjuskaldisniPrenosi_Load(object sender, EventArgs e)
        {
            var select2 = " select PrTStDok from PrometTrans where PrTStatPrTrans = 'OD' and PrTTipPrTrans = 'MS' order by PrTStDok desc";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);

            cboMedjuskladisni.DataSource = ds2.Tables[0];
            cboMedjuskladisni.DisplayMember = "PrTStDok";
            cboMedjuskladisni.ValueMember = "PrTStDok";

            usao = 1;
        }

        private void cboMedjuskladisni_Leave(object sender, EventArgs e)
        {
            RefreshDataGrid1();  
        }

        private void RefreshDataGrid1()
        {
            var select = "  select PrTpStZapisa as ID, PrTpStDokPos as RB, PrTpSifMp as Sifra, PrTpStaraSifMp as Kod, PrTpPrevKol as Kolicina, kolicina2, " +
             "  PrTpSifSklad as SkladIz, PrTpLok as LokIz, Lokac1.LokOpis as LokIzIz, " +
             "  PrTpSifSkladpr as SkladU, PrTpLokpr as LokU, Lokac2.LokOpis as LokUO from PrometTransPos " +
             "  inner Join Lokac as Lokac1 on Lokac1.LokSifra = PrometTransPos.PrTpLok " +
             "  inner Join Lokac as Lokac2 on Lokac2.LokSifra = PrometTransPos.PrTpLokpr " +
             "  where PrTpStDokumenta =  " + Convert.ToInt32(cboMedjuskladisni.SelectedValue);

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
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Šifra";
            dataGridView1.Columns[2].Width = 70;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Kod";
            dataGridView1.Columns[3].Width = 130;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Kol";
            dataGridView1.Columns[4].Width = 50;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Kol pred";
            dataGridView1.Columns[5].Width = 50;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "SkladIz";
            dataGridView1.Columns[6].Width = 50;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "LokIz";
            dataGridView1.Columns[7].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "LokIzOpis";
            dataGridView1.Columns[8].Width = 150;

            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "SkladU";
            dataGridView1.Columns[9].Width = 50;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "LokU";
            dataGridView1.Columns[10].Width = 100;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "LokUOpis";
            dataGridView1.Columns[11].Width = 150;

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
                if (Razlika == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.SelectionBackColor = Color.Green;
                }
                else if (Razlika > 0)
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
             

        }


        private void RefreshDataGridPoBroju()
        {
            var select = "  select PrTpStZapisa as ID, PrTpStDokPos as RB, PrTpSifMp as Sifra, PrTpStaraSifMp as Kod, PrTpPrevKol as Kolicina, kolicina2, " +
             "  PrTpSifSklad as SkladIz, PrTpLok as LokIz, Lokac1.LokOpis as LokIzIz, " +
             "  PrTpSifSkladpr as SkladU, PrTpLokpr as LokU, Lokac2.LokOpis as LokUO from PrometTransPos " +
             "  inner Join Lokac as Lokac1 on Lokac1.LokSifra = PrometTransPos.PrTpLok " +
             "  inner Join Lokac as Lokac2 on Lokac2.LokSifra = PrometTransPos.PrTpLokpr " +
             "  where PrTpStDokumenta =  " + Convert.ToInt32(txtMSP.Text);

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
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Šifra";
            dataGridView1.Columns[2].Width = 70;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Kod";
            dataGridView1.Columns[3].Width = 130;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Kol";
            dataGridView1.Columns[4].Width = 50;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Kol pred";
            dataGridView1.Columns[5].Width = 50;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "SkladIz";
            dataGridView1.Columns[6].Width = 50;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "LokIz";
            dataGridView1.Columns[7].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "LokIzOpis";
            dataGridView1.Columns[8].Width = 150;

            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "SkladU";
            dataGridView1.Columns[9].Width = 50;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "LokU";
            dataGridView1.Columns[10].Width = 100;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "LokUOpis";
            dataGridView1.Columns[11].Width = 150;

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
                if (Razlika == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.SelectionBackColor = Color.Green;
                }
                else if (Razlika > 0)
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


        }


        private void btnPrihvatiIzmene_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    InsertMedjuskladisniPrenos ins = new InsertMedjuskladisniPrenos();
                    ins.UpdateMedjuskladisniBrojKolicina(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToDouble(row.Cells[5].Value.ToString()));
                    MessageBox.Show("Završena je izmena stavki");
                
                }
            }
            
        }

        private void cboMedjuskladisni_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (usao == 1)
            {
                RefreshDataGrid1();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    InsertMedjuskladisniPrenos ins = new InsertMedjuskladisniPrenos();
                    ins.UpdatePrometTransKolicinaStavka(Convert.ToInt32(Convert.ToInt32(row.Cells[0].Value.ToString())));
                }
            }
            MessageBox.Show("Završena je izmena stavki");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var select2 = " select PrTStDok from PrometTrans where PrTStatPrTrans = 'OD' and PrTTipPrTrans = 'MS' order by PrTStDok desc";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);

            cboMedjuskladisni.DataSource = ds2.Tables[0];
            cboMedjuskladisni.DisplayMember = "PrTStDok";
            cboMedjuskladisni.ValueMember = "PrTStDok";

            usao = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshDataGrid1();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RefreshDataGridPoBroju();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    InsertMedjuskladisniPrenos ins = new InsertMedjuskladisniPrenos();
                    ins.UpdatePrometTransKolicina2(Convert.ToInt32(txtMSP.Text));

                    

                }
            }
            MessageBox.Show("Završena je izmena stavki");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    InsertMedjuskladisniPrenos ins = new InsertMedjuskladisniPrenos();
                    ins.UpdateMedjuskladisniBrojKolicina(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToDouble(row.Cells[5].Value.ToString()));
                    MessageBox.Show("Završena je izmena stavki");

                }
            }
        }
    }
}
