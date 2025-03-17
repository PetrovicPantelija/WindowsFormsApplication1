using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
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
    public partial class StatistikaPrijema : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        Statistika stat = new Statistika();
        public StatistikaPrijema()
        {
            InitializeComponent();
            FillPrijemnice();
            panel1.Visible = false;
        }
        private void FillPrijemnice()
        {
            SqlConnection conn = new SqlConnection(connect);

            var query = "select NPrStPre as Prijemnica,NPrDatPre as Datum,NPrStatus as Status from NPre Where NPrStatus='OD' or NPrStatus='PO'   order by NPrStPre desc";
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

        int primljenoTacno;
        int primljenoManje;
        int primljenoVise;

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    Prijemnica = Convert.ToInt32(row.Cells[0].Value.ToString());
                    var prijemData = stat.GetPrijemnica(Prijemnica);
                    foreach (var item in prijemData)
                    {
                        lblPrijemnica.Text = "Prijemnica:" + Prijemnica;
                        lblKreirao.Text = "Kreirao:" + item.Kreirao;
                        lblKreiranje.Text = "Datum i vreme:" + item.DatumKreiranja.ToString();
                    }

                    var stavkeData = stat.GetPrijemnicaStavke(Prijemnica);
                    foreach (var item in stavkeData)
                    {
                        if (item.Magacioner != null && item.Magacioner.Count > 0)
                        {
                            lblMagacioner.Text += string.Join("\n", item.Magacioner) + "\n";
                        }
                        lblStavke.Text = "Stavki na prijemnici: " + item.Stavke;
                        lblOdbacio.Text = "Odbačena: " + item.Odbacena;
                        lblKoristnikOdbacio.Text = "Korisnik odbacio: " + item.KorisnikOdbacio;
                        lblPocetakPrijema.Text = "Početak prijema: " + item.PrijemPocetak.ToString("dd.MM.yyyy HH:mm:ss");
                        lblKrajPrijema.Text = "Kraj prijema: " + item.PrijemKraj.ToString("dd.MM.yyyy HH:mm:ss");
                        lblVremePrijema.Text = "Vreme prijema:" + item.VremePrijema;
                        primljenoTacno = item.PrimljenoTacno;
                        primljenoManje = item.PrimljenoManje;
                        primljenoVise = item.PrimljenoVise;
                        lblPocetakUzimanja.Text = "Početak uzimanja: " + item.PocetakUzimanja.ToString("dd.MM.yyyy HH:mm:ss");
                        lblKrajUlaganja.Text = "Kraj ulaganja: " + item.KrajUlaganja.ToString("dd.MM.yyyy HH:mm:ss");
                        lblVremeUzUl.Text = "Vreme uzimanje/ulaganje: " + item.VremeUzimanjaUlaganja;
                    }


                    chart1.Series["Series1"].Points.Clear(); // Clear previous data points

                    chart1.Series["Series1"].Points.AddXY("", primljenoTacno);
                    chart1.Series["Series1"].Points[0].LegendText = "Primljeno Tacno: " + primljenoTacno;
                    chart1.Series["Series1"].Points[0].Color = Color.Green;

                    chart1.Series["Series1"].Points.AddXY("", primljenoManje);
                    chart1.Series["Series1"].Points[1].LegendText = "Primljeno Manje: " + primljenoManje;
                    chart1.Series["Series1"].Points[1].Color = Color.Red;

                    chart1.Series["Series1"].Points.AddXY("", primljenoVise);
                    chart1.Series["Series1"].Points[2].LegendText = "Primljeno Vise: " + primljenoVise;
                    chart1.Series["Series1"].Points[2].Color = Color.Blue;

                    chart1.Legends[0].IsDockedInsideChartArea = true;
                    chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                    chart1.ChartAreas[0].Area3DStyle.Perspective = 10;
                    chart1.ChartAreas[0].Area3DStyle.Inclination = 30;
                    chart1.ChartAreas[0].Area3DStyle.Rotation = 60;



                    chart2.Series["Series1"].Points.Clear();
                    chart2.Series["Series2"].Points.Clear();
                    chart2.Series["Series3"].Points.Clear();
                    chart2.Series["Series1"].LegendText = "Količina sa prijemnice";
                    chart2.Series["Series2"].LegendText = "Količina uzeta";
                    chart2.Series["Series3"].LegendText = "količina uložena";

                    var uzimanjeData = stat.GetUzimanje(Prijemnica);
                    foreach(var item in uzimanjeData)
                    {
                        chart2.Series["Series1"].Points.AddXY(item.ID, item.Kolicina);
                        chart2.Series["Series1"].IsValueShownAsLabel = true;
                        chart2.Series["Series2"].Points.AddXY(item.ID,item.KolicinaUzeta);
                        chart2.Series["Series2"].IsValueShownAsLabel = true;
                        chart2.Series["Series3"].Points.AddXY(item.ID, item.KolicinaUlozena);
                        chart2.Series["Series3"].IsValueShownAsLabel= true;

                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            SqlConnection conn = new SqlConnection(connect);

            var query = "select Korisnik,BarcodeLog.NPrPPreZap as ID,NPrPSifra as MP,Kolicina,SifSklad,Lokac from BarcodeLog inner join NPrep on BarcodeLog.NprPPreZap=NPreP.NPrPPreZap Where Dokument='Uzimanje iz prijemnice' and Oznaka="+Prijemnica+" order by BarcodeLog.NprPPreZap asc,Korisnik asc";
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);

            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Columns[0].Width = 70;
            dataGridView2.Columns[1].Width = 70;
            dataGridView2.Columns[2].Width = 70;
            dataGridView2.Columns[3].Width = 70;
            dataGridView2.Columns[4].Width = 70;
            dataGridView2.Columns[5].Width = 70;

            dataGridView2.BorderStyle = BorderStyle.Fixed3D;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            dataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            SqlConnection conn = new SqlConnection(connect);

            var query = "select Korisnik,BarcodeLog.NPrPPreZap as ID,NPrPSifra as MP,Kolicina,SifSklad,Lokac from BarcodeLog inner join NPrep on BarcodeLog.NprPPreZap=NPreP.NPrPPreZap Where Dokument='Ulaganje iz prijemnice' and Oznaka=" + Prijemnica + " order by BarcodeLog.NprPPreZap asc,Korisnik asc";
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);

            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Columns[0].Width = 70;
            dataGridView2.Columns[1].Width = 70;
            dataGridView2.Columns[2].Width = 70;
            dataGridView2.Columns[3].Width = 70;
            dataGridView2.Columns[4].Width = 70;
            dataGridView2.Columns[5].Width = 70;

            dataGridView2.BorderStyle = BorderStyle.Fixed3D;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            dataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void chart2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }
}
