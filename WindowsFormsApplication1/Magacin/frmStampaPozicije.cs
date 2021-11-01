using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;

using Microsoft.Reporting.WinForms;

namespace WindowsFormsApplication1.Magacin
{
    public partial class frmStampaPozicije : Form
    {
        public frmStampaPozicije()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Barcodes.Barcode39 b = new Barcodes.Barcode39();
            String img;
            InsertPozicijaBarkod updpoz = new InsertPozicijaBarkod();
            
            //Ovde dodati kod
            //updpoz.DeletePozicijeBarkod();
            // Insert netacan napisan za probu
            //updpoz.InsPozicijeBarkod(cboMaticni.ToString(), txtBarkodPozicije.Text,txtPozicija.Text);
          
            b.TextFont = new Font("Courier New", 9);

            txtBarkodPozicije.Text = b.GenerateBarcodeImage(txtPozicija.Text);

            //Štampa

            Perftech_BeogradDataSetTableAdapters.PozicijaBarkodTableAdapter ta = new Perftech_BeogradDataSetTableAdapters.PozicijaBarkodTableAdapter();
            Perftech_BeogradDataSet.PozicijaBarkodDataTable dt = new Perftech_BeogradDataSet.PozicijaBarkodDataTable();
     
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptBarkodPozicija.rdlc";

            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();


        }

        private void frmStampaPozicije_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
