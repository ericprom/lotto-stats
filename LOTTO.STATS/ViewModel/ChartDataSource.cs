using LOTTO.STATS.Core;
using LOTTO.STATS.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTTO.STATS.ViewModel
{
   public class ChartDataSource : INotifyPropertyChanged
    {
       public ObservableCollection<LottoModel> Items { get; private set; }
        public ChartDataSource()
        {
            this.Items = new ObservableCollection<LottoModel>();

        }
        public string url { get; set; }
        public ChartDataSource(string path)
        {

            this.url = "http://lotto.promrat.com/stats/graph/" + path;
            //this.url = "http://lotto.promrat.com/stats/graph/all";
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
                    this.Items.Add(new LottoModel()
                    {
                        graph = dataItem
                    });
                }
                this.hasItem = DataItems.Count() != 0;
            }
            catch (Exception ex) { }
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
