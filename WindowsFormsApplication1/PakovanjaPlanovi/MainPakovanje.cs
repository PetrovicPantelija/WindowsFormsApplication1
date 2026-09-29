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
    public partial class MainPakovanje : Form
    {
        private readonly Color BojaMenija = Color.FromArgb(58, 77, 57);
        private readonly Color BojaPozadine = Color.FromArgb(248, 247, 244);
        private readonly Color BojaAkcenta = Color.FromArgb(190, 150, 110);
        private readonly Color BojaTekstaSvetla = Color.White;

        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);

        private Panel pnlMainContent;
        private Panel pnlSidebar;
        private Button btnMaximize;
        private Form aktivnaForma;

        string Korisnik; //Ovaj korisnik nije potreban
        bool status = false;
        public static string code = "MainPakovanje";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public MainPakovanje(string korisnik)
        {
            InitializeComponent();
            Korisnik = korisnik;
            InicijalizujUniverzalniDizajn();
            IdGrupe();
            IdForme();
            PravoPristupa();
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
        private void MainPakovanje_Load(object sender, EventArgs e)
        {

        }

        private (string Naziv, Func<Form> Kreiraj)[] StavkeMenija()
        {
            return new (string, Func<Form>)[]
            {
                ("Redovno pakovanje",    () => new NoviPlanPakovanja("RD")),
                ("Namensko pakovanje",   () => new NoviPlanPakovanja("NP")),
                ("Reklamacije",          () => new NoviPlanPakovanja("RK")),
                ("Paketi",               () => new KompletiPaket()),
                ("Pakovanje barkodovi",  () => new PakovanjeBarkod(Korisnik)),
                ("Pozicije",             () => new Magacin.frmPozicije()),
                ("Redosled pakovanja",   () => new frmRedosledPakovanja()),
                ("Reklamacije isporuka", () => new Pakovanja.ReklamacijePakovanja()),
            };
        }

        private void InicijalizujUniverzalniDizajn()
        {
            this.Size = new Size(1280, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = BojaPozadine;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            Panel pnlHeader = new Panel
            {
                Height = 60,
                Dock = DockStyle.Top,
                BackColor = Color.White
            };
            pnlHeader.MouseDown += Header_MouseDown;
            pnlHeader.MouseMove += Header_MouseMove;
            pnlHeader.MouseUp += Header_MouseUp;

            pnlHeader.DoubleClick += (s, e) => PromeniMaksimizaciju();

            Button btnMeniToggle = new Button
            {
                Text = "☰",
                Size = new Size(45, 45),
                Location = new Point(10, 7),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = BojaMenija,
                Cursor = Cursors.Hand
            };
            btnMeniToggle.FlatAppearance.BorderSize = 0;
            btnMeniToggle.Click += (s, e) => pnlSidebar.Visible = !pnlSidebar.Visible;
            pnlHeader.Controls.Add(btnMeniToggle);

            PictureBox picIkona = new PictureBox
            {
                Image = new Icon(this.Icon, 32, 32).ToBitmap(),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(32, 32),
                Location = new Point(62, 14)
            };
            pnlHeader.Controls.Add(picIkona);

            Label lblLogo = new Label
            {
                Text = "JELA Jagodina | Pakovanje",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = BojaMenija,
                AutoSize = true,
                Location = new Point(102, 18)
            };
            lblLogo.MouseDown += Header_MouseDown;
            lblLogo.MouseMove += Header_MouseMove;
            lblLogo.MouseUp += Header_MouseUp;
            lblLogo.DoubleClick += (s, e) => PromeniMaksimizaciju();
            pnlHeader.Controls.Add(lblLogo);

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
            // Close, ne Application.Exit: forma se otvara iz frmLogovanje koji mora ostati živ.
            btnClose.Click += (s, e) => this.Close();
            pnlHeader.Controls.Add(btnClose);

            Button btnMinimize = new Button
            {
                Text = "–",
                Size = new Size(45, 45),
                Location = new Point(pnlHeader.Width - 140, 7),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.Gray,
                Cursor = Cursors.Hand
            };
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.Click += (s, e) => this.WindowState = FormWindowState.Minimized;

            btnMaximize = new Button
            {
                Text = "□",
                Size = new Size(45, 45),
                Location = new Point(pnlHeader.Width - 95, 7),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.Gray,
                Cursor = Cursors.Hand
            };
            btnMaximize.FlatAppearance.BorderSize = 0;
            btnMaximize.Click += (s, e) => PromeniMaksimizaciju();
            pnlHeader.Controls.Add(btnMaximize);
            pnlHeader.Controls.Add(btnMinimize);

            pnlHeader.SizeChanged += (s, e) =>
            {
                btnClose.Location = new Point(pnlHeader.Width - 50, 7);
                btnMaximize.Location = new Point(pnlHeader.Width - 95, 7);
                btnMinimize.Location = new Point(pnlHeader.Width - 140, 7);
            };

            pnlSidebar = new Panel
            {
                Width = 240,
                Dock = DockStyle.Left,
                BackColor = BojaMenija
            };

            int topOffset = 30;

            foreach (var stavka in StavkeMenija())
            {
                Button btnMeni = new Button
                {
                    Text = " " + stavka.Naziv,
                    Size = new Size(240, 50),
                    Location = new Point(0, topOffset),
                    FlatStyle = FlatStyle.Flat,
                    ForeColor = BojaTekstaSvetla,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                    Cursor = Cursors.Hand
                };
                btnMeni.FlatAppearance.BorderSize = 0;
                btnMeni.FlatAppearance.MouseOverBackColor = BojaAkcenta;

                var kreiraj = stavka.Kreiraj;
                btnMeni.Click += (s, e) => LoadFormInPanel(kreiraj(), pnlMainContent);

                pnlSidebar.Controls.Add(btnMeni);
                topOffset += 55;
            }

            pnlMainContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = BojaPozadine,
                Padding = new Padding(20)
            };

            this.Controls.Add(pnlMainContent);
            this.Controls.Add(pnlSidebar);
            this.Controls.Add(pnlHeader);
        }

        private void LoadFormInPanel(Form formToLoad, Panel targetPanel)
        {
            if (aktivnaForma != null)
            {
                aktivnaForma.Close();
                aktivnaForma.Dispose();
            }
            targetPanel.Controls.Clear();

            formToLoad.TopLevel = false;
            formToLoad.FormBorderStyle = FormBorderStyle.None;
            formToLoad.Dock = DockStyle.Fill;

            targetPanel.Controls.Add(formToLoad);
            aktivnaForma = formToLoad;
            formToLoad.Show();
        }

        private void PromeniMaksimizaciju()
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                btnMaximize.Text = "□";
            }
            else
            {
                // Forma bez okvira bi se inače maksimizovala preko taskbara.
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.WindowState = FormWindowState.Maximized;
                btnMaximize.Text = "❐";
            }
        }

        private void Header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1 && this.WindowState != FormWindowState.Maximized)
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen borderPen = new Pen(Color.FromArgb(210, 205, 195), 2))
            {
                e.Graphics.DrawRectangle(borderPen, 1, 1, this.Width - 2, this.Height - 2);
            }
        }
    }
}
