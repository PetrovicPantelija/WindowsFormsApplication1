using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace WindowsFormsApplication1
{
    public partial class MainNabavka : Form
    {
        private IconButton curentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;

        string KorisnikM = "";
        int ReadNabavkaM = 0;
        public bool PravoP;
        string niz = "";

        public static string code = "MainNabavka";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        public MainNabavka()
        {
            InitializeComponent();

            IdGrupe();
            IdForme();
            PravoPristupa();

            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 50);
            PanelMenu.Controls.Add(leftBorderBtn);
            //form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
        public string IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            //Sifarnici.frmLogovanje frm = new Sifarnici.frmLogovanje();         
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            int count = 0;

            while (dr.Read())
            {
                if (count == 0)
                {
                    niz = dr["IdGrupe"].ToString();
                    count++;
                }
                else
                {
                    niz = niz + "," + dr["IdGrupe"].ToString();
                    count++;
                }

            }
            conn.Close();
            return niz;
        }
        private int IdForme()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select IdForme from Forme where Rtrim(Code)=" + "'" + code + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idForme = Convert.ToInt32(dr["IdForme"].ToString());
            }
            conn.Close();
            return idForme;
        }

        private void PravoPristupa()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select * From GrupaForma Where IdGrupe in (" + niz + ") and IdForme=" + idForme;
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nemate prava za pristup ovoj formi", code);
                Pravo = false;
            }
            else
            {
                Pravo = true;
                while (reader.Read())
                {
                    insert = Convert.ToBoolean(reader["Upis"]);
                    if (insert == false)
                    {
                        //tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                        //tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                        //tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
        }
        //Struktura za boje
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(250, 166, 27);
            public static Color color2 = Color.FromArgb(250, 166, 27);
            public static Color color3 = Color.FromArgb(250, 166, 27);
            public static Color color4 = Color.FromArgb(250, 166, 27);
            public static Color color5 = Color.FromArgb(250, 166, 27);
            public static Color color6 = Color.FromArgb(250, 166, 27);
            public static Color color7 = Color.FromArgb(250, 166, 27);
            public static Color color8 = Color.FromArgb(250, 166, 27);
        }

        //metoda
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //button
                curentBtn = (IconButton)senderBtn;
                curentBtn.BackColor = Color.FromArgb(82, 108, 120);
                curentBtn.ForeColor = color;
                curentBtn.TextAlign = ContentAlignment.MiddleCenter;
                curentBtn.IconColor = color;
                curentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                curentBtn.ImageAlign = ContentAlignment.MiddleCenter;
                //left border btn
                //leftBorderBtn.BackColor = color;
                // leftBorderBtn.Location = new Point(0, curentBtn.Location.Y);
                // leftBorderBtn.Visible = true;
                // leftBorderBtn.BringToFront();
                //Icon curent child form
                //iconCurentChildForm.IconChar = curentBtn.IconChar;
                //iconCurentChildForm.IconColor = color;
            }
        }

        private void DisableButton()
        {
            if (curentBtn != null)
            {
                curentBtn.BackColor = Color.FromArgb(69, 90, 100);
                curentBtn.ForeColor = Color.White;
                curentBtn.TextAlign = ContentAlignment.MiddleLeft;
                curentBtn.IconColor = Color.White;
                curentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                curentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                //otvaranje samo jedne forme
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            //lblTitlechildForm.Text = childForm.Text;
        }


        public MainNabavka(string KorisnikN, int ReadNabavka)
        {
            KorisnikM = KorisnikN;
            ReadNabavka = ReadNabavkaM;
            InitializeComponent();
        }

        private void međuskladišniPrenosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMedjuskaldisniPrenosi frmM = new frmMedjuskaldisniPrenosi();
            PravoP = frmM.Pravo;
            if (PravoP == true) { frmM.Show(); } else { return; }
        }

        private void rkShippingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrijemnica frmP = new frmPrijemnica();
            PravoP = frmP.Pravo;
            if (PravoP == true) { frmP.Show(); } else { return; }
        }

        private void pregledZalihaToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void pregledZalihaFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPregledZalihaFilter frpzf = new frmPregledZalihaFilter();
            PravoP = frpzf.Pravo;
            if (PravoP == true) { frpzf.Show(); } else { return; }
        }

        private void međuskladišniPrenosFormiranjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Magacin.frmPozicije poz = new Magacin.frmPozicije();
            PravoP = poz.Pravo;
            if (PravoP == true) { poz.Show(); } else { return; }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Magacin.frmPaleta paleta = new Magacin.frmPaleta();
            PravoP = paleta.Pravo;
            if (PravoP == true) { paleta.Show(); } else { return; }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Magacin.frmMaticniPodatak maticni = new Magacin.frmMaticniPodatak();
            PravoP = maticni.Pravo;
            if (PravoP == true) { maticni.Show(); } else { return; }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            frmMedjuskaldisniPrenosi frmM = new frmMedjuskaldisniPrenosi();
            PravoP = frmM.Pravo;
            if (PravoP == true) { frmM.Show(); } else { return; }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            frmPrebacivanjeMedjuskladisniPrenos mprp = new frmPrebacivanjeMedjuskladisniPrenos();
            PravoP = mprp.Pravo;
            if (PravoP == true) { mprp.Show(); } else { return; }
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Magacin.frmStampaPozicije poz = new Magacin.frmStampaPozicije();
            PravoP = poz.Pravo;
            if (PravoP == true) { poz.Show(); } else { return; }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            Magacin.frmPlanMedjuskladisni plan = new Magacin.frmPlanMedjuskladisni();
            PravoP = plan.Pravo;
            if (PravoP == true) { plan.Show(); } else { return; }
        }

        private void direktnaIzdavanjaTrebovanjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPregledIzdavanja izd = new Magacin.frmPregledIzdavanja();
            PravoP = izd.Pravo;
            if (PravoP == true) { izd.Show(); } else { return; }
        }

        private void direktniPrijemiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPregledPrijemi pre = new Magacin.frmPregledPrijemi();
            PravoP = pre.Pravo;
            if (PravoP == true) { pre.Show(); } else { return; }
        }

        private void pregledStanjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPrijavaStanja prijstan = new Magacin.frmPrijavaStanja();
            PravoP = prijstan.Pravo;
            if (PravoP == true) { prijstan.Show(); } else { return; }
        }

        private void pregledRadnihNalogaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPregledRadnihNaloga radninalozi = new Magacin.frmPregledRadnihNaloga();
            PravoP = radninalozi.Pravo;
            if (PravoP == true) { radninalozi.Show(); } else { return; }
        }

        private void uvozIObradaPlanaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Ulaz u Magacin
            Magacin.frmPlanImport planimport = new Magacin.frmPlanImport(KorisnikM, ReadNabavkaM);
            PravoP = planimport.Pravo;
            if (PravoP == true) { planimport.Show(); } else { return; }
        }

        private void uvozNetoPotrebaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ///Ulaz u magacin
            Magacin.frmPlanImportNeto planimpportneto = new Magacin.frmPlanImportNeto(KorisnikM, ReadNabavkaM);
            PravoP = planimpportneto.Pravo;
            if (PravoP == true) { planimpportneto.Show(); } else { return; }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void formiranjeMeđuskladišnogPrenosaIzRadnogNalogaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrebacivanjeMeđPrenosaRN medRN = new frmPrebacivanjeMeđPrenosaRN(KorisnikM);
            PravoP = medRN.Pravo;
            if (PravoP == true) { medRN.Show(); } else { return; }
        }

        private void magacinTrenutnaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmMagacinTrenutna mt = new Magacin.frmMagacinTrenutna();
            PravoP = mt.Pravo;
            if (PravoP == true) { mt.Show(); } else { return; }
        }

        private void mSPPrijemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmMagacinTrenutnaIzPrijema mip = new Magacin.frmMagacinTrenutnaIzPrijema(KorisnikM);
            PravoP = mip.Pravo;
            if (PravoP == true) { mip.Show(); } else { return; }
        }

        private void promenaKorisnikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPromenaKorisnika move = new Magacin.frmPromenaKorisnika();
            PravoP = move.Pravo;
            if (PravoP == true) { move.Show(); } else { return; }
        }

        private void formiranjMSPIzRNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrebacivanjeMeđPrenosaRN medRN = new frmPrebacivanjeMeđPrenosaRN(KorisnikM);
            PravoP = medRN.Pravo;
            if (PravoP == true) { medRN.Show(); } else { return; }
        }

        private void pregledZalihaUporedniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPregledZaliha pz = new frmPregledZaliha();
            PravoP = pz.Pravo;
            if (PravoP == true) { pz.Show(); } else { return; }
        }

        private void međuskladišniPrenosFormiranjeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmPrebacivanjeMedjuskladisniPrenos mprp = new frmPrebacivanjeMedjuskladisniPrenos();
            PravoP = mprp.Pravo;
            if (PravoP == true) { mprp.Show(); } else { return; }
        }

        private void trebovanjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPregledTrebovanja treb = new Magacin.frmPregledTrebovanja();
            PravoP = treb.Pravo;
            if (PravoP == true) { treb.Show(); } else { return; }
        }

        private void trebovanjaReversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmMagacinTrenutnaTrebovanje fMt = new Magacin.frmMagacinTrenutnaTrebovanje();
            PravoP = fMt.Pravo;
            if (PravoP == true) { fMt.Show(); } else { return; }
        }

        private void btnMagacin_Click(object sender, EventArgs e)
        {
            MenijiNabavka.frmMagacinNabavka m = new MenijiNabavka.frmMagacinNabavka();
            PravoP = m.Pravo;
            if (PravoP == true)
            {
                ActivateButton(sender, RGBColors.color1);
                OpenChildForm(new MenijiNabavka.frmMagacinNabavka());
            }
            else { return; }
        }

        private void btnNabavka_Click(object sender, EventArgs e)
        {
            MenijiNabavka.Nabavka n = new MenijiNabavka.Nabavka();
            PravoP = n.Pravo;
            if (PravoP == true)
            {
                ActivateButton(sender, RGBColors.color1);
                OpenChildForm(new MenijiNabavka.Nabavka());
            }
            else { return; }
        }

        private void btnPlan_Click(object sender, EventArgs e)
        {
            MenijiNabavka.frmPlan p = new MenijiNabavka.frmPlan();
            PravoP = p.Pravo;
            if (PravoP == true)
            {
                ActivateButton(sender, RGBColors.color1);
                OpenChildForm(new MenijiNabavka.frmPlan());
            }
            else { return; }
        }
    }
}
