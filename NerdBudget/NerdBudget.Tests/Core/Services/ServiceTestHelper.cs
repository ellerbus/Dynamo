using Augment.Caching;
using AutoMoq;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace NerdBudget.Tests.Core.Services
{
    public class ServiceTestHelper<TModel, TSvc, TRepo>
        where TModel : class
        where TRepo : class
    {
        protected AnyHelper<TModel> Any = new AnyHelper<TModel>();

        protected AutoMoqer Mocker { get; set; }

        protected Mock<TRepo> MockRepo { get; set; }

        protected Mock<IValidator<TModel>> MockValidator { get; set; }

        protected Mock<ICacheProvider> MockCache { get; set; }

        protected TSvc SubjectUnderTest { get; set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Mocker = new AutoMoqer();

            MockRepo = Mocker.GetMock<TRepo>();

            MockValidator = Mocker.GetMock<IValidator<TModel>>();

            MockCache = Mocker.GetMock<ICacheProvider>();

            Mocker.SetInstance<ICacheManager>(new CacheManager(MockCache.Object));

            SubjectUnderTest = Mocker.Create<TSvc>();
        }

        public ValidationResult ValidationSuccess { get { return new ValidationResult(); } }

        public ValidationResult ValidationFailure
        {
            get
            {
                ValidationFailure[] failures = new[] { new ValidationFailure("", "Oh NO!") };

                return new ValidationResult(failures);
            }
        }
    }
}
