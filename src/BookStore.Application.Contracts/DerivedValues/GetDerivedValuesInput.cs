using Volo.Abp.Application.Dtos;
using System;

namespace BookStore.DerivedValues
{
    public class GetDerivedValuesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public bool? Active { get; set; }
        public string DVDescription { get; set; }
        public int? DVRecordValueMin { get; set; }
        public int? DVRecordValueMax { get; set; }
        public int? DVThresholdTimeMin { get; set; }
        public int? DVThresholdTimeMax { get; set; }
        public int? DVThresholdTimeUnitMin { get; set; }
        public int? DVThresholdTimeUnitMax { get; set; }
        public string DVFormula { get; set; }

        public GetDerivedValuesInput()
        {

        }
    }
}