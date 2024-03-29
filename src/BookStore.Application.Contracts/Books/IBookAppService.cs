﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BookStore.Books
{
    public interface IBookAppService : IApplicationService
    {
        Task<IReadOnlyList<BookDto>> GetListAsync();
        Task<IReadOnlyList<BookDto>> GetTranslatorListAsync(string translatorName);
        Task<BookDto> CreateAsync(CreateUpdateBookDto bookDto);
        Task<BookDto> UpdateAsync(Guid id, CreateUpdateBookDto bookDto);
        Task DeleteAsync(Guid id);
        Task<bool> IsNameUnique(string name,Guid id);
    }
}
