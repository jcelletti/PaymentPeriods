using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;
using System;

namespace JMC.Repositories.Database.Entities
{
	public class PayerSql : EntitySql<Guid>
	{
		public string First { get; set; }

		public string Last { get; set; }
	}
	
	public static class PayerSqlMap
	{
		public static ModelBuilder MapPayers(this ModelBuilder modelBuilder)
		{
			EntityTypeBuilder<PayerSql> typeBuilder = modelBuilder
				.Entity<PayerSql>();

			typeBuilder.ForSqlServer(builder => builder.Table("Payers"));

			typeBuilder.Key(p => p.Id);

			typeBuilder.Property(p => p.First)
				.Required()
				.MaxLength(128);

			typeBuilder.Property(p => p.Last)
				.Required()
				.MaxLength(128);

			return modelBuilder;
		}
	}
}
