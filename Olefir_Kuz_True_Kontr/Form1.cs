using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Olefir_Kuz_True_Kontr
{
    public partial class Form1 : Form
    {
        private const int FIX_SIZE = 10;
        List<RGB[,]> rgbList = new List<RGB[,]>();

        public Form1()
        {
            string path = @"C:\Users\asasa\Pictures\Test2.JPG";
            InitializeComponent();

            pictureBox1.Image = new Bitmap(path);
        }

        private void ChangeRgb (object bmp)
        {
            var trueBmp = (Bitmap)bmp;

            unsafe
            {
                BitmapData bitmapData = trueBmp.LockBits(new Rectangle(0, 0, trueBmp.Width, trueBmp.Height), ImageLockMode.ReadWrite, trueBmp.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(trueBmp.PixelFormat) / 8;
                int heightPixel = trueBmp.Height;
                int widthBytes = trueBmp.Width*bytesPerPixel;


                byte* FirstPixel = (byte*)bitmapData.Scan0;
                #region
                for (int i=0;i<heightPixel;i++)
                {

                    byte* currentLine = FirstPixel + (i* bitmapData.Stride);
                    
                    for (int j=0;j<widthBytes;j+=bytesPerPixel)
                    {

                        // currentLine[j] = currentLine[j];
                    }

                }
                #endregion
                #region
                //Parallel.For(0, heightPixel, y =>
                //  {
                //      byte* currentLine = FirstPixel + (y * bitmapData.Stride);

                //      for (int x = 0; x < widthBytes; x += bytesPerPixel)
                //      {
                //          int oldBlue = currentLine[x];
                //          int oldGreen = currentLine[x + 1];
                //          int oldRed = currentLine[x + 2];


                //          int avr = (oldBlue + oldGreen + oldRed) / 3;
                //          currentLine[x] = (byte)avr;
                //          currentLine[x + 1] = (byte)avr;
                //          currentLine[x + 2] = (byte)avr;


                //      }

                //  }
                //);
                #endregion
                trueBmp.UnlockBits(bitmapData);

            }
            pictureBox2.Image = trueBmp;
            //System.Threading.Thread.Sleep(1000);
            //SaveFile(trueBmp); 
            
        }

        private void SaveFile(Bitmap _bitmap)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Jpeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog.Title = "Save an Image File";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();

                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                        _bitmap.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        _bitmap.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        _bitmap.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(1000, "Message", "My Boy)", ToolTipIcon.Error);
            ChangeRgb(pictureBox1.Image);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
