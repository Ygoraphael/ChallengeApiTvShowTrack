namespace TvShowTracker.Config.LoadDataApi.Repository
{
    public class QueryInsert
    {
        public const string InsertGenre = $@"
                        IF NOT EXISTS (SELECT ID FROM GENRES WHERE ID = @Id)
                         INSERT INTO [dbo].[Genres] ([Id],[Name]) VALUES (@Id,@Name)";

        public const string InsertTvShow = $@"
                         IF NOT EXISTS (SELECT ID FROM TvShows WHERE ID = @Id)
                          INSERT INTO[dbo].[TvShows] ([Id],[Name],[Overview],[Popularity],[Vote_average],[vote_count])
                          VALUES (@Id, @Name, @Overview, @Popularity, @Vote_average, @vote_count)";

        public const string InsertSeason = $@"
                         IF NOT EXISTS (SELECT ID FROM Seasons WHERE ID = @Id)
                          INSERT INTO[dbo].[Seasons] ([Id],[Name],[SeasonNumber],[EpisodeCount],[Overview],[PosterPath],[TvShowId])
                          VALUES (@Id, @Name, @SeasonNumber, @EpisodeCount, @Overview, @PosterPath, @TvShowId)";

        public const string InsertEpisode = $@"
                         IF NOT EXISTS (SELECT ID FROM Episodes WHERE ID = @Id)
                          INSERT INTO [dbo].[Episodes] ([Id],[Chapter],[Name],[Overview],[PosterPath],[SeasonId])
                          VALUES (@Id, @Chapter, @Name, @Overview, @PosterPath, @SeasonId)";

        public const string InsertActor = $@"
                         IF NOT EXISTS (SELECT ID FROM Actors WHERE ID = @Id)
                          INSERT INTO [dbo].[Actors] ([Id],[Name],[Character],[Picture])
                          VALUES (@Id,@Name, @Character, @Picture)";

        public const string InsertEpisodesActors = $@"
                         IF NOT EXISTS (SELECT ID FROM EpisodesActors WHERE EpisodeId = @EpisodeId and ActorId = @ActorId)
                          INSERT INTO[dbo].[EpisodesActors] ([EpisodeId],[ActorId])
                          VALUES(@EpisodeId, @ActorId)";

        public const string InsertTvShowGenres = $@"
                         IF NOT EXISTS (SELECT ID FROM TvShowGenres WHERE TvShowId = @TvShowId and GenreId = @GenreId)
                          INSERT INTO [dbo].[TvShowGenres]([TvShowId],[GenreId])
                          VALUES (@TvShowId ,@GenreId)";
    }
}