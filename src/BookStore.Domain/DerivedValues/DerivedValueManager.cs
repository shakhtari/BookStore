using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace BookStore.DerivedValues
{
    public class DerivedValueManager : DomainService
    {
        private readonly IDerivedValueRepository _derivedValueRepository;

        public DerivedValueManager(IDerivedValueRepository derivedValueRepository)
        {
            _derivedValueRepository = derivedValueRepository;
        }

        public async Task<DerivedValue> CreateAsync(
        bool active, string dVDescription, int dVRecordValue, int dVThresholdTime, int dVThresholdTimeUnit, string dVFormula)
        {
            var derivedValue = new DerivedValue(

             active, dVDescription, dVRecordValue, dVThresholdTime, dVThresholdTimeUnit, dVFormula
             );

            return await _derivedValueRepository.InsertAsync(derivedValue);
        }

        public async Task<DerivedValue> UpdateAsync(
            int id,
            bool active, string dVDescription, int dVRecordValue, int dVThresholdTime, int dVThresholdTimeUnit, string dVFormula, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _derivedValueRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var derivedValue = await AsyncExecuter.FirstOrDefaultAsync(query);

            derivedValue.Active = active;
            derivedValue.DVDescription = dVDescription;
            derivedValue.DVRecordValue = dVRecordValue;
            derivedValue.DVThresholdTime = dVThresholdTime;
            derivedValue.DVThresholdTimeUnit = dVThresholdTimeUnit;
            derivedValue.DVFormula = dVFormula;

            derivedValue.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _derivedValueRepository.UpdateAsync(derivedValue);
        }

    }
}