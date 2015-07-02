using JMC.Repositories.Abstractions.Interfaces;
using Microsoft.Framework.DependencyInjection;

namespace JMC.Repositories.Database.Repositories
{
	public static class DatabaseInjection
	{
		public static IServiceCollection UseDatabase(this IServiceCollection services)
		{
			return services
					.AddScoped<IPayerRepository, PayerRepository>();
		}
	}
}
