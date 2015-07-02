using JMC.Core.Entities;
using System;

namespace JMC.Web.DTOs
{
	public class Period : DtoEntity<Guid>
	{
		public string Name { get; set; }

		public bool Validated { get; set; }

		public static Period Parse(PeriodEntity entity)
		{
			return new Period
			{
				Id = entity.Id,
				Name = entity.Name,
				Validated = entity.Validated
			};
		}

		public PeriodEntity ToEntity()
		{
			return new PeriodEntity
			{
				Id = this.Id,
				Name = this.Name,
				Validated = this.Validated
			};
		}
	}
}
