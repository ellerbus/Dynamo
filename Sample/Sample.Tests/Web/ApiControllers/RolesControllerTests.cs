using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMoq;
using FizzWare.NBuilder;
using Moq;
using FluentValidation;
using FluentValidation.Results;
using Sample.Core.Models;
using Sample.Core.Services;
using Sample.Core.Validators;
using Sample.Web.ApiControllers;

namespace Sample.Tests.ApiControllers
{
	[TestClass]
	public class RolesControllerTests
	{
		#region Helpers/Test Initializers

		private AutoMoqer Mocker { get; set; }
		private Mock<IRoleService> MockService { get; set; }
		private Mock<IValidator<Role>> MockValidator { get; set; }
		private RolesController SubjectUnderTest { get; set; }

		[TestInitialize]
		public void TestInitialize()
		{
			Mocker = new AutoMoqer();

			SubjectUnderTest = Mocker.Create<RolesController>();
			
			MockService = Mocker.GetMock<IRoleService>();
			MockValidator = Mocker.GetMock<IValidator<Role>>();
		}

		#endregion

		#region Tests - Get All
		
		[TestMethod]
		public void RolesController_Should_GetAll()
		{
			//	arrange
			var expected = Builder<Role>.CreateListOfSize(10).Build();

			MockService.Setup(x => x.Get()).Returns(expected);

			//	act
			var actual = SubjectUnderTest.GetAll();

			//	assert
			CollectionAssert.AreEqual(expected as ICollection, actual as ICollection);
			
			MockService.VerifyAll();
			MockValidator.VerifyAll();
		}

		#endregion

		#region Tests - Get One
		
		[TestMethod]
		public void RolesController_Should_GetOne()
		{
			//	arrange
			var expected = Builder<Role>.CreateNew().Build();
			
			MockService.Setup(x => x.Get(expected.Id)).Returns(expected);

			//	act
			var actual = SubjectUnderTest.Get(expected.Id);

			//	assert
			Assert.IsNotNull(actual);			
			
			Assert.AreEqual(expected.Id, actual.Id);
			Assert.AreEqual(expected.Name, actual.Name);
			Assert.AreEqual(expected.CreatedAt, actual.CreatedAt);
			Assert.AreEqual(expected.UpdatedAt, actual.UpdatedAt);
			
			MockService.VerifyAll();
			MockValidator.VerifyAll();
		}

		#endregion

		#region Tests - Post One
		
		[TestMethod]
		public void RolesController_Should_PostOne()
		{
			//	arrange
			var expected = Builder<Role>.CreateNew().Build();

			MockValidator.Setup(x => x.Validate(expected)).Returns(new ValidationResult(new ValidationFailure[0]));
			
			MockService.Setup(x => x.Insert(expected));

			//	act
			SubjectUnderTest.Post(expected);

			//	assert
			
			MockService.VerifyAll();
			MockValidator.VerifyAll();
		}

		#endregion

		#region Tests - Put One
		
		[TestMethod]
		public void RolesController_Should_PutOne()
		{
			//	arrange
			var expected = Builder<Role>.CreateNew().Build();

			MockValidator.Setup(x => x.Validate(expected)).Returns(new ValidationResult(new ValidationFailure[0]));
			
			MockService.Setup(x => x.Get(expected.Id)).Returns(expected);
			
			MockService.Setup(x => x.Update(expected));

			//	act
			SubjectUnderTest.Put(expected.Id, expected);

			//	assert
			
			MockService.VerifyAll();
			MockValidator.VerifyAll();
		}

		#endregion

		#region Tests - Delete One
		
		[TestMethod]
		public void RolesController_Should_DeleteOne()
		{
			//	arrange
			var expected = Builder<Role>.CreateNew().Build();
			
			MockService.Setup(x => x.Get(expected.Id)).Returns(expected);
			
			MockService.Setup(x => x.Delete(expected));

			//	act
			SubjectUnderTest.Delete(expected.Id);

			//	assert
			
			MockService.VerifyAll();
			MockValidator.VerifyAll();
		}

		#endregion
/*		
		#region Verb Actions

		// GET: api/role
		[HttpGet, Route("")]
		public IEnumerable<Role> GetAll()
		{
			return _service.Get();
		}

		// GET: api/role/5
		[HttpGet, Route("{id}")]
		public Role Get(int id)
		{
			Role role = _service.Get(id);
			
			if (role == null)
			{
			
			}

			return role;
		}

		// POST: api/role
		[HttpPost, Route("")]
		public void Post([FromBody]Role role)
		{
			ValidationResult vr = _validator.Validate(role);

			if (vr.IsValid)
			{
				_service.Save(role);

				return;
			}
			
			throw new NotImplementedException();
		}

		// PUT: api/role/5
		[HttpPut, Route("{id}")]
		public void Put(int id, [FromBody]Role role)
		{
			Role model = _service.Get(id);
			
			model.Name = role.Name;
			model.CreatedAt = role.CreatedAt;
			model.UpdatedAt = role.UpdatedAt;

			ValidationResult vr = _validator.Validate(model);

			if (vr.IsValid)
			{
				_service.Save(model);

				return;
			}
			
			throw new NotImplementedException();
		}

		// DELETE: api/role/5
		[HttpDelete, Route("{id}")]
		public void Delete(int id)
		{
			Role model = _service.Get(id);

			_service.Delete(model);
		}
		
		#endregion
		*/
	}
}
