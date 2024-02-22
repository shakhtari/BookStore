using Volo.Abp.Application.Dtos;
using System;

namespace WebNet.MimicDiagrams
{
    public class MimicDiagramExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public bool? Active { get; set; }
        public string? MimicDiagramName { get; set; }
        public string? MimicDiagramDescription { get; set; }
        public string? MimicDiagramXML { get; set; }
        public bool? MimicDiagramAuthorization { get; set; }

        public MimicDiagramExcelDownloadDto()
        {

        }
    }
}