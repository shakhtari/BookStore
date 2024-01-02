using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using BookStore.Books;
using System.Globalization;
using Syncfusion.Blazor;
using Syncfusion.Blazor.FileManager;
using Syncfusion.Blazor.Grids;
using BookStore.Localization;
using Microsoft.Extensions.Localization;
using Syncfusion.Blazor.Navigations;

namespace BookStore.Blazor.Pages.Books
{

    public partial class Books
    {
        [Inject]
        private IBookAppService BookAppService { get; set; }

        private IReadOnlyList<BookDto> BookList { get; set; }
        private SfGrid<BookDto> Grid;
        private CreateUpdateBookDto NewBookDto { get; set; }
        private CreateUpdateBookDto EditingBookDto { get; set; }
        private Guid EditingBookId { get; set; }
        private bool Loading { get; set; }
        private bool NewDialogOpen { get; set; }
        private bool EditingDialogOpen { get; set; }
        private readonly IStringLocalizer<BookStoreResource> _localizer;
        private List<Object> ToolbarItems;


        public Books(IStringLocalizer<BookStoreResource> localizer)
        {
            BookList = new List<BookDto>();
            NewBookDto = new CreateUpdateBookDto();
            EditingBookDto = new CreateUpdateBookDto();
            _localizer = localizer;
            ToolbarItems = new List<object>() {
            new ItemModel(){Text=_localizer["Add"],PrefixIcon="e-add",Id="Grid_add"},
            new ItemModel(){Text=_localizer["Edit"],PrefixIcon="e-edit",Id="Grid_edit"},
            new ItemModel(){Text=_localizer["Delete"],PrefixIcon="e-delete",Id="Grid_delete"},
            new ItemModel(){Text=_localizer["Update"],PrefixIcon="e-update",Id="Grid_update"},
            new ItemModel(){Text=_localizer["Cancel"],PrefixIcon="e-cancel",Id="Grid_cancel"}
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

        private Task OpenCreateBookModalAsync()
        {
            NewDialogOpen = true;
            this.StateHasChanged();
            NewBookDto = new CreateUpdateBookDto();

            return Task.CompletedTask;
        }


        public class SyncfusionLocalizer : ISyncfusionStringLocalizer
        {
            // To get the locale key from mapped resources file
            public string GetText(string key)
            {
                return this.ResourceManager.GetString(key);
            }

            // To access the resource file and get the exact value for locale key

            public System.Resources.ResourceManager ResourceManager
            {
                get
                {
                    // Replace the ApplicationNamespace with your application name.
                    return BookStore.Resources.SfResources.ResourceManager;
                }
            }
        }
        public void ActionBeginHandler(ActionEventArgs<BookDto> Args)
        {
            if (Args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
            {
                if (Args.Action == "Add")
                {
                    NewBookDto = new CreateUpdateBookDto()
                    {
                        Name = Args.Data.Name,
                        Price = Args.Data.Price,
                        ReleaseDate = Args.Data.ReleaseDate,
                    };
                    BookAppService.CreateAsync(NewBookDto);
                }
                else
                {
                    EditingBookId = Args.Data.Id;
                    EditingBookDto = new CreateUpdateBookDto
                    {
                        Name = Args.Data.Name,
                        Price = Args.Data.Price,
                        ReleaseDate = Args.Data.ReleaseDate,
                    };
                    BookAppService.UpdateAsync(Args.Data.Id, EditingBookDto);
                }
            }
            if (Args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Delete))
            {
                BookAppService.DeleteAsync(Args.Data.Id);
            }
        }
        public async Task ActionCompleteHandler(ActionEventArgs<BookDto> Args)
        {
            if (Args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
            {
                BookList = await BookAppService.GetListAsync();
            }
        }
    }
}

