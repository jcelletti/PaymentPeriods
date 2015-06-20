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
using JMC.Repositories.Abstractions.Exceptions;

namespace JMC.Web.Tests.Api
{
	public class PeriodControllerTests
	{
		private static PeriodsController Setup(out Mock<IPeriodRepository> repository)
		{
			repository = new Mock<IPeriodRepository>();
			return new PeriodsController(repository.Object);
		}

		[Theory]
		[InlineData("Name 1", false)]
		[InlineData("Name 2", false)]
		[InlineData("Name 3", false)]
		[InlineData("Name 4", false)]
		[InlineData("Validated 1", true)]
		[InlineData("Validated 2", true)]
		[InlineData("Validated 3", true)]
		[InlineData("Validated 4", true)]
		public void Get_GetById_IsValid(string name, bool validated)
		{
			Mock<IPeriodRepository> mock;
			PeriodsController sut = PeriodControllerTests.Setup(out mock);

			Guid id = Guid.NewGuid();

			var dummy = new PeriodEntity
			{
				Id = id,
				Name = name,
				Validated = validated
			};

			mock.Setup(r => r.Get(It.Is<Guid>(g => g == id)))
				.Returns(dummy);

			IActionResult result = sut.Get(id);

			Assert.IsType<ObjectResult>(result);
			var objResult = (ObjectResult)result;

			Assert.IsType<Period>(objResult.Value);

			Period period = (Period)objResult.Value;

			Assert.Equal(id, period.Id);

			Assert.Equal(period.Id, dummy.Id);

			Assert.Equal(period.Name, dummy.Name);

			Assert.Equal(period.Validated, dummy.Validated);

			mock.Verify(r => r.Get(It.Is<Guid>(g => g == id)), Times.Once);
		}

		[Fact]
		public void Get_GetById_NotFound()
		{
			Mock<IPeriodRepository> mock;
			PeriodsController sut = PeriodControllerTests.Setup(out mock);

			Guid id = Guid.NewGuid();

			mock.Setup(r => r.Get(It.Is<Guid>(g => g == id)))
				.Returns<Period>(null);

			IActionResult result = sut.Get(id);

			this.NotFound(mock, result, id);
		}

		[Fact]
		public void Get_GuidEmpty_InvalidId()
		{
			Mock<IPeriodRepository> mock;
			PeriodsController sut = PeriodControllerTests.Setup(out mock);

			Guid id = Guid.Empty;

			IActionResult result = sut.Get(id);

			this.InvalidId(result, id);
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

		[Fact]
		public void Post_NoModel_InvalidRequest()
		{
			Mock<IPeriodRepository> mock;
			PeriodsController sut = PeriodControllerTests.Setup(out mock);

			IActionResult result = sut.Post(null);

			this.InvalidRequest(result);
		}

		[Theory]
		[InlineData("Name 1")]
		[InlineData("Name 2")]
		[InlineData("Name 3")]
		public void Post_DuplicateName_Duplicate(string name)
		{
			Mock<IPeriodRepository> mock;
			PeriodsController sut = PeriodControllerTests.Setup(out mock);

			Guid id = Guid.NewGuid();

			mock.Setup(r => r.Add(It.Is<PeriodEntity>(e => e.Id == Guid.Empty && string.Equals(e.Name, name))))
				.Throws(new DuplicateObjectException("Name"));

			var newObject = new Period
			{
				Name = name,
				Validated = false
			};

			IActionResult result = sut.Post(newObject);

			Assert.IsType<DuplicateObjectResult>(result);

			var dup = (DuplicateObjectResult)result;

			Assert.Equal(nameof(newObject.Name), dup.Property);

			Assert.Equal(typeof(Period), dup.ObjectType);
		}

		//todo: post validated = true

		[Theory]
		[InlineData("Name", null, false)]
		[InlineData("Validated", "Some Name", true)]
		public void Post_BadState_InvalidState(string propertyName, string name, bool validated)
		{
			Mock<IPeriodRepository> mock;
			PeriodsController sut = PeriodControllerTests.Setup(out mock);

			var ex = new InvalidObjectStateException(propertyName);

			mock.Setup(r => r.Add(It.Is<PeriodEntity>(e => e.Id == Guid.Empty && string.Equals(e.Name, name) && e.Validated == validated)))
				.Throws(ex);

			IActionResult result = sut.Post(new Period
			{
				Name = name,
				Validated = validated
			});

			Assert.IsType<InvalidStateObjectResult>(result);

			Assert.Equal(propertyName, ((InvalidStateObjectResult)result).Property);

			mock.Verify(r => r.Add(It.Is<PeriodEntity>(e => e.Id == Guid.Empty && string.Equals(e.Name, name) && e.Validated == validated)), Times.Once);
		}

		[Theory]
		[InlineData("Old Name", "New Name")]
		[InlineData("Old Name 2", "New Name 2")]
		[InlineData("Old Name 3", "New Name 3")]
		public void Put_Update_IsValid(string oldName, string newName)
		{
			Mock<IPeriodRepository> mock;
			PeriodsController sut = PeriodControllerTests.Setup(out mock);

			Guid id = Guid.NewGuid();

			var dummy = new PeriodEntity
			{
				Id = id,
				Name = oldName,
				Validated = false
			};

			var updated = new Period
			{
				Id = id,
				Name = newName,
				Validated = dummy.Validated
			};

			mock.Setup(r => r.Get(It.Is<Guid>(g => g == id)))
				.Returns(dummy);

			mock.Setup(r => r.Update(It.Is<PeriodEntity>(e => e.Id == updated.Id && string.Equals(e.Name, newName))));

			IActionResult result = sut.Put(id, updated);

			Assert.IsType<NoContentResult>(result);

			mock.Verify(r => r.Get(It.Is<Guid>(g => g == id)), Times.Once);

			mock.Verify(r => r.Update(It.Is<PeriodEntity>(e => e.Id == updated.Id && string.Equals(e.Name, newName))), Times.Once);
		}

		[Theory]
		[InlineData("New Name")]
		[InlineData("New Name 2")]
		[InlineData("New Name 3")]
		public void Put_Id_NotFound(string newName)
		{
			Mock<IPeriodRepository> mock;
			PeriodsController sut = PeriodControllerTests.Setup(out mock);

			Guid id = Guid.NewGuid();

			var updated = new Period
			{
				Id = id,
				Name = newName,
				Validated = false
			};

			mock.Setup(r => r.Get(It.Is<Guid>(g => g == id)))
				.Returns<PeriodEntity>(null);

			IActionResult result = sut.Put(id, updated);

			this.NotFound(mock, result, id);
		}

		[Fact]
		public void Put_InvalidId_InvalidId()
		{
			Mock<IPeriodRepository> mock;
			PeriodsController sut = PeriodControllerTests.Setup(out mock);

			Guid id = Guid.Empty;

			var updated = new Period
			{
				Id = id,
				Name = "New Name",
				Validated = false
			};

			IActionResult result = sut.Put(id, updated);

			this.InvalidId(result, id);
		}

		[Fact]
		public void Put_NoModel_InvalidRequest()
		{
			Mock<IPeriodRepository> mock;
			PeriodsController sut = PeriodControllerTests.Setup(out mock);

			Guid id = Guid.NewGuid();

			IActionResult result = sut.Put(id, null);

			this.InvalidRequest(result);
		}

		//todo: model null

		//todo: id and object id do not match

		//todo: cannot update validated

		//todo: cannot update to validated

		private void InvalidId(IActionResult result, Guid id)
		{
			Assert.IsType<InvalidIdObjectResult>(result);

			var invalidId = (InvalidIdObjectResult)result;

			Assert.Equal(id, invalidId.ObjectId);

			Assert.Equal(typeof(Period), invalidId.ObjectType);
		}

		private void NotFound(Mock<IPeriodRepository> mock, IActionResult result, Guid id)
		{
			Assert.IsType<HttpNotFoundObjectResult>(result);

			mock.Verify(r => r.Get(It.Is<Guid>(g => g == id)), Times.Once);
		}

		private void InvalidRequest(IActionResult result)
		{
			Assert.IsType<InvalidArgumentObjectResult>(result);

			Assert.Equal(typeof(Period), ((InvalidArgumentObjectResult)result).ObjectType);
		}
	}
}
