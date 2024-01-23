using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BookStore.MimicProfiles
{
    public class MimicProfileDto: AuditedEntityDto<Guid>
    {
        public Guid TenantId { get; set; }
        public string MimicProfileName { get; set; }
        public string MimicProfileDetail { get; set; }
    }
}
