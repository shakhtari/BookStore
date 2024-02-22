using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Shared
{
	public interface IHasReadDateTime
	{
		public DateTime? ReadDateTime { get; set; }
	}
}
