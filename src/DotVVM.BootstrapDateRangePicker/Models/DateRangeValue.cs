using System;

namespace DotVVM.BootstrapDateRangePicker.Models
{
	public class DateRangeValue
	{
		public DateTime? Start { get; set; }
		public DateTime? End { get; set; }

		public int? SelectedRangeIndex { get; set; }
		public DateRange SelectedRange { get; }

		public bool IsOneDay => Start?.Date == End?.Date;
		public bool HasValue => Start.HasValue;
		public bool HasSelectedRange => SelectedRangeIndex.HasValue;
	}
}
