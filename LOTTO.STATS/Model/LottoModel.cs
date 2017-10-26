using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTTO.STATS.Model
{
    public class LottoModel : INotifyPropertyChanged
    {
        private string _date;
        public string date
        {
            get
            {
                return _date;
            }
            set
            {
                if (value != _date)
                {
                    _date = value;
                    NotifyPropertyChanged("date");

                }
            }

        }
        private string _jackpot;
        public string jackpot
        {

            get
            {
                return _jackpot;
            }
            set
            {
                if (value != _jackpot)
                {
                    _jackpot = value;
                    NotifyPropertyChanged("jackpot");

                }
            }
        }
        private string _two_digit;
        public string two_digit
        {
            get
            {
                return _two_digit;
            }
            set
            {
                if (value != _two_digit)
                {
                    _two_digit = value;
                    NotifyPropertyChanged("two_digit");

                }
            }

        }
        private string _three_digit;
        public string three_digit
        {

            get
            {
                return _three_digit;
            }
            set
            {
                if (value != _three_digit)
                {
                    _three_digit = value;
                    NotifyPropertyChanged("three_digit");

                }
            }
        }

        //frequently digit

        private string _frequent;
        public string frequent
        {
            get
            {
                return _frequent;
            }
            set
            {
                if (value != _frequent)
                {
                    _frequent = value;
                    NotifyPropertyChanged("frequent");

                }
            }

        }
        private string _digit;
        public string digit
        {

            get
            {
                return _digit;
            }
            set
            {
                if (value != _digit)
                {
                    _digit = value;
                    NotifyPropertyChanged("digit");

                }
            }
        }

        //graph

        private Object _graph;
        public Object graph
        {

            get
            {
                return _graph;
            }
            set
            {
                if (value != _graph)
                {
                    _graph = value;
                    NotifyPropertyChanged("graph");

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