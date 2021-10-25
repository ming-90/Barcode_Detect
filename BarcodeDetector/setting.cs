using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeDetector
{
    class setting
    {
        //이미지 회전
        public Mat Rotate(Mat src, int angle)
        {
            Mat rotate = new Mat(src.Size(), MatType.CV_8UC3);
            Mat matrix = Cv2.GetRotationMatrix2D(new Point2f(src.Width / 2, src.Height / 2), angle, 1);
            Cv2.WarpAffine(src, rotate, matrix, src.Size(), InterpolationFlags.Linear);
            return rotate;
        }
        //바코드 리사이즈
        public Mat resizeBarcode(Mat src)
        {
            int resizeWidth = (int)(200);
            int resizeHeight = (int)(200);
            Mat returnImg = new Mat();
            Cv2.Resize(src, returnImg, new OpenCvSharp.Size(resizeWidth, resizeHeight));
            return returnImg;
        }
        //캔버스 리사이즈
        public Mat resizeCanvas(Mat src)
        {
            Mat returnImg = new Mat();
            //Cv2.Resize(src, returnImg, new OpenCvSharp.Size(231, 309));
            Cv2.Resize(src, returnImg, new OpenCvSharp.Size(480, 270));
            return returnImg;
        }

        //큰 캔버스 리사이즈
        public Mat resizeBigCanvas(Mat src)
        {
            Mat returnImg = new Mat();
            Cv2.Resize(src, returnImg, new OpenCvSharp.Size(960, 540));
            return returnImg;
        }
    }
}
