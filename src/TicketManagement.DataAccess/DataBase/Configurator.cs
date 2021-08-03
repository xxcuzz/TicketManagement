using Microsoft.Extensions.Configuration;

namespace TicketManagement.DataAccess.DataBase
{
    public static class Configurator
    {
        private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
            .AddXmlFile("appsettings.config", optional: true, reloadOnChange: true)
            .Build();

        public static string GetConnString()
        {
            return Configuration["connectionString:value"];
        }

        /// <summary>
        /// Takes connection string of the test database.
        /// </summary>
        public static string GetTestConnString()
        {
            return Configuration["testConnectionString:value"];
        }
    }
}
