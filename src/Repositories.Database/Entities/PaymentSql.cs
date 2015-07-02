using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;
using System;

namespace JMC.Repositories.Database.Entities
{
	public class PaymentSql : EntitySql<Guid>
	{
		public int Type { get; set; }

		public decimal Value { get; set; }

		public Guid GroupId { get; set; }

		public Guid PayerId { get; set; }
	}

	public static class PaymentSqlMap
	{
		public static ModelBuilder MapPayments(this ModelBuilder modelBuilder)
		{
			EntityTypeBuilder<PaymentSql> typeBuilder = modelBuilder
				.Entity<PaymentSql>();

			typeBuilder.ForSqlServer(builder => builder.Table("Payments"));

			typeBuilder.Key(p => p.Id);

			typeBuilder.Property(p => p.Type)
				.Required();

			typeBuilder.Property(p => p.Value)
				.Required();

			typeBuilder.Property(p => p.GroupId)
				.Required();

			typeBuilder.Property(p => p.PayerId)
				.Required();

			return modelBuilder;
		}
	}
}
