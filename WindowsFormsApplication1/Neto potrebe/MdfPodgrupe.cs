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
    public partial class MdfPodgrupe : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        //komentar1
        public MdfPodgrupe()
        {
            InitializeComponent();
        }

        private void MdfPodgrupe_Load(object sender, EventArgs e)
        {
            FillGV();
            FillCombo();
        }
        private void FillGV()
        {
            var query = @"Select MdfPodgrupe.ID as ID,IDNadredjena,MdfGrupe.Naziv as Grupa,MDfPodgrupe.Naziv as Podgrupa 
From MdfPodgrupe 
inner join MdfGrupe on MdfPodgrupe.IdNadredjena=MdfGrupe.ID 
order by MdfPodgrupe.ID";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns["IDNadredjena"].Visible = false;
        }
        private void FillCombo()
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

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (txtNaziv.Text != null)
            {
                InsertPodgrupa(Convert.ToInt32(cboGrupa.SelectedValue.ToString()), txtNaziv.Text.ToString().TrimEnd());
                FillGV();
            }
            else
            {
                MessageBox.Show("Mora se upisati naziv podgrupe!");
                return;
            }
        }

        private void btnIzmeni_Click(object sender, EventArgs e)
        {
            if (txtID.Text != null)
            {
                UpdatePodgrupa(Convert.ToInt32(txtID.Text), Convert.ToInt32(cboGrupa.SelectedValue.ToString()), txtNaziv.Text.ToString().TrimEnd());
                FillGV();
            }
            else
            {
                MessageBox.Show("Nije odabrana zapis za izmenu!"); 
                return;
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if(txtID.Text != null) 
            { 
                DeletePodgrupa(Convert.ToInt32(txtID.Text));
                FillGV();
            } 
            else 
            { 
                MessageBox.Show("Nije odabrana zapis za brisanje!"); 
                return; 
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    txtID.Text = row.Cells["ID"].Value.ToString();
                    txtNaziv.Text = row.Cells["Podgrupa"].Value.ToString().TrimEnd();
                    cboGrupa.SelectedValue = Convert.ToInt32(row.Cells["IDNadredjena"].Value.ToString());
                }
            }
        }
        private void InsertPodgrupa(int IdNadredjena,string Naziv)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertMdfPodgrupe";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idNadredjena = new SqlParameter();
            idNadredjena.ParameterName = "@IDNadredjena";
            idNadredjena.SqlDbType = SqlDbType.Int;
            idNadredjena.Direction = ParameterDirection.Input;
            idNadredjena.Value = IdNadredjena;
            cmd.Parameters.Add(idNadredjena);

            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@Naziv";
            naziv.SqlDbType = SqlDbType.NVarChar;
            naziv.Size = 100;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = Naziv;
            cmd.Parameters.Add(naziv);

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
                throw new Exception("Neuspešan upis NHM brojeva");
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos NHM broja je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        private void UpdatePodgrupa(int ID,int IdNadredjena, string Naziv)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateMdfPodgrupe";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter idNadredjena = new SqlParameter();
            idNadredjena.ParameterName = "@IDNadredjena";
            idNadredjena.SqlDbType = SqlDbType.Int;
            idNadredjena.Direction = ParameterDirection.Input;
            idNadredjena.Value = IdNadredjena;
            cmd.Parameters.Add(idNadredjena);

            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@Naziv";
            naziv.SqlDbType = SqlDbType.NVarChar;
            naziv.Size = 100;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = Naziv;
            cmd.Parameters.Add(naziv);

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
                throw new Exception("Neuspešan upis NHM brojeva");
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos NHM broja je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        private void DeletePodgrupa(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteMdfPodgrupe";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
                throw new Exception("Neuspešan upis NHM brojeva");
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos NHM broja je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
    }
}
