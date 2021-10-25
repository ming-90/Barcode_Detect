using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeDetector
{
    class Model
    {
        public List<OpenCvSharp.Point[]> Contour { get; set; }
        public string Barcode { get; set; }
        public int searchQty { get; set; }
    }
}
