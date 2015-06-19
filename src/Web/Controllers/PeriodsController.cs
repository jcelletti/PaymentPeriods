using JMC.Repositories.Abstractions.Interfaces;
using System;
using Microsoft.AspNet.Mvc;
using JMC.Web.DTOs;
using JMC.Core.Entities;
using System.Collections.Generic;
using System.Linq;

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
			throw new NotImplementedException();
		}

		public override IActionResult Post(Period entity)
		{
			Guid id = this._repository.Add(entity.ToEntity());

			return this.CreatedAtAction("Get", id);
		}

		public override IActionResult Put(Guid id, Period entity)
		{
			throw new NotImplementedException();
		}
	}
}
