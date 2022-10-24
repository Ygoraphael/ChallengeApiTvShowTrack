namespace TvShowTracker.Model
{
    public class Season : Entity
    {
        public string? Name { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeCount { get; set; }
        public string? Overview { get; set; }
        public string? PosterPath { get; set; }
        public int TvShowId { get; set; }
        public virtual TvShow TvShow { get; set; }
        public virtual ICollection<Episode> Episodes { get; set; }
    }
}