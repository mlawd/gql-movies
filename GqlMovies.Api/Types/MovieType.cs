using GqlMovies.Api.Models;
using GraphQL.Types;

namespace GqlMovies.Api.Types
{
	public class MovieType : ObjectGraphType<Movie>
	{
		public MovieType()
		{
			Field(m => m.Id);
			Field(m => m.Adult);
			Field(m => m.Budget);
			Field(m => m.Title);
			Field(m => m.Tagline);
			Field(m => m.Popularity);
			Field(m => m.VoteAverage);
			Field(m => m.PosterPath);
			Field(m => m.ReleaseDate);
		}
	}
}