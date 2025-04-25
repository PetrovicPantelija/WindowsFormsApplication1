using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
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

namespace WindowsFormsApplication1.ProizvodnjaUKadrovskoj
{
    public partial class MainZaposleni: Form
    {
        int sifraLargo;
        public MainZaposleni(int sifraLargo)
        {
            this.sifraLargo = sifraLargo;
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            ProizvodnjaUKadrovskoj.Sektor kol = new ProizvodnjaUKadrovskoj.Sektor();
            kol.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            ProizvodnjaUKadrovskoj.frmTipRada tr = new ProizvodnjaUKadrovskoj.frmTipRada();
            tr.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            ProizvodnjaUKadrovskoj.frmKadroviSektora tr = new ProizvodnjaUKadrovskoj.frmKadroviSektora();
            tr.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            ProizvodnjaUKadrovskoj.frmRadnoMesto tr = new ProizvodnjaUKadrovskoj.frmRadnoMesto();
            tr.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            ProizvodnjaUKadrovskoj.frmKadroviRadnogMesta tr = new ProizvodnjaUKadrovskoj.frmKadroviRadnogMesta();
            tr.Show();
        }

        private void lblEvidencijaRada_Click(object sender, EventArgs e)
        {
            //user = cboKorisnik.Text.ToString().TrimEnd();
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            SqlCommand command = new SqlCommand("SELECT * " +
                                                "FROM Korisnici k " +
                                                "INNER JOIN KadroviRadnihMesta krm ON k.SifraLargo = krm.LargoBroj " +
                                                "INNER JOIN RadnaMesta rm ON krm.IDRadnoMesto = rm.ID " +
                                                "WHERE LTRIM(RTRIM(rm.Naziv)) = 'Vođa sektora'  AND k.SifraLargo =" + sifraLargo, myConnection);
            myConnection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Samo korisnik sa radnim mestom Vođa sektora ima pravo za pristup");
                return;
            }
            reader.Close();
            myConnection.Close();
            ProizvodnjaUKadrovskoj.frmEvidencijaRada tr = new ProizvodnjaUKadrovskoj.frmEvidencijaRada(sifraLargo);
            tr.Show();
        }

        private void lblPodaciOZaposlenima_Click(object sender, EventArgs e)
        {
            ProizvodnjaUKadrovskoj.frmPodaciOZaposlenima pz = new ProizvodnjaUKadrovskoj.frmPodaciOZaposlenima();
            pz.Show();
        }

        private void lblPodaciSaKoefijentom_Click(object sender, EventArgs e)
        {
            ProizvodnjaUKadrovskoj.frmPodaciSaKoeficijentima pz = new ProizvodnjaUKadrovskoj.frmPodaciSaKoeficijentima();
            pz.Show();
        }
    }
}
