using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace JMC.Repositories.Database.Orm
{
	public partial class Orm<TEntity>
	{
		public static void Insert<T>(SqlCommand command, T item)
		{
			command.CommandText = Orm<TEntity>.GetInsertInfo(item);

			command.ExecuteNonQuery();
		}

		private static string GetInsertInfo<T>(T item)
		{
			TableInformation table = Orm<TEntity>.GetTableInformation();

			StringBuilder sb = new StringBuilder("INSERT INTO ");

			sb.AppendFormat("{0} (", table.TableName);
			Orm<TEntity>.GetItems(sb);
			sb.Append(") ");

			var sbVals = new StringBuilder();
			foreach (PropertyInfo pi in Orm<TEntity>.GetProperties())
			{
				object val = pi.GetValue(item);

				if (Orm<TEntity>.UseQuotedValue(pi.PropertyType))
				{
					sbVals.AppendFormat("'{0}', ", val);
				}
				else
				{
					sbVals.AppendFormat("{0}, ", val);
				}
			}

			sbVals.Remove(sbVals.Length - 2, 1);
			
			sb.AppendFormat("VALUES({0})", sbVals.ToString());

			return sb.ToString();
		}
	}
}
