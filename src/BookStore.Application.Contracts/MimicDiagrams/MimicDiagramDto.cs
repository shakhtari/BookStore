using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace BookStore.MimicDiagrams
{
    public class MimicDiagramDto : FullAuditedEntityDto<int>, IHasConcurrencyStamp
    {
        public bool Active { get; set; }
        public string MimicDiagramName { get; set; }
        public string? MimicDiagramDescription { get; set; }
        public string? MimicDiagramXML { get; set; }
        public bool? MimicDiagramAuthorization { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}