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
//using WebNet.Permissions;
using BookStore.MimicDiagrams;
//using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using System.Xml.Serialization;
//using WebNet.Shared;
//using WebNet.Devices;
//using WebNet.Organizations;

namespace BookStore.MimicDiagrams
{
    [RemoteService(IsEnabled = false)]
    //[Authorize(WebNetPermissions.MimicDiagrams.Default)]
    public class MimicDiagramsAppService : ApplicationService, IMimicDiagramsAppService
    {
        //private readonly IDistributedCache<MimicDiagramExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IMimicDiagramRepository _mimicDiagramRepository;
        //private readonly MimicDiagramManager _mimicDiagramManager;

        public MimicDiagramsAppService(IMimicDiagramRepository mimicDiagramRepository)
        {
            //_excelDownloadTokenCache = excelDownloadTokenCache;
            _mimicDiagramRepository = mimicDiagramRepository;
                                                                                                                                                                                                    
        }

        public async Task<IReadOnlyList<MimicDiagramDto>> GetAllAsync()
        {
            var items = await _mimicDiagramRepository.GetListAsync();
            return items
                .Select(item => new MimicDiagramDto
                {
                    Id = item.Id,
                    MimicDiagramName = item.MimicDiagramName,
                    MimicDiagramAuthorization = item.MimicDiagramAuthorization
                }).ToList();
        }

        //      public virtual async Task<PagedResultDto<MimicDiagramDto>> GetListAsync(GetMimicDiagramsInput input)
        //      {
        //          var totalCount = await _mimicDiagramRepository.GetCountAsync(input.FilterText, input.Active, input.MimicDiagramName, input.MimicDiagramDescription, input.MimicDiagramXML, input.MimicDiagramAuthorization);
        //          var items = await _mimicDiagramRepository.GetListAsync(input.FilterText, input.Active, input.MimicDiagramName, input.MimicDiagramDescription, input.MimicDiagramXML, input.MimicDiagramAuthorization, input.Sorting, input.MaxResultCount, input.SkipCount);

        //          return new PagedResultDto<MimicDiagramDto>
        //          {
        //              TotalCount = totalCount,
        //              Items = ObjectMapper.Map<List<MimicDiagram>, List<MimicDiagramDto>>(items)
        //          };
        //      }

        //      public virtual async Task<MimicDiagramDto> GetAsync(int id)
        //      {
        //          return ObjectMapper.Map<MimicDiagram, MimicDiagramDto>(await _mimicDiagramRepository.GetAsync(id));
        //      }

        //      [Authorize(WebNetPermissions.MimicDiagrams.Delete)]
        //      public virtual async Task DeleteAsync(int id)
        //      {
        //          await _mimicDiagramRepository.DeleteAsync(id);
        //      }

        //      [Authorize(WebNetPermissions.MimicDiagrams.Create)]
        //      public virtual async Task<MimicDiagramDto> CreateAsync(MimicDiagramCreateDto input)
        //      {

        //          var mimicDiagram = await _mimicDiagramManager.CreateAsync(
        //          input.Active, input.MimicDiagramName, input.MimicDiagramDescription, input.MimicDiagramXML, input.MimicDiagramAuthorization
        //          );

        //          return ObjectMapper.Map<MimicDiagram, MimicDiagramDto>(mimicDiagram);
        //      }

        //      [Authorize(WebNetPermissions.MimicDiagrams.Edit)]
        //      public virtual async Task<MimicDiagramDto> UpdateAsync(int id, MimicDiagramUpdateDto input)
        //      {

        //          var mimicDiagram = await _mimicDiagramManager.UpdateAsync(
        //          id,
        //          input.Active, input.MimicDiagramName, input.MimicDiagramDescription, input.MimicDiagramXML, input.MimicDiagramAuthorization, input.ConcurrencyStamp
        //          );

        //          return ObjectMapper.Map<MimicDiagram, MimicDiagramDto>(mimicDiagram);
        //      }

        //      [AllowAnonymous]
        //      public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(MimicDiagramExcelDownloadDto input)
        //      {
        //          var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
        //          if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        //          {
        //              throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        //          }

        //          var items = await _mimicDiagramRepository.GetListAsync(input.FilterText, input.Active, input.MimicDiagramName, input.MimicDiagramDescription, input.MimicDiagramXML, input.MimicDiagramAuthorization);

        //          var memoryStream = new MemoryStream();
        //          await memoryStream.SaveAsAsync(ObjectMapper.Map<List<MimicDiagram>, List<MimicDiagramExcelDto>>(items));
        //          memoryStream.Seek(0, SeekOrigin.Begin);

        //          return new RemoteStreamContent(memoryStream, "MimicDiagrams.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        //      }

        //      public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        //      {
        //          var token = Guid.NewGuid().ToString("N");

        //          await _excelDownloadTokenCache.SetAsync(
        //              token,
        //              new MimicDiagramExcelDownloadTokenCacheItem { Token = token },
        //              new DistributedCacheEntryOptions
        //              {
        //                  AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
        //              });

        //          return new DownloadTokenResultDto
        //          {
        //              Token = token
        //          };
        //      }

        //public async Task<List<LookupDto<int>>> GetOrganizationMimicDiagramLookupAsync(int organizationId)
        //{
        //	var mimicList = (await _mimicDiagramRepository.GetListAsync(a => a.Active && a.MimicDiagramAuthorization==true));
        //	return ObjectMapper.Map<List<MimicDiagram>, List<LookupDto<int>>>(mimicList).OrderBy(a=>a.DisplayName).ToList();
        //}
    }
}