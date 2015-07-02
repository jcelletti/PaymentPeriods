using JMC.Core.Entities;
using System;

namespace JMC.Web.DTOs
{
	public class Payer : DtoEntity<Guid>
	{
		public string First { get; set; }

		public string Last { get; set; }

		public static Payer Parse(PayerEntity entity)
		{
			return new Payer
			{
				Id = entity.Id,
				First = entity.First,
				Last = entity.Last
			};
		}

		public PayerEntity ToEntity()
		{
			return new PayerEntity
			{
				Id = this.Id,
				First = this.First,
				Last = this.Last
			};
		}
	}

	public class PayerDetail : DtoEntity<Guid>
	{
		public string Name { get; set; }

		public static PayerDetail Parse(PayerEntity entity)
		{
			return new PayerDetail
			{
				Id = entity.Id,
				Name = $"{entity.First} {entity.Last}".Trim()
			};
		}

		public PayerEntity ToEntity()
		{
			throw new NotImplementedException();
		}
	}
}
