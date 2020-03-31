using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model.Context;
using Services.Interfaces;
using Services.Services;

namespace curso_ApiTable
{
	public class Startup
	{
		private readonly string PoliticaCors = "__curso__";

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			//**** Mi config ****
			services.AddCors(options => {
				options.AddPolicy(PoliticaCors, builder => {
					builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
				});
			});

			//Services
			services.AddScoped<ITableService, TableService>();

			string connectionStringNotebook = this.Configuration.GetConnectionString("notebookDb");
			string connectionStringPc = this.Configuration.GetConnectionString("notebookDb");
			services.AddDbContext<MyContext>(options => options.UseSqlServer(connectionStringNotebook));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyContext context)
		{
			context.Database.EnsureCreated();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseCors(PoliticaCors);

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
