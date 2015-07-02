using System;
using JMC.Web.DTOs;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;

namespace JMC.Web.Controllers.Rest
{
	[Route("api/[controller]")]
	public abstract class RestControllerBase<TApi, TId> : Controller where TApi : DtoEntity<TId>
	{
		[HttpPost]
		[Route("")]
		public abstract IActionResult Post([FromBody]TApi api);

		[HttpGet]
		[Route("")]
		public abstract IActionResult Get();

		[HttpGet]
		[Route("{id}")]
		public abstract IActionResult Get(TId id);

		[HttpPut]
		[Route("{id}")]
		public abstract IActionResult Put(TId id, [FromBody] TApi api);

		[HttpDelete]
		[Route("")]
		public abstract IActionResult Delete(TId id);

		protected IActionResult Ok(TApi value)
		{
			return new ObjectResult(value);
		}

		protected IActionResult Ok(IEnumerable<TApi> value)
		{
			return new ObjectResult(value);
		}

		protected IActionResult InvalidArgument(string argumentName)
		{
			return this.InvalidArgument<TApi>(argumentName);
		}

		protected IActionResult InvalidArgument<T>(string argumentName)
		{
			return new InvalidArgumentObjectResult(argumentName, typeof(T));
		}

		protected IActionResult InvalidState(string property)
		{
			return this.InvalidState<TApi>(property);
		}

		protected IActionResult InvalidState<T>(string property)
		{
			return new InvalidStateObjectResult(property, typeof(T));
		}

		protected IActionResult HttpNotFoundObject()
		{
			return this.HttpNotFoundObject<TApi>();
		}

		protected IActionResult HttpNotFoundObject<T>()
		{
			return new HttpNotFoundObjectResult(typeof(T));
		}

		protected IActionResult InvalidId(TId id)
		{
			return this.InvalidId<TApi>(id);
		}

		protected IActionResult InvalidId<T>(TId id)
		{
			return new InvalidIdObjectResult(id, typeof(T));
		}

		protected IActionResult NoContent()
		{
			return new NoContentResult();
		}

		protected IActionResult DuplicateObject(string property)
		{
			return this.DuplicateObject<TApi>(property);
		}

		protected IActionResult DuplicateObject<T>(string property)
		{
			return new DuplicateObjectResult(property, typeof(T));
		}
	}

	public class InvalidArgumentObjectResult : BadRequestObjectResult
	{
		public string ArgumentName { get; private set; }

		public Type ObjectType { get; private set; }

		public InvalidArgumentObjectResult(string argumentName, Type type)
			: base(new { name = argumentName, type })
		{
			this.ArgumentName = argumentName;
			this.ObjectType = type;
		}
	}

	public class InvalidStateObjectResult : BadRequestObjectResult
	{
		public string Property { get; private set; }

		public Type ObjectType { get; private set; }

		public InvalidStateObjectResult(string property, Type type)
			: base(new { property, type })
		{
			this.Property = property;
			this.ObjectType = type;
		}
	}

	public class InvalidIdObjectResult : BadRequestObjectResult
	{
		public object ObjectId { get; private set; }

		public Type ObjectType { get; private set; }

		public InvalidIdObjectResult(object id, Type type)
			: base(new { id, type })
		{
			this.ObjectId = id;
			this.ObjectType = type;
		}
	}

	public class DuplicateObjectResult : BadRequestObjectResult
	{
		public string Property { get; private set; }

		public Type ObjectType { get; private set; }

		public DuplicateObjectResult(string property, Type type)
			: base(new { property, type })
		{
			this.Property = property;
			this.ObjectType = type;
		}
	}
}
