using System;
using System.Collections.Generic;
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

namespace image
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Create data array to hold source pixel data
        byte[] data;

        int stride;

        public MainWindow()
        {
            InitializeComponent();

            CopyBitmap();

            Doit(0);
           // Doit(10);

           // Repair();


        }

        private void CopyBitmap()
        {
            // get starting image
            BitmapSource source = myImage.Source as BitmapSource;

            // Calculate stride of source
            stride = source.PixelWidth * (source.Format.BitsPerPixel / 8);

            // Create data array to hold source pixel data
            if (data == null)
                data = new byte[stride * source.PixelHeight];

            // Copy source image pixels to the data array
            source.CopyPixels(data, stride, 0);

            // put in some pixels
            Random random = new Random();

            //for (int x=0;x<100; x++)
            //    SetDataPixel((int)random.Next((int)255), (int)random.Next((int)255), (int)random.Next((int)255), (int)random.Next((int)ActualWidth), 10);
        }

        private void Repair()
        {
            // get starting image
            BitmapSource source = myImage.Source as BitmapSource;

            // Calculate stride of source
            int stride = source.PixelWidth * (source.Format.BitsPerPixel / 8);

            // Create WriteableBitmap to copy the pixel data to.      
            WriteableBitmap target = new WriteableBitmap(
              source.PixelWidth,
              source.PixelHeight,
              source.DpiX, source.DpiY,
              source.Format, null);

            // Write the pixel data to the WriteableBitmap.
            target.WritePixels(
              new Int32Rect(0, 0, source.PixelWidth, source.PixelHeight),
              data, stride, 0);

            // Set the WriteableBitmap as the source for the <Image> element 
            // in XAML so you can see the result of the copy
            myImage.Source = target;

        }

        private void SetDataPixel(int red, int green, int blue, int xv, int yv)
        {
            int x;
            int y;

            // get starting image
            BitmapSource source = myImage.Source as BitmapSource;

            // Calculate stride of source
            int stride = source.PixelWidth * (source.Format.BitsPerPixel / 8);

            x = (xv*4) + yv*stride;

            data[x + 0] = (byte) blue;  // B
            data[x + 1] = (byte) green;    // G
            data[x + 2] = (byte) red;    // R
            data[x + 3] = 255;    // A
        }

        private void Doit(int offset)
        {
            // get starting image
            BitmapSource source = myImage.Source as BitmapSource;
            int stride = source.PixelWidth * (source.Format.BitsPerPixel / 8);

            //we can set colurs here
            for (int x = 0; x < source.PixelWidth; x ++)
            {
                SetDataPixel(255, 0, 0, x, 100);
                SetDataPixel(255,0, 0, x , 200 );
                //Setdata[x + 0] = 255;  // B
                //data[x + 1] = 0;    // G
                //data[x + 2] = 255;    // R
               // data[x + 3] = 255;    // A
            }

            //for (int x = 0; x < 1000; x++)
            //    SetDataPixel(255, 0, 0, (int)random.Next((int)ActualWidth), 10 );

            //for (int x = 0; x < 1000; x++)
            //    SetDataPixel(0, 255, 0, (int)random.Next((int)ActualWidth), 10);

            // Create WriteableBitmap to copy the pixel data to.      
            WriteableBitmap target = new WriteableBitmap(
              source.PixelWidth,
              source.PixelHeight,
              source.DpiX, source.DpiY,
              source.Format, null);

            // Write the pixel data to the WriteableBitmap.
            target.WritePixels(
              new Int32Rect(0, 0, source.PixelWidth, source.PixelHeight ),
              data, stride, 0);

            // Set the WriteableBitmap as the source for the <Image> element 
            // in XAML so you can see the result of the copy
            myImage.Source = target;
        }

        private void loaded(object sender, RoutedEventArgs e)
        {
            Doit(0);
        }
    }
}
