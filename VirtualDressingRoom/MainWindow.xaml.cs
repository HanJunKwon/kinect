using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Kinect;
using System.Windows.Shapes;
using System.Drawing;

namespace VirtualDressingRoom
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        Polyline[] m_poly = new Polyline[5];
        Person person = new Person();
        Position position = new Position();
        System.Windows.Point btnCategoryUp, btnCategoryDown, btnCategory1, btnCategory2, btnCategory3, btnClothesUp, btnClothesDown,
            btnClothes1, btnClothes2, btnClothes3, btnClothes4, btnBuy, btnCapture;
        short category_index = 1; // 카테고리 페이지 개념
        short category_min_index = 1; // 카테고리의 최소 페이지
        short category_max_index = 2; // 카테고리의 최대 페이지

        short clothes_index = 1; // 의상 페이지 개념
        short clothes_min_index = 1; // 의상의 최소 페이지
        short clothes_max_index; // 의상의 최대 페이지, 선택한 카테고리에 따른 의상의 수가 다르므로 동적으로 변경 되야한다.
        short select_index = 1;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            this.DataContext = new CategoryButtonViewModel();
            InitializeNui();

            for (int i=0; i<5; i++)
            {
                m_poly[i] = new Polyline();
                m_poly[i].Stroke = new SolidColorBrush(Colors.Green);
                m_poly[i].StrokeThickness = 4;
                m_poly[i].Visibility = Visibility.Collapsed;

                canvas1.Children.Add(m_poly[i]);
            }
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Width = SystemParameters.MaximizedPrimaryScreenWidth;
            Application.Current.MainWindow.Height = SystemParameters.MaximizedPrimaryScreenHeight;

            // 영상 출력 이미지의 넓이, 높이 지정
            Image1.Width = this.Width;
            Image1.Height = this.Height;

            // 브랜드 마크 이미지의 넓이 지정
            Image2.Width = this.Width;

            // 구매 버튼 위치 지정
            Thickness margin = Buy.Margin;
            margin.Left = 1100;
            Buy.Margin = margin;

            // 카메라 버튼 위치 지정
            margin = Capture.Margin;
            margin.Left = 1200;
            Capture.Margin = margin;

            // 카테고리 선택 버튼들 초기화
            Category_Change();
            Clothes_Change();

            // 각 버튼들의 x,y좌표 값을 얻어온다. 반드시 마지막에 실행되야 한다.
            GeneralTransform generalTransform = Buy.TransformToAncestor(this);
            btnBuy = generalTransform.Transform(new System.Windows.Point(0, 0)); // 구매 버튼 x,y 좌표 얻기
            generalTransform = Capture.TransformToAncestor(this);
            btnCapture = generalTransform.Transform(new System.Windows.Point(0, 0)); // 캡처 버튼 x,y 좌표 얻기
            generalTransform = Category_down.TransformToAncestor(this);
            btnCategoryDown = generalTransform.Transform(new System.Windows.Point(0, 0)); // 카테고리 다운 버튼 x,y 좌표 얻기
            generalTransform = Category_up.TransformToAncestor(this);
            btnCategoryUp = generalTransform.Transform(new System.Windows.Point(0, 0)); // 카테고리 업 버튼 x,y 좌표 얻기
            generalTransform = Category1.TransformToAncestor(this);
            btnCategory1 = generalTransform.Transform(new System.Windows.Point(0, 0)); // 카테고리1 버튼 x,y 좌표 얻기
            generalTransform = Category2.TransformToAncestor(this);
            btnCategory2 = generalTransform.Transform(new System.Windows.Point(0, 0)); // 카테고리2 버튼 x,y 좌표 얻기
            generalTransform = Category3.TransformToAncestor(this);
            btnCategory3 = generalTransform.Transform(new System.Windows.Point(0, 0)); // 카테고리3 버튼 x,y 좌표 얻기
            generalTransform = Clothes_down.TransformToAncestor(this);
            btnClothesDown = generalTransform.Transform(new System.Windows.Point(0, 0)); // Clothes 다운 x,y 좌표 얻기
            generalTransform = Clothes_up.TransformToAncestor(this);
            btnClothesUp = generalTransform.Transform(new System.Windows.Point(0, 0)); // Clothes 업 x,y 좌표 얻기
            generalTransform = Clothes1.TransformToAncestor(this);
            btnClothes1 = generalTransform.Transform(new System.Windows.Point(0, 0)); // Clothes1 버튼 x,y 좌표 얻기
            generalTransform = Clothes2.TransformToAncestor(this);
            btnClothes2 = generalTransform.Transform(new System.Windows.Point(0, 0)); // Clothes2 버튼 x,y 좌표 얻기
            generalTransform = Clothes3.TransformToAncestor(this);
            btnClothes3 = generalTransform.Transform(new System.Windows.Point(0, 0)); // Clothes3 버튼 x,y 좌표 얻기
            generalTransform = Clothes4.TransformToAncestor(this);
            btnClothes4 = generalTransform.Transform(new System.Windows.Point(0, 0)); // Clothes4 버튼 x,y 좌표 얻기
        }

        KinectSensor nui = null;

        void InitializeNui()
        {
            nui = KinectSensor.KinectSensors[0];
            nui.ColorStream.Enable();
            nui.ColorFrameReady += new EventHandler<ColorImageFrameReadyEventArgs>(nui_ColorFrameReady);
            nui.DepthStream.Enable();
            nui.SkeletonStream.Enable();
            nui.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(nui_AllFrameReady);
            nui.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(capture);
            nui.Start();
        }
        
        // 사진 찎는 함수
        DateTime startDate;
        DateTime endDate;
        bool captureMessage = false;
        private void capture(object sender, AllFramesReadyEventArgs e)
        {
            endDate = DateTime.Now;
            TimeSpan dateDiff = endDate - startDate;
            int diffSecond = dateDiff.Seconds;
            if (captureMessage == true && diffSecond == 3)
            {
                //BitmapEncoder encoder = null;
                //encoder = new PngBitmapEncoder();
                //encoder.Frames.Add(BitmapFrame.Create(src));
                //String fileName = DateTime.Now.ToString("yyyMMddHHmm");
                //FileStream stream = new FileStream(fileName + ".png", FileMode.Create, FileAccess.Write);

                //// 사진 찍기
                //encoder.Save(stream);
                //stream.Close();
                //captureMessage = false;

                //// 찍은 사진을 여는 과정
                //System.Diagnostics.Process exe = new System.Diagnostics.Process();
                //exe.StartInfo.FileName = fileName + ".png";
                //exe.Start();
                double screenLeft = SystemParameters.VirtualScreenLeft;
                double screenTop = SystemParameters.VirtualScreenTop;
                double screenWidth = SystemParameters.VirtualScreenWidth;
                double screenHeight = SystemParameters.VirtualScreenHeight;

                using (Bitmap bmp = new Bitmap((int)screenWidth,
                    (int)screenHeight))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        String filename = "ScreenCapture-" + DateTime.Now.ToString("ddMMyyyy-hhmmss") + ".png";
                        //Opacity = .0;
                        g.CopyFromScreen((int)screenLeft, (int)screenTop, 0, 0, bmp.Size);
                        bmp.Save("D:\\Screenshots\\" + filename);
                        //Opacity = 1;
                    }
                }
                captureMessage = false;
            }
        }

        BitmapSource src = null;
        void nui_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            ColorImageFrame ImageParam = e.OpenColorImageFrame();
            if (ImageParam == null) return;

            byte[] ImageBits = new byte[ImageParam.PixelDataLength];

            ImageParam.CopyPixelDataTo(ImageBits);

            src = BitmapSource.Create(ImageParam.Width,
                ImageParam.Height,
                96, 96,
                PixelFormats.Bgr32,
                null,
                ImageBits,
                ImageParam.Width * ImageParam.BytesPerPixel);

            Image1.Source = src;
        }

        private void nui_AllFrameReady(object sender, AllFramesReadyEventArgs e)
        {
            SkeletonFrame sf = e.OpenSkeletonFrame();

            if (sf == null) return;
            Skeleton[] skeletonData = new Skeleton[sf.SkeletonArrayLength];

            sf.CopySkeletonDataTo(skeletonData);

            using (DepthImageFrame depthImageFrame = e.OpenDepthImageFrame())
            {
                if(depthImageFrame != null)
                {
                    foreach(Skeleton sd in skeletonData)
                    {
                        if(sd.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            int nMax = 20;
                            Joint[] joints = new Joint[nMax];
                            for(int j =0; j<nMax; j++)
                            {
                                joints[j] = sd.Joints[(JointType)j];
                            }

                            System.Windows.Point[] points = new System.Windows.Point[nMax];
                            for(int j=0; j<nMax; j++)
                            {
                                DepthImagePoint depthPoint;
                                depthPoint = depthImageFrame.MapFromSkeletonPoint(joints[j].Position);

                                points[j] = new System.Windows.Point((int)(Image1.Width * depthPoint.X / depthImageFrame.Width),
                                    (int)(Image1.Height * depthPoint.Y / depthImageFrame.Height));

                                // 각 조인트의 위치를 person객체에 저장
                                switch (j)
                                {
                                    case (int)JointType.Head:
                                        position.Head = points[j];
                                        break;
                                    case (int)JointType.ShoulderCenter:
                                        position.ShoulderCenter = points[j];
                                        break;
                                    case (int)JointType.Spine:
                                        position.Spine = points[j];
                                        break;
                                    case (int)JointType.ShoulderLeft:
                                        position.ShoulderLeft = points[j];
                                        break;
                                    case (int)JointType.ElbowLeft:
                                        position.ElbowLeft = points[j];
                                        break;
                                    case (int)JointType.WristLeft:
                                        position.WristLeft = points[j];
                                        break;
                                    case (int)JointType.HandLeft:
                                        position.HandLeft = points[j];
                                        break;
                                    case (int)JointType.ShoulderRight:
                                        position.ShoulderRight = points[j];
                                        break;
                                    case (int)JointType.ElbowRight:
                                        position.ShoulderRight = points[j];
                                        break;
                                    case (int)JointType.HandRight:
                                        position.HandRight = points[j];
                                        break;
                                    case (int)JointType.WristRight:
                                        position.WristRight = points[j];
                                        break;
                                    case (int)JointType.HipCenter:
                                        position.ZIndex = joints[j].Position.Z;
                                        position.HipCenter = points[j];
                                        break;
                                    case (int)JointType.HipLeft:
                                        position.HipLeft = points[j];
                                        break;
                                    case (int)JointType.KneeLeft:
                                        position.KneeLeft = points[j];
                                        break;
                                    case (int)JointType.AnkleLeft:
                                        position.AnkleLeft = points[j];
                                        break;
                                    case (int)JointType.FootLeft:
                                        position.FootLeft = points[j];
                                        break;
                                    case (int)JointType.HipRight:
                                        position.HipRight = points[j];
                                        break;
                                    case (int)JointType.KneeRight:
                                        position.KneeRight = points[j];
                                        break;
                                    case (int)JointType.AnkleRight:
                                        position.AnkleRight = points[j];
                                        break;
                                    case (int)JointType.FootRight:
                                        position.FootRight = points[j];
                                        break;
                                }
                                // 오른손과 왼손의 위치를 받아서 어느 버튼을 누르고 있는지 판단하는 함수 호출
                                if (j == (int)JointType.HandLeft || j==(int)JointType.HandRight)
                                {
                                    ButtonClickAction(points[j].X, points[j].Y);
                                }
                            }
                            Z.Text = string.Format("Z:{0:0.00}", position.ZIndex);

                            PointCollection pc0 = new PointCollection(4);
                            pc0.Add(points[(int)JointType.HipCenter]);
                            pc0.Add(points[(int)JointType.Spine]);
                            pc0.Add(points[(int)JointType.ShoulderCenter]);
                            pc0.Add(points[(int)JointType.Head]);
                            m_poly[0].Points = pc0;
                            m_poly[0].Visibility = Visibility.Visible;

                            PointCollection pc1 = new PointCollection(5);
                            pc1.Add(points[(int)JointType.ShoulderCenter]);
                            pc1.Add(points[(int)JointType.ShoulderLeft]);
                            pc1.Add(points[(int)JointType.ElbowLeft]);
                            pc1.Add(points[(int)JointType.WristLeft]);
                            pc1.Add(points[(int)JointType.HandLeft]);
                            m_poly[1].Points = pc1;
                            m_poly[1].Visibility = Visibility.Visible;

                            PointCollection pc2 = new PointCollection(5);
                            pc2.Add(points[(int)JointType.ShoulderCenter]);
                            pc2.Add(points[(int)JointType.ShoulderRight]);
                            pc2.Add(points[(int)JointType.ElbowRight]);
                            pc2.Add(points[(int)JointType.WristRight]);
                            pc2.Add(points[(int)JointType.HandRight]);
                            m_poly[2].Points = pc2;
                            m_poly[2].Visibility = Visibility.Visible;

                            PointCollection pc3 = new PointCollection(5);
                            pc3.Add(points[(int)JointType.HipCenter]);
                            pc3.Add(points[(int)JointType.HipLeft]);
                            pc3.Add(points[(int)JointType.KneeLeft]);
                            pc3.Add(points[(int)JointType.AnkleLeft]);
                            pc3.Add(points[(int)JointType.FootLeft]);
                            m_poly[3].Points = pc3;
                            m_poly[3].Visibility = Visibility.Visible;

                            PointCollection pc4 = new PointCollection(5);
                            pc4.Add(points[(int)JointType.HipCenter]);
                            pc4.Add(points[(int)JointType.HipRight]);
                            pc4.Add(points[(int)JointType.KneeRight]);
                            pc4.Add(points[(int)JointType.AnkleRight]);
                            pc4.Add(points[(int)JointType.FootRight]);
                            m_poly[4].Points = pc4;
                            m_poly[4].Visibility = Visibility.Visible;

                            ClothesTrackSkeleton();
                        }
                    }
                }
            }
        }

        // 의상이 각자 맞는 스켈레톤을 추적하도록 한다.
        private void ClothesTrackSkeleton()
        {
            // 이미지 위치 조절
            Hair.Margin = new System.Windows.Thickness(position.Head.X-Hair.ActualWidth/2, position.Head.Y-Hair.ActualHeight/2,0,0);
            Necklace.Margin = new System.Windows.Thickness(position.ShoulderCenter.X-100, position.ShoulderCenter.Y-100, 0, 0);
            ClothesTop.Margin = new System.Windows.Thickness(position.ShoulderCenter.X - ClothesTop.ActualWidth / 2, position.ShoulderCenter.Y, 0, 0);
            ClothesBottom.Margin = new System.Windows.Thickness(position.HipCenter.X - ClothesBottom.ActualWidth / 2, position.HipLeft.Y, 0, 0);
            ShoesLeft.Margin = new System.Windows.Thickness(position.FootLeft.X - ShoesLeft.ActualWidth / 2, position.FootLeft.Y-ShoesLeft.ActualHeight / 2, 0, 0 );
            ShoesRight.Margin = new System.Windows.Thickness(position.FootRight.X - ShoesRight.ActualWidth / 2, position.FootRight.Y - ShoesRight.ActualWidth / 2, 0, 0);
            try
            {
                ImgClothesTop.Width = Convert.ToInt32(position.ShoulderRight.X - position.ShoulderLeft.X + 200);
                ClothesTop.Width = ImgClothesTop.Width;
                ImgClothesTop.Height = Convert.ToInt32(position.HipLeft.Y - position.ShoulderCenter.Y);
                ClothesTop.Height = ImgClothesTop.Height;
                ImgClothesBottom.Width = Convert.ToInt32((position.HipRight.X - position.HipLeft.X) * 6);
                ClothesBottom.Width = ImgClothesBottom.Width;
                ImgClothesBottom.Height = Convert.ToInt32(position.AnkleLeft.Y - position.HipCenter.Y);
                ClothesBottom.Height = ImgClothesBottom.Height;
            }
            catch (Exception e)
            {
                return;
            }

        }

    #region 모션 인식으로하는 버튼 클릭 이벤트
        private void ButtonClickAction(double x, double y)
        {
            if (x >= btnBuy.X && x <= btnBuy.X + 75)
            {
                if (y >= btnBuy.Y && y <= btnBuy.Y + 45)
                {
                    Buy_Click();
                }
            }
            else if (x >= btnCapture.X && x <= btnCapture.X + 75)
            {
                if (y >= btnCapture.Y && y <= btnCapture.Y + 45)
                {
                    Capture_Click();
                }
            }
            else if (x >= btnCategoryDown.X && x <= btnCategoryDown.X + 150)
            {
                //  카테고리 다운 버튼 선택
                if (y >= btnCategoryDown.Y && y <= btnCategoryDown.Y + 45)
                {
                    Category_down_Click();
                }
                // 카테고리 업 버튼 선택
                else if (y >= btnCategoryUp.Y && y <= btnCategoryUp.Y + 45)
                {
                    Category_up_Click();
                }
                // 카테고리1 버튼 선택
                else if (y >= btnCategory1.Y && y <= btnCategory1.Y + 150)
                {
                    Category1_Click();
                }
                // 카테고리2 버튼 선택
                else if (y >= btnCategory2.Y && y <= btnCategory2.Y + 150)
                {
                    Category2_Click();
                }
                // 카테고리3 버튼 선택
                else if (y >= btnCategory3.Y && y <= btnCategory3.Y + 150)
                {
                    Category3_Click();
                }
            }
            else if (x >= btnClothesDown.X && x <= btnClothesDown.X + 150)
            {
                if (y >= btnClothesDown.Y && y <= btnClothesDown.Y + 45)
                {
                    Clothes_down_Click();
                }
                else if(y >= btnClothesUp.Y && y <= btnClothesUp.Y + 45)
                {
                    Clothes_up_Click();
                }
                else if(y >= btnClothes1.Y && y <= btnClothes1.Y + 150)
                {
                    Clothes1_Click();
                }
                else if(y >= btnClothes2.Y && y <= btnClothes2.Y + 150)
                {
                    Clothes2_Click();
                }
                else if(y >= btnClothes3.Y && y <= btnClothes3.Y + 150)
                {
                    Clothes3_Click();
                }
                else if(y >= btnClothes4.Y && y <= btnClothes4.Y + 150)
                {
                    Clothes4_Click();
                }
            }
        }

        private void Buy_Click()
        {
            MessageBox.Show("보여줄 메시지 입니다.");
        }

        private void Capture_Click()
        {
            if (src != null)
            {
                startDate = DateTime.Now;
                captureMessage = true;
            }
        }

        private void Category1_Click()
        {
            if (category_index == 1)
            {
                select_index = 1;
                clothes_max_index = 3;
                clothes_index = 1;
                Clothes_Change();
            }
            else if (category_index == 2)
            {
                select_index = 4;
                clothes_max_index = 3;
                clothes_index = 1;
                Clothes_Change();
            }
        }

        private void Category2_Click()
        {
            if (category_index == 1)
            {
                select_index = 2;
                clothes_max_index = 1;
                clothes_index = 1;
                Clothes_Change();
            }
            else if (category_index == 2)
            {
                select_index = 5;
                clothes_max_index = 1;
                clothes_index = 1;
                Clothes_Change();
            }
        }

        private void Category3_Click()
        {
            if (category_index == 1)
            {
                select_index = 3;
                clothes_max_index = 3;
                clothes_index = 1;
                Clothes_Change();
            }
            else if (category_index == 2)
            {
                // 배경제거
            }
        }

        private void Category_up_Click()
        {
            if (category_index >= category_max_index)
            {
                return;
            }
            category_index++;
            Category_Change();
        }

        private void Category_down_Click()
        {
            if (category_index <= category_min_index)
            {
                return;
            }
            category_index--;
            Category_Change();
        }

        private void Clothes1_Click()
        {
            Dressing(1);
        }

        private void Clothes2_Click()
        {
            Dressing(2);
        }

        private void Clothes3_Click()
        {
            Dressing(3);
        }

        private void Clothes4_Click()
        {
            Dressing(4);
        }

        private void Clothes_up_Click()
        {
            if (clothes_index >= clothes_max_index)
            {
                return;
            }
            clothes_index++;
            Clothes_Change();
        }

        private void Clothes_down_Click()
        {
            if (clothes_index <= clothes_min_index)
            {
                return;
            }
            clothes_index--;
            Clothes_Change();
        }
    #endregion
        
    #region 마우스로 버튼을 클릭 시 버튼 클릭 이벤트
        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }

        private void Capture_Click(object sender, RoutedEventArgs e)
        {
            if (src != null)
            {
                startDate = DateTime.Now;
                captureMessage = true;
            }
        }
        
        private void Category_down_Click(object sender, RoutedEventArgs e)
        {
            if (category_index <= category_min_index)
            {
                return;
            }
            category_index--;
            Category_Change();
        }

        private void Category_up_Click(object sender, RoutedEventArgs e)
        {
            if (category_index >= category_max_index)
            {
                return;
            }
            category_index++;
            Category_Change();
        }

        private void Clothes_up_Click(object sender, RoutedEventArgs e)
        {
            if (clothes_index >= clothes_max_index)
            {
                return;
            }
            clothes_index++;
            Clothes_Change();
        }

        private void Clothes_down_Click(object sender, RoutedEventArgs e)
        {
            if (clothes_index <= clothes_min_index)
            {
                return;
            }
            clothes_index--;
            Clothes_Change();
        }

        private void Category1_Click(object sender, RoutedEventArgs e)
        {
            if (category_index == 1)
            {
                // 모자
                select_index = 1;
                clothes_max_index = 3;
                clothes_index = 1;
                Clothes_Change();
            }
            else if(category_index == 2)
            {
                // 하의
                select_index = 4;
                clothes_max_index = 3;
                clothes_index = 1;
                Clothes_Change();
            }
        }

        private void Category2_Click(object sender, RoutedEventArgs e)
        {
            if(category_index == 1)
            {
                // 목걸이
                    select_index = 2;
                    clothes_max_index = 1;
                    clothes_index = 1;
                    Clothes_Change();
            }
            else if(category_index == 2)
            {
                // 신발
                    select_index = 5;
                    clothes_max_index = 2;
                    clothes_index = 1;
                    Clothes_Change();
            }
        }

        private void Category3_Click(object sender, RoutedEventArgs e)
        {
            if (category_index == 1)
            {
                // 상의
                    select_index = 3;
                    clothes_max_index = 3;
                    clothes_index = 1;
                    Clothes_Change();
                }
            else if(category_index ==2 )
            {
                // 배경제거
            }
        }

        private void Clothes1_Click(object sender, RoutedEventArgs e)
        {
            Dressing(1);
        }

        private void Clothes2_Click(object sender, RoutedEventArgs e)
        {
            Dressing(2);
        }

        private void Clothes3_Click(object sender, RoutedEventArgs e)
        {
            Dressing(3);
        }

        private void Clothes4_Click(object sender, RoutedEventArgs e)
        {
            Dressing(4);
        }
    #endregion

        private void Category_Change()
        {
            if (category_index == 1)
            {
                var categoryButtonViewomodel = category.DataContext as CategoryButtonViewModel;
                categoryButtonViewomodel.Path1 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/img_hair.jpg";
                categoryButtonViewomodel.Path2 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/img_necklace.jpg";
                categoryButtonViewomodel.Path3 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/img_top.jpg";
            }
            else if (category_index == 2)
            {
                var categoryButtonViewomodel = category.DataContext as CategoryButtonViewModel;
                categoryButtonViewomodel.Path1 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/img_bottom.jpg";
                categoryButtonViewomodel.Path2 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/img_shoes.jpg";
                categoryButtonViewomodel.Path3 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/img_background.jpg";
            }
            return;
        }

        ClothesButtonViewModel clothesButtonImage;
        private void Clothes_Change()
        {
            clothesButtonImage = clothes.DataContext as ClothesButtonViewModel;
            if (category_index == 1)
            {
                // 머리
                if (select_index == 1)
                {
                    if (clothes_index == 1)
                    {
                        clothesButtonImage.Path1 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/hair/ball1.png";
                        clothesButtonImage.Path2 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/hair/ball2.png";
                        clothesButtonImage.Path3 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/hair/beanie1.png";
                        clothesButtonImage.Path4 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/hair/beret.png";
                    }
                    else if (clothes_index == 2)
                    {
                        clothesButtonImage.Path1 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/hair/bucket1.png";
                        clothesButtonImage.Path2 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/hair/bucket2.png";
                        clothesButtonImage.Path3 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/hair/papier.png";
                        clothesButtonImage.Path4 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/hair/wide.png";
                    }
                    else if (clothes_index == 3)
                    {
                        clothesButtonImage.Path1 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/hair/hair1.png";
                        clothesButtonImage.Path2 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/hair/hair2.png";
                        clothesButtonImage.Path3 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/hair/hair3.png";
                        clothesButtonImage.Path4 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/hair/hair4.png";
                    }
                }

                // 목걸이
                else if (select_index == 2)
                {
                    clothesButtonImage.Path1 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/necklace/necklace1.png";
                    clothesButtonImage.Path2 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/necklace/necklace2.png";
                    clothesButtonImage.Path3 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/necklace/necklace3.png";
                    clothesButtonImage.Path4 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/necklace/necklace4.png";
                }

                //상의
                else if (select_index == 3)
                {
                    if (clothes_index == 1)
                    {
                        clothesButtonImage.Path1 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/top/hoodie.png";
                        clothesButtonImage.Path2 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/top/jersey.png";
                        clothesButtonImage.Path3 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/top/mtm.png";
                        clothesButtonImage.Path4 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/top/off.png";
                    }
                    else if (clothes_index == 2)
                    {
                        clothesButtonImage.Path1 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/top/onepiece.png";
                        clothesButtonImage.Path2 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/top/polo.png";
                        clothesButtonImage.Path3 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/top/shirt1.png";
                        clothesButtonImage.Path4 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/top/shirt2.png";
                    }
                    else if (clothes_index == 3)
                    {
                        clothesButtonImage.Path1 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/top/shirts1.png";
                        clothesButtonImage.Path2 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/top/tee1.png";
                        clothesButtonImage.Path3 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/top/tee2.png";
                        clothesButtonImage.Path4 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/top/tank.png";
                    }
                }
            }

            else if (category_index == 2)
            {
                // 하의
                if (select_index == 4)
                {
                    if (clothes_index == 1)
                    {
                        clothesButtonImage.Path2 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/bottom/jean1.png";
                        clothesButtonImage.Path3 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/bottom/jean2.png";
                        clothesButtonImage.Path4 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/bottom/shorts1.png";
                        clothesButtonImage.Path1 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/bottom/shorts2.png";
                    }
                    else if (clothes_index == 2)
                    {
                        clothesButtonImage.Path2 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/bottom/shorts3.png";
                        clothesButtonImage.Path3 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/bottom/shorts4.png";
                        clothesButtonImage.Path4 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/bottom/shorts5.png";
                        clothesButtonImage.Path1 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/bottom/skirts1.png";
                    }
                    else if (clothes_index == 3)
                    {
                        clothesButtonImage.Path2 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/bottom/skirts2.png";
                        clothesButtonImage.Path3 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/bottom/skirts3.png";
                        clothesButtonImage.Path1 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/bottom/training1.png";
                        clothesButtonImage.Path4 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/bottom/training2.png";
                    }
                }

                //신발
                else if (select_index == 5)
                {
                    if(clothes_index == 1)
                    {
                        clothesButtonImage.Path1 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/shoes/shoe1.png";
                        clothesButtonImage.Path2 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/shoes/shoe2.png";
                        clothesButtonImage.Path3 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/shoes/shoe3.png";
                        clothesButtonImage.Path4 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/shoes/shoe4.png";
                    }
                    else if(clothes_index == 2)
                    {
                        clothesButtonImage.Path1 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/shoes/shoe5.png";
                        clothesButtonImage.Path2 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/shoes/shoe6.png";
                        clothesButtonImage.Path3 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/shoes/shoe7.png";
                        clothesButtonImage.Path4 = "D:/VirtualDressingRoom/VirtualDressingRoom/image/shoes/shoe8.png";
                    }
                }

                //배경
                else if (select_index == 6)
                {
                }
            }
        }

        private void Dressing(int clothesIndex)
        {
            int index = IndexJudgment();
            String path = null; 

            if (index == 11)
            {
                var hairViewModel = Hair.DataContext as HairViewModel;
                if (clothesIndex == 1)
                {
                    path = clothesButtonImage.Path1;
                }
                else if(clothesIndex == 2)
                {
                    path = clothesButtonImage.Path2;
                }
                else if (clothesIndex == 3)
                {
                    path = clothesButtonImage.Path3;
                }
                else if (clothesIndex == 4)
                {
                    path = clothesButtonImage.Path4;
                }
                hairViewModel.Path = path;
            }
            else if (index == 12)
            {
                var necklaceViewModel = Necklace.DataContext as NecklaceViewModel;
                if(clothesIndex == 1)
                {
                    path = clothesButtonImage.Path1;
                }
                else if(clothesIndex == 2)
                {
                    path = clothesButtonImage.Path2;
                }
                else if(clothesIndex == 3)
                {
                    path = clothesButtonImage.Path3;
                }
                else if(clothesIndex == 4)
                {
                    path = clothesButtonImage.Path4;
                }
                necklaceViewModel.Path = path;
            }
            else if(index == 13)
            {
                var topViewModel = ClothesTop.DataContext as TopViewModel;
                if (clothesIndex == 1)
                {
                    path = clothesButtonImage.Path1;
                }
                else if(clothesIndex == 2)
                {
                    path = clothesButtonImage.Path2;
                }
                else if(clothesIndex == 3)
                {
                    path = clothesButtonImage.Path3;
                }
                else if(clothesIndex == 4)
                {
                    path = clothesButtonImage.Path4;
                }
                topViewModel.Path = path;
            }
            else if(index == 24)
            {
                var bottomViewModel = ClothesBottom.DataContext as BottomViewModel;
                if (clothesIndex == 1)
                {
                    path = clothesButtonImage.Path1;
                }
                else if (clothesIndex == 2)
                {
                    path = clothesButtonImage.Path2;
                }
                else if (clothesIndex == 3)
                {
                    path = clothesButtonImage.Path3;
                }
                else if (clothesIndex == 4)
                {
                    path = clothesButtonImage.Path4;
                }
                bottomViewModel.Path = path;
            }
            else if(index == 25)
            {
                var shoesLeftViewModel = ShoesLeft.DataContext as ShoesLeftViewModel;
                var shoesRightViewModel = ShoesRight.DataContext as ShoesRightViewModel;
                if (clothesIndex == 1)
                {
                    path = clothesButtonImage.Path1;
                }
                else if (clothesIndex == 2)
                {
                    path = clothesButtonImage.Path2;
                }
                else if (clothesIndex == 3)
                {
                    path = clothesButtonImage.Path3;
                }
                else if (clothesIndex == 4)
                {
                    path = clothesButtonImage.Path4;
                }
                shoesLeftViewModel.Path = path;
                shoesRightViewModel.Path = path;
            }
        }
        
        public int IndexJudgment()
        {
            int index = 0;
            if(category_index == 1)
            {
                if(select_index == 1)
                {
                    index = 11;
                }
                else if(select_index == 2)
                {
                    index = 12;
                }
                else if(select_index == 3)
                {
                    index = 13;
                }
            }
            else if(category_index == 2)
            {
                if(select_index == 4)
                {
                    // 하의 선택
                    index = 24;
                }
                else if(select_index == 5)
                {
                    // 신발 선택
                    index = 25;
                }
                else if(select_index == 6)
                {
                    // 배경 제거
                    index = 26;
                }
            }
            return index;
        }
    }
}
