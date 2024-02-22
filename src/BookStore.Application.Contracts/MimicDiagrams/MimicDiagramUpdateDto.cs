using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace BookStore.MimicDiagrams
{
    public class MimicDiagramUpdateDto : IHasConcurrencyStamp
    {
        public bool Active { get; set; }
        [Required]
        //[StringLength(MimicDiagramConsts.MimicDiagramNameMaxLength)]
        public string MimicDiagramName { get; set; }
        [Required]
        //[StringLength(MimicDiagramConsts.MimicDiagramDescriptionMaxLength)]
        public string MimicDiagramDescription { get; set; }
        [Required]
        public string MimicDiagramXML { get; set; }
        public bool MimicDiagramAuthorization { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}