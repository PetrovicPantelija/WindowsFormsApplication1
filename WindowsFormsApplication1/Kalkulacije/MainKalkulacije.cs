using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Kalkulacije
{
    public partial class MainKalkulacije : Form
    {
        public MainKalkulacije()
        {
            InitializeComponent();
            this.Focus();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Kalkulacije.frmKalkulacioniCenovnici kalk = new frmKalkulacioniCenovnici();
            kalk.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Kalkulacije.frmKolekcije kol = new frmKolekcije();
            kol.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Kalkulacije.frmKolekcijePripadnost kolP = new frmKolekcijePripadnost();
            kolP.Show();
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
            label2.ForeColor = Color.Blue;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.White;
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Blue;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.White;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Kalkulacije.frmZaglavlja zag = new frmZaglavlja();
            zag.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Kalkulacije.frmKalkulatorStavke kalk = new frmKalkulatorStavke();
            kalk.Show();
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
            btnSifarnici.ForeColor = Color.FromArgb(53, 54, 56);
            btnSifarnici.IconColor= Color.FromArgb(53, 54, 56);
        }

        private void btn_Kalkulator_MouseEnter(object sender, EventArgs e)
        {
            btn_Kalkulator.ForeColor = Color.FromArgb(53, 54, 56);
            btn_Kalkulator.IconColor= Color.FromArgb(53, 54, 56);
        }

        private void btnSifarnici_MouseLeave(object sender, EventArgs e)
        {
            btnSifarnici.ForeColor = Color.White;
            btnSifarnici.IconColor = Color.White;
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
            tc.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Kalkulacije.frmCenaKamiona kam = new frmCenaKamiona();
            kam.Show();
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
            cene.Show();
        }
    }
}
