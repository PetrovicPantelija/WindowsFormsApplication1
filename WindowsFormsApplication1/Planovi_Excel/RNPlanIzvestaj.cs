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

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class RNPlanIzvestaj : Form
    {
        int id;
        public RNPlanIzvestaj()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
        }
        public RNPlanIzvestaj(int ID)
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            id = ID;
        }

        private void RNPlanIzvestaj_Load(object sender, EventArgs e)
        {
            
            Jela_testDataSetTableAdapters.RnPlanIzvestajTableAdapter ta = new Jela_testDataSetTableAdapters.RnPlanIzvestajTableAdapter();
            
            Jela_testDataSet.RnPlanIzvestajDataTable dt = new Jela_testDataSet.RnPlanIzvestajDataTable();
            ta.Fill(dt, id);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;
            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("ID", id.ToString());

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "ReportRN.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();

            System.Drawing.Printing.PageSettings newPageSettings = new System.Drawing.Printing.PageSettings();
            newPageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            newPageSettings.Landscape = false;
            reportViewer1.SetPageSettings(newPageSettings);
        }
    }
}
