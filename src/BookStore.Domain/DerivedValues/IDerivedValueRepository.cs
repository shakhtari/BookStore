using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace BookStore.DerivedValues
{
    public interface IDerivedValueRepository : IRepository<DerivedValue, int>
    {
        Task<List<DerivedValue>> GetListAsync(
            string filterText = null,
            bool? active = null,
            string dVDescription = null,
            int? dVRecordValueMin = null,
            int? dVRecordValueMax = null,
            int? dVThresholdTimeMin = null,
            int? dVThresholdTimeMax = null,
            int? dVThresholdTimeUnitMin = null,
            int? dVThresholdTimeUnitMax = null,
            string dVFormula = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            bool? active = null,
            string dVDescription = null,
            int? dVRecordValueMin = null,
            int? dVRecordValueMax = null,
            int? dVThresholdTimeMin = null,
            int? dVThresholdTimeMax = null,
            int? dVThresholdTimeUnitMin = null,
            int? dVThresholdTimeUnitMax = null,
            string dVFormula = null,
            CancellationToken cancellationToken = default);
    }
}