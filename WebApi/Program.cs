using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Owin;
using static System.Net.WebRequestMethods;

namespace WebApi
{
    // The source for seting up a Webapi as a Console application:
    // * https://anthonychu.ca/post/web-api-owin-self-host-docker-windows-containers/
    // * https://learn.microsoft.com/en-us/aspnet/web-api/overview/security/enabling-cross-origin-requests-in-web-api

    internal class Program
    {
        static void Main(string[] args)
        {
            var name = nameof(WebApi);
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            Console.Title = $"{name} {version}";

            var baseAddress = "http://localhost:9000/";

            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Thread.Sleep(Timeout.Infinite);
            }
        }
    }
}
