using System;
using System.Diagnostics;
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Drawing;
using Zen.Barcode;

namespace ZonBarcode
{
    /// <summary>
    /// This sample is the obligatory Hello World program.
    /// </summary>
    public class Pdf
    {

        private const int minFontSize = 6;

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }


        public void create(Image img, string title, Double widthInches, Double heightInches)
        {


            PdfDocument document = new PdfDocument();
            document.Info.Title = title;

            PdfPage page = document.AddPage();
            page.Orientation = PageOrientation.Landscape;
            page.Height = XUnit.FromInch(heightInches);
            page.Width = XUnit.FromInch(widthInches);
            using (XGraphics gfx = XGraphics.FromPdfPage(page))
            {

                gfx.DrawImage(img, new Point(0, 0));


            }

            const string filename = "HelloWorld2.pdf";
            document.Save(filename);
            Process.Start(filename);



        }

        public void create(FNSkuLabel label, string condition, int copies, Double widthInches, Double heightInches)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = label.FNSKU;
            int ImageHeight = 0;






            for (int i = 0; i < copies; i++)
            {

                // Create an empty page
                PdfPage page = document.AddPage();
                page.Orientation = PageOrientation.Landscape;
                page.Height = XUnit.FromInch(heightInches);
                page.Width = XUnit.FromInch(widthInches);

                // Get an XGraphics object for drawing
                using (XGraphics gfx = XGraphics.FromPdfPage(page))
                {


                    using (FNSKUBarcode bc = new FNSKUBarcode(label.FNSKU))
                    {
                        var barCodeImage = bc.getBarCodeImage(1);


                        ImageHeight = barCodeImage.Height;

                        var xOffsetBcToCenter = (page.Width - barCodeImage.Width) / 2;
                        if (xOffsetBcToCenter < 0)
                        {
                            xOffsetBcToCenter = 0;
                        }
                        gfx.DrawImage(barCodeImage, new Point((int)xOffsetBcToCenter + 20, 2));

                    }


                    string labelToFit = label.Title;
                    bool fit = false;
                    int fontSize = 10;
                    var font = new XFont("Verdana", fontSize);
                    XSize sizeText = new XSize();

                    while (!fit)
                    {
                        // Create a font

                        sizeText = gfx.MeasureString(labelToFit, font);
                        if (sizeText.Width < page.Width)
                        {
                            fit = true;
                        }
                        else
                        {
                            if (fontSize > minFontSize)
                            {
                                fontSize--;
                                font = new XFont("Verdana", fontSize);
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

                    Double xOffsetToCenter = (page.Width - sizeText.Width) / 2;

                    gfx.DrawString(labelToFit, font, XBrushes.Black, new PointF((float)xOffsetToCenter, ImageHeight + 3));


                    sizeText = gfx.MeasureString(label.FNSKU, font);
                    xOffsetToCenter = (page.Width - sizeText.Width) / 2;
                    gfx.DrawString(label.FNSKU, font, XBrushes.Black, new PointF((float)xOffsetToCenter, (float)ImageHeight + (float)sizeText.Height + 6.0f));


                    sizeText = gfx.MeasureString(condition, font);
                    xOffsetToCenter = (page.Width - sizeText.Width) / 2;
                    gfx.DrawString(condition, font, XBrushes.Black, new PointF((float)xOffsetToCenter, (float)ImageHeight + (float)sizeText.Height * 2 + 8.0f));
                    // Draw the text
                    //gfx.DrawString(label.title, font, XBrushes.Black,
                    //new XRect(i.Height+1, 0, page.Width, page.Height),
                    //XStringFormats.Center);

                    // Save the document...


                }
            }
            const string filename = "HelloWorld.pdf";
            document.Save(filename);
            Process.Start(filename);
        }
        // ...and start a viewer.

    }
}

