using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Waes.Core;

namespace Waes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using(var ctx = new WaesContext())
            {
                ctx.Database.Migrate();
            }
            CreateWebHostBuilder(args).Build().Run();
            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
