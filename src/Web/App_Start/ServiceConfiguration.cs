using Microsoft.Framework.DependencyInjection;
using JMC.Repositories.Abstractions.Interfaces;
using JMC.Repositories.Database.Repositories;

namespace JMC.Web.App_Start
{
	internal static class ServiceConfiguration
	{
		public static void AddDependencies(IServiceCollection services)
		{
			ServiceConfiguration.AddPeriodDependencies(services);
			ServiceConfiguration.AddGroupDependencies(services);
			ServiceConfiguration.AddPaymentDependencies(services);
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
}
