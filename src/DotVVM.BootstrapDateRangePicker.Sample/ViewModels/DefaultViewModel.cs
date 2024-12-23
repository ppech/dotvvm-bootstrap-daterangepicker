using DotVVM.BootstrapDateRangePicker.Models;
using DotVVM.Framework.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotVVM.BootstrapDateRangePicker.Sample.ViewModels
{
	public class DefaultViewModel : DotvvmViewModelBase
	{
		public DateRangeValue DateRange { get; set; } = new();

		//[Bind(Direction.None)]
		public List<DateRange> Ranges { get; set; } = new List<DateRange>()
			{
				new("today", "Dnes", DateTime.Today),
				new("yesterday", "Včera", DateTime.Today.AddDays(-1)),
				new("last7days", "Posledních 7 dní", DateTime.Today.AddDays(-7)),
				new("last30days", "Posledních 30 dní", DateTime.Today.AddDays(-30)),
				new("thismonth", "Tento měsíc", new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), DateTime.Today),
				new("lastmonth", "Předchozí měsíc", new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1), new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1))
			};

		public override async Task Init()
		{
			await base.Init();
		}

		public void ClearDateRange()
		{
			DateRange.Start = null;
			DateRange.End = null;
			DateRange.SelectedRangeIndex = null;
		}
	}
}
