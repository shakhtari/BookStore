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
using BookStore.DerivedValues;
using Syncfusion.Blazor.Grids.Internal;
using System.IO;
using System.Xml.Serialization;
using BookStore.MimicProfiles;
using BookStore.Enums;
using BookStore.DerivedValueDetails;
using System.ComponentModel;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using BookStore.Shared;

namespace BookStore.Blazor.Pages.DerivedValues
{

    public partial class DerivedValues
    {
        [Inject]
        private IDerivedValuesAppService DerivedValuesAppService { get; set; }
        [Inject]
        private IDerivedValueDetailsAppService DerivedValueDetailsAppService { get; set; }
        private IReadOnlyList<DerivedValueDto> DerivedValuesList { get; set; }
        private IReadOnlyList<DerivedValueDetailDto> DerivedValueDetailsList { get; set; }
        private DerivedValueCreateDto NewDerivedValueDto { get; set; }
        private DerivedValueUpdateDto EditingDerivedValueDto { get; set; }
        private DerivedValueDetailCreateDto NewDerivedValueDetailDto { get; set; }
        private int EditingDerivedValueId { get; set; }
        private int DeletingDerivedValueId { get; set; }
        private int DeletingDerivedValueDetailId { get; set; }
        private bool Loading { get; set; }
        SfGrid<DerivedValueDto> Grid;
        SfGrid<DerivedValueDetailDto> NestedGrid;
        private DialogSettings DialogParams = new DialogSettings { Height = "900px", Width = "950px" };
        private bool Check = false;
        private bool IsVisible { get; set; } = false;
        public string[] ThresholdTimeUnitEnumValues = Enum.GetNames(typeof(ThresholdTimeUnitTypes));
        public string[] ComminicationTypeEnumValues = Enum.GetNames(typeof(ComminicationType));
        //public string[] OperatorTypeEnumValues = Enum.GetNames(typeof(OperatorTypes));
        public string[] AlphabetItemsEnumValues = Enum.GetNames(typeof(AlphabetItem));
        public string[] DVItemNrValues { get; set; }
        public ThresholdTimeUnitTypes ThresholdTimeUnitSelected { get; set; }
        public ComminicationType RecordValueSelected { get; set; }
        public AlphabetItem selectedItem { get; set; }
        public AlphabetItem selectedDVItemNr { get; set; }
        public string SelectedOperator { get; set; }
        public string DVDeviceResult { get; set; }
        public string ConstValue = "1,00000000";
        public int dvId { get; set; }
        private IReadOnlyList<LookupDto<int>> DVDeviceLookup { get; set; } = new List<LookupDto<int>>();
        public DerivedValues()
        {
            DerivedValuesList = new List<DerivedValueDto>();
            DerivedValueDetailsList = new List<DerivedValueDetailDto>();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetDerivedValuesAsync();
            }

            await base.OnAfterRenderAsync(firstRender);
        }
        private async Task GetDerivedValuesAsync()
        {
            try
            {
                Loading = true;

                await InvokeAsync(() => StateHasChanged());

                DerivedValuesList = await DerivedValuesAppService.GetAllAsync();
                
            }
            finally
            {
                Loading = false;

                await InvokeAsync(() => StateHasChanged());
            }
        }

        public async void ActionBeginHandler(ActionEventArgs<DerivedValueDto> args)
        {
            //SelectedOperator = Enums.OperatorTypes.Subtraction;
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Add) || args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.BeginEdit))
            {

                //Handles Add Operation
                if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Add))
                {
                    Check = true;
                    NewDerivedValueDetailDto = new DerivedValueDetailCreateDto();
                    dvId = args.RowData.Id;
                }

                //Handles Edit Operation
                if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.BeginEdit))
                {
                    await GetDerivedValueDetailsList(args.RowData.Id);
                    EditingDerivedValueId = args.RowData.Id;
                    ThresholdTimeUnitSelected = (ThresholdTimeUnitTypes)args.RowData.DVThresholdTimeUnit;
                    RecordValueSelected = (ComminicationType)args.RowData.DVRecordValue;
                    EditingDerivedValueDto = new DerivedValueUpdateDto()
                    {
                        Id = args.RowData.Id,
                        Active = args.RowData.Active,
                        DVDescription = args.RowData.DVDescription,
                        DVThresholdTime = args.RowData.DVThresholdTime,
                        DVThresholdTimeUnit = (int)ThresholdTimeUnitSelected,
                        DVRecordValue = (int)RecordValueSelected,
                        DVFormula = args.RowData.DVFormula,
                    };
                }
            }
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
            {
                if (args.Action.Equals("Add"))
                {
                    NewDerivedValueDto = new DerivedValueCreateDto();
                    NewDerivedValueDto.Id = args.Data.Id;
                    NewDerivedValueDto.Active = args.Data.Active;
                    NewDerivedValueDto.DVDescription = args.Data.DVDescription;
                    NewDerivedValueDto.DVThresholdTime = args.Data.DVThresholdTime;
                    NewDerivedValueDto.DVThresholdTimeUnit = (int)ThresholdTimeUnitSelected;
                    NewDerivedValueDto.DVRecordValue = (int)RecordValueSelected;
                    NewDerivedValueDto.DVFormula = args.Data.DVFormula;

                    await DerivedValuesAppService.CreateAsync(NewDerivedValueDto);

                    DerivedValuesList = await DerivedValuesAppService.GetAllAsync();
                    await Grid.Refresh();
                }
                if (args.Action.Equals("Edit"))
                {
                    EditingDerivedValueDto.Id = args.Data.Id;
                    EditingDerivedValueDto.Active = args.Data.Active;
                    EditingDerivedValueDto.DVDescription = args.Data.DVDescription;
                    EditingDerivedValueDto.DVThresholdTime = args.Data.DVThresholdTime;
                    EditingDerivedValueDto.DVThresholdTimeUnit = (int)ThresholdTimeUnitSelected;
                    EditingDerivedValueDto.DVRecordValue = (int)RecordValueSelected;
                    EditingDerivedValueDto.DVFormula = args.Data.DVFormula;

                    await DerivedValuesAppService.UpdateAsync(EditingDerivedValueId, EditingDerivedValueDto);
                    DerivedValuesList = await DerivedValuesAppService.GetAllAsync();
                    await Grid.Refresh();
                }
            }
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Delete))
            {
                this.IsVisible = true;
                DeletingDerivedValueId = args.Data.Id;
            }
        }

        private async Task CancelClick()
        {
            await GetDerivedValuesAsync();
            this.IsVisible = false;
        }
        private async Task OkClick()
        {
            DerivedValuesAppService.DeleteAsync(DeletingDerivedValueId);
            this.IsVisible = false;
            Grid.Refresh();
        }

        private async void AddItemToFormula(DerivedValueDto input)
        {
            input.DVFormula = (input.DVFormula) + (Enum.GetName(selectedItem));
        }
        private async void AddOperatorToFormula(DerivedValueDto input)
        {
            input.DVFormula = (input.DVFormula) + (SelectedOperator);
        }

        public class Operator
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }

        List<Operator> Operators = new List<Operator>
        {
        new Operator() { Name = "-", Code = "1" },
        new Operator() { Name = "*", Code = "2" },
        new Operator() { Name = "/", Code = "3" },
        new Operator() { Name = "(", Code = "4" },
        new Operator() { Name = ")", Code = "5" },
        };

        public async Task GetDerivedValueDetailsList(int? input)
        {
            DerivedValueDetailsList = await DerivedValueDetailsAppService.GetDetailsList(input);
        }

        public async void OnActionBeginDerivedValueDetailGrid(ActionEventArgs<DerivedValueDetailDto> args)
        {
            
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
            {
                if (args.Action.Equals("Add"))
                {
                    NewDerivedValueDetailDto = new DerivedValueDetailCreateDto();
                    NewDerivedValueDetailDto.Id = args.Data.Id;
                    NewDerivedValueDetailDto.DVId = dvId;
                    NewDerivedValueDetailDto.DVDeviceId = args.Data.DVDeviceId;
                    NewDerivedValueDetailDto.DVLabelId = args.Data.DVLabelId;
                    NewDerivedValueDetailDto.DVItemNr = (int)selectedItem;
                    NewDerivedValueDetailDto.DVMathOperator = Convert.ToInt32(SelectedOperator);
                    NewDerivedValueDetailDto.DVConstantValue = 1;

                    await DerivedValueDetailsAppService.CreateAsync(NewDerivedValueDetailDto);

                    await GetDerivedValueDetailsList(NewDerivedValueDetailDto.Id);
                    await NestedGrid.Refresh();
                }
            }
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Delete))
            {
                await DerivedValueDetailsAppService.DeleteAsync(args.Data.Id);
            }
        }
    }
}