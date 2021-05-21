using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.IO;
using static System.IO.Directory;
using static System.IO.Path;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

using testEnv.Models;

namespace testEnv.Controllers
{
  [ApiController]
  [Route("api")] // /[controller]
  public class HelloController : ControllerBase
  {

    private readonly IConfiguration Configuration;
    private readonly MuleDBContext _context;

    //private readonly IHttpClientFactory _clientFactory;

    public HelloController(IConfiguration configuration, MuleDBContext context)//, IHttpClientFactory clientFactory)
    {
      Configuration = configuration;
      // _clientFactory = clientFactory;
      _context = context;

    }


    [HttpGet]
    public ActionResult<string> sayHello()
    {
      var myKeyValue = Configuration["afwa"];
      var Config = Configuration.GetSection("section1");


      { return "Hello World " + myKeyValue + Configuration["test"]; }
    }

    //https://localhost:5001/api/hello/valami
    [HttpGet("valami")]
    public async Task<ActionResult<string>> Valami()
    {

      //await ProcessRepositories();
      Crypto mycripto = new Crypto("");
      string encrypted = mycripto.encrypt("Hello world");
      Console.WriteLine(encrypted);
      string decrypted = mycripto.decrypt(encrypted);
      Console.WriteLine(decrypted);

      return "valami";
    }

    [HttpGet("database")]
    public async Task<ActionResult<string>> callDB()
    {
      Console.WriteLine(_context.Users.Count());
      _context.Users.Add(new User { name = "Teszt", email = "dummy" }); //ha kiszedem a Firstname = vagy : írok és elromlik
      _context.SaveChanges();
      return "called DB";

    }

    [HttpGet("certs")]
    public async Task<ActionResult<string>> certs()
    {
      var store = new X509Store(StoreLocation.CurrentUser); //StoreLocation.LocalMachine fails too
      store.Open(OpenFlags.ReadOnly);
      var certificates = store.Certificates;
      foreach (var certificate in certificates)
      {
        var friendlyName = certificate.FriendlyName;
        Console.WriteLine(friendlyName);
      }
      store.Close();
      return "certs";
    }

    private async Task ProcessRepositories()
    {
      //var client = _clientFactory.CreateClient("fwafa");

      string rootPath = Combine(Environment.CurrentDirectory, "certs", "otherCert.cer");

      var handler = new HttpClientHandler()
      {
        SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
      };

      X509Certificate2 certificate = new X509Certificate2(rootPath);
      X509Certificate2Collection collection = new X509Certificate2Collection();
      collection.Add(certificate);

      //handler.ServerCertificateCustomValidationCallback = Test.CreateCustomRootValidator(collection);

      String result = Test.valami();
      //Console.WriteLine(result);

      var client = new HttpClient(handler);



      //var handler = new HttpClientHandler();

      //handler.ClientCertificates.Add();

      //handler.ServerCertificateCustomValidationCallback = ServerCertificateCustomValidation;


      var stringTask = client.GetStringAsync("https://api.chucknorris.io/jokes/random");

      var msg = await stringTask;
      Console.Write(msg);
    }



  }
  public class Test
  {

    // public static Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> CreateCustomRootValidator(X509Certificate2Collection trustedRoots, X509Certificate2Collection intermediates = null)
    // {
    //   RemoteCertificateValidationCallback callback = Test.CreateCustomRootRemoteValidator(trustedRoots, intermediates);
    //   return (message, serverCert, chain, errors) => callback(null, serverCert, chain, errors);
    // }

    public static string valami()
    {
      return "jó anyádat";
    }
  }
}
