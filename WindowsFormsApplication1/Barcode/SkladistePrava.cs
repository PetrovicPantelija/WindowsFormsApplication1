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
    public partial class SkladistePrava : Form
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public SkladistePrava()
        {
            InitializeComponent();
            FillCombo();
            FillChkList();
        }
        private void FillCombo()
        {
            var query = " Select RTrim(Korisnik) as Korisnik from Korisnici order by Korisnik asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "Korisnik";
            comboBox1.ValueMember = "Korisnik";
        }
        private void FillChkList()
        {
            var query = "Select SkSifra,RTrim(SkNaziv) as SkNaziv From Sklad order by SkSifra asc";
            SqlConnection conn = new SqlConnection(connect);
            var da=new SqlDataAdapter(query, conn);
            var ds=new DataSet();
            da.Fill(ds);
            checkedListBox1.DataSource = ds.Tables[0];
            checkedListBox1.DisplayMember = "SkNaziv";
            checkedListBox1.ValueMember = "SkSifra";
        }
        private void GetChecked()
        {
            

            List<int> skladList = new List<int>();
            var query = "Select sklad From BarcodeSkladisteKorisnik Where Korisnik='" + comboBox1.SelectedValue + "' order by sklad asc";
            using (SqlConnection conn = new SqlConnection(connect))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    skladList.Add(Convert.ToInt32(row["sklad"]));
                }
            }
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                var item = checkedListBox1.Items[i];
                int skSifra = Convert.ToInt32(((DataRowView)item)["SkSifra"].ToString());
                if (skladList.Contains(skSifra))
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            GetChecked();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string korisnik = comboBox1.SelectedValue.ToString();

            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                foreach (var item in checkedListBox1.CheckedItems)
                {
                    var skSifra = ((DataRowView)item)["SkSifra"].ToString();
                    var checkQuery = "SELECT COUNT(1) FROM BarcodeSkladisteKorisnik WHERE Korisnik = @Korisnik AND sklad = @Sklad";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Korisnik", korisnik);
                        checkCmd.Parameters.AddWithValue("@Sklad", skSifra);

                        int exists = (int)checkCmd.ExecuteScalar();
                        if (exists == 0)
                        {
                            var insertQuery = "INSERT INTO BarcodeSkladisteKorisnik (Korisnik, sklad) VALUES (@Korisnik, @Sklad)";
                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@Korisnik", korisnik);
                                insertCmd.Parameters.AddWithValue("@Sklad", skSifra);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                foreach (var item in checkedListBox1.Items)
                {
                    var skSifra = ((DataRowView)item)["SkSifra"].ToString();
                    if (!checkedListBox1.CheckedItems.Contains(item))
                    {
                        var deleteQuery = "DELETE FROM BarcodeSkladisteKorisnik WHERE Korisnik = @Korisnik AND sklad = @Sklad";
                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                        {
                            deleteCmd.Parameters.AddWithValue("@Korisnik", korisnik);
                            deleteCmd.Parameters.AddWithValue("@Sklad", skSifra);
                            deleteCmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Prava dodeljena!");
            }
        }
    }
}
