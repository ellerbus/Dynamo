using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMoq;
using FizzWare.NBuilder;
using Moq;
using Sample.Core.Models;
using Sample.Core.Repositories;
using Sample.Core.Services;

namespace Sample.Tests.Core.Services
{
	[TestClass]
	public class MemberServiceTests
	{
		#region Helpers/Test Initializers

		private AutoMoqer Mocker { get; set; }
		private Mock<IMemberRepository> MockRepo { get; set; }
		private MemberService SubjectUnderTest { get; set; }

		[TestInitialize]
		public void TestInitialize()
		{
			Mocker = new AutoMoqer();

			SubjectUnderTest = Mocker.Create<MemberService>();
			
			MockRepo = Mocker.GetMock<IMemberRepository>();
		}

		#endregion

		#region Tests
		
		[TestMethod]
		public void MemberService_Should_GetMany()
		{
			//	arrange
			var expected = Builder<Member>.CreateListOfSize(10).Build(); 
		
			MockRepo.Setup(x => x.Get()).Returns(expected);

			//	act
			var actual = SubjectUnderTest.Get();

			//	assert
			CollectionAssert.AreEqual(expected as ICollection, actual as ICollection);

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberService_Should_GetOne()
		{
			//	arrange
			var expected = Builder<Member>.CreateNew().Build();
			
			MockRepo.Setup(x => x.Get(expected.Id)).Returns(expected);

			//	act
			var actual = SubjectUnderTest.Get(expected.Id);

			//	assert
			Assert.IsNotNull(actual);			
			
			Assert.AreEqual(expected.Id, actual.Id);
			Assert.AreEqual(expected.Name, actual.Name);
			Assert.AreEqual(expected.CreatedAt, actual.CreatedAt);
			Assert.AreEqual(expected.UpdatedAt, actual.UpdatedAt);

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberService_Should_Save()
		{
			//	arrange
			var expected = Builder<Member>.CreateNew().Build();
			
			MockRepo.Setup(x => x.Save(expected));

			//	act
			SubjectUnderTest.Save(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberService_Should_Save_Many()
		{
			//	arrange
			var expected = Builder<Member>.CreateListOfSize(10).Build(); 
			
			MockRepo.Setup(x => x.Save(expected));

			//	act
			SubjectUnderTest.Save(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberService_Should_Insert()
		{
			//	arrange
			var expected = Builder<Member>.CreateNew().Build();
			
			MockRepo.Setup(x => x.Insert(expected));

			//	act
			SubjectUnderTest.Insert(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberService_Should_Insert_Many()
		{
			//	arrange
			var expected = Builder<Member>.CreateListOfSize(10).Build(); 
			
			MockRepo.Setup(x => x.Insert(expected));

			//	act
			SubjectUnderTest.Insert(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberService_Should_Update()
		{
			//	arrange
			var expected = Builder<Member>.CreateNew().Build();
			
			MockRepo.Setup(x => x.Update(expected));

			//	act
			SubjectUnderTest.Update(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberService_Should_Update_Many()
		{
			//	arrange
			var expected = Builder<Member>.CreateListOfSize(10).Build(); 
			
			MockRepo.Setup(x => x.Update(expected));

			//	act
			SubjectUnderTest.Update(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberService_Should_Delete()
		{
			//	arrange
			var expected = Builder<Member>.CreateNew().Build();
			
			MockRepo.Setup(x => x.Delete(expected));

			//	act
			SubjectUnderTest.Delete(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberService_Should_Delete_Many()
		{
			//	arrange
			var expected = Builder<Member>.CreateListOfSize(10).Build(); 
			
			MockRepo.Setup(x => x.Delete(expected));

			//	act
			SubjectUnderTest.Delete(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		#endregion
	}
}