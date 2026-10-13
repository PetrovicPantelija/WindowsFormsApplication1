using CsvHelper.Expressions;
using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using WindowsFormsApplication1.Pakovanja.PlanPakovanja;

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    public partial class StavkePlanaPakovanjaNovi : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        string Korisnik = frmLogovanje.user.ToString().TrimEnd();
        int IdPakovanja;
        public StavkePlanaPakovanjaNovi()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
        }
        public StavkePlanaPakovanjaNovi(int ID)
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            IdPakovanja= ID;
        }
        private void StavkePlanaPakovanjaNovi_Load(object sender, EventArgs e)
        {
            FillGV();
            FillCombo();
            groupBox2.Visible = false;
        }
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);
            var query = "select ID,Naziv from PlanArhiv order by ID desc";
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboPlan.DataSource = ds.Tables[0];
            cboPlan.DisplayMember = "Naziv";
            cboPlan.ValueMember = "ID";

            var vrste = "Select Cast((VDnSifra) as int) as VDnSifra,Cast(RTrim(VDnSifra+'-'+VdNOpis) as nvarchar(100)) as Vrsta From VrsteDN order by VDnSifra asc";
            var da2 = new SqlDataAdapter(vrste, conn);
            var ds2= new DataSet();
            da2.Fill(ds2);
            cboVrstaRN.DataSource = ds2.Tables[0];
            cboVrstaRN.DisplayMember = "Vrsta";
            cboVrstaRN.ValueMember = "VDnSifra";
        }
        private void cboPlan_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void FillGV()
        {
            var query = "SELECT ID,IDPlanPakovanja,RNBroj,StatusRN,VrstaRN,Artikal,Kod,Naziv,KolicinaZaProizvodnju,PlaniraniKrajnjiDatumZavrsetka,Termin,Planer,RasporedPakovanja as Raspored,RTrim(Varijanta) as Varijanta " +
                "FROM StavkePlanaPakovanja " +
                "WHERE IDPlanPakovanja=" + IdPakovanja+" order by RNBroj asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Visible= false;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[7].Width = 300;
            dataGridView1.Columns[9].HeaderText = "KrajnjiDatum";

            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[5].ReadOnly= true;
            dataGridView1.Columns[6].ReadOnly = true;
            dataGridView1.Columns[7].ReadOnly = true;
            dataGridView1.Columns[9].ReadOnly = true;
            dataGridView1.Columns[10].ReadOnly = true;
            dataGridView1.Columns[11].ReadOnly = true;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            pomPaint = 0;
            //var query = "select * from DelNalogProizvodni Where DNpSifVrDok='PDN' and DNpSifStat='LA' or DnPSifStat='PL' or DnpSifStat='OD' and (Datum Between DATEADD(m, -3, GETDATE()) and GETDATE()) and IdPlana is not null and IDPlana<>" + Convert.ToInt32(cboPlanProizvodnje.SelectedValue) + "  order by DNpStZapisa asc";
            SqlConnection conn = new SqlConnection(connect);
            var query = "Select NameOper, Datum, DNpStZapisa, DNpStevilka, DNpStPos, DNpSifStat,DNpSifVrsteDN, DnpSifMp, DnpNaziv, DnpTeh, DnpSifSTor, DNpSifPlanerja, DNpKolZaIzdelavo, DNpIzdelanaKol, DNpIzlocenaKol, DNpPlanDatLans," +
                "DNpPlanDatZac, DNpPlanDatZak, DNpSifSklad, DNpLokacija,  DNpSifStroskMesta, DNpSifVrDokRef, DelNalogProizvodni.IDPlana " +
                "from DelNalogProizvodni " +
                "Where DNpSifVrDok='PDN' and (DNpSifStat='LA' or DNpSifStat='PL' or DNpSifStat='OD') and IDPlana=" + Convert.ToInt32(cboPlan.SelectedValue);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];
        }

        private void cboPlan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connect);
            var query = "select distinct Kolekcije.ID as ID,Kolekcije.Naziv as Naziv From PlanArhivStavke inner join Kolekcije on PlanArhivStavke.IDKolekcije=Kolekcije.ID Where IDPlana=" + Convert.ToInt32(cboPlan.SelectedValue);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboKolekcija.DataSource = ds.Tables[0];
            cboKolekcija.DisplayMember = "Naziv";
            cboKolekcija.ValueMember = "ID";
        }

        private void btnPretrazi_Click(object sender, EventArgs e)
        {
            pomPaint = 1;
            SqlConnection conn = new SqlConnection(connect);
            var query = "Select distinct NameOper,Datum,DNpStZapisa,DNpStevilka,DNpStPos,DNpSifStat,DNpSifVrsteDN,DnpSifMp,DnpNaziv,DnpTeh,DnpSifSTor,DNpSifPlanerja,DNpKolZaIzdelavo,DNpIzdelanaKol,DNpIzlocenaKol,DNpPlanDatLans,DNpPlanDatZac," +
                "DNpPlanDatZak,DNpSifSklad,DNpLokacija,DNpSifStroskMesta,DNpSifVrDokRef,DelNalogProizvodni.IDPlana " +
                "from DelNalogProizvodni " +
                "inner join PlanArhivStavke on DelNalogProizvodni.DNpSifMp=PlanArhivStavke.MpSifra " +
                "Where DNpSifVrDok='PDN' and (DNpSifStat='LA' or DNpSifStat='PL' or DNpSifStat='OD') and DelNalogProizvodni.IDPlana="+Convert.ToInt32(cboPlan.SelectedValue)+" and IDKolekcije="+Convert.ToInt32(cboKolekcija.SelectedValue)+" " +
                "Union " +
                "Select distinct NameOper,Datum,DNpStZapisa,DNpStevilka,DNpStPos,DNpSifStat,DNpSifVrsteDN,DnpSifMp,DnpNaziv,DnpTeh,DnpSifSTor,DNpSifPlanerja,DNpKolZaIzdelavo,DNpIzdelanaKol,DNpIzlocenaKol,DNpPlanDatLans,DNpPlanDatZac," +
                "DNpPlanDatZak,DNpSifSklad,DNpLokacija,DNpSifStroskMesta,DNpSifVrDokRef,DelNalogProizvodni.IDPlana " +
                "from DelNalogProizvodni " +
                "inner join PlanArhivStavke on DelNalogProizvodni.DNpSifMp=PlanArhivStavke.MpSifra " +
                "Where DNpSifVrDok='PDN' and (DNpSifStat='LA' or DNpSifStat='PL' or DNpSifStat='OD') and DelNalogProizvodni.IDPlana<"+Convert.ToInt32(cboPlan.SelectedValue)+" and IDKolekcije="+Convert.ToInt32(cboKolekcija.SelectedValue);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            //dataGridView2.AllowUserToAddRows = false;
            //dataGridView2.RowHeadersVisible = false;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
        }
        int pomPaint;
        private void dataGridView2_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (pomPaint == 1)
            {
                int pomID = Convert.ToInt32(cboPlan.SelectedValue);
                string pom = pomID.ToString();
                if (dataGridView2["IDPlana", e.RowIndex].Value.ToString() != pom)
                {
                    dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Tomato;
                }
            }
            else
            {
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White;
            }
        }

        private void btnNazad_Click(object sender, EventArgs e)
        {
            NoviPlanPakovanja frm = new NoviPlanPakovanja();
            frm.Show();
            this.Hide();
        }

        private void btnMP_Click(object sender, EventArgs e)
        {
            var query = "SELECT * FROM StavkePlanaPakovanja WHERE Artikal="+Convert.ToInt32(txtMP.Text)+" IDPlanPakovanja=" + IdPakovanja + " order by PlaniraniKrajnjiDatumZavrsetka asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[7].Width = 300;
            dataGridView1.Columns[9].HeaderText = "KrajnjiDatum";
           
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillGV();
        }
        
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            
        }
        int RNStavka;
        public class Stavke
        {
            public int ID { get; set; }
            public int RN { get; set; }
            public string Status {  get; set; }
            public int Vrsta { get; set; }
            public  int Artikal {  get; set; }
            public string Kod {get; set; }
            public string Naziv { get; set; }
            public decimal KolicinaZaProizvodnju {  get; set; }
            public DateTime KrajnjiDatum { get; set; }
            public int Termin {  get; set; }
            public string Varijanta { get; set; }
        }
        
        private void btnDodeli_Click(object sender, EventArgs e)
        {
            
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        RNStavka = Convert.ToInt32(row.Cells[2].Value.ToString());
                        InsertPakovanja ins = new InsertPakovanja();
                        ins.DodeliPlanuPakovanja(IdPakovanja, RNStavka, 0,Korisnik);
                    }
                }
            }
            catch { }

            FillGV();

            List<Stavke> stavke = new List<Stavke>();
            foreach(DataGridViewRow row in dataGridView1.Rows)
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
                    KolicinaZaProizvodnju = Convert.ToDecimal(row.Cells[8].Value.ToString()),
                    KrajnjiDatum = Convert.ToDateTime(row.Cells[9].Value.ToString()),
                    Termin = Convert.ToInt32(row.Cells[10].Value.ToString()),
                    Varijanta = row.Cells[13].Value.ToString()
                }) ;
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

            foreach(var item in stavke)
            {
                if (list.Contains(Convert.ToInt32(item.ID)) == false)
                {
                    if (item.Varijanta == "")
                    {
                        item.Varijanta = "*";
                    }
                    InsertPakovanja ins = new InsertPakovanja();
                    ins.InsertPregledStavkiPakovanja(item.ID, item.KolicinaZaProizvodnju, item.Artikal,item.Varijanta, IdPakovanja);
                }
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
        }
        string kod;
        private void btnEdit_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected) 
                {
                    txtID.Text = row.Cells["ID"].Value.ToString();
                    txtPlanPakovanja.Text = IdPakovanja.ToString();
                    txtRN.Text = row.Cells["RNBroj"].Value.ToString();
                    txtStatusRN.Text = row.Cells["StatusRN"].Value.ToString();
                    txtVrstaRN.Text = row.Cells["VrstaRN"].Value.ToString();
                    txtArtikal.Text = row.Cells["Artikal"].Value.ToString();
                    kod= row.Cells["Kod"].Value.ToString();
                    txtNaziv.Text = row.Cells["Naziv"].Value.ToString();
                    txtTermin.Text = row.Cells["Termin"].Value.ToString();
                    txtKolicina.Text = row.Cells["KolicinaZaProizvodnju"].Value.ToString();
                    DateTime pom = Convert.ToDateTime(row.Cells["PlaniraniKrajnjiDatumZavrsetka"].Value.ToString());

                    dateTimePicker3.Value = Convert.ToDateTime(pom.ToShortDateString());                
                }
            }
        }

        private void btnIzmeni_Click(object sender, EventArgs e)
        {
            InsertPakovanja ins = new InsertPakovanja();
            ins.UpdateStavkuPlanaPakovanja(Convert.ToInt32(txtID.Text), IdPakovanja, Convert.ToInt32(txtRN.Text), txtStatusRN.Text.ToString().TrimEnd(), Convert.ToInt32(txtVrstaRN.Text), Convert.ToInt32(txtArtikal.Text),
                kod, txtNaziv.Text, Convert.ToDecimal(txtKolicina.Text), Convert.ToDateTime(dateTimePicker3.Value), Convert.ToInt32(txtTermin.Text));
            FillGV();
            groupBox2.Visible = false;
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Da li ste sigurni da želite da obrišete stavku iz plana?",
                "Brisanje stavke pakovanja!", MessageBoxButtons.YesNo);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    
                    if (confirmResult == DialogResult.Yes)
                    {
                        InsertPakovanja ins = new InsertPakovanja();
                        ins.DeletePregledStavkiItem(Convert.ToInt32(row.Cells[0].Value.ToString()));
                        ins.DeleteStavkuPlanaPakovanja(Convert.ToInt32(row.Cells[0].Value.ToString()));
                    }
                    else
                    {
                        return;
                    }
                }
            }
            FillGV();
        }

        private void btnPretrazipoDatumu_Click(object sender, EventArgs e)
        {
            pomPaint = 0;
            DateTime datOd = dateTimePicker1.Value;
            DateTime datDo= dateTimePicker2.Value;

            SqlConnection conn = new SqlConnection(connect);
            string query = "Select NameOper, Datum, DNpStZapisa, DNpStevilka, DNpStPos,DNpSifStat, DNpSifVrsteDN, DnpSifMp, DnpNaziv, DnpTeh, DnpSifSTor, DNpSifPlanerja, DNpKolZaIzdelavo, DNpIzdelanaKol, DNpIzlocenaKol, DNpPlanDatLans," +
                "DNpPlanDatZac, DNpPlanDatZak, DNpSifSklad, DNpLokacija, DNpSifStroskMesta, DNpSifVrDokRef, DelNalogProizvodni.IDPlana " +
                "from DelNalogProizvodni " +
                "Where DNpSifVrDok='PDN' and (DNpSifStat='LA' or DNpSifStat='PL' or DNpSifStat='OD') and Datum between '" + datOd.ToString("yyyy-MM-dd") + "' and '" + datDo.ToString("yyyy-MM-dd") + "' and DnpSifVrsteDN="+Convert.ToInt32(cboVrstaRN.SelectedValue);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];
        }

        private void btnPretraziRN_Click(object sender, EventArgs e)
        {
            pomPaint = 0;
            SqlConnection conn = new SqlConnection(connect);
            var query = "Select NameOper, Datum, DNpStZapisa, DNpStevilka, DNpStPos,DNpSifStat, DNpSifVrsteDN, DnpSifMp, DnpNaziv, DnpTeh, DnpSifSTor, DNpSifPlanerja, DNpKolZaIzdelavo, DNpIzdelanaKol, DNpIzlocenaKol, DNpPlanDatLans," +
                "DNpPlanDatZac, DNpPlanDatZak, DNpSifSklad, DNpLokacija,  DNpSifStroskMesta, DNpSifVrDokRef, DelNalogProizvodni.IDPlana " +
                "from DelNalogProizvodni " +
                "Where DNpSifVrDok='PDN' and (DNpSifStat='LA' or DNpSifStat='PL' or DNpSifStat='OD') and DnpStevilka between " + Convert.ToInt32(txtRNod.Text) + " and " + Convert.ToInt32(txtRnDo.Text);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];
        }

        private void btnRealizacija_Click(object sender, EventArgs e)
        {
            try
            {
                InsertPakovanja ins = new InsertPakovanja();
                int RB = 1;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    int id = Convert.ToInt32(row.Cells["ID"].Value);
                    ins.UpisiRaspored(id, RB);
                    RB++;
                }
                RealizacijaPlanaPakovanja frm = new RealizacijaPlanaPakovanja(IdPakovanja);
                frm.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmPlanPakovanja frm = new frmPlanPakovanja();
            frm.Show();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void btnKolicine_Click(object sender, EventArgs e)
        {
            InsertPakovanja ins = new InsertPakovanja();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    int id = Convert.ToInt32(row.Cells[0].Value.ToString());
                    int artikal = Convert.ToInt32(row.Cells[5].Value.ToString());
                    decimal kolicina = Convert.ToDecimal(row.Cells[8].Value.ToString());
                    int rn= Convert.ToInt32(row.Cells[2].Value.ToString());

                    var query = "Select DNpKolZaIzdelavo From DelNalogProizvodni Where DNpStevilka=" + rn;
                    SqlConnection conn = new SqlConnection(connect);
                    conn.Open();
                    SqlCommand cmd=new SqlCommand(query, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while(dr.Read())
                    {
                        if (kolicina <= Convert.ToDecimal(dr[0].ToString()))
                        {
                            decimal rnKolicina = Convert.ToDecimal(dr[0].ToString()) - kolicina;
                            ins.UpdateKolicinePlanaPakovanja(id, kolicina, artikal, rn,rnKolicina);
                        }
                        else
                        {
                            MessageBox.Show("Količina mora biti manja od količine na radnom nalogu!");
                            return;
                        }
                    }
                    conn.Close();
                }
            }
            FillGV();
        }

        private void btnIzvestaj_Click(object sender, EventArgs e)
        {
            RedovnoPakovanjeIzvestaj frm = new RedovnoPakovanjeIzvestaj(IdPakovanja);
            frm.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
