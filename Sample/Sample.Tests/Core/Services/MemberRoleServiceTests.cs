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
	public class MemberRoleServiceTests
	{
		#region Helpers/Test Initializers

		private AutoMoqer Mocker { get; set; }
		private Mock<IMemberRoleRepository> MockRepo { get; set; }
		private MemberRoleService SubjectUnderTest { get; set; }

		[TestInitialize]
		public void TestInitialize()
		{
			Mocker = new AutoMoqer();

			SubjectUnderTest = Mocker.Create<MemberRoleService>();
			
			MockRepo = Mocker.GetMock<IMemberRoleRepository>();
		}

		#endregion

		#region Tests
		
		[TestMethod]
		public void MemberRoleService_Should_GetMany()
		{
			//	arrange
			var expected = Builder<MemberRole>.CreateListOfSize(10).Build(); 
		
			MockRepo.Setup(x => x.Get()).Returns(expected);

			//	act
			var actual = SubjectUnderTest.Get();

			//	assert
			CollectionAssert.AreEqual(expected as ICollection, actual as ICollection);

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberRoleService_Should_GetOne()
		{
			//	arrange
			var expected = Builder<MemberRole>.CreateNew().Build();
			
			MockRepo.Setup(x => x.Get(expected.MemberId, expected.RoleId)).Returns(expected);

			//	act
			var actual = SubjectUnderTest.Get(expected.MemberId, expected.RoleId);

			//	assert
			Assert.IsNotNull(actual);			
			
			Assert.AreEqual(expected.MemberId, actual.MemberId);
			Assert.AreEqual(expected.RoleId, actual.RoleId);
			Assert.AreEqual(expected.CreatedAt, actual.CreatedAt);

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberRoleService_Should_Save()
		{
			//	arrange
			var expected = Builder<MemberRole>.CreateNew().Build();
			
			MockRepo.Setup(x => x.Save(expected));

			//	act
			SubjectUnderTest.Save(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberRoleService_Should_Save_Many()
		{
			//	arrange
			var expected = Builder<MemberRole>.CreateListOfSize(10).Build(); 
			
			MockRepo.Setup(x => x.Save(expected));

			//	act
			SubjectUnderTest.Save(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberRoleService_Should_Insert()
		{
			//	arrange
			var expected = Builder<MemberRole>.CreateNew().Build();
			
			MockRepo.Setup(x => x.Insert(expected));

			//	act
			SubjectUnderTest.Insert(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberRoleService_Should_Insert_Many()
		{
			//	arrange
			var expected = Builder<MemberRole>.CreateListOfSize(10).Build(); 
			
			MockRepo.Setup(x => x.Insert(expected));

			//	act
			SubjectUnderTest.Insert(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberRoleService_Should_Update()
		{
			//	arrange
			var expected = Builder<MemberRole>.CreateNew().Build();
			
			MockRepo.Setup(x => x.Update(expected));

			//	act
			SubjectUnderTest.Update(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberRoleService_Should_Update_Many()
		{
			//	arrange
			var expected = Builder<MemberRole>.CreateListOfSize(10).Build(); 
			
			MockRepo.Setup(x => x.Update(expected));

			//	act
			SubjectUnderTest.Update(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberRoleService_Should_Delete()
		{
			//	arrange
			var expected = Builder<MemberRole>.CreateNew().Build();
			
			MockRepo.Setup(x => x.Delete(expected));

			//	act
			SubjectUnderTest.Delete(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		[TestMethod]
		public void MemberRoleService_Should_Delete_Many()
		{
			//	arrange
			var expected = Builder<MemberRole>.CreateListOfSize(10).Build(); 
			
			MockRepo.Setup(x => x.Delete(expected));

			//	act
			SubjectUnderTest.Delete(expected);

			//	assert

			MockRepo.VerifyAll();
		}

		#endregion
	}
}