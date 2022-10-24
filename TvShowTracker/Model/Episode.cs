namespace TvShowTracker.Model
{
    public class Episode : Entity
    {
        public int? Chapter { get; set; }
        public string? Name { get; set; }
        public string? Overview { get; set; }
        public string? PosterPath { get; set; }
        public int SeasonId { get; set; }
        public virtual Season Season { get; set; }
        public virtual ICollection<EpisodeActor> EpisodeActors { get; set; }
    }
}