using System;
/**
 * Created: 
 * Date: 
 * Modified: Daniel Quintal
 * Date: January, 31, 2022
 *
 * Application main program - BACK
 * 
 */

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Tecman
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      var port = Environment.GetEnvironmentVariable("PORT");
      return Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseStartup<Startup>();
          webBuilder.UseUrls($"http://0.0.0.0:{port};http://localhost:5000");
        }
      );
    }
  }
}
