using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace BookStore.MimicDiagrams
{
    public interface IMimicDiagramRepository : IRepository<MimicDiagram, int>
    {

        Task<IReadOnlyList<MimicDiagram>> GetAllAsync();
    }
}