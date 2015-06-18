using JMC.Core.Entities;
using System;
using System.Collections.Generic;

namespace JMC.Repositories.Abstractions.Interfaces
{
	public interface IPaymentRepository : ICrud<PaymentEntity, Guid>
	{
		IEnumerable<PaymentEntity> GetByGroup(Guid groupId);

		IEnumerable<PaymentEntity> GetByPeriod(Guid periodId);
	}
}
