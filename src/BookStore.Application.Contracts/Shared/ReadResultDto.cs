using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BookStore.Shared
{

	[Serializable]
	public class ReadResultDto<T> : ListResultDto<T>, IListResult<T>, IHasReadDateTime
	{
		public DateTime? ReadDateTime { get; set; }
		public bool IsSuccess { get; set; }
		public string ErrorDescription { get; set; }
		public string ErrorNumber{ get; set; }


		public ReadResultDto()
		{

		}

		public ReadResultDto(DateTime readDateTime, IReadOnlyList<T> items)
				: base(items)
		{
			ReadDateTime = readDateTime;
		}


	}
}
