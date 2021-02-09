// <copyright file="Startup.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.ChatService
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using XamarinSignalRChat.ChatService.Hubs;

	/// <summary>Service startup where services required by the app are configured and the request handling pipeline is defined as a series of middleware components.</summary>
	public class Startup
	{
		/// <summary>Initialises a new instance of the <see cref="Startup" /> class.</summary>
		/// <param name="configuration">Service configuration.</param>
		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		/// <summary>Gets the service configuration.</summary>
		public IConfiguration Configuration { get; }

		/// <summary>This method gets called by the runtime. Use this method to configure the HTTP request pipeline.</summary>
		/// <param name="app">Application builder.</param>
		/// <param name="env">Web host environment.</param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseRouting();

			app.UseCors("CorsPolicy");
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHub<ChatHub>("/hubs/chat");
			});
		}

		/// <summary>This method gets called by the runtime. Use this method to add services to the container.</summary>
		/// <param name="services">Service collection.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddSignalR();
			services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
			{
				builder.AllowAnyMethod()
					.AllowAnyHeader()
					.WithOrigins("http://localhost:5002");
			}));
		}
	}
}