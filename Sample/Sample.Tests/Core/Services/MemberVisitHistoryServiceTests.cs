using System;
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
	public class MemberVisitHistoryServiceTests
	{
		#region Helpers/Test Initializers

		private AutoMoqer Mocker { get; set; }
		private Mock<IMemberVisitHistoryRepository> MockRepo { get; set; }
		private MemberVisitHistoryService SubjectUnderTest { get; set; }

		[TestInitialize]
		public void TestInitialize()
		{
			Mocker = new AutoMoqer();

			SubjectUnderTest = Mocker.Create<MemberVisitHistoryService>();
			
			MockRepo = Mocker.GetMock<IMemberVisitHistoryRepository>();
		}

		#endregion

		#region Tests
		
		//[TestMethod]
		//public void MemberVisitHistoryService_Should_GetList()
		//{
		//	//	arrange
		//	var expected = Builder<MemberVisitHistory>.CreateListOfSize(10).Build(); 
		
		//	MockRepo.Setup(x => x.GetList()).Returns(expected);

		//	//	act
		//	var actual = SubjectUnderTest.GetList();

		//	//	assert
		//	CollectionAssert.AreEqual(expected, actual);

		//	MockRepo.VerifyAll();
		//}

		[TestMethod]
		public void MemberVisitHistoryService_Should_Get()
		{
			//	arrange
			var expected = Builder<MemberVisitHistory>.CreateNew().Build();
			
			MockRepo.Setup(x => x.Get(expected.MemberId, expected.VisitedAt)).Returns(expected);

			//	act
			var actual = SubjectUnderTest.Get(expected.MemberId, expected.VisitedAt);

			//	assert
			Assert.IsNotNull(actual);			
			
			Assert.AreEqual(expected.MemberId, actual.MemberId);
			Assert.AreEqual(expected.VisitedAt, actual.VisitedAt);
			Assert.AreEqual(expected.PageUrl, actual.PageUrl);

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberVisitHistoryService_Should_Save()
		{
			//	arrange
			var expected = Builder<MemberVisitHistory>.CreateNew().Build();
			
			MockRepo.Setup(x => x.Save(expected));

			//	act
			SubjectUnderTest.Save(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberVisitHistoryService_Should_Save_Many()
		{
			//	arrange
			var expected = Builder<MemberVisitHistory>.CreateListOfSize(10).Build(); 
			
			MockRepo.Setup(x => x.Save(expected));

			//	act
			SubjectUnderTest.Save(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberVisitHistoryService_Should_Delete()
		{
			//	arrange
			var expected = Builder<MemberVisitHistory>.CreateNew().Build();
			
			MockRepo.Setup(x => x.Delete(expected));

			//	act
			SubjectUnderTest.Delete(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberVisitHistoryService_Should_Delete_Many()
		{
			//	arrange
			var expected = Builder<MemberVisitHistory>.CreateListOfSize(10).Build(); 
			
			MockRepo.Setup(x => x.Delete(expected));

			//	act
			SubjectUnderTest.Delete(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		#endregion
	}
}