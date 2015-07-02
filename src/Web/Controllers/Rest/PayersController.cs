using JMC.Repositories.Abstractions.Interfaces;
using System;
using Microsoft.AspNet.Mvc;
using JMC.Web.DTOs;
using JMC.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace JMC.Web.Controllers.Rest
{
	public class PayersController : RestControllerBase<Payer, Guid>
	{
		private IPayerRepository repository;

		public PayersController(IPayerRepository repository)
		{
			this.repository = repository;
		}

		public override IActionResult Delete(Guid id)
		{
			this.repository.Delete(id);

			return this.NoContent();
		}

		public override IActionResult Get()
		{
			IEnumerable<PayerEntity> entities = this.repository.Get();

			IEnumerable<Payer> outItems = entities.Select(Payer.Parse);

			return this.Ok(outItems);
		}

		public override IActionResult Get(Guid id)
		{
			PayerEntity entity = this.repository.Get(id);

			return this.Ok(Payer.Parse(entity));
		}

		public override IActionResult Post([FromBody]Payer model)
		{
			//todo: model state

			PayerEntity entity = model.ToEntity();

			Guid id = this.repository.Add(entity);

			return this.CreatedAtAction("Get", id);
		}

		public override IActionResult Put(Guid id, [FromBody]Payer model)
		{
			//todo: model state
			model.Id = id;
			
			this.repository.Update(model.ToEntity());

			return this.NoContent();
		}
	}

	public class PayerDetailsController : RestControllerBase<PayerDetail, Guid>
	{
		private IPayerRepository repository;

		public PayerDetailsController(IPayerRepository repository)
		{
			this.repository = repository;
		}

		public override IActionResult Delete(Guid id)
		{
			return this.HttpNotFound();
		}

		public override IActionResult Get()
		{
			IEnumerable<PayerEntity> entities = this.repository.Get();

			IEnumerable<PayerDetail> outItems = entities.Select(PayerDetail.Parse);

			return this.Ok(outItems);
		}

		public override IActionResult Get(Guid id)
		{
			return this.HttpNotFound();
		}

		public override IActionResult Post([FromBody]PayerDetail model)
		{
			return this.HttpNotFound();
		}

		public override IActionResult Put(Guid id, [FromBody]PayerDetail model)
		{
			return this.HttpNotFound();
		}
	}

}
