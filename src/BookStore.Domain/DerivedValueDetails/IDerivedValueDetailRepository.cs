using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace BookStore.DerivedValueDetails
{
    public interface IDerivedValueDetailRepository : IRepository<DerivedValueDetail, int>
    {
        Task<List<DerivedValueDetail>> GetListAsync(
            string filterText = null,
            int? dVIdMin = null,
            int? dVIdMax = null,
            int? dVDeviceIdMin = null,
            int? dVDeviceIdMax = null,
            int? dVLabelIdMin = null,
            int? dVLabelIdMax = null,
            int? dVItemNrMin = null,
            int? dVItemNrMax = null,
            int? dVMathOperatorMin = null,
            int? dVMathOperatorMax = null,
            decimal? dVConstantValueMin = null,
            decimal? dVConstantValueMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            int? dVIdMin = null,
            int? dVIdMax = null,
            int? dVDeviceIdMin = null,
            int? dVDeviceIdMax = null,
            int? dVLabelIdMin = null,
            int? dVLabelIdMax = null,
            int? dVItemNrMin = null,
            int? dVItemNrMax = null,
            int? dVMathOperatorMin = null,
            int? dVMathOperatorMax = null,
            decimal? dVConstantValueMin = null,
            decimal? dVConstantValueMax = null,
            CancellationToken cancellationToken = default);
    }
}