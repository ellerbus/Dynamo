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
	public class MembersControllerTests
	{
		#region Helpers/Test Initializers

		private AutoMoqer Mocker { get; set; }
		private Mock<IMemberService> MockService { get; set; }
		private Mock<IValidator<Member>> MockValidator { get; set; }
		private MembersController SubjectUnderTest { get; set; }

		[TestInitialize]
		public void TestInitialize()
		{
			Mocker = new AutoMoqer();

			SubjectUnderTest = Mocker.Create<MembersController>();
			
			MockService = Mocker.GetMock<IMemberService>();
			MockValidator = Mocker.GetMock<IValidator<Member>>();
		}

		#endregion

		#region Tests - Get All
		
		[TestMethod]
		public void MembersController_Should_GetAll()
		{
			//	arrange
			var expected = Builder<Member>.CreateListOfSize(10).Build();

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
		public void MembersController_Should_GetOne()
		{
			//	arrange
			var expected = Builder<Member>.CreateNew().Build();
			
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
		public void MembersController_Should_PostOne()
		{
			//	arrange
			var expected = Builder<Member>.CreateNew().Build();

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
		public void MembersController_Should_PutOne()
		{
			//	arrange
			var expected = Builder<Member>.CreateNew().Build();

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
		public void MembersController_Should_DeleteOne()
		{
			//	arrange
			var expected = Builder<Member>.CreateNew().Build();
			
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

		// GET: api/member
		[HttpGet, Route("")]
		public IEnumerable<Member> GetAll()
		{
			return _service.Get();
		}

		// GET: api/member/5
		[HttpGet, Route("{id}")]
		public Member Get(int id)
		{
			Member member = _service.Get(id);
			
			if (member == null)
			{
			
			}

			return member;
		}

		// POST: api/member
		[HttpPost, Route("")]
		public void Post([FromBody]Member member)
		{
			ValidationResult vr = _validator.Validate(member);

			if (vr.IsValid)
			{
				_service.Save(member);

				return;
			}
			
			throw new NotImplementedException();
		}

		// PUT: api/member/5
		[HttpPut, Route("{id}")]
		public void Put(int id, [FromBody]Member member)
		{
			Member model = _service.Get(id);
			
			model.Name = member.Name;
			model.CreatedAt = member.CreatedAt;
			model.UpdatedAt = member.UpdatedAt;

			ValidationResult vr = _validator.Validate(model);

			if (vr.IsValid)
			{
				_service.Save(model);

				return;
			}
			
			throw new NotImplementedException();
		}

		// DELETE: api/member/5
		[HttpDelete, Route("{id}")]
		public void Delete(int id)
		{
			Member model = _service.Get(id);

			_service.Delete(model);
		}
		
		#endregion
		*/
	}
}
