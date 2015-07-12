using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;
//using BarcodeLib;

namespace ZonBarcode
{
    public class printLabel

    {
        PrintDocument pdoc = null;


        public FNSkuLabel label;

        public printLabel()
        {

        }
        public printLabel(FNSkuLabel fnskuLabel)
        {
            this.label = fnskuLabel;

        }
        public void print()
        {
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 15);
           

            PaperSize psize = new PaperSize("Custom", 300, 100);
            ps.DefaultPageSettings.PaperSize = psize;

            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
          
            pdoc.DefaultPageSettings.PaperSize = psize;

            
            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
           
            DialogResult result = pd.ShowDialog();
            if (result == DialogResult.OK)
            {
                PrintPreviewDialog pp = new PrintPreviewDialog();
                pp.Document = pdoc;

                result = pp.ShowDialog();
                if (result == DialogResult.OK)
                {
                    pdoc.Print();
                }
            }

        }
        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {


            Font textFont = new Font("Arial", 6);

            
            //BarcodeLib.Barcode bc = new Barcode();
            FNSKUBarcode bc = new FNSKUBarcode(label.FNSKU);

            Image i = bc.getBarCodeImage(1);
  
            Graphics graphics = e.Graphics;
          //  float fontHeight = font.GetHeight();
            int startX = 10;
            int startY = 10;
            int Offset = 10;
            //Offset = Offset + 20;
            graphics.DrawImage(i, new Point(startX, startY));
            Offset += i.Height;
            graphics.DrawString(label.FNSKU, textFont,
                              new SolidBrush(Color.Black), startX, startY + Offset);
            Offset += (int)textFont.GetHeight() + 2;


            graphics.DrawString(label.Title, textFont, 
                               new SolidBrush(Color.Black), startX, startY + Offset);
            

            
          
        }
    }
}

