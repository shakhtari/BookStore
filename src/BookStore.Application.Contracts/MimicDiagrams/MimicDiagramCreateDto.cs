using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BookStore.MimicDiagrams
{
    public class MimicDiagramCreateDto
    {
        public bool Active { get; set; } = true;
        [Required]
        //[StringLength(MimicDiagramConsts.MimicDiagramNameMaxLength)]
        public string MimicDiagramName { get; set; }
        [Required]
        //[StringLength(MimicDiagramConsts.MimicDiagramDescriptionMaxLength)]
        public string? MimicDiagramDescription { get; set; }
        [Required]
        public string? MimicDiagramXML { get; set; }
        public bool MimicDiagramAuthorization { get; set; }
    }
}