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
        }
        private void FillGV()
        {
            var query = @"select ID,NetoPotrebeIgor.Datum as Datum,IDNadredjena,IDZapisa,Mp,MpStaraSif,Sklad,skNaziv,Lokac,LokOpis,Kolicina
From NetoPotrebeIgor
inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra
inner join sklad on NetoPotrebeIGor.Sklad=sklad.SkSifra
inner join Lokac on NetoPotrebeIgor.Lokac=Lokac.LokSifra 
where LokSkSifra=Sklad and IdPlana="+Convert.ToInt32(cboPlan.SelectedValue)+" order by id asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
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
        private void FillPodgrupe()
        {
            var query = "Select ID,Naziv From Podgrupe Where IDNadredjena=" + Convert.ToInt32(cboGrupa.SelectedValue) + " order by ID asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboGrupa.DataSource = ds.Tables[0];
            cboGrupa.DisplayMember = "Naziv";
            cboGrupa.ValueMember = "ID";
        }

        private void cboGrupa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillPodgrupe();
        }
    }
}
