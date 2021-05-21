using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using System.IO;
using static System.IO.Directory;
using static System.IO.Path;


namespace testEnv.Models
{
  public class User
  {

    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
  }

}