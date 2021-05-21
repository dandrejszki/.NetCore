using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace testEnv
{
  public class TestSectionModel
  {
    private readonly IConfiguration Config;

    public TestSectionModel(IConfiguration configuration)
    {
      Config = configuration.GetSection("section1");
    }

    public String OnGet()
    {
      return (
              $"section1:key0: '{Config["key0"]}'\n" +
              $"section1:key1: '{Config["key1"]}'");
    }
  }
}
