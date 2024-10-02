using Microsoft.AspNetCore.Components.Forms;

public interface IMovieService
    {
        Task<List<Movie>> GetAllMoviesAsync();
        Task<Movie?> GetMovieByIdAsync(int movieId);
        Task<List<Movie>> SearchMoviesAsync(string searchTerm);
        Task AddMovieAsync(Movie movie,IBrowserFile? posterImage);
        Task UpdateMovieAsync(Movie movie , IBrowserFile? posterImage);
        Task DeleteMovieAsync(int movieId);
        Task<List<Movie>> GetMoviesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<List<Movie>> GetMoviesByLikeAsync(string is_liked);

    }