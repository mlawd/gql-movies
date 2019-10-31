using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using GqlMovies.Api.Models;

namespace GqlMovies.Api.Services
{
    public enum GetTypesEnum
    {
        NOW_PLAYING, POPULAR, TOP_RATED, UPCOMING
    }

    public interface IMovieService
    {
        Task<Movie> GetAsync(int id);
        Task<Results<Movie>> ListAsync(GetTypesEnum type);
        Task<Results<Movie>> ListAsync(Dictionary<string, string> input);
    }
}