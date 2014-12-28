using System.Configuration;
using System.Data;
using System.Web.Mvc;
using FluentValidation;
using Insight.Database;
using Sample.Core.Models;
using Sample.Core.Repositories;
using Sample.Core.Services;
using Sample.Core.Validators;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace Sample.WebMvc
{
    public static class InjectorConfig
    {
        public static void Register()
        {
            Container c = new Container();

            c.Register<IMemberRepository>(() => GetRepository<IMemberRepository>(), Lifestyle.Transient);
            c.Register<IMemberService, MemberService>();
            c.Register<IValidator<Member>, MemberValidator>();

            c.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(c));
        }

        private static T GetRepository<T>() where T : class
        {
            ConnectionStringSettings css = ConfigurationManager.ConnectionStrings["DB"];

            IDbConnection con = css.OpenAs<IDbConnection>();

            return con.As<T>();
        }
    }
}