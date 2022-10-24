using System.ComponentModel.DataAnnotations;
namespace TvShowTracker.Model
{
    public class TvShowGenre
    {
        [Key]
        public int Id { get; set; }
        public int TvShowId { get; set; }
        public virtual TvShow TvShow { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}