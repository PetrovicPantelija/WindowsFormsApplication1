using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using TextBox = System.Windows.Forms.TextBox;

namespace WindowsFormsApplication1.Pakovanja
{
    public class General
    {
        public static bool IsInt(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsDecimal(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '-')
            {
                return true;
            }
            if (e.KeyChar == ',' && (sender as TextBox).Text.IndexOf(',') > -1)
            {
                return true;
            }
            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
            {
                return true;
            }
            return false;
        }


    }
}
