using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // pregled jednog cenovnika, samo citanje (vazeci ili bilo koji drugi)
    public class frmCenovnikPregled : Form
    {
        readonly ucCenovnik uc = new ucCenovnik();

        public frmCenovnikPregled(int cenovnikID)
        {
            Text = "Cenovnik";
            Size = new Size(1300, 760);
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            Font = Izgled.Slova;
            uc.Dock = DockStyle.Fill;
            var info = new Label { Dock = DockStyle.Top, Height = 30, BackColor = Izgled.Krem, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(10, 0, 0, 0) };
            Controls.Add(uc);
            Controls.Add(info);
            try
            {
                uc.Ucitaj(cenovnikID, RezimCenovnika.Citanje);
                var z = uc.Zaglavlje;
                Text = "Cenovnik - " + z["Naziv"];
                info.Text = z["Naziv"] + "   ·   " + z["Valuta"] + "   ·   " + z["OpstiIliPartner"]
                    + (z["VaziOd"] == DBNull.Value ? "" : "   ·   važi od " + Convert.ToDateTime(z["VaziOd"]).ToString("dd.MM.yyyy")
                    + (z["VaziDo"] == DBNull.Value ? " do daljnjeg" : " do " + Convert.ToDateTime(z["VaziDo"]).ToString("dd.MM.yyyy")))
                    + "   ·   kurs " + Convert.ToDecimal(z["Kurs"]).ToString("N2") + "   ·   " + uc.Stavke.Count + " redova";
            }
            catch (Exception ex) { Izgled.Greska(this, "Učitavanje", ex); }
        }
    }
}
