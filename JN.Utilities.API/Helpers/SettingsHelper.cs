using System.Collections.Generic;
using JN.Utilities.API.Swagger;
using JN.Utilities.Core.Entities;
using Microsoft.Extensions.Configuration;

namespace JN.Utilities.API.Helpers
{
    public static class SettingsHelper
    {

        public static SwaggerOptionsConfig GetSwaggerConfig(this IConfiguration configuration, string sectionName)
        {
            var config = new SwaggerOptionsConfig();

            configuration.Bind(sectionName, config);

            return config;
        }


        public static IEnumerable<User> GetUsersConfig(this IConfiguration configuration, string sectionName)
        {
            var config = new List<User>();

            configuration.Bind(sectionName, config);

            return config;
        }


        public static string GetString(this IConfiguration configuration, string configItemName)
        {
            return configuration[configItemName];

        }

        public static int GetInt(this IConfiguration configuration, string configItemName)
        {
            int.TryParse(configuration[configItemName], out var res);

            return res;
        }

        public static short GetShort(this IConfiguration configuration, string configItemName)
        {
            short.TryParse(configuration[configItemName], out var res);

            return res;
        }

   

    }
}
