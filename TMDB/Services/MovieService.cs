using Dapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MovieService : IMovieService
{
    private readonly IDbService _dbService;
    private readonly IWebHostEnvironment _environment;

    public MovieService(IDbService dbService, IWebHostEnvironment environment)
    {
        _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
        _environment = environment ?? throw new ArgumentNullException(nameof(environment));
    }

    // Add a new movie with optional poster image
    public async Task AddMovieAsync(Movie movie, IBrowserFile? posterImage)
    {
        if (movie == null) throw new ArgumentNullException(nameof(movie));

        try
        {
            // Save the image if provided
            if (posterImage != null)
            {
                movie.PosterPath = await SaveImageAsync(posterImage ,SetImageNameAndExtension(posterImage ,movie.Title!)); 
            }

            // Query to insert a new movie record
            var query = @"INSERT INTO public.movies 
                            (title, overview, rating, releasedate, runtime, language, country, isliked, posterpath, trailerurl) 
                          VALUES 
                            (@Title, @Overview, @Rating, @ReleaseDate, @Runtime, @Language, @Country, @IsLiked, @PosterPath, @TrailerUrl)";

            await _dbService.Insert<Movie>(query, movie);
            Console.WriteLine("Movie added successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding movie: {ex.Message}");
        }
    }

    // Retrieve all movies from the database
    public async Task<List<Movie>> GetAllMoviesAsync()
    {
        try
        {
            var movies = await _dbService.GetAll<Movie>("SELECT * FROM public.movies", new { });
            return movies;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving movies: {ex.Message}");
            return new List<Movie>();
        }
    }

    // Retrieve a movie by its ID
    public async Task<Movie?> GetMovieByIdAsync(int movieId)
    {
        try
        {
            var query = "SELECT * FROM public.movies WHERE movieid = @MovieId";
            var movie = await _dbService.GetAsync<Movie>(query, new { MovieId = movieId });
            return movie;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving movie by ID: {ex.Message}");
            return null;
        }
    }

    // Search for movies by title
    public async Task<List<Movie>> SearchMoviesAsync(string searchTerm)
    {
        try
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return await GetAllMoviesAsync();
            }

            var query = "SELECT * FROM public.movies WHERE title ILIKE '%' || @SearchTerm || '%'";
            var movies = await _dbService.GetAll<Movie>(query, new { SearchTerm = searchTerm });
            return movies;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error searching movies: {ex.Message}");
            return new List<Movie>();
        }
    }

    // Toggle the 'IsLiked' property of a movie
    private async Task ToggleLike(Movie movie)
    {
        try
        {
            if (movie == null) throw new ArgumentNullException(nameof(movie));

            movie.IsLiked = !movie.IsLiked;
            await UpdateMovieAsync(movie, null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error toggling like: {ex.Message}");
        }
    }

    // Update an existing movie
    public async Task UpdateMovieAsync(Movie movie, IBrowserFile? posterImage)
    {
        if (movie == null) throw new ArgumentNullException(nameof(movie));

        try
        {
            // If a new image is provided, delete the old one and save the new one
            if (posterImage != null)
            {
                DeleteImage(movie.PosterPath);
                
                movie.PosterPath = await SaveImageAsync(posterImage ,SetImageNameAndExtension(posterImage ,movie.Title!));
            }

            // Query to update the movie record
            var query = @"UPDATE public.movies 
                          SET title = @Title, 
                              overview = @Overview, 
                              rating = @Rating, 
                              releasedate = @ReleaseDate, 
                              runtime = @Runtime, 
                              language = @Language, 
                              country = @Country, 
                              isliked = @IsLiked, 
                              posterpath = @PosterPath, 
                              trailerurl = @TrailerUrl 
                          WHERE movieid = @MovieId";

            await _dbService.Update<Movie>(query, movie);
            Console.WriteLine("Movie updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating movie: {ex.Message}");
        }
    }

    // Delete a movie and its associated image
    public async Task DeleteMovieAsync(int movieId)
    {
        try
        {
            var movie = await GetMovieByIdAsync(movieId);
            if (movie != null)
            {
                DeleteImage(movie.PosterPath);

                var query = "DELETE FROM public.movies WHERE movieid = @MovieId";
                await _dbService.Delete<Movie>(query, new { MovieId = movieId });
                Console.WriteLine("Movie deleted successfully.");
            }
            else
            {
                Console.WriteLine("Movie not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting movie: {ex.Message}");
        }
    }

    // Get movies released between specified dates
    public async Task<List<Movie>> GetMoviesByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        try
        {
            var query = @"SELECT * FROM public.movies 
                          WHERE releasedate BETWEEN @StartDate AND @EndDate
                          ORDER BY releasedate";

            var movies = await _dbService.GetAll<Movie>(query, new { StartDate = startDate, EndDate = endDate });
            return movies;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving movies by date range: {ex.Message}");
            return new List<Movie>();
        }
    }

    // Save the image to the server
    private async Task<string> SaveImageAsync(IBrowserFile image,string imageNewName)
    {
        const long maxFileSize = 10 * 1024 * 1024; // 10 MB
        var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }
        var filePath = Path.Combine(uploadsFolder, imageNewName);

        try
        {
            // Save the file with a size limit
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.OpenReadStream(maxFileSize).CopyToAsync(fileStream);
            }

            return "/uploads/" + imageNewName;
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error saving image: {ex.Message}");
            return string.Empty; // Returning an empty path in case of failure
        }
    }

    // Delete the image from the server
    private void DeleteImage(string? imagePath)
    {
        if (!string.IsNullOrEmpty(imagePath))
        {
            var fullPath = Path.Combine(_environment.WebRootPath, imagePath.TrimStart('/'));

            try
            {
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    Console.WriteLine("Image deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting image: {ex.Message}");
            }
        }
    }

    private string SetImageNameAndExtension(IBrowserFile image ,string newName){

        string imageExtension= image.Name.Split(".")[1];
        return newName+"."+imageExtension;

    }
    // Get movies by their 'liked' status
    public async Task<List<Movie>> GetMoviesByLikeAsync(string isLiked)
    {
        try
        {
            var query = "SELECT * FROM public.movies WHERE isliked = @IsLiked";
            var parameters = new DynamicParameters();
            parameters.Add("IsLiked", bool.Parse(isLiked));

            var movies = await _dbService.GetAll<Movie>(query, parameters);
            return movies;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving liked movies: {ex.Message}");
            return new List<Movie>();
        }
    }
}
