using GqlMovies.Api.Models;
using GqlMovies.Api.Types;
using GqlMovies.Api.Services;
using GraphQL.Types;
using System.Collections.Generic;
using System.Reflection;

namespace GqlMovies.Api.Schemas
{
	public class MovieQuery : ObjectGraphType
	{
		public MovieQuery(IMovieService service)
		{
			FieldAsync<MovieType, Movie>(
				"single",
				arguments: new QueryArguments(
					new QueryArgument<IntGraphType> { Name = "id" }
				),
				resolve: context =>
				{
					var id = context.GetArgument<int>("id");
					return service.GetAsync(id);
				}
			);

			FieldAsync<ResultsType<MovieType, Movie>, Results<Movie>>(
				"search",
				arguments: new QueryArguments(
					new QueryArgument<StringGraphType> { Name = "query" }
				),
				resolve: context =>
				{
					var obj = new Dictionary<string, string>();
					
					var query = context.GetArgument<string>("query");

					if (query != null) obj.Add("query", query);

					return service.ListAsync(obj);
				}
			);

			FieldAsync<ResultsType<MovieType, Movie>, Results<Movie>>(
				"nowPlaying",
				resolve: context => service.ListAsync(GetTypesEnum.NOW_PLAYING)
			);

			FieldAsync<ResultsType<MovieType, Movie>, Results<Movie>>(
				"popular",
				resolve: context => service.ListAsync(GetTypesEnum.POPULAR)
			);

			FieldAsync<ResultsType<MovieType, Movie>, Results<Movie>>(
				"topRated",
				resolve: context => service.ListAsync(GetTypesEnum.TOP_RATED)
			);

			FieldAsync<ResultsType<MovieType, Movie>, Results<Movie>>(
				"upcoming",
				resolve: context => service.ListAsync(GetTypesEnum.UPCOMING)
			);
		}
	}
}