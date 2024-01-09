using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BookStore.Publications
{
    public class PublicationDto:EntityDto
    {
        public int PubId { get; set; }
        public string PubName { get; set; }
    }
}
