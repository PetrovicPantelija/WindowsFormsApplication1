using DocumentFormat.OpenXml.Office2010.Excel;
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
using static WindowsFormsApplication1.Barcode.Statistika;

namespace WindowsFormsApplication1.Barcode
{
    public partial class Pregled : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public Pregled()
        {
            InitializeComponent();
            FillPrijemnice();
            timer1.Start();
        }
        private void FillPrijemnice()
        {
            SqlConnection conn = new SqlConnection(connect);

            var query = "select NPrStPre as Prijemnica,NPrDatPre as Datum,NPrStatus  as Status,Ucitana from NPre Where NPrStatus='PO'  order by NPrStPre desc";
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[1].Width = 250;

            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
        }
        int Prijemnica;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    Prijemnica = Convert.ToInt32(row.Cells[0].Value.ToString());
                    PopuniStavkePrijemnnice(Prijemnica);
                }
            }
        }
        private void PopuniStavkePrijemnnice(int prijemnica)
        {
            SqlConnection conn = new SqlConnection(connect);

            var query = "select NPrPPreZap as ID,NPrPStPos as RB,NPrPSifra as MP,RTrim(NPrPNaziv) as KOD,NPrPkolPre as Kolicina,Kolicina2 as PrimljenaKolicina," +
                "NPrPVpisalIme as Magacioner,NPrPSifSkl as SifSklad,RTrim(sklad.SkNaziv) as Skladiste,NPrPLokac as Lokacija " +
                "from NPreP " +
                "inner join sklad on NPreP.NPrPSifSkl = sklad.SkSifra " +
                "Where NPrPStPre = "+prijemnica;
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);

            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Columns["ID"].Width = 90;
            dataGridView2.Columns["RB"].Width = 70;
            dataGridView2.Columns["MP"].Width = 90;
            dataGridView2.Columns["KOD"].Width = 250;
            dataGridView2.Columns["Kolicina"].Width = 150;
            dataGridView2.Columns["PrimljenaKolicina"].Width = 150;
            dataGridView2.Columns["Magacioner"].Width = 120;
            dataGridView2.Columns["SifSklad"].Width = 70;
            dataGridView2.Columns["Skladiste"].Width = 150;
            dataGridView2.Columns["Lokacija"].Width = 150;

            dataGridView2.BorderStyle = BorderStyle.Fixed3D;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            dataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment= DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.DefaultCellStyle.Alignment= DataGridViewContentAlignment.MiddleCenter;


            dataGridView2.RowPrePaint += dataGridView2_RowPrePaint;
        }

        private void dataGridView2_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv?.Rows[e.RowIndex].DataBoundItem is DataRowView row)
            {
                object kolicinaObj = row["Kolicina"];
                object primljenaKolicinaObj = row["PrimljenaKolicina"];

                if (primljenaKolicinaObj == DBNull.Value) // PrimljenaKolicina is null
                {
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    decimal kolicina = Convert.ToDecimal(kolicinaObj);
                    decimal primljenaKolicina = Convert.ToDecimal(primljenaKolicinaObj);

                    if (kolicina == primljenaKolicina)
                    {
                        dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Green;
                        dgv.Rows[e.RowIndex].DefaultCellStyle.ForeColor= System.Drawing.Color.White;

                    }
                    else if (primljenaKolicina < kolicina)
                    {
                        dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                        dgv.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.White;

                    }
                    else if (kolicina < primljenaKolicina)
                    {
                        dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Blue;
                        dgv.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.White;

                    }
                }
            }
        }

        private void pravaSkladišteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SkladistePrava frm = new SkladistePrava();
            frm.Show();
        }
        
        private void statistikaPrijemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatistikaPrijema frm = new StatistikaPrijema();
            frm.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Pregled_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FillPrijemnice();
        }

        private void promenaLokacijeMPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PromenaLokacijeMP frm = new PromenaLokacijeMP();
            frm.Show();
        }

        private void prijemnicaponoviPrijemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    using (SqlConnection conn = new SqlConnection(connect))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "Update Npre Set Ucitana=0 WHere NPrStPre=@ID";
                            cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(row.Cells[0].Value.ToString()));
                            cmd.ExecuteNonQuery();

                            FillPrijemnice();
                        }
                        conn.Close();
                    }
                }
            }
        }

        private void trebovanjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trebovanja frm = new Trebovanja();
            frm.Show();
        }
    }
}
