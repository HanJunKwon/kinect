using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDressingRoom
{
    class CategoryButtonViewModel : INotifyPropertyChanged
    {
        private string path1;
        public string Path1
        {
            get { return path1; }
            set
            {
                path1 = value;
                OnPropertyUpdate("Path1");
            }
        }
        private string path2;
        public string Path2
        {
            get { return path2; }
            set
            {
                path2 = value;
                OnPropertyUpdate("Path2");
            }
        }

        private string path3;
        public string Path3
        {
            get { return path3; }
            set
            {
                path3 = value;
                OnPropertyUpdate("Path3");
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
