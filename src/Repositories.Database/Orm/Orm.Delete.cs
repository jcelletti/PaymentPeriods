using System;
using System.Data.SqlClient;

namespace JMC.Repositories.Database.Orm
{
	public partial class Orm<TEntity>
	{
		public static void Delete(SqlCommand command, Guid id)
		{
			TableInformation table = Orm<TEntity>.GetTableInformation();

			command.CommandText = string.Format("DELETE FROM {0} WHERE Id = '{1}'", table.TableName, id);

			command.ExecuteNonQuery();
		}
	}
}
