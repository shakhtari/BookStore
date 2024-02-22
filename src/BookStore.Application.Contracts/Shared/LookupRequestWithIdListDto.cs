using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BookStore.Shared
{
        public class LookupRequestWithIdListDto : PagedResultRequestDto
        {
        public string Filter { get; set; }
        public List<int> Ids { get; set; }

        public LookupRequestWithIdListDto()
            {
                MaxResultCount = MaxMaxResultCount;
            }
        
    }
}
