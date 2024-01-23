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

namespace BookStore.Blazor.Pages.MimicProfiles
{

    public partial class MimicProfiles
    {
        [Inject]
        private IMimicProfileAppService MimicProfileAppService { get; set; }
        [Inject]
        private IMimicDiagramAppService MimicDiagramAppService { get; set; }

        private IReadOnlyList<MimicProfileDto> MimicProfileList { get; set; }
        private IReadOnlyList<MimicDiagramDto> MimicDiagramList { get; set; }
        private string[] MimicDiagramNames { get; set; }
        private bool Loading { get; set; }
        private CreateUpdateMimicProfileDto NewMimicProfileDto { get; set; }
        private CreateUpdateMimicProfileDto EditingMimicProfileDto { get; set; }
        private Guid EditingMimicProfileId { get; set; }
        private Guid DeletingMimicProfileId { get; set; }
        private bool Check = false;
        private bool IsVisible { get; set; } = false;
        private DialogSettings DialogParams = new DialogSettings { Height = "700px", Width = "850px" };
        SfGrid<MimicProfileDto> Grid { get; set; }
        public MimicProfiles()
        {
            MimicProfileList = new List<MimicProfileDto>();
            MimicDiagramList = new List<MimicDiagramDto>();
            MimicDiagramNames = new string[] { };
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetMimicProfilesAsync();
                 GetMimicDiagramsAsync();
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

        private async void GetMimicDiagramsAsync()
        {
                MimicDiagramList = await MimicDiagramAppService.GetListAsync();
        }

        public async void ActionBeginHandler(ActionEventArgs<MimicProfileDto> args)
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
                    EditingMimicProfileId = args.RowData.Id;
                    MimicDiagramNames = args.RowData.MimicProfileDetail.Split(" | ");
                    EditingMimicProfileDto = new CreateUpdateMimicProfileDto()
                    {
                        Id = args.RowData.Id,
                        MimicProfileName = args.RowData.MimicProfileName,
                        MimicProfileDetail = MimicDiagramNames.JoinAsString(",")
                    };
                }
            }
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
            {
                if (Check)
                {
                    NewMimicProfileDto = new CreateUpdateMimicProfileDto();
                    NewMimicProfileDto.Id = args.Data.Id;
                    NewMimicProfileDto.MimicProfileName = args.Data.MimicProfileName;
                    MimicDiagramNames= args.Data.MimicProfileDetail.Split(",");
                    NewMimicProfileDto.MimicProfileDetail = MimicDiagramNames.JoinAsString(" | ");

                    MimicProfileAppService.CreateAsync(NewMimicProfileDto);
                }
                if (!Check)
                {
                    EditingMimicProfileDto.Id = args.Data.Id;
                    EditingMimicProfileDto.MimicProfileName = args.Data.MimicProfileName;
                    MimicDiagramNames = args.Data.MimicProfileDetail.Split(",");
                    EditingMimicProfileDto.MimicProfileDetail = MimicDiagramNames.JoinAsString(" | ");

                    MimicProfileAppService.UpdateAsync(EditingMimicProfileId, EditingMimicProfileDto);
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

        }
        public string GetHeader(MimicProfileDto value)
        {
            if (value.MimicProfileName == null)
            {
                return Ml["Insert New Profile"];
            }
            else
            {
                return Ml["Edit "] + value.MimicProfileName;
            }
        }
    }
}