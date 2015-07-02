using JMC.Repositories.Abstractions.Interfaces;
using System;
using Microsoft.AspNet.Mvc;
using JMC.Web.DTOs;
using JMC.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using JMC.Repositories.Abstractions.Exceptions;

namespace JMC.Web.Controllers.Rest
{
	public class PayersController : RestControllerBase<Payer, Guid>
	{
		private IPayerRepository _repository;

		public PayersController(IPayerRepository repository)
		{
			this._repository = repository;
		}

		public override IActionResult Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public override IActionResult Get()
		{
			IEnumerable<PayerEntity> entities = this._repository.Get();

			IEnumerable<Payer> outItems = entities.Select(e => new Payer
			{
				Id = e.Id,
				First = e.First,
				Last = e.Last
			});

			return this.Ok(outItems);
		}

		public override IActionResult Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public override IActionResult Post([FromBody]Payer api)
		{
			throw new NotImplementedException();
		}

		public override IActionResult Put(Guid id, [FromBody]Payer api)
		{
			throw new NotImplementedException();
		}
	}
}
