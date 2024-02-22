using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace BookStore.DerivedValues
{
    public class DerivedValueDto : FullAuditedEntityDto<int>, IHasConcurrencyStamp
    {
        public bool Active { get; set; }
        public string DVDescription { get; set; }
        public int DVRecordValue { get; set; }
        public int DVThresholdTime { get; set; }
        public int DVThresholdTimeUnit { get; set; }
        public string DVFormula { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}