using DatStat.SqlContexts;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;

namespace JMC.Repositories.Database
{
	public static class SqlConfiguration
	{
		public static IServiceCollection UseAndSetupEntityFramework(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			serviceCollection
				.AddEntityFramework()
				.AddSqlServer()
				.AddDbContext<SqlDbContext>(options =>
				{
					string connectionString = configuration.Get("Data:DefaultConnection:ConnectionString");
					options.UseSqlServer(connectionString);
				});

			return serviceCollection;
		}
	}
}
