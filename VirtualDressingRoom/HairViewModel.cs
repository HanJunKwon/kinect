
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDressingRoom
{
    // 헤어 선택하고 화면에 나오는걸 변경하기 위한 클래스
    class HairViewModel : INotifyPropertyChanged
    {
        private String path;
        private int width;
        private int height;

        public String Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
                OnPropertyUpdate("Path");
            }
        }

        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                OnPropertyUpdate("Width");
            }
        }

        public int Height
        {
            get { return height; }
            set
            {
                height = value;
                OnPropertyUpdate("Height");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyUpdate(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
