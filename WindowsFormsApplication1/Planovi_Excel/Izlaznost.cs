using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class Izlaznost : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public Izlaznost()
        {
            InitializeComponent();
        }

        private void FillGV()
        {
            var query = "Select ID,Izlaznost.MPSifra,MpStaraSif,MpNaziv,Mesec,Godina,Kolicina " +
            "From Izlaznost " +
            "Inner join MaticniPodatki on Izlaznost.MpSifra=MaticniPodatki.MpSifra " +
            "Order by ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);

            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void Izlaznost_Load(object sender, EventArgs e)
        {
            FillGV();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}