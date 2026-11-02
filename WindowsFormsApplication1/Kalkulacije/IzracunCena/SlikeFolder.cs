using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // ZAH-008: automatsko povezivanje slika sa Code (kao LIKE '%' + Code + '%').
    // Slika pripada proizvodu ciji se Code iz jela_products nalazi u imenu fajla; naziv proizvoda se ne koristi.
    // Znaci koji ne mogu u ime fajla (/ \ : * ? " < > |) su u imenu zamenjeni sa _ (P62A/GL -> P62A_GL).
    // Code mora biti cela rec (pre i posle njega nije slovo ni cifra), pa P2 ne pogadja P22A_GL;
    // ako ime sadrzi vise Code, vazi najduzi (V7-30-1K/6, a ne V7-30). Velika/mala slova i broj razmaka nisu bitni.
    // Fajl sa dodatkom "(2)" je dodatna slika istog Code.
    // Uzimaju se samo .jpg/.jpeg/.png iz svih podfoldera; putanja se cuva relativno prema korenu.
    public static class SlikeFolder
    {
        static readonly string[] Ekstenzije = { ".jpg", ".jpeg", ".jfif", ".png", ".bmp", ".gif" };
        public const string FilterDijaloga = "Slike (*.jpg;*.jpeg;*.jfif;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.jfif;*.png;*.bmp;*.gif";

        // pregled foldera se pamti za ceo rad programa (mrezno deljenje je sporo);
        // ZaboraviIndeks posle "Povezi slike" ili kad se dodaju nove slike
        static readonly object zakljucaj = new object();
        static string indeksKoren;
        static Dictionary<string, List<IzracunCenaBaza.SlikaFajl>> indeks;

        // slike iz foldera za jedan Code (prvi poziv pregleda ceo folder)
        public static List<IzracunCenaBaza.SlikaFajl> ZaCode(string koren, string code, Func<IEnumerable<string>> kodovi)
        {
            lock (zakljucaj)
            {
                if (indeks == null || indeksKoren != koren)
                {
                    indeks = Skeniraj(koren, kodovi())
                        .GroupBy(s => s.Code, StringComparer.OrdinalIgnoreCase)
                        .ToDictionary(g => g.Key, g => g.ToList(), StringComparer.OrdinalIgnoreCase);
                    indeksKoren = koren;
                }
                List<IzracunCenaBaza.SlikaFajl> l;
                return indeks.TryGetValue((code ?? "").Trim(), out l) ? l : new List<IzracunCenaBaza.SlikaFajl>();
            }
        }

        public static void ZaboraviIndeks()
        {
            lock (zakljucaj) indeks = null;
        }
        static readonly char[] Nedozvoljeni = Path.GetInvalidFileNameChars();

        // ime fajla slike (bez ekstenzije) za proizvod: NazivProizvoda + "  " + Code (Code je u imenu, pa se slika povezuje)
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

        // najduzi Code iz recnika (kljuc -> Code) koji se u imenu nalazi kao cela rec; null ako ga nema
        static string NadjiCode(string ime, Dictionary<string, string> kodovi, int najduzi)
        {
            string s = Kljuc(ime);
            string nadjen = null;
            int duzina = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (i > 0 && char.IsLetterOrDigit(s[i - 1])) continue;          // pocetak reci
                int max = Math.Min(s.Length, i + najduzi);
                for (int j = i + 1; j <= max; j++)
                {
                    if (j < s.Length && char.IsLetterOrDigit(s[j])) continue;   // kraj reci
                    if (j - i < duzina) continue;
                    string code;
                    if (kodovi.TryGetValue(s.Substring(i, j - i), out code))
                    {
                        nadjen = code;
                        duzina = j - i;
                    }
                }
            }
            return nadjen;
        }

        // kodovi: Code iz jela_products
        public static List<IzracunCenaBaza.SlikaFajl> Skeniraj(string koren, IEnumerable<string> kodovi)
        {
            var lista = new List<IzracunCenaBaza.SlikaFajl>();
            if (!Directory.Exists(koren)) return lista;
            string k = Path.GetFullPath(koren).TrimEnd('\\') + "\\";

            var poKljucu = new Dictionary<string, string>();
            foreach (var c in kodovi)
            {
                string code = (c ?? "").Trim();
                if (code.Length == 0 || code.Length > 30) continue;
                string kljuc = Kljuc(code);
                if (!poKljucu.ContainsKey(kljuc)) poKljucu[kljuc] = code;
            }
            int najduzi = poKljucu.Count == 0 ? 0 : poKljucu.Keys.Max(x => x.Length);

            foreach (var f in Fajlovi(k))
            {
                if (!Ekstenzije.Contains(Path.GetExtension(f).ToLowerInvariant())) continue;
                bool dodatna;
                string code = NadjiCode(OsnovnoIme(f, out dodatna), poKljucu, najduzi);
                string rel = f.Substring(k.Length);
                if (rel.Length > 400) continue;
                // bez Code u imenu: ime fajla ide u izvestaj "Slika bez Code u bazi"
                lista.Add(new IzracunCenaBaza.SlikaFajl
                {
                    Code = code ?? Path.GetFileNameWithoutExtension(f).Substring(0, Math.Min(30, Path.GetFileNameWithoutExtension(f).Length)),
                    Putanja = rel,
                    Dodatna = dodatna
                });
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
