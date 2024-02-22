using BookStore.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using BookStore.Shared;
using BookStore.MimicProfiles;

namespace BookStore.MimicProfiles
{
    public interface IMimicProfileAppService: IApplicationService
    {
        Task<IReadOnlyList<MimicProfileDto>> GetListAsync();
        Task<MimicProfileDto> CreateAsync(CreateUpdateMimicProfileDto mimicProfileDto);
        Task<MimicProfileDto> UpdateAsync(int id, CreateUpdateMimicProfileDto mimicProfileDtookDto);
        Task DeleteAsync(int id);
        Task<bool> IsNameUnique(string name, int id);
        Task<bool> GetMimicProfileLookupAsync(LookupRequestWithIdDto input);
    }
}
