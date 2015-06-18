using JMC.Core.Entities;
using JMC.Repositories.Abstractions.Interfaces;
using System;
using System.Collections.Generic;

namespace JMC.Repositories.Database.Repositories
{
	public class GroupRepository : IGroupRepository
	{
		public Guid Add(GroupEntity entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<GroupEntity> Get()
		{
			throw new NotImplementedException();
		}

		public GroupEntity Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<GroupEntity> GetByPeriod(Guid periodId)
		{
			throw new NotImplementedException();
		}

		public void Update(GroupEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}
