using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.ProizvodnjaUKadrovskoj
{
    class BojenjePoTipuRada
    {
        public Dictionary<string, Color> BojePoTipuRada = new Dictionary<string, Color>
        {
            //Bolovanje
            {"1", Color.FromArgb(255,0,0)},
            //Odmor
            {"2", Color.FromArgb(102,255,0)},
            // Službeni put
            {"3", Color.FromArgb(0,220,255)},
            //Plaćeni dani u celosti
            {"4", Color.FromArgb(255,255,0)},
            //Neplaćeni sati
            {"5",  Color.FromArgb(188,220,229)},
            //Opravdano kašnjenje
            {"6", Color.FromArgb(255,149,14)},
            //Neopravdano kašnjenje
            {"7",  Color.FromArgb(107,94,155)},
            // Duže plačeno odsustvo
            {"8", Color.FromArgb(63,175,70)},
            //Praznik
            {"9", Color.FromArgb(199,223,85)},

        };
    }
}
