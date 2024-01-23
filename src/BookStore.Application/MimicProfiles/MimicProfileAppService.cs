using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Application.Dtos;
using BookStore.Books;

namespace BookStore.MimicProfiles
{
    public class MimicProfileAppService : BookStoreAppService, IMimicProfileAppService
    {
        private readonly IRepository<MimicProfile , Guid> _mimicProfileRepository;
        public MimicProfileAppService(IRepository<MimicProfile, Guid> mimicProfileRepository)
        {
            _mimicProfileRepository = mimicProfileRepository;
        }
        public async Task<MimicProfileDto> CreateAsync(CreateUpdateMimicProfileDto mimicProfileDto)
        {
            var todoItem = await _mimicProfileRepository.InsertAsync(new MimicProfile
            {
                MimicProfileName = mimicProfileDto.MimicProfileName,
                MimicProfileDetail = mimicProfileDto.MimicProfileDetail
            });
            return new MimicProfileDto
            {
                Id = Guid.NewGuid(),
                MimicProfileName =mimicProfileDto.MimicProfileName,
                MimicProfileDetail=mimicProfileDto.MimicProfileDetail
            };
        }

        public async Task DeleteAsync(Guid profileId)
        {
            await _mimicProfileRepository.DeleteAsync(profileId);
        }

        public async Task<IReadOnlyList<MimicProfileDto>> GetListAsync()
        {
            var items = await _mimicProfileRepository.GetListAsync();
            return items
                .Select(item => new MimicProfileDto
                {
                    Id = item.Id,
                    MimicProfileName = item.MimicProfileName,
                    MimicProfileDetail = item.MimicProfileDetail
                }).ToList();
        }

        public async Task<MimicProfileDto> UpdateAsync(Guid profileId, CreateUpdateMimicProfileDto input)
        {
            var mimicProfile = await Task.Run(() => _mimicProfileRepository.GetAsync(profileId));
            
            mimicProfile.MimicProfileName = input.MimicProfileName;
            mimicProfile.MimicProfileDetail = input.MimicProfileDetail;

            mimicProfile = await Task.Run(() => _mimicProfileRepository.UpdateAsync(mimicProfile));

            return new MimicProfileDto
            {
                Id = mimicProfile.Id,
                MimicProfileName = mimicProfile.MimicProfileName,
                MimicProfileDetail = mimicProfile.MimicProfileDetail
            };
        }
        public Task<bool> IsNameUnique(string name, Guid id)
        {
            var isUnique = _mimicProfileRepository.AnyAsync(x => x.MimicProfileName == name && x.Id != id);
            return isUnique;
        }
    }
}
