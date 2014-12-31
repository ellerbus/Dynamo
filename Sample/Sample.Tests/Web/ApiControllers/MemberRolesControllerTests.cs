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
	public class MemberRolesControllerTests
	{
		#region Helpers/Test Initializers

		private AutoMoqer Mocker { get; set; }
		private Mock<IMemberRoleService> MockService { get; set; }
		private Mock<IValidator<MemberRole>> MockValidator { get; set; }
		private MemberRolesController SubjectUnderTest { get; set; }

		[TestInitialize]
		public void TestInitialize()
		{
			Mocker = new AutoMoqer();

			SubjectUnderTest = Mocker.Create<MemberRolesController>();
			
			MockService = Mocker.GetMock<IMemberRoleService>();
			MockValidator = Mocker.GetMock<IValidator<MemberRole>>();
		}

		#endregion

		#region Tests - Get All
		
		[TestMethod]
		public void MemberRolesController_Should_GetAll()
		{
			//	arrange
			var expected = Builder<MemberRole>.CreateListOfSize(10).Build();

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
		public void MemberRolesController_Should_GetOne()
		{
			//	arrange
			var expected = Builder<MemberRole>.CreateNew().Build();
			
			MockService.Setup(x => x.Get(expected.MemberId, expected.RoleId)).Returns(expected);

			//	act
			var actual = SubjectUnderTest.Get(expected.MemberId, expected.RoleId);

			//	assert
			Assert.IsNotNull(actual);			
			
			Assert.AreEqual(expected.MemberId, actual.MemberId);
			Assert.AreEqual(expected.RoleId, actual.RoleId);
			Assert.AreEqual(expected.CreatedAt, actual.CreatedAt);
			
			MockService.VerifyAll();
			MockValidator.VerifyAll();
		}

		#endregion

		#region Tests - Post One
		
		[TestMethod]
		public void MemberRolesController_Should_PostOne()
		{
			//	arrange
			var expected = Builder<MemberRole>.CreateNew().Build();

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
		public void MemberRolesController_Should_PutOne()
		{
			//	arrange
			var expected = Builder<MemberRole>.CreateNew().Build();

			MockValidator.Setup(x => x.Validate(expected)).Returns(new ValidationResult(new ValidationFailure[0]));
			
			MockService.Setup(x => x.Get(expected.MemberId, expected.RoleId)).Returns(expected);
			
			MockService.Setup(x => x.Update(expected));

			//	act
			SubjectUnderTest.Put(expected.MemberId, expected.RoleId, expected);

			//	assert
			
			MockService.VerifyAll();
			MockValidator.VerifyAll();
		}

		#endregion

		#region Tests - Delete One
		
		[TestMethod]
		public void MemberRolesController_Should_DeleteOne()
		{
			//	arrange
			var expected = Builder<MemberRole>.CreateNew().Build();
			
			MockService.Setup(x => x.Get(expected.MemberId, expected.RoleId)).Returns(expected);
			
			MockService.Setup(x => x.Delete(expected));

			//	act
			SubjectUnderTest.Delete(expected.MemberId, expected.RoleId);

			//	assert
			
			MockService.VerifyAll();
			MockValidator.VerifyAll();
		}

		#endregion
/*		
		#region Verb Actions

		// GET: api/memberRole
		[HttpGet, Route("")]
		public IEnumerable<MemberRole> GetAll()
		{
			return _service.Get();
		}

		// GET: api/memberRole/5
		[HttpGet, Route("{memberId}/{roleId}")]
		public MemberRole Get(int memberId, int roleId)
		{
			MemberRole memberRole = _service.Get(memberId, roleId);
			
			if (memberRole == null)
			{
			
			}

			return memberRole;
		}

		// POST: api/memberRole
		[HttpPost, Route("")]
		public void Post([FromBody]MemberRole memberRole)
		{
			ValidationResult vr = _validator.Validate(memberRole);

			if (vr.IsValid)
			{
				_service.Save(memberRole);

				return;
			}
			
			throw new NotImplementedException();
		}

		// PUT: api/memberRole/5
		[HttpPut, Route("{memberId}/{roleId}")]
		public void Put(int memberId, int roleId, [FromBody]MemberRole memberRole)
		{
			MemberRole model = _service.Get(memberId, roleId);
			
			model.CreatedAt = memberRole.CreatedAt;

			ValidationResult vr = _validator.Validate(model);

			if (vr.IsValid)
			{
				_service.Save(model);

				return;
			}
			
			throw new NotImplementedException();
		}

		// DELETE: api/memberRole/5
		[HttpDelete, Route("{memberId}/{roleId}")]
		public void Delete(int memberId, int roleId)
		{
			MemberRole model = _service.Get(memberId, roleId);

			_service.Delete(model);
		}
		
		#endregion
		*/
	}
}
