using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
			       
			SubjectUnderTest.Request = new HttpRequestMessage();
			SubjectUnderTest.Configuration = new HttpConfiguration();
 
			MockService = Mocker.GetMock<IRoleService>();
			MockValidator = Mocker.GetMock<IValidator<Role>>();
		}
 
		#endregion
 
		#region Tests - Get All
		       
		[TestMethod]
		public void RolesController_Should_GetAll()
		{
			//	    arrange
			var expected = Builder<Role>.CreateListOfSize(10).Build();
 
			MockService.Setup(x => x.Get()).Returns(expected);
 
			//	    act
			var actual = SubjectUnderTest.GetAll();
 
			//	    assert
			CollectionAssert.AreEqual(expected as ICollection, actual as ICollection);
			       
			MockService.VerifyAll();
			MockValidator.VerifyAll();
		}
 
		#endregion
 
		#region Tests - Get One
		       
		[TestMethod]
		public void RolesController_Should_GetOne()
		{
			//	    arrange
			var expected = Builder<Role>.CreateNew().Build();
			       
			MockService.Setup(x => x.Get(expected.Id)).Returns(expected);
 
			//	    act
			var result = SubjectUnderTest.Get(expected.Id);
 
			var response = result.ExecuteAsync(CancellationToken.None);
 
			response.Wait();
 
			//	    assert
 
			Role actual = null;
 
			Assert.IsTrue(response.Result.TryGetContentValue<Role>(out actual));
			
			Assert.AreEqual(expected.Id, actual.Id);
			Assert.AreEqual(expected.Name, actual.Name);
			Assert.AreEqual(expected.CreatedAt, actual.CreatedAt);
			Assert.AreEqual(expected.UpdatedAt, actual.UpdatedAt);
			       
			MockService.VerifyAll();
			MockValidator.VerifyAll();
		}
		       
		[TestMethod]
		public void RolesController_Should_GetOne_NotFound()
		{
			//	    arrange
			var expected = Builder<Role>.CreateNew().Build();
			       
			MockService.Setup(x => x.Get(expected.Id)).Returns(null as Role);
 
			//	    act
			var result = SubjectUnderTest.Get(expected.Id);
 
			var response = result.ExecuteAsync(CancellationToken.None);
 
			response.Wait();
 
			//	    assert
			Assert.IsTrue(response.Result.StatusCode == HttpStatusCode.NotFound);
			       
			MockService.VerifyAll();
			MockValidator.VerifyAll();
		}
 
		#endregion
 
		#region Tests - Post One
		       
		[TestMethod]
		public void RolesController_Should_PostOne()
		{
			//	    arrange
			var expected = Builder<Role>.CreateNew().Build();
 
			MockValidator.Setup(x => x.Validate(expected)).Returns(new ValidationResult(new ValidationFailure[0]));
			       
			MockService.Setup(x => x.Insert(expected));
 
			//	    act
			var result = SubjectUnderTest.Post(expected);
 
			var response = result.ExecuteAsync(CancellationToken.None);
 
			response.Wait();
 
			//	    assert
			Assert.IsTrue(response.Result.StatusCode == HttpStatusCode.OK);
			       
			MockService.VerifyAll();
			MockValidator.VerifyAll();
		}
		       
		[TestMethod]
		public void RolesController_Should_PostOne_BadRequest()
		{
			//	    arrange
			var expected = Builder<Role>.CreateNew().Build();
 
			MockValidator.Setup(x => x.Validate(expected)).Returns(new ValidationResult(new []{ new ValidationFailure("", "") }));
 
			//	    act
			var result = SubjectUnderTest.Post(expected);
 
			var response = result.ExecuteAsync(CancellationToken.None);
 
			response.Wait();
 
			//	    assert
			Assert.IsTrue(response.Result.StatusCode == HttpStatusCode.BadRequest);
			       
			MockService.VerifyAll();
			MockValidator.VerifyAll();
		}
 
		#endregion
 
		#region Tests - Put One
		       
		[TestMethod]
		public void RolesController_Should_PutOne()
		{
			//	    arrange
			var expected = Builder<Role>.CreateNew().Build();
 
			MockValidator.Setup(x => x.Validate(expected)).Returns(new ValidationResult(new ValidationFailure[0]));
			       
			MockService.Setup(x => x.Get(expected.Id)).Returns(expected);
			       
			MockService.Setup(x => x.Update(expected));
 
			//	    act
			var result = SubjectUnderTest.Put(expected.Id, expected);
 
			var response = result.ExecuteAsync(CancellationToken.None);
 
			response.Wait();
 
			//	    assert
			Assert.IsTrue(response.Result.StatusCode == HttpStatusCode.OK);
			       
			MockService.VerifyAll();
			MockValidator.VerifyAll();
		}
		       
		[TestMethod]
		public void RolesController_Should_PutOne_BadRequest()
		{
			//	    arrange
			var expected = Builder<Role>.CreateNew().Build();
 
			MockValidator.Setup(x => x.Validate(expected)).Returns(new ValidationResult(new []{ new ValidationFailure("", "") }));
			       
			MockService.Setup(x => x.Get(expected.Id)).Returns(expected);
 
			//	    act
			var result = SubjectUnderTest.Put(expected.Id, expected);
 
			var response = result.ExecuteAsync(CancellationToken.None);
 
			response.Wait();
 
			//	    assert
			Assert.IsTrue(response.Result.StatusCode == HttpStatusCode.BadRequest);
			       
			MockService.VerifyAll();
			MockValidator.VerifyAll();
		}
		       
		[TestMethod]
		public void RolesController_Should_PutOne_NotFound()
		{
			//	    arrange
			var expected = Builder<Role>.CreateNew().Build();
			       
			MockService.Setup(x => x.Get(expected.Id)).Returns(null as Role);
 
			//	    act
			var result = SubjectUnderTest.Put(expected.Id, expected);
 
			var response = result.ExecuteAsync(CancellationToken.None);
 
			response.Wait();
 
			//	    assert
			Assert.IsTrue(response.Result.StatusCode == HttpStatusCode.NotFound);
			       
			MockService.VerifyAll();
			MockValidator.VerifyAll();
		}
 
		#endregion
 
		#region Tests - Delete One
		       
		[TestMethod]
		public void RolesController_Should_DeleteOne()
		{
			//	    arrange
			var expected = Builder<Role>.CreateNew().Build();
			       
			MockService.Setup(x => x.Get(expected.Id)).Returns(expected);
			       
			MockService.Setup(x => x.Delete(expected));
 
			//	    act
			var result = SubjectUnderTest.Delete(expected.Id);
 
			var response = result.ExecuteAsync(CancellationToken.None);
 
			response.Wait();
 
			//	    assert
			Assert.IsTrue(response.Result.StatusCode == HttpStatusCode.OK);
			       
			MockService.VerifyAll();
			MockValidator.VerifyAll();
		}
		       
		[TestMethod]
		public void RolesController_Should_DeleteOne_NotFound()
		{
			//	    arrange
			var expected = Builder<Role>.CreateNew().Build();
			       
			MockService.Setup(x => x.Get(expected.Id)).Returns(null as Role);
 
			//	    act
			var result = SubjectUnderTest.Delete(expected.Id);
 
			var response = result.ExecuteAsync(CancellationToken.None);
 
			response.Wait();
 
			//	    assert
			Assert.IsTrue(response.Result.StatusCode == HttpStatusCode.NotFound);
			       
			MockService.VerifyAll();
			MockValidator.VerifyAll();
		}
 
		#endregion
	}
}