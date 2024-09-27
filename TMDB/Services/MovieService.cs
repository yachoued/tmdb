using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
public class MovieService : IMovieService
    {
        private readonly IDbService _dbService;

        public MovieService(IDbService dbService)
        {
            _dbService = dbService;
    }

    public Task AddMovieAsync(Movie movie)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Movie>> GetAllMoviesAsync()
    {
        var movies = await _dbService.GetAll<Movie>("SELECT * FROM public.movies", new { });
        return movies;
    }

    public Task<Movie?> GetMovieByIdAsync(int movieId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Movie>> SearchMoviesAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public Task UpdateMovieAsync(Movie movie)
    {
        throw new NotImplementedException();
    }
}