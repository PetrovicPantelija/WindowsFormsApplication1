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
using WindowsFormsApplication1.Barcode;
using WindowsFormsApplication1.Neto_potrebe;

namespace WindowsFormsApplication1.Magacin
{
    public partial class MainAdministracija : Form
    {
        public static string code = "MainAdministracija";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public bool PravoP;

        private readonly Color BojaMenija = Color.FromArgb(58, 77, 57);
        private readonly Color BojaPozadine = Color.FromArgb(248, 247, 244);
        private readonly Color BojaAkcenta = Color.FromArgb(190, 150, 110);
        private readonly Color BojaTekstaSvetla = Color.White;

        // Varijable za pomeranje forme bez ivica
        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);

        // Glavni kontejner za dinamički sadržaj
        private Panel pnlMainContent;


        public MainAdministracija()
        {
            InitializeComponent();
            InicijalizujUniverzalniDizajn();
          
        }

        private void InicijalizujUniverzalniDizajn()
        {
            // 1. Osnovna podešavanja prozora
            this.Size = new Size(1280, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = BojaPozadine;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            // 2. Gornja traka (Header Panel)
            Panel pnlHeader = new Panel
            {
                Height = 60,
                Dock = DockStyle.Top,
                BackColor = Color.White
            };
            pnlHeader.MouseDown += Header_MouseDown;
            pnlHeader.MouseMove += Header_MouseMove;
            pnlHeader.MouseUp += Header_MouseUp;

            // Naslov / Logotip unutar gornje trake
            Label lblLogo = new Label
            {
                Text = "JELA Jagodina | Informacioni Sistem",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = BojaMenija,
                AutoSize = true,
                Location = new Point(20, 18)
            };
            pnlHeader.Controls.Add(lblLogo);

            // Dugme za zatvaranje (Custom X)
            Button btnClose = new Button
            {
                Text = "✕",
                Size = new Size(45, 45),
                Location = new Point(pnlHeader.Width - 50, 7),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.Gray,
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => Application.Exit();
            pnlHeader.Controls.Add(btnClose);
            pnlHeader.SizeChanged += (s, e) => btnClose.Location = new Point(pnlHeader.Width - 50, 7);

            // 3. Levi Bočni Meni (Sidebar Panel)
            Panel pnlSidebar = new Panel
            {
                Width = 240,
                Dock = DockStyle.Left,
                BackColor = BojaMenija
            };

            // Kreiranje univerzalnih menija u petlji
            string[] stavkeMenija = { "Organizacione jedinice", "Korisnici", "Prava - Grupe", "Prava - forme", "Dodeli grupu", "Dodeli pravo", "Skladiste pravo", "UvozWebaplikacija" };
            int topOffset = 30;

            foreach (string stavka in stavkeMenija)
            {
                Button btnMeni = new Button
                {
                    Text = " " + stavka,
                    Size = new Size(240, 50),
                    Location = new Point(0, topOffset),
                    FlatStyle = FlatStyle.Flat,
                    ForeColor = BojaTekstaSvetla,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                    Cursor = Cursors.Hand
                };
                btnMeni.FlatAppearance.BorderSize = 0;
                btnMeni.FlatAppearance.MouseOverBackColor = BojaAkcenta; // Promena boje na prelet

                // Događaj na klik za dinamičko učitavanje pod-ekrana
                btnMeni.Click += (s, e) => UcitajPodEkran(btnMeni.Text.Trim());

                pnlSidebar.Controls.Add(btnMeni);
                topOffset += 55;
            }

            // 4. Centralni radni prostor (Glavni panel)
            pnlMainContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = BojaPozadine,
                Padding = new Padding(10)
            };

            // Dodavanje panela na glavnu formu
            this.Controls.Add(pnlMainContent);
            this.Controls.Add(pnlSidebar);
            this.Controls.Add(pnlHeader);
        }

        private void LoadFormInPanel(Form formToLoad, Panel targetPanel)
        {
            if (formToLoad == null || targetPanel == null)
                throw new ArgumentNullException("Forma ili panel nisu definisani.");

            // Očisti prethodni sadržaj panela
            targetPanel.Controls.Clear();

            // Podesi formu da se ponaša kao kontrola
            formToLoad.TopLevel = false;
            formToLoad.FormBorderStyle = FormBorderStyle.None;
            formToLoad.Dock = DockStyle.Fill;

            // Dodaj formu u panel
            targetPanel.Controls.Add(formToLoad);
            formToLoad.Show();
        }

        // Metoda koja omogućava dinamičku zamenu ekrana bez otvaranja novih formi
        private void UcitajPodEkran(string nazivModula)
        {
            pnlMainContent.Controls.Clear();

            // Univerzalni kontejner ekrana (Simulacija UserControl-a)
            Panel pnlEkran = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            // Naslov ekrana koji je učitan
            Label lblNaslov = new Label
            {
                Text = nazivModula.ToUpper(),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = BojaMenija,
                Location = new Point(20, 20),
                AutoSize = true
            };
            pnlEkran.Controls.Add(lblNaslov);

            // U zavisnosti od klika na meni ubacuje se specifična logika
            //  "Organizacione jedinice", "korisnici", "Prava - Grupe", "Prava - forme", "Dodeli grupu", "Dodeli pravo", "Skladiste pravo"
            if (nazivModula == "Organizacione jedinice")
            {
                /*
                // Primer tabele (DataGridView) za nameštaj
                DataGridView dgv = new DataGridView
                {
                    Location = new Point(20, 70),
                    Size = new Size(960, 550),
                    BorderStyle = BorderStyle.None,
                    BackgroundColor = Color.White,
                    EnableHeadersVisualStyles = false
                };
                dgv.ColumnHeadersDefaultCellStyle.BackColor = BojaMenija;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                pnlEkran.Controls.Add(dgv);
                */
                Label lblInfo = new Label
                {
                    Text = "Organizacione jedinice",
                    Location = new Point(20, 80),
                    AutoSize = true,
                    ForeColor = System.Drawing.Color.Gray
                };
                pnlEkran.Controls.Add(lblInfo);
                LoadFormInPanel(new frmOrganizacionaJedinica(), pnlEkran);
                
            }
            else if (nazivModula == "Korisnici")
            {
                // Ovde možete dodati logiku za Podgrupe
                Label lblInfo = new Label
                {
                    Text = "Korisnici",
                    Location = new Point(20, 80),
                    AutoSize = true,
                    ForeColor = System.Drawing.Color.Gray
                };
                pnlEkran.Controls.Add(lblInfo);
                LoadFormInPanel(new frmKorisnici(), pnlEkran);
               // "Prava - forme", "Dodeli grupu", "Dodeli pravo", "Skladiste pravo"
            }
            else if (nazivModula == "Prava - Grupe")
            {
                // Ovde možete dodati logiku za Podgrupe
                Label lblInfo = new Label
                {
                    Text = "Prava - Grupe",
                    Location = new Point(20, 80),
                    AutoSize = true,
                    ForeColor = System.Drawing.Color.Gray
                };
                pnlEkran.Controls.Add(lblInfo);
                LoadFormInPanel(new Administracija___Prava_pristupa.frmGrupe(), pnlEkran);
            }
            else if (nazivModula == "Prava - forme")
            {
                // Ovde možete dodati logiku za Podgrupe
                Label lblInfo = new Label
                {
                    Text = "Prava - forme",
                    Location = new Point(20, 80),
                    AutoSize = true,
                    ForeColor = System.Drawing.Color.Gray
                };
                pnlEkran.Controls.Add(lblInfo);
                LoadFormInPanel(new Administracija___Prava_pristupa.frmForme(), pnlEkran);
            }
            else if (nazivModula == "Dodeli grupu")
            {
                // Ovde možete dodati logiku za Podgrupe
                Label lblInfo = new Label
                {
                    Text = "Dodeli grupu",
                    Location = new Point(20, 80),
                    AutoSize = true,
                    ForeColor = System.Drawing.Color.Gray
                };
                pnlEkran.Controls.Add(lblInfo);
                LoadFormInPanel(new Administracija___Prava_pristupa.frmDodeliGrupu(), pnlEkran);
            }
            else if (nazivModula == "Dodeli pravo")
            {
                // Ovde možete dodati logiku za Podgrupe
                Label lblInfo = new Label
                {
                    Text = "Dodeli pravo",
                    Location = new Point(20, 80),
                    AutoSize = true,
                    ForeColor = System.Drawing.Color.Gray
                };
                pnlEkran.Controls.Add(lblInfo);
                LoadFormInPanel(new Administracija___Prava_pristupa.frmDodeliPravo(), pnlEkran);
            }
            else if (nazivModula == "Skladiste pravo")
            {
                // Ovde možete dodati logiku za Podgrupe
                Label lblInfo = new Label
                {
                    Text = "Skladiste pravo",
                    Location = new Point(20, 80),
                    AutoSize = true,
                    ForeColor = System.Drawing.Color.Gray
                };
                pnlEkran.Controls.Add(lblInfo);
                LoadFormInPanel(new SkladistePrava(), pnlEkran);
            }
            else if (nazivModula == "UvozWebaplikacija")
            {
                // ZAH-005: forma za uvoz iz web aplikacije - povezati kada bude napravljena
                Label lblInfo = new Label
                {
                    Text = "Uvoz iz web aplikacije - u izradi",
                    Location = new Point(20, 80),
                    AutoSize = true,
                    ForeColor = System.Drawing.Color.Gray
                };
                pnlEkran.Controls.Add(lblInfo);
            }

            else
            {
                // Za ostale module prikazujemo placeholder obaveštenje
                Label lblInfo = new Label
                {
                    Text = $"Modul '{nazivModula}' je uspešno aktiviran unutar univerzalnog interfejsa.",
                    Location = new Point(20, 80),
                    AutoSize = true,
                    ForeColor = Color.Gray
                };
                pnlEkran.Controls.Add(lblInfo);
            }

            pnlMainContent.Controls.Add(pnlEkran);
        }

        // --- Logika za pomeranje forme (Drag & Drop) ---
        private void Header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                startPoint = new Point(e.X, e.Y);
            }
        }

        private void Header_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y);
            }
        }

        private void Header_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        // Iscrtavanje elegantne spoljne ivice forme
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen borderPen = new Pen(Color.FromArgb(210, 205, 195), 2))
            {
                e.Graphics.DrawRectangle(borderPen, 1, 1, this.Width - 2, this.Height - 2);
            }
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
        private void rkShippingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOrganizacionaJedinica orgjed = new frmOrganizacionaJedinica();
            PravoP = orgjed.Pravo;
            if (PravoP == true) { orgjed.Show(); } else { return; }
        }

        private void korisniciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKorisnici korisnici = new frmKorisnici();
            PravoP = korisnici.Pravo;
            if (PravoP == true){ korisnici.Show(); } else { return; }
        }

        private void grupeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Administracija___Prava_pristupa.frmGrupe grupe= new Administracija___Prava_pristupa.frmGrupe();
            PravoP = grupe.Pravo;
            if (PravoP == true) { grupe.Show(); } else { return; }
        }

        private void formeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Administracija___Prava_pristupa.frmForme forme = new Administracija___Prava_pristupa.frmForme();
            PravoP = forme.Pravo;
            if (PravoP == true) { forme.Show(); } else { return; }
        }

        private void dodeliGrupuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Administracija___Prava_pristupa.frmDodeliGrupu grupe = new Administracija___Prava_pristupa.frmDodeliGrupu();
            PravoP = grupe.Pravo;
            if (PravoP == true) { grupe.Show(); } else { return; }
        }

        private void dodeliFormuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Administracija___Prava_pristupa.frmDodeliPravo pravo = new Administracija___Prava_pristupa.frmDodeliPravo();
            PravoP = pravo.Pravo;
            if (PravoP == true) { pravo.Show(); } else { return; }
        }

        private void skladistaPravaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SkladistePrava sp = new SkladistePrava();
            sp.Show();
        }

        private void MainAdministracija_Load(object sender, EventArgs e)
        {
                
        }

        private void wEBSPodaciToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
