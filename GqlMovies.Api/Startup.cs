using GraphQL.Server.Ui.Playground;
using GraphQL.Server.Ui.Voyager;
using GqlMovies.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GqlMovies.Api.Schemas;
using GraphQL;
using GqlMovies.Api.Types;
using GqlMovies.Api.Models;

namespace GqlMovies.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IDependencyResolver>(
				s => new FuncDependencyResolver(s.GetRequiredService)
			);
			services.AddHttpClient<IMovieService, MovieService>();
			services.AddSingleton<MovieQuery>();
			services.AddSingleton<MovieType>();
			services.AddSingleton<ResultsType<MovieType, Movie>>();
			services.AddSingleton<MainSchema>();
			services.AddCors(o => o.AddPolicy("MyPolicy", p =>
			{
				p.AllowAnyHeader();
				p.AllowAnyMethod();
				p.AllowAnyOrigin();
			}));

			services.AddControllers().AddNewtonsoftJson();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHttpsRedirection();
			}

			app
			.UseCors("MyPolicy")
			.UseWebSockets()
			.UseGraphQLPlayground(new GraphQLPlaygroundOptions() { Path = "/" })
			.UseGraphQLVoyager(new GraphQLVoyagerOptions() { Path = "/voyager" });


			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
