using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // ZAH-008: automatsko povezivanje slika sa Code.
    // Ime fajla je "Naziv Code.jpg": Code je poslednja rec imena, a _ u njoj se cita kao /
    // (P62A_GL.jpg -> P62A/GL). Fajl sa dodatkom "(2)" je dodatna slika istog Code.
    // Uzimaju se samo .jpg/.jpeg/.png iz svih podfoldera; putanja se cuva relativno prema korenu.
    public static class SlikeFolder
    {
        static readonly string[] Ekstenzije = { ".jpg", ".jpeg", ".png" };

        public static string CodeIzImena(string imeFajla, out bool dodatna)
        {
            string ime = Path.GetFileNameWithoutExtension(imeFajla).Trim();
            var m = Regex.Match(ime, @"\s*\((\d+)\)\s*$");
            dodatna = m.Success;
            if (m.Success) ime = ime.Substring(0, m.Index).Trim();
            if (ime == "") return null;
            string rec = ime.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last();
            return rec.Replace('_', '/');
        }

        public static List<IzracunCenaBaza.SlikaFajl> Skeniraj(string koren)
        {
            var lista = new List<IzracunCenaBaza.SlikaFajl>();
            if (!Directory.Exists(koren)) return lista;
            string k = Path.GetFullPath(koren).TrimEnd('\\') + "\\";

            foreach (var f in Directory.EnumerateFiles(k, "*.*", SearchOption.AllDirectories))
            {
                if (!Ekstenzije.Contains(Path.GetExtension(f).ToLowerInvariant())) continue;
                bool dodatna;
                string code = CodeIzImena(f, out dodatna);
                if (string.IsNullOrEmpty(code) || code.Length > 30) continue;
                string rel = f.Substring(k.Length);
                if (rel.Length > 400) continue;
                lista.Add(new IzracunCenaBaza.SlikaFajl { Code = code, Putanja = rel, Dodatna = dodatna });
            }
            return lista;
        }
    }
}
