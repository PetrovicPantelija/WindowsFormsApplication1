using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Barcode
{
    public partial class Trebovanja : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public Trebovanja()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            FillGV();
            FillCombo();
        }
        private void FillGV()
        {
            SqlConnection conn = new SqlConnection(connect);

            var query = "select Korisnik,Artikal as ID,NPrPSifra as MP,RTrim(NPrPNaziv) as KOD,Kolicina as Uzeto,SifSklad,Pozicija as Lokacija,ID as IdUzimanje " +
                "from aUzimanjeBarkodTreb " +
                "inner join NPreP on aUzimanjeBarkodTreb.Artikal=NPrep.NPrPPreZap " +
                "Where Zavrsen<>1 order by Artikal";
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[1].Width = 70;
            dataGridView1.Columns[2].Width = 70;
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].Width = 70;
            dataGridView1.Columns[5].Width = 60;
            dataGridView1.Columns[6].Width = 80;

            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
        }
        private void FilterKorisnik(string korisnik)
        {
            SqlConnection conn = new SqlConnection(connect);

            var query = "select Korisnik,Artikal as ID,NPrPSifra as MP,RTrim(NPrPNaziv) as KOD,Kolicina as Uzeto,SifSklad,Pozicija as Lokacija,ID as IdUzimanje " +
                "from aUzimanjeBarkodTreb " +
                "inner join NPreP on aUzimanjeBarkodTreb.Artikal=NPrep.NPrPPreZap " +
                "Where Zavrsen<>1 and Korisnik='" + korisnik + "' order by Artikal";
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[1].Width = 70;
            dataGridView1.Columns[2].Width = 70;
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].Width = 70;
            dataGridView1.Columns[5].Width = 60;
            dataGridView1.Columns[6].Width = 80;

            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
        }
            private void FillCombo()
        {
            var query = " Select Korisnik from Korisnici order by Korisnik asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboKorisnik.DataSource = ds.Tables[0];
            cboKorisnik.DisplayMember = "Korisnik";
            cboKorisnik.ValueMember = "Korisnik";

            var query3 = "SELECT name FROM sys.server_principals WHERE type IN ('S')";
            var da3 = new SqlDataAdapter(query3, conn);
            var ds3 = new DataSet();
            da3.Fill(ds3);
            comboBox1.DataSource = ds3.Tables[0];
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "name";

            var query4 = "Select DeSifra,RTrim(DeIme)+' '+RTrim(DePriimek) as Korisnik From Delavci order by DePriimek asc";
            var da4 = new SqlDataAdapter(query4, conn);
            var ds4 = new DataSet();
            da4.Fill(ds4);
            comboBox2.DataSource = ds4.Tables[0];
            comboBox2.DisplayMember = "Korisnik";
            comboBox2.ValueMember = "DeSifra";
        }

        private void btnSvi_Click(object sender, EventArgs e)
        {
            FillGV();
        }

        private void btnKorisnik_Click(object sender, EventArgs e)
        {
            FilterKorisnik(cboKorisnik.SelectedValue.ToString().TrimEnd());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Statistika stat = new Statistika();
            stat.InsertPrometTrans(comboBox1.SelectedValue.ToString().TrimEnd(), DateTime.Now, Convert.ToInt32(comboBox2.SelectedValue), "IZ");

            int PrStDok = stat.GetPrStDok();

            int rb = dataGridView1.SelectedRows.Count+1;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            { 
                if(row.Selected)
                {
                    rb--;
                    stat.InsertPrometTransPos(comboBox1.SelectedValue.ToString(), DateTime.Now, PrStDok, rb, Convert.ToInt32(row.Cells[5].Value), row.Cells[6].Value.ToString(), Convert.ToInt32(row.Cells[2].Value), 0, Convert.ToDecimal(row.Cells[4].Value), 0, "", "IZ", "IZD", "003");
                    stat.UpdateTrebovanje(Convert.ToInt32(row.Cells[7].Value), PrStDok);
                }
            }
            FillGV();
        }
    }
}
