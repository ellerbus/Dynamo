using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using log4net;
using log4net.Config;

namespace ShortFuze.WebApp
{
    public class Global : HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start(object sender, EventArgs e)
        {
            XmlConfigurator.Configure();

            log.Info(new string('-', 30));

            // Code that runs on application startup
            GlobalConfiguration.Configure(WebApiConfig.Register);

            InjectorConfig.Register();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            ThreadContext.Properties["url"] = HttpContext.Current.Request.RawUrl.ToString();
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            // look if any security information exists for this request
            if (HttpContext.Current.User != null)
            {
                // see if this user is authenticated, any authenticated cookie (ticket) exists for this user
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    // see if the authentication is done using FormsAuthentication
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        // Get the roles stored for this request from the ticket
                        // get the identity of the user
                        FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;

                        log.InfoFormat("Creating Identity Name=[{0}]", identity.Name);

                        //Get the form authentication ticket of the user
                        FormsAuthenticationTicket ticket = identity.Ticket;

                        //Get the roles stored as UserData into ticket
                        List<string> roles = new List<string>();

                        //Create general prrincipal and assign it to current request
                        HttpContext.Current.User = new GenericPrincipal(identity, roles.ToArray());
                    }
                }
            }
        }
    }
}