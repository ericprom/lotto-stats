using LOTTO.STATS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTTO.STATS.Model
{
   public class DataManager
    {
        static volatile DataManager _classInstance = null;
        static readonly object _threadSafetyLock = new object();
        public static DataManager shareInstance
        {
            get
            {
                if (_classInstance == null)
                {
                    lock (_threadSafetyLock)
                    {
                        if (_classInstance == null)
                            _classInstance = new DataManager();
                    }
                }
                return _classInstance;
            }
        }
       
        private LookbackDataSource _LookbackDataSource;
        public LookbackDataSource LookbackDataSource
        {
            get { return _LookbackDataSource; }
            set
            {
                if (_LookbackDataSource != value)
                {
                    _LookbackDataSource = value;
                }
            }
        }
        private FrequentDataSource _FrequentDataSource;
        public FrequentDataSource FrequentDataSource
        {
            get { return _FrequentDataSource; }
            set
            {
                if (_FrequentDataSource != value)
                {
                    _FrequentDataSource = value;
                }
            }
        }
        private ChartDataSource _ChartDataSource;
        public ChartDataSource ChartDataSource
        {
            get { return _ChartDataSource; }
            set
            {
                if (_ChartDataSource != value)
                {
                    _ChartDataSource = value;
                }
            }
        }
       
        
    }
}