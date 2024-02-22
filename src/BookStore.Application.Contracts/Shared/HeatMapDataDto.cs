using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Shared
{
    public class HeatMapDataDto<T>
    {
        public DateTime Date { get; set; }
        public T Value { get; set; }

        public string DayText
        {
            get
            {
                return Date.ToString("dd MMMM");
            }
        }

        public string HourText
        {
            get
            {
                return Date.ToString("HH:mm");
            }
        }
    }
}
