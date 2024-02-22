using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BookStore.Shared
{
        public class LookupRequestWithIdDto : PagedResultRequestDto
        {
            public string Filter { get; set; }
            public int? Id { get; set; }

            public LookupRequestWithIdDto()
            {
                MaxResultCount = MaxMaxResultCount;
            }
        
    }
}
