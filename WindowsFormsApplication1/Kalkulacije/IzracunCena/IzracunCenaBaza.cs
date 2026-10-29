using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // ZAH-008: pristup bazi za izracun cena (IC_ tabele, pogledi i procedure iz SQL/IC_Cenovnici).
    public class IzracunCenaBaza
    {
        readonly string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        // uvoz i preracun troskova nad ~12.000 redova traju duze od podrazumevanih 30 s
        const int Timeout = 300;

        SqlCommand Procedura(SqlConnection conn, string naziv)
        {
            var cmd = new SqlCommand(naziv, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = Timeout;
            return cmd;
        }

        static SqlParameter Tabela(string naziv, string tip, DataTable vrednost)
        {
            var p = new SqlParameter(naziv, SqlDbType.Structured);
            p.TypeName = tip;
            p.Value = vrednost;
            return p;
        }

        static object Db(object vrednost)
        {
            return vrednost ?? DBNull.Value;
        }

        static decimal? Dec(DataRow r, string kolona)
        {
            return r.IsNull(kolona) ? (decimal?)null : Convert.ToDecimal(r[kolona]);
        }

        DataTable Upit(string sql, params SqlParameter[] parametri)
        {
            using (var conn = new SqlConnection(connect))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.CommandTimeout = Timeout;
                cmd.Parameters.AddRange(parametri);
                var dt = new DataTable();
                new SqlDataAdapter(cmd).Fill(dt);
                return dt;
            }
        }

        // izvrsi proceduru i vrati sve skupove redova koje vraca
        DataSet Izvrsi(string naziv, Action<SqlCommand> parametri)
        {
            using (var conn = new SqlConnection(connect))
            using (var cmd = Procedura(conn, naziv))
            {
                parametri(cmd);
                var ds = new DataSet();
                new SqlDataAdapter(cmd).Fill(ds);
                return ds;
            }
        }

        /* =====================================================================
           UCITAVANJE
           ===================================================================== */

        // lista cenovnika (vrsta: Opsti, Radni, Vazeci ili null za sve)
        public DataTable Cenovnici(string vrsta)
        {
            return Upit("SELECT * FROM dbo.vw_IC_Cenovnik WHERE @Vrsta IS NULL OR Vrsta = @Vrsta ORDER BY Vrsta, DatumKreiranja DESC",
                        new SqlParameter("@Vrsta", Db(vrsta)));
        }

        public DataRow Zaglavlje(int cenovnikID)
        {
            var dt = Upit("SELECT * FROM dbo.vw_IC_Cenovnik WHERE CenovnikID = @ID", new SqlParameter("@ID", cenovnikID));
            return dt.Rows.Count == 0 ? null : dt.Rows[0];
        }

        // stablo Program -> Kolekcija, sa brojem redova u cenovniku
        public DataTable ProgramiKolekcije(int cenovnikID)
        {
            return Upit(@"SELECT s.ProgramID, g.Naziv AS Program, s.KolekcijaID, k.Naziv AS Kolekcija, COUNT(*) AS BrojRedova
                          FROM dbo.IC_CenovnikStavka s
                          JOIN dbo.Program g   ON g.ID = s.ProgramID
                          JOIN dbo.Kolekcije k ON k.ID = s.KolekcijaID
                          WHERE s.CenovnikID = @ID
                          GROUP BY s.ProgramID, g.Naziv, s.KolekcijaID, k.Naziv
                          ORDER BY g.Naziv, k.Naziv",
                        new SqlParameter("@ID", cenovnikID));
        }

        // svi redovi cenovnika iz vw_IC_CenovnikObracun; posle toga forma racuna u C# (ObracunCene)
        public List<StavkaObracuna> Stavke(int cenovnikID)
        {
            var dt = Upit("SELECT * FROM dbo.vw_IC_CenovnikObracun WHERE CenovnikID = @ID ORDER BY Program, Kolekcija, LargoProizvodID",
                          new SqlParameter("@ID", cenovnikID));
            var lista = new List<StavkaObracuna>(dt.Rows.Count);
            foreach (DataRow r in dt.Rows)
            {
                lista.Add(new StavkaObracuna
                {
                    StavkaID = (int)r["StavkaID"],
                    LargoProizvodID = (int)r["LargoProizvodID"],
                    SifraProizvodaLargo = r["SifraProizvodaLargo"].ToString(),
                    Code = r["Code"].ToString(),
                    NazivProizvoda = r["NazivProizvoda"].ToString(),
                    ProgramID = (int)r["ProgramID"],
                    Program = r["Program"].ToString(),
                    KolekcijaID = (int)r["KolekcijaID"],
                    Kolekcija = r["Kolekcija"].ToString(),
                    Valuta = r["Valuta"].ToString(),
                    Kurs = Convert.ToDecimal(r["Kurs"]),
                    PDV = Convert.ToDecimal(r["PDV"]),
                    TrosakStari = Dec(r, "TrosakStari"),
                    TrosakNovi = Dec(r, "TrosakNovi"),
                    MarzaStara = Dec(r, "MarzaStara"),
                    BezTroskaUBazi = Convert.ToBoolean(r["BezTroska"]),
                    VPAktuelna = Dec(r, "VPAktuelna"),
                    PovecanjeVP = Convert.ToDecimal(r["PovecanjeVP"]),
                    VP = Convert.ToDecimal(r["VP"]),
                    VPIzBaze = Convert.ToBoolean(r["VPIzBaze"]),
                    Rabat = Convert.ToDecimal(r["Rabat"]),
                    Akcija = Dec(r, "Akcija")
                });
            }
            return lista;
        }

        public ParametriCenovnika Parametri(int cenovnikID)
        {
            var dt = Upit("SELECT * FROM dbo.IC_CenovnikParametar WHERE CenovnikID = @ID", new SqlParameter("@ID", cenovnikID));
            var ak = Upit(@"SELECT a.ParametarID, a.Procenat
                            FROM dbo.IC_CenovnikAkcija a
                            JOIN dbo.IC_CenovnikParametar p ON p.ParametarID = a.ParametarID
                            WHERE p.CenovnikID = @ID
                            ORDER BY a.ParametarID, a.Redosled",
                          new SqlParameter("@ID", cenovnikID));

            var par = new ParametriCenovnika();
            foreach (DataRow r in dt.Rows)
            {
                var p = new ParametarCenovnika
                {
                    Nivo = (NivoParametra)Enum.Parse(typeof(NivoParametra), r["Nivo"].ToString()),
                    ProgramID = r.IsNull("ProgramID") ? (int?)null : (int)r["ProgramID"],
                    KolekcijaID = r.IsNull("KolekcijaID") ? (int?)null : (int)r["KolekcijaID"],
                    Code = r.IsNull("Code") ? null : r["Code"].ToString(),
                    Rabat = Dec(r, "Rabat"),
                    Akcija = Dec(r, "Akcija")
                };
                int id = (int)r["ParametarID"];
                p.AkcijePoredjenje.AddRange(ak.AsEnumerable()
                    .Where(a => (int)a["ParametarID"] == id)
                    .Select(a => Convert.ToDecimal(a["Procenat"])));
                par.Dodaj(p);
            }
            return par;
        }

        public string GlavnaSlika(string code)
        {
            var dt = Upit("SELECT TOP (1) Putanja FROM dbo.IC_SlikaProizvoda WHERE Code = @Code ORDER BY Glavna DESC, SlikaID",
                          new SqlParameter("@Code", code));
            return dt.Rows.Count == 0 ? null : dt.Rows[0][0].ToString();
        }

        // novi programi i kolekcije iz jela_products (pre uvoza)
        public void OsveziProgrameIKolekcije()
        {
            Izvrsi("dbo.IC_OsveziProgrameIKolekcije", c => c.Parameters.AddWithValue("@BezRezultata", true));
        }

        public DataTable MaticniPodaci()
        {
            return Upit("SELECT LargoProizvodID, Code, ProgramID, Program, KolekcijaID, Kolekcija, CenaRSD, CenaEUR FROM dbo.vw_IC_MaticniPodatak");
        }

        public DataTable Partneri()
        {
            return Upit("SELECT PaSifra, PaNaziv FROM dbo.Partnerji ORDER BY PaNaziv");
        }

        /* =====================================================================
           TROSKOVI
           ===================================================================== */

        // vraca redove bez vazeceg troska
        public DataTable AzurirajTroskove(int cenovnikID)
        {
            return Izvrsi("dbo.IC_AzurirajTroskove", c => c.Parameters.AddWithValue("@CenovnikID", cenovnikID)).Tables[0];
        }

        // samo oznaceni redovi (StavkaID)
        public int BrojZaProglasenjeTroskova(int cenovnikID, IEnumerable<int> stavke, string korisnik)
        {
            return ProglasiTroskove(cenovnikID, stavke, korisnik, true);
        }

        public int ProglasiTroskoveNovim(int cenovnikID, IEnumerable<int> stavke, string korisnik)
        {
            return ProglasiTroskove(cenovnikID, stavke, korisnik, false);
        }

        int ProglasiTroskove(int cenovnikID, IEnumerable<int> stavke, string korisnik, bool samoBroj)
        {
            var dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            foreach (var id in stavke.Distinct()) dt.Rows.Add(id);

            var ds = Izvrsi("dbo.IC_ProglasiTroskoveNovim", c =>
            {
                c.Parameters.AddWithValue("@CenovnikID", cenovnikID);
                c.Parameters.AddWithValue("@Korisnik", korisnik);
                c.Parameters.Add(Tabela("@Stavke", "dbo.IC_IdTip", dt));
                c.Parameters.AddWithValue("@SamoBroj", samoBroj);
            });
            return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }

        /* =====================================================================
           CUVANJE
           ===================================================================== */

        public void SacuvajZaglavlje(int cenovnikID, decimal kurs, decimal pdv, string korisnik)
        {
            Izvrsi("dbo.IC_SacuvajZaglavlje", c =>
            {
                c.Parameters.AddWithValue("@CenovnikID", cenovnikID);
                c.Parameters.AddWithValue("@Kurs", kurs);
                c.Parameters.AddWithValue("@PDV", pdv);
                c.Parameters.AddWithValue("@Korisnik", korisnik);
            });
        }

        // izmenjene VP po Code (jedan red po Code, uzima se prvi red tog Code)
        public int SacuvajCene(int cenovnikID, IEnumerable<StavkaObracuna> izmenjeni, string korisnik)
        {
            var dt = new DataTable();
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("VPAktuelna", typeof(decimal));
            dt.Columns.Add("PovecanjeVP", typeof(decimal));
            dt.Columns.Add("VP", typeof(decimal));
            foreach (var g in izmenjeni.GroupBy(r => r.Code))
            {
                var r = g.First();
                dt.Rows.Add(r.Code, Db(r.VPAktuelna), r.PovecanjeVP, r.VP);
            }
            if (dt.Rows.Count == 0) return 0;

            var ds = Izvrsi("dbo.IC_SacuvajCene", c =>
            {
                c.Parameters.AddWithValue("@CenovnikID", cenovnikID);
                c.Parameters.Add(Tabela("@Cene", "dbo.IC_CenaTip", dt));
                c.Parameters.AddWithValue("@Korisnik", korisnik);
            });
            return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }

        static DataTable TabelaParametara(IEnumerable<ParametarCenovnika> parametri, bool sve)
        {
            var dt = new DataTable();
            dt.Columns.Add("RedBr", typeof(int));
            dt.Columns.Add("Nivo", typeof(string));
            dt.Columns.Add("ProgramID", typeof(int));
            dt.Columns.Add("KolekcijaID", typeof(int));
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Rabat", typeof(decimal));
            dt.Columns.Add("Akcija", typeof(decimal));
            dt.Columns.Add("PostaviRabat", typeof(bool));
            dt.Columns.Add("PostaviAkciju", typeof(bool));
            int rb = 1;
            foreach (var p in parametri)
            {
                bool rabat = sve || p.PromenjenRabat, akcija = sve || p.PromenjenaAkcija;
                if (!rabat && !akcija) continue;
                dt.Rows.Add(rb++, p.Nivo.ToString(), Db(p.ProgramID), Db(p.KolekcijaID), Db(p.Code),
                            Db(p.Rabat), Db(p.Akcija), rabat, akcija);
            }
            return dt;
        }

        // parametri sa oznakom PromenjenRabat / PromenjenaAkcija;
        // pregaziNizeNivoe = true za "Primeni na prikazane"
        public void SacuvajParametre(int cenovnikID, IEnumerable<ParametarCenovnika> parametri, string korisnik, bool pregaziNizeNivoe)
        {
            var dt = TabelaParametara(parametri, false);
            if (dt.Rows.Count == 0) return;

            Izvrsi("dbo.IC_SacuvajParametre", c =>
            {
                c.Parameters.AddWithValue("@CenovnikID", cenovnikID);
                c.Parameters.Add(Tabela("@Parametri", "dbo.IC_ParametarTip", dt));
                c.Parameters.AddWithValue("@Korisnik", korisnik);
                c.Parameters.AddWithValue("@PregaziNizeNivoe", pregaziNizeNivoe);
            });
        }

        /* =====================================================================
           RADNA KOPIJA I VAZECI
           ===================================================================== */

        public int NapraviRadniCenovnik(int izvorniCenovnikID, string naziv, string korisnik)
        {
            using (var conn = new SqlConnection(connect))
            using (var cmd = Procedura(conn, "dbo.IC_NapraviRadniCenovnik"))
            {
                cmd.Parameters.AddWithValue("@IzvorniCenovnikID", izvorniCenovnikID);
                cmd.Parameters.AddWithValue("@Naziv", naziv);
                cmd.Parameters.AddWithValue("@Korisnik", korisnik);
                var id = cmd.Parameters.Add("@CenovnikID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                conn.Open();
                cmd.ExecuteNonQuery();
                return (int)id.Value;
            }
        }

        // vraca: preuzete cene, preuzeti troskovi, novi redovi
        public DataRow PreuzmiIzmeneOpsteg(int cenovnikID, string korisnik)
        {
            return Izvrsi("dbo.IC_PreuzmiIzmeneOpsteg", c =>
            {
                c.Parameters.AddWithValue("@CenovnikID", cenovnikID);
                c.Parameters.AddWithValue("@Korisnik", korisnik);
            }).Tables[0].Rows[0];
        }

        public class RezultatProglasenja
        {
            public bool TrebaPotvrda;          // postoji isti naziv ili preklapanje perioda
            public DataTable Sporni;           // CenovnikID, Naziv, Valuta, PaSifra, VaziOd, VaziDo, Razlog
            public int? VazeciCenovnikID;
        }

        // prvi poziv sa prepisi = false; ako TrebaPotvrda, pitati korisnika i pozvati ponovo sa prepisi = true
        public RezultatProglasenja ProglasiVazecim(int radniCenovnikID, string naziv, DateTime vaziOd, DateTime? vaziDo,
                                                   int? paSifra, string korisnik, bool prepisi)
        {
            using (var conn = new SqlConnection(connect))
            using (var cmd = Procedura(conn, "dbo.IC_ProglasiVazecim"))
            {
                cmd.Parameters.AddWithValue("@RadniCenovnikID", radniCenovnikID);
                cmd.Parameters.AddWithValue("@Naziv", naziv);
                cmd.Parameters.Add("@VaziOd", SqlDbType.Date).Value = vaziOd.Date;
                cmd.Parameters.Add("@VaziDo", SqlDbType.Date).Value = vaziDo.HasValue ? (object)vaziDo.Value.Date : DBNull.Value;
                cmd.Parameters.Add("@PaSifra", SqlDbType.Int).Value = Db(paSifra);
                cmd.Parameters.AddWithValue("@Korisnik", korisnik);
                cmd.Parameters.AddWithValue("@Prepisi", prepisi);
                var id = cmd.Parameters.Add("@VazeciCenovnikID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                var rc = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                rc.Direction = ParameterDirection.ReturnValue;

                var sporni = new DataTable();
                new SqlDataAdapter(cmd).Fill(sporni);

                return new RezultatProglasenja
                {
                    TrebaPotvrda = rc.Value != DBNull.Value && (int)rc.Value == 1,
                    Sporni = sporni,
                    VazeciCenovnikID = id.Value == DBNull.Value ? (int?)null : (int)id.Value
                };
            }
        }

        public void ObrisiRadniCenovnik(int cenovnikID)
        {
            Izvrsi("dbo.IC_ObrisiRadniCenovnik", c => c.Parameters.AddWithValue("@CenovnikID", cenovnikID));
        }

        // zamrznute cene vazeceg cenovnika, sa nazivima iz stavki
        public DataTable VazeceCene(int cenovnikID)
        {
            return Upit(@"SELECT s.LargoProizvodID, s.SifraProizvodaLargo, v.Code, s.NazivProizvoda,
                                 g.Naziv AS Program, k.Naziv AS Kolekcija,
                                 v.TP, v.VP, v.VPsaPDV, v.Marza, v.Rabat, v.CenaPosleRabata, v.JelaMarza,
                                 v.Akcija, v.CenaAkcija, v.JelaMarzaAkcija
                          FROM dbo.IC_CenovnikVazecaCena v
                          JOIN dbo.IC_CenovnikStavka s ON s.CenovnikID = v.CenovnikID AND s.LargoProizvodID = v.LargoProizvodID
                          JOIN dbo.Program g   ON g.ID = s.ProgramID
                          JOIN dbo.Kolekcije k ON k.ID = s.KolekcijaID
                          WHERE v.CenovnikID = @ID
                          ORDER BY g.Naziv, k.Naziv, v.LargoProizvodID",
                        new SqlParameter("@ID", cenovnikID));
        }

        /* =====================================================================
           UVOZ IZ EXCELA
           ===================================================================== */

        public class UvozRed
        {
            public int LargoProizvodID;
            public string Code;
            public decimal? TrosakStari;   // TP - maj
            public decimal? MarzaStara;    // marza maj
        }

        public class RezultatUvoza
        {
            public int CenovnikID;
            public DataTable Preskoceni;   // maticni podaci bez Code, programa ili kolekcije
            public DataTable BezTroska;    // redovi bez vazeceg troska u KalkCenik
        }

        // cene: VP po Code iz Excela (prazno = sve cene iz jela_products);
        // parametri: rabat/akcija po nivou sa akcijama za poredjenje
        public RezultatUvoza UpisiUvoz(string naziv, string valuta, decimal kurs, decimal pdv, string korisnik,
                                       IDictionary<string, decimal> cene, IEnumerable<UvozRed> redovi,
                                       IEnumerable<ParametarCenovnika> parametri, bool samoIzExcela = false)
        {
            var dtCene = new DataTable();
            dtCene.Columns.Add("Code", typeof(string));
            dtCene.Columns.Add("VP", typeof(decimal));
            foreach (var c in cene) dtCene.Rows.Add(c.Key, c.Value);

            var dtRedovi = new DataTable();
            dtRedovi.Columns.Add("LargoProizvodID", typeof(int));
            dtRedovi.Columns.Add("Code", typeof(string));
            dtRedovi.Columns.Add("TrosakStari", typeof(decimal));
            dtRedovi.Columns.Add("MarzaStara", typeof(decimal));
            foreach (var r in redovi) dtRedovi.Rows.Add(r.LargoProizvodID, r.Code, Db(r.TrosakStari), Db(r.MarzaStara));

            var lista = parametri.ToList();
            var dtPar = TabelaParametara(lista, true);

            var dtAk = new DataTable();
            dtAk.Columns.Add("RedBr", typeof(int));
            dtAk.Columns.Add("Redosled", typeof(byte));
            dtAk.Columns.Add("Procenat", typeof(decimal));
            for (int i = 0; i < lista.Count; i++)
                for (int j = 0; j < lista[i].AkcijePoredjenje.Count; j++)
                    dtAk.Rows.Add(i + 1, (byte)(j + 1), lista[i].AkcijePoredjenje[j]);

            using (var conn = new SqlConnection(connect))
            using (var cmd = Procedura(conn, "dbo.IC_UpisiUvoz"))
            {
                cmd.Parameters.AddWithValue("@Naziv", naziv);
                cmd.Parameters.AddWithValue("@Valuta", valuta);
                cmd.Parameters.AddWithValue("@Kurs", kurs);
                cmd.Parameters.AddWithValue("@PDV", pdv);
                cmd.Parameters.AddWithValue("@Korisnik", korisnik);
                cmd.Parameters.Add(Tabela("@Cene", "dbo.IC_UvozCenaTip", dtCene));
                cmd.Parameters.Add(Tabela("@Stavke", "dbo.IC_UvozStavkaTip", dtRedovi));
                cmd.Parameters.Add(Tabela("@Parametri", "dbo.IC_ParametarTip", dtPar));
                cmd.Parameters.Add(Tabela("@Akcije", "dbo.IC_AkcijaTip", dtAk));
                var id = cmd.Parameters.Add("@CenovnikID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@SamoIzExcela", samoIzExcela);

                var ds = new DataSet();
                new SqlDataAdapter(cmd).Fill(ds);

                return new RezultatUvoza
                {
                    CenovnikID = (int)id.Value,
                    Preskoceni = ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable(),
                    BezTroska = ds.Tables.Count > 1 ? ds.Tables[1] : new DataTable()
                };
            }
        }

        /* =====================================================================
           SLIKE I PRESEK
           ===================================================================== */

        public class SlikaFajl
        {
            public string Code;
            public string Putanja;   // relativno prema korenskom folderu
            public bool Dodatna;     // "(2)" u imenu
        }

        // vraca: slike ciji Code nije u bazi (Tables[0]) i broj dodatih/obrisanih (Tables[1])
        public DataSet PoveziSlikeAutomatski(IEnumerable<SlikaFajl> slike, bool obrisiNepostojece)
        {
            var dt = new DataTable();
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Putanja", typeof(string));
            dt.Columns.Add("Dodatna", typeof(bool));
            foreach (var s in slike.GroupBy(x => new { x.Code, x.Putanja }).Select(g => g.First()))
                dt.Rows.Add(s.Code, s.Putanja, s.Dodatna);

            return Izvrsi("dbo.IC_PoveziSlikeAutomatski", c =>
            {
                c.Parameters.Add(Tabela("@Slike", "dbo.IC_SlikaTip", dt));
                c.Parameters.AddWithValue("@ObrisiNepostojece", obrisiNepostojece);
            });
        }

        public void DodeliSlikuRucno(string code, string putanja, bool glavna)
        {
            Izvrsi("dbo.IC_DodeliSlikuRucno", c =>
            {
                c.Parameters.AddWithValue("@Code", code);
                c.Parameters.AddWithValue("@Putanja", putanja);
                c.Parameters.AddWithValue("@Glavna", glavna);
            });
        }

        // Tables[0]: Code bez slike, Tables[1]: Code sa slikama bez glavne
        public DataSet IzvestajSlika()
        {
            return Izvrsi("dbo.IC_IzvestajSlika", c => { });
        }

        public void UpisiPresek(int cenovnikID, string fajl, string korisnik)
        {
            Izvrsi("dbo.IC_UpisiPresek", c =>
            {
                c.Parameters.AddWithValue("@CenovnikID", cenovnikID);
                c.Parameters.AddWithValue("@Fajl", fajl);
                c.Parameters.AddWithValue("@Korisnik", korisnik);
            });
        }
    }
}
