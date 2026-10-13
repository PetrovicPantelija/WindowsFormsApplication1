using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    public partial class NalogStiropor : Form
    {
        int plan;
        string skladiste, korisnik;
        public NalogStiropor()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
        }
        public NalogStiropor(int IDPlanaPakovanja,string Skladista)
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            plan = IDPlanaPakovanja;
            skladiste= Skladista;
        }       
        private void NalogStiropor_Load(object sender, EventArgs e)
        {
            Jela_TestDataSet3TableAdapters.NalogPlanStiroporTableAdapter ta = new Jela_TestDataSet3TableAdapters.NalogPlanStiroporTableAdapter();
            Jela_TestDataSet3.NalogPlanStiroporDataTable dt = new Jela_TestDataSet3.NalogPlanStiroporDataTable();
            ta.Fill(dt, plan, skladiste, korisnik);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;
            ReportParameter[] par = new ReportParameter[2];
            par[0] = new ReportParameter("IDPlanaPakovanja", plan.ToString());
            par[1] = new ReportParameter("Skladiste", skladiste);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "NalogStiropor.rdlc";
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

