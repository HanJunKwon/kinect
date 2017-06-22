using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDressingRoom
{
    class BottomViewModel : INotifyPropertyChanged
    {

        private String path;
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


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyUpdate(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
