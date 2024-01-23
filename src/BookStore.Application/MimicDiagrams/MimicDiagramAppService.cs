using BookStore.Books;
using BookStore.MimicDiagrams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace BookStore.MimicDiagrams
{
    public class MimicDiagramAppService : BookStoreAppService, IMimicDiagramAppService
    {
        private readonly IRepository<MimicDiagram,int> _mimicDiagramRepository;
        public MimicDiagramAppService(IRepository<MimicDiagram,int> mimicDiagramRepository)
        {
            _mimicDiagramRepository = mimicDiagramRepository;
        }
        public async Task<IReadOnlyList<MimicDiagramDto>> GetListAsync()
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
    }
}
