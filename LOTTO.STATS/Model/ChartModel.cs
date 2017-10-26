using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTTO.STATS.Model
{
    public class ChartModel : INotifyPropertyChanged
    {
        private string _Group;
        public string Group
        {
            get
            {
                return _Group;
            }
            set
            {
                if (value != _Group)
                {
                    _Group = value;
                    NotifyPropertyChanged("Group");

                }
            }

        }
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (value != _Name)
                {
                    _Name = value;
                    NotifyPropertyChanged("Name");

                }
            }

        }
        private int _Value;
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (value != _Value)
                {
                    _Value = value;
                    NotifyPropertyChanged("Value");

                }
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}