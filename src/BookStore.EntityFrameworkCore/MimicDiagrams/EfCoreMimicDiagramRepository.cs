using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using BookStore.EntityFrameworkCore;

namespace BookStore.MimicDiagrams
{
    public class EfCoreMimicDiagramRepository : EfCoreRepository<BookStoreDbContext, MimicDiagram, int>, IMimicDiagramRepository
    {
        public EfCoreMimicDiagramRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }


        public Task<IReadOnlyList<MimicDiagram>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}