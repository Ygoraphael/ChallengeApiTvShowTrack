using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TvShowTracker.Model
{
    public class Genre : Entity
    {
        public string? Name { get; set; }
        public virtual ICollection<TvShowGenre> TvShowGenres { get; set; }
    }
}