using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using BookStore.Books;
using System.Globalization;

namespace BookStore.Blazor.Pages.Books
{
    public partial class Books
    {
        [Inject]
        private IBookAppService BookAppService { get; set; }

        private IReadOnlyList<BookDto> BookList { get; set; }
        private CreateUpdateBookDto NewBookDto { get; set; }
        private CreateUpdateBookDto EditingBookDto { get; set; }
        private Guid EditingBookId { get; set; }
        private bool Loading { get; set; }
        private bool NewDialogOpen { get; set; }
        private bool EditingDialogOpen { get; set; }

        public Books()
        {
            BookList = new List<BookDto>();
            NewBookDto = new CreateUpdateBookDto();
            EditingBookDto = new CreateUpdateBookDto();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetBooksAsync();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task GetBooksAsync()
        {
            try
            {
                Loading = true;

                await InvokeAsync(() => StateHasChanged());

                BookList = await BookAppService.GetListAsync();
            }
            finally
            {
                Loading = false;

                await InvokeAsync(() => StateHasChanged());
            }
        }

        private Task OpenCreateBookModalAsync()
        {
            NewDialogOpen = true;
            this.StateHasChanged();
            NewBookDto = new CreateUpdateBookDto();

            return Task.CompletedTask;
        }

        private async Task CreateBookAsync()
        {
            try
            {
                await BookAppService.CreateAsync(NewBookDto);

                await GetBooksAsync();
            }
            finally
            {
                NewDialogOpen = false;
            }
        }

        private Task OpenEditingBookModalAsync(BookDto book)
        {
            EditingDialogOpen = true;
            this.StateHasChanged();
            EditingBookId = book.Id;
            EditingBookDto = new CreateUpdateBookDto
            {
                Name = book.Name,
                Price = book.Price,
                ReleaseDate = book.ReleaseDate,
            };

            return Task.CompletedTask;
        }

        private async Task UpdateBookAsync()
        {
            try
            {
                await BookAppService.UpdateAsync(EditingBookId, EditingBookDto);
            }
            finally
            {
                EditingDialogOpen = false;

                await GetBooksAsync();
            }
        }

        private async Task DeleteBookAsync(BookDto book)
        {
            var confirmMessage = L["BookDeletionConfirmationMessage", book.Name];
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await BookAppService.DeleteAsync(book.Id);
            await GetBooksAsync();
        }

            
    }
}

