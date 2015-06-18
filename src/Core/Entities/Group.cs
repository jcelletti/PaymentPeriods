using System;

namespace JMC.Core.Entities
{
	public class GroupEntity : Entity<Guid>
	{
		public Guid PeriodId { get; set; }

		public string Name { get; set; }

		public bool Validated { get; set; }
	}
}
