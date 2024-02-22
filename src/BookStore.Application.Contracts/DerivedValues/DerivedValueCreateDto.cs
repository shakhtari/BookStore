using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BookStore.DerivedValues
{
    public class DerivedValueCreateDto
    {
        public int Id { get; set; }
        public bool Active { get; set; } = true;
        [Required]
        [StringLength(DerivedValueConsts.DVDescriptionMaxLength)]
        public string DVDescription { get; set; }
        public int DVRecordValue { get; set; } = 1;
        public int DVThresholdTime { get; set; }
        public int DVThresholdTimeUnit { get; set; } = 1;
        [Required]
        [StringLength(DerivedValueConsts.DVFormulaMaxLength)]
        public string DVFormula { get; set; }
    }
}