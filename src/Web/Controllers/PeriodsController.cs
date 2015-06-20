using JMC.Repositories.Abstractions.Interfaces;
using System;
using Microsoft.AspNet.Mvc;
using JMC.Web.DTOs;
using JMC.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using JMC.Repositories.Abstractions.Exceptions;

namespace JMC.Web.Controllers
{
	public class PeriodsController : RestControllerBase<Period, Guid>
	{
		private IPeriodRepository _repository;

		public PeriodsController(IPeriodRepository repository)
		{
			this._repository = repository;
		}

		public override IActionResult Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public override IActionResult Get()
		{
			IEnumerable<PeriodEntity> items = this._repository.Get();

			IEnumerable<Period> periods = items.Select(Period.Parse);

			return this.Ok(periods);
		}

		public override IActionResult Get(Guid id)
		{
			if (id == Guid.Empty)
			{
				return this.InvalidId(id);
			}

			PeriodEntity entity = this._repository.Get(id);

			if (entity == null)
			{
				return this.HttpNotFoundObject();
			}

			return this.Ok(Period.Parse(entity));
		}

		public override IActionResult Post(Period model)
		{
			if (model == null)
			{
				return this.InvalidArgument<Period>(nameof(model));
			}

			try
			{
				Guid id = this._repository.Add(model.ToEntity());

				return this.CreatedAtAction("Get", id);
			}
			catch (InvalidObjectStateException e)
			{
				return this.InvalidState(e.Property);
			}
			catch (DuplicateObjectException e)
			{
				return this.DuplicateObject(e.Property);
			}
		}

		public override IActionResult Put(Guid id, Period model)
		{
			if (id == Guid.Empty)
			{
				return this.InvalidId(id);
			}

			if (model == null)
			{
				return this.InvalidArgument(nameof(model));
			}

			PeriodEntity entity = this._repository.Get(id);

			if (entity == null)
			{
				return this.HttpNotFoundObject();
			}

			//todo: mapper
			entity.Name = model.Name;

			this._repository.Update(entity);

			return this.NoContent();
		}

		//todo: get by validated state
	}
}
