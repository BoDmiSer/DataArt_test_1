using ImageMagick;
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
        public static string ExtensionFile { get; private set; } = "";
        public static string FullName { get; private set; } = "";

        #endregion

        #region Constructor
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                ConvertToGray(args[0]);
            }
            else
            {
                while (FullName == "")
                {
                    ReadFromTxt();
                }
            }
            ConvertToGray("");
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
                    ExtensionFile = Path.GetExtension(openFileDialog.FileName);
                    FullName = Path.GetDirectoryName(openFileDialog.FileName) + "\\"
                               + Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                }
            }
            catch (Exception eReadFromTxt)
            {
                MessageBoxResult message = MessageBox.Show(eReadFromTxt.Message, "Error", MessageBoxButton.OK);
            }
        }

        static void ConvertToGray(string path)
        {
            if (path != "")
            {
                string[] partPath;
                partPath = path.Split('.');
                FullName = partPath[0];
                ExtensionFile = '.'+partPath[1];
            }
            using (var image = new MagickImage(FullName + ExtensionFile))
            {
                image.ColorSpace = ColorSpace.Gray;
                image.Write(FullName + "-result" + ExtensionFile);
            }
        }
        #endregion
    }
}
