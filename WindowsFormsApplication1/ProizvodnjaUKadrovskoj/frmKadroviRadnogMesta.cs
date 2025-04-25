using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.ProizvodnjaUKadrovskoj
{
    public partial class frmKadroviRadnogMesta: Form
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public frmKadroviRadnogMesta()
        {
            InitializeComponent();
        }
        private void FillGVZaposleni()
        {
            var select = "SELECT distinct d.DeSifra AS ID, d.DePriimek AS Prezime, d.DeIme AS Ime, CONVERT(varchar(10), d.DeDatRoj, 104) AS DatumRodjenja " +
                         "FROM   Delavci d " +
                         "WHERE  d.deSifStat = 'A' AND NOT EXISTS ( SELECT  1 " +
                                                                   "FROM   KadroviRadnihMesta kp " +
                                                                   "WHERE  kp.LargoBroj = d.DeSifra AND kp.Aktivan = 1)" +
                         " ORDER BY d.DeSifra asc";

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

            if (dataGridView2.Columns.Contains("Select"))
                dataGridView2.Columns["Select"].Width = 50;
            if (dataGridView2.Columns.Contains("ID"))
                dataGridView2.Columns["ID"].Width = 50;
            if (dataGridView2.Columns.Contains("DatumRodjenja"))
                dataGridView2.Columns["DatumRodjenja"].Width = 90;

            var query = "SELECT distinct Naziv,ID FROM RadnaMesta Order By Naziv asc";
            var da = new SqlDataAdapter(query, conn2);
            var ds = new DataSet();
            da.Fill(ds);
            cboRadnoMesto.DataSource = ds.Tables[0];
            cboRadnoMesto.DisplayMember = "Naziv";
            cboRadnoMesto.ValueMember = "ID";

            var da1 = new SqlDataAdapter(query, conn2);
            var ds1 = new DataSet();
            da1.Fill(ds1);
            DataRow row = ds1.Tables[0].NewRow();
            row["Naziv"] = "All";
            row["ID"] = -1;
            ds1.Tables[0].Rows.InsertAt(row, 0);
            cboRMestoKadrova.DataSource = ds1.Tables[0];
            cboRMestoKadrova.DisplayMember = "Naziv";
            cboRMestoKadrova.ValueMember = "ID";

        }

        private void FillGVKadroviRadnogMesta()
        {
            string condition = " AND 1 = 1 ";
            if (cboRMestoKadrova.SelectedIndex > 0)
            {
                if (Convert.ToInt32(cboRMestoKadrova.SelectedValue) > 0)
                {
                    condition = condition + "AND kp.IDRadnoMesto = " + Convert.ToInt32(cboRMestoKadrova.SelectedValue);
                }
            }
            var selectKP = "SELECT rm.Naziv AS RadnoMesto,d.DeSifra AS ID, d.DePriimek AS Prezime, d.DeIme AS Ime," +
                            "CONVERT(varchar(10), d.DeDatRoj, 104) AS DatumRodjenja,  kp.ID AS  KadroviRMID " +
                            "FROM KadroviRadnihMesta kp " +
                            "INNER JOIN Delavci d ON d.deSifra = kp.LargoBroj AND d.deSifStat = 'A' " +
                            "INNER JOIN RadnaMesta rm ON kp.IDRadnoMesto = rm.ID " +
                            "WHERE kp.Aktivan = 1 " + condition +
                            " ORDER BY d.DeSifra asc";
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

            if (dataGridView1.Columns.Contains("KadroviRMID"))
                dataGridView1.Columns["KadroviRMID"].Visible = false;
            if (dataGridView1.Columns.Contains("Select"))
                dataGridView1.Columns["Select"].Width = 50;
            if (dataGridView1.Columns.Contains("ID"))
                dataGridView1.Columns["ID"].Width = 50;
            if (dataGridView1.Columns.Contains("DatumRodjenja"))
                dataGridView1.Columns["DatumRodjenja"].Width = 90;
        }

        private void frmKadroviRadnogMesta_Load(object sender, EventArgs e)
        {
            FillGVZaposleni();
            FillGVKadroviRadnogMesta();
        }

        private void btnSnimi_Click(object sender, EventArgs e)
        {
            InsertKadroviRadnogMesta ks = new InsertKadroviRadnogMesta();
            foreach (DataGridViewRow red in dataGridView2.Rows)
            {
                var cellValue = red.Cells["Select"].Value;
                if (cellValue != DBNull.Value && Convert.ToBoolean(cellValue) == true && cboRadnoMesto.SelectedValue.ToString() != "")
                {
                    int id = Convert.ToInt32(red.Cells["ID"].Value);
                    string query = "SELECT TOP (1) id, idRadnoMesto, Napomena " +
                                   "FROM KadroviRadnihMesta " +
                                   "WHERE LargoBroj = " + id + " AND idRadnoMesto = " + Convert.ToInt32(cboRadnoMesto.SelectedValue) +
                                   " Order BY id desc";
                    SqlConnection conn2 = new SqlConnection(connect);
                    var da = new SqlDataAdapter(query, conn2);
                    var ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                      int RmID = Convert.ToInt32(ds.Tables[0].Rows[0]["idRadnoMesto"].ToString());
                      ks.UpdKadroviRM(id, RmID);
                       
                    }
                    else
                    {
                        ks.InsKadroviRadnihMesta(Convert.ToInt32(cboRadnoMesto.SelectedValue), id, "");
                    }
                }
            }
            FillGVZaposleni();
            FillGVKadroviRadnogMesta();
        }

        private void btnIzbrisi_Click(object sender, EventArgs e)
        {
        
            InsertKadroviRadnogMesta ks = new InsertKadroviRadnogMesta();
     
            foreach (DataGridViewRow red in dataGridView1.Rows)
            {
                var cellValue = red.Cells["Select"].Value;
                if (cellValue != DBNull.Value && Convert.ToBoolean(cellValue) == true) 
                {
                    int id = Convert.ToInt32(red.Cells["KadroviRMID"].Value);
                    ks.DelKadroviRadnogMesta(id);
                }
            }
            FillGVZaposleni();
            FillGVKadroviRadnogMesta();
        }

        private void cboRMestoKadrova_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGVKadroviRadnogMesta();
        }
    }
}
