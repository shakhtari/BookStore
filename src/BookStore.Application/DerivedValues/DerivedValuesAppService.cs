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
using BookStore.DerivedValues;
//using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using BookStore.Shared;
using BookStore.MimicDiagrams;
using BookStore.MimicProfiles;
using Volo.Abp.ObjectMapping;
using AutoMapper;

namespace BookStore.DerivedValues
{
    //[RemoteService(IsEnabled = false)]
    //[Authorize(WebNetPermissions.DerivedValues.Default)]
    public class DerivedValuesAppService : ApplicationService, IDerivedValuesAppService
    {
        private readonly IDistributedCache<DerivedValueExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IRepository<DerivedValue, int> _derivedValueRepository;
        //private readonly DerivedValueManager _derivedValueManager;

        public DerivedValuesAppService(IRepository<DerivedValue, int> derivedValueRepository,  IDistributedCache<DerivedValueExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            //_excelDownloadTokenCache = excelDownloadTokenCache;
            _derivedValueRepository = derivedValueRepository;
            //_derivedValueManager = derivedValueManager;
        }

        //public virtual async Task<PagedResultDto<DerivedValueDto>> GetListAsync(GetDerivedValuesInput input)
        //{
        //    var totalCount = await _derivedValueRepository.GetCountAsync(input.FilterText, input.Active, input.DVDescription, input.DVRecordValueMin, input.DVRecordValueMax, input.DVThresholdTimeMin, input.DVThresholdTimeMax, input.DVThresholdTimeUnitMin, input.DVThresholdTimeUnitMax, input.DVFormula);
        //    var items = await _derivedValueRepository.GetListAsync(input.FilterText, input.Active, input.DVDescription, input.DVRecordValueMin, input.DVRecordValueMax, input.DVThresholdTimeMin, input.DVThresholdTimeMax, input.DVThresholdTimeUnitMin, input.DVThresholdTimeUnitMax, input.DVFormula, input.Sorting, input.MaxResultCount, input.SkipCount);

        //    return new PagedResultDto<DerivedValueDto>
        //    {
        //        TotalCount = totalCount,
        //        Items = ObjectMapper.Map<List<DerivedValue>, List<DerivedValueDto>>(items)
        //    };
        //}
        public async Task<IReadOnlyList<DerivedValueDto>> GetAllAsync()
        {
            var items = await _derivedValueRepository.GetListAsync();
            return items
                .Select(item => new DerivedValueDto{
                    Id= item.Id,
                    Active = item.Active,
                    DVDescription= item.DVDescription,
                    DVRecordValue= item.DVRecordValue,
                    DVThresholdTime= item.DVThresholdTime,
                    DVThresholdTimeUnit=item.DVThresholdTimeUnit,
                    DVFormula= item.DVFormula,
                }).ToList();
        }
        public virtual async Task<DerivedValueDto> GetAsync(int id)
        {
            return ObjectMapper.Map<DerivedValue, DerivedValueDto>(await _derivedValueRepository.GetAsync(id));
        }

        //[Authorize(WebNetPermissions.DerivedValues.Delete)]
        public virtual async Task DeleteAsync(int id)
        {
            await _derivedValueRepository.DeleteAsync(id);
        }

        //[Authorize(WebNetPermissions.DerivedValues.Create)]
        public virtual async Task<DerivedValueDto> CreateAsync(DerivedValueCreateDto input)
        {

            var derivedValue = await _derivedValueRepository.InsertAsync(new DerivedValue
            {
                Active= input.Active,
                DVDescription= input.DVDescription,
                DVRecordValue= input.DVRecordValue,
                DVThresholdTime= input.DVThresholdTime,
                DVThresholdTimeUnit= input.DVThresholdTimeUnit,
                DVFormula= input.DVFormula,
            });
            return ObjectMapper.Map<DerivedValue, DerivedValueDto>(derivedValue);
        }

        //[Authorize(WebNetPermissions.DerivedValues.Edit)]
        public virtual async Task<DerivedValueDto> UpdateAsync(int id, DerivedValueUpdateDto input)
        {

            var derivedValue = await _derivedValueRepository.GetAsync(id);

            derivedValue.Active = input.Active;
            derivedValue.DVDescription = input.DVDescription;
            derivedValue.DVRecordValue = input.DVRecordValue;
            derivedValue.DVThresholdTime = input.DVThresholdTime;
            derivedValue.DVThresholdTimeUnit = input.DVThresholdTimeUnit;
            derivedValue.DVFormula = input.DVFormula;

            derivedValue = await _derivedValueRepository.UpdateAsync(derivedValue);

            return ObjectMapper.Map<DerivedValue, DerivedValueDto>(derivedValue);
        }

        //[AllowAnonymous]
        //public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(DerivedValueExcelDownloadDto input)
        //{
        //    var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
        //    if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        //    {
        //        throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        //    }

        //    var items = await _derivedValueRepository.GetListAsync(input.FilterText, input.Active, input.DVDescription, input.DVRecordValueMin, input.DVRecordValueMax, input.DVThresholdTimeMin, input.DVThresholdTimeMax, input.DVThresholdTimeUnitMin, input.DVThresholdTimeUnitMax, input.DVFormula);

        //    var memoryStream = new MemoryStream();
        //    await memoryStream.SaveAsAsync(ObjectMapper.Map<List<DerivedValue>, List<DerivedValueExcelDto>>(items));
        //    memoryStream.Seek(0, SeekOrigin.Begin);

        //    return new RemoteStreamContent(memoryStream, "DerivedValues.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        //}

        //public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        //{
        //    var token = Guid.NewGuid().ToString("N");

        //    await _excelDownloadTokenCache.SetAsync(
        //        token,
        //        new DerivedValueExcelDownloadTokenCacheItem { Token = token },
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