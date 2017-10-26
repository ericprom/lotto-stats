using LOTTO.STATS.Core;
using LOTTO.STATS.Model;
using LOTTO.STATS.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace LOTTO.STATS
{
    public sealed partial class MainPage : Page
    {
        StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
        public ControlTemplate defaultColumnSeriesTemplate;
        DataManager shareDataManager;
        DateTime now = DateTime.Now;
        int startYear = 2533; 
        CommandBar mainBar;
        AppBarButton lookbackBtn;
        Flyout lookbackFlyout;
        public MainPage()
        {
            this.InitializeComponent();
            lookbackFlyout = (Flyout)Resources["lookbackFlyout"];
            shareDataManager = DataManager.shareInstance;
            statusBar.BackgroundColor = Color.FromArgb(255, 44, 69, 102);
            statusBar.BackgroundOpacity = 1;
            PrepareAppBars();
            BottomAppBar = mainBar;
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private async void HubControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                statusBar.ProgressIndicator.Text = "Loading...";
                await statusBar.ProgressIndicator.ShowAsync();

                var totalYear = (now.Year + 543) - startYear;
                generateFlyout(totalYear);
                generateContent(totalYear.ToString());
            }
            catch (Exception ex) { }
        }
        private void generateContent(string totalYear)
        {
            try
            {
                var month = "month/" + now.Month;
                lookback.Header = "สถิติย้อนหลัง " + totalYear + " ปี";

                //load lookback
                shareDataManager.LookbackDataSource = new LookbackDataSource(totalYear);
                shareDataManager.LookbackDataSource.LoadData(0);
                shareDataManager.LookbackDataSource.PropertyChanged += LookbackDataSource_PropertyChanged;

                //load stats
                shareDataManager.FrequentDataSource = new FrequentDataSource(totalYear);
                shareDataManager.FrequentDataSource.LoadData(0);
                shareDataManager.FrequentDataSource.PropertyChanged += FrequentDataSource_PropertyChanged;

                //load graph
                shareDataManager.ChartDataSource = new ChartDataSource(totalYear);
                shareDataManager.ChartDataSource.LoadData(0);
                shareDataManager.ChartDataSource.PropertyChanged += ChartDataSource_PropertyChanged;
            }
            catch (Exception ex) { }
        }
        private async void LookbackDataSource_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == "IsLoading")
                {
                    if (shareDataManager.LookbackDataSource.IsLoading == false)
                    {
                        await statusBar.ProgressIndicator.HideAsync();
                        if (shareDataManager.LookbackDataSource.hasItem)
                        {
                            var result = from t in shareDataManager.LookbackDataSource.Items
                                         group t by t.date into g
                                         select new { Key = g.Key, Items = g };
                            groupLookbackFeed.Source = result;
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }
       
        private async void FrequentDataSource_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == "IsLoading")
                {
                    if (shareDataManager.FrequentDataSource.IsLoading == false)
                    {
                        await statusBar.ProgressIndicator.HideAsync();
                        if (shareDataManager.FrequentDataSource.hasItem)
                        {
                            var result = from t in shareDataManager.FrequentDataSource.Items
                                         group t by t.frequent into g
                                         select new { Key = g.Key, Items = g };
                            groupFrequentlyFeed.Source = result;
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        #region Prepare AppBars
        private void PrepareAppBars()
        {
            try
            {
                mainBar = new CommandBar();
                mainBar.Background = new SolidColorBrush(Color.FromArgb(255, 44, 69, 102));
                mainBar.IsOpen = true;
                lookbackBtn = new AppBarButton() { Icon = new BitmapIcon() { UriSource = new Uri("ms-appx:///Assets/icons/appbar.sort.numeric.png") } };
                lookbackBtn.Label = "lookback";
                lookbackBtn.Tag = "stats";
                lookbackBtn.Click += appbarBtn_Clicked;
                lookbackBtn.IsEnabled = false;
                mainBar.PrimaryCommands.Add(lookbackBtn);
            }
            catch (Exception ex) { }
        }
        private void appbarBtn_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                var Button = sender as Button;
                if (Button == null) return;
                switch ((string)Button.Tag)
                {
                    case "stats":
                        lookbackFlyout.ShowAt(BottomAppBar);
                        break;
                }
            }
            catch (Exception ex) { }
        }
        private void generateFlyout(int number)
        {
            try
            {
                List<TimeModel> data = new List<TimeModel>();
                for (int i = 1; i <= number; i++)
                {
                    data.Add(new TimeModel(i.ToString(), i.ToString()));
                }
                this.timeList.ItemsSource = data;
                if (data.Count() > 0)
                {
                    lookbackBtn.IsEnabled = true;
                }
            }
            catch (Exception ex) { }
        }
        private async void timeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListView listview = (ListView)sender;
                if (listview.SelectedItem == null)
                    return;

                TimeModel entry = (TimeModel)listview.SelectedItem;
                statusBar.ProgressIndicator.Text = "Loading...";
                await statusBar.ProgressIndicator.ShowAsync();
                lookback.Header = "สถิติย้อนหลัง " + entry.Number + " ปี";
                generateContent(entry.Number);
                await statusBar.ProgressIndicator.HideAsync();
                listview.SelectedItem = null;
                lookbackFlyout.Hide();
            }
            catch (Exception ex) { }
        }
        #endregion

        #region controll hub section
        private void HubControl_SectionsInViewChanged(object sender, SectionsInViewChangedEventArgs e)
        {
            try
            {
                var section = HubControl.SectionsInView[0];
                var tag = section.Tag.ToString();
                switch(tag)
                {
                    case "stats":
                        //if (BottomAppBar != null && BottomAppBar.Visibility == Visibility.Collapsed)
                        //    BottomAppBar.Visibility = Visibility.Visible;
                        break;
                    case "frequent":
                        //if (BottomAppBar != null && BottomAppBar.Visibility == Visibility.Collapsed)
                        //    BottomAppBar.Visibility = Visibility.Visible;
                        break;
                    case "graph":
                        //if (BottomAppBar != null && BottomAppBar.Visibility == Visibility.Visible)
                        //    BottomAppBar.Visibility = Visibility.Collapsed;
                        break;
                }
            }
            catch (Exception ex) { }
        }
        #endregion

        #region generate graph 
        private async void ChartDataSource_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == "IsLoading")
                {
                    if (shareDataManager.ChartDataSource.IsLoading == false)
                    {
                        await statusBar.ProgressIndicator.HideAsync();
                        if (shareDataManager.ChartDataSource.hasItem)
                        {
                            var Items = shareDataManager.ChartDataSource.Items;
                            foreach (var get in Items)
                            {
                                var lotto = get.graph as JProperty;
                                switch (lotto.Name)
                                {
                                    case "tens":
                                        List<ChartModel> tensList = new List<ChartModel>();
                                        foreach (var tens in lotto.Value)
                                        {
                                            tensList.Add(new ChartModel() { Name = (string)tens["digit"], Value = (int)tens["count"], Group = "หลักสิบ" });
                                        }
                                        var tensResult = from t in tensList
                                                     group t by t.Group into g
                                         select new { Key = g.Key, Items = g };
                                        groupTensFeed.Source = tensResult;
                                        
                                        break;
                                    case "digit":
                                        List<ChartModel> digitList = new List<ChartModel>();
                                        foreach (var digit in lotto.Value)
                                        {
                                            digitList.Add(new ChartModel() { Name = (string)digit["digit"], Value = (int)digit["count"], Group = "หลักหน่วย" });
                                        }

                                        var digitResult = from t in digitList
                                                     group t by t.Group into g
                                         select new { Key = g.Key, Items = g };
                                        groupDigitFeed.Source = digitResult;
                                        
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }
        #endregion

        #region navigation back
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }
        private async void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            try
            {
                e.Handled = true;
                var frame = Window.Current.Content as Frame;
                if (frame != null && frame.CanGoBack)
                {
                    frame.GoBack();
                }
                else
                {
                    var msg = new MessageDialog("ออกไปซื้อหวย?", "LOTTO STATS");
                    var okBtn = new UICommand("OK");
                    var cancelBtn = new UICommand("Cancel");
                    msg.Commands.Add(okBtn);
                    msg.Commands.Add(cancelBtn);
                    IUICommand result = await msg.ShowAsync();

                    if (result != null && result.Label == "OK")
                    {
                        Application.Current.Exit();
                    }
                }

            }
            catch (Exception ex) { }
        }
        #endregion
    }
}
