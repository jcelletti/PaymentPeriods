﻿using JMC.Web.DTOs;
using Microsoft.AspNet.Mvc;

namespace JMC.Web.Controllers
{
	[Route("api/[controller]")]
	public abstract class RestControllerBase<TEntity, TId> where TEntity : DtoEntity<TId>
	{
		[HttpPost]
		[Route("")]
		public abstract IActionResult Post();

		[HttpGet]
		[Route("")]
		public abstract IActionResult Get();

		[HttpGet]
		[Route("")]
		public abstract IActionResult Get(TId id);

		[HttpPut]
		[Route("")]
		public abstract IActionResult Put(TId id, TEntity entity);

		[HttpDelete]
		[Route("")]
		public abstract IActionResult Delete(TId id);
		
		protected IActionResult Ok(object value)
		{
			return new ObjectResult(value);
		}
	}
}