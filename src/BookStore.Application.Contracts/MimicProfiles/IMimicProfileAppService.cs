using BookStore.Books;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace BookStore.MimicProfiles
{
    public interface IMimicProfileAppService: IApplicationService
    {
        Task<IReadOnlyList<MimicProfileDto>> GetListAsync();
        Task<MimicProfileDto> CreateAsync(CreateUpdateMimicProfileDto mimicProfileDto);
        Task<MimicProfileDto> UpdateAsync(Guid id, CreateUpdateMimicProfileDto mimicProfileDtookDto);
        Task DeleteAsync(Guid id);
        Task<bool> IsNameUnique(string name, Guid id);
    }
}
