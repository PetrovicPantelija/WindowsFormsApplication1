using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.ProizvodnjaUKadrovskoj
{
    public partial class frmPodaciOZaposlenima: Form
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public frmPodaciOZaposlenima()
        {
            InitializeComponent();
        }

        private void frmPodaciOZaposlenima_Load(object sender, EventArgs e)
        {
            FillEvidencija();
            FillCboFilter();
            DodajLegendu();
        }

        public void FillCboFilter() 
        {
            SqlConnection conn2 = new SqlConnection(connect);

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
            cboSektor.DataSource = ds1.Tables[0];
            cboSektor.DisplayMember = "Naziv";
            cboSektor.ValueMember = "ID";
        }
        private void FillEvidencija()
        {
            int mesec = dtDan.Value.Month;
            int godina = dtDan.Value.Year;

            var podaci = VratiEvidencijuIzBaze(mesec, godina);
            DataTable dtSQL = podaci.Item1;
            DataTable dtUkupno = podaci.Item2;

            PrikaziEvidencijuZaMesec(mesec, godina, dtSQL, dtUkupno);
        }
        private Tuple<DataTable, DataTable> VratiEvidencijuIzBaze(int mesec, int godina) 
        {
            string uslov = $" MONTH(Datum) = {mesec} AND YEAR(Datum) = {godina} ";
            string uslovMain = "";

            if (cboSektor.SelectedIndex > 0)
            {
                if (Convert.ToInt32(cboSektor.SelectedValue) > 0)
                if (Convert.ToInt32(cboSektor.SelectedValue) > 0)
                {
                    uslovMain =  "AND kp.IDSektora = " + Convert.ToInt32(cboSektor.SelectedValue);
                }
            }

            string upit = @" SELECT DISTINCT d.deSifra AS LargoBroj, " +
                                    "LTRIM(RTRIM(d.DeIme)) +' ' + LTRIM(RTRIM(d.DePriimek)) AS ImePrezime,  " +
                                    "LTRIM(RTRIM(s.Naziv)) AS Sektor, " +
                                    "rm.Naziv AS NazivRadnogMesta, " +
                                    "er.Datum, " +
                                    "er.BrojSati AS VremeRada, " +
                                    "er.TipRadaID AS TipRadaID, " +
                                    "er.Napomena, " +
                                    "LTRIM(RTRIM(tp.Naziv)) AS TipRada " +
                            "FROM    Delavci d  " +
                                    "LEFT JOIN (SELECT * FROM EvidencijaRada WHERE "+uslov+" )  er ON d.deSifra = er.LargoBroj " +
                                    "LEFT JOIN TipRada tp ON tp.ID = er.TipRadaID  " +
                                    "LEFT JOIN KadroviUProizvodnji kp ON kp.LargoBroj = d.deSifra " +
                                    "LEFT JOIN KadroviRadnihMesta krm ON krm.LargoBroj = d.deSifra AND krm.Aktivan = 1 " +
                                    "LEFT JOIN RadnaMesta rm ON krm.IDRadnoMesto = rm.ID " +
                                    "LEFT JOIN Sektor s ON kp.IDSektora = s.ID " +
                            "WHERE d.deSifStat= 'A' " + uslovMain;

            string upitUkupno = @" SELECT  LargoBroj, TipRadaID, SUM(BrojSati) AS UkupnoSati  " +
                                  "FROM    EvidencijaRada  " +
                                  "WHERE   YEAR(Datum) = " + godina + " AND MONTH(Datum) <= " + mesec + " "  +
                                  "GROUP BY LargoBroj, TipRadaID";


            using (SqlConnection conn = new SqlConnection(connect))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(upit, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                SqlDataAdapter adapterUkupno = new SqlDataAdapter(upitUkupno, conn);
                DataTable dtUkupno = new DataTable();
                adapterUkupno.Fill(dtUkupno);

                return new Tuple<DataTable, DataTable>(dt, dtUkupno);
            }
        }

        private void PrikaziEvidencijuZaMesec(int mesec, int godina, DataTable dtSQL, DataTable dtUkupno)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("RedniBr", typeof(int));
            dt.Columns.Add("LargoBroj");
            dt.Columns.Add("ImePrezime");
            dt.Columns.Add("Sektor");
            dt.Columns.Add("NazivRadnogMesta");

            int brojDana = DateTime.DaysInMonth(godina, mesec);

            for (int i = 1; i <= brojDana; i++)
            {
                dt.Columns.Add($"{i}_tip");
                dt.Columns.Add(i.ToString());
            }

            var query1 = "SELECT distinct LTRIM(RTRIM(Naziv)) AS TipRada,ID AS TipRadaID FROM TipRada Order By ID asc";

            DataTable dt1 = new DataTable();
            using (SqlConnection conn = new SqlConnection(connect))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query1, conn);
                
                adapter.Fill(dt1);
            }

            Dictionary<string, string> tipoviRada2 = new Dictionary<string, string>();
             var tipoviRada3 = dt1.AsEnumerable()
                .Select(r=> new { 
                    TipRada = r.Field<string>("TipRada"),
                    ID = r.Field<int>("TipRadaID").ToString()
                }).Distinct()
                .ToList(); ;
            Dictionary<string, string> tipoviRada1 = new Dictionary<string, string>();
            foreach (var tipoviRada in tipoviRada3)
            {
                dt.Columns.Add(tipoviRada.TipRada);
                tipoviRada1.Add(tipoviRada.TipRada, tipoviRada.ID);
            }

            dt.Columns.Add("Ukupno");
            dt.Columns.Add("Napomena");
            foreach (var tip in tipoviRada3)
            {
                string nazivKolone = $"Ukupno {tip.TipRada}";
                if(!dt.Columns.Contains(nazivKolone))
                dt.Columns.Add(nazivKolone);
            }


            var radnici = dtSQL.AsEnumerable()
                .Select(r => new
                {
                    LargoBroj = r.Field<int>("LargoBroj"),
                    ImePrezime = r.Field<string>("ImePrezime"),
                    Sektor = r.Field<string>("Sektor"),
                    NazivRadnogMesta = r.Field<string>("NazivRadnogMesta"),
                    Napomena = r.Field<string>("Napomena")
                })
                .Distinct()
                .ToList();

            int redniBroj = 1;
            HashSet<int> dodatiRadnici = new HashSet<int>();

            foreach (var radnik in radnici)
            {
                //ako je radnik sa tim Largo brojem vec dodat, preskoci
                if (dodatiRadnici.Contains(radnik.LargoBroj))
                    continue;

                dodatiRadnici.Add(radnik.LargoBroj);
                DataRow red = dt.NewRow();
                red["RedniBr"] = redniBroj++;
                red["LargoBroj"] = radnik.LargoBroj;
                red["ImePrezime"] = radnik.ImePrezime;
                red["Sektor"] = radnik.Sektor;
                red["NazivRadnogMesta"] = radnik.NazivRadnogMesta;

                double ukupno = 0;

                var dani = dtSQL.AsEnumerable()
                    .Where(r => r.Field<int>("LargoBroj") == radnik.LargoBroj);

                var napomene = dani
                    .Where(r => !string.IsNullOrWhiteSpace(r.Field<string>("Napomena")))
                    .Select(r =>
                    $"{r.Field<DateTime>("Datum").ToString("dd.MM")}.{r.Field<string>("Napomena")}").ToList();
           
                string sveNapomene = string.Join(", ", napomene);
                Dictionary<string, double> sumaPoTipu = new Dictionary<string, double>();

                foreach (var dan in dani)
                {
                    DateTime? datumNullable = dan.Field<DateTime?>("Datum");
                    if (datumNullable == null)
                        continue; 

                    DateTime datum = datumNullable.Value;
                    int danBroj = datum.Day;
                    double sati = dan.Field<double>("VremeRada");
                    string tip1 = dan.Field<string>("TipRada");
                    int tipId = dan.Field<int>("TipRadaID");

                    red[$"{danBroj}_tip"] = tipId;
                    red[danBroj.ToString()] = sati.ToString("0.00");

                    ukupno += sati;

                    if (tipoviRada1.TryGetValue(tip1, out string naziv))
                    {
                        // Sabiranje po tipu
                        if (!sumaPoTipu.ContainsKey(tip1))
                            sumaPoTipu[tip1] = 0;

                        sumaPoTipu[tip1] += sati;
                    }

                    else
                    {
                        // Ako tip nije mapiran
                        Console.WriteLine($"Nepoznat tip rada: '{tip1}' na dan {datum.ToShortDateString()}");
                    }
                    var ukupnoZaRadnika = dtUkupno.AsEnumerable()
                    .Where(r => r.Field<int>("LargoBroj") == radnik.LargoBroj);

                    foreach (var zapis in ukupnoZaRadnika)
                    {
                        int tipId1 = zapis.Field<int>("TipRadaID");
                        double sati1 = zapis.Field<double>("UkupnoSati");

                        var nazivTipa = tipoviRada3.FirstOrDefault(t => t.ID == tipId1.ToString())?.TipRada;
                        if (!string.IsNullOrEmpty(nazivTipa))
                        {
                            string nazivKolone = $"Ukupno {nazivTipa}";
                            if (dt.Columns.Contains(nazivKolone))
                            {
                                red[nazivKolone] = sati1.ToString("0.00");
                            }
                        }
                    }
                }

                // Dodavanje sumiranih vrednosti po tipu u red
                foreach (var tip in tipoviRada1)
                {
                    string naziv = tip.Value;
                    string sifra = tip.Key;
                    red[sifra] = sumaPoTipu.ContainsKey(sifra) ? sumaPoTipu[sifra].ToString("0.00") : "0.00";

                }

                //Dodaj kolonu "Ukupno"(suma svih sati)
                 //ukupno = sumaPoTipu.Values.Sum();
                 red["Ukupno"] = ukupno.ToString("0.00");
                 red["Napomena"] = sveNapomene;

                // Dodavanje reda u tabelu
                dt.Rows.Add(red);
            }

            dgEvidencija.DataSource = dt;
            dgEvidencija.ReadOnly = true;
            BojenjePoTipuRada bp = new BojenjePoTipuRada();
            foreach (DataGridViewColumn col in dgEvidencija.Columns)
            {
                if (col.Name.EndsWith("_tip"))
                    col.Visible = false;

                string name = col.Name;
                dgEvidencija.EnableHeadersVisualStyles = false;
                if (tipoviRada1.ContainsKey(name))
                {
                    if (tipoviRada1.TryGetValue(name, out string id))
                    {
                        if (bp.BojePoTipuRada.TryGetValue(id, out Color boja))
                        {
                            col.HeaderCell.Style.BackColor = boja;
                            //col.HeaderCell.Style.Font = new Font(dgEvidencija.Font, FontStyle.Bold);
                        }
                    }
                }
                else if (name.StartsWith("Ukupno ")) 
                {
                    string naziv = name.Substring("Ukupno ".Length);
                    var tipID = tipoviRada1.FirstOrDefault(kpv =>
                    kpv.Key.Equals(naziv,
                    StringComparison.OrdinalIgnoreCase)).Value;
                    if (!string.IsNullOrEmpty(tipID) && bp.BojePoTipuRada.TryGetValue(tipID, out Color ukupnoBoja))
                    {
                        col.HeaderCell.Style.BackColor = ukupnoBoja;
                    }
                }
            }
            // sirine kolona 
            if (dgEvidencija.Columns.Contains("RedniBr"))
                dgEvidencija.Columns["RedniBr"].Width = 60;
            if (dgEvidencija.Columns.Contains("LargoBroj"))
                dgEvidencija.Columns["LargoBroj"].Width = 60;
            if (dgEvidencija.Columns.Contains("ImePrezime"))
                dgEvidencija.Columns["ImePrezime"].Width = 180;
            if (dgEvidencija.Columns.Contains("Sektor"))
                dgEvidencija.Columns["Sektor"].Width = 120;
            if (dgEvidencija.Columns.Contains("Ukupno"))
                dgEvidencija.Columns["Ukupno"].Width = 70;
            if (dgEvidencija.Columns.Contains("NazivRadnogMesta"))
                dgEvidencija.Columns["NazivRadnogMesta"].Width = 180;
            if (dgEvidencija.Columns.Contains("Napomena"))
                dgEvidencija.Columns["Napomena"].Width = 380;

            for (int i = 0; i < dgEvidencija.Columns.Count; i++)
            {
                string columnName = dgEvidencija.Columns[i].Name;
                if (int.TryParse(columnName, out int dan) && dan >= 1 && dan <= 31)
                {
                    dgEvidencija.Columns[i].Width = 35;
                    dgEvidencija.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else if (tipoviRada1.ContainsKey(columnName) || columnName == "Ukupno")
                {
                    dgEvidencija.Columns[i].Width = 70;
                    dgEvidencija.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else if ( columnName.StartsWith("Ukupno "))
                {
                    string naziv = columnName.Substring("Ukupno ".Length);
                    if (tipoviRada1.ContainsKey(naziv))
                    {
                        dgEvidencija.Columns[i].Width = 70;
                        dgEvidencija.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            dgEvidencija.Columns["ImePrezime"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }
   
        private void dgEvidencija_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 ) 
            {
                int mesec = dtDan.Value.Month;
                int godina = dtDan.Value.Year;

                string colName = dgEvidencija.Columns[e.ColumnIndex].Name;

                if (int.TryParse(colName, out int dan))
                {
                    DateTime datum = new DateTime(godina, mesec, dan);
                    if (datum.DayOfWeek == DayOfWeek.Sunday)
                    {
                        e.CellStyle.BackColor = Color.LightGray;
                        return;
                    }
                }
                else 
                {
                    return;
                }
                string tipCol = $"{dan}_tip";
            
                var tip = dgEvidencija.Rows[e.RowIndex].Cells[tipCol].Value?.ToString();
                if (!string.IsNullOrEmpty(tip))
                {
                    var cell = dgEvidencija.Rows[e.RowIndex].Cells[e.ColumnIndex];
        
                    BojenjePoTipuRada boje = new BojenjePoTipuRada();
                    if (boje.BojePoTipuRada.TryGetValue(tip, out Color boja))
                    {
                        cell.Style.BackColor = boja;
                    }
                    else 
                    {
                        cell.Style.BackColor = Color.White;
                    }
                }
            }
        }

        private void DodajLegendu() 
        {
            var query1 = "SELECT ID AS TipRadaID, LTRIM(RTRIM(Naziv)) AS TipRada FROM TipRada";

            SqlConnection conn = new SqlConnection(connect);
            var da1 = new SqlDataAdapter(query1, conn);
            var ds1 = new DataSet();
            da1.Fill(ds1);

            FlowLayoutPanel legendaPanel = new FlowLayoutPanel();
            legendaPanel.Dock = DockStyle.Bottom;
            legendaPanel.Height = 45;

            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                //Color color;
                string id = ds1.Tables[0].Rows[i]["TipRadaID"].ToString();

                BojenjePoTipuRada boje = new BojenjePoTipuRada();
                if (boje.BojePoTipuRada.TryGetValue(id, out Color boja))
                {
                    legendaPanel.Controls.Add(NapraviOznaku(ds1.Tables[0].Rows[i]["TipRada"].ToString(), boja));
                }
                else
                {
                    legendaPanel.Controls.Add(NapraviOznaku(ds1.Tables[0].Rows[i]["TipRada"].ToString(), Color.White));
                }
            }

            this.Controls.Add(legendaPanel);
        }
        
        private Panel NapraviOznaku(string tekst, Color boja)
        {
            Panel p = new Panel();
            p.Width = 120;
            p.Height = 40;
            p.Margin = new Padding(5);
            p.BackColor = boja;
            p.BorderStyle = BorderStyle.FixedSingle;

            Label l = new Label();
            l.Text = tekst;
            l.Dock = DockStyle.Fill;
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.AutoSize = false;
            l.MaximumSize = new Size(120, 0);
            p.Controls.Add(l);

            return p;
        }

  
        private async void dtDan_ValueChanged(object sender, EventArgs e)
        {
            await Task.Delay(100);
            dgEvidencija.CellFormatting -= dgEvidencija_CellFormatting;
            dgEvidencija.CellFormatting += dgEvidencija_CellFormatting;
            FillEvidencija();
        }

        private void cboSektor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillEvidencija();
        }
    }
}
