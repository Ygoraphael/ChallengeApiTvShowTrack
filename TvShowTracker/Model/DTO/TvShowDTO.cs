namespace TvShowTracker.Model
{
    public class TvShowDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Overview { get; set; }
        public double? Popularity { get; set; }
        public double? Vote_average { get; set; }
        public double? vote_count { get; set; }
    }
}