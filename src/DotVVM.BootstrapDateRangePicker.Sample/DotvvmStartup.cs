using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.BootstrapDateRangePicker.Sample
{
	public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
	{
		// For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
		public void Configure(DotvvmConfiguration config, string applicationPath)
		{
			ConfigureRoutes(config, applicationPath);
			ConfigureControls(config, applicationPath);
			ConfigureResources(config, applicationPath);
		}

		private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
		{
			config.RouteTable.Add("Default", "", "Views/Default.dothtml");
			config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));
		}

		private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
		{
			config.AddDateRangePicker();
		}

		private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
		{
			config.Resources.RegisterScriptUrl(
				name: "jquery",
				url: "https://cdn.jsdelivr.net/npm/jquery@3.5.1/dist/jquery.min.js",
				integrityHash: default
			);
			config.Resources.RegisterScriptUrl(
				name: "bootstrap",
				url: "https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js",
				integrityHash: default,
				dependencies: new[] { "jquery", "bootstrap-css" }
			);
			config.Resources.RegisterStylesheetUrl(
				name: "bootstrap-css",
				url: "https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css",
				integrityHash: default
			);
			config.Resources.RegisterStylesheetUrl(
				name: "font-awesome",
				url: "https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.7.2/css/all.min.css",
				integrityHash: default);

			// register custom resources and adjust paths to the built-in resources
			//config.Resources.Register("Styles", new StylesheetResource()
			//{
			//	Location = new UrlResourceLocation("~/Resources/style.css")
			//});
		}

		public void ConfigureServices(IDotvvmServiceCollection options)
		{
			options.AddDefaultTempStorages("temp");
			options.AddHotReload();
		}
	}
}
