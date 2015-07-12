using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZonBarcode
{
    public class FNSkuLabel : IDisposable,IComparable<FNSkuLabel>

    {
        public string ASIN { get; set; }
        public string FNSKU { get; set; }
        public string Title { get; set; }
        public string Condition {get;set;}
        public Double Height { get; set; }
        public Double Width { get; set; }
        public string displayVal { get { return this.ToString(); } }

        private Image img;
        
        public override string ToString()
        {
            return ASIN + ":" + FNSKU + ":" + Title;
        }


        public FNSkuLabel(string asin, string fnsku, string title, string condition, Double widthInches, Double heightInches)
        {

            ASIN = asin;
            FNSKU = fnsku;
            Title = title;
            Height = heightInches;
            Width = widthInches;
            Condition = condition;
        }

        public int CompareTo(FNSkuLabel other)
        {
            // Should be a null check here...
            return this.ASIN.CompareTo(other.ASIN);
        }

        public Image getImage()
        {

            
            int dpiX = 0;
            int dpiY = 0;

            Bitmap test = new Bitmap(100,100);
            using(Graphics g = Graphics.FromImage(test))
            {

                dpiX = (int)g.DpiX;
                dpiY = (int)g.DpiY;
            }

            Bitmap b = new Bitmap((int)Width * dpiX*3, (int)Height * dpiY*3);
            
             int barcodeHeight = 100;
             using (Graphics gfx = Graphics.FromImage(b))
             {
                 gfx.Clear(Color.White);
                 // get barcode
                 using (FNSKUBarcode bc = new FNSKUBarcode(FNSKU))
                 {

                     Image barcodeImage = bc.getBarCodeImage(3);
                     barcodeHeight = barcodeImage.Height;
                     gfx.DrawImage(barcodeImage, new Point(0, 2));

                 }


                 string labelToFit = Title;
                 bool fit = false;
                 int fontSize = 10;
                 var font = new Font("Veranda", fontSize);
                 int minFontSize = 6;
                 SizeF sizeText = new SizeF();

                 while (!fit)
                 {
                     // Create a font

                     sizeText = gfx.MeasureString(labelToFit, font);
                     if (sizeText.Width < b.Width)
                     {
                         fit = true;
                     }
                     else
                     {
                         if (fontSize > minFontSize)
                         {
                             fontSize--;
                             font = new Font("Verdana", fontSize);
                         }
                         else
                         {
                             string[] words = labelToFit.Split(' ');
                             if (words.Length > 1)
                             {
                                 if (words[words.Length / 2].Length <= 0)
                                 {

                                     labelToFit = labelToFit.Remove(labelToFit.Length / 2, 2);
                                 }
                                 else
                                 {
                                     labelToFit = labelToFit.Replace(words[words.Length / 2], "");
                                 }
                             }
                             else
                             {
                                 labelToFit = labelToFit.Remove(labelToFit.Length / 2, 1);

                             }
                         }
                     }
                 }

                 Double xOffsetToCenter = (b.Width - sizeText.Width) / 2;
                 
                 


                 gfx.DrawString(labelToFit, font, Brushes.Black, new PointF((float)xOffsetToCenter, barcodeHeight + 3));

                 sizeText = gfx.MeasureString(FNSKU, font);
                 xOffsetToCenter = (b.Width - sizeText.Width) / 2;
                 gfx.DrawString(FNSKU, font, Brushes.Black, new PointF((float)xOffsetToCenter, (float)barcodeHeight + (float)sizeText.Height + 6.0f));


                 sizeText = gfx.MeasureString(Condition, font);
                 xOffsetToCenter = (b.Width - sizeText.Width) / 2;
                 gfx.DrawString(Condition, font, Brushes.Black, new PointF((float)xOffsetToCenter, (float)barcodeHeight + (float)sizeText.Height * 2 + 8.0f));




             }
             return b;



        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (img != null) img.Dispose();
            }
        }


    }


}
