using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Http;
using Augment.Caching;
using FluentValidation;
using Insight.Database;
using NerdBudget.Core.Models;
using NerdBudget.Core.Repositories;
using NerdBudget.Core.Services;
using NerdBudget.Core.Validators;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace NerdBudget.Web
{
    public static class InjectorConfig
    {
        public static void Register()
        {
            Container c = new Container();

            c.Register<ICacheProvider, HttpRuntimeCacheProvider>();
            c.Register<ICacheManager, CacheManager>();

            c.RegisterWebApiRequest<IAccountRepository>(() => GetRepository<IAccountRepository>(), true);
            c.Register<IAccountService, AccountService>();
            c.Register<IValidator<Account>, AccountValidator>();

            c.RegisterWebApiRequest<ICategoryRepository>(() => GetRepository<ICategoryRepository>(), true);
            c.Register<ICategoryService, CategoryService>();
            c.Register<IValidator<Category>, CategoryValidator>();

            c.RegisterWebApiRequest<IBudgetRepository>(() => GetRepository<IBudgetRepository>(), true);
            c.Register<IBudgetService, BudgetService>();
            c.Register<IValidator<Budget>, BudgetValidator>();

            c.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(c);
        }

        private static T GetRepository<T>() where T : class
        {
            ConnectionStringSettings css = ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>().First();

            IDbConnection con = css.OpenAs<IDbConnection>();

            return con.As<T>();
        }
    }
}