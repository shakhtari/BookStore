using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace BookStore.DerivedValueDetails
{
    public class DerivedValueDetailDto : FullAuditedEntityDto<int>, IHasConcurrencyStamp
    {
        public int DVId { get; set; }
        public int DVDeviceId { get; set; }
        public int DVLabelId { get; set; }
        public int DVItemNr { get; set; }
        public int DVMathOperator { get; set; }
        public decimal DVConstantValue { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}