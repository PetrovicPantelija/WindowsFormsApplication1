using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;


namespace WindowsFormsApplication1
{
    public partial class StampaP : Form
    {
        public StampaP()
        {
            InitializeComponent();
        }

        private void StampaP_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet2.TestProcedurePal' table. You can move, or remove it, as needed.
           // this.TestProcedurePalTableAdapter.Fill(this.DataSet2.TestProcedurePal);
            // TODO: This line of code loads data into the 'MyDataSet.TestProcedure' table. You can move, or remove it, as needed.
            this.TestProcedureTableAdapter.Fill(this.MyDataSet.TestProcedure,"0");
            // TODO: This line of code loads data into the 'DataSet2.TestProcedurePal' table. You can move, or remove it, as needed.
           // this.TestProcedureTableAdapter.Fill(this.DataSet1.);
            // TODO: This line of code loads data into the 'MyDataSet.TestProcedure' table. You can move, or remove it, as needed.
          //  this.TestProcedureTableAdapter.Fill(this.MyDataSet.TestProcedure);
            // TODO: This line of code loads data into the 'MyDataSet.TestProcedure' table. You can move, or remove it, as needed.
          ////  this.TestProcedureTableAdapter.Fill(this.MyDataSet.TestProcedure);
            // TODO: This line of code loads data into the 'MyDataSet.TestProcedure' table. You can move, or remove it, as needed.
           /*
           //// this.TestProcedureTableAdapter.Fill(this.MyDataSet.TestProcedure);
            MyDataSetTableAdapters.TestProcedureTableAdapter ta = new MyDataSetTableAdapters.TestProcedureTableAdapter();
            MyDataSet.TestProcedureDataTable dt = new MyDataSet.TestProcedureDataTable();
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "Report1.rdlc";
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.Refresh();
            // this.reportViewer1.RefreshReport();
            // this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
            * */
           // this.reportViewer1.RefreshReport();
          //  this.reportViewer1.RefreshReport();
//this.reportViewer1.RefreshReport();
         //   this.reportViewer1.RefreshReport();
         //   this.reportViewer1.RefreshReport();
            }

        private void button1_Click(object sender, EventArgs e)
        {
            MyDataSetTableAdapters.TestProcedureTableAdapter ta = new MyDataSetTableAdapters.TestProcedureTableAdapter();
            MyDataSet.TestProcedureDataTable dt = new MyDataSet.TestProcedureDataTable();
            ta.Fill(dt, textBox1.Text);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            ReportDataSource rds2 = new ReportDataSource();
            rds2.Name = "DataSet2";
            rds2.Value = dt;

            ReportParameter rp = new ReportParameter("BrojUtovara", textBox1.Text.ToString());

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "Report1.rdlc";
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.DataSources.Add(rds2);
            //reportViewer1.RefreshReportLocalReport.Refresh();
            reportViewer1.RefreshReport();
        }
        }
    }

