using AutoMoq;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NerdBudget.Core.Models;
using NerdBudget.Core.Repositories;
using NerdBudget.Core.Services;

namespace NerdBudget.Tests.Core.Services
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

        #region Test

        [TestMethod]
        public void MemberService_Should_Login_Successfully()
        {
            //  arrange
            var name = "abc";
            var password = "hello!";

            var member = Builder<Member>.CreateNew()
                .With(x => x.Name = name)
                .With(x => x.Password = BCrypt.Net.BCrypt.HashPassword(password))
                .Build();

            MockRepo.Setup(x => x.Get(name)).Returns(member);

            //  act
            var results = SubjectUnderTest.VerifyLogin(name, password);

            //  assert

            Assert.IsTrue(results);

            MockRepo.VerifyAll();
        }

        [TestMethod]
        public void MemberService_Should_Login_Fails()
        {
            //  arrange
            var name = "abc";
            var password = "hello!";

            var member = Builder<Member>.CreateNew()
                .With(x => x.Name = name)
                .With(x => x.Password = BCrypt.Net.BCrypt.HashPassword("world!"))
                .Build();

            MockRepo.Setup(x => x.Get(name)).Returns(member);

            //  act
            var results = SubjectUnderTest.VerifyLogin(name, password);

            //  assert

            Assert.IsFalse(results);

            MockRepo.VerifyAll();
        }

        [TestMethod]
        public void MemberService_Should_Login_AutoAdds()
        {
            //  arrange
            var name = "abc";
            var password = "hello!";

            MockRepo.Setup(x => x.Get(name)).Returns(null as Member);

            MockRepo.Setup(x => x.Save(It.IsAny<Member>()));

            //  act
            var results = SubjectUnderTest.VerifyLogin(name, password);

            //  assert

            Assert.IsTrue(results);

            MockRepo.VerifyAll();
        }

        #endregion
    }
}