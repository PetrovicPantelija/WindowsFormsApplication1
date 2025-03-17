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

    public partial class NamenskoPakovanjeIzvestaj : Form
    {
        int plan;
        public NamenskoPakovanjeIzvestaj()
        {
            InitializeComponent();
        }
        public NamenskoPakovanjeIzvestaj(int Id)
        {
            InitializeComponent();
            plan = Id;
        }

        private void NamenskoPakovanjeIzvestaj_Load(object sender, EventArgs e)
        { 
            Jela_TestDataSet5TableAdapters.PlanNamenskoRptTableAdapter ta = new Jela_TestDataSet5TableAdapters.PlanNamenskoRptTableAdapter();
            Jela_TestDataSet5.PlanNamenskoRptDataTable dt = new Jela_TestDataSet5.PlanNamenskoRptDataTable();
            ta.Fill(dt, plan);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;
            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("IDPlana", plan.ToString());

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "NamenskoPakovanje.rdlc";
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
