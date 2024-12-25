# DotVVM Bootstrap Daterangepicker

This is a [DotVVM](https://www.dotvvm.com/) control that wraps the [Bootstrap Daterangepicker](https://www.daterangepicker.com/) jQuery plugin.

## Installation

You can install the control from [NuGet](https://www.nuget.org/packages/DotVVM.Bootstrap.Daterangepicker/):
```
Install-Package DotVVM.Bootstrap.Daterangepicker
```

or using the `dotnet` command line:
```
dotnet add package DotVVM.Bootstrap.Daterangepicker
```

## Usage

First, you need to include registration of the control in your `DotvvmStartup.cs` file:
```csharp
private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
{
    config.AddDateRangePicker();
}
```

Then, you can use the `DateRangePicker` control in your DotHTML file:
```html
<cc:DateRangePicker Value="{value: DateRange}"
                    Ranges="{resource: Ranges}"
                    ApplyLabel="OK"
                    CancelLabel="Storno"
                    CustomRangeLabel="Vlastní"
                    class="input-group">
    <div class="input-group-prepend">
        <span class="input-group-text"><span class="fas fa-calendar"></span></span>
    </div>
    <span class="form-control" Visible="{value: DateRange.HasValue && !DateRange.HasSelectedRange}">
        <dot:Literal Text="{value: DateRange.Start}" FormatString="d. M. yyyy">
        </dot:Literal>
        <span Visible="{value: !DateRange.IsOneDay}">
            <span class="separator">-</span>
            <dot:Literal Text="{value: DateRange.End}" FormatString="d. M. yyyy">
            </dot:Literal>
        </span>
    </span>
    <span class="form-control" Visible="{value: DateRange.HasValue && DateRange.HasSelectedRange}">
        <dot:Literal Text="{value: DateRange.SelectedRange.Label}">
        </dot:Literal>
    </span>
    <div class="input-group-append" Visible="{value: DateRange.HasValue}">
        <dot:Button Click="{command: ClearDateRange()}"
                    ButtonTagName="button"
                    class="input-group-text">
            <span class="fas fa-times"></span>
        </dot:Button>
    </div>
    <span class="form-control text-muted" IncludeInPage="{value: !DateRange.HasValue}">
        Choose date...
    </span>
</cc:DateRangePicker>
```

The `DateRange` and `Ranges` properties should be defined in your viewmodel:
```csharp
public DateRangeValue DateRange { get; set; } = new();

public List<DateRange> Ranges { get; set; } = new List<DateRange>()
{
	new("today", "Today", DateTime.Today),
	new("yesterday", "Yesterday", DateTime.Today.AddDays(-1)),
	new("last7days", "Last 7 days", DateTime.Today.AddDays(-7)),
	new("last30days", "Last 30 days", DateTime.Today.AddDays(-30)),
	new("thismonth", "This month", new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), DateTime.Today),
	new("lastmonth", "Last month", new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1), new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1))
};
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Credits

www.dotvvm.com

www.daterangepicker.com