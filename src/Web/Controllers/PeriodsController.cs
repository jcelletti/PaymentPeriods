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

		public override IActionResult Post()
		{
			throw new NotImplementedException();
		}

		public override IActionResult Put(Guid id, Period entity)
		{
			throw new NotImplementedException();
		}
	}
}
