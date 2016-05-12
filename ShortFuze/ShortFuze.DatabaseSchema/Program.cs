using System;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Insight.Database.Schema;

namespace ShortFuze.DatabaseSchema
{
    class Program
    {
        /// <summary>
        /// Code pulled from Insight.Schema's Project Sample
        /// </summary>
        /// <param name="args"></param>
        /// <remarks>
        /// https://github.com/jonwagner/Insight.Database.Schema
        /// </remarks>
        static void Main(string[] args)
        {
            string connectionString = GetConnectionStringSettings().ConnectionString;

            if (!SchemaInstaller.DatabaseExists(connectionString))
            {
                SchemaInstaller.CreateDatabase(connectionString);
            }

            using (DbConnection connection = GetOpenConnection())
            {
                // make sure our database exists
                SchemaInstaller installer = new SchemaInstaller(connection);

                new SchemaEventConsoleLogger().Attach(installer);

                // load the schema from the embedded resources in this project
                SchemaObjectCollection schema = new SchemaObjectCollection();

                schema.Load(Assembly.GetExecutingAssembly());

                string schemaGroup = "ShortFuze";

                // install the schema
                Console.WriteLine("Installing");

                installer.Install(schemaGroup, schema);

                // uninstall the schema
                if (args.Length > 0 && args[0].ToUpperInvariant() == "UNINSTALL")
                {
                    Console.WriteLine("Uninstalling");
                    installer.Uninstall(schemaGroup);
                }
            }
        }

        private static ConnectionStringSettings GetConnectionStringSettings()
        {
            ConnectionStringSettings css = ConfigurationManager
                .ConnectionStrings
                .Cast<ConnectionStringSettings>().First();

            return css;
        }

        private static DbConnection GetOpenConnection()
        {
            ConnectionStringSettings css = GetConnectionStringSettings();

            DbProviderFactory factory = DbProviderFactories.GetFactory(css.ProviderName);

            DbConnection con = factory.CreateConnection();

            con.ConnectionString = css.ConnectionString;

            con.Open();

            return con;
        }
    }
}
