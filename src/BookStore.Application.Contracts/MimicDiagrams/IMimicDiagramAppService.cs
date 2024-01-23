using BookStore.Books;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace BookStore.MimicDiagrams
{
    public interface IMimicDiagramAppService: IApplicationService
    {
        Task<IReadOnlyList<MimicDiagramDto>> GetListAsync();
    }
}
