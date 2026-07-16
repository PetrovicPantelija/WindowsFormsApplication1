using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
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
using static WindowsFormsApplication1.Planovi_Excel.PripadnostKP;

namespace WindowsFormsApplication1.Neto_potrebe
{
    public partial class NetoPotrebe : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        //komentar1
        public NetoPotrebe()
        {
            InitializeComponent();
        }

        private void NetoPotrebe_Load(object sender, EventArgs e)
        {
            FillGV();
            FillGrupe();
            FillMAterijal1();
            FillMAterijal2();
            FillDebljina();
            FillTehnologija1();
            FillTehnologija2();
            FillPlanovi();
            
        }
        private void FillGV()
        {

            string wheredodatak = "";

            if (chkRazvrstani.Checked == true)
            {
                wheredodatak += " and NetoPotrebeIgor.PodPlan <> '" + "" + "'";
            }
        


                var query = @"select NetoPotrebeIgor.ID,NetoPotrebeIgor.Datum as Datum,NetoPotrebeIgor.IDNadredjena,NetoPotrebeIgor.IDZapisa,Mp,MpStaraSif,Sklad,skNaziv as Skladiste,Lokac,LokOpis,Kolicina,
MdfGrupe.Naziv as MDFGrupa,  MDFPodgrupe.Naziv as Podgrupa, MaticniPodatkiIgor.Materijal1,MaticniPodatkiIgor.Materijal2,MaticniPodatkiIgor.Debljina,MaticniPodatkiIgor.Tehnologija1,MaticniPodatkiIgor.Tehnologija2, Podplan,
MaticniPodatkiIgor.MdfGrupa as GrupaID,MaticniPodatkiIgor.MdfPodgrupa as PodgrupaID,  NetoPotrebeIGor.RB
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
left join MaticniPodatkiIgor on MAticniPodatkiIgor.MPSifra=MaticniPodatki.MpSifra
left join MDFGrupe on MdfGrupe.ID = MaticniPodatkiIgor.MDFGrupa 
left join MdfPodgrupe on MAticniPodatkiIgor.MDFPodgrupa = MDFPodgrupe.ID
where LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + wheredodatak + " order by NetoPotrebeIgor.Rb, NetoPotrebeIgor.id asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns["GrupaID"].Visible = false;
            dataGridView1.Columns["PodgrupaID"].Visible = false;

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Datum";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Nadredjeni";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Zapis";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "MP";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Kod";
            dataGridView1.Columns[5].Width = 180;

        }

        int filterGrupa = 0;
        private void FillGVGrupa()
        {
           
            var query = @"select NetoPotrebeIgor.ID,NetoPotrebeIgor.Datum as Datum,NetoPotrebeIgor.IDNadredjena,
IDZapisa,Mp,MpStaraSif,Sklad,skNaziv,Lokac,LokOpis,Kolicina, MdfGrupe.Naziv as MDFGrupa,  
MDFPodgrupe.Naziv as Podgrupa, MaticniPodatkiIgor.Materijal1,MaticniPodatkiIgor.Materijal2,MaticniPodatkiIgor.Debljina,
MaticniPodatkiIgor.Tehnologija1,MaticniPodatkiIgor.Tehnologija2, Podplan,  NetoPotrebeIGor.RB
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
left join MaticniPodatkiIgor on MAticniPodatkiIgor.MPSifra=MaticniPodatki.MpSifra
left join mdfgrupe on mdfgrupe.ID = MaticniPodatkiIgor.mdfgrupa
left join mdfpodgrupe on mdfpodgrupe.ID = MaticniPodatkiIgor.mdfpodgrupa";

var where = " where Rtrim(NetoPotrebeIgor.Podplan) = '' and LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " and MaticniPodatkiIgor.MdfGrupa = " + Convert.ToInt32(cboGrupa.SelectedValue) + " order by RB, MpStaraSif asc";

            if (chkRazvrstani.Checked == true)
            {
                where = " where NetoPotrebeIgor.Podplan <> '' and LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " and MaticniPodatkiIgor.MdfGrupa = " + Convert.ToInt32(cboGrupa.SelectedValue) + " order by RB, MpStaraSif asc";
                query = query + where;
                //MessageBox.Show(query.ToString());
            }
            else
            {
                query = query + where;
              //  MessageBox.Show(query.ToString());

            }
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Datum";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Nadredjeni";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Zapis";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "MP";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Kod";
            dataGridView1.Columns[5].Width = 180;
        }

        private void FillGVOsobina()
        {
            string wheredodatak = "";

            if (chkMaterijal1.Checked == true)
            {
                wheredodatak += " and MaticniPodatkiIgor.Materijal1 = '" + cboMaterijal1.Text.ToString() + "'";
            }
            if (chkMaterijal2.Checked == true)
            {
                wheredodatak += " and MaticniPodatkiIgor.Materijal2 = '" + cboMaterijal2.Text.ToString() + "'";
            }
            if (chkDebljina.Checked == true)
            {
                wheredodatak += " and MaticniPodatkiIgor.Debljina = '" + cboDebljina.Text.ToString() + "'";
            }
            if (chkTehnologija1.Checked == true)
            {
                wheredodatak += " and MaticniPodatkiIgor.Tehnologija1 = '" + cboTehnologija1.Text.ToString() + "'";
            }
            if (chkTehnologija2.Checked == true)
            {
                wheredodatak += " and MaticniPodatkiIgor.Tehnologija2 = '" + cboTehnologija2.Text.ToString() + "'";
            }
            if (chkGrupa.Checked == true)
            {
                wheredodatak += " and MaticniPodatkiIgor.MdfGrupa = " + Convert.ToInt32(cboGrupa.SelectedValue);
            }
            if (chkRazvrstani.Checked == true)
            {
                wheredodatak += " and NetoPotrebeIgor.PodPlan <> '" + "" + "'";
            }
            else
            {
                wheredodatak += " and NetoPotrebeIgor.PodPlan = '" + "" + "'";
            }

                var query = "select NetoPotrebeIgor.ID,NetoPotrebeIgor.Datum as Datum,NetoPotrebeIgor.IDNadredjena,IDZapisa,Mp,MpStaraSif,Sklad,skNaziv,Lokac,LokOpis,Kolicina, " +
    " MdfGrupe.Naziv as MDFGrupa,  MDFPodgrupe.Naziv as Podgrupa, MAticniPodatkiIgor.Materijal1, " +
    " MAticniPodatkiIgor.Materijal2,MAticniPodatkiIgor.Debljina,MAticniPodatkiIgor.Tehnologija1,MAticniPodatkiIgor.Tehnologija2, PodPlan,  NetoPotrebeIGor.RB " +
    " From NetoPotrebeIgor " +
    " inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra " +
    " inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra " +
    " inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra  " +
    " left join MaticniPodatkiIgor on MAticniPodatkiIgor.MPSifra = MaticniPodatki.MpSifra " +
    " left join mdfgrupe on mdfgrupe.ID = MAticniPodatkiIgor.mdfgrupa " +
    " left join mdfpodgrupe on mdfpodgrupe.ID = MAticniPodatkiIgor.mdfpodgrupa " +
    "  where LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + wheredodatak + " order by RB, MpStaraSif asc";
           
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Datum";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Nadredjeni";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Zapis";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "MP";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Kod";
            dataGridView1.Columns[5].Width = 180;
        }

        private void FillGVGrupaPodgrupa()
        {
            var query = @"select ID,NetoPotrebeIgor.Datum as Datum,IDNadredjena,IDZapisa,Mp,MpStaraSif,Sklad,skNaziv,Lokac,LokOpis,Kolicina,
 MdfGrupe.Naziv as MDFGrupa,  MDFPodgrupe.Naziv as Podgrupa, MAticniPodatkiIgor.Materijal1, 
MAticniPodatkiIgor.Materijal2,MAticniPodatkiIgor.Debljina,MAticniPodatkiIgor.Tehnologija1,MAticniPodatkiIgor.Tehnologija2, podplan 
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
left join MaticniPodatkiIgor on MAticniPodatkiIgor.MPSifra = MaticniPodatki.MpSifra 
left join mdfgrupe on mdfgrupe.ID = MAticniPodatkiIgor.mdfgrupa  +
left join mdfpodgrupe on mdfpodgrupe.ID = MAticniPodatkiIgor.mdfpodgrupa +
where LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " and MAticniPodatkiIgor.MdfGrupa = " + Convert.ToInt32(cboGrupa.SelectedValue) + " and MAticniPodatkiIgor.MdfPodgrupa = " + Convert.ToInt32(cboPodgrupa.SelectedValue) + " order by Rb, id asc ";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Datum";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Nadredjeni";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Zapis";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "MP";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Kod";
            dataGridView1.Columns[5].Width = 180;
        }
        private void FillGrupe()
        {
            var query = "Select ID,Naziv From MdfGrupe order by ID asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboGrupa.DataSource = ds.Tables[0];
            cboGrupa.DisplayMember = "Naziv";
            cboGrupa.ValueMember = "ID";


        }
        private void FillMAterijal1()
        {
            var query = "Select distinct Materijal1 From MaticniPodatkiIgor order by Materijal1 asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboMaterijal1.DataSource = ds.Tables[0];
            cboMaterijal1.DisplayMember = "Materijal1";
            cboMaterijal1.ValueMember = "Materijal1";
        }

        private void FillMAterijal2()
        {
            var query = "Select distinct Materijal2 From MaticniPodatkiIgor order by Materijal2 asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboMaterijal2.DataSource = ds.Tables[0];
            cboMaterijal2.DisplayMember = "Materijal2";
            cboMaterijal2.ValueMember = "Materijal2";
        }

        private void FillDebljina()
        {
            var query = "Select distinct Debljina From MaticniPodatkiIgor order by Debljina asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboDebljina.DataSource = ds.Tables[0];
            cboDebljina.DisplayMember = "Debljina";
            cboDebljina.ValueMember = "Debljina";
        }

        private void FillTehnologija1()
        {
            var query = "Select distinct Tehnologija1 From MaticniPodatkiIgor order by Tehnologija1 asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboTehnologija1.DataSource = ds.Tables[0];
            cboTehnologija1.DisplayMember = "Tehnologija1";
            cboTehnologija1.ValueMember = "Tehnologija1";
        }

        private void FillTehnologija2()
        {
            var query = "Select distinct Tehnologija2 From MaticniPodatkiIgor order by Tehnologija2 asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboTehnologija2.DataSource = ds.Tables[0];
            cboTehnologija2.DisplayMember = "Tehnologija2";
            cboTehnologija2.ValueMember = "Tehnologija2";
        }

        private void FillPlanovi()
        {
            var query = "Select ID,Naziv From PlanArhiv order by ID asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboPlan.DataSource = ds.Tables[0];
            cboPlan.DisplayMember = "Naziv";
            cboPlan.ValueMember = "ID";
        }
        private void FillPodgrupe()
        {
            var query = "Select ID,Naziv From MDFPodgrupe Where IDNadredjena=" + Convert.ToInt32(cboGrupa.SelectedValue) + " order by ID asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboPodgrupa.DataSource = ds.Tables[0];
            cboPodgrupa.DisplayMember = "Naziv";
            cboPodgrupa.ValueMember = "ID";
        }
        private void FillComboPodplan()
        {
            var query = "select distinct Podplan From NetoPotrebeIgor where PodPlan is not null and IDPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " order by Podplan asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "Podplan";
            comboBox1.ValueMember = "Podplan";
        }

        private void cboGrupa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillPodgrupe();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            filterGrupa = 0;
            FillGV();
            FillComboPodplan();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filterGrupa = 1;
            FillGVGrupa();
            FillComboPodplan();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FillGVGrupaPodgrupa();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InsertVeza iv = new InsertVeza();
           
                iv.ObrisiNetoPotrebe(Convert.ToInt32(cboPlan.SelectedValue));
                FillGV();
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InsertVeza MspNal = new InsertVeza();
          
            //Skupiti sve parametre

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                { 
                 MspNal.PromeniKolicinu(Convert.ToInt32(row.Cells[0].Value.ToString()),
                    Convert.ToDouble(txtNovaKolicina.Text));
                }    
               
            }
           // FillGV();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FillGVOsobina();
            FillComboPodplan();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            InsertVeza MspNal = new InsertVeza();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    MspNal.DefinisiPodplan(Convert.ToInt32(row.Cells[0].Value.ToString()),txtPodPlan.Text);
                      // Convert.ToDouble(txtNovaKolicina.Text));
                }

            }
            FillGV();
            FillComboPodplan();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            KrojnaLista kl = new KrojnaLista(Convert.ToInt32(cboPlan.SelectedValue), txtPodPlan.Text);
            kl.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            PregledPodPlan pp = new PregledPodPlan(Convert.ToInt32(cboPlan.SelectedValue), txtPodPlan.Text);
            pp.Show();
        }

        private void chkRazvrstani_CheckStateChanged(object sender, EventArgs e)
        {
          
            if (filterGrupa == 0)
            {
                FillGV();
            }
            if (filterGrupa == 1)
            {
                FillGVGrupa();
            }
        }
        private int? DgvValue(object value)
        {
            if (value == null || value == DBNull.Value)
                return null;

            if (int.TryParse(value.ToString(), out int result))
                return result;

            return null;
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var row = dataGridView1.SelectedRows[0];

                if (filterGrupa == 0)
                {
                    var m1 = DgvValue(row.Cells["Materijal1"].Value);
                    var m2 = DgvValue(row.Cells["Materijal2"].Value);
                    var d = DgvValue(row.Cells["Debljina"].Value);
                    var t1 = DgvValue(row.Cells["Tehnologija1"].Value);
                    var t2 = DgvValue(row.Cells["Tehnologija2"].Value);
                    var g = DgvValue(row.Cells["MdfGrupa"].Value);
                    var pg = DgvValue(row.Cells["PodPlan"].Value);

                    if (m1.HasValue) cboMaterijal1.SelectedValue = m1.Value;
                    if (m2.HasValue) cboMaterijal2.SelectedValue = m2.Value;
                    if (d.HasValue) cboDebljina.SelectedValue = d.Value;
                    if (t1.HasValue) cboTehnologija1.SelectedValue = t1.Value;
                    if (t2.HasValue) cboTehnologija2.SelectedValue = t2.Value;
                    if (g.HasValue)
                    {
                        cboGrupa.SelectedValue = g.Value;
                        FillPodgrupe();

                        if (pg.HasValue) cboPodgrupa.SelectedValue = pg.Value;
                    }

                    txtPodPlan.Text = row.Cells["Podplan"].Value?.ToString() ?? "";
                }

            }
        }

        private void chkNerazvrstani_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkNerazvrstani.Checked == false)
            {
                FillGV();
            }
            else
            {
                var query = @"select NetoPotrebeIgor.ID,NetoPotrebeIgor.Datum as Datum,NetoPotrebeIgor.IDNadredjena,NetoPotrebeIgor.IDZapisa,Mp,MpStaraSif,Sklad,skNaziv as Skladiste,Lokac,LokOpis,Kolicina,
MdfGrupe.Naziv as MDFGrupa,  MDFPodgrupe.Naziv as Podgrupa, MaticniPodatkiIgor.Materijal1,MaticniPodatkiIgor.Materijal2,MaticniPodatkiIgor.Debljina,MaticniPodatkiIgor.Tehnologija1,MaticniPodatkiIgor.Tehnologija2, Podplan,
MaticniPodatkiIgor.MdfGrupa as GrupaID,MaticniPodatkiIgor.MdfPodgrupa as PodgrupaID , NetoPotrebeIGor.RB
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
left join MaticniPodatkiIgor on MAticniPodatkiIgor.MPSifra = MaticniPodatki.MpSifra 
left join MDFGrupe on MdfGrupe.ID = MaticniPodatkiIgor.MDFGrupa 
left join MdfPodgrupe on MAticniPodatkiIgor.MDFPodgrupa = MDFPodgrupe.ID
where LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " and Podplan='' order by RB, NetoPotrebeIgor.id asc";
                SqlConnection conn = new SqlConnection(connect);
                var da = new SqlDataAdapter(query, conn);
                var ds = new DataSet();
                da.Fill(ds);
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];

                dataGridView1.Columns["GrupaID"].Visible = false;
                dataGridView1.Columns["PodgrupaID"].Visible = false;

                DataGridViewColumn column = dataGridView1.Columns[0];
                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[0].Width = 50;

                DataGridViewColumn column2 = dataGridView1.Columns[1];
                dataGridView1.Columns[1].HeaderText = "Datum";
                dataGridView1.Columns[1].Width = 60;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Nadredjeni";
                dataGridView1.Columns[2].Width = 60;

                DataGridViewColumn column4 = dataGridView1.Columns[3];
                dataGridView1.Columns[3].HeaderText = "Zapis";
                dataGridView1.Columns[3].Width = 60;

                DataGridViewColumn column5 = dataGridView1.Columns[4];
                dataGridView1.Columns[4].HeaderText = "MP";
                dataGridView1.Columns[4].Width = 80;

                DataGridViewColumn column6 = dataGridView1.Columns[5];
                dataGridView1.Columns[5].HeaderText = "Kod";
                dataGridView1.Columns[5].Width = 180;
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtPodPlan.Text = comboBox1.SelectedValue.ToString();

            var query = @"select NetoPotrebeIgor.ID,NetoPotrebeIgor.Datum as Datum,NetoPotrebeIgor.IDNadredjena,NetoPotrebeIgor.IDZapisa,Mp,MpStaraSif,Sklad,skNaziv as Skladiste,Lokac,LokOpis,Kolicina,
MdfGrupe.Naziv as MDFGrupa,  MDFPodgrupe.Naziv as Podgrupa, MaticniPodatkiIgor.Materijal1,MaticniPodatkiIgor.Materijal2,MaticniPodatkiIgor.Debljina,MaticniPodatkiIgor.Tehnologija1,MaticniPodatkiIgor.Tehnologija2, Podplan,
MaticniPodatkiIgor.MdfGrupa as GrupaID,MaticniPodatkiIgor.MdfPodgrupa as PodgrupaID, NetoPotrebeIgor.rb 
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
inner join MaticniPodatkiIgor on MAticniPodatki.MpSifra=MaticniPodatkiIgor.MpSifra
left join MDFGrupe on MdfGrupe.ID = MaticniPodatkiIgor.MDFGrupa 
left join MdfPodgrupe on MaticniPodatkiIgor.MDFPodgrupa = MDFPodgrupe.ID
where LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " and Podplan='"+txtPodPlan.Text+ "' order by NetoPotrebeIgor.rb, NetoPotrebeIgor.id asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns["GrupaID"].Visible = false;
            dataGridView1.Columns["PodgrupaID"].Visible = false;

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Datum";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Nadredjeni";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Zapis";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "MP";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Kod";
            dataGridView1.Columns[5].Width = 180;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Planovi_Excel.PoveziRN prn = new Planovi_Excel.PoveziRN();
            prn.Show();
        }

        private void chkRazvrstani_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRazvrstani.Checked == true)
            { 
            chkNerazvrstani.Checked = false;
            }
               
                   

            if (chkRazvrstani.Checked == false)
            {
                chkNerazvrstani.Checked = true;
            }
        }

        private void chkNerazvrstani_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNerazvrstani.Checked == true)
            {
                chkRazvrstani.Checked = false;
            }



            if (chkNerazvrstani.Checked == false)
            {
                chkRazvrstani.Checked = true;
            }
        }
    }
}
