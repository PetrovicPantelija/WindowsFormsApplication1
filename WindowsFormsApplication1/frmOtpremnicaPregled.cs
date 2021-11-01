using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace WindowsFormsApplication1
{
    public partial class frmOtpremnicaPregled : Form
    {
        public frmOtpremnicaPregled()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var select2 = " select t1.['Šif.Mp.'], t1.['Kod'], t1.['Ime MP'],  t1.['Stv.kol'], t1.['Proizvodnja'],  t1.['Otpremnica'], t1.['Neisporuceni']" +
" from ((select  PrSifMp['Šif.Mp.'], MpStaraSif['Kod'],  MpNaziv['Ime MP']," +
"  sum(PrPrevKol - PrIzdKol)['Stv.kol'], " +
" (select (CASE WHEN sum(DNpKolZaIzdelavo - DNpIzdelanaKol) is null THEN 0 ELSE sum(DNpKolZaIzdelavo - DNpIzdelanaKol) END) from DelNalogProizvodni   where DNpSifMp = PrSifMP AND DNpSifStat IN ('LA')) ['Proizvodnja'], " +
" (SELECT (CASE WHEN sum(DobavnicaPostav.DoPKOlOtpr) is null THEN 0 ELSE sum(DobavnicaPostav.DoPKOlOtpr) END) FROM DobavnicaPostav WHERE DoPSifra = PrSifMP      and DobavnicaPostav.DoPStDob = " + textBox1.Text + " ) ['Otpremnica'] " +
" (SELECT (CASE WHEN sum(NarociloPostav.NaPKolNar) is null THEN 0 ELSE sum(NarociloPostav.NaPKolNar) END) FROM Narocilo,NarociloPostav WHERE NaPStNar = NaStNar  AND NaPSifra = PrSifMP    AND NaStatus = 'PO'  and Narocilo.NaDatNar = CONVERT(VARCHAR(10),+ '" + DateTime.Now + "'  ,101)) ['Neisporuceni'] " +
" from Promet    join MaticniPodatki on MPSifra = PrSifMP where Promet.PrDatTrans <= + '" + DateTime.Now + "'" +  
" group by  PrSifMp, PrSifSklad, PrLokacija, MpStaraSif,  MpNaziv  )) t1 ";

            /*
             "        (select distinct LargoPakId from RkShippingItemPak   " +
      "        inner join RkShipping on RkShipping.ShippingID = RkShippingItemPak.ShipingIDz   " +
      "         where Cast(SUBSTRING(RkShipping.DatumIstovara,7,4) + '-' +  " +
      "         SUBSTRING(RkShipping.DatumIstovara,4,2) + '-' +  " +
      "         SUBSTRING(RkShipping.DatumIstovara,1,2) as DateTime) >   " +
       "         DATEADD(day, -2, CONVERT(VARCHAR(10),'" + dtpPredvidjeno.Text + "'  ,101)) ))   " +
             */

            textBox1.Text = select2;

            /*
             var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
             SqlConnection myConnection2 = new SqlConnection(s_connection2);
             var c2 = new SqlConnection(s_connection2);
             var dataAdapter2 = new SqlDataAdapter(select2, c2);

             var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
             var ds2 = new DataSet();
             dataAdapter2.Fill(ds2);
             dataGridView3.ReadOnly = true;
             dataGridView3.DataSource = ds2.Tables[0];
            */
        }
    }
}
