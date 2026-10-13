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
    public partial class PromenaLokacijeMP : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public PromenaLokacijeMP()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            FillGV();
            FillCombo();
        }
        private void FillGV()
        {
            SqlConnection conn = new SqlConnection(connect);

            var query = "select Korisnik,Artikal as ID,NPrPSifra as MP,RTrim(NPrPNaziv) as KOD,KolicinaUzeta as Uzeto,SifSklad,Pozicija as Lokacija,ID as IdUzimanje " +
                "from aUzimanjeBarkod " +
                "inner join NPreP on aUzimanjeBarkod.Artikal=NPrep.NPrPPreZap " +
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



            var query2 = "select Korisnik,Artikal,NPrPSifra,RTrim(NPrPNaziv),KolicinaUlozena,SifSklad,Pozicija,ID as idUlaganje " +
                  "from aUlaganjeBarkod " +
                  "inner join NPreP on aUlaganjeBarkod.Artikal=NPrep.NPrPPreZap " +
                  "Where Zavrsen<>1 order by Artikal asc";
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);

            dataGridView2.DataSource = ds2.Tables[0];
            dataGridView2.Columns[0].Width = 70;
            dataGridView2.Columns[1].Width = 70;
            dataGridView2.Columns[2].Width = 70;
            dataGridView2.Columns[3].Width = 200;
            dataGridView2.Columns[4].Width = 70;
            dataGridView2.Columns[5].Width = 60;
            dataGridView2.Columns[6].Width = 80;

            dataGridView2.BorderStyle = BorderStyle.Fixed3D;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            dataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
        }
        private void FilterKorisnik(string korisnik)
        {
            SqlConnection conn = new SqlConnection(connect);

            var query = "select Korisnik,Artikal as ID,NPrPSifra as MP,RTrim(NPrPNaziv) as KOD,KolicinaUzeta as Uzeto,SifSklad,Pozicija as Lokacija " +
                "from aUzimanjeBarkod " +
                "inner join NPreP on aUzimanjeBarkod.Artikal=NPrep.NPrPPreZap " +
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



            var query2 = "select Korisnik,Artikal,NPrPSifra,RTrim(NPrPNaziv),KolicinaUlozena,SifSklad,Pozicija " +
                  "from aUlaganjeBarkod " +
                  "inner join NPreP on aUlaganjeBarkod.Artikal=NPrep.NPrPPreZap " +
                  "Where Zavrsen<>1 and Korisnik='" + korisnik + "' order by Artikal asc";
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);

            dataGridView2.DataSource = ds2.Tables[0];
            dataGridView2.Columns[0].Width = 70;
            dataGridView2.Columns[1].Width = 70;
            dataGridView2.Columns[2].Width = 70;
            dataGridView2.Columns[3].Width = 200;
            dataGridView2.Columns[4].Width = 70;
            dataGridView2.Columns[5].Width = 60;
            dataGridView2.Columns[6].Width = 80;

            dataGridView2.BorderStyle = BorderStyle.Fixed3D;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            dataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
        }
        private void FilterPrijemnica(int prijemnica)
        {
            SqlConnection conn = new SqlConnection(connect);

            var query = "select Korisnik,Artikal as ID,NPrPSifra as MP,RTrim(NPrPNaziv) as KOD,KolicinaUzeta as Uzeto,SifSklad,Pozicija as Lokacija " +
                "from aUzimanjeBarkod " +
                "inner join NPreP on aUzimanjeBarkod.Artikal=NPrep.NPrPPreZap " +
                "Where Zavrsen<>1 and Broj=" + prijemnica + " order by Artikal";
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



            var query2 = "select Korisnik,Artikal as ID,NPrPSifra as MP,RTrim(NPrPNaziv) as KOD,KolicinaUlozena as Ulozeno,SifSklad,Pozicija as Lokacija " +
                  "from aUlaganjeBarkod " +
                  "inner join NPreP on aUlaganjeBarkod.Artikal=NPrep.NPrPPreZap " +
                  "Where Zavrsen<>1 and Broj=" + prijemnica + " order by Artikal asc";
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);

            dataGridView2.DataSource = ds2.Tables[0];
            dataGridView2.Columns[0].Width = 70;
            dataGridView2.Columns[1].Width = 70;
            dataGridView2.Columns[2].Width = 70;
            dataGridView2.Columns[3].Width = 200;
            dataGridView2.Columns[4].Width = 70;
            dataGridView2.Columns[5].Width = 60;
            dataGridView2.Columns[6].Width = 80;

            dataGridView2.BorderStyle = BorderStyle.Fixed3D;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            dataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
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

            var query2 = "select NPrStPre from NPre Where NPrStatus='OD' order by NPrStPre desc";
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            cboPrijemnica.DataSource = ds2.Tables[0];
            cboPrijemnica.DisplayMember = "NprStPre";
            cboPrijemnica.ValueMember = "NprStPre";

            var query3 = "SELECT Name FROM sys.server_principals Where type='S' and name not like('%#%')";
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

        private void btnKorisnik_Click(object sender, EventArgs e)
        {
            FilterKorisnik(cboKorisnik.SelectedValue.ToString().TrimEnd());
        }

        private void btnPrijemnica_Click(object sender, EventArgs e)
        {
            FilterPrijemnica(Convert.ToInt32(cboPrijemnica.SelectedValue.ToString()));
        }

        private void btnSvi_Click(object sender, EventArgs e)
        {
            FillGV();
        }
        int rb = 1;
        class MSP
        {
            public int RB { get; set; }
            public string Korisnik { get; set; }
            public int Artikal { get; set; }
            public int MP { get; set; }
            public decimal Uzeto { get; set; }
            public int SifSkladIz { get; set; }
            public string LokacIz { get; set; }
            public decimal Ulozeno { get; set; }
            public int SifSkladU { get; set; }
            public string LokacU { get; set; }
            public int IDUzimanja { get; set; }
            public int IDUlaganja { get; set; }
        }
        List<MSP> mspList = new List<MSP>();
        private void button1_Click(object sender, EventArgs e)
        {
            List<int> selektovaniPrvi = new List<int>();
            List<int> selektovaniDrugi = new List<int>();
            foreach (DataGridViewRow row1 in dataGridView1.SelectedRows)
            {
                foreach (DataGridViewRow row2 in dataGridView2.SelectedRows)
                {
                    int artikal1 = Convert.ToInt32(row1.Cells[1].Value);
                    int mp = Convert.ToInt32(row1.Cells[2].Value);
                    decimal kolicina1 = Convert.ToDecimal(row1.Cells[4].Value);
                    int sifSklad = Convert.ToInt32(row1.Cells[5].Value);
                    string lokac = row1.Cells[6].Value.ToString();

                    int artikal2 = Convert.ToInt32(row2.Cells[1].Value);
                    decimal kolicina2 = Convert.ToDecimal(row2.Cells[4].Value);

                    if (!selektovaniPrvi.Contains(artikal1))
                    {
                        selektovaniPrvi.Add(artikal1);
                    }
                    if (!selektovaniDrugi.Contains(artikal2))
                    {
                        selektovaniDrugi.Add(artikal2);
                    }

                    selektovaniPrvi.Sort();
                    selektovaniDrugi.Sort();

                    if (!selektovaniPrvi.All(id => selektovaniDrugi.Contains(id)))
                    {
                        MessageBox.Show("Moraju se izabrati isti artikli za formiranje MSP-a!");
                        return;
                    }

                    int select1 = dataGridView1.SelectedRows.Count;
                    int select2 = dataGridView2.SelectedRows.Count;

                    if (select1 != select2)
                    {

                        decimal totalUzeto = dataGridView1.Rows.Cast<DataGridViewRow>()
                                .Where(r => Convert.ToInt32(r.Cells[1].Value) == artikal1 &&
                                            Convert.ToInt32(r.Cells[5].Value) == sifSklad &&
                                            Convert.ToString(r.Cells[6].Value) == lokac)
                                .Sum(r => Convert.ToDecimal(r.Cells[4].Value));

                        decimal totalUlozena = dataGridView2.Rows.Cast<DataGridViewRow>()
                        .Where(r => Convert.ToInt32(r.Cells[1].Value) == artikal1)
                        .Sum(r => Convert.ToDecimal(r.Cells[4].Value));

                        if (totalUzeto == totalUlozena)
                        {
                            decimal preostaloUzeto = totalUzeto;
                            foreach (DataGridViewRow row2ForArtikal in dataGridView2.Rows)
                            {
                                if (Convert.ToInt32(row2ForArtikal.Cells[1].Value) == artikal1)
                                {
                                    decimal ulozena = Convert.ToDecimal(row2ForArtikal.Cells[4].Value);

                                    if (preostaloUzeto >= ulozena)
                                    {
                                        bool alreadyAdded = mspList.Any(m => m.Artikal == artikal1 &&
                                                                  m.SifSkladIz == sifSklad &&
                                                                  m.LokacIz == lokac &&
                                                                  m.SifSkladU == Convert.ToInt32(row2ForArtikal.Cells[5].Value) &&
                                                                  m.LokacU == Convert.ToString(row2ForArtikal.Cells[6].Value));
                                        if (!alreadyAdded)
                                        {

                                            MSP msp = new MSP
                                            {
                                                RB = rb++,
                                                Korisnik = Convert.ToString(row1.Cells[0].Value),
                                                Artikal = artikal1,
                                                MP = mp,
                                                Uzeto = ulozena,
                                                SifSkladIz = sifSklad,
                                                LokacIz = lokac,
                                                Ulozeno = ulozena,
                                                SifSkladU = Convert.ToInt32(row2ForArtikal.Cells[5].Value),
                                                LokacU = Convert.ToString(row2ForArtikal.Cells[6].Value),
                                                IDUzimanja = Convert.ToInt32(row1.Cells[7].Value),
                                                IDUlaganja = Convert.ToInt32(row2ForArtikal.Cells[7].Value)
                                            };

                                            mspList.Add(msp);

                                            preostaloUzeto -= ulozena;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Uzeta kolicina nije ista ulozenoj!");
                            return;
                        }
                    }
                    else
                    {
                        if (artikal1 == artikal2)
                        {
                            MSP msp = new MSP
                            {
                                RB = rb++,
                                Korisnik = Convert.ToString(row1.Cells[0].Value),
                                Artikal = artikal1,
                                MP = mp,
                                Uzeto = Convert.ToDecimal(row1.Cells[4].Value),
                                SifSkladIz = Convert.ToInt32(row1.Cells[5].Value),
                                LokacIz = Convert.ToString(row1.Cells[6].Value),
                                Ulozeno = Convert.ToDecimal(row2.Cells[4].Value),
                                SifSkladU = Convert.ToInt32(row2.Cells[5].Value),
                                LokacU = Convert.ToString(row2.Cells[6].Value),
                                IDUzimanja = Convert.ToInt32(row1.Cells[7].Value),
                                IDUlaganja = Convert.ToInt32(row2.Cells[7].Value)
                            };
                            mspList.Add(msp);
                        }
                    }
                }
            }
            UpdateGV3();
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
        }
        private void ResetRB()
        {
            int broj = 1;
            foreach (var item in mspList)
            {
                item.RB = broj++;
            }
            rb = broj;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView3.SelectedRows)
            {
                MSP izbaci = (MSP)row.DataBoundItem;
                int selectedIDUzimanja = izbaci.IDUzimanja;
                mspList.RemoveAll(m => m.IDUzimanja == selectedIDUzimanja);
                ResetRB();
            }
            UpdateGV3();
        }
        private void UpdateGV3()
        {
            dataGridView3.DataSource = null;
            dataGridView3.DataSource = mspList;
            dataGridView3.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Statistika stat = new Statistika();
            stat.InsertPrometTrans(comboBox1.SelectedValue.ToString().TrimEnd(), DateTime.Now, Convert.ToInt32(comboBox2.SelectedValue),"MS");
            int PrStDok = stat.GetPrStDok();
            foreach (var i in mspList)
            {
                stat.InsertPrometTransPos(comboBox1.SelectedValue.ToString(), DateTime.Now, PrStDok, i.RB, i.SifSkladIz, i.LokacIz, i.MP, i.Uzeto, i.Ulozeno, i.SifSkladU, i.LokacU,"MS","MSP","303");
                stat.UpdateUzimanje(i.IDUzimanja, PrStDok);
                stat.UpdateUlaganje(i.IDUlaganja, PrStDok);
            }
            FillGV();
            mspList = null;
            UpdateGV3();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
