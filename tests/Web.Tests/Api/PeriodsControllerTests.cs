using JMC.Core.Entities;
using JMC.Repositories.Abstractions.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.AspNet.Mvc;

namespace JMC.Web.Tests.Api
{
	public class PeriodControllerTests
	{
		private static PeriodsController Setup(out Mock<IPeriodRepository> repository)
		{
			repository = new Mock<IPeriodRepository>();
			return new PeriodsController(repository.Object);
		}

		[Fact]
		public void Get()
		{
			Mock<IPeriodRepository> mock;
			PeriodsController controller = PeriodControllerTests.Setup(out mock);

			var dummyList = new List<PeriodEntity>
				{
					new PeriodEntity
					{
						Id = Guid.NewGuid(),
						Name = "Period 1"
					},
					new PeriodEntity
					{
						Id = Guid.NewGuid(),
						Name = "Period 2"
					}
				};

			mock.Setup(r => r.Get())
				.Returns(dummyList);

			IActionResult result = controller.Get();

			Assert.IsType<ObjectResult>(result);

			object content = ((ObjectResult)result).Value;

			Assert.IsType<IEnumerable<Period>>(content);

			var enumerable = (IEnumerable<Period>)content;

			Assert.Equal(dummyList.Count, enumerable.Count());
		}
	}
}
