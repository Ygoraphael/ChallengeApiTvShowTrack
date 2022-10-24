using System.ComponentModel.DataAnnotations;

namespace TvShowTracker.Model
{
    public class FavoriteTvShow
    {
        [Key]
        public int Id { get; set; }
        public int TvShowId { get; set; }
        public virtual TvShow TvShow { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}