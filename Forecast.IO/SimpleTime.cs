using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast_IO
{
    public class SimpleTime
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public SimpleTime()
        {

        }

        public SimpleTime(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }
    }
}
