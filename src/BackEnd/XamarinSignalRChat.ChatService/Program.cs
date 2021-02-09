// <copyright file="Program.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.ChatService
{
	using System.Net;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Logging;

	/// <summary>On startup the app builds a host.  The host encapsulates all of the application resources.</summary>
	public class Program
	{
		/// <summary>This method gets called by the runtime. Use this method to configure a host with a set of default options.</summary>
		/// <param name="args">Creation arguments.</param>
		/// <returns>IHostBuilder interface.</returns>
		public static IHostBuilder CreateWebHostBuilder(string[] args) =>
		   Host.CreateDefaultBuilder(args)
		   .ConfigureLogging(logging =>
		   {
			   logging.ClearProviders();
			   logging.AddConsole();
		   })
		   .ConfigureWebHostDefaults(webBuilder =>
		   {
			   webBuilder.ConfigureKestrel((context, options) =>
			   {
#if DEBUG
				   options.Listen(IPAddress.Loopback, 5000);
#endif
			   })
			   .UseStartup<Startup>();
		   });

		/// <summary>This method gets called by the runtime. Use this method to build a .NET generic host.</summary>
		/// <param name="args">Application arguments.</param>
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}
	}
}