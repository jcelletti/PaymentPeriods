﻿using JMC.Repositories.Database.Extensions;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

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
			services.AddSql(this.configuration.Get("Data:DefaultConnection:ConnectionString"), 30);
				//todo:configure
				//.Configure<SqlOptions>(
				//	o => { o.Configure(this.configuration.Get("Data.DefaultConnection.ConnectionString"), 30); }
				//);

			services.AddMvc()
				.Configure<MvcOptions>(o =>
			{
				o.OutputFormatters.RemoveTypesOf<JsonOutputFormatter>();

				var jsonFormatter = new JsonOutputFormatter
				{
					SerializerSettings = new JsonSerializerSettings
					{
						ContractResolver = new CamelCasePropertyNamesContractResolver(),
						NullValueHandling = NullValueHandling.Ignore,
						ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
						DateFormatHandling = DateFormatHandling.IsoDateFormat
					}
				};

				jsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
				jsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter());

				o.OutputFormatters.Add(jsonFormatter);
			});
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseMvc();

			app.UseSql();
		}
	}
}
