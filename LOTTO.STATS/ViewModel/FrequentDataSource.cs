using LOTTO.STATS.Core;
using LOTTO.STATS.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTTO.STATS.ViewModel
{
   public class FrequentDataSource : INotifyPropertyChanged
    {
       public ObservableCollection<LottoModel> Items { get; private set; }
        public FrequentDataSource()
        {
            this.Items = new ObservableCollection<LottoModel>();

        }
        public string url { get; set; }
        public FrequentDataSource(string path)
        {

            this.url = "http://lotto.promrat.com/stats/frequently/" + path;
            //this.url = "http://lotto.promrat.com/stats/frequently/all";
            this.Items = new ObservableCollection<LottoModel>();

        }


        private bool _hasItem = false;
        public bool hasItem
        {
            get
            {
                return _hasItem;
            }
            set
            {
                _hasItem = value;
                NotifyPropertyChanged("hasItem");

            }
        }

        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                NotifyPropertyChanged("IsLoading");

            }
        }

        public async void LoadData(int skip)
        {
            try
            {
                if (skip == 0)
                {
                    this.Items.Clear();
                }

                this.IsLoading = true;

                var content = await Utility.PostData(this.url, "");
                if (!string.IsNullOrEmpty(content))
                {
                    downloadCompleted(content);
                }
            }
            catch (Exception) { }
        }

        private void downloadCompleted(string jsonString)
        {
            try
            {
                JObject json = JObject.Parse(jsonString);
                List<JToken> DataItems = json["feed"]["entry"].ToList();
                foreach (var dataItem in DataItems)
                {
                    try
                    {
                        var prop = dataItem as JProperty;
                        var frequent = prop.Name;
                        var digit = "";
                        foreach (var lotto in prop.Value)
                        {
                            digit += lotto["digit"]+" ";
                        }

                        LottoModel model = new LottoModel()
                        {
                            frequent = frequent,
                            digit = digit
                        };
                        this.Items.Add(model);
                    }
                    catch (Exception) { }
                }
                this.hasItem = DataItems.Count() != 0;
            }
            catch (Exception) { }
            this.IsLoading = false;
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
