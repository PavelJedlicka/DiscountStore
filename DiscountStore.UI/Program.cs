// Copyright (c) PavelJedlicka. All rights reserved.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DiscountStore.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
