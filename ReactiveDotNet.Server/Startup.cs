using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ReactiveDotNet.Contracts;

namespace ReactiveDotNet.Server
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddSignalR();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();
			app.UseSignalR(routes =>
			{
				routes.MapHub<KeyWatcherHub>(Common.KeyWatcherPartialUri);
			});
		}
	}
}
