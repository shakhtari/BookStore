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
using BookStore.MimicProfiles;
using BookStore.MimicDiagrams;
using Syncfusion.Blazor.Grids.Internal;
using System.IO;
using System.Xml.Serialization;

namespace BookStore.Blazor.Pages.MimicProfiles
{

    public partial class MimicProfiles
    {
        [Inject]
        private IMimicProfileAppService MimicProfileAppService { get; set; }
        [Inject]
        private IMimicDiagramsAppService MimicDiagramAppService { get; set; }

        private IReadOnlyList<MimicProfileDto> MimicProfileList { get; set; }
        private IReadOnlyList<MimicDiagramDto> MimicDiagramList { get; set; }
        private List<MimicDiagramDto> mimicDiagramDtos { get; set; }
        public string[] MimicDiagramNames { get; set; }
        private List<int> MimicDiagramIds { get; set; }
        private int[] MimicDiagramResult { get; set; }
        private bool Loading { get; set; }
        private CreateUpdateMimicProfileDto NewMimicProfileDto { get; set; }
        private CreateUpdateMimicProfileDto EditingMimicProfileDto { get; set; }
        private int EditingMimicProfileId { get; set; }
        private int DeletingMimicProfileId { get; set; }
        private bool Check = false;
        private bool IsVisible { get; set; } = false;
        private DialogSettings DialogParams = new DialogSettings { Height = "700px", Width = "850px" };
        SfGrid<MimicProfileDto> Grid;
        private MimicDiagramDto MimicDiagramDto { get; set; }
        public MimicProfiles()
        {
            MimicProfileList = new List<MimicProfileDto>();
            MimicDiagramList = new List<MimicDiagramDto>();
            MimicDiagramNames = new string[] { };
            MimicDiagramIds = new List<int> { };
            MimicDiagramResult = new int[] { };
            mimicDiagramDtos = new List<MimicDiagramDto>();
            MimicDiagramDto = new MimicDiagramDto();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetMimicProfilesAsync();
                await GetMimicDiagramsAsync();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task GetMimicProfilesAsync()
        {
            try
            {
                Loading = true;

                await InvokeAsync(() => StateHasChanged());

                MimicProfileList = await MimicProfileAppService.GetListAsync();
            }
            finally
            {
                Loading = false;

                await InvokeAsync(() => StateHasChanged());
            }
        }

        private async Task GetMimicDiagramsAsync()
        {
            MimicDiagramList = await MimicDiagramAppService.GetAllAsync();
        }
        private List<MimicDiagramDto> DeserializeXmlToList(string xmlString)
        {
            var serializer = new XmlSerializer(typeof(List<MimicDiagramDto>));

            using (var reader = new StringReader(xmlString))
            {
                return (List<MimicDiagramDto>)serializer.Deserialize(reader);
            }
        }

        public async void ActionBeginHandler(ActionEventArgs<MimicProfileDto> args)
        {
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Add) || args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.BeginEdit))
            {

                //Handles Add Operation
                if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Add))
                {
                    Check = true;
                    MimicDiagramResult = null;
                }

                //Handles Edit Operation
                if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.BeginEdit))
                {
                    EditingMimicProfileId = args.RowData.Id;
                    MimicDiagramNames = args.RowData.MimicProfileDetail.Split(" | ");
                    foreach (var item in MimicDiagramNames)
                    {
                        var temp = int.TryParse(item, out int parsedValue);
                        MimicDiagramIds.AddFirst(parsedValue);
                    }
                    MimicDiagramResult = MimicDiagramIds.ToArray();
                    EditingMimicProfileDto = new CreateUpdateMimicProfileDto()
                    {
                        Id = args.RowData.Id,
                        MimicProfileName = args.RowData.MimicProfileName,
                        MimicProfileDetail = MimicDiagramResult.JoinAsString(" | ")
                    };
                }
            }
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
            {
                if (args.Action.Equals("Add"))
                {
                    NewMimicProfileDto = new CreateUpdateMimicProfileDto();
                    NewMimicProfileDto.Id = args.Data.Id;
                    NewMimicProfileDto.MimicProfileName = args.Data.MimicProfileName;
                    NewMimicProfileDto.MimicProfileDetail = MimicDiagramResult.JoinAsString(" | ");

                    await MimicProfileAppService.CreateAsync(NewMimicProfileDto);

                    MimicProfileList = await MimicProfileAppService.GetListAsync();
                    await Grid.Refresh();
                }
                if (args.Action.Equals("Edit"))
                {
                    EditingMimicProfileDto.Id = args.Data.Id;
                    EditingMimicProfileDto.MimicProfileName = args.Data.MimicProfileName;
                    EditingMimicProfileDto.MimicProfileDetail = MimicDiagramResult.JoinAsString(" | ");

                    await MimicProfileAppService.UpdateAsync(EditingMimicProfileId, EditingMimicProfileDto);
                    MimicProfileList = await MimicProfileAppService.GetListAsync();
                    await Grid.Refresh();
                }
            }
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Delete))
            {
                this.IsVisible = true;
                DeletingMimicProfileId = args.Data.Id;
            }
        }

        private void CancelClick()
        {
            GetMimicProfilesAsync();
            this.IsVisible = false;
        }
        private void OkClick()
        {
            MimicProfileAppService.DeleteAsync(DeletingMimicProfileId);
            this.IsVisible = false;
            Grid.Refresh();
        }
        public string GetHeader(MimicProfileDto value)
        {
            if (value.MimicProfileName == null)
            {
                return L["Insert New Profile"];
            }
            else
            {
                return L["Edit "] + value.MimicProfileName;
            }
        }


    }
}