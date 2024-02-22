using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using BookStore.Shared;
using BookStore.DerivedValues;
using System.Collections.Generic;

namespace BookStore.DerivedValueDetails
{
    public interface IDerivedValueDetailsAppService : IApplicationService
    {
        //Task<PagedResultDto<DerivedValueDetailDto>> GetListAsync(GetDerivedValueDetailsInput input);

        Task<IReadOnlyList<DerivedValueDetailDto>> GetAllAsync();

        Task<IReadOnlyList<DerivedValueDetailDto>> GetDetailsList(int? id);
        Task<DerivedValueDetailDto> GetAsync(int id);

        Task DeleteAsync(int id);

        Task<DerivedValueDetailDto> CreateAsync(DerivedValueDetailCreateDto input);

        //Task<DerivedValueDetailDto> UpdateAsync(int id, DerivedValueDetailUpdateDto input);

        //Task<IRemoteStreamContent> GetListAsExcelFileAsync(DerivedValueDetailExcelDownloadDto input);

        //Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}