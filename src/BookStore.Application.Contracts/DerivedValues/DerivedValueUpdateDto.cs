using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace BookStore.DerivedValues
{
    public class DerivedValueUpdateDto : IHasConcurrencyStamp
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        [Required]
        [StringLength(DerivedValueConsts.DVDescriptionMaxLength)]
        public string DVDescription { get; set; }
        public int DVRecordValue { get; set; }
        public int DVThresholdTime { get; set; }
        public int DVThresholdTimeUnit { get; set; }
        [Required]
        [StringLength(DerivedValueConsts.DVFormulaMaxLength)]
        public string DVFormula { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}