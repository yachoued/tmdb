using System.ComponentModel.DataAnnotations;

public class Movie
{
    public int MovieId { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
    public string? Title { get; set; }

    [StringLength(1000, ErrorMessage = "Overview cannot be longer than 1000 characters.")]
    public string? Overview { get; set; }

    [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10.")]
    public decimal Rating { get; set; }

    [Required(ErrorMessage = "Release Date is required.")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    [Required(ErrorMessage = "Runtime is required.")]
    [Range(1, 600, ErrorMessage = "Runtime must be between 1 and 600 minutes.")]
    public int? Runtime { get; set; }
    public string? Language { get; set; } 
    public string? Country { get; set; }
    public bool IsLiked { get; set; }

    // [Required(ErrorMessage = "Poster image is required.")]
    public string? PosterPath { get; set; }

    [Url(ErrorMessage = "Invalid URL format.")]
    public string? TrailerUrl { get; set; }
}