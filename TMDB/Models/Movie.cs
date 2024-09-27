public class Movie
{
    public int MovieId { get; set; }
    public required string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public bool IsLiked { get; set; }
}