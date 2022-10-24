using Microsoft.EntityFrameworkCore;
using TvShowTracker.Model;
namespace TvShowTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TvShow>()
                    .HasMany(a => a.Seasons)
                    .WithOne(s => s.TvShow)
                    .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TvShow>()
                    .HasMany(a => a.FavoriteTvShows)
                    .WithOne(f => f.TvShow)
                    .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TvShow>()
                    .HasMany(a => a.TvShowGenres)
                    .WithOne(f => f.TvShow)
                    .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Season>()
                    .HasMany(a => a.Episodes)
                    .WithOne(f => f.Season)
                    .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Episode>()
                    .HasMany(a => a.EpisodeActors)
                    .WithOne(f => f.Episode)
                    .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Actor>()
                    .HasMany(a => a.EpisodesActor)
                    .WithOne(f => f.Actor)
                    .OnDelete(DeleteBehavior.NoAction);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<TvShow> TvShows { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<EpisodeActor> EpisodesActors { get; set; }
        public DbSet<FavoriteTvShow> FavoriteTvShows { get; set; }
        public DbSet<TvShowGenre> TvShowGenres { get; set; }
    }
}
