using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TvShowTracker.Model
{
    public class Actor : Entity
    {
        public string? Name { get; set; }
        public string? Character { get; set; }
        public string? Picture { get; set; }
        public virtual ICollection<EpisodeActor> EpisodesActor { get; set; }
    }
}