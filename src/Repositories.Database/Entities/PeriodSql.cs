using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;
using System;

namespace JMC.Repositories.Database.Entities
{
	public class PeriodSql : EntitySql<Guid>
	{
		public string Name { get; set; }

		public bool Validated { get; set; }
	}

	public static class PeriodSqlMap
	{
		public static ModelBuilder MapPeriods(this ModelBuilder modelBuilder)
		{
			EntityTypeBuilder<PeriodSql> typeBuilder = modelBuilder
				.Entity<PeriodSql>();

			typeBuilder.ForSqlServer(builder => builder.Table("Periods"));

			typeBuilder.Key(p => p.Id);

			typeBuilder.Property(p => p.Name)
				.Required()
				.MaxLength(450);

			typeBuilder.Property(p => p.Validated)
				.Required();

			return modelBuilder;
		}
	}
}
