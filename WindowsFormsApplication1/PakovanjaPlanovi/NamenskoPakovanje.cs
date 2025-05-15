using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Drawing.Text;
using System.Linq;

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    public partial class NamenskoPakovanje : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        string Korisnik = frmLogovanje.user;
        public NamenskoPakovanje()
        {
            InitializeComponent();
        }
        int ID;
        public NamenskoPakovanje(int id)
        {
            ID = id;
            InitializeComponent();
        }
        DateTime datum;
        public class Danas
        {
            public int MP { get; set; }
            public string Sifra { get; set; }
            public string Naziv { get; set; }
            public int Skladiste { get; set; }
            public decimal Potrebno { get; set; }
        }
        List<Danas> danasList;
        private void FillListDanas()
        {
            danasList.Clear();
            danasList = new List<Danas>();
            var query = "select PrSifMp as MP,MpStaraSif as Sifra,MpNaziv as Naziv,PrSifSklad as Skladiste,0-(Sum(PrPRevKol)-Sum(PrIzdKol)) as Potrebno " +
                "from Promet " +
                "inner join MaticniPodatki on Promet.PrSifMp=MaticniPodatki.MpSifra " +
                "Where PrDatTrans<=GetDate() and PrSifSklad in (13,14) " +
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
        }
        private void FillGV()
        {
            FillListDanas();

            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = danasList;

            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.FromArgb(125, 158, 192);
            dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);

            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 180;
            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[4].Width = 70;
        }

        private void FillGVStavke()
        {
            SqlConnection conn = new SqlConnection(connect);

            var query2 = "Select ID,IDPlanPakovanja,RnBroj,StatusRn,VrstaRN,Artikal,Kod,Naziv,KolicinaZaProizvodnju,PlaniraniKrajnjiDatumZavrsetka,Planer from StavkePlanaPakovanja WHere IdPlanPakovanja=" + ID;
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);

            dataGridView2.DataSource = ds2.Tables[0];

            dataGridView2.BorderStyle = BorderStyle.Fixed3D;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.FromArgb(125, 158, 192);
            dataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);

            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[1].Width = 70;
            dataGridView2.Columns[1].HeaderText = "IDPlana";
            dataGridView2.Columns[2].Width = 70;
            dataGridView2.Columns[3].Width = 60;
            dataGridView2.Columns[4].Width = 60;
            dataGridView2.Columns[5].Width = 80;
            dataGridView2.Columns[6].Width = 150;
            //dataGridView2.Columns[8].HeaderText = "Količina";
            dataGridView2.Columns[8].Width = 70;
            dataGridView2.Columns[8].ReadOnly = false;
        }
        private void NamenskoPakovanje_Load(object sender, EventArgs e)
        {
            FillGV();
            FillGVStavke();
            dateTimePicker2.Value = DateTime.Now;
        }



        private void button2_Click(object sender, EventArgs e)
        {

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
                        ins.InsertStavkePakovanjaNamensko(ID, RN, StatusRN, Vrsta, Convert.ToInt32(row.Cells[0].Value.ToString()), row.Cells[1].Value.ToString().Trim(), row.Cells[2].Value.ToString().Trim(), Convert.ToDecimal(row.Cells[4].Value.ToString()), Convert.ToDateTime(dateTimePicker2.Value.ToString()), Korisnik);
                    }
                }
            }
            catch { }
            FillGV();
            FillGVStavke();
        }

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
                    dataGridView3.ReadOnly = true;
                    dataGridView3.DataSource = ds.Tables[0];

                    dataGridView3.BorderStyle = BorderStyle.Fixed3D;
                    dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                    dataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                    dataGridView3.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                    dataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                    dataGridView3.BackgroundColor = Color.FromArgb(125, 158, 192);
                    dataGridView3.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                    dataGridView3.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);


                    dataGridView3.Columns[0].HeaderText = "ID";
                    dataGridView3.Columns[0].Width = 60;
                    dataGridView3.Columns[1].HeaderText = "Sif vr.Dok";
                    dataGridView3.Columns[1].Width = 70;
                    dataGridView3.Columns[2].HeaderText = "RN";
                    dataGridView3.Columns[2].Width = 70;
                    dataGridView3.Columns[3].HeaderText = "Vrsta";
                    dataGridView3.Columns[3].Width = 60;
                    dataGridView3.Columns[4].HeaderText = "Potrebno";
                    dataGridView3.Columns[4].Width = 70;
                    dataGridView3.Columns[5].HeaderText = "Izradjeno";
                    dataGridView3.Columns[5].Width = 70;
                    dataGridView3.Columns[6].HeaderText = "Skladište";
                    dataGridView3.Columns[6].Width = 60;
                    dataGridView3.Columns[7].HeaderText = "Status";
                    dataGridView3.Columns[7].Width = 50;
                    dataGridView3.Columns[8].HeaderText = "Datum";

                }
            }
        }
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
        private void FillDrugiDan()
        {
            datum = dateTimePicker2.Value;
            string datumPom = datum.ToString("yyyy-MM-dd");

            var query = "select PrSifMp as MP,MpStaraSif as Sifra,MpNaziv as Naziv,PrSifSklad as Skladiste,0-(Sum(PrPRevKol)-Sum(PrIzdKol)) as Potrebno " +
                "from Promet " +
                "inner join MaticniPodatki on Promet.PrSifMp=MaticniPodatki.MpSifra " +
                "Where PrDatTrans<='" + datumPom + "' and PrSifSklad in (13,14) " +
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
            if (!prviDanList.Any())
            {
                FillDanPrvi();
            }

            foreach(var item in prviDanList)
            {
                var matched=drugiDanList.FirstOrDefault(x=>x.MP==item.MP && x.Skladiste==item.Skladiste);
                if (matched != null)
                {
                    matched.Potrebno-=item.Potrebno;
                }
            }
            drugiDanList = drugiDanList.Where(x => x.Potrebno > 0).ToList();

        }
        private void FillDanPrvi()
        {
            var query = "select PrSifMp as MP,MpStaraSif as Sifra,MpNaziv as Naziv,PrSifSklad as Skladiste,0-(Sum(PrPRevKol)-Sum(PrIzdKol)) as Potrebno " +
               "from Promet " +
               "inner join MaticniPodatki on Promet.PrSifMp=MaticniPodatki.MpSifra " +
               "Where PrDatTrans<=GetDate()+1 and PrSifSklad in (13,14) " +
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
        }
        private void RefreshGV()
        {
            DateTime prvi = DateTime.Now;
            DateTime drugi = dateTimePicker2.Value;
            string pPom = prvi.ToShortDateString();
            string dPom = drugi.ToShortDateString();

            TimeSpan ts = Convert.ToDateTime(dPom) - Convert.ToDateTime(pPom);
            int dani = ts.Days;
            if (dani == 1)
            {
                FillDanPrvi();

                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = prviDanList;

                dataGridView1.BorderStyle = BorderStyle.Fixed3D;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dataGridView1.BackgroundColor = Color.FromArgb(125, 158, 192);
                dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
                //dataGridView1.MultiSelect = false;

                dataGridView1.Columns[0].Width = 60;
                dataGridView1.Columns[1].Width = 180;
                dataGridView1.Columns[2].Width = 180;
                dataGridView1.Columns[3].Width = 70;
                dataGridView1.Columns[4].Width = 70;
            }
            if (dani >= 2)
            {
                FillDrugiDan();

                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = drugiDanList;

                dataGridView1.BorderStyle = BorderStyle.Fixed3D;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dataGridView1.BackgroundColor = Color.FromArgb(125, 158, 192);
                dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);

                dataGridView1.Columns[0].Width = 60;
                dataGridView1.Columns[1].Width = 180;
                dataGridView1.Columns[2].Width = 180;
                dataGridView1.Columns[3].Width = 70;
                dataGridView1.Columns[4].Width = 70;
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            RefreshGV();
        }
        int RN, Vrsta;
        string StatusRN;

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            InsertPakovanja ins = new InsertPakovanja();
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected)
                {
                    ins.DeleteStavkePakovanja(Convert.ToInt32(row.Cells[0].Value));
                }
            }
            FillGV();
            FillGVStavke();
        }

        private void btnIzvestaj_Click(object sender, EventArgs e)
        {
            NamenskoPakovanjeIzvestaj frm = new NamenskoPakovanjeIzvestaj(ID);
            frm.Show();
        }

        private void btnDanas_Click(object sender, EventArgs e)
        {
            var query = "select PrSifMp as MP,MpStaraSif as Sifra,MpNaziv as Naziv,PrSifSklad as Skladiste,0-(Sum(PrPRevKol)-Sum(PrIzdKol)) as Potrebno " +
                "from Promet " +
                "inner join MaticniPodatki on Promet.PrSifMp=MaticniPodatki.MpSifra " +
                "Where PrDatTrans<=GetDate() and PrSifSklad in (13,14) " +
                "Group by PrSifMp,MpStaraSif,MpNaziv,PrSifSklad " +
                "Having (Sum(PrPRevKol)-Sum(PrIzdKol))<0 " +
                "order by PrSifSklad asc";
            string a = query.ToString();
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.FromArgb(125, 158, 192);
            dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            //dataGridView1.MultiSelect = false;

            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 180;
            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[4].Width = 70;
        }

        private void btnKolicine_Click(object sender, EventArgs e)
        {
            InsertPakovanja ins = new InsertPakovanja();
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                int id = Convert.ToInt32(row.Cells["ID"].Value);
                decimal kolicina = Convert.ToDecimal(row.Cells["KolicinaZaProizvodnju"].Value);
                ins.UpdateNamenskoKolicina(id, kolicina);
            }
            FillGVStavke();
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected)
                {
                    RN = Convert.ToInt32(row.Cells[2].Value.ToString());
                    Vrsta = Convert.ToInt32(row.Cells[3].Value.ToString());
                    StatusRN = row.Cells[7].Value.ToString();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            datum = dateTimePicker2.Value;
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
    }
}
