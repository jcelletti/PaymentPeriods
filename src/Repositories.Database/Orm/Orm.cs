using JMC.Repositories.Database.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JMC.Repositories.Database.Orm
{
	public partial class Orm<TEntity> where TEntity : SqlEntity, new()
	{
		private static string GetFrom(TableInformation ti, Guid? id = null)
		{
			string where = null;

			if (id != null)
			{
				where = $"Id = '{id}'";
			}

			return Orm<TEntity>.GetFrom(ti, where);
		}

		private static string GetFrom(TableInformation ti, string where)
		{
			var sb = new StringBuilder("FROM ");

			sb.AppendFormat("{0} ", ti.TableName);

			if (!string.IsNullOrWhiteSpace(ti.Alias))
			{
				sb.AppendFormat("AS {0} ", ti.Alias);
			}

			if (!string.IsNullOrWhiteSpace(where))
			{
				sb.AppendFormat(" WHERE {0}", where);
			}

			return sb.ToString();
		}

		private static void GetItems(StringBuilder sb, string alias = null)
		{
			foreach (PropertyInfo pi in Orm<TEntity>.GetProperties())
			{
				sb.AppendFormat("{0}{1}, ", string.IsNullOrWhiteSpace(alias) ? "" : $"{alias}.", pi.Name);
			}
			sb.Remove(sb.Length - 2, 1);
		}

		private static TEntity GetFromReader(SqlDataReader reader)
		{
			TEntity item = Activator.CreateInstance<TEntity>();

			foreach (PropertyInfo property in Orm<TEntity>.GetProperties())
			{
				object obj = reader[property.Name];

				property.SetValue(item, obj);
			}

			return item;
		}

		private static bool UseQuotedValue(Type type)
		{
			return type == typeof(string) || type == typeof(Guid) || type == typeof(DateTime);
		}

		private static List<PropertyInfo> GetProperties()
		{
			return Orm<TEntity>.GetProperties(typeof(TEntity));
		}

		private static List<PropertyInfo> GetProperties(Type type)
		{
			if (type == null)
			{
				return new List<PropertyInfo>();
			}

			TypeInfo ti = type.GetTypeInfo();

			List<PropertyInfo> properties = ti.DeclaredProperties.ToList();

			properties.AddRange(Orm<TEntity>.GetProperties(ti.BaseType));

			return properties;
		}

		private static TableInformation GetTableInformation()
		{
			return (new TEntity()).GetInformation();
		}
	}
}
