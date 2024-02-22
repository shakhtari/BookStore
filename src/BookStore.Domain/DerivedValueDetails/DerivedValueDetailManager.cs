using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace BookStore.DerivedValueDetails
{
    public class DerivedValueDetailManager : DomainService
    {
        private readonly IDerivedValueDetailRepository _derivedValueDetailRepository;

        public DerivedValueDetailManager(IDerivedValueDetailRepository derivedValueDetailRepository)
        {
            _derivedValueDetailRepository = derivedValueDetailRepository;
        }

        public async Task<DerivedValueDetail> CreateAsync(
        int dVId, int dVDeviceId, int dVLabelId, int dVItemNr, int dVMathOperator, decimal dVConstantValue)
        {
            var derivedValueDetail = new DerivedValueDetail(

             dVId, dVDeviceId, dVLabelId, dVItemNr, dVMathOperator, dVConstantValue
             );

            return await _derivedValueDetailRepository.InsertAsync(derivedValueDetail);
        }

        public async Task<DerivedValueDetail> UpdateAsync(
            int id,
            int dVId, int dVDeviceId, int dVLabelId, int dVItemNr, int dVMathOperator, decimal dVConstantValue, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _derivedValueDetailRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var derivedValueDetail = await AsyncExecuter.FirstOrDefaultAsync(query);

            derivedValueDetail.DVId = dVId;
            derivedValueDetail.DVDeviceId = dVDeviceId;
            derivedValueDetail.DVLabelId = dVLabelId;
            derivedValueDetail.DVItemNr = dVItemNr;
            derivedValueDetail.DVMathOperator = dVMathOperator;
            derivedValueDetail.DVConstantValue = dVConstantValue;

            derivedValueDetail.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _derivedValueDetailRepository.UpdateAsync(derivedValueDetail);
        }

    }
}