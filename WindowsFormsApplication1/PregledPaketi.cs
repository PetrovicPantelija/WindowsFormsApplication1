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
    public partial class PregledPaketi : Form
    {
        public PregledPaketi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var select = "SELECT [ShippingID] ,[ShippingNo],[DatumUtovara],[DatumIstovara],[BrojUtovara],[BrojIstovara],[BrojPalete],[KupacIDz],[SalonIDz] ,[PackageCount] ,[Stanje]   FROM [dbo].[RkShipping] where ShippingNo = '" + textBox1.Text + "'";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            // dataGridView1.ClearSelection();
            // dataGridView1.CurrentCell.Selected = false;
            /*
            dataGridView1.Columns[2].Width = 280;
            dataGridView1.Columns[3].Width = 160;
            dataGridView1.Columns[4].Width = 270;
            dataGridView1.Columns[5].Width = 120;
            dataGridView1.Columns[6].Width = 350;
            dataGridView1.Columns[0].Width = 120;
            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[0].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            dataGridView1.Columns[0].DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Red;
            dataGridView1.Columns[1].DefaultCellStyle.ForeColor = System.Drawing.Color.Green;
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            */
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string RowType = row.Cells[10].Value.ToString();
                if (RowType == "0")
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                    row.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                }
            }

            var select2 = "SELECT [ShippingItemPakID],[ShipingIDz],[ProizvodIDz],[ProizvodGTIN],[ProizvodGTINName],[PaketName],[Porudzbina],[Napomena],[Kolicina],[LargoProID],[LargoPakID],[Pakerista],[Kontrolisao] FROM [dbo].[RkShippingItemPak] inner join RkShipping on RkShipping.ShippingID=RkShippingItemPak.ShipingIdz where RkShipping.ShippingNo = '" + textBox1.Text + "'";

            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds2.Tables[0];
            
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                string RowType = row.Cells[11].Value.ToString();
                if (RowType == "0")
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                    row.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                }
                string RowType2 = row.Cells[12].Value.ToString();
                if (RowType == "0")
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.SelectionBackColor = Color.Red;
                }

            }



        }
    }
}
