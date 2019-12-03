using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private Stopwatch stopwatch = new Stopwatch();
        private string path = @"C:\Users\Aleksey\Pictures\NGF.jpg";
        private int counter = 0;
        List<RGB[,]> rgbList = new List<RGB[,]>();

        public Form1()
        {
           
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

                bool checkAdd = false;
                int counterGor = 0,testcheck = 0;

                byte* FirstPixel = (byte*)bitmapData.Scan0;
                #region
                for (int i=0;i<heightPixel;i++)
                {
                    if ( i%10==0 || i==0)
                    {
                        checkAdd = true;
                    }
                    byte* currentLine = FirstPixel + (i* bitmapData.Stride);
                    
                    for (int j=0;j<widthBytes;j+=bytesPerPixel)
                    {
                        if (counterGor == 10)
                        {
                            counterGor = 0;
                        }
                        if (checkAdd && counterGor==0)
                      //  if (checkAdd && j%30==0)
                        {
                            rgbList.Add(new RGB[FIX_SIZE,FIX_SIZE]);

                            (rgbList[FindElement(i, j/3)])[IndexX(i), IndexY(j/3)] = new RGB(currentLine[j/3], currentLine[j/3 + 1], currentLine[j/3 + 2]);
                            counterGor++;
                            counter++;
                            testcheck++;
                        }
                        else
                            if ((checkAdd && counterGor!=0) || (!checkAdd) )
                            {
                            (rgbList[FindElement(i, j/3)])[IndexX(i), IndexY(j/3)] = new RGB(currentLine[j/3], currentLine[j/3 + 1], currentLine[j/3 + 2]);
                            counterGor++;
                                counter++;
                            testcheck++;
                            }

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
            label1.Text = counter.ToString();
            pictureBox2.Image = trueBmp;
            stopwatch.Stop();
            label2.Text = stopwatch.Elapsed.Milliseconds.ToString();

            //System.Threading.Thread.Sleep(1000);
            //SaveFile(trueBmp);
            
        }

        private int FindElement(int x, int y)
        {
            return (x / 10) * 100 + (y / 10);
        }
        private int IndexX( int i)
        {
            return i % 10;
        }
        private int IndexY(int j)
        {
            return j % 10;
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
            stopwatch.Start();
            //ChangeRgb(pictureBox1.Image);
            ChangeRgb(new Bitmap(path));
        }
        private void ShowMAtrix()
        {

        }

    }
}
