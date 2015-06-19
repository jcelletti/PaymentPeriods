using JMC.Repositories.Database.Orm;
using System;

namespace JMC.Repositories.Database.Entities
{
	public abstract class SqlEntity
	{
		public Guid Id { get; set; }

		public abstract TableInformation GetInformation();
	}
}
