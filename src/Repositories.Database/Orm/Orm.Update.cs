using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace JMC.Repositories.Database.Orm
{
	public partial class Orm<TEntity>
	{
		public static void Update(SqlCommand command, TEntity item)
		{
			TableInformation table = Orm<TEntity>.GetTableInformation();
			var sb = new StringBuilder(string.Format("UPDATE {0} SET ", table.TableName));

			foreach (PropertyInfo pi in Orm<TEntity>.GetProperties())
			{
				sb.AppendFormat("{0} = ", pi.Name);

				if (Orm<TEntity>.UseQuotedValue(pi.PropertyType))
				{
					sb.AppendFormat("'{0}', ", pi.GetValue(item));
				}
				else
				{
					sb.AppendFormat("{0}, ", pi.GetValue(item));
				}
			}

			sb.Remove(sb.Length - 2, 1);

			sb.AppendFormat("WHERE Id = '{0}'", item.Id);

			command.CommandText = sb.ToString();

			command.ExecuteNonQuery();
		}
	}
}
