using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BookStore.MimicDiagrams
{
    public class MimicDiagramDto: EntityDto<int>
    {
        public string MimicDiagramName { get; set; }
        public bool MimicDiagramAuthorization { get; set; }
    }
}
