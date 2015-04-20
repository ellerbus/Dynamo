using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Augment.Caching;
using FluentValidation;
using Insight.Database;
using NerdBudget.Core.Models;
using NerdBudget.Core.Repositories;
using NerdBudget.Core.Services;
using NerdBudget.Core.Validators;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;

namespace NerdBudget.Web
{
    public static class InjectorConfig
    {
        #region Members

        private static Container container = new Container();

        #endregion

        #region Methods

        public static void Register()
        {
            container.Register<ICacheProvider, HttpRuntimeCacheProvider>();
            container.Register<ICacheManager, CacheManager>();

            container.RegisterPerWebRequest<IDbConnection>(() => GetConnection(), true);

            container.RegisterPerWebRequest<IMemberRepository>(() => GetRepository<IMemberRepository>(), true);
            container.Register<IMemberService, MemberService>();

            container.RegisterPerWebRequest<IAccountRepository>(() => GetRepository<IAccountRepository>(), true);
            container.Register<IAccountService, AccountService>();
            container.Register<IValidator<Account>, AccountValidator>();

            container.RegisterPerWebRequest<ICategoryRepository>(() => GetRepository<ICategoryRepository>(), true);
            container.Register<ICategoryService, CategoryService>();
            container.Register<IValidator<Category>, CategoryValidator>();

            container.RegisterPerWebRequest<IBudgetRepository>(() => GetRepository<IBudgetRepository>(), true);
            container.Register<IBudgetService, BudgetService>();
            container.Register<IValidator<Budget>, BudgetValidator>();

            container.RegisterPerWebRequest<IAdjustmentRepository>(() => GetRepository<IAdjustmentRepository>(), true);
            container.Register<IAdjustmentService, AdjustmentService>();
            container.Register<IValidator<Adjustment>, AdjustmentValidator>();

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // This is an extension method from the integration package as well.
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static IDbConnection GetConnection()
        {
            ConnectionStringSettings css = ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>().First();

            return css.OpenAs<IDbConnection>();
        }

        private static T GetRepository<T>() where T : class
        {
            IDbConnection con = container.GetInstance<IDbConnection>();

            return con.As<T>();
        }

        #endregion
    }
}