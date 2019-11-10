using System.Collections.Generic;

namespace GqlMovies.Api.Models
{
    public class Results<T>
    {
        public IEnumerable<T> results { get; set; }
        public int Page { get; set; }

        public int total_results { private get; set; }
        public int TotalResults => total_results;

        public int total_pages { private get; set; }
        public int TotalPages => total_pages;


    }
}