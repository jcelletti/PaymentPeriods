using System;
using System.Collections.Generic;
using JMC.Core.Entities;
using JMC.Repositories.Abstractions.Interfaces;
using JMC.Repositories.Database.Extensions;
using JMC.Repositories.Database.Entities;
using System.Data.SqlClient;
using System.Linq;

namespace JMC.Repositories.Database.Repositories
{
	public class PeriodDatabaseRepository : IPeriodRepository
	{
		private SqlOptions options;

		public PeriodDatabaseRepository(SqlOptions options)
		{
			this.options = options;
		}

		public Guid Add(PeriodEntity entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<PeriodEntity> Get()
		{
			using (SqlCommand command = options.GetCommand())
			{
				IEnumerable<OrmPeriod> ormObjs = Orm.Orm<OrmPeriod>.Select(command);

				return ormObjs.Select(o => new PeriodEntity
				{
					Id = o.Id
				});
			}
		}

		public PeriodEntity Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Update(PeriodEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}
