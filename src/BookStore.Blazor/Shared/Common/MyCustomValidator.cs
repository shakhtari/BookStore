using BookStore.Books;
using BookStore.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using NUglify.JavaScript.Syntax;
using Syncfusion.Blazor.Grids;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;
using BookStore.Localization;
using Microsoft.Extensions.Localization;


namespace BookStore.Blazor.Shared.Common
{
    public class MyCustomValidator : ComponentBase
    {
        private readonly IBookAppService _bookAppService;
        private readonly IStringLocalizer<BookStoreResource> _localizer;
        public MyCustomValidator(IStringLocalizer<BookStoreResource> localizer ,IBookAppService bookAppService)
        {
            _localizer = localizer;
            _bookAppService = bookAppService;
        }
        [Parameter]
        public ValidatorTemplateContext context { get; set; }
        public ValidationMessageStore messageStore;
        [CascadingParameter]
        private EditContext CurrentEditContext { get; set; }
        protected override void OnInitialized()
        {
            messageStore = new ValidationMessageStore(CurrentEditContext);

            CurrentEditContext.OnValidationRequested += ValidateRequested;
            CurrentEditContext.OnFieldChanged += ValidateField;
        }
        protected void HandleValidation(FieldIdentifier identifier)
        {

            if (identifier.FieldName.Equals("Name"))
            {
                messageStore.Clear(identifier);
                if ((context.Data as BookDto).Name == null || (context.Data as BookDto).Name == "")
                {
                    messageStore.Add(identifier, _localizer["Name Field cannot be Empty"]);
                }
                else if (_bookAppService.IsNameUnique((context.Data as BookDto).Name, (context.Data as BookDto).Id).Result)
                {
                    messageStore.Add(identifier, _localizer["Name Field must be unique"]);
                }
                else
                {
                    messageStore.Clear(identifier);
                }
                if((context.Data as BookDto).ReleaseDate == default)
                {
                    messageStore.Add(identifier,"Date Field cannot be Empty");
                }
                if ((context.Data as BookDto).BookType == null)
                {
                    messageStore.Add(identifier,_localizer["Book Type Field cannot be Empty"]);
                }
                if((context.Data as BookDto).BookType == 1)
                {
                    if((context.Data as BookDto).Translator == null || (context.Data as BookDto).Translator =="")
                    {   
                        messageStore.Add(identifier,_localizer["Translator Field cannot be Empty"]);
                    }
                }
            }
        }

        protected void ValidateField(object editContext, FieldChangedEventArgs fieldChangedEventArgs)
        {
            HandleValidation(fieldChangedEventArgs.FieldIdentifier);
        }

        private void ValidateRequested(object editContext, ValidationRequestedEventArgs validationEventArgs)
        {
            HandleValidation(CurrentEditContext.Field("Name"));
            HandleValidation(CurrentEditContext.Field("ReleaseDate"));
            HandleValidation(CurrentEditContext.Field("BookType"));
        }
    }
}
