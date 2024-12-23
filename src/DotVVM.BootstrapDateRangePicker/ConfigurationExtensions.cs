using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using System.Collections.Generic;

namespace DotVVM.BootstrapDateRangePicker
{
	public static class ConfigurationExtensions
	{
		public static void AddDateRangePicker(this DotvvmConfiguration config, DateRangePickerOptions options = null)
		{
			options = options ?? new DateRangePickerOptions();

			var assembly = typeof(DateRangePicker).Assembly;
			string ns = typeof(DateRangePicker).Namespace;

			config.Markup.Controls.Add(new DotvvmControlConfiguration()
			{
				Assembly = assembly.GetName().Name,
				Namespace = ns,
				TagPrefix = options.TagPrefix
			});

			var dependencies = new List<string> { "dotvvm", options.BootstrapResourceName, options.MomentJSResourceName, options.DateRangePickerResourceName };

			if (options.IncludeDefaultStyles)
			{
				const string name = "DotVVM.BootstrapDateRangePicker.css";

				dependencies.Add(name);
				config.Resources.RegisterStylesheet(
					name: name,
					location: new EmbeddedResourceLocation(assembly, ns + ".Styles.DotVVM.BootstrapDateRangePicker.css")
				);
			}

			// register additional resources for the control and set up dependencies
			config.Resources.Register("DotVVM.BootstrapDateRangePicker", new ScriptResource()
			{
				Location = new EmbeddedResourceLocation(assembly, ns + ".Scripts.DotVVM.BootstrapDateRangePicker.js"),
				Dependencies = dependencies.ToArray()
			});

			if (options.IncludeMomentJSResource)
			{
				config.Resources.RegisterScript(
					name: options.MomentJSResourceName + "-lib",
					location: new EmbeddedResourceLocation(assembly, ns + ".Scripts.moment-with-locales.min.js"));

				config.Resources.Register(options.MomentJSResourceName, new InlineScriptResource("dotvvm.events.init.subscribe(function () {moment.locale(document.documentElement.lang || 'en'); });", defer: true)
				{
					Dependencies = new[] { "dotvvm", options.MomentJSResourceName + "-lib" }
				});
			}

			if (options.IncludeDateRangePickerResource)
			{
				config.Resources.RegisterScript(
					name: options.DateRangePickerResourceName,
					location: new EmbeddedResourceLocation(assembly, ns + ".Scripts.daterangepicker.min.js"),
					dependencies: new[] { options.MomentJSResourceName }
				);
			}
		}
	}

	public class DateRangePickerOptions
	{
		/// <summary>
		/// Gets or sets the tag prefix that will be used for the control. Default is "cc".
		/// </summary>
		public string TagPrefix { get; set; } = "cc";

		/// <summary>
		/// Gets or sets the name of the resource that contains the Bootstrap. Default is "bootstrap".
		/// </summary>
		public string BootstrapResourceName { get; set; } = "bootstrap";

		/// <summary>
		/// Gets or sets the name of the resource that contains the DateRangePicker. Default is "daterangepicker".
		/// </summary>
		public string DateRangePickerResourceName { get; set; } = "daterangepicker";

		/// <summary>
		/// Gets or sets a value indicating whether the control should include the DateRangePicker resource. Default is true.
		/// </summary>
		public bool IncludeDateRangePickerResource { get; set; } = true;

		/// <summary>
		/// Gets or sets the name of the resource that contains the moment.js. Default is "momentjs".
		/// </summary>
		public string MomentJSResourceName { get; set; } = "momentjs";

		/// <summary>
		/// Gets or sets a value indicating whether the control should include the moment.js resource. Default is true.
		/// </summary>
		public bool IncludeMomentJSResource { get; set; } = true;

		/// <summary>
		/// Gets or sets a value indicating whether the control should include the default styles. Default is true.
		/// </summary>
		public bool IncludeDefaultStyles { get; set; } = true;
	}
}
