using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

//opencv(설치) 
using OpenCvSharp;
//Xzing(설치)
using ZXing;
//ini
using System.Runtime.InteropServices;
//time
using System.Diagnostics;
//using ZXing.Common;

namespace BarcodeDetector
{
    public partial class Form1 : Form
    {
        private Mat originalImg;
        private Mat Loadimg;
        private string sImagePath;
        private VideoCapture capture;
        private Mat video = new Mat();
        private BarcodeReader barcodeReader = new BarcodeReader { AutoRotate = true, TryInverted = true };

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public Form1()
        {
            InitializeComponent();
            setting();
            capture = new VideoCapture();
            //초기 이미지 불러서(저장된 경로) 출력
            if(sImagePath != "")
            {
                Loadimg = Cv2.ImRead(sImagePath);
                originalImg = Cv2.ImRead(sImagePath); 
                CaptureImage.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(Loadimg);
            }
        }

        private void setting()
        {
            //ini 파일 내용 읽고 초기 셋팅
            StringBuilder RESIZE = new StringBuilder(255);
            StringBuilder CONTRACT = new StringBuilder(255);
            StringBuilder MOPHOLOGY = new StringBuilder(255);
            StringBuilder OTG = new StringBuilder(255);
            StringBuilder ERODE = new StringBuilder(255);
            StringBuilder IMAGEPATH = new StringBuilder(255);
            StringBuilder BOXSIZE = new StringBuilder(255);
            StringBuilder PADDING = new StringBuilder(255);

            GetPrivateProfileString("SETTING", "RESIZE", "", RESIZE, 255, Application.StartupPath + @"\setting.ini");
            GetPrivateProfileString("SETTING", "CONTRACT", "", CONTRACT, 255, Application.StartupPath + @"\setting.ini");
            GetPrivateProfileString("SETTING", "MOPHOLOGY", "", MOPHOLOGY, 255, Application.StartupPath + @"\setting.ini");
            GetPrivateProfileString("SETTING", "OTG", "", OTG, 255, Application.StartupPath + @"\setting.ini");
            GetPrivateProfileString("SETTING", "ERODE", "", ERODE, 255, Application.StartupPath + @"\setting.ini");
            GetPrivateProfileString("SETTING", "BOXSIZE", "", BOXSIZE, 255, Application.StartupPath + @"\setting.ini");
            GetPrivateProfileString("SETTING", "PADDING", "", PADDING, 255, Application.StartupPath + @"\setting.ini");

            GetPrivateProfileString("SETTING", "IMAGEPATH", "", IMAGEPATH, 255, Application.StartupPath + @"\setting.ini");

            txtContrast.Text = CONTRACT.ToString();
            txtMophology.Text = MOPHOLOGY.ToString();
            txtOtg.Text = OTG.ToString();
            txtErode.Text = ERODE.ToString();
            txtSize.Text = BOXSIZE.ToString();
            txtPadding.Text = PADDING.ToString();

            sImagePath = IMAGEPATH.ToString();
        }

        #region ==== 버튼 이벤트 ====
        //학습
        private void btnLearn_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            Loadimg = Cv2.ImRead(sImagePath);

            //stop watch
            lblTime.Text = "0 초";
            stopwatch.Start();

            OpenCvSharp.Point[][] contours = barcodeDetect(Loadimg);
            learningBoxsize(Loadimg, contours);

            stopwatch.Stop();
            lblTime.Text = String.Format("{0:0.0000}", (double)stopwatch.ElapsedMilliseconds / 1000) + " 초";
        }

        //웹캠
        public void btnWebcam_Click(object sender, EventArgs e)
        {
            Loadimg = Cv2.ImRead(sImagePath);
            capture.Open(0);
            capture.FrameWidth = 640;
            timer1.Enabled = true;
            insertText("웹캠 시작");
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            insertText("웹캠 중지");
        }
        //웹캠 타이머
        private void timer1_Tick(object sender, EventArgs e)
        {
            capture.Read(video);
            CaptureImage.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(resizeCanvas(video));
            Loadimg = resizeCanvas(video);
            OpenCvSharp.Point[][] contours = barcodeDetect(Loadimg);
        }

        //이미지 불러오기 
        private void LoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sImagePath = dlg.FileName;
                Loadimg = Cv2.ImRead(dlg.FileName);
                originalImg = Loadimg;
            }

            WritePrivateProfileString("SETTING", "IMAGEPATH", dlg.FileName, Application.StartupPath + @"\setting.ini");
            CaptureImage.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(Loadimg);
        }
        //디텍팅 시작
        private void detectStart_Click(object sender, EventArgs e)
        {
            Loadimg = Cv2.ImRead(sImagePath);
            Stopwatch stopwatch = new Stopwatch();
            //stop watch
            stopwatch.Start();

            OpenCvSharp.Point[][] contours = barcodeDetect(Loadimg);
            int totalContours = contours.Count();
            if (chkDecode.Checked)
            {
                List<OpenCvSharp.Point[]> contourList1 = new List<OpenCvSharp.Point[]>();
                List<OpenCvSharp.Point[]> contourList2 = new List<OpenCvSharp.Point[]>();
                List<OpenCvSharp.Point[]> contourList3 = new List<OpenCvSharp.Point[]>();
                OpenCvSharp.Point[][] one = new ArraySegment<OpenCvSharp.Point[]>(contours, 0, totalContours / 2).ToArray();
                OpenCvSharp.Point[][] two = new ArraySegment<OpenCvSharp.Point[]>(contours, totalContours / 2, totalContours/2).ToArray();
                Thread thread1 = new Thread(() => contourList1 = decodeBarcode(Loadimg, one));
                thread1.Start();
                Thread thread2 = new Thread(() => contourList2 = decodeBarcode(Loadimg, two));
                thread2.Start();

                thread1.Join();
                thread2.Join();
                contourList1.AddRange(contourList2);
                contourList1.AddRange(contourList3);
                drawSqure(originalImg, contourList1.ToArray());
            }
            else
            {
                drawSqure(Loadimg, contours);
            }

            stopwatch.Stop();
            lblTime.Text = (stopwatch.ElapsedMilliseconds / 1000).ToString() + "ms";
            lblTime.Text = String.Format("{0:0.0000}", (double)stopwatch.ElapsedMilliseconds / 1000) + " 초";
        }

        #endregion ==== 버튼 이벤트 ====

        #region ==== 세팅 함수 ====
        //리사이즈
        private Mat resizeImage(Mat src, double ratio)
        {
            int resizeWidth = (int)(Loadimg.Width * ratio);
            int resizeHeight = (int)(Loadimg.Height * ratio);
            Mat returnImg = new Mat();
            Cv2.Resize(src, returnImg, new OpenCvSharp.Size(resizeWidth, resizeHeight));
            return returnImg;
        }

        //바코드 리사이즈
        private Mat resizeBarcode(Mat src)
        {
            int resizeWidth = (int)(200);
            int resizeHeight = (int)(200);
            Mat returnImg = new Mat();
            Cv2.Resize(src, returnImg, new OpenCvSharp.Size(resizeWidth, resizeHeight));
            return returnImg;
        }
        private Mat resizeCanvas(Mat src)
        {
            Mat returnImg = new Mat();
            Cv2.Resize(src, returnImg, new OpenCvSharp.Size(231, 309 ));
            return returnImg;
        }

        private Mat resizeBigCanvas(Mat src)
        {
            Mat returnImg = new Mat();
            Cv2.Resize(src, returnImg, new OpenCvSharp.Size(504, 960));
            string test;
            return returnImg;
        }

        #endregion ==== 세팅 함수 ====

        #region ==== 바코드 위치 찾기 ====
        private OpenCvSharp.Point[][] barcodeDetect(Mat img)
        {
            OpenCvSharp.Point[][] contours = null;

            try
            {
                //ini 파일에 설정값 저장
                WritePrivateProfileString("SETTING", "CONTRACT", txtContrast.Text, Application.StartupPath + @"\setting.ini");
                WritePrivateProfileString("SETTING", "MOPHOLOGY", txtMophology.Text, Application.StartupPath + @"\setting.ini");
                WritePrivateProfileString("SETTING", "OTG", txtOtg.Text, Application.StartupPath + @"\setting.ini");
                WritePrivateProfileString("SETTING", "ERODE", txtErode.Text, Application.StartupPath + @"\setting.ini");
                WritePrivateProfileString("SETTING", "PADDING", txtPadding.Text, Application.StartupPath + @"\setting.ini");
                WritePrivateProfileString("SETTING", "BOXSIZE", txtSize.Text, Application.StartupPath + @"\setting.ini");

                //리사이즈 
                Mat resizeImg = img;

                //그레이스케일
                Mat gray = new Mat();
                Cv2.CvtColor(resizeImg, gray, ColorConversionCodes.BGR2GRAY);
                CaptureImage2.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(resizeCanvas(gray));

                //명암 조절
                string getContrast = txtContrast.Text;
                gray = gray + Convert.ToInt32(getContrast);
                CaptureImage3.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(resizeCanvas(gray));

                //가장자리 검출
                Mat gradX = new Mat();
                Mat gradY = new Mat();
                // - x,y 미분
                Cv2.Sobel(gray, gradX, MatType.CV_32F, 1, 0, ksize: -1);
                Cv2.Sobel(gray, gradY, MatType.CV_32F, 0, 1, ksize: -1);

                Mat gradient = new Mat();
                // - x,y 미분한 이미지들 더하기
                Cv2.Subtract(gradX, gradY, gradient);
                // - 스케일 변환
                Cv2.ConvertScaleAbs(gradient, gradient);
                CaptureImage4.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(resizeCanvas(gradient));

                //가우시안 블러
                Mat blur = new Mat();
                string[] txtBlur = txtBlur1.Text.Split(',');
                int blurfirst = Convert.ToInt32(txtBlur[0]);
                int blursecond = Convert.ToInt32(txtBlur[1]);
                Cv2.Blur(gradient, blur, new OpenCvSharp.Size(blurfirst, blursecond));

                //이진화
                Mat binery = new Mat();
                int otg = Convert.ToInt32(txtOtg.Text);
                Cv2.Threshold(blur, binery, otg, 255, ThresholdTypes.Binary);
                CaptureImage5.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(resizeCanvas(binery));

                //모폴로지 연산
                Mat Morphology = new Mat();
                string[] txtMo = txtMophology.Text.Split(',');
                int first = Convert.ToInt32(txtMo[0]);
                int second = Convert.ToInt32(txtMo[1]);
                Mat kenel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(first, second));
                Cv2.MorphologyEx(binery, Morphology, MorphTypes.Close, kenel, iterations: 1);
                CaptureImage6.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(resizeCanvas(Morphology));

                //팽창,침식(노이즈 제거 위함)
                Mat closed = new Mat();
                string[] literode = this.txtErode.Text.Split(',');
                int Efirst = Convert.ToInt32(literode[0]);
                int Esecond = Convert.ToInt32(literode[1]);
                Cv2.Erode(Morphology, closed, null, null, Esecond);
                Cv2.Dilate(closed, closed, null, null, Efirst);
                CaptureImage7.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(resizeCanvas(closed));

                HierarchyIndex[] hierarchy;

                //컨투어(가장자리 찾기)
                Cv2.FindContours(closed, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
            }
            catch
            {

            }
            return contours;
        }
        private void drawSqure(Mat img, OpenCvSharp.Point[][] contours)
        {

            foreach (OpenCvSharp.Point[] s in contours)
            {
                Rect rect = Cv2.BoundingRect(s);
                Cv2.Rectangle(img, new OpenCvSharp.Point(rect.X, rect.Y), new OpenCvSharp.Point(rect.X + rect.Width, rect.Y + rect.Height), Scalar.Red, 2, LineTypes.AntiAlias);
            }
            CaptureImage8.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(resizeBigCanvas(img));
            Cv2.ImWrite(@"E:\Project\연구과제\BarcodeDetector\BarcodeDetector\testImg\last.jpg", img);
        }

        //Rect 들을 받아서 디코드(Xzing dll 사용)
        private List<OpenCvSharp.Point[]> decodeBarcode(Mat img, OpenCvSharp.Point[][] contours)
        {
            Mat dst = new Mat();
            Mat gray = new Mat();
            Mat resizeImg = img;

            Mat decodeImg = new Mat();

            List<OpenCvSharp.Point[]> contourList = new List<OpenCvSharp.Point[]>();
            List<string> barcodes = new List<string>();

            int boxwidth = Convert.ToInt32(txtSize.Text.Split(',')[0]);
            int boxheight = Convert.ToInt32(txtSize.Text.Split(',')[1]);
            int padding = Convert.ToInt32(txtPadding.Text);

            ZXing.Result barcodeResult = null;
            //찾은 가장자리 정보를 이용하여 사각형 그리기
            foreach (OpenCvSharp.Point[] s in contours)
            {
                double length = Cv2.ArcLength(s, true);
                //RotatedRect rect = Cv2.MinAreaRect(contours[i]);
                Rect rect = Cv2.BoundingRect(s);
                if (Math.Abs(rect.Width - boxwidth) < padding || Math.Abs(rect.Width - boxheight) < padding)
                {
                    decodeImg = img.SubMat(rect);
                    Mat barcodeImg = resizeBarcode(decodeImg);
                    var barcode = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(barcodeImg);

                    try
                    {
                        barcodeResult = barcodeReader.Decode(barcode);
                    }
                    catch
                    {

                    }

                    if (barcodeResult != null)
                    {
                        contourList.Add(s);
                        //barcodes.Add(barcodeResult.ToString().Trim());
                    }
                }
            }
            Cv2.ImWrite(@"E:\Project\연구과제\BarcodeDetector\BarcodeDetector\testImg\detectSqureimg.jpg", resizeImg);

            //Console.WriteLine(barcodes);
            return contourList;
        }

        #endregion ==== 바코드 위치 찾기 ====

        #region === 편의 기능 ===

        //박스들 중 바코드가 읽히는 것들만 잡아서 가로세로 평균값을 내서 가지고 있는다
        private void learningBoxsize(Mat img, OpenCvSharp.Point[][] contours)
        {
            Mat dst = new Mat();
            Mat gray = new Mat();
            Mat resizeImg = img;

            int boxWidth = 0;
            int boxHeight = 0;
            int barcodeCount = 0;

            Mat decodeImg = new Mat();
            //찾은 가장자리 정보를 이용하여 사각형 그리기
            foreach (OpenCvSharp.Point[] s in contours)
            {
                double length = Cv2.ArcLength(s, true);
                //RotatedRect rect = Cv2.MinAreaRect(contours[i]);
                Rect rect = Cv2.BoundingRect(s);
                decodeImg = img.SubMat(rect);
                Mat barcodeImg = resizeBarcode(decodeImg);
                var barcode = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(barcodeImg);

                var barcodeResult = barcodeReader.Decode(barcode);

                if (barcodeResult != null)
                {
                    boxWidth += rect.Width;
                    boxHeight += rect.Height;
                    barcodeCount += 1;
                }
            }
            boxWidth = boxWidth / barcodeCount;
            boxHeight = boxHeight / barcodeCount;
            txtSize.Text = boxWidth.ToString() + "," + boxHeight.ToString();
        }


        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                detectStart_Click(sender, e);
            }
        }

        private void insertText(string text)
        {
            txtResult.AppendText(text + Environment.NewLine);
        }

        #endregion === 편의 기능 ===
        
    }
}
