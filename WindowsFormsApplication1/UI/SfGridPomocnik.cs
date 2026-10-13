using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Events;

namespace WindowsFormsApplication1
{
    /// <summary>Opis jedne kolone (indeks u tabeli, naslov, sirina, vidljivost) - zamena za Columns[i].HeaderText/Width/Visible.</summary>
    public class Kol
    {
        public int Indeks;
        public string Naslov;   // null = ne menjaj
        public int Sirina;      // -1 = ne menjaj
        public int Vidljiva;    // -1 = ne menjaj, 0 = sakrij, 1 = prikazi
        public Kol(int indeks, string naslov, int sirina, int vidljiva)
        {
            Indeks = indeks; Naslov = naslov; Sirina = sirina; Vidljiva = vidljiva;
        }
    }

    /// <summary>Red grida sa istim "izgledom" kao DataGridViewRow (Cells[i].Value, Selected, Index) da stari kod ostane isti.</summary>
    public class SfRed
    {
        public DataRowView Stavka;
        public bool Selected;
        public int Index;
        public SfCelije Cells { get { return new SfCelije(Stavka); } }
        public object DataBoundItem { get { return Stavka; } }
    }

    public class SfCelije
    {
        private readonly DataRowView _r;
        public SfCelije(DataRowView r) { _r = r; }
        public SfCelija this[int i] { get { return new SfCelija(_r, i); } }
        public SfCelija this[string naziv] { get { return new SfCelija(_r, _r.Row.Table.Columns.IndexOf(naziv)); } }
    }

    public class SfCelija
    {
        private readonly DataRowView _r;
        private readonly int _i;
        public SfCelija(DataRowView r, int i) { _r = r; _i = i; }
        public object Value
        {
            get { return _r.Row[_i]; }
            set { _r.Row[_i] = value ?? DBNull.Value; }
        }
    }

    /// <summary>
    /// Pomocna klasa za Syncfusion SfDataGrid koji zamenjuje DataGridView (prikaz tabele, filter kao u Excelu).
    /// </summary>
    public static class SfGridPomocnik
    {
        /// <summary>Jednom po gridu, odmah posle InitializeComponent(): stil u duhu "G - Svetli minimal" + Excel filter.</summary>
        public static void Stilizuj(SfDataGrid g)
        {
            if (g == null) return;
            try
            {
                g.AllowEditing = false;
                g.AllowResizingColumns = true;
                g.AllowSorting = true;
                g.AllowFiltering = true; // filter u zaglavlju kolone, kao u Excelu
                g.AutoGeneratingColumn += Kolona;
                g.Style.HeaderStyle.BackColor = JelaTema.ZaglavljeGrida;
                g.Style.HeaderStyle.TextColor = JelaTema.TekstZaglavlja;
                g.Style.HeaderStyle.Font.Facename = "Segoe UI Semibold";
                g.Style.HeaderStyle.Font.Size = 9F;
                g.Style.CellStyle.Font.Facename = "Segoe UI";
                g.Style.CellStyle.Font.Size = 9.75F;
                g.Style.HeaderStyle.FilterIconColor = JelaTema.TekstZaglavlja;
                g.Style.HeaderStyle.SortIconColor = JelaTema.TekstZaglavlja;
                g.Style.HeaderStyle.HoverFilterIconColor = JelaTema.Tekst;
                g.Style.HeaderStyle.HoverSortIconColor = JelaTema.Tekst;
                g.Style.SelectionStyle.BackColor = JelaTema.Selekcija;
                g.Style.SelectionStyle.TextColor = JelaTema.Tekst;
            }
            catch (Exception)
            {
                // Stil je samo kozmetika.
            }
        }

        /// <summary>Postavlja izvor podataka; sirine kolona (po redosledu kolona tabele) su opcione.</summary>
        public static void Popuni(SfDataGrid g, DataTable tabela, params int[] sirine)
        {
            g.Tag = (sirine != null && sirine.Length > 0) ? sirine : null;
            Postavi(g, tabela);
        }

        /// <summary>Postavlja izvor podataka sa opisom kolona (naslov, sirina, vidljivost).</summary>
        public static void PopuniK(SfDataGrid g, DataTable tabela, params Kol[] kolone)
        {
            g.Tag = (kolone != null && kolone.Length > 0) ? kolone : null;
            Postavi(g, tabela);
        }

        private static void Postavi(SfDataGrid g, DataTable tabela)
        {
            g.DataSource = tabela;
            try
            {
                if (tabela != null && tabela.Rows.Count > 0) g.SelectedIndex = 0;
            }
            catch (Exception) { }
        }

        /// <summary>Vrednosti selektovanog reda (kao ItemArray) ili null ako nista nije selektovano.</summary>
        public static object[] Red(SfDataGrid g)
        {
            DataRowView r = g.SelectedItem as DataRowView;
            if (r == null) return null;
            return r.Row.ItemArray;
        }

        /// <summary>Svi redovi trenutnog prikaza (kao DataGridView.Rows), sa oznakom koji su selektovani.</summary>
        public static List<SfRed> Redovi(SfDataGrid g)
        {
            List<SfRed> lista = new List<SfRed>();
            if (g == null || g.View == null) return lista;
            HashSet<object> izabrani = new HashSet<object>();
            foreach (object o in g.SelectedItems) izabrani.Add(o);
            int i = 0;
            foreach (var zapis in g.View.Records)
            {
                DataRowView v = zapis.Data as DataRowView;
                if (v == null) continue;
                SfRed r = new SfRed();
                r.Stavka = v; r.Index = i++; r.Selected = izabrani.Contains(v);
                lista.Add(r);
            }
            return lista;
        }

        /// <summary>Samo selektovani redovi (kao DataGridView.SelectedRows).</summary>
        public static List<SfRed> Izabrani(SfDataGrid g)
        {
            return Redovi(g).Where(r => r.Selected).ToList();
        }

        /// <summary>Trenutni red (kao DataGridView.CurrentRow) ili null.</summary>
        public static SfRed Tekuci(SfDataGrid g)
        {
            DataRowView v = g.SelectedItem as DataRowView;
            if (v == null) return null;
            return Redovi(g).FirstOrDefault(r => r.Stavka == v);
        }

        private static void Kolona(object sender, AutoGeneratingColumnArgs e)
        {
            try { e.Column.AllowFiltering = true; e.Column.AllowSorting = true; } catch (Exception) { }
            try
            {
                SfDataGrid g = (SfDataGrid)sender;
                DataTable t = g.DataSource as DataTable;
                if (t == null) return;
                int i = t.Columns.IndexOf(e.Column.MappingName);
                if (i < 0) return;
                int[] w = g.Tag as int[];
                if (w != null && i < w.Length) e.Column.Width = w[i];
                Kol[] k = g.Tag as Kol[];
                if (k != null)
                {
                    foreach (Kol x in k)
                    {
                        if (x.Indeks != i) continue;
                        if (x.Naslov != null) e.Column.HeaderText = x.Naslov;
                        if (x.Sirina >= 0) e.Column.Width = x.Sirina;
                        if (x.Vidljiva == 0) e.Column.Visible = false;
                        if (x.Vidljiva == 1) e.Column.Visible = true;
                    }
                }
            }
            catch (Exception) { }
        }
    }
}
