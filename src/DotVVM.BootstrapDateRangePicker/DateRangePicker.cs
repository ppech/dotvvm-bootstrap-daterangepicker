using DotVVM.BootstrapDateRangePicker.Models;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using System.Collections.Generic;

namespace DotVVM.BootstrapDateRangePicker
{
	public class DateRangePicker : CompositeControl
	{
		protected override void OnInit(IDotvvmRequestContext context)
		{
			context.ResourceManager.AddCurrentCultureGlobalizationResource();
			context.ResourceManager.AddRequiredResource("DotVVM.BootstrapDateRangePicker");
			base.OnInit(context);
		}

		public DotvvmControl GetContents(
			IValueBinding<DateRangeValue> value,
			[MarkupOptions(Required = false)] IEnumerable<DateRange> ranges,
			[MarkupOptions(MappingMode = MappingMode.InnerElement, Required = false)] ITemplate contentTemplate,
			[MarkupOptions(AllowBinding = false, Required = false)] string formatString,
			[MarkupOptions(AllowBinding = false, Required = false)] string applyLabel,
			[MarkupOptions(AllowBinding = false, Required = false)] string cancelLabel,
			[MarkupOptions(AllowBinding = false, Required = false)] string customRangeLabel,
			HtmlCapability htmlCapability)
		{
			var div = new HtmlGenericControl("div", htmlCapability);

			var binding = new KnockoutBindingGroup();

			binding.AddValue("single", false);
			binding.Add("value", value.GetKnockoutBindingExpression(div));
			binding.AddValue("ranges", ranges);

			if (!string.IsNullOrEmpty(formatString))
				binding.AddValue("format", formatString);
			if(!string.IsNullOrEmpty(applyLabel))
				binding.AddValue("applyLabel", applyLabel);
			if (!string.IsNullOrEmpty(cancelLabel))
				binding.AddValue("cancelLabel", cancelLabel);
			if (!string.IsNullOrEmpty(customRangeLabel))
				binding.AddValue("customRangeLabel", customRangeLabel);

			div.AddAttribute("data-bind", "bootstrap-daterangepicker: " + binding.ToString());

			if (contentTemplate != null)
			{
				div.AppendChildren(new TemplateHost(contentTemplate));
			}

			return div;
		}
	}
}
