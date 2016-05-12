using Augment.Caching;
using AutoMoq;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShortFuze.Core;

namespace ShortFuze.Tests.Core.Services
{
    [TestClass]
    public class BaseServiceTests<TModel, TRepo, TService>
          where TModel : class
          where TRepo : class
          where TService : class
    {
        #region Helpers/Test Initializers

        protected AutoMoqer Mocker;
        protected Mock<ICacheProvider> MockCache;
        protected Mock<ISecurityActor> MockActor;
        protected Mock<TRepo> MockRepo;
        protected Mock<IValidator<TModel>> MockValidator;
        protected TService SubjectUnderTest;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Mocker = new AutoMoqer();

            MockActor = Mocker.GetMock<ISecurityActor>();

            MockActor.Setup(x => x.UserName).Returns("tester");

            MockRepo = Mocker.GetMock<TRepo>();

            MockValidator = Mocker.GetMock<IValidator<TModel>>();

            MockCache = Mocker.GetMock<ICacheProvider>();

            Mocker.SetInstance<ICacheManager>(new CacheManager(MockCache.Object));

            SubjectUnderTest = Mocker.Create<TService>();
        }

        protected void SetupCacheMiss<T>() where T : class
        {
            MockCache.Setup(x => x.Get(Any.String)).Returns(null as T);
        }

        protected void SetupCacheHit<T>(T cachedItem) where T : class
        {
            MockCache.Setup(x => x.Get(Any.String)).Returns(cachedItem);
        }

        #endregion
    }

}
