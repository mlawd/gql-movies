using GraphQL;
using GraphQL.Types;

namespace GqlMovies.Api.Schemas
{
	public class MainSchema : Schema
	{
		public MainSchema(IDependencyResolver resolver): base(resolver)
		{
			Query = new Query();
		}
	}
}