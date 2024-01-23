using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.MimicDiagrams
{
    public class CreateUpdateMimicDiagramDto
    {
        public string MimicDiagramName { get; set; }
        public bool MimicDiagramAuthorization { get; set; }
    }
}
