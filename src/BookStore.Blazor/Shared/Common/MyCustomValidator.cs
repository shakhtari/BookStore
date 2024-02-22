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
using BookStore.MimicProfiles;
using System.Runtime.Serialization;
using AutoMapper.Internal.Mappers;
using BookStore.Shared;


namespace BookStore.Blazor.Shared.Common
{
    public class MyCustomValidator : ComponentBase
    {
        private readonly IBookAppService _bookAppService;
        private readonly IMimicProfileAppService _mimicProfileAppService;
        private readonly IStringLocalizer<BookStoreResource> _localizer;
        public MyCustomValidator(IStringLocalizer<BookStoreResource> localizer ,IBookAppService bookAppService,
            IMimicProfileAppService mimicProfileAppService)
        {
            _localizer = localizer;
            _bookAppService = bookAppService;
            _mimicProfileAppService = mimicProfileAppService;
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
        protected async void HandleValidation(FieldIdentifier identifier)
        {
            var lookupData = new LookupRequestWithIdDto
            {
                Filter = (context.Data as MimicProfileDto).MimicProfileName,
                Id = (context.Data as MimicProfileDto).Id
            };
            if(identifier.FieldName.Equals("MimicProfileName"))
            {
                messageStore.Clear(identifier);
                if ((context.Data as MimicProfileDto).MimicProfileName == null || (context.Data as MimicProfileDto).MimicProfileName == "")
                {
                    messageStore.Add(identifier, _localizer["Name Field cannot be Empty"]);
                }
                //else if (_mimicProfileAppService.IsNameUnique((context.Data as MimicProfileDto).MimicProfileName, (context.Data as MimicProfileDto).Id).Result)
                else if(await _mimicProfileAppService.GetMimicProfileLookupAsync(lookupData))
                {
                    messageStore.Add(identifier, _localizer["Name Field must be unique"]);
                }
            }
        }

        protected void ValidateField(object editContext, FieldChangedEventArgs fieldChangedEventArgs)
        {
            HandleValidation(fieldChangedEventArgs.FieldIdentifier);
        }

        private void ValidateRequested(object editContext, ValidationRequestedEventArgs validationEventArgs)
        {
            
            HandleValidation(CurrentEditContext.Field("MimicProfileName"));
        }
    }
}
