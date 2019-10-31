namespace GqlMovies.Api.Models
{
	public class Movie
	{
		public int Id { get; set; }
		public bool Adult { get; set; }
		public int Budget { get; set; }
		public string Title { get; set; }
		public string Tagline { get; set; }
		public double Popularity { get; set; }

		public double vote_average { private get; set; }
		public double VoteAverage => vote_average;

		public string poster_path { private get; set; }
		public string PosterPath => poster_path;

		public string release_date { private get; set; }
		public string ReleaseDate => release_date;

		public string imdb_id { private get; set; }
		public string ImdbId => imdb_id;
	}
}