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
using System.Configuration;

using Microsoft.Reporting.WinForms;

namespace WindowsFormsApplication1
{
    public partial class StampajReklamacijePZavrseno : Form
    {
        public StampajReklamacijePZavrseno()
        {
            InitializeComponent();
        }

        private void StampajReklamacijePZavrseno_Load(object sender, EventArgs e)
        {
            var select = " Select Distinct DatumUtovara, Cast(SUBSTRING(DatumUtovara, 1, Charindex('.',DatumUtovara) -1) as int)  as  Dan, Cast(Replace(SUBSTRING(DatumUtovara, Charindex('.',DatumUtovara)+1, Len(DatumUtovara)-12 -1 -( (Charindex('.',DatumUtovara) ))),'.','') as int) as  Mesec  From RkShippingZavrseno  order by  Mesec, Dan";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboDatumUtovara.DataSource = ds.Tables[0];
            cboDatumUtovara.DisplayMember = "DatumUtovara";
            cboDatumUtovara.ValueMember = "DatumUtovara";

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            /*
           var select = " Select Distinct DatumUtovara, Cast(SUBSTRING(DatumUtovara, 1, Charindex('.',DatumUtovara) -1) as int)  as  Dan, Cast(Replace(SUBSTRING(DatumUtovara, Charindex('.',DatumUtovara)+1, Len(DatumUtovara)-12 -1 -( (Charindex('.',DatumUtovara) ))),'.','') as int) as  Mesec from RkShipping  where  Vozilo = '" + cboVozila.Text + "' order by Mesec, Dan";

           var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
           SqlConnection myConnection = new SqlConnection(s_connection);
           var c = new SqlConnection(s_connection);
           var dataAdapter = new SqlDataAdapter(select, c);

           var commandBuilder = new SqlCommandBuilder(dataAdapter);
           var ds = new DataSet();
           dataAdapter.Fill(ds);
           cboDatumUtovara.DataSource = ds.Tables[0];
           cboDatumUtovara.DisplayMember = "DatumUtovara";
           cboDatumUtovara.ValueMember = "DatumUtovara";
            */

            var select = " Select Distinct RTrim(Vozilo) as Vozilo From RkShippingZavrseno where  DatumUtovara = '" + cboDatumUtovara.Text + "'";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboVozila.DataSource = ds.Tables[0];
            cboVozila.DisplayMember = "Vozilo";
            cboVozila.ValueMember = "Vozilo";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet55TableAdapters.TestProcedurePalSveRZavrsenoTableAdapter ta = new DataSet55TableAdapters.TestProcedurePalSveRZavrsenoTableAdapter();
            DataSet55.TestProcedurePalSveRZavrsenoDataTable dt = new DataSet55.TestProcedurePalSveRZavrsenoDataTable();
            ta.Fill(dt, cboVozila.Text, cboDatumUtovara.Text);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet55";
            rds.Value = dt;

            ReportDataSource rds2 = new ReportDataSource();
            rds2.Name = "DataSet55";
            rds2.Value = dt;

            ReportDataSource rds4 = new ReportDataSource();
            rds4.Name = "DataSet55";
            rds4.Value = dt;


            ReportParameter[] par = new ReportParameter[2];
            par[0] = new ReportParameter("Vozilo", cboVozila.Text.ToString());
            par[1] = new ReportParameter("DatumUtovara", cboDatumUtovara.Text.ToString());


            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "Report7Zavrseno.rdlc";
            reportViewer1.LocalReport.SetParameters(par);

            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.DataSources.Add(rds2);
            reportViewer1.LocalReport.DataSources.Add(rds4);
            reportViewer1.RefreshReport();
        }
    }
}
