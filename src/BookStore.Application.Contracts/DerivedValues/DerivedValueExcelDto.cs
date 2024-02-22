using System;

namespace BookStore.DerivedValues
{
    public class DerivedValueExcelDto
    {
        public bool Active { get; set; }
        public string DVDescription { get; set; }
        public int DVRecordValue { get; set; }
        public int DVThresholdTime { get; set; }
        public int DVThresholdTimeUnit { get; set; }
        public string DVFormula { get; set; }
    }
}