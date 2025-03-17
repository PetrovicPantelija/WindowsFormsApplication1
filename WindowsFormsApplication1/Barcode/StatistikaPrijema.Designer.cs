namespace WindowsFormsApplication1.Barcode
{
    partial class StatistikaPrijema
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblKoristnikOdbacio = new System.Windows.Forms.Label();
            this.lblPrijemnica = new System.Windows.Forms.Label();
            this.lblOdbacio = new System.Windows.Forms.Label();
            this.lblKreirao = new System.Windows.Forms.Label();
            this.lblVremePrijema = new System.Windows.Forms.Label();
            this.lblKreiranje = new System.Windows.Forms.Label();
            this.lblKrajPrijema = new System.Windows.Forms.Label();
            this.lblPocetakPrijema = new System.Windows.Forms.Label();
            this.lblMagacioner = new System.Windows.Forms.Label();
            this.lblStavke = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.lblPocetakUzimanja = new System.Windows.Forms.Label();
            this.lblKrajUlaganja = new System.Windows.Forms.Label();
            this.lblVremeUzUl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(440, 402);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.lblKoristnikOdbacio);
            this.panel2.Controls.Add(this.lblPrijemnica);
            this.panel2.Controls.Add(this.lblOdbacio);
            this.panel2.Controls.Add(this.lblKreirao);
            this.panel2.Controls.Add(this.lblVremeUzUl);
            this.panel2.Controls.Add(this.lblKrajUlaganja);
            this.panel2.Controls.Add(this.lblPocetakUzimanja);
            this.panel2.Controls.Add(this.lblVremePrijema);
            this.panel2.Controls.Add(this.lblKreiranje);
            this.panel2.Controls.Add(this.lblKrajPrijema);
            this.panel2.Controls.Add(this.lblPocetakPrijema);
            this.panel2.Controls.Add(this.lblMagacioner);
            this.panel2.Controls.Add(this.lblStavke);
            this.panel2.Location = new System.Drawing.Point(458, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(383, 402);
            this.panel2.TabIndex = 2;
            this.panel2.Click += new System.EventHandler(this.panel2_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(182, 358);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 37);
            this.button2.TabIndex = 2;
            this.button2.Text = "Ulaganje";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(59, 358);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 37);
            this.button1.TabIndex = 2;
            this.button1.Text = "Uzimanje";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblKoristnikOdbacio
            // 
            this.lblKoristnikOdbacio.AutoSize = true;
            this.lblKoristnikOdbacio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKoristnikOdbacio.Location = new System.Drawing.Point(146, 309);
            this.lblKoristnikOdbacio.Name = "lblKoristnikOdbacio";
            this.lblKoristnikOdbacio.Size = new System.Drawing.Size(126, 16);
            this.lblKoristnikOdbacio.TabIndex = 1;
            this.lblKoristnikOdbacio.Text = "Korisnik ponovio:";
            // 
            // lblPrijemnica
            // 
            this.lblPrijemnica.AutoSize = true;
            this.lblPrijemnica.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrijemnica.Location = new System.Drawing.Point(3, 11);
            this.lblPrijemnica.Name = "lblPrijemnica";
            this.lblPrijemnica.Size = new System.Drawing.Size(84, 16);
            this.lblPrijemnica.TabIndex = 0;
            this.lblPrijemnica.Text = "Prijemnica:";
            // 
            // lblOdbacio
            // 
            this.lblOdbacio.AutoSize = true;
            this.lblOdbacio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOdbacio.Location = new System.Drawing.Point(3, 309);
            this.lblOdbacio.Name = "lblOdbacio";
            this.lblOdbacio.Size = new System.Drawing.Size(89, 16);
            this.lblOdbacio.TabIndex = 1;
            this.lblOdbacio.Text = "Ponovljena:";
            // 
            // lblKreirao
            // 
            this.lblKreirao.AutoSize = true;
            this.lblKreirao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKreirao.Location = new System.Drawing.Point(211, 11);
            this.lblKreirao.Name = "lblKreirao";
            this.lblKreirao.Size = new System.Drawing.Size(61, 16);
            this.lblKreirao.TabIndex = 0;
            this.lblKreirao.Text = "Kreirao:";
            // 
            // lblVremePrijema
            // 
            this.lblVremePrijema.AutoSize = true;
            this.lblVremePrijema.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVremePrijema.Location = new System.Drawing.Point(3, 155);
            this.lblVremePrijema.Name = "lblVremePrijema";
            this.lblVremePrijema.Size = new System.Drawing.Size(112, 16);
            this.lblVremePrijema.TabIndex = 1;
            this.lblVremePrijema.Text = "Vreme prijema:";
            // 
            // lblKreiranje
            // 
            this.lblKreiranje.AutoSize = true;
            this.lblKreiranje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKreiranje.Location = new System.Drawing.Point(3, 50);
            this.lblKreiranje.Name = "lblKreiranje";
            this.lblKreiranje.Size = new System.Drawing.Size(110, 16);
            this.lblKreiranje.TabIndex = 0;
            this.lblKreiranje.Text = "Datum i vreme:";
            // 
            // lblKrajPrijema
            // 
            this.lblKrajPrijema.AutoSize = true;
            this.lblKrajPrijema.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKrajPrijema.Location = new System.Drawing.Point(3, 130);
            this.lblKrajPrijema.Name = "lblKrajPrijema";
            this.lblKrajPrijema.Size = new System.Drawing.Size(94, 16);
            this.lblKrajPrijema.TabIndex = 0;
            this.lblKrajPrijema.Text = "Kraj prijema:";
            // 
            // lblPocetakPrijema
            // 
            this.lblPocetakPrijema.AutoSize = true;
            this.lblPocetakPrijema.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPocetakPrijema.Location = new System.Drawing.Point(3, 104);
            this.lblPocetakPrijema.Name = "lblPocetakPrijema";
            this.lblPocetakPrijema.Size = new System.Drawing.Size(124, 16);
            this.lblPocetakPrijema.TabIndex = 0;
            this.lblPocetakPrijema.Text = "Početak prijema:";
            // 
            // lblMagacioner
            // 
            this.lblMagacioner.AutoSize = true;
            this.lblMagacioner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMagacioner.Location = new System.Drawing.Point(2, 266);
            this.lblMagacioner.Name = "lblMagacioner";
            this.lblMagacioner.Size = new System.Drawing.Size(93, 16);
            this.lblMagacioner.TabIndex = 0;
            this.lblMagacioner.Text = "Magacioner:";
            // 
            // lblStavke
            // 
            this.lblStavke.AutoSize = true;
            this.lblStavke.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStavke.Location = new System.Drawing.Point(3, 79);
            this.lblStavke.Name = "lblStavke";
            this.lblStavke.Size = new System.Drawing.Size(146, 16);
            this.lblStavke.TabIndex = 0;
            this.lblStavke.Text = "Stavki na prijemnici:";
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart1.BorderlineColor = System.Drawing.Color.WhiteSmoke;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chart1.BorderlineWidth = 5;
            this.chart1.BorderSkin.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Sunken;
            chartArea1.AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            legend1.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(847, 12);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.YValuesPerPoint = 2;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(374, 402);
            this.chart1.TabIndex = 4;
            this.chart1.Text = "chart1";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title1";
            title1.Text = "Informacije o prijemu";
            this.chart1.Titles.Add(title1);
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // chart2
            // 
            this.chart2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart2.BorderlineColor = System.Drawing.Color.WhiteSmoke;
            this.chart2.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chart2.BorderlineWidth = 5;
            this.chart2.BorderSkin.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chart2.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Sunken;
            chartArea2.AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal;
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            legend2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend2.IsTextAutoFit = false;
            legend2.Name = "Legend1";
            legend2.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chart2.Legends.Add(legend2);
            this.chart2.Location = new System.Drawing.Point(12, 420);
            this.chart2.Name = "chart2";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series2";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series3";
            this.chart2.Series.Add(series2);
            this.chart2.Series.Add(series3);
            this.chart2.Series.Add(series4);
            this.chart2.Size = new System.Drawing.Size(1209, 475);
            this.chart2.TabIndex = 4;
            this.chart2.Text = "chart1";
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.Name = "Title1";
            title2.Text = "Uzimanje po prijemu";
            this.chart2.Titles.Add(title2);
            this.chart2.Click += new System.EventHandler(this.chart2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView2);
            this.panel1.Location = new System.Drawing.Point(27, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(410, 363);
            this.panel1.TabIndex = 5;
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(3, 15);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(404, 344);
            this.dataGridView2.TabIndex = 0;
            // 
            // lblPocetakUzimanja
            // 
            this.lblPocetakUzimanja.AutoSize = true;
            this.lblPocetakUzimanja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPocetakUzimanja.Location = new System.Drawing.Point(3, 187);
            this.lblPocetakUzimanja.Name = "lblPocetakUzimanja";
            this.lblPocetakUzimanja.Size = new System.Drawing.Size(121, 16);
            this.lblPocetakUzimanja.TabIndex = 1;
            this.lblPocetakUzimanja.Text = "Vreme uzimanja:";
            // 
            // lblKrajUlaganja
            // 
            this.lblKrajUlaganja.AutoSize = true;
            this.lblKrajUlaganja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKrajUlaganja.Location = new System.Drawing.Point(3, 213);
            this.lblKrajUlaganja.Name = "lblKrajUlaganja";
            this.lblKrajUlaganja.Size = new System.Drawing.Size(120, 16);
            this.lblKrajUlaganja.TabIndex = 1;
            this.lblKrajUlaganja.Text = "Vreme ulaganja:";
            // 
            // lblVremeUzUl
            // 
            this.lblVremeUzUl.AutoSize = true;
            this.lblVremeUzUl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVremeUzUl.Location = new System.Drawing.Point(4, 239);
            this.lblVremeUzUl.Name = "lblVremeUzUl";
            this.lblVremeUzUl.Size = new System.Drawing.Size(120, 16);
            this.lblVremeUzUl.TabIndex = 1;
            this.lblVremeUzUl.Text = "Vreme ulaganja:";
            // 
            // StatistikaPrijema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 907);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel2);
            this.Name = "StatistikaPrijema";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StatistikaPrijema";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblPrijemnica;
        private System.Windows.Forms.Label lblKreirao;
        private System.Windows.Forms.Label lblKreiranje;
        private System.Windows.Forms.Label lblKoristnikOdbacio;
        private System.Windows.Forms.Label lblOdbacio;
        private System.Windows.Forms.Label lblVremePrijema;
        private System.Windows.Forms.Label lblStavke;
        private System.Windows.Forms.Label lblPocetakPrijema;
        private System.Windows.Forms.Label lblKrajPrijema;
        private System.Windows.Forms.Label lblMagacioner;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label lblVremeUzUl;
        private System.Windows.Forms.Label lblKrajUlaganja;
        private System.Windows.Forms.Label lblPocetakUzimanja;
    }
}