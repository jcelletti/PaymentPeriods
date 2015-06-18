using JMC.Core.Entities;
using System;
using System.Collections.Generic;

namespace JMC.Repositories.Abstractions.Interfaces
{
	public interface IGroupRepository : ICrud<GroupEntity, Guid>
	{
		IEnumerable<GroupEntity> GetByPeriod(Guid periodId);
	}
}
