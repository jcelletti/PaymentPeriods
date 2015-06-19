using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace JMC.Repositories.Database.Orm
{
	public partial class Orm<TEntity>
	{
		public static TEntity First(SqlCommand command)
		{
			Orm<TEntity>.FirstInternal(command);

			using (SqlDataReader reader = command.ExecuteReader())
			{
				if (reader.Read())
				{
					return Orm<TEntity>.GetFromReader(reader);
				}
			}

			return default(TEntity);
		}

		public static TEntity Select(SqlCommand command, Guid id)
		{
			Orm<TEntity>.SelectInternal(command, id);
			using (SqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					return Orm<TEntity>.GetFromReader(reader);
				}
			}

			return default(TEntity);
		}

		public static IEnumerable<TEntity> Select(SqlCommand command)
		{
			Orm<TEntity>.SelectInternal(command);
			var items = new List<TEntity>();

			using (SqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					items.Add(Orm<TEntity>.GetFromReader(reader));
				}
			}

			return items;
		}

		private static void FirstInternal(SqlCommand command)
		{
			TableInformation table = Orm<TEntity>.GetTableInformation();

			string select = Orm<TEntity>.GetSelect(table.Alias, true);
			string from = Orm<TEntity>.GetFrom(table);

			command.CommandText = string.Format("{0}{1}", select, from);
		}

		private static void SelectInternal(SqlCommand command, Guid? id = null)
		{
			TableInformation table = Orm<TEntity>.GetTableInformation();

			string select = Orm<TEntity>.GetSelect(table.Alias);
			string from = Orm<TEntity>.GetFrom(table, id);

			command.CommandText = string.Format("{0}{1}", select, from);
		}

		private static void SelectInternal(SqlCommand command, string where)
		{
			TableInformation table = Orm<TEntity>.GetTableInformation();

			string select = Orm<TEntity>.GetSelect(table.Alias);
			string from = Orm<TEntity>.GetFrom(table, where);

			command.CommandText = string.Format("{0}{1}", select, from);
		}

		private static string GetSelect(string alias, bool top1 = false)
		{
			StringBuilder sb = new StringBuilder("SELECT ");

			if (top1)
			{
				sb.Append("TOP 1 ");
			}

			Orm<TEntity>.GetItems(sb, alias);

			return sb.ToString();
		}
	}
}
