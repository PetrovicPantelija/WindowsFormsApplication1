using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    public partial class RealizacijaPlanaPakovanja : Form
    {
        string Korisnik = frmLogovanje.user.ToString().Trim();
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        int planPakovanja;
        public RealizacijaPlanaPakovanja()
        {
            InitializeComponent();
        }
        public RealizacijaPlanaPakovanja(int IDPlanapakovanja)
        {
            planPakovanja = IDPlanapakovanja;
            InitializeComponent();
        }
        private void FillCombo()
        {
            var select = "select Distinct SkSifra,SkNaziv " +
                "From Sklad " +
                "inner join MaticniPodatki on Sklad.SkSifra=MaticniPodatki.MpOsnSklad " +
                "inner join PregledStavkiPlanaPakovanja on MaticniPodatki.MpSifra=PregledStavkiPlanaPakovanja.ArtikalIzPrvogNivoaSastavnice " +
                "inner join StavkePlanaPakovanja on PregledStavkiPlanaPakovanja.ArtikalPaket=StavkePlanaPakovanja.Artikal " +
                "Where IDPlanPakovanja="+planPakovanja+" order by SkSifra asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboSkladiste.DataSource = ds.Tables[0];
            cboSkladiste.DisplayMember = "SkNaziv";
            cboSkladiste.ValueMember = "SkSifra";
        }
        private void FillCkList()
        {
            var query = "select distinct SkSifra,SkNaziv " +
                "from sklad " +
                "inner join MaticniPodatki on sklad.SkSifra=MaticniPodatki.MpOsnSklad " +
                "inner join PregledStavkiPlanaPakovanja on MaticniPodatki.MpSifra=PregledStavkiPlanaPakovanja.ArtikalIzPrvogNivoaSastavnice " +
                "inner join StavkePlanaPakovanja on PregledStavkiPlanaPakovanja.IDStavkePlanaPakovanja=StavkePlanaPakovanja.ID " +
                "Where IDPlanPakovanja=" + planPakovanja;
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            checkedListBox1.DataSource = ds.Tables[0];
            checkedListBox1.DisplayMember = "SkNaziv";
            checkedListBox1.ValueMember = "SkSifra";

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        string pomMagacini;
        private void GetChecked()
        {
            List<int> magacini = new List<int>();
            foreach (var item in checkedListBox1.CheckedItems)
            {
                DataRowView rw = (DataRowView)item;
                int skSifra = Convert.ToInt32(rw.Row["SkSifra"]);
                magacini.Add(skSifra);
            }
            pomMagacini = "";
            pomMagacini = string.Join(",", magacini);
        }
        private void FillGV()
        {
            var query = "select s.ID,IDPlanPakovanja,RnBroj,StatusRn,VrstaRn,Artikal,Kod,Naziv,s.KolicinaZaProizvodnju,PlaniraniKrajnjiDatumZavrsetka,Termin," +
                "(Count(ArtikalIzPrvogNivoaSastavnice)) as [Delova],RasporedPakovanja,RTrim(DNpOznSled) as Varijanta " +
                "from StavkePlanaPakovanja s " +
                "inner join PregledStavkiPlanaPakovanja on s.id=PregledStavkiPlanaPakovanja.IDStavkePlanaPakovanja " +
                "inner join DelNalogProizvodni on s.RNBroj=DelNalogProizvodni.DNpStevilka " +
                "Where IDPlanPakovanja=" + planPakovanja + " and RNBroj not in (SELECT RN FROM KompletiraniPlanovi) " +
                "Group by s.id,IDPlanPakovanja,RnBroj,StatusRn,VrstaRn,Artikal,Kod,Naziv,s.KolicinaZaProizvodnju,PlaniraniKrajnjiDatumZavrsetka,Termin,RasporedPakovanja,DNpOznSled order by RasporedPakovanja asc";
            
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns["RnBroj"].Width = 70;
            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[7].Width = 280;
            dataGridView1.Columns[9].HeaderText = "KrajnjiDatum";
            dataGridView1.Columns["Termin"].Width = 60;
            dataGridView1.Columns["Delova"].Width = 60;

            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            dataGridView1.MultiSelect = false;

            dataGridView1.AutoResizeColumnHeadersHeight();

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                dataGridView1.AutoResizeColumn(column.Index, DataGridViewAutoSizeColumnMode.DisplayedCells);
            }

            GetChecked();

            var query2 = "Select IDStavkePlanaPakovanja,RN,StatusStavke,ArtikalIzPrvogNivoaSastavnice,RTrim(MpStaraSif),(PotrebnaKolicina-RezervisanaKolicinaPoPlanu) as PotrebnaKolicina,TrenutnaKolicinaNaZalihama,PregledStavkiPlanaPakovanja.ID as ID,(PotrebnaKolicina-RezervisanaKolicinaPoPlanu-TrenutnaKolicinaNaZalihama) as Potrebno  " +
                "From PregledStavkiPlanaPakovanja " +
                "Inner join StavkePlanaPakovanja on PregledStavkiPlanaPakovanja.IDStavkePlanaPakovanja=StavkePlanaPakovanja.ID " +
                "inner join MaticniPodatki on PregledStavkiPlanaPakovanja.ArtikalIzPrvogNivoaSastavnice=MaticniPodatki.MpSifra " +
                "Where (TrenutnaKolicinaNaZalihama<PotrebnaKolicina and PotrebnaKolicina>RezervisanaKolicinaPoPlanu) and MaticniPodatki.MpOsnSklad in ("+pomMagacini+") And IDPlanPakovanja=" + planPakovanja+ " order by StavkePlanaPakovanja.RasporedPakovanja asc,IDStavkePlanaPakovanja asc";
            var da2=new SqlDataAdapter(query2, conn);
            var ds2=new DataSet();
            da2.Fill(ds2);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds2.Tables[0];

            dataGridView3.BorderStyle = BorderStyle.Fixed3D;
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView3.BackgroundColor = Color.FromArgb(125, 158, 192);
            dataGridView3.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

            dataGridView3.EnableHeadersVisualStyles = false;
            dataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.Tomato;
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 60;
            dataGridView3.Columns[1].Width = 60;
            dataGridView3.Columns[2].Width = 60;
            dataGridView3.Columns[2].HeaderText = "Status";
            dataGridView3.Columns[3].HeaderText = "Artikal iz prvog nivoa sastavnice";
            dataGridView3.Columns[3].Width = 100;
            dataGridView3.Columns[4].HeaderText = "Kod";
            dataGridView3.Columns[4].Width = 130;
            dataGridView3.Columns[5].HeaderText = "Potrebna kolicina po planu";
            dataGridView3.Columns[5].Width = 80;
            dataGridView3.Columns[6].HeaderText = "Stanje";
            dataGridView3.Columns[6].Width = 80;
            dataGridView3.Columns["ID"].Visible = false;
            dataGridView3.Columns["Potrebno"].Width = 80;
            
            List<int> stavke = new List<int>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.LightGreen;
                stavke.Add(Convert.ToInt32(row.Cells[0].Value));
            }
            List<int> proizvodnja = new List<int>();
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                proizvodnja.Add(Convert.ToInt32(row.Cells[0].Value.ToString()));
            }
            
            int[] stavkeArray = stavke.ToArray();
            int[] proizvodnjaArray = proizvodnja.ToArray();
            for (int i = 0; i < proizvodnjaArray.Length; i++)
            {
                int proizvodnjaItem = proizvodnjaArray[i];

                if (stavkeArray.Contains(proizvodnjaItem))
                {
                    int rowIndex = Array.IndexOf(stavkeArray, proizvodnjaItem);
                    dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
        }

        private void RealizacijaPlanaPakovanja_Load(object sender, EventArgs e)
        {
            FillCombo();
            FillCkList();
            FillGV();
            groupBox1.Visible = false;
            
        }
        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            
        }
        int IDStavke;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    IDStavke = Convert.ToInt32(row.Cells[0].Value.ToString());
                }
            }
            Stavke();
        }
        private void Stavke()
        {
            var query = "Select ID,IdStavkePlanaPakovanja,RN,StatusStavke,ArtikalPaket,MaticniPodatki.MpStaraSif,KolicinaZaProizvodnju,ArtikalIzPrvogNivoaSastavnice,m1.MpStaraSif,PotrebnaKolicina," +
                   "ISNull(TrenutnaKolicinaNaZalihama,0),RezervisanaKolicinaPoPlanu,UkucatiKolicinuZaRezervaciju,(PotrebnaKolicina-RezervisanaKolicinaPoPlanu- ISNull(TrenutnaKolicinaNaZalihama,0)) as Nedostaje " +
                   "from PregledStavkiPlanaPakovanja " +
                   "inner join MaticniPodatki on PregledStavkiPlanaPakovanja.ArtikalPaket=MaticniPodatki.MpSifra " +
                   "inner join MaticniPodatki as m1 on PregledStavkiPlanaPakovanja.ArtikalIzPrvogNivoaSastavnice=m1.MpSifra " +
                   "WHERE IDStavkePlanaPakovanja=" + IDStavke + " and IdPlana="+planPakovanja;
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            //dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.BorderStyle = BorderStyle.Fixed3D;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.FromArgb(125, 158, 192);
            dataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkOrange;
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[2].HeaderText = "Sastavnica";
            dataGridView2.Columns[2].Width = 60;
            dataGridView2.Columns[3].HeaderText = "Status";
            dataGridView2.Columns[3].Width = 40;
            dataGridView2.Columns[4].Width = 70;
            dataGridView2.Columns[5].HeaderText = "Kod";
            dataGridView2.Columns[5].Width = 150;
            dataGridView2.Columns[6].HeaderText = "Za proizvodnju";
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[7].HeaderText = "Artikal iz\n prvog nivoa";
            dataGridView2.Columns[8].HeaderText = "Kod";
            dataGridView2.Columns[8].Width = 220;
            dataGridView2.Columns[10].HeaderText = "Trenutna količina";
            dataGridView2.Columns[11].HeaderText = "Rezervisana\n količina";
            dataGridView2.Columns[12].HeaderText = "Kolčina za\n rezervaciju";
            dataGridView2.Columns[12].Width = 80;

            dataGridView2.Columns[0].ReadOnly = true;
            dataGridView2.Columns[1].ReadOnly = true;
            dataGridView2.Columns[2].ReadOnly = true;
            dataGridView2.Columns[3].ReadOnly = true;
            dataGridView2.Columns[4].ReadOnly = true;
            dataGridView2.Columns[5].ReadOnly = true;
            dataGridView2.Columns[6].ReadOnly = true;
            dataGridView2.Columns[7].ReadOnly = true;
            dataGridView2.Columns[8].ReadOnly = true;
            dataGridView2.Columns[9].ReadOnly = true;
            dataGridView2.Columns[10].ReadOnly = true;
            dataGridView2.Columns[11].ReadOnly = true;

            List<string> rezervacije = new List<string>();
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                int id = Convert.ToInt32(row.Cells["ID"].Value);
                int idStavke = Convert.ToInt32(row.Cells["IDStavkePlanaPakovanja"].Value);
                int mp = Convert.ToInt32(row.Cells["ArtikalIzPrvogNivoaSastavnice"].Value);

                string rez = id.ToString()+"-"+idStavke.ToString() + "-" + mp.ToString();
                rezervacije.Add(rez);
            }
            List<string> proizvodnja = new List<string>();
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                int id = Convert.ToInt32(row.Cells["ID"].Value);
                int idStavke = Convert.ToInt32(row.Cells["IDStavkePlanaPakovanja"].Value);
                int mp = Convert.ToInt32(row.Cells["ArtikalIzPrvogNivoaSastavnice"].Value);

                string proiz = id.ToString()+"-"+ idStavke.ToString() + "-" + mp.ToString();
                proizvodnja.Add(proiz);
            }

            string[] rNiz = rezervacije.ToArray();
            string[] pNiz = proizvodnja.ToArray();

            for (int i = 0; i < rNiz.Length; i++)
            {
                string rezervacijaItem = rNiz[i];

                if (pNiz.Contains(rezervacijaItem)==false)
                { 
                    string pom = rezervacijaItem;
                    string[] id = pom.Split('-');

                    if (int.TryParse(id[0], out int result))
                    {
                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {
                            if (Convert.ToInt32(row.Cells[0].Value) == result)
                            {
                                row.DefaultCellStyle.BackColor = Color.LightGreen;
                            }
                        }
                    }
                }
            }

            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                dataGridView2.AutoResizeColumn(column.Index, DataGridViewAutoSizeColumnMode.DisplayedCells);
            }
        }
        int ID,MP;
        decimal KolZaRezervaciju;
        decimal Stanje;

        private void btnSvi_Click(object sender, EventArgs e)
        {
            FillGV();
        }

        private void btnPretrazi_Click(object sender, EventArgs e)
        {
            var query2 = "Select IDStavkePlanaPakovanja,RN,StatusStavke,ArtikalIzPrvogNivoaSastavnice,RTrim(MpStaraSif),(PotrebnaKolicina-RezervisanaKolicinaPoPlanu) as PotrebnaKolicina,TrenutnaKolicinaNaZalihama,PregledStavkiPlanaPakovanja.ID as ID " +
                "From PregledStavkiPlanaPakovanja " +
                "Inner join StavkePlanaPakovanja on PregledStavkiPlanaPakovanja.IDStavkePlanaPakovanja=StavkePlanaPakovanja.ID " +
                "inner join MaticniPodatki on PregledStavkiPlanaPakovanja.ArtikalIzPrvogNivoaSastavnice=MaticniPodatki.MpSifra " +
                "Where (TrenutnaKolicinaNaZalihama<PotrebnaKolicina and PotrebnaKolicina>RezervisanaKolicinaPoPlanu) And IDPlanPakovanja=" + planPakovanja + " and MpOsnSklad="+Convert.ToInt32(cboSkladiste.SelectedValue)+" order by PregledStavkiPlanaPakovanja.ID asc";
            SqlConnection conn = new SqlConnection(connect);
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds2.Tables[0];

            dataGridView3.BorderStyle = BorderStyle.Fixed3D;
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView3.BackgroundColor = Color.FromArgb(125, 158, 192);
            dataGridView3.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

            dataGridView3.EnableHeadersVisualStyles = false;
            dataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.Tomato;
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 60;
            dataGridView3.Columns[1].Width = 60;
            dataGridView3.Columns[2].Width = 60;
            dataGridView3.Columns[2].HeaderText = "Status";
            dataGridView3.Columns[3].HeaderText = "Artikal iz prvog nivoa sastavnice";
            dataGridView3.Columns[3].Width = 100;
            dataGridView3.Columns[4].HeaderText = "Kod";
            dataGridView3.Columns[4].Width = 130;
            dataGridView3.Columns[5].HeaderText = "Potrebna kolicina";
            dataGridView3.Columns[5].Width = 80;
            dataGridView3.Columns[6].HeaderText = "Stanje";
            dataGridView3.Columns[6].Width = 80;
            dataGridView3.Columns["ID"].Visible = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string obavestenje = "Učitavanje novih stanja!";
            Obavestenje frm = new Obavestenje(obavestenje);
            frm.Show();
            Application.DoEvents(); 

            var query = "select PregledSTavkiPlanaPakovanja.ID,IDStavkePlanaPakovanja,ArtikalIzPrvogNivoaSastavnice " +
                "from PregledStavkiPlanaPakovanja " +
                "inner join StavkePlanaPakovanja on PregledStavkiPlanaPakovanja.IDStavkePlanaPakovanja=StavkePlanaPakovanja.ID " +
                "Where IDPlanPakovanja=" + planPakovanja;
            SqlConnection conn = new SqlConnection(connect);

            conn.Open();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(query, conn);
            dt.Load(cmd.ExecuteReader());
            conn.Close();

            frm.SetProgressBarMaximum(dt.Rows.Count); 

            foreach (DataRow row in dt.Rows)
            {
                int id = Convert.ToInt32(row[0]);
                int idS = Convert.ToInt32(row[1]);
                int artikal = Convert.ToInt32(row[2]);
                

                InsertPakovanja ins = new InsertPakovanja();
                ins.UpdateStanjePregledStavkiPakovanja(id, idS, artikal);

                frm.IncrementProgressBar();
            }

            FillGV();
            frm.Close();
        }
        private void btn_Rezervisi_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected)
                {
                    ID = Convert.ToInt32(row.Cells["ID"].Value.ToString());
                    MP = Convert.ToInt32(row.Cells[7].Value.ToString());
                    Stanje = Convert.ToDecimal(row.Cells[10].Value.ToString());
                    KolZaRezervaciju = Convert.ToDecimal(row.Cells[12].Value.ToString());

                    InsertPakovanja ins = new InsertPakovanja();
                    ins.UpdatePregledStavkiPakovanja(ID, MP, Stanje, KolZaRezervaciju);
                }
            }
            FillGV();
            
        }

        private void btnNalogAmbalaza_Click(object sender, EventArgs e)
        {
            string sklad = "5";
            NaloziZaIzradu frm = new NaloziZaIzradu(planPakovanja, sklad,"ambalaže");
            frm.Show();
        }

        private void btnNalogPanel_Click(object sender, EventArgs e)
        {
            string sklad = "6,7,9";
            NaloziPanel frm = new NaloziPanel(planPakovanja, sklad);
            frm.Show();
        }
        private void btnNalogStiropor_Click(object sender, EventArgs e)
        {
            string sklad = "5";
            NalogStiropor frm = new NalogStiropor(planPakovanja, sklad);
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NamenskoPakovanje frm = new NamenskoPakovanje();
            frm.Show();
        }
        private void btnRezervisiZbirno_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            var query2 = "select ID,RNBroj,Artikal,Kod,KolicinaZaProizvodnju,RasporedPakovanja from StavkePlanaPakovanja Where IDPlanPakovanja=" + planPakovanja+" order by RasporedPakovanja";
            SqlConnection conn = new SqlConnection(connect);
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView4.DataSource = ds2.Tables[0];
            dataGridView4.ForeColor = Color.Black;

            dataGridView4.BorderStyle = BorderStyle.Fixed3D;
            dataGridView4.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView4.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView4.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView4.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView4.BackgroundColor = Color.FromArgb(125, 158, 192);
            dataGridView4.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

            dataGridView4.EnableHeadersVisualStyles = false;
            dataGridView4.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            dataGridView4.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkOrange;
            dataGridView4.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView4.Columns[0].Width = 50;
            dataGridView4.Columns[1].Width = 60;
            dataGridView4.Columns[2].Width = 60;

        }
        int pom;
        private void btnPotvrda_Click(object sender, EventArgs e)
        {
            InsertPakovanja ins = new InsertPakovanja();
            foreach (DataGridViewRow row in dataGridView4.Rows)
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
            string obavestenje = "Rezervacija stavki plana";
            Obavestenje frm = new Obavestenje(obavestenje);
            frm.Show();
            Application.DoEvents();

            var query = "Select PregledStavkiPlanaPakovanja.ID,StavkePlanaPakovanja.ID,PregledStavkiPlanaPakovanja.ArtikalIzPrvogNivoaSastavnice " +
                "From PregledStavkiPlanaPakovanja " +
                "inner join StavkePlanaPakovanja on PregledStavkiPlanaPakovanja.IDStavkePlanaPakovanja=StavkePlanaPakovanja.ID " +
                "Where IDPlanPakovanja=" + planPakovanja + " order by RasporedPakovanja asc";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(query, conn);
            dt.Load(cmd.ExecuteReader());
            conn.Close();
            frm.SetProgressBarMaximum(dt.Rows.Count);
            foreach (DataRow row in dt.Rows)
            {
                int id = Convert.ToInt32(row[0]);
                int idS = Convert.ToInt32(row[1]);
                int artikal = Convert.ToInt32(row[2]);
                ins.ResetRezervacija(id);
                ins.RezervacijaPoRasporedu(id, idS, artikal);
                frm.IncrementProgressBar();
            }
           /* foreach(DataRow row in dt.Rows)
            {
                int artikal = Convert.ToInt32(row[2]);
                ins.PoravnajStanja(artikal, planPakovanja);
            }  */  
            frm.Close();
            groupBox1.Visible = false;

            FillGV();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }

        private void listMagacin_Click(object sender, EventArgs e)
        {
            FillGV();
        }



        private void btnCSV_Click(object sender, EventArgs e)
        {
            string[] path = { Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "RedovnoPakovanje_Proizvodnja-" };
            string folder = Path.Combine(path);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                ShouldQuote = (field) => false,
                Delimiter = ";"
            };
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            var query = "Select IDStavkePlanaPakovanja,RN,StatusStavke,ArtikalIzPrvogNivoaSastavnice,RTrim(MpStaraSif) as KOD,(PotrebnaKolicina-RezervisanaKolicinaPoPlanu) as PotrebnaKolicina,TrenutnaKolicinaNaZalihama,PregledStavkiPlanaPakovanja.ID as ID " +
                            "From PregledStavkiPlanaPakovanja " +
                            "Inner join StavkePlanaPakovanja on PregledStavkiPlanaPakovanja.IDStavkePlanaPakovanja=StavkePlanaPakovanja.ID " +
                            "inner join MaticniPodatki on PregledStavkiPlanaPakovanja.ArtikalIzPrvogNivoaSastavnice=MaticniPodatki.MpSifra " +
                            "Where (TrenutnaKolicinaNaZalihama<PotrebnaKolicina and PotrebnaKolicina>RezervisanaKolicinaPoPlanu) and MaticniPodatki.MpOsnSklad in (" + pomMagacini + ") And IDPlanPakovanja=" + planPakovanja + " order by PregledStavkiPlanaPakovanja.ID asc";
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

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    InsertPakovanja ins = new InsertPakovanja();
                    ins.DeleteStavkePakovanja(Convert.ToInt32(row.Cells[0].Value.ToString()));
                }
            }
            FillGV();
        }

        private void txtArtikal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtArtikal.Text != "")
                {
                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {
                        if (Convert.ToInt32(row.Cells["Artikal"].Value.ToString()) == Convert.ToInt32(txtArtikal.Text.ToString()))
                        {
                            row.Selected = true;
                            dataGridView4.FirstDisplayedScrollingRowIndex = dataGridView4.SelectedRows[0].Index;
                        }
                    }
                }
                else
                {
                    dataGridView4.ClearSelection();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnPaneliSvi_Click(object sender, EventArgs e)
        {
            string sklad = "6,7,9";
            NalogPaneliSVI frm=new NalogPaneliSVI(planPakovanja, sklad);
            frm.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            InsertPakovanja ins = new InsertPakovanja();

            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    ins.InsertKompletiraniPlanovi(planPakovanja, Convert.ToInt32(row.Cells[2].Value.ToString()), Convert.ToInt32(row.Cells[0].Value.ToString()),0);
                }
            }
            FillGV();
        }

        private void btnKompletirano_Click(object sender, EventArgs e)
        {
            NalogKomplet frm = new NalogKomplet(planPakovanja);
            frm.Show();
        }

        private void btnNalogOkov_Click(object sender, EventArgs e)
        {
            string sklad = "4,11";
            NaloziZaIzradu frm = new NaloziZaIzradu(planPakovanja, sklad,"okova");
            frm.Show();
        }
    }
}
