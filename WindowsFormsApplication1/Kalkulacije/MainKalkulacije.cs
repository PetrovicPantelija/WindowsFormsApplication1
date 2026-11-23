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

namespace WindowsFormsApplication1.Kalkulacije
{
    public partial class MainKalkulacije : Form
    {
        public bool PravoP;
        public static string code = "frmMainKalkulacije";
        public bool Pravo;
        // Jela izgled (kao Opsti cenovnik): tamni tekst, braon pri prelasku misem, zeleni naslovi
        static readonly Color MeniTekst = Color.FromArgb(40, 40, 40);
        static readonly Color MeniHover = Color.FromArgb(190, 150, 110);
        static readonly Color MeniNaslov = Color.FromArgb(58, 77, 57);
        public MainKalkulacije()
        {
            InitializeComponent();
            this.Focus();
            // ZAH-012: ista provera kao forme izracuna cena (KorisnikGrupa -> GrupaForma, parametri);
            // korisnik bez ijedne grupe dobija poruku, a ne SQL gresku "IdGrupe in ()"
            Pravo = new IzracunCena.PravaForme(code).Pravo;
            foreach (var l in new[] { label2, label3, label8, lblIcUvoz, lblIcOpsti, lblIcPriprema, lblIcCenovnici, lblIcProizvodi, lblIcTroskovni }) l.Cursor = Cursors.Hand;
        }
        private void label1_Click(object sender, EventArgs e)
        {
            Kalkulacije.frmKalkulacioniCenovnici kalk = new frmKalkulacioniCenovnici();
            PravoP = kalk.Pravo;
            if (PravoP == true) { kalk.Show(); } else { return; }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // ZAH-008: kolekcije izracuna cena (IC_Kolekcija), ne stara tabela Kolekcije
            var kol = new IzracunCena.frmICSifarnik(IzracunCena.VrstaSifarnika.Kolekcija);
            if (kol.Pravo) { kol.Show(); } else { kol.Dispose(); }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // ZAH-008: pripadnost kolekcijama izracuna cena (IC_KolekcijaPripadnost), ne stara KolekcijePripadnost
            IzracunCena.frmICPripadnost prip = new IzracunCena.frmICPripadnost();
            if (prip.Pravo) { prip.Show(); } else { prip.Dispose(); }
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Blue;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.White;
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = MeniHover;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = MeniTekst;
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = MeniHover;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = MeniTekst;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Kalkulacije.frmZaglavlja zag = new frmZaglavlja();
            zag.Show();

           // PravoP = zag.Pravo;
           // if (PravoP == true) { zag.Show(); } else { return; }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Kalkulacije.frmKalkulatorStavke kalk = new frmKalkulatorStavke(1);
            kalk.Show();

           // PravoP = kalk.Pravo;
           // if (PravoP == true) { kalk.Show(); } else { return; }
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Blue;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.White;
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Blue;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.ForeColor = Color.White;
        }
        private void btnSifarnici_MouseEnter(object sender, EventArgs e)
        {
            btnSifarnici.ForeColor = MeniHover;
            btnSifarnici.IconColor = MeniHover;
        }

        private void btn_Kalkulator_MouseEnter(object sender, EventArgs e)
        {
            btn_Kalkulator.ForeColor = Color.FromArgb(53, 54, 56);
            btn_Kalkulator.IconColor= Color.FromArgb(53, 54, 56);
        }

        private void btnSifarnici_MouseLeave(object sender, EventArgs e)
        {
            btnSifarnici.ForeColor = MeniNaslov;
            btnSifarnici.IconColor = MeniNaslov;
        }

        private void btn_Kalkulator_MouseLeave(object sender, EventArgs e)
        {
            btn_Kalkulator.ForeColor = Color.White;
            btn_Kalkulator.IconColor = Color.White;
        }
        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.ForeColor = Color.White;
        }
        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Blue;
        }
        private void label6_Click(object sender,EventArgs e)
        {
            Kalkulacije.frmPrimenaTroskovnogCenovnika tc = new frmPrimenaTroskovnogCenovnika();
            PravoP = tc.Pravo;
            if (PravoP == true) { tc.Show(); } else { return; }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Kalkulacije.frmCenaKamiona kam = new frmCenaKamiona();
            PravoP = kam.Pravo;
            if (PravoP == true) { kam.Show(); } else { return; }
        }
        private void label7_MouseLeave(object sender, EventArgs e)
        {
            label7.ForeColor = Color.White;
        }
        private void label7_MouseEnter(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Blue;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            frmBazaProdajnihCena cene = new frmBazaProdajnihCena();
            PravoP = cene.Pravo;
            if (PravoP == true) { cene.Show(); } else { return; }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            // ZAH-008: programi izracuna cena (IC_Program), ne stara tabela Program
            var program = new IzracunCena.frmICSifarnik(IzracunCena.VrstaSifarnika.Program);
            if (program.Pravo) { program.Show(); } else { program.Dispose(); }
        }

        private void label8_MouseEnter(object sender, EventArgs e)
        {
            label8.ForeColor = MeniHover;
        }

        private void label8_MouseLeave(object sender, EventArgs e)
        {
            label8.ForeColor = MeniTekst;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            frmPoredjenjeKalkulatora program = new frmPoredjenjeKalkulatora();
            program.Show();
        }

        // ZAH-008 Izracun cena
        private void lblIcUvoz_Click(object sender, EventArgs e)
        {
            IzracunCena.frmUvozExcel uvoz = new IzracunCena.frmUvozExcel();
            if (uvoz.Pravo) { uvoz.Show(); } else { uvoz.Dispose(); }
        }

        private void lblIcOpsti_Click(object sender, EventArgs e)
        {
            IzracunCena.frmOpstiCenovnik opsti = new IzracunCena.frmOpstiCenovnik();
            if (opsti.Pravo) { opsti.Show(); } else { opsti.Dispose(); }
        }

        private void lblIcPriprema_Click(object sender, EventArgs e)
        {
            IzracunCena.frmPripremaCenovnika priprema = new IzracunCena.frmPripremaCenovnika();
            if (priprema.Pravo) { priprema.Show(); } else { priprema.Dispose(); }
        }

        private void lblIcCenovnici_Click(object sender, EventArgs e)
        {
            IzracunCena.frmCenovnici cenovnici = new IzracunCena.frmCenovnici();
            if (cenovnici.Pravo) { cenovnici.Show(); } else { cenovnici.Dispose(); }
        }

        private void lblIcProizvodi_Click(object sender, EventArgs e)
        {
            // jela_products: unos, izmena, slika, uvoz iz CSV
            IzracunCena.frmICProizvodi proizvodi = new IzracunCena.frmICProizvodi();
            if (proizvodi.Pravo) { proizvodi.Show(); } else { proizvodi.Dispose(); }
        }

        private void lblIcTroskovni_Click(object sender, EventArgs e)
        {
            // ZAH-011: ista forma kao dugme "Troskovni obracun" u Kalkulaciji stavki
            frmTroskoviIzracun ti = new frmTroskoviIzracun();
            ti.Show();
        }

        private void lblMeni_MouseEnter(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = MeniHover;
        }

        private void lblMeni_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = MeniTekst;
        }
    }
}
