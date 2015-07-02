using JMC.Repositories.Database.Orm;

namespace JMC.Repositories.Database.Entities
{
	public class OrmPeriod : SqlEntity
	{
		public override TableInformation GetInformation()
		{
			return new TableInformation("Periods");
		}
	}
}
