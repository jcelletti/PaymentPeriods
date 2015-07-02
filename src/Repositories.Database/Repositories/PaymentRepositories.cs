using System;
using System.Collections.Generic;
using JMC.Core.Entities;
using JMC.Repositories.Abstractions.Interfaces;

namespace JMC.Repositories.Database.Repositories
{
	public class PaymentDatabaseRepository : IPaymentRepository
	{
		public Guid Add(PaymentEntity entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<PaymentEntity> Get()
		{
			throw new NotImplementedException();
		}

		public PaymentEntity Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<PaymentEntity> GetByGroup(Guid groupId)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<PaymentEntity> GetByPeriod(Guid periodId)
		{
			throw new NotImplementedException();
		}

		public void Update(PaymentEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}
