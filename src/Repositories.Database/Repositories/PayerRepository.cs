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
			throw new NotImplementedException();
		}

		public void Delete(Guid id)
		{
			throw new NotImplementedException();
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
			throw new NotImplementedException();
		}

		public void Update(PayerEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}
