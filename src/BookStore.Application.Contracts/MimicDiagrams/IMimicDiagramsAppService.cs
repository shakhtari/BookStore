using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
//using WebNet.Shared;

namespace BookStore.MimicDiagrams
{
    public interface IMimicDiagramsAppService : IApplicationService
    {
        //Task<PagedResultDto<MimicDiagramDto>> GetListAsync(GetMimicDiagramsInput input);
        Task<IReadOnlyList<MimicDiagramDto>> GetAllAsync();

        //Task<MimicDiagramDto> GetAsync(int id);

        //Task DeleteAsync(int id);

        //Task<MimicDiagramDto> CreateAsync(MimicDiagramCreateDto input);

        //Task<MimicDiagramDto> UpdateAsync(int id, MimicDiagramUpdateDto input);

        //Task<IRemoteStreamContent> GetListAsExcelFileAsync(MimicDiagramExcelDownloadDto input);

        //Task<DownloadTokenResultDto> GetDownloadTokenAsync();
        //Task<List<LookupDto<int>>> GetOrganizationMimicDiagramLookupAsync(int organizationId);

	}
}