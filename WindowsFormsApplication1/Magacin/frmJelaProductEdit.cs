using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Magacin
{
    /// <summary>Dijalog za unos / izmenu jednog zapisa (bez designera - polja se prave iz opisa kolona).</summary>
    internal sealed class frmJelaProductEdit : Form
    {
        private readonly bool _isNew;
        private readonly Dictionary<string, TextBox> _boxes = new Dictionary<string, TextBox>();
        private readonly CheckBox _chkActive = new CheckBox();

        /// <summary>Vrednosti nakon OK (null = prazno polje).</summary>
        public Dictionary<string, object> Values { get; private set; }
        public bool Active { get { return _chkActive.Checked; } }

        /// <param name="current">null za novi zapis</param>
        public frmJelaProductEdit(Dictionary<string, object> current, bool active)
        {
            _isNew = current == null;
            Text = _isNew ? "Jela product - unos" : "Jela product - izmena";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(520, 690);
            Font = new Font("Segoe UI", 9F);

            var grid = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                ColumnCount = 2,
                Padding = new Padding(12, 12, 12, 0)
            };
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320));

            foreach (var f in JelaProductRepo.Fields)
            {
                var lbl = new Label
                {
                    Text = f.Name + (f.Required ? " *" : ""),
                    AutoSize = false,
                    Width = 160,
                    Height = 24,
                    TextAlign = ContentAlignment.MiddleRight
                };
                var box = new TextBox { Width = 310, Margin = new Padding(3, 3, 3, 3) };
                if (f.Type == JelaType.Text) box.MaxLength = f.Size;
                if (current != null) box.Text = JelaProductRepo.ToText(current[f.Name]);
                if (!_isNew && f.Name == JelaProductRepo.Key) box.ReadOnly = true;   // primarni kljuc se ne menja

                _boxes[f.Name] = box;
                grid.Controls.Add(lbl);
                grid.Controls.Add(box);
            }

            _chkActive.Text = "Active";
            _chkActive.AutoSize = true;
            _chkActive.Checked = _isNew ? true : active;
            grid.Controls.Add(new Label { Width = 160, Height = 24 });
            grid.Controls.Add(_chkActive);

            var bottom = new Panel { Dock = DockStyle.Bottom, Height = 48 };
            var btnOk = new Button { Text = "Snimi", Width = 100, Height = 30, Location = new Point(300, 9) };
            var btnCancel = new Button { Text = "Otkaži", Width = 100, Height = 30, Location = new Point(408, 9), DialogResult = DialogResult.Cancel };
            btnOk.Click += BtnOk_Click;
            bottom.Controls.Add(btnOk);
            bottom.Controls.Add(btnCancel);

            Controls.Add(grid);
            Controls.Add(bottom);
            AcceptButton = btnOk;
            CancelButton = btnCancel;
            JelaTema.Primeni(this);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            var vals = new Dictionary<string, object>();
            var errs = new List<string>();

            foreach (var f in JelaProductRepo.Fields)
            {
                object v;
                string err;
                if (JelaProductRepo.TryParse(f, _boxes[f.Name].Text, out v, out err)) vals[f.Name] = v;
                else errs.Add(f.Name + ": " + err);
            }

            if (errs.Count > 0)
            {
                MessageBox.Show(this, string.Join("\n", errs), "Proveri unos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Values = vals;
            DialogResult = DialogResult.OK;
        }
    }
}
