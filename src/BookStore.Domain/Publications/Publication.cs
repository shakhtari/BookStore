using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace BookStore.Publications
{
    public class Publication:Entity
    {
        [Key]
        public int PubId { get; set; }
        public string PubName { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { PubId };
        }
    }
}
