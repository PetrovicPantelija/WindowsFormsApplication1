using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1.ProizvodnjaUKadrovskoj
{
    public partial class frmKadroviSektora: Form
    {

        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public frmKadroviSektora()
        {
            InitializeComponent();
        }
        private void FillGVZaposleni()
        {
            var select = "SELECT d.DeSifra AS ID, d.DePriimek AS Prezime, d.DeIme AS Ime, CONVERT(varchar(10), d.DeDatRoj, 104) AS DatumRodjenja " +
                         "FROM   Delavci d " +
                         "LEFT JOIN KadroviUProizvodnji kp ON d.deSifra = kp.LargoBroj " +
                         "WHERE d.deSifStat='A' AND kp.ID is null "+
                         "Order by d.DeSifra asc"; 
            SqlConnection conn2 = new SqlConnection(connect);
            var da2 = new SqlDataAdapter(select, conn2);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            DataColumn Col = ds2.Tables[0].Columns.Add("Select", typeof(bool));
            Col.SetOrdinal(0);
            ds2.Tables[0].Columns[0].DefaultValue = 0;
            dataGridView2.DataSource = ds2.Tables[0];
            dataGridView2.Columns[0].ReadOnly = false;
            dataGridView2.AllowUserToAddRows = false;

            for (int i = 1; i <= dataGridView2.Columns.Count - 1; i++)
            {
                dataGridView2.Columns[i].ReadOnly = true;
            }
         
            if (dataGridView2.Columns.Contains("ID"))
                dataGridView2.Columns["ID"].Width = 50;
            if (dataGridView2.Columns.Contains("Select"))
                dataGridView2.Columns["Select"].Width = 50;
            if (dataGridView2.Columns.Contains("DatumRodjenja"))
                dataGridView2.Columns["DatumRodjenja"].Width = 90;

            var query = "SELECT distinct Naziv,ID FROM Sektor Order By Naziv asc";
            var da = new SqlDataAdapter(query, conn2);
            var ds = new DataSet();
            da.Fill(ds);
            cboSektor.DataSource = ds.Tables[0];
            cboSektor.DisplayMember = "Naziv";
            cboSektor.ValueMember = "ID";

            var da1 = new SqlDataAdapter(query, conn2);
            var ds1 = new DataSet();
            da1.Fill(ds1);
            DataRow row = ds1.Tables[0].NewRow();
            row["Naziv"] = "All";
            row["ID"] = -1;
            ds1.Tables[0].Rows.InsertAt(row, 0);
            cboSektorKadrova.DataSource = ds1.Tables[0];
            cboSektorKadrova.DisplayMember = "Naziv";
            cboSektorKadrova.ValueMember = "ID";
          
        }
        private void FillGVKadroviUProizvodnji() 
        {
            string condition = "1 = 1 ";
            if (cboSektorKadrova.SelectedIndex > 0 )
            {
                if (Convert.ToInt32(cboSektorKadrova.SelectedValue) > 0)
                {
                    condition = condition +  "AND kp.IDSektora = " + Convert.ToInt32(cboSektorKadrova.SelectedValue);
                }
            }
            var selectKP = "SELECT s.Naziv AS Sektor,d.DeSifra AS ID, d.DePriimek AS Prezime, d.DeIme AS Ime, CONVERT(varchar(10), d.DeDatRoj, 104) AS DatumRodjenja, kp.ID  as KPID " +
                           "FROM KadroviUProizvodnji kp " +
                           "INNER JOIN Delavci d ON d.deSifra = kp.LargoBroj AND d.deSifStat='A' " +
                           "INNER JOIN Sektor s ON kp.IDSektora = s.ID " + 
                           "WHERE " + condition +
                           " Order by d.DeSifra asc";
            SqlConnection conn3 = new SqlConnection(connect);
            var da3 = new SqlDataAdapter(selectKP, conn3);
            var ds3 = new DataSet();
            da3.Fill(ds3);
            DataColumn Col1 = ds3.Tables[0].Columns.Add("Select", typeof(bool));
            Col1.SetOrdinal(0);
            dataGridView1.DataSource = ds3.Tables[0];
            dataGridView1.Columns[0].ReadOnly = false;
            dataGridView1.AllowUserToAddRows = false;

            for (int i = 1; i <= dataGridView1.Columns.Count - 1; i++)
            {
                dataGridView1.Columns[i].ReadOnly = true;
            }

            if (dataGridView1.Columns.Contains("KPID"))
                dataGridView1.Columns["KPID"].Visible = false;
            if (dataGridView1.Columns.Contains("ID"))
                dataGridView1.Columns["ID"].Width = 50;
                dataGridView1.Columns["ID"].Width = 50;
            if (dataGridView1.Columns.Contains("Select"))
                dataGridView1.Columns["Select"].Width = 50;
            if (dataGridView1.Columns.Contains("DatumRodjenja"))
                dataGridView1.Columns["DatumRodjenja"].Width = 90;
        }

        private void frmKadroviSektora_Load(object sender, EventArgs e)
        {
            FillGVZaposleni();
            FillGVKadroviUProizvodnji();
        }

        private void btnSnimiKadrove_Click(object sender, EventArgs e)
        {
            InsertKadroviSektora ks = new InsertKadroviSektora();
            foreach (DataGridViewRow red in dataGridView2.Rows)
            {
                var cellValue = red.Cells["Select"].Value;
                if (cellValue != DBNull.Value && Convert.ToBoolean(cellValue) == true && cboSektor.SelectedValue.ToString() != "")
                {
                    int zaposleniId = Convert.ToInt32(red.Cells["ID"].Value);
                    ks.InsKadroviUProizvodnji(zaposleniId, Convert.ToInt32(cboSektor.SelectedValue));
                }
            }
            FillGVZaposleni();
            FillGVKadroviUProizvodnji();
        }

        private void btnIzbrisi_Click(object sender, EventArgs e)
        {
            InsertKadroviSektora ks = new InsertKadroviSektora();
            foreach (DataGridViewRow red in dataGridView1.Rows)
            {
                var cellValue = red.Cells["Select"].Value;
                if (cellValue != DBNull.Value && Convert.ToBoolean(cellValue) == true)
                {
                    int id = Convert.ToInt32(red.Cells["KPID"].Value);
                    ks.DelKadroviUProizvodnji(id);
                }
            }

            FillGVZaposleni();
            FillGVKadroviUProizvodnji();
        }

        private void cboSektorKadrova_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGVKadroviUProizvodnji();
        }
    }
}
