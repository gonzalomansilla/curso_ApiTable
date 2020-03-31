using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace curso_ApiTable
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateHostBuilder(string[] args)
		{
			return WebHost.CreateDefaultBuilder(args)
				.ConfigureLogging((hostingContext, logging) => {
					logging.ClearProviders();
					logging.SetMinimumLevel(LogLevel.Warning);
					logging.AddConsole();
				})
				.UseStartup<Startup>();
		}

	}
}
