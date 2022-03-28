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
using System.Configuration;

namespace WindowsFormsApplication1
{
    public partial class StampaP : Form
    {
        public static string code = "StampaP";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public StampaP()
        {
            InitializeComponent();
            IdGrupe();
            IdForme();
            PravoPristupa();
        }
        public string IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            //Sifarnici.frmLogovanje frm = new Sifarnici.frmLogovanje();         
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            int count = 0;

            while (dr.Read())
            {
                if (count == 0)
                {
                    niz = dr["IdGrupe"].ToString();
                    count++;
                }
                else
                {
                    niz = niz + "," + dr["IdGrupe"].ToString();
                    count++;
                }

            }
            conn.Close();
            return niz;
        }
        private int IdForme()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select IdForme from Forme where Rtrim(Code)=" + "'" + code + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idForme = Convert.ToInt32(dr["IdForme"].ToString());
            }
            conn.Close();
            return idForme;
        }

        private void PravoPristupa()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select * From GrupaForma Where IdGrupe in (" + niz + ") and IdForme=" + idForme;
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nemate prava za pristup ovoj formi", code);
                Pravo = false;
            }
            else
            {
                Pravo = true;
                while (reader.Read())
                {
                    insert = Convert.ToBoolean(reader["Upis"]);
                    if (insert == false)
                    {
                        //tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                        //tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                        //tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
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

