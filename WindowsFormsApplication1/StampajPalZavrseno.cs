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
    public partial class StampajPalZavrseno : Form
    {
        public StampajPalZavrseno()
        {
            InitializeComponent();
        }

        private void StampajPalZavrseno_Load(object sender, EventArgs e)
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
        }

        private void button5_Click(object sender, EventArgs e)
        {
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

        private void button3_Click(object sender, EventArgs e)
        {
            var select = " Select Distinct RkShippingItemPakZavrseno.Paleta From RkShippingZavrseno inner Join RkShippingItemPakZavrseno on RkShippingZavrseno.ShippingId = RkShippingItemPakZavrseno.ShipingIdz where  RkShippingZavrseno.Vozilo = '" + cboVozila.Text + "' and RkShippingZavrseno.DatumUtovara = '" + cboDatumUtovara.Text + "'";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboPaleta.DataSource = ds.Tables[0];
            cboPaleta.DisplayMember = "Paleta";
            cboPaleta.ValueMember = "Paleta";
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
            DataSet22TableAdapters.TestProcedurePalZavrsenoTableAdapter ta = new DataSet22TableAdapters.TestProcedurePalZavrsenoTableAdapter();
            DataSet22.TestProcedurePalZavrsenoDataTable dt = new DataSet22.TestProcedurePalZavrsenoDataTable();
            ta.Fill(dt, cboVozila.Text, cboDatumUtovara.Text, cboPaleta.Text, Soriranje);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet22";
            rds.Value = dt;

            // ReportDataSource rds2 = new ReportDataSource();
            //rds2.Name = "DataSet1";
            //rds2.Value = dt;

            ReportParameter[] par = new ReportParameter[4];
            par[0] = new ReportParameter("Vozilo", cboVozila.Text.ToString());
            par[1] = new ReportParameter("DatumUtovara", cboDatumUtovara.Text.ToString());
            par[2] = new ReportParameter("Paleta", cboPaleta.Text.ToString());
            par[3] = new ReportParameter("Sorting", Soriranje);
            //this.ReportViewer1.ServerReport.SetParameters(params);

            //   ReportParameter rp = new ReportParameter();
            // ReportParameter rp2 = new ReportParameter();
            //ReportParameter rp3 = new ReportParameter();
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "Report2Zavrseno.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            // reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp2 });
            //reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp3 });
            reportViewer1.LocalReport.DataSources.Add(rds);
            // reportViewer1.LocalReport.DataSources.Add(rds2);
            reportViewer1.RefreshReport();
        }
    }
}
