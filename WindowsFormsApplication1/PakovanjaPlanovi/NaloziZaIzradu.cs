using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using WindowsFormsApplication1.Planovi_Excel;

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    public partial class NaloziZaIzradu : Form
    {
        int plan;
        string skladiste, korisnik;
        public NaloziZaIzradu()
        {
            InitializeComponent();
        }
        public NaloziZaIzradu(int IDPlanaPakovanja,string Skladista)
        {
            InitializeComponent();

            plan=IDPlanaPakovanja;
            skladiste=Skladista;
        }

        private void NaloziZaIzradu_Load(object sender, EventArgs e)
        {
            Jela_TestDataSet1TableAdapters.NalogProizvodnjaPlanTableAdapter ta = new Jela_TestDataSet1TableAdapters.NalogProizvodnjaPlanTableAdapter();
            Jela_TestDataSet1.NalogProizvodnjaPlanDataTable dt = new Jela_TestDataSet1.NalogProizvodnjaPlanDataTable();
            ta.Fill(dt,plan,skladiste,korisnik);
            ReportDataSource rds= new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;
            ReportParameter[] par=new ReportParameter[2];
            par[0] = new ReportParameter("IDPlanaPakovanja", plan.ToString());
            par[1] = new ReportParameter("Skladiste", skladiste);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "Nalozi.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();

            System.Drawing.Printing.PageSettings newPageSettings = new System.Drawing.Printing.PageSettings();
            newPageSettings.Margins = new System.Drawing.Printing.Margins(5, 0, 5, 0);
            newPageSettings.Landscape = false;
            reportViewer1.SetPageSettings(newPageSettings);
        }
    }
}
