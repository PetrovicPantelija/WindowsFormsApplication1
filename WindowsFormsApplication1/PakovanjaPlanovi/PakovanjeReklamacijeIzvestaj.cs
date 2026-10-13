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
    public partial class PakovanjeReklamacijeIzvestaj : Form
    {
        public PakovanjeReklamacijeIzvestaj()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
        }   
        int ID;
        public PakovanjeReklamacijeIzvestaj(int id)
        {
            ID = id;
            InitializeComponent();
            JelaTema.Primeni(this);
        }

        private void PakovanjeReklamacijeIzvestaj_Load(object sender, EventArgs e)
        {
            Jela_TestDataSet2TableAdapters.PakovanjeReklamacijeRptTableAdapter ta = new Jela_TestDataSet2TableAdapters.PakovanjeReklamacijeRptTableAdapter();
            Jela_TestDataSet2.PakovanjeReklamacijeRptDataTable dt = new Jela_TestDataSet2.PakovanjeReklamacijeRptDataTable();
            ta.Fill(dt, ID);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;
            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("IDPlana", ID.ToString());

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "ReklamacijeRPT.rdlc";
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
