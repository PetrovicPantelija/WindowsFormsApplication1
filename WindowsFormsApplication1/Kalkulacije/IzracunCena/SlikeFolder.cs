using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // ZAH-008: automatsko povezivanje slika sa Code.
    // Ime fajla je NazivProizvoda + "  " + Code iz jela_products (npr. "VISECI ELEMENT  V7-30-1K_6.jpg");
    // znaci koji ne mogu u ime fajla (/ \ : * ? " < > |) su zamenjeni sa _.
    // Poredi se ceo naziv bez obzira na velika/mala slova i broj razmaka. Ako se ime ne poklopi
    // ni sa jednim proizvodom, vazi staro pravilo: Code je poslednja rec imena, a _ se cita kao /.
    // Fajl sa dodatkom "(2)" je dodatna slika istog Code.
    // Uzimaju se samo .jpg/.jpeg/.png iz svih podfoldera; putanja se cuva relativno prema korenu.
    public static class SlikeFolder
    {
        static readonly string[] Ekstenzije = { ".jpg", ".jpeg", ".png" };
        static readonly char[] Nedozvoljeni = Path.GetInvalidFileNameChars();

        // ime fajla slike (bez ekstenzije) za proizvod: NazivProizvoda + "  " + Code
        public static string ImeSlike(string nazivProizvoda, string code)
        {
            string ime = ((nazivProizvoda ?? "").Trim() + "  " + (code ?? "").Trim()).Trim();
            foreach (char c in Nedozvoljeni) ime = ime.Replace(c, '_');
            return ime;
        }

        // kljuc za poredjenje: nedozvoljeni znaci -> _, razmaci svedeni na jedan, velika slova
        static string Kljuc(string ime)
        {
            foreach (char c in Nedozvoljeni) ime = ime.Replace(c, '_');
            return Regex.Replace(ime, @"\s+", " ").Trim().ToUpperInvariant();
        }

        // ime bez ekstenzije i bez dodatka "(n)"
        static string OsnovnoIme(string imeFajla, out bool dodatna)
        {
            string ime = Path.GetFileNameWithoutExtension(imeFajla).Trim();
            var m = Regex.Match(ime, @"\s*\((\d+)\)\s*$");
            dodatna = m.Success;
            if (m.Success) ime = ime.Substring(0, m.Index).Trim();
            return ime;
        }

        // staro pravilo: poslednja rec imena, _ -> /
        public static string CodeIzImena(string imeFajla, out bool dodatna)
        {
            string ime = OsnovnoIme(imeFajla, out dodatna);
            if (ime == "") return null;
            string rec = ime.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last();
            return rec.Replace('_', '/');
        }

        // proizvodi: parovi (NazivProizvoda, Code) iz jela_products
        public static List<IzracunCenaBaza.SlikaFajl> Skeniraj(string koren, IEnumerable<KeyValuePair<string, string>> proizvodi)
        {
            var lista = new List<IzracunCenaBaza.SlikaFajl>();
            if (!Directory.Exists(koren)) return lista;
            string k = Path.GetFullPath(koren).TrimEnd('\\') + "\\";

            var poImenu = new Dictionary<string, string>();
            foreach (var p in proizvodi)
            {
                string kljuc = Kljuc(ImeSlike(p.Key, p.Value));
                if (!poImenu.ContainsKey(kljuc)) poImenu[kljuc] = p.Value.Trim();
            }

            foreach (var f in Fajlovi(k))
            {
                if (!Ekstenzije.Contains(Path.GetExtension(f).ToLowerInvariant())) continue;
                bool dodatna;
                string code;
                if (!poImenu.TryGetValue(Kljuc(OsnovnoIme(f, out dodatna)), out code))
                    code = CodeIzImena(f, out dodatna);
                if (string.IsNullOrEmpty(code) || code.Length > 30) continue;
                string rel = f.Substring(k.Length);
                if (rel.Length > 400) continue;
                lista.Add(new IzracunCenaBaza.SlikaFajl { Code = code, Putanja = rel, Dodatna = dodatna });
            }
            return lista;
        }

        // svi fajlovi iz foldera i podfoldera; folder bez prava citanja (cesto na mreznom deljenju)
        // se preskace umesto da prekine ceo pregled
        static IEnumerable<string> Fajlovi(string koren)
        {
            var folderi = new Stack<string>();
            folderi.Push(koren);
            while (folderi.Count > 0)
            {
                string folder = folderi.Pop();
                string[] fajlovi, podfolderi;
                try
                {
                    fajlovi = Directory.GetFiles(folder);
                    podfolderi = Directory.GetDirectories(folder);
                }
                catch (UnauthorizedAccessException) { continue; }
                catch (IOException) { continue; }
                foreach (var f in fajlovi) yield return f;
                foreach (var p in podfolderi) folderi.Push(p);
            }
        }
    }
}
