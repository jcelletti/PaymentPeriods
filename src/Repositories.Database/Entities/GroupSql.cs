using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;
using System;

namespace JMC.Repositories.Database.Entities
{
	public class GroupSql : EntitySql<Guid>
	{
		public string Name { get; set; }

		public bool Validated { get; set; }

		public Guid PeriodId { get; set; }

		public Guid PayerId { get; set; }
	}

	public static class GroupSqlMap
	{
		public static ModelBuilder MapGroups(this ModelBuilder modelBuilder)
		{
			EntityTypeBuilder<GroupSql> typeBuilder = modelBuilder
				.Entity<GroupSql>();

			typeBuilder.ForSqlServer(builder => builder.Table("Groups"));

			typeBuilder.Key(p => p.Id);

			typeBuilder.Property(p => p.Name)
				.MaxLength(256)
				.Required();

			typeBuilder.Property(p => p.Validated)
				.Required();

			typeBuilder.Property(p => p.PayerId)
				.Required();

			return modelBuilder;
		}
	}
}
