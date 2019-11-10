using GraphQL.Types;

namespace GqlMovies.Api.Schemas
{
	public class Query : ObjectGraphType
	{
		public Query()
		{
			Name = "Query";

			Field<MovieQuery>( "movie", resolve: context => new{ });
		}
	}
}