using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    public partial class NaloziZaIzradu : Form
    {
        int plan;
        string skladiste, korisnik,materijal;
        public NaloziZaIzradu()
        {
            InitializeComponent();
        }
        public NaloziZaIzradu(int IDPlanaPakovanja,string Skladista,string Materijal)
        {
            InitializeComponent();

            plan=IDPlanaPakovanja;
            skladiste=Skladista;
            materijal=Materijal;
        }

        private void NaloziZaIzradu_Load(object sender, EventArgs e)
        {
            Jela_TestDataSet1TableAdapters.NalogProizvodnjaPlanTableAdapter ta = new Jela_TestDataSet1TableAdapters.NalogProizvodnjaPlanTableAdapter();
            Jela_TestDataSet1.NalogProizvodnjaPlanDataTable dt = new Jela_TestDataSet1.NalogProizvodnjaPlanDataTable();
            ta.Fill(dt,plan,skladiste,korisnik,materijal);
            ReportDataSource rds= new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;
            ReportParameter[] par=new ReportParameter[3];
            par[0] = new ReportParameter("IDPlanaPakovanja", plan.ToString());
            par[1] = new ReportParameter("Skladiste", skladiste);
            par[2] = new ReportParameter("Materijal", materijal);

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
