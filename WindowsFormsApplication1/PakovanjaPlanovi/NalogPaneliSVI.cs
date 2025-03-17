using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    public partial class NalogPaneliSVI : Form
    {
        int plan;
        string skladista, korisnik;
        public NalogPaneliSVI()
        {
            InitializeComponent();
        }
        public NalogPaneliSVI(int IDPlanaPakovanja,string Skladista)
        {
            InitializeComponent();
            plan = IDPlanaPakovanja;
            skladista = Skladista;
        }
        private void NalogPaneliSVI_Load(object sender, EventArgs e)
        {
            Jela_TestDataSet8TableAdapters.NalogPaneliSVITableAdapter ta = new Jela_TestDataSet8TableAdapters.NalogPaneliSVITableAdapter();
            Jela_TestDataSet8.NalogPaneliSVIDataTable dt = new Jela_TestDataSet8.NalogPaneliSVIDataTable();
            ta.Fill(dt,plan,skladista,korisnik);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;
            ReportParameter[] par = new ReportParameter[2];
            par[0] = new ReportParameter("IDPlanaPakovanja", plan.ToString());
            par[1] = new ReportParameter("Skladiste", skladista);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "PaneliSVI.rdlc";
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
