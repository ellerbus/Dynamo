using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using Augment.Caching;
using Insight.Database;
using log4net;
using ShortFuze.Core;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace ShortFuze.WebApp
{
    public static class InjectorConfig
    {
        #region Members

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static Container _container = new Container();

        #endregion

        #region Methods

        public static void Register()
        {
            _container.RegisterWebApiRequest<ICacheProvider, HttpRuntimeCacheProvider>();
            _container.RegisterWebApiRequest<ICacheManager, CacheManager>();
            _container.RegisterWebApiRequest<ISecurityActor>(() => GetSecurityActor());
            _container.RegisterWebApiRequest<IDbConnection>(() => GetConnection(), true);

            //  members
            //_container.RegisterWebApiRequest<I BASECLASS Repository>(() => GetRepository< I BASECLASS Repository> (), true);
            //_container.RegisterWebApiRequest<I BASECLASS Service, BASECLASS Service> ();
            //_container.RegisterSingleton<IValidator< BASECLASS >, BASECLASS Validator>();

            _container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(_container);
        }

        public static ISecurityActor GetSecurityActor()
        {
            IPrincipal p = new GenericPrincipal(new GenericIdentity("(anonymous)"), null);

            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    p = HttpContext.Current.User;
                }
                else
                {
                    p = new GenericPrincipal(new GenericIdentity("(no auth)"), null);
                }
            }

            return new SecurityActor(p.Identity.Name);
        }

        private static IDbConnection GetConnection()
        {
            log.Debug("Accessing Database");

            ConnectionStringSettings css = ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>().First();

            IDbConnection con = css.OpenAs<IDbConnection>();

            log.DebugFormat("Accessed Database=[{0}]", con.Database);

            return con;
        }

        private static T GetRepository<T>() where T : class
        {
            log.DebugFormat("Creating Repository=[{0}]", typeof(T).Name);

            IDbConnection con = _container.GetInstance<IDbConnection>();

            return con.As<T>();
        }

        #endregion
    }
}