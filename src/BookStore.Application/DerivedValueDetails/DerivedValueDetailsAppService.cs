using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using BookStore.Permissions;
using BookStore.DerivedValueDetails;
//using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using BookStore.Shared;
using BookStore.DerivedValues;
using Volo.Abp.ObjectMapping;
using BookStore.MimicProfiles;

namespace BookStore.DerivedValueDetails
{
    //[RemoteService(IsEnabled = false)]
    //[Authorize(BookStorePermissions.DerivedValueDetails.Default)]
    public class DerivedValueDetailsAppService : ApplicationService, IDerivedValueDetailsAppService
    {
        private readonly IDistributedCache<DerivedValueDetailExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IRepository<DerivedValueDetail, int> _derivedValueDetailRepository;
        //private readonly DerivedValueDetailManager _derivedValueDetailManager;

        public DerivedValueDetailsAppService(IRepository<DerivedValueDetail, int> derivedValueDetailRepository, /*DerivedValueDetailManager derivedValueDetailManager,*/ IDistributedCache<DerivedValueDetailExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _derivedValueDetailRepository = derivedValueDetailRepository;
            //_derivedValueDetailManager = derivedValueDetailManager;
        }

        //public virtual async Task<PagedResultDto<DerivedValueDetailDto>> GetListAsync(GetDerivedValueDetailsInput input)
        //{
        //    var totalCount = await _derivedValueDetailRepository.GetCountAsync(input.FilterText, input.DVIdMin, input.DVIdMax, input.DVDeviceIdMin, input.DVDeviceIdMax, input.DVLabelIdMin, input.DVLabelIdMax, input.DVItemNrMin, input.DVItemNrMax, input.DVMathOperatorMin, input.DVMathOperatorMax, input.DVConstantValueMin, input.DVConstantValueMax);
        //    var items = await _derivedValueDetailRepository.GetListAsync(input.FilterText, input.DVIdMin, input.DVIdMax, input.DVDeviceIdMin, input.DVDeviceIdMax, input.DVLabelIdMin, input.DVLabelIdMax, input.DVItemNrMin, input.DVItemNrMax, input.DVMathOperatorMin, input.DVMathOperatorMax, input.DVConstantValueMin, input.DVConstantValueMax, input.Sorting, input.MaxResultCount, input.SkipCount);

        //    return new PagedResultDto<DerivedValueDetailDto>
        //    {
        //        TotalCount = totalCount,
        //        Items = ObjectMapper.Map<List<DerivedValueDetail>, List<DerivedValueDetailDto>>(items)
        //    };
        //}
        public async Task<IReadOnlyList<DerivedValueDetailDto>> GetAllAsync()
        {
            var items = await _derivedValueDetailRepository.GetListAsync();
            return items
                .Select(item => new DerivedValueDetailDto
                {
                    DVId = item.DVId,
                    DVDeviceId = item.DVDeviceId,
                    DVLabelId = item.DVLabelId,
                    DVItemNr = item.DVItemNr,
                    DVMathOperator = item.DVMathOperator,
                    DVConstantValue = item.DVConstantValue,
                }).ToList();
        }


        public virtual async Task<DerivedValueDetailDto> GetAsync(int id)
        {
            return ObjectMapper.Map<DerivedValueDetail, DerivedValueDetailDto>(await _derivedValueDetailRepository.GetAsync(id));
        }

        //[Authorize(WebNetPermissions.DerivedValueDetails.Delete)]
        public virtual async Task DeleteAsync(int id)
        {
            await _derivedValueDetailRepository.DeleteAsync(id);
        }

        public virtual async Task<IReadOnlyList<DerivedValueDetailDto>> GetDetailsList(int? id)
        {
            var queryable = (await _derivedValueDetailRepository.GetQueryableAsync());
            var detailsList = queryable.ToList();

            detailsList = queryable.Where(x => x.DVId == id).ToList();


            return ObjectMapper.Map<List<DerivedValueDetail>, List<DerivedValueDetailDto>>(detailsList);
        }

        //[Authorize(WebNetPermissions.DerivedValueDetails.Create)]
        public virtual async Task<DerivedValueDetailDto> CreateAsync(DerivedValueDetailCreateDto input)
        {
            var derivedValueDetail = await _derivedValueDetailRepository.InsertAsync(new DerivedValueDetail
            {
                DVId = input.DVId,
                DVDeviceId = input.DVDeviceId,
                DVLabelId = input.DVLabelId,
                DVItemNr = input.DVItemNr,
                DVMathOperator = input.DVMathOperator,
                DVConstantValue = 1
        });
            return ObjectMapper.Map<DerivedValueDetail, DerivedValueDetailDto>(derivedValueDetail);
        }

    //[Authorize(WebNetPermissions.DerivedValueDetails.Edit)]
    //public virtual async Task<DerivedValueDetailDto> UpdateAsync(int id, DerivedValueDetailUpdateDto input)
    //{

    //    var derivedValueDetail = await _derivedValueDetailManager.UpdateAsync(
    //    id,
    //    input.DVId, input.DVDeviceId, input.DVLabelId, input.DVItemNr, input.DVMathOperator, input.DVConstantValue, input.ConcurrencyStamp
    //    );

    //    return ObjectMapper.Map<DerivedValueDetail, DerivedValueDetailDto>(derivedValueDetail);
    //}

    //[AllowAnonymous]
    //public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(DerivedValueDetailExcelDownloadDto input)
    //{
    //    var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
    //    if (downloadToken == null || input.DownloadToken != downloadToken.Token)
    //    {
    //        throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
    //    }

    //    var items = await _derivedValueDetailRepository.GetListAsync(input.FilterText, input.DVIdMin, input.DVIdMax, input.DVDeviceIdMin, input.DVDeviceIdMax, input.DVLabelIdMin, input.DVLabelIdMax, input.DVItemNrMin, input.DVItemNrMax, input.DVMathOperatorMin, input.DVMathOperatorMax, input.DVConstantValueMin, input.DVConstantValueMax);

    //    var memoryStream = new MemoryStream();
    //    await memoryStream.SaveAsAsync(ObjectMapper.Map<List<DerivedValueDetail>, List<DerivedValueDetailExcelDto>>(items));
    //    memoryStream.Seek(0, SeekOrigin.Begin);

    //    return new RemoteStreamContent(memoryStream, "DerivedValueDetails.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    //}

    //public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    //{
    //    var token = Guid.NewGuid().ToString("N");

    //    await _excelDownloadTokenCache.SetAsync(
    //        token,
    //        new DerivedValueDetailExcelDownloadTokenCacheItem { Token = token },
    //        new DistributedCacheEntryOptions
    //        {
    //            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
    //        });

    //    return new DownloadTokenResultDto
    //    {
    //        Token = token
    //    };
    //}
}
}