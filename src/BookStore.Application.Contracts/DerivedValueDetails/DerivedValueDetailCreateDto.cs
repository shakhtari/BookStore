using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BookStore.DerivedValueDetails
{
    public class DerivedValueDetailCreateDto
    {
        public int Id { get; set; }
        public int DVId { get; set; }
        public int DVDeviceId { get; set; }
        public int DVLabelId { get; set; }
        public int DVItemNr { get; set; }
        public int DVMathOperator { get; set; } = 1;
        public decimal DVConstantValue { get; set; } = 1;
    }
}