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
        private Mat squareImg;
        private Mat Loadimg;
        private Mat origineimg;
        private string sImagePath;
        private VideoCapture capture;
        private Mat video = new Mat();
        private BarcodeReader barcodeReader = new BarcodeReader { AutoRotate = true, TryInverted = true };
        private setting set = new setting();

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
                //Loadimg = Cv2.ImRead(sImagePath);
            }
        }

        private void setting()
        {
            //ini 파일 내용 읽고 초기 셋팅
            StringBuilder RESIZE = new StringBuilder(255);
            StringBuilder BLUR = new StringBuilder(255);
            StringBuilder CONTRACT = new StringBuilder(255);
            StringBuilder MOPHOLOGY = new StringBuilder(255);
            StringBuilder OTG = new StringBuilder(255);
            StringBuilder ERODE = new StringBuilder(255);
            StringBuilder IMAGEPATH = new StringBuilder(255);
            StringBuilder BOXSIZE = new StringBuilder(255);
            StringBuilder PADDING = new StringBuilder(255);

            GetPrivateProfileString("SETTING", "RESIZE", "", RESIZE, 255, Application.StartupPath + @"\setting.ini");
            GetPrivateProfileString("SETTING", "BLUR", "", BLUR, 255, Application.StartupPath + @"\setting.ini");
            GetPrivateProfileString("SETTING", "CONTRACT", "", CONTRACT, 255, Application.StartupPath + @"\setting.ini");
            GetPrivateProfileString("SETTING", "MOPHOLOGY", "", MOPHOLOGY, 255, Application.StartupPath + @"\setting.ini");
            GetPrivateProfileString("SETTING", "OTG", "", OTG, 255, Application.StartupPath + @"\setting.ini");
            GetPrivateProfileString("SETTING", "ERODE", "", ERODE, 255, Application.StartupPath + @"\setting.ini");
            GetPrivateProfileString("SETTING", "BOXSIZE", "", BOXSIZE, 255, Application.StartupPath + @"\setting.ini");
            GetPrivateProfileString("SETTING", "PADDING", "", PADDING, 255, Application.StartupPath + @"\setting.ini");
            GetPrivateProfileString("SETTING", "IMAGEPATH", "", IMAGEPATH, 255, Application.StartupPath + @"\setting.ini");

            txtContrast.Text = CONTRACT.ToString();
            txtBlur1.Text = BLUR.ToString();
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

            //stop watch
            lblTime.Text = "0 초";
            stopwatch.Start();

            OpenCvSharp.Point[][] contours = barcodeDetect(origineimg);
            learningBoxsize(origineimg, contours);

            stopwatch.Stop();
            lblTime.Text = String.Format("{0:0.0000}", (double)stopwatch.ElapsedMilliseconds / 1000) + " 초";
        }

        //웹캠
        public void btnWebcam_Click(object sender, EventArgs e)
        {
            lblTime.Text = "0 초";
            capture.Open(1);
            capture.FrameWidth = 1920;
            capture.FrameHeight = 1080;
            timer1.Enabled = true;
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
        //웹캠 타이머
        private void timer1_Tick(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            //stop watch
            stopwatch.Start();

            capture.Read(video);
            Loadimg = set.Rotate(video,180);
            origineimg = set.Rotate(video,180);
            OpenCvSharp.Point[][] contours = barcodeDetect(Loadimg);
            if (chkDecode.Checked)
            {
                Model con1 = new Model();
                con1 = decodeBarcode(Loadimg, contours);

                drawSqure(Loadimg, con1.Contour.ToArray());
                insertText(con1.Barcode, txtBarcode);
                lblTime.Text = (con1.searchQty).ToString();
            }
            else
            {
                drawSqure(Loadimg, contours);
            }
            stopwatch.Stop();
            string time = String.Format("{0:0.0000}", (double)stopwatch.ElapsedMilliseconds / 1000) + " 초" + Environment.NewLine;
            insertText(time, txtTime);
        }

        //이미지 캡처 
        private void LoadImage_Click(object sender, EventArgs e)
        {
            Cv2.ImWrite(@"E:\Project\연구과제\Barcode_Detect\BarcodeDetector\testImg\loadImg.jpg", Loadimg);
            Cv2.ImWrite(@"E:\Project\연구과제\Barcode_Detect\BarcodeDetector\testImg\squarImg.jpg", squareImg);
        }
        //디텍팅 시작
        private void detectStart_Click(object sender, EventArgs e)
        {
            //Stopwatch stopwatch = new Stopwatch();
            ////stop watch
            //stopwatch.Start();

            //OpenCvSharp.Point[][] contours = barcodeDetect(Loadimg);
            //int totalContours = contours.Count();
            //if (chkDecode.Checked)
            //{
            //    List<OpenCvSharp.Point[]> contourList1 = new List<OpenCvSharp.Point[]>();
            //    List<OpenCvSharp.Point[]> contourList2 = new List<OpenCvSharp.Point[]>();
            //    List<OpenCvSharp.Point[]> contourList3 = new List<OpenCvSharp.Point[]>();
            //    OpenCvSharp.Point[][] one = new ArraySegment<OpenCvSharp.Point[]>(contours, 0, totalContours / 2).ToArray();
            //    OpenCvSharp.Point[][] two = new ArraySegment<OpenCvSharp.Point[]>(contours, totalContours / 2, totalContours/2).ToArray();
            //    Thread thread1 = new Thread(() => contourList1 = decodeBarcode(Loadimg, one));
            //    thread1.Start();
            //    Thread thread2 = new Thread(() => contourList2 = decodeBarcode(Loadimg, two));
            //    thread2.Start();

            //    thread1.Join();
            //    thread2.Join();
            //    contourList1.AddRange(contourList2);
            //    contourList1.AddRange(contourList3);
            //    drawSqure(Loadimg, contourList1.ToArray());
            //}
            //else
            //{
            //    drawSqure(Loadimg, contours);
            //}

            //stopwatch.Stop();
            //lblTime.Text = (stopwatch.ElapsedMilliseconds / 1000).ToString() + "ms";
            //lblTime.Text = String.Format("{0:0.0000}", (double)stopwatch.ElapsedMilliseconds / 1000) + " 초";
        }

        #endregion ==== 버튼 이벤트 ====

        #region ==== 바코드 위치 찾기 ====
        private OpenCvSharp.Point[][] barcodeDetect(Mat img)
        {
            OpenCvSharp.Point[][] contours = null;

            try
            {
                //ini 파일에 설정값 저장
                WritePrivateProfileString("SETTING", "CONTRACT", txtContrast.Text, Application.StartupPath + @"\setting.ini");
                WritePrivateProfileString("SETTING", "BLUR", txtBlur1.Text, Application.StartupPath + @"\setting.ini");
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

                //명암 조절
                string getContrast = txtContrast.Text;
                gray = gray + Convert.ToInt32(getContrast);

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
                CaptureImage5.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(set.resizeCanvas(binery));

                //모폴로지 연산
                Mat Morphology = new Mat();
                string[] txtMo = txtMophology.Text.Split(',');
                int first = Convert.ToInt32(txtMo[0]);
                int second = Convert.ToInt32(txtMo[1]);
                Mat kenel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(first, second));
                Cv2.MorphologyEx(binery, Morphology, MorphTypes.Close, kenel, iterations: 1);
                CaptureImage6.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(set.resizeCanvas(Morphology));

                //팽창,침식(노이즈 제거 위함)
                Mat closed = new Mat();
                string[] literode = this.txtErode.Text.Split(',');
                int Efirst = Convert.ToInt32(literode[0]);
                int Esecond = Convert.ToInt32(literode[1]);
                Cv2.Erode(Morphology, closed, null, null, Esecond);
                Cv2.Dilate(closed, closed, null, null, Efirst);
                CaptureImage7.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(set.resizeCanvas(closed));

                HierarchyIndex[] hierarchy;

                //컨투어(가장자리 찾기)
                Cv2.FindContours(closed, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
            }
            catch
            {

            }
            return contours;
        }
        //박스 그리기
        private void drawSqure(Mat img, OpenCvSharp.Point[][] contours)
        {
            int padding = Convert.ToInt32(txtPadding.Text);
            int halfPadding = padding / 2;
            foreach (OpenCvSharp.Point[] s in contours)
            {
                Rect rect = Cv2.BoundingRect(s);
                Cv2.Rectangle(img, new OpenCvSharp.Point(rect.X - padding, rect.Y - padding), 
                    new OpenCvSharp.Point(rect.X + rect.Width + halfPadding, rect.Y + rect.Height + halfPadding), Scalar.Red, 2, LineTypes.AntiAlias);
            }
            CaptureImage8.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(set.resizeBigCanvas(img));
            squareImg = img;
        }

        //Rect 들을 받아서 디코드(Xzing dll 사용)
        private Model decodeBarcode(Mat img, OpenCvSharp.Point[][] contours)
        {
            Mat decodeImg = new Mat();
            Model Con = new Model();
            Con.Contour = new List<OpenCvSharp.Point[]>();
            Con.Barcode = "";

            int boxwidth = Convert.ToInt32(txtSize.Text.Split(',')[0]);
            int boxheight = Convert.ToInt32(txtSize.Text.Split(',')[1]);
            int padding = Convert.ToInt32(txtPadding.Text);
            int halfPadding = padding / 2;
            int count = 0;
            int contourCount = 0;

            ZXing.Result barcodeResult = null;
            Console.WriteLine(contours.Count());
            //찾은 가장자리 정보를 이용하여 사각형 그리기
            foreach (OpenCvSharp.Point[] s in contours)
            {
                try
                {
                    contourCount += 1;
                    double length = Cv2.ArcLength(s, true);
                    //RotatedRect rect = Cv2.MinAreaRect(contours[i]);
                    Rect rect = Cv2.BoundingRect(s);
                    rect.X = rect.X - halfPadding;
                    rect.Y = rect.Y - halfPadding;
                    rect.Width = rect.Width + padding;
                    rect.Height = rect.Height + padding;
                    decodeImg = img.SubMat(rect);
                    Mat barcodeImg = set.resizeBarcode(decodeImg);
                    if(chkCapture.Checked) Cv2.ImWrite(@"E:\Project\연구과제\Barcode_Detect\BarcodeDetector\testImg\barcode" + contourCount.ToString() + ".jpg", barcodeImg);
                    if (Math.Abs(rect.Width - boxwidth) < padding || Math.Abs(rect.Height - boxheight) < padding)
                    {
                        decodeImg = img.SubMat(rect);
                        barcodeImg = set.resizeBarcode(decodeImg);
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
                            Con.Contour.Add(s);
                            Con.Barcode += barcodeResult;
                            count += 1;
                        }
                    }
                }
                catch
                {

                }
            }
            Con.searchQty = count;

            return Con;
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
                Mat barcodeImg = set.resizeBarcode(decodeImg);
                var barcode = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(barcodeImg);

                var barcodeResult = barcodeReader.Decode(barcode);

                if (barcodeResult != null)
                {
                    boxWidth += rect.Width;
                    boxHeight += rect.Height;
                    barcodeCount += 1;
                }
            }
            if (barcodeCount != 0)
            {
                boxWidth = boxWidth / barcodeCount;
                boxHeight = boxHeight / barcodeCount;
                txtSize.Text = boxWidth.ToString() + "," + boxHeight.ToString();
            }
        }


        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                detectStart_Click(sender, e);
            }
        }

        private void insertText(string str, TextBox txtBox)
        {
            txtBox.Text = str + Environment.NewLine;
            txtBox.Select(txtBox.Text.Length, 0);
            txtBox.ScrollToCaret();

        }

        #endregion === 편의 기능 ===
        
    }
}
