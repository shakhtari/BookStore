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
using BookStore.MimicProfiles;
using BookStore.MimicDiagrams;
using BookStore.Formulas;
using Syncfusion.Blazor.Grids.Internal;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using BookStore.Enums;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace BookStore.Blazor.Pages.Formulas
{
    public partial class Formulas
    {
        [Inject]
        private IFormulaAppService FormulaAppService { get; set; }
        private IReadOnlyList<FormulaDto> FormulaList { get; set; }
        private CreateUpdateFormulaDto NewFormulaDto { get; set; }
        private CreateUpdateFormulaDto EditingFormulaDto { get; set; }
        private int EditingFormulaId { get; set; }
        private int DeletingFormulaId { get; set; }
        private bool Check = false;
        private bool IsVisible { get; set; } = false;
        private DialogSettings DialogParams = new DialogSettings { Height = "900px", Width = "850px" };
        SfGrid<FormulaDto> Grid;
        private bool Loading { get; set; }
        public string[] EnumValues = Enum.GetNames(typeof(FormulaType));
        
        public Formulas()
        {
            FormulaList = new List<FormulaDto>();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetFormulasAsync();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task GetFormulasAsync()
        {
            try
            {
                Loading = true;

                await InvokeAsync(() => StateHasChanged());

                FormulaList = await FormulaAppService.GetListAsync();
            }
            finally
            {
                Loading = false;

                await InvokeAsync(() => StateHasChanged());
            }
        }
        public async void ActionBeginHandler(ActionEventArgs<FormulaDto> args)
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
                    EditingFormulaId = args.RowData.Id;
                    EditingFormulaDto = new CreateUpdateFormulaDto()
                    {
                        Id = args.RowData.Id,
                        Active= true,
                        FormulaName= args.RowData.FormulaName,
                        FormulaType = args.RowData.FormulaType,
                        FormulaMultiplier=args.RowData.FormulaMultiplier,
                        FormulaInputMaximum=args.RowData.FormulaInputMaximum,
                        FormulaInputMinimum=args.RowData.FormulaInputMinimum,
                        FormulaOutputMaximim= args.RowData.FormulaOutputMaximim,
                        FormulaOutputMinimum=args.RowData.FormulaOutputMinimum
                    };
                }
            }
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
            {
                if (args.Action.Equals("Add"))
                {
                    NewFormulaDto = new CreateUpdateFormulaDto();
                    NewFormulaDto.Id = args.Data.Id;
                    NewFormulaDto.Active = args.Data.Active;
                    NewFormulaDto.FormulaName = args.Data.FormulaName;
                    NewFormulaDto.FormulaType= args.Data.FormulaType;
                    NewFormulaDto.FormulaMultiplier= args.Data.FormulaMultiplier;
                    NewFormulaDto.FormulaInputMaximum= args.Data.FormulaInputMaximum;
                    NewFormulaDto.FormulaInputMinimum= args.Data.FormulaInputMinimum;
                    NewFormulaDto.FormulaOutputMinimum= args.Data.FormulaOutputMinimum;
                    NewFormulaDto.FormulaOutputMaximim = args.Data.FormulaOutputMaximim;

                    await FormulaAppService.CreateAsync(NewFormulaDto);
                    
                    FormulaList = await FormulaAppService.GetListAsync();
                    await Grid.Refresh();
                }
                if (args.Action.Equals("Edit"))
                {
                    EditingFormulaDto.Id = args.Data.Id;
                    EditingFormulaDto.FormulaName = args.Data.FormulaName;
                    EditingFormulaDto.Active = args.Data.Active;
                    EditingFormulaDto.FormulaType = args.Data.FormulaType;
                    EditingFormulaDto.FormulaMultiplier = args.Data.FormulaMultiplier;
                    EditingFormulaDto.FormulaOutputMinimum = args.Data.FormulaOutputMinimum;
                    EditingFormulaDto.FormulaOutputMaximim = args.Data.FormulaOutputMaximim;
                    EditingFormulaDto.FormulaInputMaximum = args.Data.FormulaInputMaximum;
                    EditingFormulaDto.FormulaInputMinimum = args.Data.FormulaInputMinimum;

                    await FormulaAppService.UpdateAsync(EditingFormulaId, EditingFormulaDto);
                    FormulaList = await FormulaAppService.GetListAsync();
                    await Grid.Refresh();
                }
            }
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Delete))
            {
                this.IsVisible = true;
                DeletingFormulaId = args.Data.Id;
            }
        }
        public string GetHeader(FormulaDto value)
        {
            if (value.FormulaName == null)
            {
                return Ml["Insert New Formula"];
            }
            else
            {
                return Ml["Edit "] + value.FormulaName;
            }
        }
        private void CancelClick()
        {
            GetFormulasAsync();
            this.IsVisible = false;
        }
        private void OkClick()
        {
            FormulaAppService.DeleteAsync(DeletingFormulaId);
            this.IsVisible = false;
            //Grid.Refresh();
        }
    }
}