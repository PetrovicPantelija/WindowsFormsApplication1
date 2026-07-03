using System;
using System.Collections.Generic;
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

using Microsoft.Reporting.WinForms;

namespace WindowsFormsApplication1.Neto_potrebe
{
    public partial class StampaNP : Form
    {
        int tmpPlan = 0;
        string tmpPodPlan = "";

        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public StampaNP()
        {
            InitializeComponent();
        }

        public StampaNP(int Plan, string Podplan)
        {
            InitializeComponent();
            tmpPlan = Plan;
            tmpPodPlan = Podplan;
        }

        private void StampaNP_Load(object sender, EventArgs e)
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
            StampajNPDataSetTableAdapters.StampaNPTableAdapter ta = new StampajNPDataSetTableAdapters.StampaNPTableAdapter();
            StampajNPDataSet.StampaNPDataTable dt = new StampajNPDataSet.StampaNPDataTable();


            ta.Fill(dt);

            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptStampaNP.rdlc";
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }
    }
}
