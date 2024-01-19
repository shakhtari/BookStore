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
using Blazorise;
using Syncfusion.Blazor.Calendars;


namespace BookStore.Blazor.Pages.Books
{

    public partial class Books
    {
        [Inject]
        private IBookAppService BookAppService { get; set; }

        private IReadOnlyList<BookDto> BookList { get; set; }
        private IReadOnlyList<BookDto> TranslatorBookList { get; set; }
        private IReadOnlyList<PublicationDto> PubList { get; set; }
        private CreateUpdateBookDto NewBookDto { get; set; }
        private CreateUpdateBookDto EditingBookDto { get; set; }
        private Guid EditingBookId { get; set; }
        private string EditingBookName { get; set; }
        private string EditingTranslatorName { get; set; }
        private Guid DeletingBookId { get; set; }
        private bool Loading { get; set; }
        private readonly IStringLocalizer<BookStoreResource> _localizer;
        public List<BookType> TypeLists;

        public Books(IStringLocalizer<BookStoreResource> localizer)
        {
            BookList = new List<BookDto>();
            TranslatorBookList = new List<BookDto>();
            NewBookDto = new CreateUpdateBookDto();
            EditingBookDto = new CreateUpdateBookDto();
            PubList = new List<PublicationDto>();
            _localizer = localizer;
            TypeLists = new List<BookType>
              {
                new BookType() { ID=0,TypeName=_localizer["Original Language"]},
                new BookType() {ID=1,TypeName=_localizer["Translation"]}
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
        private async Task GetTranslatorBooksList(InputEventArgs args)
        {
            TranslatorBookList = null;
            TranslatorBookList =await BookAppService.GetTranslatorListAsync(args.Value);
        }
        private async void RowSelectedHandler(RowSelectEventArgs<BookDto> args)
        {
            if (args.Data.BookType == 1)
            {
                TranslatorBookList = await BookAppService.GetTranslatorListAsync(args.Data.Translator);
            }
            else
            {
                TranslatorBookList = null;
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

        private bool Check = false;
        private bool IsVisible { get; set; } = false;
        SfGrid<BookDto> Grid;
        SfGrid<BookDto> NestedGrid;
        private BookDto book { get; set; }
        private DialogSettings DialogParams = new DialogSettings { Height="700px", Width="850px" };
        private void CancelClick()
        {
            GetBooksAsync();
            this.IsVisible = false;
        }
        private void OkClick()
        {
            BookAppService.DeleteAsync(DeletingBookId);
            this.IsVisible = false;
        }
        public async void ActionBeginHandler(ActionEventArgs<BookDto> args)
        {
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Add) || args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.BeginEdit))
            {
                
                //Handles Add Operation
                if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Add))
                {
                    Check = true;
                    TranslatorBookList = null;
                }
                
                //Handles Edit Operation
                if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.BeginEdit))
                {
                    EditingBookId = args.RowData.Id;
                    EditingBookName=args.RowData.Name;
                    EditingTranslatorName = args.RowData.Translator;
                    EditingBookDto = new CreateUpdateBookDto()
                    {
                        Id = args.RowData.Id,
                        Name = args.RowData.Name,
                        Price = args.RowData.Price,
                        ReleaseDate = args.RowData.ReleaseDate,
                        Translator = args.RowData.Translator,
                        Publication = args.RowData.Publication,
                        BookType = args.RowData.BookType
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
                    NewBookDto.Translator = args.Data.Translator;
                    NewBookDto.BookType = args.Data.BookType;
                    NewBookDto.Publication = args.Data.Publication;

                    BookAppService.CreateAsync(NewBookDto);
                }
                if (!Check)
                {
                    EditingBookDto.Id = args.Data.Id;
                    EditingBookDto.Name = args.Data.Name;
                    EditingBookDto.Price = args.Data.Price;
                    EditingBookDto.ReleaseDate = args.Data.ReleaseDate;
                    if (args.Data.BookType == 1)
                    {
                        EditingBookDto.Translator = args.Data.Translator;
                    }
                    else
                    {
                        EditingBookDto.Translator = null;
                        args.Data.Translator = null;
                    }

                    EditingBookDto.Publication = args.Data.Publication;
                    EditingBookDto.BookType = args.Data.BookType;
                    EditingBookId = args.Data.Id;
                    BookAppService.UpdateAsync(EditingBookId, EditingBookDto);
                }
            }
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Delete))
            {
                this.IsVisible = true;
                DeletingBookId = args.Data.Id;
            }
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Cancel))
            {
                args.Data.Translator = null;
            }
        }

        public class BookType
        { 
            public int ID { get; set; }
            public string TypeName { get; set; }
        }

        public string GetHeader(BookDto value)
        {
            if(value.Name == null)
            {
                return Ml["Insert New Book"];
            }
            else
            {
                return Ml["Edit "] + value.Name;
            }
        }


    }
}

