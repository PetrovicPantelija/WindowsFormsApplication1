using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
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

namespace WindowsFormsApplication1.Neto_potrebe
{
    public partial class KrojnaLista : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
       int tmpPlanID  = 0;
        string tmpPodPlan = "";

        // ADDED: authorization/state members copied from PregledPodPlan
        bool status = false;
        // existing connect stays; keep single definition
        public bool PravoP;
        public static string code = "PregledPodPlan";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;  // added members
        bool delete;
        string niz = "";
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        public KrojnaLista()
        {
            InitializeComponent();
       
            panel2.Visible = false;
            dataGridView2.CellEndEdit += dataGridView2_CellEndEdit;
        }

        public KrojnaLista(int PlanID, string PodPlan)
        {
            InitializeComponent();
         
            tmpPlanID = PlanID;
            tmpPodPlan = PodPlan;
            panel2.Visible = false;
            dataGridView2.CellEndEdit += dataGridView2_CellEndEdit;
        }
        private void KrojnaLista_Load(object sender, EventArgs e)
        {
            IdGrupe();
            IdForme();
            PravoPristupa();
            if (Pravo == false)
            {
                this.Close();
                return;
            }

            if (tmpPlanID!=0 && tmpPodPlan != "")
            {
                FillPlanovi();
                txtPodPlan.Text = tmpPodPlan;
                cboPlan.SelectedValue = tmpPlanID;
                FillGV();
                VratiKrojneListe();
            }
            else
            {
                FillPlanovi();
            }

        }

        public string IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            using (SqlConnection conn = new SqlConnection(s_connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    int count = 0;
                    while (dr.Read())
                    {
                        if (count == 0)
                        {
                            niz = dr["IdGrupe"].ToString();
                            count++;
                        }
                        else
                        {
                            niz = niz + "," + dr["IdGrupe"].ToString();
                            count++;
                        }
                    }
                }
            }
            return niz;
        }

        private int IdForme()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select IdForme from Forme where Rtrim(Code)=" + "'" + code + "'";
            using (SqlConnection conn = new SqlConnection(s_connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        idForme = Convert.ToInt32(dr["IdForme"].ToString());
                    }
                }
            }
            return idForme;
        }

        private void PravoPristupa()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select * From GrupaForma Where IdGrupe in (" + niz + ") and IdForme=" + idForme;
            using (SqlConnection conn = new SqlConnection(s_connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows == false)
                    {
                        MessageBox.Show("Nemate prava za pristup ovoj formi", code);
                        Pravo = false;
                    }
                    else
                    {
                        Pravo = true;
                        while (reader.Read())
                        {
                            insert = Convert.ToBoolean(reader["Upis"]);
                            if (insert == false)
                            {
                                // disable new control if present e.g. tsNew.Enabled = false;
                            }
                            update = Convert.ToBoolean(reader["Izmena"]);
                            if (update == false)
                            {
                                // disable save control if present e.g. tsSave.Enabled = false;
                            }
                            delete = Convert.ToBoolean(reader["Brisanje"]);
                            if (delete == false)
                            {
                                // disable delete control if present e.g. tsDelete.Enabled = false;
                            }
                        }
                    }
                }
            }
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
        private void VratiKrojneListe()
        {
            if (tmpPlanID == 0)
            {
                var query = "select distinct KrojnaLista From KrojnaLista Where PlanID=" + Convert.ToInt32(cboPlan.SelectedValue);
                SqlConnection conn = new SqlConnection(connect);
                var da = new SqlDataAdapter(query, conn);
                var ds = new DataSet();
                da.Fill(ds);
                cboKrojnaLista.DataSource = ds.Tables[0];
                cboKrojnaLista.DisplayMember = "KrojnaLista";
                cboKrojnaLista.ValueMember = "KrojnaLIsta";
            }
            else
            {
                var query = "select distinct KrojnaLista From KrojnaLista Where PlanID=" + tmpPlanID;
                SqlConnection conn = new SqlConnection(connect);
                var da = new SqlDataAdapter(query, conn);
                var ds = new DataSet();
                da.Fill(ds);
                cboKrojnaLista.DataSource = ds.Tables[0];
                cboKrojnaLista.DisplayMember = "KrojnaLista";
                cboKrojnaLista.ValueMember = "KrojnaLIsta";
            }
            
        }
     //stavke plana i podplana kojima nije dodeljena krojna lista
     private void FillGV()
        {
            var query = @"SELECT 
    n.ID,
    n.IDPlana,
    n.Podplan,
    n.IDNadredjena,
    n.IDZapisa,
    n.Mp,
    m.MpStaraSif,
    n.Kolicina - ISNULL(SUM(k.Kolicina), 0) AS Kolicina
FROM NetoPotrebeIgor n
INNER JOIN MaticniPodatki m 
    ON n.MP = m.MpSifra
INNER JOIN Sklad s 
    ON n.Sklad = s.SkSifra
INNER JOIN Lokac l 
    ON n.Lokac = l.LokSifra
LEFT JOIN KrojnaLista k 
    ON k.ZapisNP = n.IDZapisa
   AND k.PlanID = n.IDPlana
WHERE 
    l.LokSkSifra = s.SkSifra and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " and n.PodPlan  Like ('%" + txtPodPlan.Text + "%') " +
    "GROUP BY n.Rb, n.ID, n.IDPlana, n.Podplan, n.IDNadredjena,n.IDZapisa, n.Mp, m.MpStaraSif, n.Kolicina " +
    "HAVING n.Kolicina - ISNULL(SUM(k.Kolicina), 0) > 0 " +
    "ORDER BY n.Rb,n.ID ASC;";


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
            dataGridView1.Columns[1].HeaderText = "IDPlana";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Podplan";
            dataGridView1.Columns[2].Width = 140;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "IDNadredjena";
            dataGridView1.Columns[3].Width = 60;
            dataGridView1.Columns[3].Visible = false;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "IDZapisa";
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[4].Visible = false;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "MP";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Stara sif";
            dataGridView1.Columns[6].Width = 160;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Kolicina";
            dataGridView1.Columns[7].Width = 60;
        }

     /*
        private void FillGV()
        {
            var query = @"select NetoPotrebeIgor.ID, NetoPotrebeIGor.IDPlana, Podplan, NetoPotrebeIgor.IDNadredjena,NetoPotrebeIgor.IDZapisa,Mp,MpStaraSif,Kolicina
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
left join MDFGrupe on MdfGrupe.ID = MaticniPodatki.MDFGrupa 
left join MdfPodgrupe on MAticniPodatki.MDFPodgrupa = MDFPodgrupe.ID
where LokSkSifra=Sklad and IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " and NetoPotrebeIgor.PodPlan  Like ('%" + txtPodPlan.Text + "%') order by NetoPotrebeIgor.id asc";
            
            
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
            dataGridView1.Columns[1].HeaderText = "IDPlana";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Podplan";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "IDNadredjena";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "IDZapisa";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "MP";
            dataGridView1.Columns[5].Width = 180;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Stara sif";
            dataGridView1.Columns[6].Width = 180;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Kolicina";
            dataGridView1.Columns[7].Width = 100;
        }*/

        public class StavkeKrojneListe
        {
            public int ID { get; set; }
            public int IDPlana { get; set; }
            public string Podplan { get; set; }
            public int IDNadredjena { get; set; }
            public int IDZapisa { get; set; }
            public int MP { get; set; }
            public string MPStaraSifra { get; set; }
            public decimal Kolicina { get; set; }

            public int Prioritet { get; set; }
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            FillPodplanovi();

        }
        private void FillPodplanovi()
        {
            var query = @"select distinct Podplan from NetopotrebeIgor Where IDPlana="+Convert.ToInt32(cboPlan.SelectedValue)+" and PodPlan<>''";

            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "PodPlan";
            dataGridView3.Columns[0].Width = 250;
        }

        private void btnUbaci_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    var stavka = new StavkeKrojneListe
                    {
                        ID = Convert.ToInt32(row.Cells[0].Value),
                        IDPlana = Convert.ToInt32(row.Cells[1].Value),
                        Podplan = row.Cells[2].Value.ToString(),
                        IDNadredjena = Convert.ToInt32(row.Cells[3].Value),
                        IDZapisa = Convert.ToInt32(row.Cells[4].Value),
                        MP = Convert.ToInt32(row.Cells[5].Value),
                        MPStaraSifra = row.Cells[6].Value.ToString(),
                        Kolicina = Convert.ToDecimal(row.Cells[7].Value),
                        Prioritet = Convert.ToInt32(0)
                    };
                    list.Add(stavka);
                }
            }
            // bind directly to the BindingList so edits propagate
            list = new BindingList<StavkeKrojneListe>(list.OrderByDescending(x => x.Prioritet).ToList());
            dataGridView2.DataSource = list;

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Plan ID";
            dataGridView2.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Podplan";
            dataGridView2.Columns[2].Width = 140;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "IDNadredjena";
            dataGridView2.Columns[3].Width = 60;
            dataGridView2.Columns[3].Visible = false;

            DataGridViewColumn column5 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "IDZapisa";
            dataGridView2.Columns[4].Width = 80;
            dataGridView2.Columns[4].Visible = false;

            DataGridViewColumn column6 = dataGridView2.Columns[5];
            dataGridView2.Columns[5].HeaderText = "MP";
            dataGridView2.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView2.Columns[6];
            dataGridView2.Columns[6].HeaderText = "Stara sif";
            dataGridView2.Columns[6].Width = 160;

            DataGridViewColumn column8 = dataGridView2.Columns[7];
            dataGridView2.Columns[7].HeaderText = "Kol";
            dataGridView2.Columns[7].Width = 50;
            /*
             for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
             {
                 int index = dataGridView2.Rows.Add();
                 dataGridView2.Rows[index].Cells[0].Value = dataGridView1.SelectedRows[i].Cells[0].Value.ToString();
                 dataGridView2.Rows[index].Cells[1].Value = dataGridView1.SelectedRows[i].Cells[1].Value.ToString();
                 dataGridView2.Rows[index].Cells[2].Value = dataGridView1.SelectedRows[i].Cells[2].Value.ToString();
                 dataGridView2.Rows[index].Cells[3].Value = dataGridView1.SelectedRows[i].Cells[3].Value.ToString();
                 dataGridView2.Rows[index].Cells[4].Value = dataGridView1.SelectedRows[i].Cells[4].Value.ToString();
                 dataGridView2.Rows[index].Cells[5].Value = dataGridView1.SelectedRows[i].Cells[5].Value.ToString();
                 dataGridView2.Rows[index].Cells[6].Value = dataGridView1.SelectedRows[i].Cells[6].Value.ToString();
                 dataGridView2.Rows[index].Cells[7].Value = dataGridView1.SelectedRows[i].Cells[7].Value.ToString();
                 // NetoPotrebeIgor.ID,Podplan, NetoPotrebeIgor.IDNadredjena,NetoPotrebeIgor.IDZapisa,Mp,MpStaraSif,Kolicina, Sklad,skNaziv as Skladiste,Lokac,LokOpis,
                 //MdfGrupe.Naziv as MDFGrupa,  MDFPodgrupe.Naziv as Podgrupa, MaticniPodatki.Materijal1,MaticniPodatki.Materijal2,MaticniPodatki.Debljina,MaticniPodatki.Tehnologija1,MaticniPodatki.Tehnologija2
             }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertKrojnaLista isporuka = new InsertKrojnaLista();
           
         
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row != null && row.Cells[0].Value != null)
                {
                    isporuka.InsertKrojna(Convert.ToInt32(row.Cells[5].Value.ToString()),Convert.ToInt32(row.Cells[4].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), Convert.ToDouble(row.Cells[7].Value.ToString()), txtKrojna.Text, Convert.ToInt32(row.Cells[8].Value.ToString()));
                   
                }
            }
           
            MessageBox.Show("Formirana je krojna lista");
            FillGV();
            VratiKrojneListe();
        }
        private BindingList<StavkeKrojneListe> list = new BindingList<StavkeKrojneListe>();
        private void VratiStavkeKrojneListe()
        {
            list.Clear();
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                string query = @"select NetoPotrebeIgor.ID, NetoPotrebeIGor.IDPlana, Podplan, NetoPotrebeIgor.IDNadredjena,NetoPotrebeIgor.IDZapisa,NetoPotrebeIgor.MP,MpStaraSif,KrojnaLista.Kolicina,
  KrojnaLista.Prioritet
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
left join MaticniPodatkiIgor on MAticniPodatkiIgor.MPSifra=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
left join MDFGrupe on MdfGrupe.ID = MaticniPodatkiIgor.MDFGrupa 
left join MdfPodgrupe on MAticniPodatkiIgor.MDFPodgrupa = MDFPodgrupe.ID
inner join KrojnaLista on NetopotrebeIgor.IDZapisa=KrojnaLista.ZapisNP
where LokSkSifra=Sklad and IdPlana=" + cboPlan.SelectedValue + " and NetoPotrebeIgor.PodPlan  Like ('" + txtPodPlan.Text + "') and KrojnaLista.KrojnaLista='" + cboKrojnaLista.SelectedValue + "' " +
" order by NetoPotrebeIgor.id asc";

                
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var stavka = new StavkeKrojneListe
                            {
                                ID = Convert.ToInt32(dr[0]),
                                IDPlana = Convert.ToInt32(dr[1]),
                                Podplan = dr[2].ToString(),
                                IDNadredjena = Convert.ToInt32(dr[3]),
                                IDZapisa = Convert.ToInt32(dr[4]),
                                MP = Convert.ToInt32(dr[5]),
                                MPStaraSifra = dr[6].ToString(),
                                Kolicina = Convert.ToDecimal(dr[7]),
                                Prioritet = Convert.ToInt32(dr[8])
                            };
                            list.Add(stavka);
                        }
                    }
                }
            }
            dataGridView2.DataSource = null;
            list = new BindingList<StavkeKrojneListe>(list.OrderByDescending(x => x.Prioritet).ToList());
            dataGridView2.DataSource = list;

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Plan ID";
            dataGridView2.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Podplan";
            dataGridView2.Columns[2].Width = 140;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "IDNadredjena";
            dataGridView2.Columns[3].Width = 60;
            dataGridView2.Columns[3].Visible = false;

            DataGridViewColumn column5 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "IDZapisa";
            dataGridView2.Columns[4].Width = 80;
            dataGridView2.Columns[4].Visible = false;

            DataGridViewColumn column6 = dataGridView2.Columns[5];
            dataGridView2.Columns[5].HeaderText = "MP";
            dataGridView2.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView2.Columns[6];
            dataGridView2.Columns[6].HeaderText = "Stara sif";
            dataGridView2.Columns[6].Width = 160;

            DataGridViewColumn column8 = dataGridView2.Columns[7];
            dataGridView2.Columns[7].HeaderText = "Kol";
            dataGridView2.Columns[7].Width = 50;
            // Allow editing of the quantity column if present
            if (dataGridView2.Columns.Count > 0 && dataGridView2.Columns.Contains("Kolicina"))
            {
                dataGridView2.Columns["Kolicina"].ReadOnly = false;
            }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var colName = dataGridView2.Columns[e.ColumnIndex].Name;
            if (colName != "Kolicina")
                return;

            var cell = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex];
            decimal newVal;
            if (cell.Value == null || !decimal.TryParse(cell.Value.ToString(), out newVal))
            {
                MessageBox.Show("Neispravna vrednost za količinu.");
                // restore previous value from the bound list
                dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = list[e.RowIndex].Kolicina;
                return;
            }

            // Update the underlying bound object
            list[e.RowIndex].Kolicina = newVal;
/*
            // Persist change to database: update existing KrojnaLista row or insert if missing
            try
            {
                using (SqlConnection conn = new SqlConnection(connect))
                {
                    conn.Open();
                    string updateSql = "UPDATE KrojnaLista SET Kolicina = @Kolicina WHERE ZapisNP = @ZapisNP AND PlanID = @PlanID AND KrojnaLista = @KrojnaLista";
                    using (SqlCommand cmd = new SqlCommand(updateSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Kolicina", newVal);
                        cmd.Parameters.AddWithValue("@ZapisNP", list[e.RowIndex].IDZapisa);
                        cmd.Parameters.AddWithValue("@PlanID", list[e.RowIndex].IDPlana);
                        cmd.Parameters.AddWithValue("@KrojnaLista", txtKrojna.Text);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows == 0)
                        {
                            // No existing row updated - insert new record
                            InsertKrojnaLista inserter = new InsertKrojnaLista();
                            inserter.InsertKrojna(list[e.RowIndex].MP, list[e.RowIndex].IDZapisa, list[e.RowIndex].IDPlana, Convert.ToDouble(newVal), txtKrojna.Text, list[e.RowIndex].Prioritet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri čuvanju količine: " + ex.Message);
            }
*/
        }
        private async void cboKrojnaLista_SelectedValueChanged(object sender, EventArgs e)
        { if (cboKrojnaLista.SelectedValue != null)
            { 
            txtKrojna.Text = cboKrojnaLista.SelectedValue.ToString();
            VratiStavkeKrojneListe();
            }
        }

        private void btnNovaKL_Click(object sender, EventArgs e)
        {
            txtKrojna.Text = "";
            list.Clear();
            dataGridView2.DataSource = null;
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected)
                {
                    txtPodPlan.Text = row.Cells[0].Value.ToString();
                    panel2.Visible = false;

                    FillGV();
                    VratiKrojneListe();
                }
            }
            
        }

        private int GetIntValue(object value)
        {
            if (value == null || value == DBNull.Value)
                return 0;

            int result;
            if (int.TryParse(value.ToString(), out result))
                return result;

            return 0;
        }

   

        private void iconButton2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dvr in dataGridView2.SelectedRows)
            {
                if (dvr != null)
                {
                    list.RemoveAt(dvr.Index);
                  
                }
            }
            list = new BindingList<StavkeKrojneListe>(list.OrderByDescending(x => x.Prioritet).ToList());
            dataGridView2.DataSource = list;
            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Plan ID";
            dataGridView2.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Podplan";
            dataGridView2.Columns[2].Width = 140;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "IDNadredjena";
            dataGridView2.Columns[3].Width = 60;
            dataGridView2.Columns[3].Visible = false;

            DataGridViewColumn column5 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "IDZapisa";
            dataGridView2.Columns[4].Width = 80;
            dataGridView2.Columns[4].Visible = false;

            DataGridViewColumn column6 = dataGridView2.Columns[5];
            dataGridView2.Columns[5].HeaderText = "MP";
            dataGridView2.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView2.Columns[6];
            dataGridView2.Columns[6].HeaderText = "Stara sif";
            dataGridView2.Columns[6].Width = 160;

            DataGridViewColumn column8 = dataGridView2.Columns[7];
            dataGridView2.Columns[7].HeaderText = "Kol";
            dataGridView2.Columns[7].Width = 50;

        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dvr in dataGridView2.SelectedRows)
            {
                if (dvr != null)
                {
                    list[dvr.Index].Prioritet = 1;
                  
                }
            }
            list = new BindingList<StavkeKrojneListe>(list.OrderByDescending(x => x.Prioritet).ToList());


            dataGridView2.DataSource = list;
            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Plan ID";
            dataGridView2.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Podplan";
            dataGridView2.Columns[2].Width = 140;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "IDNadredjena";
            dataGridView2.Columns[3].Width = 60;
            dataGridView2.Columns[3].Visible = false;

            DataGridViewColumn column5 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "IDZapisa";
            dataGridView2.Columns[4].Width = 80;
            dataGridView2.Columns[4].Visible = false;

            DataGridViewColumn column6 = dataGridView2.Columns[5];
            dataGridView2.Columns[5].HeaderText = "MP";
            dataGridView2.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView2.Columns[6];
            dataGridView2.Columns[6].HeaderText = "Stara sif";
            dataGridView2.Columns[6].Width = 160;

            DataGridViewColumn column8 = dataGridView2.Columns[7];
            dataGridView2.Columns[7].HeaderText = "Kol";
            dataGridView2.Columns[7].Width = 50;


            //dataGridView2.Sort(dataGridView2.Columns["Prioritet"], ListSortDirection.Ascending);

        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            InsertKrojnaLista isporuka = new InsertKrojnaLista();


            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row != null && row.Cells[0].Value != null)
                {
                    isporuka.UpdateKrojna(Convert.ToInt32(row.Cells[5].Value.ToString()), Convert.ToInt32(row.Cells[4].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), Convert.ToDouble(row.Cells[7].Value.ToString()), txtKrojna.Text, Convert.ToInt32(row.Cells[8].Value.ToString()));

                }
            }

            MessageBox.Show("Promenjene su vrednosti krojne liste");
           // FillGV();
            //VratiKrojneListe();
            VratiStavkeKrojneListe();
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            InsertKrojnaLista isporuka = new InsertKrojnaLista();
            isporuka.DeleteKrojna(Convert.ToInt32(cboPlan.SelectedValue), cboKrojnaLista.Text);
            MessageBox.Show("Izbrisana je cela krojna lista " + cboKrojnaLista.Text);
            VratiKrojneListe();
        }
    }
}
