using System.Collections.Generic;

namespace BookStore.Shared
{
    public class ExcelExportRequestDto<T>
    {
        public List<T> Data { get; set; }

        public List<string> FilterParameter { get; set; }
    }
}