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

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    public partial class Reklamacije : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        int ID;
        public Reklamacije()
        {
            InitializeComponent();
        }
        public Reklamacije(int id)
        {
            ID = id;
            InitializeComponent();
        }
        DateTime datum;
        DateTime datum2;
        private void FillGV()
        {
            //var query = "select RkShipping.DatumUtovara,RkShippingItemRec.* from RkShippingItemRec inner join RkShipping on RkShippingItemRec.ShipingIDz=RkShipping.ShippingID Where RkShipping.DatumUtovara>=GetDate()";
            var query = "select RkShippingItemRecPocetna.ShippingItemRecID, RkShippingItemRecPocetna.ShipingIdz,  RkShippingPocetna.ShippingNo as BarkodIstovara, RkShippingPocetna.BrojUtovara as BrojUtovara," +
                "RkShippingPocetna.BrojIstovara as BrojIstovara, RkShippingItemRecPocetna.SifraElementa as Element, Saloni.MestoIsporuke, RkShippingItemRecPocetna.SifraReklamacije, RkShippingItemRecPocetna.NazivReklamacije, " +
                "RkShippingItemRecPocetna.KolicinaOrg, RkShippingItemRecPocetna.Zapisnik,  RkShippingItemRecPocetna.Paleta, RkShippingItemRecPocetna.Paket, RkShippingItemRecPocetna.BrojNalepnica," +
                "RkShippingItemRecPocetna.Tezina, RkShippingItemRecPocetna.LargoDimenzija, RkShippingItemRecPocetna.SifraReklamacijeLargo,  RkShippingItemRecPocetna.BrojKoleta  " +
                "FROM RkShippingItemRecPocetna " +
                "inner join RkShippingPocetna on  RkShippingPocetna.ShippingID = RkShippingItemRecPocetna.ShipingIdz  " +
                "inner join SysKomitenti on RkShippingPocetna.KupacIDz = SysKomitenti.KomintentID " +
                "inner join Saloni on RkShippingPocetna.SalonIDz = Saloni.SifraKomintentaMestoIsporuke " +
                "where RkShippingPocetna.DatumUtovara >= GETDATE() " +
                "Union " +
                "Select RkShippingItemRec.ShippingItemRecID, RkShippingItemRec.ShipingIdz,  RkShipping.ShippingNo as BarkodIstovara, RkShipping.BrojUtovara as BrojUtovara,RkShipping.BrojIstovara as BrojIstovara, " +
                "RkShippingItemRec.SifraElementa as Element, Saloni.MestoIsporuke, RkShippingItemRec.SifraReklamacije, RkShippingItemRec.NazivReklamacije, RkShippingItemRec.KolicinaOrg, RkShippingItemRec.Zapisnik, " +
                "RkShippingItemRec.Paleta, RkShippingItemRec.Paket, RkShippingItemRec.BrojNalepnica, RkShippingItemRec.Tezina, RkShippingItemRec.LargoDimenzija, RkShippingItemRec.SifraReklamacijeLargo, " +
                "RkShippingItemRec.BrojKoleta  " +
                "FROM RkShippingItemRec " +
                "inner join RkShipping on  RkShipping.ShippingID = RkShippingItemRec.ShipingIdz  " +
                "inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID " +
                "inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke " +
                "where RkShipping.DatumUtovara>=GETDATE()";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.FromArgb(125, 158, 192);
            dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
        }

        private void Reklamacije_Load(object sender, EventArgs e)
        {
            FillGV();
            FillGVStavke();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            datum = dateTimePicker1.Value;
            datum2 = dateTimePicker2.Value;
            string datumPom = datum.ToString("yyyy-MM-dd");
            string datumPom2 = datum2.ToString("yyyy-MM-dd");

            //var query = "select RkShipping.DatumUtovara,RkShippingItemRec.* from RkShippingItemRec inner join RkShipping on RkShippingItemRec.ShipingIDz=RkShipping.ShippingID Where RkShipping.DatumUtovara between'" + datumPom + "' and '"+datumPom2+"'";
            var query = "select RkShippingItemRecPocetna.ShippingItemRecID, RkShippingItemRecPocetna.ShipingIdz,  RkShippingPocetna.ShippingNo as BarkodIstovara, RkShippingPocetna.BrojUtovara as BrojUtovara," +
                "RkShippingPocetna.BrojIstovara as BrojIstovara, RkShippingItemRecPocetna.SifraElementa as Element, Saloni.MestoIsporuke, RkShippingItemRecPocetna.SifraReklamacije, RkShippingItemRecPocetna.NazivReklamacije, " +
                "RkShippingItemRecPocetna.KolicinaOrg, RkShippingItemRecPocetna.Zapisnik,  RkShippingItemRecPocetna.Paleta, RkShippingItemRecPocetna.Paket, RkShippingItemRecPocetna.BrojNalepnica," +
                "RkShippingItemRecPocetna.Tezina, RkShippingItemRecPocetna.LargoDimenzija, RkShippingItemRecPocetna.SifraReklamacijeLargo,  RkShippingItemRecPocetna.BrojKoleta  " +
                "FROM RkShippingItemRecPocetna " +
                "inner join RkShippingPocetna on  RkShippingPocetna.ShippingID = RkShippingItemRecPocetna.ShipingIdz  " +
                "inner join SysKomitenti on RkShippingPocetna.KupacIDz = SysKomitenti.KomintentID " +
                "inner join Saloni on RkShippingPocetna.SalonIDz = Saloni.SifraKomintentaMestoIsporuke " +
                "where RkShippingPocetna.DatumUtovara between '" + datumPom + "' and '" + datumPom2 + "' " +
                "Union " +
                "Select RkShippingItemRec.ShippingItemRecID, RkShippingItemRec.ShipingIdz,  RkShipping.ShippingNo as BarkodIstovara, RkShipping.BrojUtovara as BrojUtovara,RkShipping.BrojIstovara as BrojIstovara, " +
                "RkShippingItemRec.SifraElementa as Element, Saloni.MestoIsporuke, RkShippingItemRec.SifraReklamacije, RkShippingItemRec.NazivReklamacije, RkShippingItemRec.KolicinaOrg, RkShippingItemRec.Zapisnik, " +
                "RkShippingItemRec.Paleta, RkShippingItemRec.Paket, RkShippingItemRec.BrojNalepnica, RkShippingItemRec.Tezina, RkShippingItemRec.LargoDimenzija, RkShippingItemRec.SifraReklamacijeLargo, " +
                "RkShippingItemRec.BrojKoleta  " +
                "FROM RkShippingItemRec " +
                "inner join RkShipping on  RkShipping.ShippingID = RkShippingItemRec.ShipingIdz  " +
                "inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID " +
                "inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke " +
                "where RkShipping.DatumUtovara between '" + datumPom + "' and '" + datumPom2 + "'";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.FromArgb(125, 158, 192);
            dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
        }
        private void FillGVStavke()
        {
            var query = "Select * from StavkePlanaPakovanjaRec Where IdPlanaPakovanja="+ID;
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.BorderStyle = BorderStyle.Fixed3D;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.FromArgb(125, 158, 192);
            dataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            InsertPakovanja ins = new InsertPakovanja();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    ins.InsertStavkePakovanjaRec(ID, Convert.ToInt32(row.Cells[0].Value), Convert.ToInt32(row.Cells[1].Value), row.Cells[5].Value.ToString().Trim(), row.Cells[6].Value.ToString().Trim(), row.Cells[7].Value.ToString().Trim(),
                        row.Cells[8].Value.ToString().Trim(), Convert.ToDecimal(row.Cells[9].Value.ToString()));
                }
            }
            FillGVStavke();
        }

        private void btnKonacan_Click(object sender, EventArgs e)
        {
            InsertPakovanja ins=new InsertPakovanja();
            ins.ProgalsiPlanPakovanja(ID);
            btnDodaj.Enabled = false;
        }

        private void btnIzvestaj_Click(object sender, EventArgs e)
        {
            PakovanjeReklamacijeIzvestaj frm = new PakovanjeReklamacijeIzvestaj(ID);
            frm.Show();
        }
    }
}
