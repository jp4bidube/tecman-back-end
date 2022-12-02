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
        public static void Main(string[] args){
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }
        );
    }
}
