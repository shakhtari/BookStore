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
using BookStore.Formulas;


namespace BookStore.Blazor.Shared.Common
{
    public class MyCustomValidatorFormula : ComponentBase
    {
        private readonly IFormulaAppService _formulaAppService;
        private readonly IStringLocalizer<BookStoreResource> _localizer;
        public MyCustomValidatorFormula(IStringLocalizer<BookStoreResource> localizer ,IFormulaAppService formulaAppService)
        {
            _localizer = localizer;
            _formulaAppService = formulaAppService;
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
            if(identifier.FieldName.Equals("FormulaName"))
            {
                messageStore.Clear(identifier);
                if ((context.Data as FormulaDto).FormulaName == null || (context.Data as FormulaDto).FormulaName == "")
                {
                    messageStore.Add(identifier, _localizer["Name Field cannot be Empty"]);
                }
                else if (_formulaAppService.IsNameUnique((context.Data as FormulaDto).FormulaName, (context.Data as FormulaDto).Id).Result)
                {
                    messageStore.Add(identifier, _localizer["Name Field must be unique"]);
                }
                if ((context.Data as FormulaDto).FormulaType ==0)
                {
                    messageStore.Add(identifier, _localizer["Formula Type Field cannot be Empty"]);
                }
            }
        }

        protected void ValidateField(object editContext, FieldChangedEventArgs fieldChangedEventArgs)
        {
            HandleValidation(fieldChangedEventArgs.FieldIdentifier);
        }

        private void ValidateRequested(object editContext, ValidationRequestedEventArgs validationEventArgs)
        {
            
            HandleValidation(CurrentEditContext.Field("FormulaName"));
        }
    }
}
