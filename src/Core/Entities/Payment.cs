using System;

namespace JMC.Core.Entities
{
	public enum PaymentType
	{
		Tip = 0,
		Tax = 1,
		Total = 2
	}

	public class PaymentEntity : Entity<Guid>
	{
		public Guid GroupId { get; set; }

		public Guid UserId { get; set; }

		public PaymentType Type { get; set; }

		public decimal Value { get; set; }
	}
}
