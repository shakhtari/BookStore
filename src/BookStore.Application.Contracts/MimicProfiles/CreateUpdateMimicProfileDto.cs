using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.MimicProfiles
{
    public class CreateUpdateMimicProfileDto
    {
        public int Id { get; set; }
        public Guid TenantId { get; set; }
        public string MimicProfileName { get; set; }
        public string MimicProfileDetail { get; set; }
    }
}
