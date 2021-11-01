using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using Microsoft.Reporting.WinForms;

namespace WindowsFormsApplication1
{
    public partial class StampajNeisporucene : Form
    {
        public StampajNeisporucene()
        {
            InitializeComponent();
        }

        private void StampajNeisporucene_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet3.TestProcedureNeispor' table. You can move, or remove it, as needed.
           // this.TestProcedureNeisporTableAdapter.Fill(this.DataSet3.TestProcedureNeispor);
            string upit2 = " SELECT * from TrVozila";
            var select = upit2;

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            //string value = dataGridView1.Rows[0].Cells[0].Value.ToString();


            dataGridView1.Refresh();
           // this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet3TableAdapters.TestProcedureNeisporTableAdapter ta = new DataSet3TableAdapters.TestProcedureNeisporTableAdapter();
            DataSet3.TestProcedureNeisporDataTable dt = new DataSet3.TestProcedureNeisporDataTable();
            ta.Fill(dt, txtVozila.Text, txtDatum.Text);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet3";
            rds.Value = dt;


            ReportParameter[] par = new ReportParameter[2];
            par[0] = new ReportParameter("Vozila", txtVozila.Text.ToString());
            par[1] = new ReportParameter("Datum", txtDatum.Text.ToString());
         
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "Report3.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }
    }
}
