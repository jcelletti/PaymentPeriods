using JMC.Core.Entities;
using System.Collections.Generic;

namespace JMC.Repositories.Abstractions.Interfaces
{
	public interface ICrud<TEntity, TId> : ICreate<TEntity, TId>, IRead<TEntity, TId>, IUpdate<TEntity, TId>, IDelete<TEntity, TId> where TEntity : Entity<TId>
	{
	}

	public interface ICreate<TEntity, TId> where TEntity : Entity<TId>
	{
		TId Add(TEntity entity);
	}

	public interface IRead<TEntity, TId> where TEntity : Entity<TId>
	{
		IEnumerable<TEntity> Get();

		TEntity Get(TId id);
	}

	public interface IUpdate<TEntity, TId> where TEntity : Entity<TId>
	{
		void Update(TEntity entity);
	}

	public interface IDelete<TEntity, TId> where TEntity : Entity<TId>
	{
		void Delete(TId id);
	}
}
