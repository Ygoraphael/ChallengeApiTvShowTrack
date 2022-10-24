using System.ComponentModel.DataAnnotations;

namespace TvShowTracker.Model
{
    public class EpisodeActor
    {
        [Key]
        public int id { get; set; }
        public int EpisodeId { get; set; }
        public virtual Episode Episode { get; set; }
        public int ActorId { get; set; }
        public virtual Actor Actor { get; set; }
    }
}