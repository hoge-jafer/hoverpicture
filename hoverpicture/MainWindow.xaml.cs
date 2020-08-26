using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace hoverpicture
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ImageSource> images = new List<ImageSource>();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            string a = AppDomain.CurrentDomain.BaseDirectory + "test.mtsd";
            string b = AppDomain.CurrentDomain.BaseDirectory + "s000.mtsd";
            FileStream fsMyfile = new FileStream(a, FileMode.Open);
            BinaryReader brMyfile = new BinaryReader(fsMyfile);
            byte[] vs = brMyfile.ReadBytes(430000);
            test.Source = WriteableBitmapNew(vs, 500, 860, 96, 96, PixelFormats.Bgr24);
            images.Add(WriteableBitmapNewIndexed8(vs, 500, 860, 96, 96, PixelFormats.Bgr24));
            fsMyfile.Close();
            FileStream fsMyfiles = new FileStream(b, FileMode.Open);
            BinaryReader brMyfiles = new BinaryReader(fsMyfiles);
            byte[] vss = brMyfiles.ReadBytes(430000);
            //test.Source = WriteableBitmapNewIndexed8(vss, 500, 860, 96, 96, PixelFormats.Gray8);
            images.Add(WriteableBitmapNewIndexed8(vss, 500, 860, 96, 96, PixelFormats.Bgr24));
            fsMyfiles.Close();
        }

        public static WriteableBitmap WriteableBitmapNewIndexed8(byte[] imageByte, int pixelWidth, int pixelHeight, double dpiX, double dpiY, PixelFormat pixelFormat)
        {
            Debug.WriteLine("执行8位灰度图");
            WriteableBitmap writeableBitmap = new WriteableBitmap(pixelWidth, pixelHeight, dpiX, dpiY, PixelFormats.Bgr24, null);
            try
            {
                byte[] pixels = new byte[pixelWidth * pixelHeight * writeableBitmap.Format.BitsPerPixel / 8];
                Int32Rect rect = new Int32Rect(0, 0, pixelWidth, pixelHeight);
                writeableBitmap.Lock();
                int i = 0;
                for (int x = 0; x < writeableBitmap.PixelWidth; ++x)
                {
                    for (int y = 0; y < writeableBitmap.PixelHeight; ++y)
                    {
                        int pixeloffset = ((writeableBitmap.PixelHeight - y - 1) * writeableBitmap.PixelWidth + x) * writeableBitmap.Format.BitsPerPixel / 8;
                        byte d = imageByte[i++];
                        pixels[pixeloffset + 0] = d;
                        pixels[pixeloffset + 1] = d;
                        pixels[pixeloffset + 2] = d;
                    }
                }
                int strides = (writeableBitmap.PixelWidth * writeableBitmap.Format.BitsPerPixel) / 8;
                writeableBitmap.WritePixels(rect, pixels, strides, 0);
                writeableBitmap.AddDirtyRect(rect);
                writeableBitmap.Unlock();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("WriteableBitmapNew 执行8位灰度图：" + ex.Message);
            }
            return writeableBitmap;
        }

        public static WriteableBitmap WriteableBitmapNew(byte[] imageByte, int pixelWidth, int pixelHeight, double dpiX, double dpiY, PixelFormat pixelFormat)
        {
            Debug.WriteLine("执行24位灰度图");
            WriteableBitmap writeableBitmap = new WriteableBitmap(pixelWidth, pixelHeight, dpiX, dpiY, pixelFormat, null);
            try
            {
                byte[] pixels = new byte[pixelWidth * pixelHeight * writeableBitmap.Format.BitsPerPixel / 8];
                Int32Rect rect = new Int32Rect(0, 0, pixelWidth, pixelHeight);
                writeableBitmap.Lock();
                int i = 0;
                for (int x = 0; x < writeableBitmap.PixelWidth; ++x)
                {
                    for (int y = 0; y < writeableBitmap.PixelHeight; ++y)
                    {
                        int pixeloffset = ((writeableBitmap.PixelHeight - y - 1) * writeableBitmap.PixelWidth + x) * writeableBitmap.Format.BitsPerPixel / 8;
                        byte d = imageByte[i++];
                        pixels[pixeloffset + 0] = d;
                        pixels[pixeloffset + 1] = d;
                        pixels[pixeloffset + 2] = d;
                    }
                }
                int strides = (writeableBitmap.PixelWidth * writeableBitmap.Format.BitsPerPixel) / 8;
                writeableBitmap.WritePixels(rect, pixels, strides, 0);
                writeableBitmap.AddDirtyRect(rect);
                writeableBitmap.Unlock();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("WriteableBitmapNew：" + ex.Message);
            }
            return writeableBitmap;
        }
        static int index = 0;
        private void Kaishi_Click(object sender, RoutedEventArgs e)
        {
            //手动可以切换
            //index++;
            //if (index % 2 == 0)
            //{
            //    FileStream fsMyfiles = new FileStream(@"C:\Users\Administrator\Desktop\s000.mtsd", FileMode.Open);
            //    BinaryReader brMyfiles = new BinaryReader(fsMyfiles);
            //    byte[] vss = brMyfiles.ReadBytes(430000);
            //    test.Source = null;
            //    test.Source = WriteableBitmapNew(vss, 500, 860, 96, 96, PixelFormats.Bgr24);
            //    fsMyfiles.Close();
            //}
            //else
            //{
            //    FileStream fsMyfiles = new FileStream(@"C:\Users\Administrator\Desktop\test.mtsd", FileMode.Open);
            //    BinaryReader brMyfiles = new BinaryReader(fsMyfiles);
            //    byte[] vss = brMyfiles.ReadBytes(430000);
            //    test.Source = null;
            //    test.Source = WriteableBitmapNew(vss, 500, 860, 96, 96, PixelFormats.Bgr24);
            //    fsMyfiles.Close();
            //}




            //定时器不断的切换
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 5);
            dispatcherTimer.Start();

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //        Dispatcher.Invoke((EventHandler)delegate
            //{
            string a = AppDomain.CurrentDomain.BaseDirectory + "test.mtsd";
            string b = AppDomain.CurrentDomain.BaseDirectory + "s000.mtsd";
            for (int i = 0; i < images.Count; i++)
            {
                if (i == 0)
                {
                    FileStream fsMyfiles = new FileStream(a, FileMode.Open);
                    BinaryReader brMyfiles = new BinaryReader(fsMyfiles);
                    byte[] vss = brMyfiles.ReadBytes(430000);
                    test.Source = null;
                    test.Source = WriteableBitmapNew(vss, 500, 860, 96, 96, PixelFormats.Bgr24);
                    fsMyfiles.Close();
                }
                else
                {
                    FileStream fsMyfiles = new FileStream(b, FileMode.Open);
                    BinaryReader brMyfiles = new BinaryReader(fsMyfiles);
                    byte[] vss = brMyfiles.ReadBytes(430000);
                    test.Source = null;
                    test.Source = WriteableBitmapNew(vss, 500, 860, 96, 96, PixelFormats.Bgr24);
                    fsMyfiles.Close();
                }
                Debug.WriteLine("i：" + i);
            }
            //}, new object[2]);

        }
    }
}
