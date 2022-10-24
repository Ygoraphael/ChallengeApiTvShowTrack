using System.ComponentModel.DataAnnotations;
namespace TvShowTracker.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password  { get; set; }
        public virtual ICollection<FavoriteTvShow> FavoritesTvShows { get; set; }
    }
}