using System;

namespace JMC.Core.Entities
{
	public class PeriodEntity : Entity<Guid>
	{
		public string Name { get; set; }

		public bool Validated { get; set; }
	}
}
