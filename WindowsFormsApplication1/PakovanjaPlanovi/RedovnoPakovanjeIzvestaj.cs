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
    public partial class RedovnoPakovanjeIzvestaj : Form
    {
        int idPlanaPakovanja;
        public RedovnoPakovanjeIzvestaj()
        {
            InitializeComponent();
        }
        public RedovnoPakovanjeIzvestaj(int ID)
        {
            InitializeComponent();
            idPlanaPakovanja= ID;
        }

        private void RedovnoPakovanjeIzvestaj_Load(object sender, EventArgs e)
        {
            Jela_TestDataSet6TableAdapters.RedovnoPakovanjeRptTableAdapter ta = new Jela_TestDataSet6TableAdapters.RedovnoPakovanjeRptTableAdapter();
            Jela_TestDataSet6.RedovnoPakovanjeRptDataTable dt = new Jela_TestDataSet6.RedovnoPakovanjeRptDataTable();
            ta.Fill(dt, idPlanaPakovanja);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;
            ReportParameter[] par = new ReportParameter[1];
            par[0]=new ReportParameter("IDPakovanja",idPlanaPakovanja.ToString());
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "RedovnoPakovanjeRpt.rdlc";
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
