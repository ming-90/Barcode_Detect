namespace BarcodeDetector
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.detectStart = new System.Windows.Forms.Button();
            this.LoadImage = new System.Windows.Forms.Button();
            this.txtContrast = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMophology = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CaptureImage7 = new System.Windows.Forms.PictureBox();
            this.CaptureImage6 = new System.Windows.Forms.PictureBox();
            this.CaptureImage5 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOtg = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtErode = new System.Windows.Forms.TextBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.btnWebcam = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label17 = new System.Windows.Forms.Label();
            this.txtBlur1 = new System.Windows.Forms.TextBox();
            this.chkDecode = new System.Windows.Forms.CheckBox();
            this.btnLearn = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.txtPadding = new System.Windows.Forms.TextBox();
            this.CaptureImage8 = new System.Windows.Forms.PictureBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.chkCapture = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.CaptureImage7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CaptureImage6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CaptureImage5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CaptureImage8)).BeginInit();
            this.SuspendLayout();
            // 
            // detectStart
            // 
            this.detectStart.Location = new System.Drawing.Point(1736, 227);
            this.detectStart.Name = "detectStart";
            this.detectStart.Size = new System.Drawing.Size(156, 55);
            this.detectStart.TabIndex = 1;
            this.detectStart.Text = "시작";
            this.detectStart.UseVisualStyleBackColor = true;
            this.detectStart.Click += new System.EventHandler(this.detectStart_Click);
            // 
            // LoadImage
            // 
            this.LoadImage.Location = new System.Drawing.Point(1736, 166);
            this.LoadImage.Name = "LoadImage";
            this.LoadImage.Size = new System.Drawing.Size(156, 55);
            this.LoadImage.TabIndex = 3;
            this.LoadImage.Text = "캡쳐";
            this.LoadImage.UseVisualStyleBackColor = true;
            this.LoadImage.Click += new System.EventHandler(this.LoadImage_Click);
            // 
            // txtContrast
            // 
            this.txtContrast.Location = new System.Drawing.Point(1807, 351);
            this.txtContrast.Name = "txtContrast";
            this.txtContrast.Size = new System.Drawing.Size(75, 21);
            this.txtContrast.TabIndex = 5;
            this.txtContrast.Text = "0";
            this.txtContrast.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1744, 354);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "명암 조절";
            // 
            // txtMophology
            // 
            this.txtMophology.Location = new System.Drawing.Point(1807, 434);
            this.txtMophology.Name = "txtMophology";
            this.txtMophology.Size = new System.Drawing.Size(75, 21);
            this.txtMophology.TabIndex = 10;
            this.txtMophology.Text = "40,10";
            this.txtMophology.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1708, 437);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "모폴로지 가중치";
            // 
            // CaptureImage7
            // 
            this.CaptureImage7.Location = new System.Drawing.Point(1016, 30);
            this.CaptureImage7.Name = "CaptureImage7";
            this.CaptureImage7.Size = new System.Drawing.Size(480, 270);
            this.CaptureImage7.TabIndex = 16;
            this.CaptureImage7.TabStop = false;
            // 
            // CaptureImage6
            // 
            this.CaptureImage6.Location = new System.Drawing.Point(520, 30);
            this.CaptureImage6.Name = "CaptureImage6";
            this.CaptureImage6.Size = new System.Drawing.Size(480, 270);
            this.CaptureImage6.TabIndex = 15;
            this.CaptureImage6.TabStop = false;
            // 
            // CaptureImage5
            // 
            this.CaptureImage5.Location = new System.Drawing.Point(24, 30);
            this.CaptureImage5.Name = "CaptureImage5";
            this.CaptureImage5.Size = new System.Drawing.Size(480, 270);
            this.CaptureImage5.TabIndex = 14;
            this.CaptureImage5.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1720, 410);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "이진화 가중치";
            // 
            // txtOtg
            // 
            this.txtOtg.Location = new System.Drawing.Point(1807, 407);
            this.txtOtg.Name = "txtOtg";
            this.txtOtg.Size = new System.Drawing.Size(75, 21);
            this.txtOtg.TabIndex = 18;
            this.txtOtg.Text = "225";
            this.txtOtg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(22, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 23;
            this.label9.Text = "이진화";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(518, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 25;
            this.label11.Text = "모폴로지";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1014, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 12);
            this.label12.TabIndex = 26;
            this.label12.Text = "팽창,침식";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(22, 313);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 27;
            this.label13.Text = "컨투어";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1744, 463);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 12);
            this.label14.TabIndex = 29;
            this.label14.Text = "팽창 침식";
            // 
            // txtErode
            // 
            this.txtErode.Location = new System.Drawing.Point(1807, 461);
            this.txtErode.Name = "txtErode";
            this.txtErode.Size = new System.Drawing.Size(75, 21);
            this.txtErode.TabIndex = 28;
            this.txtErode.Text = "4,4";
            this.txtErode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
            // 
            // lblTime
            // 
            this.lblTime.Location = new System.Drawing.Point(1807, 551);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(75, 12);
            this.lblTime.TabIndex = 31;
            this.lblTime.Text = "1000";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1760, 551);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 32;
            this.label15.Text = "검출수";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(1720, 491);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(81, 12);
            this.label.TabIndex = 34;
            this.label.Text = "마스크 사이즈";
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(1807, 488);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(75, 21);
            this.txtSize.TabIndex = 33;
            this.txtSize.Text = "0";
            // 
            // btnWebcam
            // 
            this.btnWebcam.Location = new System.Drawing.Point(1736, 105);
            this.btnWebcam.Name = "btnWebcam";
            this.btnWebcam.Size = new System.Drawing.Size(104, 55);
            this.btnWebcam.TabIndex = 35;
            this.btnWebcam.Text = "웹캠";
            this.btnWebcam.UseVisualStyleBackColor = true;
            this.btnWebcam.Click += new System.EventHandler(this.btnWebcam_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(1720, 381);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(81, 12);
            this.label17.TabIndex = 37;
            this.label17.Text = "가우시안 블러";
            // 
            // txtBlur1
            // 
            this.txtBlur1.Location = new System.Drawing.Point(1807, 378);
            this.txtBlur1.Name = "txtBlur1";
            this.txtBlur1.Size = new System.Drawing.Size(75, 21);
            this.txtBlur1.TabIndex = 36;
            this.txtBlur1.Text = "9,9";
            this.txtBlur1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
            // 
            // chkDecode
            // 
            this.chkDecode.AutoSize = true;
            this.chkDecode.Location = new System.Drawing.Point(1736, 292);
            this.chkDecode.Name = "chkDecode";
            this.chkDecode.Size = new System.Drawing.Size(67, 16);
            this.chkDecode.TabIndex = 39;
            this.chkDecode.Text = "Decode";
            this.chkDecode.UseVisualStyleBackColor = true;
            // 
            // btnLearn
            // 
            this.btnLearn.Location = new System.Drawing.Point(1736, 44);
            this.btnLearn.Name = "btnLearn";
            this.btnLearn.Size = new System.Drawing.Size(156, 55);
            this.btnLearn.TabIndex = 40;
            this.btnLearn.Text = "학습";
            this.btnLearn.UseVisualStyleBackColor = true;
            this.btnLearn.Click += new System.EventHandler(this.btnLearn_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(1846, 105);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(46, 55);
            this.btnStop.TabIndex = 43;
            this.btnStop.Text = "중지";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(1732, 518);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(69, 12);
            this.label18.TabIndex = 45;
            this.label18.Text = "마스크 패딩";
            // 
            // txtPadding
            // 
            this.txtPadding.Location = new System.Drawing.Point(1807, 515);
            this.txtPadding.Name = "txtPadding";
            this.txtPadding.Size = new System.Drawing.Size(75, 21);
            this.txtPadding.TabIndex = 44;
            this.txtPadding.Text = "0";
            // 
            // CaptureImage8
            // 
            this.CaptureImage8.Location = new System.Drawing.Point(24, 328);
            this.CaptureImage8.Name = "CaptureImage8";
            this.CaptureImage8.Size = new System.Drawing.Size(960, 540);
            this.CaptureImage8.TabIndex = 17;
            this.CaptureImage8.TabStop = false;
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(1514, 328);
            this.txtTime.Multiline = true;
            this.txtTime.Name = "txtTime";
            this.txtTime.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTime.Size = new System.Drawing.Size(173, 525);
            this.txtTime.TabIndex = 46;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(1016, 328);
            this.txtBarcode.Multiline = true;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBarcode.Size = new System.Drawing.Size(480, 525);
            this.txtBarcode.TabIndex = 47;
            // 
            // chkCapture
            // 
            this.chkCapture.AutoSize = true;
            this.chkCapture.Location = new System.Drawing.Point(1736, 312);
            this.chkCapture.Name = "chkCapture";
            this.chkCapture.Size = new System.Drawing.Size(119, 16);
            this.chkCapture.TabIndex = 48;
            this.chkCapture.Text = "Barcode Capture";
            this.chkCapture.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.chkCapture);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtPadding);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnLearn);
            this.Controls.Add(this.chkDecode);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtBlur1);
            this.Controls.Add(this.btnWebcam);
            this.Controls.Add(this.label);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtErode);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtOtg);
            this.Controls.Add(this.CaptureImage8);
            this.Controls.Add(this.CaptureImage7);
            this.Controls.Add(this.CaptureImage6);
            this.Controls.Add(this.CaptureImage5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMophology);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtContrast);
            this.Controls.Add(this.LoadImage);
            this.Controls.Add(this.detectStart);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.CaptureImage7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CaptureImage6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CaptureImage5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CaptureImage8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button detectStart;
        private System.Windows.Forms.Button LoadImage;
        private System.Windows.Forms.TextBox txtContrast;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMophology;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox CaptureImage7;
        private System.Windows.Forms.PictureBox CaptureImage6;
        private System.Windows.Forms.PictureBox CaptureImage5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOtg;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtErode;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.Button btnWebcam;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtBlur1;
        private System.Windows.Forms.CheckBox chkDecode;
        private System.Windows.Forms.Button btnLearn;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtPadding;
        private System.Windows.Forms.PictureBox CaptureImage8;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.CheckBox chkCapture;
    }
}

