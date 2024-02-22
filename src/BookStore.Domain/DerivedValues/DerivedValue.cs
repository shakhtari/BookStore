using BookStore.Formulas;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace BookStore.DerivedValues
{
    public class DerivedValue: FullAuditedAggregateRoot<int>
    {
        public virtual Guid? TenantId { get; set; }
        public virtual bool Active { get; set; }

        [NotNull]
        public virtual string DVDescription { get; set; }
        [NotNull]
        public virtual int DVThresholdTime { get; set; }
        public virtual int DVRecordValue { get; set; }
        public virtual int DVThresholdTimeUnit { get; set; }

        [NotNull]
        public virtual string DVFormula { get; set; }
        public DerivedValue()
        {

        }

        public DerivedValue(bool active, string dVDescription, int dVRecordValue, int dVThresholdTime, int dVThresholdTimeUnit, string dVFormula)
        {

            Check.NotNull(dVDescription, nameof(dVDescription));
            Check.Length(dVDescription, nameof(dVDescription), DerivedValueConsts.DVDescriptionMaxLength, 0);
            Check.NotNull(dVFormula, nameof(dVFormula));
            Check.Length(dVFormula, nameof(dVFormula), DerivedValueConsts.DVFormulaMaxLength, 0);
            Active = active;
            DVDescription = dVDescription;
            DVRecordValue = dVRecordValue;
            DVThresholdTime = dVThresholdTime;
            DVThresholdTimeUnit = dVThresholdTimeUnit;
            DVFormula = dVFormula;
        }
    }
    
}
