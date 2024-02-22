using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace BookStore.DerivedValueDetails
{
    public class DerivedValueDetail : FullAuditedAggregateRoot<int>
    {
        public virtual int DVId { get; set; }

        public virtual int DVDeviceId { get; set; }

        public virtual int DVLabelId { get; set; }

        public virtual int DVItemNr { get; set; }

        public virtual int DVMathOperator { get; set; }

        public virtual decimal DVConstantValue { get; set; }

        public DerivedValueDetail()
        {

        }

        public DerivedValueDetail(int dVId, int dVDeviceId, int dVLabelId, int dVItemNr, int dVMathOperator, decimal dVConstantValue)
        {

            DVId = dVId;
            DVDeviceId = dVDeviceId;
            DVLabelId = dVLabelId;
            DVItemNr = dVItemNr;
            DVMathOperator = dVMathOperator;
            DVConstantValue = dVConstantValue;
        }

    }
}