using System.Linq;
using System;
using System.Net.Http;
using System.Collections.Generic;
using GqlMovies.Api.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.WebUtilities;
using GqlMovies.Api.Types;

namespace GqlMovies.Api.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;

        public MovieService(HttpClient client, IConfiguration config)
        {
            _client = client;
            _apiKey = config["TMDB_API_KEY"];

            if (_apiKey == null)
            {
                throw new KeyNotFoundException("TMDB_API_KEY not defined");
            }
        }

        public async Task<Movie> GetAsync(int id)
        {
            var query = new Dictionary<string, string>
            {
                { "api_key", _apiKey }
            };

            var resp = await _client.GetAsync(
                QueryHelpers.AddQueryString($"https://api.themoviedb.org/3/movie/{id}", query)
            );

            return JsonConvert.DeserializeObject<Movie>(await resp.Content.ReadAsStringAsync());
        }

        public async Task<Results<Movie>> ListAsync(GetTypesEnum type)
        {
            var query = new Dictionary<string, string>
            {
                { "api_key", _apiKey }
            };

            string uri = QueryHelpers.AddQueryString($"https://api.themoviedb.org/3/movie/{type.ToString().ToLower()}", query);

            Console.WriteLine($"Requesting {uri}");

            var resp = await _client.GetAsync(uri);

            return JsonConvert.DeserializeObject<Results<Movie>>(await resp.Content.ReadAsStringAsync());
        }

        public async Task<Results<Movie>> ListAsync(Dictionary<string, string> query)
        {
            query.Add("api_key", _apiKey);

            var resp = await _client.GetAsync(
                QueryHelpers.AddQueryString($"https://api.themoviedb.org/3/search/movie", query)
            );

            var obj = JsonConvert.DeserializeObject<Results<Movie>>(await resp.Content.ReadAsStringAsync());
            return obj;
        }
    }
}