using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Reporting.WinForms;
using Microsoft.Office.Interop.Excel;
using System.Configuration;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class frmExcelImportPorudzbina : Form
    {
        public static string code = "frmExcelImportPorudzbina";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public frmExcelImportPorudzbina()
        {
            InitializeComponent();
            txtPutanja.Text = ConfigurationManager.AppSettings["UPDoExcellFajlaInstruktori"];
            IdGrupe();
            IdForme();
            PravoPristupa();
        }
        public string IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            //Sifarnici.frmLogovanje frm = new Sifarnici.frmLogovanje();         
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            int count = 0;

            while (dr.Read())
            {
                if (count == 0)
                {
                    niz = dr["IdGrupe"].ToString();
                    count++;
                }
                else
                {
                    niz = niz + "," + dr["IdGrupe"].ToString();
                    count++;
                }

            }
            conn.Close();
            return niz;
        }
        private int IdForme()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select IdForme from Forme where Rtrim(Code)=" + "'" + code + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idForme = Convert.ToInt32(dr["IdForme"].ToString());
            }
            conn.Close();
            return idForme;
        }

        private void PravoPristupa()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select * From GrupaForma Where IdGrupe in (" + niz + ") and IdForme=" + idForme;
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nemate prava za pristup ovoj formi", code);
                Pravo = false;
            }
            else
            {
                Pravo = true;
                while (reader.Read())
                {
                    insert = Convert.ToBoolean(reader["Upis"]);
                    if (insert == false)
                    {
                        //tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                        //tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                        //tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
        }
        private void btnImeFajla_Click(object sender, EventArgs e)
        {
            string PictureFolder = txtPutanja.Text;
            ofd1.InitialDirectory = PictureFolder;
            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                txtPutanja.Text = fbd.SelectedPath.ToString() + ofd1.FileName;

            }
        }

        private static string GetValue(dynamic cell)
        {
           // Excel = Microsoft.Office.Interop.Excel;
            string ret = string.Empty;
            if (null == cell) { return ret; }
            Excel.Range singleCell = cell as Excel.Range;
            if (null == singleCell) { return ret; }
            if (null != singleCell.Text)
            {
                ret = singleCell.Text as string;
            }
            if (null == ret) { return string.Empty; }
            return ret.Replace("\n", " "); // remove line break
        }

        private static string GetComment(dynamic cell)
        {
            string ret = string.Empty;
            if ((null == cell) || (null == cell.Comment)) { return ret; }
            ret = cell.Comment.Text as string;
            if (null == ret) { return string.Empty; }
            return ret.Replace("\n", " "); // remove line break
        }

        byte[] ReadFile(string sPath)
        {
            byte[] data = null;
            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            data = br.ReadBytes((int)numBytes);
            return data;

        }

        public static void AppendAllBytes(string path, byte[] bytes)
        {
            //argument-checking here.

            using (var stream = new FileStream(path, FileMode.Append))
            {
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        public static Encoding DetectEncoding(String fileName, out String contents)
        {
            // open the file with the stream-reader:
            using (StreamReader reader = new StreamReader(@"C:\Hello.txt", true))
            {
                // read the contents of the file into a string
                contents = reader.ReadToEnd();

                // return the encoding.
                return reader.CurrentEncoding;
            }
        }

        private string LatinicaToAscii(string InString)
        {
            string newString = string.Empty, charString;
            char ch;
            int charsCopied;

            for (int i = 0; i < InString.Length; i++)
            {
                charString = InString.Substring(i, 1);
                charString = charString.Normalize(NormalizationForm.FormKD);
                // If the character doesn't decompose, leave it as-is

                switch (charString)
                {
                    case ";":
                        charString = ";";
                        break;
                    case "А":
                        charString = "A";
                        break;
                    case "B":
                        charString = "B";
                        break;
                    case "C":
                        charString = "C";
                        break;
                    case "D":
                        charString = "D";
                        break;
                    case "Е":
                        charString = "E";
                        break;
                    case "F":
                        charString = "F";
                        break;
                    case "G":
                        charString = "G";
                        break;
                    case "Х":
                        charString = "X";
                        break;
                    case "H":
                        charString = "H";
                        break;
                    case "I":
                        charString = "I";
                        break;
                    case "Ј":
                        charString = "J";
                        break;
                    case "К":
                        charString = "K";
                        break;
                    case "L":
                        charString = "L";
                        break;
                    case "М":
                        charString = "M";
                        break;
                    case "N":
                        charString = "N";
                        break;
                    case "О":
                        charString = "O";
                        break;
                    case "P":
                        charString = "P";
                        break;
                    case "R":
                        charString = "R";
                        break;
                    case "S":
                        charString = "S";
                        break;
                    case "Т":
                        charString = "T";
                        break;
                    case "V":
                        charString = "V";
                        break;
                    case "Z":
                        charString = "Z";
                        break;
                    case "а":
                        charString = "a";
                        break;
                    case "b":
                        charString = "b";
                        break;
                    case "c":
                        charString = "c";
                        break;
                    case "d":
                        charString = "d";
                        break;
                    case "е":
                        charString = "e";
                        break;
                    case "f":
                        charString = "f";
                        break;
                    case "g":
                        charString = "g";
                        break;
                    case "х":
                        charString = "x";
                        break;
                    case "i":
                        charString = "i";
                        break;
                    case "ј":
                        charString = "j";
                        break;
                    case "k":
                        charString = "k";
                        break;
                    case "l":
                        charString = "l";
                        break;
                    case "m":
                        charString = "m";
                        break;
                    case "n":
                        charString = "n";
                        break;
                    case "o":
                        charString = "o";
                        break;
                    case "p":
                        charString = "p";
                        break;
                    case "r":
                        charString = "r";
                        break;
                    case "s":
                        charString = "s";
                        break;
                    case "t":
                        charString = "t";
                        break;
                    case "u":
                        charString = "u";
                        break;
                    case "v":
                        charString = "v";
                        break;
                    case "џ":
                        charString = "x";
                        break;
                    case "з":
                        charString = "z";
                        break;
                    case "š":
                        charString = "{";
                        break;
                    case "Š":
                        charString = "[";
                        break;
                    case "ž":
                        charString = "`";
                        break;
                    case "Ž":
                        charString = "@";
                        break;
                    case "Ć":
                        charString = "]";
                        break;
                    case "ć":
                        charString = "}";
                        break;
                    case "ђ":
                        charString = "|";
                        break;
                    case "Ђ":
                        charString = "\\";
                        break;
                    case "Њ":
                        charString = "W";
                        break;
                    case "њ":
                        charString = "w";
                        break;
                    case "Љ":
                        charString = "Q";
                        break;
                    case "љ":
                        charString = "q";
                        break;
                    case "Џ":
                        charString = "X";
                        break;
                    case "Č":
                        charString = "^";
                        break;
                    case "č":
                        charString = "~";
                        break;
                    case "#":
                        charString = "#";
                        break;


                    default:
                        break;
                }
                /* 
                 #
                 $
                 %
                 &
                 '
                 (
                 )
                 *
                 +
                 ,
                 -
                 .
                 /
                 0
                 1
                 2
                 3
                 4
                 5
                 6
                 7
                 8
                 9
                 :
                 ;
                 <
                 =
                 >
                 ?
                 * 
                  [
                 \
                 ]
                 ^
                 _
                 `
                
                 */






                if (charString.Length == 1)
                    newString += charString;
                else
                {
                    charsCopied = 0;
                    for (int j = 0; j < charString.Length; j++)
                    {
                        ch = charString[j];
                        // If the char is 7-bit ASCII, add
                        if (ch < 128)
                        {
                            newString += ch;
                            charsCopied++;
                        }
                    }
                    if (charsCopied == 0)
                        newString += InString.Substring(i, 1);
                }
            }
            return newString;




        }

        private string CirilicaToAscii(string InString)
        {
            string newString = string.Empty, charString;
            char ch;
            int charsCopied;

            for (int i = 0; i < InString.Length; i++)
            {
                charString = InString.Substring(i, 1);
                charString = charString.Normalize(NormalizationForm.FormKD);
                // If the character doesn't decompose, leave it as-is

                switch (charString)
                {
                    case ";":
                        charString = ";";
                        break;
                    case "А":
                        charString = "A";
                        break;
                    case "Б":
                        charString = "B";
                        break;
                    case "Ц":
                        charString = "C";
                        break;
                    case "Д":
                        charString = "D";
                        break;
                    case "Е":
                        charString = "E";
                        break;
                    case "Ф":
                        charString = "F";
                        break;
                    case "Г":
                        charString = "G";
                        break;
                    case "Х":
                        charString = "H";
                        break;
                    case "И":
                        charString = "I";
                        break;
                    case "Ј":
                        charString = "J";
                        break;
                    case "К":
                        charString = "K";
                        break;
                    case "Л":
                        charString = "L";
                        break;
                    case "М":
                        charString = "M";
                        break;
                    case "Н":
                        charString = "N";
                        break;
                    case "О":
                        charString = "O";
                        break;
                    case "П":
                        charString = "P";
                        break;
                    case "Р":
                        charString = "R";
                        break;
                    case "С":
                        charString = "S";
                        break;
                    case "Т":
                        charString = "T";
                        break;
                    case "У":
                        charString = "U";
                        break;
                    case "В":
                        charString = "V";
                        break;
                    case "З":
                        charString = "Z";
                        break;
                    case "а":
                        charString = "a";
                        break;
                    case "б":
                        charString = "b";
                        break;
                    case "ц":
                        charString = "c";
                        break;
                    case "д":
                        charString = "d";
                        break;
                    case "е":
                        charString = "e";
                        break;
                    case "ф":
                        charString = "f";
                        break;
                    case "г":
                        charString = "g";
                        break;
                    case "х":
                        charString = "h";
                        break;
                    case "и":
                        charString = "i";
                        break;
                    case "ј":
                        charString = "j";
                        break;
                    case "к":
                        charString = "k";
                        break;
                    case "л":
                        charString = "l";
                        break;
                    case "м":
                        charString = "m";
                        break;
                    case "н":
                        charString = "n";
                        break;
                    case "о":
                        charString = "o";
                        break;
                    case "п":
                        charString = "p";
                        break;
                    case "р":
                        charString = "r";
                        break;
                    case "с":
                        charString = "s";
                        break;
                    case "т":
                        charString = "t";
                        break;
                    case "у":
                        charString = "u";
                        break;
                    case "в":
                        charString = "v";
                        break;
                    case "џ":
                        charString = "x";
                        break;
                    case "з":
                        charString = "z";
                        break;
                    case "ш":
                        charString = "{";
                        break;
                    case "Ш":
                        charString = "[";
                        break;
                    case "ж":
                        charString = "`";
                        break;
                    case "Ж":
                        charString = "@";
                        break;
                    case "Ћ":
                        charString = "]";
                        break;
                    case "ћ":
                        charString = "}";
                        break;
                    case "ђ":
                        charString = "|";
                        break;
                    case "Ђ":
                        charString = "\\";
                        break;
                    case "Њ":
                        charString = "W";
                        break;
                    case "њ":
                        charString = "w";
                        break;
                    case "Љ":
                        charString = "Q";
                        break;
                    case "љ":
                        charString = "q";
                        break;
                    case "Џ":
                        charString = "X";
                        break;
                    case "Ч":
                        charString = "^";
                        break;
                    case "ч":
                        charString = "~";
                        break;
                    case "#":
                        charString = "#";
                        break;


                    default:
                        break;
                }
                /* 
                 #
                 $
                 %
                 &
                 '
                 (
                 )
                 *
                 +
                 ,
                 -
                 .
                 /
                 0
                 1
                 2
                 3
                 4
                 5
                 6
                 7
                 8
                 9
                 :
                 ;
                 <
                 =
                 >
                 ?
                 * 
                  [
                 \
                 ]
                 ^
                 _
                 `
                
                 */






                if (charString.Length == 1)
                    newString += charString;
                else
                {
                    charsCopied = 0;
                    for (int j = 0; j < charString.Length; j++)
                    {
                        ch = charString[j];
                        // If the char is 7-bit ASCII, add
                        if (ch < 128)
                        {
                            newString += ch;
                            charsCopied++;
                        }
                    }
                    if (charsCopied == 0)
                        newString += InString.Substring(i, 1);
                }
            }
            return newString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int brojac = 1;
            string fileName = txtPutanja.Text;
            string svfileName = txtPutanja.Text;
            string extractFile = svfileName;
            DateTime now = DateTime.Now;
            string datum = String.Format("{0:ddMMyyyyHHmmss}", now);
            bool greska = false;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Excel.Application xlApp = null;
            Excel.Workbook xlWorkbook = null;
            Excel.Range xlUsedRange = null;
            Excel.Range xlCells = null;
            try
            {
                xlApp = new Excel.Application();

                if (null == xlApp)
                {
                    MessageBox.Show("Excel could not be started. Check that your office installation and project references are correct.");
                    return;
                }
                bool openReadOnly = true;
                xlWorkbook = xlApp.Workbooks.Open(extractFile,
                    0, openReadOnly, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);
                if (null == xlWorkbook)
                {
                    MessageBox.Show(string.Format("Excel Workbook '{0}' could not be opened.", extractFile));
                }
                InsertPorudzbina porudzbina = new InsertPorudzbina();
                porudzbina.InsertPor(cboDatumUtovara.Text);
                foreach (Excel.Worksheet xlSheet in xlWorkbook.Worksheets)
                {
                    if (null != xlSheet)
                    {
                        Excel.Range usedRange = xlSheet.UsedRange;
                        if ((null != usedRange) && (null != usedRange.Cells))
                        {
                            xlCells = usedRange.Cells;
                            progressBar.Maximum = (usedRange.Rows.Count - 1);
                            for (int rowNumber = 1; rowNumber <= usedRange.Rows.Count; ++rowNumber)
                            {

                                if (rowNumber > 1)
                                {
                                    string LargoPaketID = "";//4  ---1
                                    string Kolicina = "";//2 ---2


                                    for (int colNumber = 1; colNumber <= usedRange.Columns.Count; ++colNumber)
                                    {
                                        dynamic cell = xlCells[rowNumber, colNumber];
                                        if (null != cell)
                                        {
                                            if (colNumber == 3)
                                            {
                                                LargoPaketID = GetValue(cell);

                                            }
                                            if (colNumber == 5)
                                            {
                                                Kolicina = GetValue(cell);
                                            }
                                            brojac = brojac + 1;
                                            progressBar.Value = (rowNumber - 1);

                                        } //End of IF
                                    } //End of for columns
                                    porudzbina.InsertPorudzbinaPostav(Convert.ToInt32(LargoPaketID), Convert.ToDecimal(Kolicina));
                                } // end of for row
                                } // End if 

                                

                            Marshal.FinalReleaseComObject(xlSheet);
                        } //end of if
                    }
                    xlWorkbook.Close(Type.Missing, Type.Missing, Type.Missing);
                    xlApp.Quit();
                }    
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
                if (null != ex.InnerException)
                {
                    MessageBox.Show("Inner Exception: " + ex.InnerException.Message);
                }
            }
            finally
            {
                if (null != xlUsedRange) { Marshal.FinalReleaseComObject(xlUsedRange); xlUsedRange = null; }
                if (null != xlCells) { Marshal.FinalReleaseComObject(xlCells); xlCells = null; }
                if (null != xlWorkbook) { Marshal.FinalReleaseComObject(xlWorkbook); xlWorkbook = null; }
                if (null != xlApp) { Marshal.FinalReleaseComObject(xlApp); xlApp = null; }
            }
            if (greska == true)
            {
                MessageBox.Show("Obrada je zavrsena nije uspeo uvoz", "Porudžbina", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

            }
            else
            {

                MessageBox.Show("Obrada je zavrsena uspeo je uvoz" , "Porudžbina", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

            }
            stopwatch.Stop();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void progressBar_Click(object sender, EventArgs e)
        {

        }

        private void cboDatumUtovara_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtPutanja_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
   
    }
}
