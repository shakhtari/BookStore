using Volo.Abp.Application.Dtos;
using System;

namespace BookStore.DerivedValueDetails
{
    public class DerivedValueDetailExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public int? DVIdMin { get; set; }
        public int? DVIdMax { get; set; }
        public int? DVDeviceIdMin { get; set; }
        public int? DVDeviceIdMax { get; set; }
        public int? DVLabelIdMin { get; set; }
        public int? DVLabelIdMax { get; set; }
        public int? DVItemNrMin { get; set; }
        public int? DVItemNrMax { get; set; }
        public int? DVMathOperatorMin { get; set; }
        public int? DVMathOperatorMax { get; set; }
        public decimal? DVConstantValueMin { get; set; }
        public decimal? DVConstantValueMax { get; set; }

        public DerivedValueDetailExcelDownloadDto()
        {

        }
    }
}