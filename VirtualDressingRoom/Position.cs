using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VirtualDressingRoom
{
    class Position
    {
        double zIndex;
        public double ZIndex
        {
            get
            {
                return zIndex;
            }
            set
            {
                zIndex = value;
            }
        }
        Point head; // 머리
        public Point Head
        {
            get { return head; }
            set
            {
                head.X = value.X;
                head.Y = value.Y;
            }
        }
        Point shoulderCenter; // 어깨 중간
        public Point ShoulderCenter
        {
            get { return shoulderCenter; }
            set
            {
                shoulderCenter.X = value.X;
                shoulderCenter.Y = value.Y;
            }
        }
        Point spine; // 등뼈
        public Point Spine
        {
            get { return spine; }
            set
            {
                spine.X = value.X;
                spine.Y = value.Y;
            }
        }
        Point shoulderLeft; // 왼쪽 어깨
        public Point ShoulderLeft
        {
            get
            {
                return shoulderLeft;
            }
            set
            {
                shoulderLeft.X = value.X;
                shoulderLeft.Y = value.Y;
            }
        }
        Point elbowLeft; // 왼쪽 팔꿈치
        public Point ElbowLeft
        {
            get
            {
                return elbowLeft;
            }
            set
            {
                elbowLeft.X = value.X;
                elbowLeft.Y = value.Y;
            }
        }
        Point wristLeft; // 왼쪽 손목
        public Point WristLeft
        {
            get
            {
                return wristLeft;
            }
            set
            {
                wristLeft.X = value.X;
                wristLeft.Y = value.Y;
            }
        }
        Point handLeft; // 왼손
        public Point HandLeft
        {
            get { return handLeft; }
            set
            {
                handLeft.X = value.X;
                handLeft.Y = value.Y;
            }
        }
        Point shoulderRight; // 오른쪽 어깨
        public Point ShoulderRight
        {
            get
            {
                return shoulderRight;
            }
            set
            {
                shoulderRight.X = value.X;
                shoulderRight.Y = value.Y;
            }
        }
        Point elbowRight; // 오른쪽 팔꿈치
        public Point ElbowRight
        {
            get
            {
                return elbowRight;
            }
            set
            {
                elbowRight.X = value.X;
                elbowRight.Y = value.Y;
            }
        }
        Point wristRight; // 오른쪽 손목
        public Point WristRight
        {
            get
            {
                return wristRight;
            }
            set
            {
                wristRight.X = value.X;
                wristRight.Y = value.Y;
            }
        }
        Point handRight; // 오른손
        public Point HandRight
        {
            get { return handRight; }
            set
            {
                handRight.X = value.X;
                handRight.Y = value.Y;
            }
        }
        Point hipCenter; // 엉덩이 중간
        public Point HipCenter
        {
            get { return hipCenter; }
            set
            {
                hipCenter.X = value.X;
                hipCenter.Y = value.Y;
            }
        }
        Point hipLeft; // 엉덩이 왼쪽
        public Point HipLeft
        {
            get { return hipLeft; }
            set
            {
                hipLeft.X = value.X;
                hipLeft.Y = value.Y;
            }
        }
        Point kneeLeft; // 왼쪽 무릎
        public Point KneeLeft
        {
            get { return kneeLeft; }
            set
            {
                kneeLeft.X = value.X;
                kneeLeft.Y = value.Y;
            }
        }
        Point ankleLeft; // 왼쪽 발목
        public Point AnkleLeft
        {
            get { return ankleLeft; }
            set
            {
                ankleLeft.X = value.X;
                ankleLeft.Y = value.Y;
            }
        }
        Point footLeft; // 왼쪽 발
        public Point FootLeft
        {
            get { return footLeft; }
            set
            {
                footLeft.X = value.X;
                footLeft.Y = value.Y;
            }
        }
        Point hipRight; // 엉덩이 오른쪽
        public Point HipRight
        {
            get { return hipRight; }
            set
            {
                hipRight.X = value.X;
                hipRight.Y = value.Y;
            }
        }
        Point kneeRight; // 오른쪽 무릎
        public Point KneeRight
        {
            get { return kneeRight; }
            set
            {
                kneeRight.X = value.X;
                kneeRight.Y = value.Y;
            }
        }
        Point ankleRight; // 오른쪽 발목
        public Point AnkleRight
        {
            get { return AnkleRight; }
            set
            {
                ankleRight.X = value.X;
                ankleRight.Y = value.Y;
            }
        }
        Point footRight; // 오른쪽 발
        public Point FootRight
        {
            get { return footRight; }
            set
            {
                footRight.X = value.X;
                footRight.Y = value.Y;
            }
        }
    }
}
