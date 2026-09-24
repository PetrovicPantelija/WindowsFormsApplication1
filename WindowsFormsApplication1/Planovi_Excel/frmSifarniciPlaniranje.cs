using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.Neto_potrebe;


namespace WindowsFormsApplication1.Planovi_Excel
{
  
    public partial class frmSifarniciPlaniranje : Form
    {
        // Boje inspirisane Jela dekorima (Evergreen mat i Soft bež)
        private readonly Color BojaMenija = Color.FromArgb(58, 77, 57);
        private readonly Color BojaPozadine = Color.FromArgb(248, 247, 244);
        private readonly Color BojaAkcenta = Color.FromArgb(190, 150, 110);
        private readonly Color BojaTekstaSvetla = Color.White;

        // Varijable za pomeranje forme bez ivica
        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);

        // Glavni kontejner za dinamički sadržaj
        private Panel pnlMainContent;
        public frmSifarniciPlaniranje()
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

            // Dugme za minimizovanje (Custom -)
            Button btnMinimize = new Button
            {
                Text = "–",
                Size = new Size(45, 45),
                Location = new Point(pnlHeader.Width - 95, 7),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.Gray,
                Cursor = Cursors.Hand
            };
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.Click += (s, e) => this.WindowState = FormWindowState.Minimized;
            pnlHeader.Controls.Add(btnMinimize);

            pnlHeader.SizeChanged += (s, e) =>
            {
                btnClose.Location = new Point(pnlHeader.Width - 50, 7);
                btnMinimize.Location = new Point(pnlHeader.Width - 95, 7);
            };

            // 3. Levi Bočni Meni (Sidebar Panel)
            Panel pnlSidebar = new Panel
            {
                Width = 240,
                Dock = DockStyle.Left,
                BackColor = BojaMenija
            };

            // Kreiranje univerzalnih menija u petlji
            string[] stavkeMenija = { "Grupa", "Podgrupa", "Materijal 1", "Materijal 2", "Tehnologija 1", "Tehnologija 2" };
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
                Padding = new Padding(20)
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
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = BojaMenija,
                Location = new Point(20, 20),
                AutoSize = true
            };
            pnlEkran.Controls.Add(lblNaslov);

            // U zavisnosti od klika na meni ubacuje se specifična logika
            if (nazivModula == "Grupa")
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
                    Text = "Grupe",
                    Location = new Point(20, 80),
                    AutoSize = true,
                    ForeColor = System.Drawing.Color.Gray
                };
                pnlEkran.Controls.Add(lblInfo);
                LoadFormInPanel(new MdfGrupe(), pnlEkran);
            }
            else if (nazivModula == "Podgrupa")
            {
                // Ovde možete dodati logiku za Podgrupe
                Label lblInfo = new Label
                {
                    Text = "Podgrupe .",
                    Location = new Point(20, 80),
                    AutoSize = true,
                    ForeColor = System.Drawing.Color.Gray
                };
                pnlEkran.Controls.Add(lblInfo);
                LoadFormInPanel(new MdfPodgrupe(), pnlEkran);
            }
            else if (nazivModula == "Materijal 1")
            {
                // Ovde možete dodati logiku za Podgrupe
                Label lblInfo = new Label
                {
                    Text = "Materijal 1.",
                    Location = new Point(20, 80),
                    AutoSize = true,
                    ForeColor = System.Drawing.Color.Gray
                };
                pnlEkran.Controls.Add(lblInfo);
                LoadFormInPanel(new frmMaterijali1(), pnlEkran);
            }
            else if (nazivModula == "Materijal 2")
            {
                // Ovde možete dodati logiku za Podgrupe
                Label lblInfo = new Label
                {
                    Text = "Materijal 2.",
                    Location = new Point(20, 80),
                    AutoSize = true,
                    ForeColor = System.Drawing.Color.Gray
                };
                pnlEkran.Controls.Add(lblInfo);
                LoadFormInPanel(new frmMaterijali2(), pnlEkran);
            }
            else if (nazivModula == "Tehnologija 1")
            {
                // Ovde možete dodati logiku za Podgrupe
                Label lblInfo = new Label
                {
                    Text = "Tehnologija 1.",
                    Location = new Point(20, 80),
                    AutoSize = true,
                    ForeColor = System.Drawing.Color.Gray
                };
                pnlEkran.Controls.Add(lblInfo);
                LoadFormInPanel(new frmTehnologija1(), pnlEkran);
            }
            else if (nazivModula == "Tehnologija 2")
            {
                // Ovde možete dodati logiku za Podgrupe
                Label lblInfo = new Label
                {
                    Text = "Tehnologija 2.",
                    Location = new Point(20, 80),
                    AutoSize = true,
                    ForeColor = System.Drawing.Color.Gray
                };
                pnlEkran.Controls.Add(lblInfo);
                LoadFormInPanel(new frmTehnologija2(), pnlEkran);
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

        private void frmSifarniciPlaniranje_Load(object sender, EventArgs e)
        {

        }
    }
}
