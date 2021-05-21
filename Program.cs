using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using testEnv.Models;

namespace testEnv
{
  public class Program
  {
    public static void Main(string[] args)
    {
      //   var host = CreateHostBuilder(args).Build();

      //   var config = host.Services.GetRequiredService<IConfiguration>();

      //   foreach (var c in config.AsEnumerable())
      //   {
      //     Console.WriteLine(c.Key + " = " + c.Value);
      //   }
      //   host.Run();

      var host = CreateHostBuilder(args).Build();

      var config = host.Services.GetRequiredService<IConfiguration>();

      TestSectionModel valami = new TestSectionModel(config);
      Console.WriteLine(valami.OnGet());
      //Console.WriteLine(config.GetValue<string>("MULE_HOME")); //gets env variable
      Console.WriteLine(config["MULE_HOME"]);
      host.Run();

      // using (var context = new MuleDBContext())
      // {
      //   Console.WriteLine(context.Users.Count());
      //   context.Users.Add(new User { name = "Teszt", email = "dummy" }); //ha kiszedem a Firstname = vagy : írok és elromlik
      // }

    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
              config.AddEnvironmentVariables()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("MySubsection.json",
                    optional: true,
                    reloadOnChange: true);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
