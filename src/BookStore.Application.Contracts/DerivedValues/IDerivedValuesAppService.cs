using BookStore.MimicDiagrams;
using BookStore.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace BookStore.DerivedValues
{
    public interface IDerivedValuesAppService : IApplicationService
    {
        //Task<PagedResultDto<DerivedValueDto>> GetListAsync(GetDerivedValuesInput input);
        Task<IReadOnlyList<DerivedValueDto>> GetAllAsync();

        Task<DerivedValueDto> GetAsync(int id);

        Task DeleteAsync(int id);

        Task<DerivedValueDto> CreateAsync(DerivedValueCreateDto input);

        Task<DerivedValueDto> UpdateAsync(int id, DerivedValueUpdateDto input);

        //Task<IRemoteStreamContent> GetListAsExcelFileAsync(DerivedValueExcelDownloadDto input);

        //Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}