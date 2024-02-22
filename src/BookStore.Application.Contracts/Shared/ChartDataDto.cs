//using Scriban.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using BookStore.Enums;

namespace BookStore.Shared
{
    public class ChartDataDto<T>
    {
        private string dateText = "";

        public DateTime Date { get; set; }
        public string Type { get; set; }
        public T Value { get; set; }
        public PeriodType PeriodType { get; set; } = PeriodType.Daily;

        public string DateText
        {
            get
            {
                if (dateText == "")
                {
                    if (PeriodType == PeriodType.Yearly) return Date.ToString("MMMM yy");

                    else if (Date.Hour == 0) return Date.ToString("dd MMMM");
                    else return Date.ToString("HH:mm");
                }
                else return dateText;
            }
            set
            {
                dateText = value;
            }
        }
    }
}
