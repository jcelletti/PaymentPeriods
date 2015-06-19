using System;

namespace JMC.Repositories.Database.Orm
{
	public class TableInformation
	{
		public TableInformation(string tableName, string alias = null)
		{
			this.TableName = tableName;
			this.Alias = alias;
		}

		public string TableName { get; private set; }

		public string Alias { get; private set; }
	}
}
