using Polly;
using Scriban.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;
namespace BookStore.Books
{
    public class BookAppService : BookStoreAppService, IBookAppService
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        public BookAppService(IRepository<Book, Guid> bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<IReadOnlyList<BookDto>> GetListAsync()
        {
            var items = await _bookRepository.GetListAsync();
            return items
                .Select(item => new BookDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    ReleaseDate = item.ReleaseDate,
                    Price = item.Price,
                    BookType = item.BookType,
                    Translator = item.Translator,
                    Publication = item.Publication
                }).ToList();
        }

        public async Task<IReadOnlyList<BookDto>> GetTranslatorListAsync(string translatorName)
        {
            var items =await _bookRepository.GetListAsync(item => item.Translator == translatorName);
            return items
                .Select(item => new BookDto
                 {
                     Id = item.Id,
                     Name = item.Name,
                     ReleaseDate = item.ReleaseDate,
                     Price = item.Price,
                     BookType = item.BookType,
                     Translator = item.Translator,
                     Publication = item.Publication
                 }).ToList();
        }

        public async Task<BookDto> CreateAsync(CreateUpdateBookDto bookDto)
        {
            var todoItem = await _bookRepository.InsertAsync(new Book
            {
                Name = bookDto.Name,
                ReleaseDate = bookDto.ReleaseDate,
                Price = bookDto.Price,
                BookType = bookDto.BookType,
                Translator = bookDto.Translator,
                Publication = bookDto.Publication
            });
            
                return new BookDto
                {
                    Id = Guid.NewGuid(),
                    Name = bookDto.Name,
                    ReleaseDate = bookDto.ReleaseDate,
                    Price = bookDto.Price,
                    BookType = bookDto.BookType,
                    Translator = bookDto.Translator,
                    Publication = bookDto.Publication

                };
            
        }

        public async Task<BookDto> UpdateAsync(Guid id, CreateUpdateBookDto input)
        {
            var book = await Task.Run(() => _bookRepository.GetAsync(id));

            book.Name = input.Name;
            book.Price = input.Price;
            book.ReleaseDate = input.ReleaseDate;
            book.BookType = input.BookType;
            book.Translator = input.Translator;
            book.Publication = input.Publication;

            book = await Task.Run(() => _bookRepository.UpdateAsync(book));

            return new BookDto
            {
                Id = book.Id,
                Name = book.Name,
                ReleaseDate = book.ReleaseDate,
                Price = book.Price,
                BookType = book.BookType,
                Translator = input.Translator,
                Publication = input.Publication

            };
        }

        public async Task DeleteAsync(Guid id)
        {
            await _bookRepository.DeleteAsync(id);
        }

        public Task<bool> IsNameUnique(string name,Guid id)
        {
            var isUnique=_bookRepository.AnyAsync(x => x.Name == name && x.Id!=id);
            return isUnique;
        }


    }
}
