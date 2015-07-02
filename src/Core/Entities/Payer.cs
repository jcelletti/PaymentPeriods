using System;

namespace JMC.Core.Entities
{
	public class PayerEntity : Entity<Guid>
	{
		public string First { get; set; }

		public string Last { get; set; }
	}
}
