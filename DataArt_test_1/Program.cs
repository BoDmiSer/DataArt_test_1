using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataArt_test_1
{
    class Program
    {
        #region Properties
        public static string PathFile { get; private set; }
        public static string FileName { get; private set; } = "";

        public static string FullName { get; private set; } = "";
        #endregion

        #region Constructor
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                while (FullName == "")
                {
                    ReadFromTxt();
                }
            }

            ConvertToGray();

            Console.WriteLine("Save complited");
            Console.ReadKey();
        }
        #endregion

        #region Methods
        static void ReadFromTxt()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text documents (.jpg)|*.jpg";
                if (openFileDialog.ShowDialog() == true)
                {
                    PathFile = Path.GetDirectoryName(openFileDialog.FileName);
                    FileName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                    FullName = PathFile + "\\" + FileName;
                }
            }
            catch (Exception eReadFromTxt)
            {
                MessageBoxResult message = MessageBox.Show(eReadFromTxt.Message, "Error", MessageBoxButton.OK);
            }
        }

        static void ConvertToGray()
        {

            Bitmap myBitmap = new Bitmap(FullName+".jpg");
            int x, y;
            for (x = 0; x < myBitmap.Width; x++)
            {
                for (y = 0; y < myBitmap.Height; y++)
                {
                    Color getpixel = myBitmap.GetPixel(x, y);
                    myBitmap.SetPixel(x, y, ChangeColor(getpixel.R, getpixel.G, getpixel.B));
                }
            }
            myBitmap.Save(FullName + "-result" + ".jpg");
        }

        static Color ChangeColor(byte R, byte G, byte B)
        {
            byte gray = (byte)((R + G + B)/3);
            Color newColor = Color.FromArgb(gray, gray, gray);
            //Console.WriteLine(R + " " + G + " " + B + "          " + gray + " " + gray + " " + gray);
            return newColor;         
        }
        
        #endregion
    }
}
