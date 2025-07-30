using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using Antlr.Runtime.Misc;
using System.Windows.Controls.Primitives;
using static WindowsFormsApplication1.PakovanjaPlanovi.StavkePlanaPakovanjaNovi;
using Irony.Parsing;
using Microsoft.Ajax.Utilities;

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    public partial class NamenskoPakovanjeNovi : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        string Korisnik = frmLogovanje.user;
        int ID;
        public NamenskoPakovanjeNovi()
        {
            InitializeComponent();
        }
        public NamenskoPakovanjeNovi(int id)
        {
            InitializeComponent();
            ID = id;
            groupBox1.Visible = false;
        }
        public class Danas
        {
            public int MP { get; set; }
            public string Sifra { get; set; }
            public string Naziv { get; set; }
            public int Skladiste { get; set; }
            public decimal Potrebno { get; set; }
        }
        List<Danas> danasList;
        public class PrviDan
        {
            public int MP { get; set; }
            public string Sifra { get; set; }
            public string Naziv { get; set; }
            public int Skladiste { get; set; }
            public decimal Potrebno { get; set; }
        }
        public class DrugiDan
        {
            public int MP { get; set; }
            public string Sifra { get; set; }
            public string Naziv { get; set; }
            public int Skladiste { get; set; }
            public decimal Potrebno { get; set; }
        }
        List<PrviDan> prviDanList;
        List<DrugiDan> drugiDanList;

        string d, d1, d2;
        string danas = DateTime.Now.DayOfWeek.ToString();
        private void FillListDanas()
        {
            danasList = new List<Danas>();
            var query = "select PrSifMp as MP,MpStaraSif as Sifra,MpNaziv as Naziv,PrSifSklad as Skladiste,0-(Sum(PrPRevKol)-Sum(PrIzdKol)) as Potrebno " +
                "from Promet " +
                "inner join MaticniPodatki on Promet.PrSifMp=MaticniPodatki.MpSifra " +
                "Where PrDatTrans<='"+d+"' and PrSifSklad in (13,14) " +
                "Group by PrSifMp,MpStaraSif,MpNaziv,PrSifSklad " +
                "Having (Sum(PrPRevKol)-Sum(PrIzdKol))<0 " +
                "order by PrSifSklad asc";
            using (SqlConnection conn = new SqlConnection(connect))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            danasList.Add(new Danas
                            {
                                MP = Convert.ToInt32(dr["MP"]),
                                Sifra = dr["Sifra"].ToString().TrimEnd(),
                                Naziv = dr["Naziv"].ToString().TrimEnd(),
                                Skladiste = Convert.ToInt32(dr["Skladiste"].ToString()),
                                Potrebno = Convert.ToDecimal(dr["Potrebno"].ToString())
                            });
                        }
                    }
                    conn.Close();
                }
            }

            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = danasList;
        }
        private void FillDanPrvi()
        {
            var query = "select PrSifMp as MP,MpStaraSif as Sifra,MpNaziv as Naziv,PrSifSklad as Skladiste,0-(Sum(PrPRevKol)-Sum(PrIzdKol)) as Potrebno " +
               "from Promet " +
               "inner join MaticniPodatki on Promet.PrSifMp=MaticniPodatki.MpSifra " +
               "Where PrDatTrans<='" + d1 + "' and PrSifSklad in (13,14) " +
               "Group by PrSifMp,MpStaraSif,MpNaziv,PrSifSklad " +
               "Having (Sum(PrPRevKol)-Sum(PrIzdKol))<0 " +
               "order by PrSifSklad asc";

            prviDanList = new List<PrviDan>();
            using (SqlConnection conn = new SqlConnection(connect))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            prviDanList.Add(new PrviDan
                            {
                                MP = Convert.ToInt32(dr["MP"]),
                                Sifra = dr["Sifra"].ToString().TrimEnd(),
                                Naziv = dr["Naziv"].ToString().TrimEnd(),
                                Skladiste = Convert.ToInt32(dr["Skladiste"].ToString()),
                                Potrebno = Convert.ToDecimal(dr["Potrebno"].ToString())
                            });
                        }
                    }
                    conn.Close();
                }
            }

            foreach (var item in danasList)
            {
                var matched = prviDanList.FirstOrDefault(x => x.MP == item.MP && x.Skladiste == item.Skladiste);
                if (matched != null)
                {
                    matched.Potrebno -= item.Potrebno;
                }
            }

            prviDanList = prviDanList
                .Where(x => x.Potrebno > 0)
                .ToList();

            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = prviDanList;
        }
        private void FillDrugiDan()
        {
            var query = "select PrSifMp as MP,MpStaraSif as Sifra,MpNaziv as Naziv,PrSifSklad as Skladiste,0-(Sum(PrPRevKol)-Sum(PrIzdKol)) as Potrebno " +
                "from Promet " +
                "inner join MaticniPodatki on Promet.PrSifMp=MaticniPodatki.MpSifra " +
                "Where PrDatTrans<='" + d2 + "' and PrSifSklad in (13,14) " +
                "Group by PrSifMp,MpStaraSif,MpNaziv,PrSifSklad " +
                "Having (Sum(PrPRevKol)-Sum(PrIzdKol))<0 " +
                "order by PrSifSklad asc";
            drugiDanList = new List<DrugiDan>();

            using (SqlConnection conn = new SqlConnection(connect))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            drugiDanList.Add(new DrugiDan
                            {
                                MP = Convert.ToInt32(dr["MP"]),
                                Sifra = dr["Sifra"].ToString().TrimEnd(),
                                Naziv = dr["Naziv"].ToString().TrimEnd(),
                                Skladiste = Convert.ToInt32(dr["Skladiste"].ToString()),
                                Potrebno = Convert.ToDecimal(dr["Potrebno"].ToString())
                            });
                        }
                    }
                    conn.Close();
                }
            }


            foreach(var item in prviDanList)
            {
                var matched = drugiDanList.FirstOrDefault(x => x.MP == item.MP && x.Skladiste == item.Skladiste);
                if (matched != null)
                {
                    matched.Potrebno-=item.Potrebno;
                }
            }

            drugiDanList = drugiDanList
                .Where(x => x.Potrebno > 0)
                .ToList();

            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = drugiDanList;

        }
        private void TabCtrl()
        {


            if (danas == "Friday")
            {
                d = DateTime.Now.ToString("yyyy-MM-dd");
                d1 = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                d2 = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd");
            }
            if (danas == "Saturday")
            {
                d = DateTime.Now.ToString("yyyy-MM-dd");
                d1 = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd");
                d2 = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd");
            }
            if (danas!="Saturday" || danas!="Friday")
            {
                d = DateTime.Now.ToString("yyyy-MM-dd");
                d1 = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                d2 = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd");
            }

            FillListDanas();
            FillDanPrvi();
            FillDrugiDan();

            GVStyle();
        }
        private void GVStyle()
        {
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 180;
            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[4].Width = 70;
            dataGridView2.Columns[0].Width = 60;
            dataGridView2.Columns[1].Width = 180;
            dataGridView2.Columns[2].Width = 180;
            dataGridView2.Columns[3].Width = 70;
            dataGridView2.Columns[4].Width = 70;
            dataGridView3.Columns[0].Width = 60;
            dataGridView3.Columns[1].Width = 180;
            dataGridView3.Columns[2].Width = 180;
            dataGridView3.Columns[3].Width = 70;
            dataGridView3.Columns[4].Width = 70;


            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.FromArgb(125, 158, 192);
            dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            //dataGridView1.MultiSelect = false;
            dataGridView2.BorderStyle = BorderStyle.Fixed3D;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.FromArgb(125, 158, 192);
            dataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            //dataGridView1.MultiSelect = false;
            dataGridView3.BorderStyle = BorderStyle.Fixed3D;
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView3.BackgroundColor = Color.FromArgb(125, 158, 192);
            dataGridView3.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView3.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            //dataGridView1.MultiSelect = false;


        }
        private void FillStavke()
        {
            SqlConnection conn = new SqlConnection(connect);

            var query2 = @"SELECT 
                          s.ID,
                          s.IDPlanPakovanja,
                          s.RnBroj,
                          s.StatusRn,
                          s.VrstaRN,
                          s.Artikal,
                          s.Kod,
                          s.Naziv,
                          CAST(ISNULL(p.Potrebno, 0) AS decimal(18,2)) AS Potrebno, 
                          s.KolicinaZaProizvodnju,
                          s.PlaniraniKrajnjiDatumZavrsetka,
                          s.Planer,
                          ISNULL(s.Termin,0) AS Termin,
                          RTrim(s.Varijanta) as Varijanta 
                        FROM StavkePlanaPakovanja s
                        LEFT JOIN 
                            ( SELECT  DnpStevilka, 
                                      DnpSifMp,
                                      SUM(DnpKolZaIzdelavo) AS Potrebno
                              FROM DelNalogProizvodni
                              WHERE DNpSifStat NOT IN ('ZA', 'ST')
                              GROUP BY DnpStevilka, DnpSifMp
                            ) p ON s.RnBroj = p.DnpStevilka AND s.Artikal = p.DnpSifMp
                        WHERE s.IDPlanPakovanja =  " + ID + " order by RNBroj asc"; 
           
            //var query2 = "Select ID,IDPlanPakovanja,RnBroj,StatusRn,VrstaRN,Artikal,Kod,Naziv,d.DnpKolZaIzdelavo AS Potrebno,KolicinaZaProizvodnju,PlaniraniKrajnjiDatumZavrsetka,Planer from StavkePlanaPakovanja s left join DelNalogProizvodni d on s.RNBroj=d.DNpStevilka WHere IdPlanPakovanja=" + ID;
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);

            dataGridView5.DataSource = ds2.Tables[0];

            dataGridView5.BorderStyle = BorderStyle.Fixed3D;
            dataGridView5.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView5.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView5.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView5.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView5.BackgroundColor = Color.FromArgb(125, 158, 192);
            dataGridView5.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView5.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);

            dataGridView5.Columns[0].Width = 50;
            dataGridView5.Columns[1].Width = 70;
            dataGridView5.Columns[1].HeaderText = "IDPlana";
            dataGridView5.Columns[2].Width = 70;
            dataGridView5.Columns[3].Width = 60;
            dataGridView5.Columns[4].Width = 60;
            dataGridView5.Columns[5].Width = 80;
            dataGridView5.Columns[6].Width = 150;
            //dataGridView2.Columns[8].HeaderText = "Količina";
            dataGridView5.Columns[8].Width = 70;
            dataGridView5.Columns[8].ReadOnly = false;
            if (dataGridView5.Columns.Count > 1)
                dataGridView5.Columns[1].Visible = false;
            if (dataGridView5.Columns.Contains("Termin"))
                dataGridView5.Columns["Termin"].Visible = false;
            if (dataGridView5.Columns.Contains("Varijanta"))
                dataGridView5.Columns["Varijanta"].Visible = false;
        }
        private void NamenskoPakovanjeNovi_Load(object sender, EventArgs e)
        {
            FillStavke();
            TabCtrl();
        }
        private void brnDodaj_Click(object sender, EventArgs e)
        {
            InsertPakovanja ins = new InsertPakovanja();
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        ins.InsertStavkePakovanjaNamensko(ID, RN, StatusRN, Vrsta, Convert.ToInt32(row.Cells[0].Value.ToString()), row.Cells[1].Value.ToString().Trim(), row.Cells[2].Value.ToString().Trim(), Convert.ToDecimal(row.Cells[4].Value.ToString()), DateTime.Now, Korisnik);
                    }
                }
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        ins.InsertStavkePakovanjaNamensko(ID, RN, StatusRN, Vrsta, Convert.ToInt32(row.Cells[0].Value.ToString()), row.Cells[1].Value.ToString().Trim(), row.Cells[2].Value.ToString().Trim(), Convert.ToDecimal(row.Cells[4].Value.ToString()), DateTime.Now, Korisnik);
                    }
                }
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.Selected)
                    {
                        ins.InsertStavkePakovanjaNamensko(ID, RN, StatusRN, Vrsta, Convert.ToInt32(row.Cells[0].Value.ToString()), row.Cells[1].Value.ToString().Trim(), row.Cells[2].Value.ToString().Trim(), Convert.ToDecimal(row.Cells[4].Value.ToString()), DateTime.Now, Korisnik);
                    }
                }
            }
            catch { }
            TabCtrl();
            FillStavke();
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            InsertPakovanja ins = new InsertPakovanja();
            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                if (row.Selected)
                {
                    ins.DeleteStavkePakovanja(Convert.ToInt32(row.Cells[0].Value));
                }
            }
            FillStavke();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TabCtrl();
        }


        private void btnIzvestaj_Click(object sender, EventArgs e)
        {
            NamenskoPakovanjeIzvestaj frm = new NamenskoPakovanjeIzvestaj(ID);
            frm.Show();
        }
        int RN, Vrsta;
        string StatusRN;

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    var query = "Select DNpStZapisa,DnPSifVrDok,DnpStevilka,DnpSifVrsteDN,DnpKolZaIzdelavo,DnpIzdelanaKol,DnpSifSklad,DnpSifStat,DnpPlanDatLans from DelNalogProizvodni Where (DNpSifStat<>'ZA' and DNpSifStat<>'ST') and DnpSifMp=" + Convert.ToInt32(row.Cells[0].Value);
                    SqlConnection conn = new SqlConnection(connect);
                    var da = new SqlDataAdapter(query, conn);
                    var ds = new DataSet();
                    da.Fill(ds);
                    dataGridView4.ReadOnly = true;
                    dataGridView4.DataSource = ds.Tables[0];

                    dataGridView4.BorderStyle = BorderStyle.Fixed3D;
                    dataGridView4.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                    dataGridView4.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                    dataGridView4.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                    dataGridView4.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                    dataGridView4.BackgroundColor = Color.FromArgb(125, 158, 192);
                    dataGridView4.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                    dataGridView4.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);


                    dataGridView4.Columns[0].HeaderText = "ID";
                    dataGridView4.Columns[0].Width = 60;
                    dataGridView4.Columns[1].HeaderText = "Sif vr.Dok";
                    dataGridView4.Columns[1].Width = 70;
                    dataGridView4.Columns[2].HeaderText = "RN";
                    dataGridView4.Columns[2].Width = 70;
                    dataGridView4.Columns[3].HeaderText = "Vrsta";
                    dataGridView4.Columns[3].Width = 60;
                    dataGridView4.Columns[4].HeaderText = "Potrebno";
                    dataGridView4.Columns[4].Width = 70;
                    dataGridView4.Columns[5].HeaderText = "Izradjeno";
                    dataGridView4.Columns[5].Width = 70;
                    dataGridView4.Columns[6].HeaderText = "Skladište";
                    dataGridView4.Columns[6].Width = 60;
                    dataGridView4.Columns[7].HeaderText = "Status";
                    dataGridView4.Columns[7].Width = 50;
                    dataGridView4.Columns[8].HeaderText = "Datum";
                }
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected)
                {
                    var query = "Select DNpStZapisa,DnPSifVrDok,DnpStevilka,DnpSifVrsteDN,DnpKolZaIzdelavo,DnpIzdelanaKol,DnpSifSklad,DnpSifStat,DnpPlanDatLans from DelNalogProizvodni Where (DNpSifStat<>'ZA' and DNpSifStat<>'ST') and DnpSifMp=" + Convert.ToInt32(row.Cells[0].Value);
                    SqlConnection conn = new SqlConnection(connect);
                    var da = new SqlDataAdapter(query, conn);
                    var ds = new DataSet();
                    da.Fill(ds);
                    dataGridView4.ReadOnly = true;
                    dataGridView4.DataSource = ds.Tables[0];

                    dataGridView4.BorderStyle = BorderStyle.Fixed3D;
                    dataGridView4.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                    dataGridView4.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                    dataGridView4.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                    dataGridView4.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                    dataGridView4.BackgroundColor = Color.FromArgb(125, 158, 192);
                    dataGridView4.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                    dataGridView4.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);


                    dataGridView4.Columns[0].HeaderText = "ID";
                    dataGridView4.Columns[0].Width = 60;
                    dataGridView4.Columns[1].HeaderText = "Sif vr.Dok";
                    dataGridView4.Columns[1].Width = 70;
                    dataGridView4.Columns[2].HeaderText = "RN";
                    dataGridView4.Columns[2].Width = 70;
                    dataGridView4.Columns[3].HeaderText = "Vrsta";
                    dataGridView4.Columns[3].Width = 60;
                    dataGridView4.Columns[4].HeaderText = "Potrebno";
                    dataGridView4.Columns[4].Width = 70;
                    dataGridView4.Columns[5].HeaderText = "Izradjeno";
                    dataGridView4.Columns[5].Width = 70;
                    dataGridView4.Columns[6].HeaderText = "Skladište";
                    dataGridView4.Columns[6].Width = 60;
                    dataGridView4.Columns[7].HeaderText = "Status";
                    dataGridView4.Columns[7].Width = 50;
                    dataGridView4.Columns[8].HeaderText = "Datum";
                }
            }
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected)
                {
                    var query = "Select DNpStZapisa,DnPSifVrDok,DnpStevilka,DnpSifVrsteDN,DnpKolZaIzdelavo,DnpIzdelanaKol,DnpSifSklad,DnpSifStat,DnpPlanDatLans from DelNalogProizvodni Where (DNpSifStat<>'ZA' and DNpSifStat<>'ST') and DnpSifMp=" + Convert.ToInt32(row.Cells[0].Value);
                    SqlConnection conn = new SqlConnection(connect);
                    var da = new SqlDataAdapter(query, conn);
                    var ds = new DataSet();
                    da.Fill(ds);
                    dataGridView4.ReadOnly = true;
                    dataGridView4.DataSource = ds.Tables[0];

                    dataGridView4.BorderStyle = BorderStyle.Fixed3D;
                    dataGridView4.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                    dataGridView4.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                    dataGridView4.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                    dataGridView4.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                    dataGridView4.BackgroundColor = Color.FromArgb(125, 158, 192);
                    dataGridView4.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                    dataGridView4.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);


                    dataGridView4.Columns[0].HeaderText = "ID";
                    dataGridView4.Columns[0].Width = 60;
                    dataGridView4.Columns[1].HeaderText = "Sif vr.Dok";
                    dataGridView4.Columns[1].Width = 70;
                    dataGridView4.Columns[2].HeaderText = "RN";
                    dataGridView4.Columns[2].Width = 70;
                    dataGridView4.Columns[3].HeaderText = "Vrsta";
                    dataGridView4.Columns[3].Width = 60;
                    dataGridView4.Columns[4].HeaderText = "Potrebno";
                    dataGridView4.Columns[4].Width = 70;
                    dataGridView4.Columns[5].HeaderText = "Izradjeno";
                    dataGridView4.Columns[5].Width = 70;
                    dataGridView4.Columns[6].HeaderText = "Skladište";
                    dataGridView4.Columns[6].Width = 60;
                    dataGridView4.Columns[7].HeaderText = "Status";
                    dataGridView4.Columns[7].Width = 50;
                    dataGridView4.Columns[8].HeaderText = "Datum";
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            var query2 = "select ID,RNBroj,Artikal,Kod,KolicinaZaProizvodnju,RasporedPakovanja from StavkePlanaPakovanja Where IDPlanPakovanja=" + ID + " order by RasporedPakovanja";
            SqlConnection conn = new SqlConnection(connect);
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView6.DataSource = ds2.Tables[0];
            dataGridView6.ForeColor = Color.Black;

            dataGridView6.BorderStyle = BorderStyle.Fixed3D;
            dataGridView6.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView6.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView6.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView6.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView6.BackgroundColor = Color.FromArgb(125, 158, 192);
            dataGridView6.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

            dataGridView6.EnableHeadersVisualStyles = false;
            dataGridView6.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            dataGridView6.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkOrange;
            dataGridView6.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView6.Columns[0].Width = 50;
            dataGridView6.Columns[1].Width = 60;
            dataGridView6.Columns[2].Width = 60;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }

        private void btnPotvrda_Click(object sender, EventArgs e)
        {
            InsertPakovanja ins = new InsertPakovanja();
            foreach (DataGridViewRow row in dataGridView6.Rows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                int raspored = Convert.ToInt32(row.Cells["RasporedPakovanja"].Value);
                if (raspored > 0)
                {
                    ins.UpisiRaspored(id, raspored);
                }
                else
                {
                    MessageBox.Show("Mora se upisati pozicija pakovanja!");
                    return;
                }
            }
        }

        private void btnRealizacija_Click(object sender, EventArgs e)
        {
            try
            {

                InsertPakovanja ins = new InsertPakovanja();
                int RB = 1;
                foreach (DataGridViewRow row in dataGridView5.Rows)
                {
                    int id = Convert.ToInt32(row.Cells["ID"].Value);
                    ins.UpisiRaspored(id, RB);
                    RB++;
                }

                List<Stavke> stavke = new List<Stavke>();
                foreach (DataGridViewRow row in dataGridView5.Rows)
                {
                    stavke.Add(new Stavke
                    {   
                        ID = Convert.ToInt32(row.Cells[0].Value.ToString()),
                        RN = Convert.ToInt32(row.Cells[2].Value.ToString()),
                        Status = row.Cells[3].Value.ToString(),
                        Vrsta = Convert.ToInt32(row.Cells[4].Value.ToString()),
                        Artikal = Convert.ToInt32(row.Cells[5].Value.ToString()),
                        Kod = row.Cells[6].Value.ToString(),
                        Naziv = row.Cells[7].Value.ToString(),
                        KolicinaZaProizvodnju = Convert.ToDecimal(row.Cells[9].Value.ToString()),
                        KrajnjiDatum = Convert.ToDateTime(row.Cells[10].Value.ToString()),
                        Termin = Convert.ToInt32(row.Cells[12].Value.ToString()),
                        Varijanta = row.Cells[13].Value.ToString()
                    });
                }

                List<int> list = new List<int>();
                using (SqlConnection conn = new SqlConnection(connect))
                {
                    conn.Open();
                    string query = "select distinct IdStavkePlanaPakovanja from PregledStavkiPlanaPakovanja";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                list.Add(dr.GetInt32(0));
                            }
                        }
                    }
                    conn.Close();
                }
                list.ToArray();
                ins = new InsertPakovanja();
                foreach (var item in stavke)
                {
                    if (list.Contains(Convert.ToInt32(item.ID)) == false)
                    {
                        if (item.Varijanta == "")
                        {
                            item.Varijanta = "*";
                        }
                        ins.InsertPregledStavkiPakovanja(item.ID, item.KolicinaZaProizvodnju, item.Artikal, item.Varijanta, ID);
                    }
                }
                RealizacijaPlanaPakovanja frm = new RealizacijaPlanaPakovanja(ID);
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime datum = DateTime.Now;
            string datumPom = datum.ToString("yyyy-MM-dd");

            string[] path = { Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "NamenskoPakovanje_Plan-" };
            string folder = Path.Combine(path);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                ShouldQuote = (field) => false,
                Delimiter = ";"
            };
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            var query = "select '' as a,''as b,''as c,PrSifMp as MP,RTrim(MpStaraSif) as Sifra,'','',0-(Sum(PrPRevKol)-Sum(PrIzdKol)) as [|Potrebno|] " +
                "from Promet " +
                "inner join MaticniPodatki on Promet.PrSifMp=MaticniPodatki.MpSifra " +
               "Where PrDatTrans<='" + datumPom + "' and PrSifSklad in (13,14) " +
                "Group by PrSifMp,MpStaraSif,MpNaziv,PrSifSklad " +
                "Having (Sum(PrPRevKol)-Sum(PrIzdKol))<0 " +
                "order by PrSifSklad asc";
            SqlCommand cmd = new SqlCommand(query, conn);
            string file = folder + DateTime.Now.ToShortDateString() + ".csv";
            try
            {
                using (var textWriter = File.CreateText(file))
                {
                    using (CsvWriter csv = new CsvWriter(textWriter, config))
                    {
                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        foreach (DataColumn column in dt.Columns)
                        {
                            csv.WriteField(column.ColumnName);
                        }
                        csv.NextRecord();

                        foreach (DataRow row in dt.Rows)
                        {
                            for (var i = 0; i < dt.Columns.Count; i++)
                            {
                                csv.WriteField(row[i]);
                            }
                            csv.NextRecord();
                        }
                    }
                }
                MessageBox.Show("Kreiran dokument", "CSV");
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "CSV"); }
            conn.Close();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();
        }

        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                if (row.Selected)
                {
                    RN = Convert.ToInt32(row.Cells[2].Value.ToString());
                    Vrsta = Convert.ToInt32(row.Cells[3].Value.ToString());
                    StatusRN = row.Cells[7].Value.ToString();
                }
            }
        }
    }
}
