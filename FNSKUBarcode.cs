using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zen.Barcode;

namespace ZonBarcode
{
    public  class FNSKUBarcode : IDisposable
    {
        private string _FNSKU;
        private Image img;

        public FNSKUBarcode(string FNSKU) 
        {
            _FNSKU = FNSKU;
        }

        public Image getBarCodeImage(int scale)
        {
            if (img == null)
            {
                Code128BarcodeDraw x = BarcodeDrawFactory.Code128WithChecksum;

                img = x.Draw(_FNSKU, 30, scale);
            }
            return img;
            

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
