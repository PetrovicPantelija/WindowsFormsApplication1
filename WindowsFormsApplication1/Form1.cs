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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string connString =
            ConfigurationManager.AppSettings["webs"];

            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM nalozizautovar", conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(dataTableRes);
                dataGridView1.DataSource = dataTableRes;
                conn.Close();
            }
             
        }

        private void btnPoDatumuIVozilu_Click(object sender, EventArgs e)
        {

            string connString =
            ConfigurationManager.AppSettings["webs"];
            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM NaloziZaUtovar where Vozilo = " + "'" + txtVozilo.Text + "' and DatumIsporuke =" + "DateValue('" + txtDatumIsporuke.Text + "')", conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(dataTableRes);
            }

            dataGridView1.DataSource = dataTableRes;

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            /*"Data Source=ServerName;" + 
              "Initial Catalog=DataBaseName;" + 
              "User id=UserName;" + 
              "Password=Secret;"; */

            /*
             [tbl_Prenos]
           ([VrstaDokumentaIDz] ,[DatumDokumenta] ,[KomintentIDz] ,[KomintentMestoIsporukeIDz] ,[DatumPorudzbine] ,[Poruceno] ,[SifraProizvoda] ,[NazivProizvoda] ,[IsporukaID] ,[DatumUtovara] ,[DatumIsporuke],[BrojUtovara],[BrojIsporuke] ,[VozacIDz] ,[Stampa],[Zavrseno] ,[NazivKomitenta])
            */
            using (SqlConnection connection = new SqlConnection("Data Source=NPSBGNB-95\\sqlexpress2012;Initial Catalog=KrsteniceTest;Integrated Security=SSPI;"))
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                   // currQty += row.Cells["qty"].Value;
                    //More code here
                    if (row != null && row.Cells[0].Value!= null)
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = connection;            // <== lacking
                            command.CommandType = CommandType.Text;
                          //  command.CommandText = "INSERT into tbl_Prenos (Vozilo, VrstaDokumentaIDz ,DatumDokumenta ,KomintentIDz ,KomintentMestoIsporukeIDz ,DatumPorudzbine ,Poruceno ,SifraProizvoda ,NazivProizvoda ,IsporukaID ,DatumUtovara ,DatumIsporuke,BrojUtovara,BrojIsporuke ,VozacIDz ,Stampa,Zavrseno ,NazivKomitenta) VALUES (@Vozilo, @VrstaDokumentaIDz ,@DatumDokumenta ,@KomintentIDz ,@KomintentMestoIsporukeIDz ,@DatumPorudzbine ,@Poruceno ,@SifraProizvoda ,@NazivProizvoda ,@IsporukaID ,@DatumUtovara ,@DatumIsporuke,@BrojUtovara,@BrojIsporuke ,@VozacIDz ,@Stampa,@Zavrseno ,@NazivKomitenta)";
                           // command.CommandText = "INSERT into tbl_prenos (Vozilo, VrstaDokumentaIDz ,DatumDokumenta ,KomintentIDz ,KomintentMestoIsporukeIDz ,DatumPorudzbine ,Poruceno ,SifraProizvoda ,NazivProizvoda ,IsporukaID ,DatumUtovara ,DatumIsporuke,BrojUtovara,BrojIsporuke ,VozacIDz ,Stampa,Zavrseno ,NazivKomitenta) VALUES ('1', '1' ,'1' ,'1' ,'1' ,'1' ,'1' ,'1','1' ,'1' ,'1' ,'1','1','1' ,'1' ,'1','1','1')";
                            command.CommandText = "INSERT into tbl_prenos (Vozilo, VrstaDokumentaIDz ,DatumDokumenta , " +
                           " KomintentIDz ,KomintentMestoIsporukeIDz ,DatumPorudzbine " +
                            " ,Poruceno ,SifraProizvoda , " +
                           " NazivProizvoda ,IsporukaID ,DatumUtovara ,DatumIsporuke,BrojUtovara,BrojIsporuke ," +
                            "VozacIDz ,Stampa,Zavrseno ,NazivKomitenta) VALUES " +
                            "(" + "'" + row.Cells[0].Value.ToString() +"','" + row.Cells[1].Value.ToString() +"','"  +
                             row.Cells[2].Value.ToString() + "','" + 
                            row.Cells[3].Value.ToString()+"','" +row.Cells[4].Value.ToString()+"','" +row.Cells[5].Value.ToString()+"','" +
                                row.Cells[6].Value.ToString()+"','" +row.Cells[7].Value.ToString()+"','" +row.Cells[8].Value.ToString()+"','" +
                                row.Cells[9].Value.ToString()+"','" +row.Cells[10].Value.ToString()+"','" +row.Cells[11].Value.ToString()+"','" + 
                                row.Cells[12].Value.ToString()+"','" +row.Cells[13].Value.ToString()+"','" +row.Cells[14].Value.ToString()+"','" +
                                row.Cells[15].Value.ToString()+"','" +row.Cells[16].Value.ToString()+"','" +row.Cells[17].Value.ToString() +"'" +")";
                            /*
                             command.Parameters.Add("@Vozilo", row.Cells[0].Value.ToString());
                             
                             command.Parameters.Add("@Vozilo", SqlDbType.NVarChar,50).Value = row.Cells[0].Value.ToString();
                             command.Parameters.Add("@VrstaDokumentaIDz", SqlDbType.NVarChar, 50).Value = row.Cells[0].Value.ToString();
                             command.Parameters.Add("@DatumDokumenta", SqlDbType.NVarChar, 50).Value = row.Cells[0].Value.ToString();
                             command.Parameters.Add("@KomintentIDz", SqlDbType.NVarChar, 50).Value = row.Cells[0].Value.ToString();
                             command.Parameters.Add("@KomintentMestoIsporukeID", SqlDbType.NVarChar, 50).Value = row.Cells[0].Value.ToString();
                             command.Parameters.Add("@DatumPorudzbine", SqlDbType.NVarChar, 50).Value = row.Cells[0].Value.ToString();
                             command.Parameters.Add("@Poruceno", SqlDbType.NVarChar, 50).Value = row.Cells[0].Value.ToString();
                             command.Parameters.Add("@SifraProizvoda", SqlDbType.NVarChar, 50).Value = row.Cells[0].Value.ToString();
                             command.Parameters.Add("@NazivProizvoda", SqlDbType.NVarChar, 50).Value = row.Cells[0].Value.ToString();
                             command.Parameters.Add("@IsporukaID", SqlDbType.NVarChar, 50).Value = row.Cells[0].Value.ToString();
                             command.Parameters.Add("@DatumUtovara", SqlDbType.NVarChar, 50).Value = row.Cells[0].Value.ToString();
                             command.Parameters.Add("@DatumIsporuke", SqlDbType.NVarChar, 50).Value = row.Cells[0].Value.ToString();
                             command.Parameters.Add("@BrojUtovara", SqlDbType.NVarChar, 50).Value = row.Cells[0].Value.ToString();
                             command.Parameters.Add("@BrojIsporuke", SqlDbType.NVarChar, 50).Value = row.Cells[0].Value.ToString();
                             command.Parameters.Add("@VozacIDz", SqlDbType.NVarChar, 50).Value = row.Cells[0].Value.ToString();
                             command.Parameters.Add("@Zavrseno", SqlDbType.NVarChar, 50).Value = row.Cells[0].Value.ToString();
                             command.Parameters.Add("@NazivKomitenta", SqlDbType.NVarChar, 50).Value = row.Cells[0].Value.ToString();
                             /*
                             command.Parameters.Add("@VrstaDokumentaIDz", row.Cells[1].Value.ToString());
                              command.Parameters.Add("@DatumDokumenta", row.Cells[2].Value.ToString());
                              command.Parameters.Add("@KomintentIDz", row.Cells[3].Value.ToString());
                              command.Parameters.Add("@KomintentMestoIsporukeIDz", row.Cells[4].Value.ToString());
                              command.Parameters.Add("@DatumPorudzbine", row.Cells[5].Value.ToString());
                              command.Parameters.Add("@Poruceno", row.Cells[6].Value.ToString());
                              command.Parameters.Add("@SifraProizvoda", row.Cells[7].Value.ToString());
                              command.Parameters.Add("@NazivProizvoda", row.Cells[8].Value.ToString());
                              command.Parameters.Add("@IsporukaID", row.Cells[9].Value.ToString());
                              command.Parameters.Add("@DatumUtovara", row.Cells[10].Value.ToString());
                              command.Parameters.Add("@DatumIsporuke", row.Cells[11].Value.ToString());
                              command.Parameters.Add("@BrojUtovara", row.Cells[12].Value.ToString());
                              command.Parameters.Add("@BrojIsporuke", row.Cells[13].Value.ToString());
                              command.Parameters.Add("@VozacIDz", row.Cells[14].Value.ToString());
                              command.Parameters.Add("@Zavrseno", row.Cells[15].Value.ToString());
                              command.Parameters.Add("@NazivKomitenta", row.Cells[16].Value.ToString());
                             
                             command.Parameters.Add("@Vozilo", '1');
                         

                             command.Parameters.Add("@VrstaDokumentaIDz", '1');
                             command.Parameters.Add("@DatumDokumenta", '1');
                             command.Parameters.Add("@KomintentIDz", '1');
                             command.Parameters.Add("@KomintentMestoIsporukeIDz", '1');
                             command.Parameters.Add("@DatumPorudzbine", '1');
                             command.Parameters.Add("@Poruceno", '1');
                             command.Parameters.Add("@SifraProizvoda", '1');
                             command.Parameters.Add("@NazivProizvoda", '1');
                             command.Parameters.Add("@IsporukaID", '1');
                             command.Parameters.Add("@DatumUtovara", '1');
                             command.Parameters.Add("@DatumIsporuke", '1');
                             command.Parameters.Add("@BrojUtovara", '1');
                             command.Parameters.Add("@BrojIsporuke", '1');
                             command.Parameters.Add("@VozacIDz",'1');
                             command.Parameters.Add("@Zavrseno", '1');
                             command.Parameters.Add("@NazivKomitenta", '1');
                        */
                             try
                            {
                                connection.Open();
                                command.ExecuteNonQuery();
                                //command.Parameters.Clear();
                                command.Dispose();
                            }
                            catch (SqlException)
                            {
                                // error here
                            }
                            finally
                            {
                                connection.Close();
                            }
                    
                    }
               
                }
                }
            }
        }

        private void btnInsertNakNalog_Click(object sender, EventArgs e)
        {
            int brojac = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // currQty += row.Cells["qty"].Value;
                //More code here
                var date2 = Convert.ToDateTime(txtDatumIsporuke.Text).ToString("yyyy-MM-dd");
                var date3 = Convert.ToDateTime(Convert.ToDateTime(row.Cells[6].Value.ToString())).ToString("yyyy-MM-dd");
                if (row != null && row.Cells[0].Value != null)
                {
                    if (brojac < 1)
                    {
                        InsertNakNalog inn = new InsertNakNalog();
                       // inn.InsertNakNalogV(Convert.ToDateTime(row.Cells[11].Value), row.Cells[0].Value.ToString());
                        inn.InsertNakNalogV(date2, txtVozilo.Text, date3, row.Cells[22].Value.ToString(), row.Cells[24].Value.ToString(), row.Cells[26].Value.ToString());
                        brojac = brojac + 1;
                    }

                }
            }
            //Vraca broj NakNaloga
     string constring = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
    int BrojNakNalog = 0;
    using (SqlConnection con = new SqlConnection(constring))
    {
        using (SqlCommand cmd = new SqlCommand("SelectNakNAlog", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaxBroj", SqlDbType.Int);
            cmd.Parameters["@MaxBroj"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            BrojNakNalog = Convert.ToInt32(cmd.Parameters["@MaxBroj"].Value.ToString());
        }
    }

          // Unosstavki
    foreach (DataGridViewRow row in dataGridView1.Rows)
    {
        // currQty += row.Cells["qty"].Value;
        //More code here
        if (row != null && row.Cells[0].Value != null)
        {
            
                InsertNakNalogP innp = new InsertNakNalogP();
                string kod = row.Cells[7].Value.ToString() + "-" + row.Cells[15].Value.ToString() + "-" + row.Cells[17].Value.ToString();
                innp.InsertNakNalogV(BrojNakNalog, row.Cells[8].Value.ToString(), "KOM", Convert.ToDouble(row.Cells[4].Value.ToString()), kod, row.Cells[20].Value.ToString(), Convert.ToInt32(row.Cells[10].Value.ToString()), row.Cells[11].Value.ToString(), Convert.ToInt32(row.Cells[3].Value.ToString()), row.Cells[13].Value.ToString(), row.Cells[21].Value.ToString(), Convert.ToInt32(row.Cells[22].Value.ToString()));
                
        }
    }
   
            MessageBox.Show("Završen uvoz");

        }

        private void btnSastavnica_Click(object sender, EventArgs e)
        {
            //Nadji sifru maticnog podatka na koda
            string constring = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            int SifraMP = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string kod = row.Cells[7].Value.ToString() + "-" + row.Cells[15].Value.ToString() + "-" + row.Cells[17].Value.ToString();
                using (SqlConnection con = new SqlConnection(constring))
                {
                    using (SqlCommand cmd = new SqlCommand("SelectMpSifra", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                       // cmd.Parameters.Add("@Kod", SqlDbType.Char,24);
                        cmd.Parameters.Add("@Kod", SqlDbType.Char,24);
                        cmd.Parameters["@Kod"].Value = kod; 
                       // cmd.Parameters.Add(kod);
                   

                        //
                        cmd.Parameters.Add("@Sifra", SqlDbType.Int);
                       // cmd.Parameters.AddWithValue(Value = BrojUtovara;
                        cmd.Parameters["@Sifra"].Direction = ParameterDirection.Output;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        SifraMP = Convert.ToInt32(cmd.Parameters["@Sifra"].Value.ToString());
                    }
                }

            }

            int brojac = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // currQty += row.Cells["qty"].Value;
                //More code here
                if (row != null && row.Cells[0].Value != null)
                {
                    if (brojac < 1)
                    {
                        var date2 = Convert.ToDateTime(txtDatumIsporuke.Text).ToString("yyyy-MM-dd");
                        var date3 = Convert.ToDateTime(Convert.ToDateTime(row.Cells[6].Value.ToString())).ToString("yyyy-MM-dd");
                        //var date2 = Date.ToString(Convert.ToDateTime(txtDatumIsporuke.Text));
                        //var date3 = Date.ToString(txtDatumIsporuke.Text);
                         //var date3 = Date.ToString(Convert.ToDateTime(row.Cells[6].Value.ToString()));
                        InsertNakNalog inn = new InsertNakNalog();
                        // inn.InsertNakNalogV(Convert.ToDateTime(row.Cells[11].Value), row.Cells[0].Value.ToString());
                       // inn.InsertNakNalogV(txtDatumIsporuke.Text, txtVozilo.Text, row.Cells[6].Value.ToString(), row.Cells[22].Value.ToString(), row.Cells[24].Value.ToString(), row.Cells[25].Value.ToString());
                        inn.InsertNakNalogV(date2, txtVozilo.Text, date3, row.Cells[22].Value.ToString(), row.Cells[24].Value.ToString(), row.Cells[25].Value.ToString());
                        //inn.InsertNakNalogV(Convert.ToDateTime(txtDatumIsporuke.Text), txtVozilo.Text, Convert.ToDateTime(row.Cells[6].Value.ToString()), row.Cells[22].Value.ToString(), row.Cells[24].Value.ToString(), "ddd");
                        brojac = brojac + 1;
                    }

                }
            }

            //Vraca broj NakNaloga
           // string constring = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            int BrojNakNalog = 0;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("SelectNakNAlog", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaxBroj", SqlDbType.Int);
                    cmd.Parameters["@MaxBroj"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    BrojNakNalog = Convert.ToInt32(cmd.Parameters["@MaxBroj"].Value.ToString());
                }
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // currQty += row.Cells["qty"].Value;
                //More code here
                if (row != null && row.Cells[0].Value != null)
                {
                    InsertNakNalogPP innp = new InsertNakNalogPP();
                    string kod = row.Cells[7].Value.ToString() + "-" + row.Cells[15].Value.ToString() + "-" + row.Cells[17].Value.ToString();
                    innp.InsertNakNalogV(BrojNakNalog, row.Cells[8].Value.ToString(), "KOM", Convert.ToDouble(row.Cells[4].Value.ToString()), SifraMP, row.Cells[20].Value.ToString(), Convert.ToInt32(row.Cells[10].Value.ToString()), row.Cells[11].Value.ToString(), Convert.ToInt32(row.Cells[2].Value.ToString()), row.Cells[12].Value.ToString(), row.Cells[20].Value.ToString(), Convert.ToInt32(row.Cells[21].Value.ToString()));

                }
            }


        }

        private void btnIzvuciStavku_Click(object sender, EventArgs e)
        {
            string connString =
          ConfigurationManager.AppSettings["webs"];
            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM NaloziZaUtovar where Vozilo = " + "'" + txtVozilo.Text + "' and DatumIsporuke =" + "DateValue('" + txtDatumIsporuke.Text + "')" +  " And SifraProizvoda = " + "'" + txtSifraProizvoda.Text + "'", conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(dataTableRes);
            }

            dataGridView1.DataSource = dataTableRes;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string constring = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            int SifraMP = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string kod = row.Cells[7].Value.ToString() + "-" + row.Cells[15].Value.ToString() + "-" + row.Cells[17].Value.ToString();
                using (SqlConnection con = new SqlConnection(constring))
                {
                    using (SqlCommand cmd = new SqlCommand("SelectMpSifra", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // cmd.Parameters.Add("@Kod", SqlDbType.Char,24);
                        cmd.Parameters.Add("@Kod", SqlDbType.Char, 24);
                        cmd.Parameters["@Kod"].Value = kod;
                        // cmd.Parameters.Add(kod);


                        //
                        cmd.Parameters.Add("@Sifra", SqlDbType.Int);
                        // cmd.Parameters.AddWithValue(Value = BrojUtovara;
                        cmd.Parameters["@Sifra"].Direction = ParameterDirection.Output;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        SifraMP = Convert.ToInt32(cmd.Parameters["@Sifra"].Value.ToString());
                    }
                }

            }
            
            int brojac = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // currQty += row.Cells["qty"].Value;
                //More code here
                string kod = row.Cells[7].Value.ToString() + "-" + row.Cells[15].Value.ToString() + "-" + row.Cells[17].Value.ToString();
                if (row != null && row.Cells[0].Value != null)
                {
                        InsertTanjaPregled intp = new InsertTanjaPregled();
                        intp.InsertTP(row.Cells[25].Value.ToString(), SifraMP.ToString(), kod, row.Cells[8].Value.ToString());
                        brojac = brojac + 1;
                }
            }
            TanjaPregled tp = new TanjaPregled();
            tp.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TanjaPregled tp = new TanjaPregled();
            tp.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

            DataTable dataTableRes = new DataTable();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("Delete FROM TanjaPregled", conn);
                conn.Open();
             
                cmd.ExecuteNonQuery();
                conn.Close();
            } 
        }
    }
}
