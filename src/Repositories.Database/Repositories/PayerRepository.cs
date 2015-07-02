using System;
using System.Collections.Generic;
using JMC.Core.Entities;
using JMC.Repositories.Abstractions.Interfaces;
using JMC.Repositories.Database.Entities;
using DatStat.SqlContexts;
using System.Linq;

namespace JMC.Repositories.Database.Repositories
{
	public class PayerRepository : IPayerRepository
	{
		private SqlDbContext dbContext { get; }

		public PayerRepository(SqlDbContext context)
		{
			this.dbContext = context;
		}

		public Guid Add(PayerEntity entity)
		{
			Guid id = Guid.NewGuid();
			var newSql = new PayerSql
			{
				Id = id,
				First = entity.First,
				Last = entity.Last
			};

			this.dbContext.Payers.Add(newSql);

			this.dbContext.SaveChanges();

			return id;
		}

		public void Delete(Guid id)
		{
			PayerSql payer = this.dbContext.Payers.FirstOrDefault(p => p.Id == id);

			if (payer == null)
			{
				throw new Exception("Not found");
			}

			this.dbContext.Payers.Remove(payer);

			this.dbContext.SaveChanges();
		}

		public IEnumerable<PayerEntity> Get()
		{
			IEnumerable<PayerSql> sqls = from sql in this.dbContext.Payers select sql;

			return sqls.Select(p => new PayerEntity
			{
				Id = p.Id,
				First = p.First,
				Last = p.Last
			});
		}

		public PayerEntity Get(Guid id)
		{
			PayerSql payer = this.dbContext.Payers.FirstOrDefault(p => p.Id == id);

			if (payer == null)
			{
				return null;
			}

			return new PayerEntity
			{
				Id = payer.Id,
				First = payer.First,
				Last = payer.Last
			};
		}

		public void Update(PayerEntity entity)
		{
			PayerSql payer = this.dbContext.Payers.FirstOrDefault(p => p.Id == entity.Id);

			if (payer == null)
			{
				throw new Exception("Not found");
			}

			payer.First = entity.First;
			payer.Last = entity.Last;

			this.dbContext.SaveChanges();
		}
	}
}
