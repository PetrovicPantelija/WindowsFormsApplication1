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
    public partial class StampajPalSveZavrseno : Form
    {
        public StampajPalSveZavrseno()
        {
            InitializeComponent();
        }

        private void StampajPalSveZavrseno_Load(object sender, EventArgs e)
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
           * */
            var select = " Select Distinct RTrim(Vozilo) as Vozilo From RkShippingZavrseno where DatumUtovara = '" + cboDatumUtovara.Text + "'";
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
            string Soriranje = "1";
            if (cb1.Checked == true)
            {
                Soriranje = "1";
            }
            if (cb2.Checked == true)
            {
                Soriranje = "2";
            }
            if (cb3.Checked == true)
            {
                Soriranje = "3";
            }
            DataSet10TableAdapters.TestProcedurePalSveTableAdapter ta = new DataSet10TableAdapters.TestProcedurePalSveTableAdapter();
            // DataSet4TableAdapters.TestProcedurePalSveTableAdapter ta = new DataSet2TableAdapters.TestProcedurePalTableAdapter();
            DataSet10.TestProcedurePalSveDataTable dt = new DataSet10.TestProcedurePalSveDataTable();
            // DataSet4.TestProcedurePalDataTable dt = new DataSet2.TestProcedurePalDataTable();
            ta.Fill(dt, cboVozila.Text, cboDatumUtovara.Text, Soriranje);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet2";
            rds.Value = dt;

            ReportDataSource rds2 = new ReportDataSource();
            rds2.Name = "DataSet1";
            rds2.Value = dt;

            ReportDataSource rds4 = new ReportDataSource();
            rds4.Name = "DataSet10";
            rds4.Value = dt;


            ReportParameter[] par = new ReportParameter[3];
            par[0] = new ReportParameter("Vozilo", cboVozila.Text.ToString());
            par[1] = new ReportParameter("DatumUtovara", cboDatumUtovara.Text.ToString());
            par[2] = new ReportParameter("Sorting", Soriranje);

            //this.ReportViewer1.ServerReport.SetParameters(params);

            //   ReportParameter rp = new ReportParameter();
            // ReportParameter rp2 = new ReportParameter();
            //ReportParameter rp3 = new ReportParameter();
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "Report4.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            // reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp2 });
            //reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp3 });
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.DataSources.Add(rds2);
            reportViewer1.LocalReport.DataSources.Add(rds4);
            reportViewer1.RefreshReport();
        }
    }
}
