using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackGroundServiceForProductTermsControl
{
    public static class Serilogging
    {
        public static void SerilogInitial(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration, "Serilog")
            //.MinimumLevel.Information()
            .WriteTo.MySQL(connectionString: configuration.GetConnectionString("WebApiDatabase"))
            .CreateLogger();
        }
    }
}
