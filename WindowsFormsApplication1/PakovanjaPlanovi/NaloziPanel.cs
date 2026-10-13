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
    public partial class NaloziPanel : Form
    {
        int plan;
        string skladiste, korisnik;
        public NaloziPanel()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
        }

        public NaloziPanel(int IDPlanPakovanja,string Skladista)
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            plan = IDPlanPakovanja;
            skladiste = Skladista;
        }
        private void NaloziPanel_Load(object sender, EventArgs e)
        {
            Jela_TestDataSet4TableAdapters.NalogPlanPaneliTableAdapter ta = new Jela_TestDataSet4TableAdapters.NalogPlanPaneliTableAdapter();
            Jela_TestDataSet4.NalogPlanPaneliDataTable dt = new Jela_TestDataSet4.NalogPlanPaneliDataTable();
            ta.Fill(dt,plan,skladiste,korisnik);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;
            ReportParameter[] par = new ReportParameter[2];
            par[0] = new ReportParameter("IDPlanaPakovanja", plan.ToString());
            par[1] = new ReportParameter("Skladiste", skladiste);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "NalogPaneli.rdlc";
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
