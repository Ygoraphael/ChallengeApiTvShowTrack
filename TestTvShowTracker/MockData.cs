using Microsoft.Extensions.DependencyInjection;
using TvShowTracker.Data;

namespace TvShowTracker.Tests;
public class MockData
{
    public static async Task CreateAllBase(RebuildApi application, bool seed)
    {
        await CreateUsers(application, seed);
        await CreateFavoriteTvShow(application, seed);
        await CreateGenres(application, seed);
        await CreatetvShows(application, seed);
        await CreateSeason(application, seed);
        await CreateEpsiodes(application, seed);
        await CreateActors(application, seed);
        await CreateEpisodeActors(application, seed);
        await CreatetvShowGenres(application, seed);
    }
    public static async Task CreateGenres(RebuildApi application, bool seed)
    {
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            using (var genreDbContext = provider.GetRequiredService<AppDbContext>())
            {
                await genreDbContext.Database.EnsureCreatedAsync();
                if (seed)
                {
                    await genreDbContext.Genres.AddAsync(new Genre { Id = 1, Name = "Genre 1" });
                    await genreDbContext.Genres.AddAsync(new Genre { Id = 2, Name = "Genre 2" });
                    await genreDbContext.SaveChangesAsync();
                }
                else
                {
                    genreDbContext.Genres.RemoveRange(genreDbContext.Genres);
                    await genreDbContext.SaveChangesAsync();
                }
            }
        }
    }
    public static async Task CreatetvShows(RebuildApi application, bool seed)
    {
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            using (var tvShowDbContext = provider.GetRequiredService<AppDbContext>())
            {
                await tvShowDbContext.Database.EnsureCreatedAsync();
                if (seed)
                {
                    await tvShowDbContext.TvShows.AddAsync(new TvShow { Id = 1, Name = "tvShow 1", Overview = "Overview description 2"});
                    await tvShowDbContext.TvShows.AddAsync(new TvShow { Id = 2, Name = "tvShow 2", Overview = "Overview description 2"});
                    await tvShowDbContext.SaveChangesAsync();
                }
                else
                {
                    tvShowDbContext.TvShows.RemoveRange(tvShowDbContext.TvShows);
                    await tvShowDbContext.SaveChangesAsync();
                }
            }
        }
    }
    public static async Task CreatetvShowGenres(RebuildApi application, bool seed)
    {
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            using (var tvShowGenreDbContext = provider.GetRequiredService<AppDbContext>())
            {
                await tvShowGenreDbContext.Database.EnsureCreatedAsync();
                if (seed)
                {
                    await tvShowGenreDbContext.TvShowGenres.AddAsync(new TvShowGenre { Id = 1, TvShowId = 1, GenreId = 1 });
                    await tvShowGenreDbContext.TvShowGenres.AddAsync(new TvShowGenre { Id = 2, TvShowId = 1, GenreId = 2 });
                    await tvShowGenreDbContext.TvShowGenres.AddAsync(new TvShowGenre { Id = 3, TvShowId = 2, GenreId = 1 });
                    await tvShowGenreDbContext.SaveChangesAsync();
                }
                else
                {
                    tvShowGenreDbContext.TvShowGenres.RemoveRange(tvShowGenreDbContext.TvShowGenres);
                    await tvShowGenreDbContext.SaveChangesAsync();
                }
            }
        }
    }
    public static async Task CreateSeason(RebuildApi application, bool seed)
    {
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            using (var seasonDbContext = provider.GetRequiredService<AppDbContext>())
            {
                await seasonDbContext.Database.EnsureCreatedAsync();
                if (seed)
                {
                    await seasonDbContext.Seasons.AddAsync(new Season { Id = 1, Name = "sesaon 1", Overview = "Overview description 2", TvShowId = 1 });
                    await seasonDbContext.Seasons.AddAsync(new Season { Id = 2, Name = "season 2", Overview = "Overview description 2", TvShowId = 2 });
                    await seasonDbContext.SaveChangesAsync();
                }
                else
                {
                    seasonDbContext.Seasons.RemoveRange(seasonDbContext.Seasons);
                    await seasonDbContext.SaveChangesAsync();
                }
            }
        }
    }
    public static async Task CreateEpsiodes(RebuildApi application, bool seed)
    {
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            using (var espisodesDbContext = provider.GetRequiredService<AppDbContext>())
            {
                await espisodesDbContext.Database.EnsureCreatedAsync();
                if (seed)
                {
                    await espisodesDbContext.Episodes.AddAsync(new Episode
                    { Id = 1, Name = "Episode 1", Overview = "Overview Episode description 1", SeasonId = 1 });
                    await espisodesDbContext.Episodes.AddAsync(new Episode
                    { Id = 2, Name = "Episode 2", Overview = "Overview Episode description 2", SeasonId = 2 });
                    await espisodesDbContext.SaveChangesAsync();
                }
                else
                {
                    espisodesDbContext.Episodes.RemoveRange(espisodesDbContext.Episodes);
                    await espisodesDbContext.SaveChangesAsync();
                }
            }
        }
    }
    public static async Task CreateActors(RebuildApi application, bool seed)
    {
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            using (var actorsDbContext = provider.GetRequiredService<AppDbContext>())
            {
                await actorsDbContext.Database.EnsureCreatedAsync();
                if (seed)
                {
                    await actorsDbContext.Actors.AddAsync(new Actor { Id = 1, Name = "Actor 1" });
                    await actorsDbContext.Actors.AddAsync(new Actor { Id = 2, Name = "Actor 2" });
                    await actorsDbContext.SaveChangesAsync();
                }
                else
                {
                    actorsDbContext.Actors.RemoveRange(actorsDbContext.Actors);
                    await actorsDbContext.SaveChangesAsync();
                }
            }
        }
    }
    public static async Task CreateEpisodeActors(RebuildApi application, bool seed)
    {
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            using (var episodeActorsDbContext = provider.GetRequiredService<AppDbContext>())
            {
                await episodeActorsDbContext.Database.EnsureCreatedAsync();
                if (seed)
                {
                    await episodeActorsDbContext.EpisodesActors.AddAsync(new EpisodeActor { id = 1, EpisodeId = 1, ActorId = 1 });
                    await episodeActorsDbContext.EpisodesActors.AddAsync(new EpisodeActor { id = 2, EpisodeId = 1, ActorId = 2 });
                    await episodeActorsDbContext.SaveChangesAsync();
                }
                else
                {
                    episodeActorsDbContext.EpisodesActors.RemoveRange(episodeActorsDbContext.EpisodesActors);
                    await episodeActorsDbContext.SaveChangesAsync();
                }
            }
        }
    }
    public static async Task CreateUsers(RebuildApi application, bool seed)
    {
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            using (var userDbContext = provider.GetRequiredService<AppDbContext>())
            {
                await userDbContext.Database.EnsureCreatedAsync();
                if (seed)
                {
                    await userDbContext.Users.AddAsync(new User { Email = "teste@mail.com", Password = "123456" });
                    await userDbContext.SaveChangesAsync();
                }
                else
                {
                    userDbContext.Users.RemoveRange(userDbContext.Users);
                    await userDbContext.SaveChangesAsync();
                }
            }
        }
    }
    public static async Task CreateFavoriteTvShow(RebuildApi application, bool seed)
    {
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            using (var favoTvShowDbContext = provider.GetRequiredService<AppDbContext>())
            {
                await favoTvShowDbContext.Database.EnsureCreatedAsync();
                if (seed)
                {
                    await favoTvShowDbContext.FavoriteTvShows.AddAsync(new FavoriteTvShow { Id = 1, TvShowId = 1, UserId = 1 });
                    await favoTvShowDbContext.FavoriteTvShows.AddAsync(new FavoriteTvShow { Id = 2, TvShowId = 2, UserId = 1 });
                    await favoTvShowDbContext.SaveChangesAsync();
                }
                else
                {
                    favoTvShowDbContext.FavoriteTvShows.RemoveRange(favoTvShowDbContext.FavoriteTvShows);
                    await favoTvShowDbContext.SaveChangesAsync();
                }
            }
        }
    }
}