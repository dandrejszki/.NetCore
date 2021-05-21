using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.IO;
using testEnv.Models;
using Microsoft.EntityFrameworkCore;

namespace testEnv
{
  public class Startup
  {
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
      Configuration = configuration;
      _env = env;
    }

    public IConfiguration Configuration { get; }
    private IWebHostEnvironment _env { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {

      services.AddControllers();
      services.AddMvc();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "testEnv", Version = "v1" });
      });

      services.AddSingleton<IConfiguration>(Configuration);

      //string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NodeCourse;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;user id=test_login;password= pass1234";
      string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NodeCourse;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;user id=test_login;password= pass1234";

      services.AddDbContext<MuleDBContext>(options =>
                options.UseSqlServer(connectionString));

      //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


      // services.AddHttpClient("HttpClientName", client =>
      // {
      //   // code to configure headers etc..
      // }).ConfigurePrimaryHttpMessageHandler(() =>
      // {
      //   var handler = new HttpClientHandler();
      //   if (!_env.IsDevelopment())
      //   {
      //     //Console.Write(CurrentEnvironment.EnvironmentName);
      //     handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
      //     // Directory.GetCurrentDirectory()
      //    // string rootPath = Combine(CurrentDirectory, "certs", "otherCert.cer");

      //   }
      //   return handler;
      // });


    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration config)
    {
      //   if (env.IsDevelopment())
      //   {
      //     app.UseDeveloperExceptionPage();
      //     app.UseSwagger();
      //     app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "testEnv v1"));
      //   }

      //   app.UseHttpsRedirection();

      app.UseStaticFiles();

      var value = config["JAVA_HOME"];

      app.UseRouting();

      app.UseHsts();

      //   app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      app.Run(async context =>
      {
        await context.Response.WriteAsync("Hello World " + env.EnvironmentName + value);
      });

    }
  }
}
