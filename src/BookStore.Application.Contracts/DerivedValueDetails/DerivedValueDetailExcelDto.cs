using System;

namespace BookStore.DerivedValueDetails
{
    public class DerivedValueDetailExcelDto
    {
        public int DVId { get; set; }
        public int DVDeviceId { get; set; }
        public int DVLabelId { get; set; }
        public int DVItemNr { get; set; }
        public int DVMathOperator { get; set; }
        public decimal DVConstantValue { get; set; }
    }
}