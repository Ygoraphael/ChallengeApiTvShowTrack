namespace TvShowTracker.Model
{
    public class TvShow : Entity
    {
        public string Name { get; set; }
        public string? Overview { get; set; }
        public double? Popularity { get; set; }
        public double? Vote_average { get; set; }
        public double? vote_count { get; set; }
        public virtual ICollection<FavoriteTvShow> FavoriteTvShows { get; set; }
        public virtual ICollection<Season> Seasons { get; set; }
        public virtual ICollection<TvShowGenre> TvShowGenres { get; set; }
    }
}