using System;

namespace DotVVM.BootstrapDateRangePicker.Models
{
	public class DateRange
	{
		public DateRange(string key, string label, DateTime date, int days = 0)
		{
			Key = key;
			Label = label;
			Start = date;
			End = date.Date.AddDays(days)
				.AddDays(1)
				.AddSeconds(-1);
		}

		public DateRange(string key, string label, DateTime startDate, DateTime endDate)
		{
			Key = key;
			Label = label;
			Start = startDate;
			End = endDate;
		}

		public string Key { get; set; }
		public string Label { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
	}
}
