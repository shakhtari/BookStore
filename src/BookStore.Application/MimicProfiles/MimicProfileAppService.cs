using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Application.Dtos;
using BookStore.Books;
using BookStore.Shared;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

namespace BookStore.MimicProfiles
{
    public class MimicProfileAppService : BookStoreAppService, IMimicProfileAppService
    {
        private readonly IRepository<MimicProfile, int> _mimicProfileRepository;
        
        public MimicProfileAppService(IRepository<MimicProfile, int> mimicProfileRepository)
        {
            _mimicProfileRepository = mimicProfileRepository;
        }
        public async Task<MimicProfileDto> CreateAsync(CreateUpdateMimicProfileDto mimicProfileDto)
        { 
          
                var mimicProfile = await _mimicProfileRepository.InsertAsync(new MimicProfile
                {
                    MimicProfileName = mimicProfileDto.MimicProfileName,
                    MimicProfileDetail = mimicProfileDto.MimicProfileDetail
                });
                return ObjectMapper.Map<MimicProfile, MimicProfileDto>(mimicProfile);
            
        }
        public  async Task<bool> GetMimicProfileLookupAsync(LookupRequestWithIdDto input)
        {
            var query = (await _mimicProfileRepository.GetQueryableAsync());

            var lookupData= query.Any(x => x.MimicProfileName == input.Filter && x.Id != input.Id);
            return lookupData;
        }
        public async Task DeleteAsync(int profileId)
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

        public async Task<MimicProfileDto> UpdateAsync(int profileId, CreateUpdateMimicProfileDto input)
        {
            var mimicProfile = await _mimicProfileRepository.GetAsync(profileId);

            mimicProfile.MimicProfileName = input.MimicProfileName;
            mimicProfile.MimicProfileDetail = input.MimicProfileDetail;

            mimicProfile = await _mimicProfileRepository.UpdateAsync(mimicProfile);

            return ObjectMapper.Map<MimicProfile, MimicProfileDto>(mimicProfile);
        }
        public Task<bool> IsNameUnique(string name, int id)
        {
            var isUnique = _mimicProfileRepository.AnyAsync(x => x.MimicProfileName == name && x.Id != id);
            return isUnique;
        }
    }
}
