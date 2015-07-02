using JMC.Repositories.Database.Entities;
using Microsoft.Data.Entity;

namespace DatStat.SqlContexts
{
	public sealed class SqlDbContext : DbContext
	{
		public SqlDbContext()
		{
			this.ChangeTracker.AutoDetectChangesEnabled = false;
		}

		public DbSet<PayerSql> Payers { get; set; }

		public DbSet<PeriodSql> Periods { get; set; }

		public DbSet<GroupSql> Groups { get; set; }

		public DbSet<PaymentSql> Payments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.MapPayers()
				.MapPeriods()
				.MapGroups()
				.MapPayments();

			base.OnModelCreating(modelBuilder);
		}
	}
}