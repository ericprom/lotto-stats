using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTTO.STATS.Model
{
    public class TimeModel
    {
        public string Title { get; set; }
        public string Number { get; set; }
        public TimeModel(string Title, string Number)
        {
            this.Title = Title;
            this.Number = Number;

        }
    }
}
