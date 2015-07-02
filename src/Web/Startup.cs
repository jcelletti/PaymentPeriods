using JMC.Repositories.Database;
using JMC.Repositories.Database.Repositories;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace JMC.Web
{
	public class Startup
	{
		private IConfiguration configuration { get; set; }

		public Startup(IHostingEnvironment env)
		{
			this.configuration = new Configuration()
				.AddJsonFile("config.json");
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.UseAndSetupEntityFramework(this.configuration);

			services
				.UseDatabase();

			services.AddMvc()
				.ConfigureMvc(o =>
				{
					JsonOutputFormatter jsonOutputFormatter = o.OutputFormatters.OfType<JsonOutputFormatter>().FirstOrDefault();

					if (jsonOutputFormatter == null)
					{
						jsonOutputFormatter = new JsonOutputFormatter();
						o.OutputFormatters.Insert(0, jsonOutputFormatter);
					}

					jsonOutputFormatter.SerializerSettings = new JsonSerializerSettings
					{
						ContractResolver = new CamelCasePropertyNamesContractResolver(),
						ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
						NullValueHandling = NullValueHandling.Ignore,
						TypeNameHandling = TypeNameHandling.Auto,
						DateFormatHandling = DateFormatHandling.IsoDateFormat,
#if DEBUG
						Formatting = Formatting.Indented,
#endif
						Converters = new List<JsonConverter>
							{
								new StringEnumConverter(),
								new IsoDateTimeConverter()
							}
					};

				});
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseMvc();
		}
	}
}
