using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using BookStore.Books;
using Syncfusion.Blazor.Grids;
using IdentityServer4.Models;
using Syncfusion.Blazor.Notifications;
using BookStore.Publications;
using Syncfusion.Blazor.Inputs;
using System.Runtime.CompilerServices;
using Syncfusion.Blazor.DropDowns;
using BookStore.Localization;
using Microsoft.Extensions.Localization;


namespace BookStore.Blazor.Pages.Books
{

    public partial class Books
    {
        [Inject]
        private IBookAppService BookAppService { get; set; }

        private IReadOnlyList<BookDto> BookList { get; set; }
        private IReadOnlyList<PublicationDto> PubList { get; set; }
        private CreateUpdateBookDto NewBookDto { get; set; }
        private CreateUpdateBookDto EditingBookDto { get; set; }
        private Guid EditingBookId { get; set; }
        private bool Loading { get; set; }
        private bool NewDialogOpen { get; set; }
        private bool EditingDialogOpen { get; set; }
        private readonly IStringLocalizer<BookStoreResource> _localizer;
        SfGrid<BookDto> Grid { get; set; }
        public List<BookType> TypeLists;

        public Books(IStringLocalizer<BookStoreResource> localizer)
        {
            BookList = new List<BookDto>();
            NewBookDto = new CreateUpdateBookDto();
            EditingBookDto = new CreateUpdateBookDto();
            PubList = new List<PublicationDto>();
            _localizer = localizer;

           TypeLists = new List<BookType>
    {
    new BookType() { ID=0 , TypeName=_localizer["Original Language"]},
    new BookType() { ID=1 , TypeName=_localizer["Translation"]}
    };
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

        //private async Task GetPubsAsync()
        //{
        //    try
        //    {
        //        Loading = true;

        //        await InvokeAsync(() => StateHasChanged());

        //        PubList = await PubAppService.GetListAsync();
        //    }
        //    finally
        //    {
        //        Loading = false;

        //        await InvokeAsync(() => StateHasChanged());
        //    }
        //}

        private Task OpenCreateBookModalAsync()
        {
            NewDialogOpen = true;
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
            EditingBookId = book.Id;
            EditingBookDto = new CreateUpdateBookDto
            {
                Name = book.Name,
                Price = book.Price,
                ReleaseDate = book.ReleaseDate
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
            var confirmMessage = Ml["BookDeletionConfirmationMessage", book.Name];
            //if (!await Message.Confirm(confirmMessage))
            //{
            //    return;
            //}

            await BookAppService.DeleteAsync(book.Id);
            await GetBooksAsync();
        }

        private bool Check = false;
        private bool isTranslated = false;

        private DialogSettings DialogParams = new DialogSettings { MinHeight = "400px", Width = "450px" };




        public void ActionBeginHandler(ActionEventArgs<BookDto> args)
        {
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Add) || args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.BeginEdit))
            {
                //Handles Add Operation
                if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Add))
                {
                    Check = true;

                }
                //Handles Edit Operation
                if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.BeginEdit))
                {
                    EditingBookId = args.RowData.Id;
                    EditingBookDto = new CreateUpdateBookDto()
                    {
                        Id = args.RowData.Id,
                        Name = args.RowData.Name,
                        Price = args.RowData.Price,
                        ReleaseDate = args.RowData.ReleaseDate
                    };
                }
            }
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
            {
                if (Check)
                {
                    NewBookDto = new CreateUpdateBookDto();
                    NewBookDto.Name = args.Data.Name;
                    NewBookDto.Price = args.Data.Price;
                    NewBookDto.ReleaseDate = args.Data.ReleaseDate;

                    BookAppService.CreateAsync(NewBookDto);
                }
                if (!Check)
                {
                    EditingBookDto.Id = args.Data.Id;
                    EditingBookDto.Name = args.Data.Name;
                    EditingBookDto.Price = args.Data.Price;
                    EditingBookDto.ReleaseDate = args.Data.ReleaseDate;
                    EditingBookId = args.Data.Id;
                    BookAppService.UpdateAsync(EditingBookId, EditingBookDto);
                }
            }
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Delete))
            {

                BookAppService.DeleteAsync(args.Data.Id);
            }
        }
        private RenderFragment dynamicComponent { get; set; }
        private void RenderComponent(ChangeEventArgs<string, BookType> args)
        {
            if (args.Value == "1")
            {
                dynamicComponent = CreateComponent();
            }
            else
            {
                dynamicComponent = null;
            }
        }

        RenderFragment CreateComponent() => builder =>
        {
            builder.OpenComponent(0, typeof(SfTextBox));
            builder.AddAttribute(1, "bind-value", "@book.Translator");
            builder.AddAttribute(2, "Placeholder", Ml["Translator"]);
            builder.CloseComponent();
        };

        public class BookType
        {
            public int ID { get; set; }
            public string TypeName { get; set; }
        }




    }
}

