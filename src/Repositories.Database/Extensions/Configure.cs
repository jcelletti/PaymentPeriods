using JMC.Repositories.Abstractions.Interfaces;
using JMC.Repositories.Database.Repositories;
using Microsoft.Framework.DependencyInjection;
using System.Data.SqlClient;
using Microsoft.AspNet.Builder;

namespace JMC.Repositories.Database.Extensions
{
	public static class DatabaseConfiguration
	{
		public static IServiceCollection AddSql(this IServiceCollection services, string connectionString, int timeout)
		{
			DatabaseConfiguration.AddPeriodDependencies(services);
			DatabaseConfiguration.AddGroupDependencies(services);
			DatabaseConfiguration.AddPaymentDependencies(services);

			var sqlOptions = new SqlOptions();

			sqlOptions.Configure(connectionString, timeout);

			services.AddInstance(sqlOptions);

			return services;
		}

		public static IApplicationBuilder UseSql(this IApplicationBuilder app)
		{
			return app;
		}

		private static void AddPeriodDependencies(IServiceCollection services)
		{
			services.AddScoped<IPeriodRepository, PeriodDatabaseRepository>();
		}

		private static void AddGroupDependencies(IServiceCollection services)
		{
			services.AddScoped<IGroupRepository, GroupDatabaseRepository>();
		}

		private static void AddPaymentDependencies(IServiceCollection services)
		{
			services.AddScoped<IPaymentRepository, PaymentDatabaseRepository>();
		}
	}

	public class SqlOptions
	{
		public string ConnectionString { get; private set; }

		public int Timeout { get; private set; }

		public void Configure(string connectionString, int timeout)
		{
			this.ConnectionString = connectionString;

			this.Timeout = timeout > 0 ? timeout : 30;
		}

		public SqlCommand GetCommand(int timeout = -1)
		{
			if (timeout <= 0) { timeout = this.Timeout; }

			var command = new SqlCommand();
			var conn = new SqlConnection(this.ConnectionString);
			conn.Open();

			command.Connection = conn;
			command.CommandTimeout = timeout;

			//todo: transactions

			return command;
		}
	}
}
