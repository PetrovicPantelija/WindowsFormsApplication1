using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Markup;


namespace WindowsFormsApplication1.Barcode
{
    public class Statistika
    {
        //private string connectionString = "Data Source=192.168.1.6\\SQLEXPRESS2008K;Initial Catalog=Perftech2;User ID=sa;Password=duki7990;"; /*KP*/
        private string connectionString = "Data Source=192.168.1.3\\sql20082;Initial Catalog=Jela_Test;Encrypt=False;User ID=panta;Password=1234;"; /*JELA*/
        public class Prijemnica
        {
            public string Kreirao { get; set; }
            public DateTime DatumKreiranja { get; set; }
        }
        public List<Prijemnica> GetPrijemnica(int id)
        {
            var prijemnicaList = new List<Prijemnica>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Select NprVpisalIme From NPre WHere NPrStPre=@ID";
                    cmd.Parameters.AddWithValue("@ID", id);
                    string kreirao = (string)cmd.ExecuteScalar();

                    cmd.CommandText = "Select NprVpisalDat From NPre WHere NPrStPre=@ID";
                    DateTime datum = (DateTime)cmd.ExecuteScalar();

                    prijemnicaList.Add(new Prijemnica
                    {
                        Kreirao = kreirao,
                        DatumKreiranja = datum
                    });
                }
                conn.Close();
            }
            return prijemnicaList;
        }
        public class PrijemnicaStavke
        {
            public int Stavke { get; set; }
            public List<string> Magacioner { get; set; }
            public int Odbacena { get; set; }
            public string KorisnikOdbacio { get; set; }
            public DateTime PrijemPocetak { get; set; }
            public DateTime PrijemKraj { get; set; }
            public string VremePrijema { get; set; }
            public int PrimljenoTacno { get; set; }
            public int PrimljenoManje { get; set; }
            public int PrimljenoVise { get; set; }
            public DateTime PocetakUzimanja { get; set; }
            public DateTime KrajUlaganja { get; set;}
            public string VremeUzimanjaUlaganja { get; set; }
        }
        public List<PrijemnicaStavke> GetPrijemnicaStavke(int id)
        {

            var prijemnicaList = new List<PrijemnicaStavke>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "Select count(Nprpprezap) as Stavke from NPrep Where NPrPStPre=@ID";
                        cmd.Parameters.AddWithValue("@ID", id);
                        int stavke = (int)cmd.ExecuteScalar();

                        cmd.CommandText = "select distinct Korisnik from BarcodeLog Where Dokument='Prijemnica' and Oznaka=@ID";
                        var magacionerList = new List<string>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                magacionerList.Add(reader.GetString(0));
                            }
                        }

                        cmd.CommandText = "select IsNull(Count(ID),0) as Odbacena From BarcodeLog Where Oznaka=@ID and Dokument='Prijemnica-odbaci'";
                        int odbacena = (int)cmd.ExecuteScalar();

                        cmd.CommandText = "select IsNull(Korisnik,'') from BarcodeLog Where Oznaka=@ID and Dokument='Prijemnica-odbaci'";
                        string odbacio = (string)cmd.ExecuteScalar();

                        cmd.CommandText = "select IsNull(Min(Datum),'1900-01-01') as Pocetak from BarcodeLog Where Oznaka=@ID and Dokument='Prijemnica'";
                        DateTime pocetak = (DateTime)cmd.ExecuteScalar();

                        cmd.CommandText = "select IsNull(MAX(Datum),'1900-01-01') as Pocetak from BarcodeLog Where Oznaka=@ID and Dokument='Prijemnica'";
                        DateTime kraj = (DateTime)cmd.ExecuteScalar();

                        cmd.CommandText = "SELECT IsNULL(CAST(DATEDIFF(MINUTE, MIN(Datum), MAX(Datum)) / 60 AS VARCHAR) + 'h ' + CAST(DATEDIFF(MINUTE, MIN(Datum), MAX(Datum)) % 60 AS VARCHAR) + 'min',0) FROM BarcodeLog WHERE Oznaka = @ID AND Dokument = 'Prijemnica'";
                        string vremePrijema = (string)cmd.ExecuteScalar();

                        cmd.CommandText = "select IsNULL(count(Kolicina2-NprPkolPre),'') from NPreP Where (Kolicina2-NprPkolPre)=0 and NPrPStPre=@ID";
                        int primljeno = (int)cmd.ExecuteScalar();

                        cmd.CommandText = "select IsNULL(count(Kolicina2-NprPkolPre),'') from NPreP Where (Kolicina2-NprPkolPre)>0 and NPrPStPre=@ID";
                        int primljenoVise = (int)cmd.ExecuteScalar();

                        cmd.CommandText = "select ISNull(count(Kolicina2-NprPkolPre),0) from NPreP Where (Kolicina2-NprPkolPre)<0 and NPrPStPre=@ID";
                        int primljenoManje = (int)cmd.ExecuteScalar();

                        cmd.CommandText = "Select ISNull(Min(Datum),'1900-01-01') from BarcodeLog Where Oznaka=@ID and Dokument='Uzimanje iz prijemnice'";
                        DateTime uzimanje= (DateTime)cmd.ExecuteScalar();

                        cmd.CommandText = "Select ISNull(Max(Datum),'1900-01-01') from BarcodeLog Where Oznaka=@ID and Dokument='Ulaganje iz prijemnice'";
                        DateTime ulaganje = (DateTime)cmd.ExecuteScalar();

                        TimeSpan vremeUzimanja = ulaganje - uzimanje;
                        string vreme = string.Format("{0:D2}:{1:D2}", (int)vremeUzimanja.TotalHours, vremeUzimanja.Minutes);

                        prijemnicaList.Add(new PrijemnicaStavke
                        {
                            Stavke = stavke,
                            Magacioner = magacionerList,
                            Odbacena = odbacena,
                            KorisnikOdbacio = odbacio,
                            PrijemPocetak = pocetak,
                            PrijemKraj = kraj,
                            VremePrijema = vremePrijema,
                            PrimljenoTacno = primljeno,
                            PrimljenoVise = primljenoVise,
                            PrimljenoManje = primljenoManje,
                            PocetakUzimanja = uzimanje,
                            KrajUlaganja = ulaganje,
                            VremeUzimanjaUlaganja = vreme
                        });
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Greška prilikom učitavanja informacija" + ex.ToString());
            }
            return prijemnicaList;
        }
        public class UzimanjePrijemnica
        {
            public int ID { get; set; }
            public decimal Kolicina { get; set; }
            public decimal KolicinaUzeta { get; set; }
            public decimal KolicinaUlozena { get; set; }
        }
        public List<UzimanjePrijemnica> GetUzimanje(int prijemnica)
        {
            var uzimanje = new List<UzimanjePrijemnica>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select BarcodeLog.NPrPPreZap, NPreP.Kolicina2, Sum(Kolicina) " +
                        "from BarcodeLog " +
                        "inner join NPrep on BarcodeLog.NprPPreZap = NPrep.NPrPPreZap " +
                        "where Dokument = 'Uzimanje iz prijemnice' and Oznaka = @Prijemnica group by BarcodeLog.NPrPPreZap, NPreP.Kolicina2 " +
                        "order by BarcodeLog.NprPPreZap asc", conn))
                    {
                        cmd.Parameters.AddWithValue("@Prijemnica", prijemnica);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            var item = new UzimanjePrijemnica
                            {
                                ID = Convert.ToInt32(dr[0].ToString()),
                                Kolicina = Convert.ToDecimal(dr[1].ToString()),
                                KolicinaUzeta = Convert.ToDecimal(dr[2].ToString())
                            };
                            uzimanje.Add(item);
                        }
                    }
                }

                // Second query for 'Ulaganje iz prijemnice'
                using (SqlConnection conn2 = new SqlConnection(connectionString))
                {
                    conn2.Open();
                    using (SqlCommand cmd2 = new SqlCommand("select NPrPPreZap, Sum(Kolicina) from BarcodeLog " +
                        "where Dokument = 'Ulaganje iz prijemnice' and Oznaka = @Prijemnica group by NprPPreZap " +
                        "order by BarcodeLog.NprPPreZap asc", conn2))
                    {
                        cmd2.Parameters.AddWithValue("@Prijemnica", prijemnica);
                        SqlDataReader dr2 = cmd2.ExecuteReader();
                        while (dr2.Read())
                        {
                            int id = Convert.ToInt32(dr2[0].ToString());
                            decimal kolicinaUlozena = Convert.ToDecimal(dr2[1].ToString());

                            // Find the matching UzimanjePrijemnica in the list by ID and update KolicinaUlozena
                            var matchingItem = uzimanje.FirstOrDefault(x => x.ID == id);
                            if (matchingItem != null)
                            {
                                matchingItem.KolicinaUlozena = kolicinaUlozena;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }

            return uzimanje;
        }
        public void InsertPrometTrans(string Korisnik, DateTime Datum, int Referent,string Dokument)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "InsertPrometTransBarcode";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@Korisnik", SqlDbType.Char, 20) { Value = Korisnik });
                            cmd.Parameters.Add(new SqlParameter("@Datum", SqlDbType.DateTime) { Value = Datum });
                            cmd.Parameters.Add(new SqlParameter("@Referent", SqlDbType.Int) { Value = Referent });
                            cmd.Parameters.Add(new SqlParameter("@Dokument", SqlDbType.Char, 2) { Value=Dokument });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show("Neuspešan upis u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        System.Windows.Forms.MessageBox.Show(ex.ToString());
                    }
                }
                conn.Close();
            }
        }
        public int GetPrStDok()
        {
            int prstdok = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Select Max(PrTStDok) From PrometTrans", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        prstdok = Convert.ToInt32(dr[0].ToString());
                    }
                }
                conn.Close();
            }
            return prstdok;
        }
        public void InsertPrometTransPos(string Korisnik,DateTime Datum,int prStDokumenta,int Rb,int SkladIz,string LokacIz,int Mp,decimal Uzeto,decimal Ulozeno,int SkladU,string LokacU,string Dokument,string VrstaDokumenta,string VrstaPrometa)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "InsertPrometTransPosBarcode";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@Korisnik", SqlDbType.Char, 20) { Value = Korisnik });
                            cmd.Parameters.Add(new SqlParameter("@Datum", SqlDbType.DateTime) { Value = Datum });
                            cmd.Parameters.Add(new SqlParameter("@PrTpStDokumenta", SqlDbType.Int) { Value = prStDokumenta });
                            cmd.Parameters.Add(new SqlParameter("@RB", SqlDbType.Int) { Value = Rb });
                            cmd.Parameters.Add(new SqlParameter("@SifSkladIz", SqlDbType.Int) { Value = SkladIz });
                            cmd.Parameters.Add(new SqlParameter("@LokacIz", SqlDbType.Char,12) { Value = LokacIz });
                            cmd.Parameters.Add(new SqlParameter("@MP", SqlDbType.Int) { Value = Mp });
                            cmd.Parameters.Add(new SqlParameter("@Uzeto", SqlDbType.Decimal) { Value = Uzeto });
                            cmd.Parameters.Add(new SqlParameter("@Ulozeno", SqlDbType.Decimal) { Value = Ulozeno });
                            cmd.Parameters.Add(new SqlParameter("@SifSkladU", SqlDbType.Int) { Value = SkladU });
                            cmd.Parameters.Add(new SqlParameter("@LokacU", SqlDbType.Char,12) { Value = LokacU });
                            cmd.Parameters.Add(new SqlParameter("@Dokument", SqlDbType.Char, 2) { Value = Dokument });
                            cmd.Parameters.Add(new SqlParameter("@VrstaDokumenta", SqlDbType.Char, 3) { Value=VrstaDokumenta });
                            cmd.Parameters.Add(new SqlParameter("@VrstaPrometa", SqlDbType.Char, 3) { Value = VrstaPrometa });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show("Neuspešan upis u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        System.Windows.Forms.MessageBox.Show(ex.ToString());
                    }
                }
                conn.Close();
            }
        }
        public void UpdateUzimanje(int IDUzimanja,int Dokument)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "UzimanjeZavrsi";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = IDUzimanja });
                            cmd.Parameters.Add(new SqlParameter("@Dokument", SqlDbType.Int) { Value = Dokument });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show("Neuspešan upis u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        System.Windows.Forms.MessageBox.Show(ex.ToString());
                    }
                }
                conn.Close();
            }
        }
        public void UpdateUlaganje(int IDUlaganja, int Dokument)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "UlaganjeZavrsi";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = IDUlaganja });
                            cmd.Parameters.Add(new SqlParameter("@Dokument", SqlDbType.Int) { Value = Dokument });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show("Neuspešan upis u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        System.Windows.Forms.MessageBox.Show(ex.ToString());
                    }
                }
                conn.Close();
            }
        }
        public void UpdateTrebovanje(int IDTrebovanja, int Dokument)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "TrebovanjeZavrsi";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = IDTrebovanja });
                            cmd.Parameters.Add(new SqlParameter("@Dokument", SqlDbType.Int) { Value = Dokument });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show("Neuspešan upis u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        System.Windows.Forms.MessageBox.Show(ex.ToString());
                    }
                }
                conn.Close();
            }
        }

    }
}
