using BookStore.Books;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BookStore.Publications
{
    public interface IPubAppService:IApplicationService
    {
        Task<IReadOnlyList<PublicationDto>> GetListAsync();
    }
}
