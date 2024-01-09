using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BookStore.Publications
{
    public class PubAppService : BookStoreAppService, IPubAppService
    {
        private readonly IRepository<Publication> _pubRepository;
        public PubAppService(IRepository<Publication> pubRepository)
        {
            _pubRepository = pubRepository;
        }
        public async Task<IReadOnlyList<PublicationDto>> GetListAsync()
        {
            var items = await _pubRepository.GetListAsync();
            return items
                .Select(item => new PublicationDto
                {
                    PubId = item.PubId, 
                    PubName = item.PubName
                }).ToList();
        }
    }
}
