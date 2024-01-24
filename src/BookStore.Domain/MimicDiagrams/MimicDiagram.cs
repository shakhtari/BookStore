using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace BookStore.MimicDiagrams
{
    public class MimicDiagram : Entity<string>
    {
        public string MimicDiagramName { get; set; }
        public bool MimicDiagramAuthorization { get; set; }

        public override object[] GetKeys()
        {
            throw new NotImplementedException();
        }
    }
}
