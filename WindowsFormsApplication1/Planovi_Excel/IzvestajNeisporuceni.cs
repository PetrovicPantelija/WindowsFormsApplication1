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
using System.Windows.Forms;

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class IzvestajNeisporuceni : Form
    {
        public IzvestajNeisporuceni()
        {
            InitializeComponent();
        }

        private void IzvestajNeisporuceni_Load(object sender, EventArgs e)
        {
            Jela_TestDataSetTableAdapters.NeisporuceniIzvestajTableAdapter ta = new Jela_TestDataSetTableAdapters.NeisporuceniIzvestajTableAdapter();
            Jela_TestDataSet.NeisporuceniIzvestajDataTable dt = new Jela_TestDataSet.NeisporuceniIzvestajDataTable();
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptNeisporuceni.rdlc";
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();

            System.Drawing.Printing.PageSettings newPageSettings = new System.Drawing.Printing.PageSettings();
            newPageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            newPageSettings.Landscape = false;
            reportViewer1.SetPageSettings(newPageSettings);
        }
    }
}
