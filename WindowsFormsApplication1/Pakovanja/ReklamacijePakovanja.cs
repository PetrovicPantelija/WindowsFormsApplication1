using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication1.Pakovanja
{
    public partial class ReklamacijePakovanja : Form
    {
        public ReklamacijePakovanja()
        {
            InitializeComponent();
        }

        private void ReklamacijePakovanja_Load(object sender, EventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string query = @"
Select * from (SELECT 'Pocetna' as Isporuka,RkShippingItemRecPocetna.ShippingItemRecID , RkShippingItemRecPocetna.Zapisnik AS Zapisnik, 
RkShippingItemRecPocetna.SifraReklamacije as SifLargo , RkShippingItemRECPocetna.NazivReklamacije,  
RkShippingItemRECPocetna.Opis,KolicinaOrg,  Kolicina,   0 as Paletirano, Paletar as Paletar, 
VremePakovanja as VremePaletiranja, RkShippingItemRecPocetna.IsporucenoKupcu as Kontrolisano, Kontrolor as KOntrolor ,  
RkShippingItemRecPocetna.VremeKontrole,  Paleta, RkShippingPocetna.BrojIstovara as BrojUtovara, RkShippingPocetna.BrojUtovara as BrojIsporuke , 
MestoIsporuke FROM [dbo].RkShippingItemRecPocetna inner join RkShippingPocetna 
on [dbo].RkShippingItemRecPocetna.ShipingIDz = RkShippingPocetna.[ShippingID] 
inner join Saloni on RkShippingPocetna.SalonIDz = Saloni.SifraKomintentaMestoIsporuke) as t1
WHERE NOT EXISTS (
    SELECT 1
    FROM RkShippingRecPakovanje t2
    WHERE t2.RkShippingItemRecID = t1.ShippingItemRecID)
union
Select * from (SELECT 'Obradjena' as Isporuka,RkShippingItemRec.ShippingItemRecID , RkShippingItemRec.Zapisnik AS Zapisnik, 
RkShippingItemRec.SifraReklamacije as SifLargo , RkShippingItemREC.NazivReklamacije,  
RkShippingItemREC.Opis,KolicinaOrg,  Kolicina,   0 as Paletirano, Paletar as Paletar, 
VremePakovanja as VremePaletiranja, RkShippingItemRec.IsporucenoKupcu as Kontrolisano, Kontrolor as KOntrolor ,  
RkShippingItemRec.VremeKontrole , Paleta, RkShipping.BrojIstovara as BrojUtovara, RkShipping.BrojUtovara as BrojIsporuke , 
MestoIsporuke  FROM [dbo].RkShippingItemRec inner join RkShipping 
on [dbo].RkShippingItemRec.ShipingIDz = RkShipping.[ShippingID] 
inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke) as t1
WHERE NOT EXISTS (
    SELECT 1
    FROM RkShippingRecPakovanje t2
    WHERE t2.RkShippingItemRecID = t1.ShippingItemRecID)
union
Select * from (SELECT 'Isporuka' as Isporuka,  RkShippingItemRecZavrseno.ShippingItemRecID , RkShippingItemRecZavrseno.Zapisnik AS Zapisnik, 
RkShippingItemRecZavrseno.SifraReklamacije as SifLargo , RkShippingItemRECZavrseno.NazivReklamacije,  
RkShippingItemRECZavrseno.Opis,KolicinaOrg,  Kolicina,   0 as Paletirano, Paletar as Paletar, 
VremePakovanja as VremePaletiranja, RkShippingItemRecZavrseno.IsporucenoKupcu as Kontrolisano, Kontrolor as KOntrolor ,  
RkShippingItemRecZavrseno.VremeKontrole , Paleta, RkShippingZavrseno.BrojIstovara as BrojUtovara, RkShippingZavrseno.BrojUtovara as BrojIsporuke , 
MestoIsporuke FROM [dbo].RkShippingItemRecZavrseno inner join RkShippingZavrseno 
on [dbo].RkShippingItemRecZavrseno.ShipingIDz = RkShippingZavrseno.[ShippingID] 
inner join Saloni on RkShippingZavrseno.SalonIDz = Saloni.SifraKomintentaMestoIsporuke
where RkShippingItemRecZavrseno.IsporucenoKupcu = 0) as t1
WHERE NOT EXISTS (
    SELECT 1
    FROM RkShippingRecPakovanje t2
    WHERE t2.RkShippingItemRecID = t1.ShippingItemRecID)";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(s_connection))
            {
                try
                {
                    var da = new SqlDataAdapter(query, conn);
                    var ds = new DataSet();
                    da.Fill(ds);
                    dataGridView1.ReadOnly = true;
                    dataGridView1.DataSource = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška pri učitavanju podataka: " + ex.Message);
                }
            }
        }

        private void btnSpakovano_Click(object sender, EventArgs e)
        {
            // Collect selected IDs
            var ids = new List<int>();
            foreach (DataGridViewRow row in dataGridView3.SelectedRows)
            {
                if (row.Cells[0].Value != null)
                {
                    if (int.TryParse(row.Cells[0].Value.ToString(), out int v))
                        ids.Add(v);
                }
            }

            if (ids.Count == 0)
            {
                MessageBox.Show("Nema selektovanih zapisa za proglašenje.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show($"Da li ste sigurni da želite da proglasite spakovano za {ids.Count} zapisa?", "Potvrda", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
                return;

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(s_connection))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    try
                    {
                        cmd.CommandText = "UPDATE RkShippingRecPakovanje SET Spakovano = 1 WHERE RkShippingItemRecID = @id";
                        cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int));

                        foreach (var id in ids)
                        {
                            cmd.Parameters["@id"].Value = id;
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Greška pri označavanju spakovanih: " + ex.Message);
                        return;
                    }
                }

                // Refresh dataGridView3 to show updated rows
                try
                {
                    var da = new SqlDataAdapter("SELECT * FROM RkShippingRecPakovanje", conn);
                    var ds = new DataSet();
                    da.Fill(ds);
                    dataGridView3.ReadOnly = true;
                    dataGridView3.DataSource = ds.Tables[0];
                    MessageBox.Show($"Uspešno označeno {ids.Count} zapisa kao spakovano.", "Uspelo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška pri učitavanju podataka: " + ex.Message);
                }
            }
        }

        private void btnZaPakovanje_Click(object sender, EventArgs e)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(s_connection))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    try
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Selected && row.Cells[1].Value != null)
                            {
                                int id = Convert.ToInt32(row.Cells[1].Value);
                                cmd.CommandText = @"
                                                MERGE RkShippingRecPakovanje AS target
                                                USING (VALUES (@id)) AS source(RkShippingItemRecID)
                                                ON (target.RkShippingItemRecID = source.RkShippingItemRecID)
                                                WHEN MATCHED THEN
                                                    UPDATE SET ZaPakovanje = 1, Spakovano = 0
                                                WHEN NOT MATCHED THEN
                                                    INSERT (RkShippingItemRecID, ZaPakovanje, Spakovano)
                                                    VALUES (source.RkShippingItemRecID, 1, 0);";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@id", id);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Greška pri upisu za pakovanje: " + ex.Message);
                        return;
                    }
                }

                // Refresh dataGridView2 to show inserted rows
                try
                {
                    var da = new SqlDataAdapter("SELECT * FROM RkShippingRecPakovanje", conn);
                    var ds = new DataSet();
                    da.Fill(ds);
                    dataGridView3.ReadOnly = true;
                    dataGridView3.DataSource = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška pri učitavanju podataka: " + ex.Message);
                }
            }
        }
    }
}
