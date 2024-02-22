using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace BookStore.MimicProfiles
{
    public class MimicProfile : FullAuditedAggregateRoot<int>
    {
        public Guid TenantId { get; set; }
        public string MimicProfileName { get; set; }
        public string MimicProfileDetail { get; set; }
    }
}
