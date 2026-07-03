using Microsoft.Reporting.WinForms;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Neto_potrebe
{
   
    public partial class StampaPodPlana : Form
    {
        int tmpPlan = 0;
        string tmpPodPlan = "";

        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public StampaPodPlana()
        {
            InitializeComponent();
        }
        public StampaPodPlana(int Plan, string Podplan)
        {
            InitializeComponent();
            tmpPlan = Plan;
            tmpPodPlan = Podplan;
        }

        private void StampaPodPlana_Load(object sender, EventArgs e)
        {
            FillPlanovi();
            if (tmpPlan != 0 && tmpPodPlan != "")
            {
                FillPlanovi();
                txtPodPlan.Text = tmpPodPlan;
                cboPlan.SelectedValue = tmpPlan;

            }
        }

        private void FillPlanovi()
        {
            var query = "Select ID,Naziv From PlanArhiv order by ID asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboPlan.DataSource = ds.Tables[0];
            cboPlan.DisplayMember = "Naziv";
            cboPlan.ValueMember = "ID";
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            StampajPodPlanDataSetTableAdapters.StampajPodPlanTableAdapter ta = new StampajPodPlanDataSetTableAdapters.StampajPodPlanTableAdapter();
            StampajPodPlanDataSet.StampajPodPlanDataTable dt = new StampajPodPlanDataSet.StampajPodPlanDataTable();
           

            ta.Fill(dt, Convert.ToInt32(cboPlan.SelectedValue), txtPodPlan.Text);
           
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            ReportParameter[] par = new ReportParameter[2];
            par[0] = new ReportParameter("Plan", cboPlan.SelectedValue.ToString());
            par[1] = new ReportParameter("PodPlan", txtPodPlan.Text.ToString());
          

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "StampajPodPlan.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }
    }
}
