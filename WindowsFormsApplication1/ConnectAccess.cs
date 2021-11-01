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

namespace WindowsFormsApplication1
{
    public partial class ConnectAccess : Form
    {
        public ConnectAccess()
        {
           /*
            OleDbConnection conObj = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = D:\\Dev\\webs\\webs_baza16.mdb");
            conObj.Open();
           
            DataTable dt = new DataTable();

            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM SysKomintenti", conObj);
          //  da = new OleDbDataAdapter("SELECT * FROM NaloziZaUtovar", conObj);
            DataSet ds = new DataSet("NaloziZaUtovar");
            // Call DataAdapter's Fill method to fill data from the
            // DataAdapter to the DataSet 
            da.SelectCommand = new OleDbCommand("SELECT * FROM SysKomintenti", conObj);
            da.Fill(ds);
            dataGridView1.DataSource = ds.DefaultViewManager;
            
            conObj.Close();
             */
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void ConnectAccess_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'webs_baza16DataSet1.NaloziZaUtovar' table. You can move, or remove it, as needed.
           // this.naloziZaUtovarTableAdapter.Fill(this.webs_baza16DataSet1.NaloziZaUtovar);
            // TODO: This line of code loads data into the 'webs_baza16DataSet.NaloziZaUtovar' table. You can move, or remove it, as needed.
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
           /*
           string connString =
               "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = D:\\Dev\\webs\\webs_baza16.mdb";

           DataTable dataTableRes = new DataTable();

           using (OleDbConnection conn = new OleDbConnection(connString))
           {
               OleDbCommand cmd = new OleDbCommand("SELECT * FROM NaloziZaUtovar", conn);

               conn.Open();

               OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

               adapter.Fill(dataTableRes);
           }

           dataGridView1.DataSource = dataTableRes;
            * */
       }
        }
    }

