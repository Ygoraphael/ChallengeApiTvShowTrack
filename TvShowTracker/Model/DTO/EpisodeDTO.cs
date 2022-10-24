namespace TvShowTracker.Model
{
    public class EpisodeDTO
    {
        public int Id { get; set; }
        public int Chapter { get; set; }
        public string? Name { get; set; }
        public string? Overview { get; set; }
        public string? PosterPath { get; set; }
    }
}