public interface IMovieService
    {
        Task<List<Movie>> GetAllMoviesAsync();
        Task<Movie?> GetMovieByIdAsync(int movieId);
        Task<List<Movie>> SearchMoviesAsync(string searchTerm);
        Task AddMovieAsync(Movie movie);
        Task UpdateMovieAsync(Movie movie);
    }