using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using Augment;

namespace ShortFuze.WebApp
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.Now);
            Response.Cache.SetValidUntilExpires(true);
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        }

        protected string GetScripts()
        {
            StringBuilder sb = new StringBuilder();

            if (Request.IsLocal)
            {
                DirectoryInfo root = new DirectoryInfo(Server.MapPath("~/App"));

                foreach (string file in GetScripts(root))
                {
                    string js = file.Replace(root.FullName, "").Replace('\\', '/');

                    sb.AppendFormat("  <script type='text/javascript' src='App{0}'></script>", js).AppendLine();
                }
            }
            else
            {
                string v = GetType().Assembly.GetName().Version.ToString();

                sb.AppendFormat("  <script type='text/javascript' src='Scripts/app.min.js?v={0}'></script>", v).AppendLine();
            }

            return sb.ToString();
        }

        protected string GetVersion()
        {
            Version v = typeof(Global).Assembly.GetName().Version;

            string a = "{0}.{1}".FormatArgs(v.Major, v.Minor);

            string b = "{0}.{1}.{2}.{3}".FormatArgs(v.Major, v.Minor, v.Build, v.Revision);

            DateTime c = new DateTime(2000, 1, 1)
                .AddDays(v.Build)
                .AddSeconds(v.Revision * 2);

            string cfg = "short: '{0}', full: '{1}', date: '{2: ddd MM/dd/yyyy HH:mm tt}'"
                .FormatArgs(a, b, c);

            return "{" + cfg + "}";
        }

        private IEnumerable<string> GetScripts(DirectoryInfo dir)
        {
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                foreach (string s in GetScripts(d))
                {
                    yield return s;
                }
            }

            foreach (FileInfo f in dir.GetFiles("*.js"))
            {
                yield return f.FullName;
            }
        }
    }
}