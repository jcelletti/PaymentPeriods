using JMC.Core.Entities;
using JMC.Repositories.Abstractions.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.AspNet.Mvc;
using JMC.Web.Controllers;
using JMC.Web.DTOs;
using System.Linq;

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
		public void Get_GetAll_IsValid()
		{
			Mock<IPeriodRepository> mock;
			PeriodsController sut = PeriodControllerTests.Setup(out mock);

			IEnumerable<PeriodEntity> dummyList = new List<PeriodEntity>
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

			IActionResult result = sut.Get();

			Assert.IsType<ObjectResult>(result);

			IEnumerable<Period> enumerable = ((ObjectResult)result).Value as IEnumerable<Period>;

			Assert.NotNull(enumerable);

			Assert.Equal(dummyList.Count(), enumerable.Count());
			mock.Verify(r => r.Get(), Times.Once);
		}

		[Fact]
		public void Post_New_IsValid()
		{
			Mock<IPeriodRepository> mock;
			PeriodsController sut = PeriodControllerTests.Setup(out mock);

			Guid id = Guid.NewGuid();
			const string name = "New Period Name";

			mock.Setup(r => r.Add(It.Is<PeriodEntity>(e => e.Id == Guid.Empty && string.Equals(e.Name, name))))
				.Returns(id);

			IActionResult result = sut.Post(new Period
			{
				Name = name
			});

			Assert.IsType<CreatedAtActionResult>(result);
			mock.Verify(r => r.Add(It.Is<PeriodEntity>(e => e.Id == Guid.Empty && string.Equals(e.Name, name))), Times.Once);
		}
	}
}
