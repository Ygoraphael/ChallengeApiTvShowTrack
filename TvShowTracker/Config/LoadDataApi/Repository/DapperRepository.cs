using System.Data.SqlClient;
using TvShowTracker.Model;
using Dapper;
namespace TvShowTracker.Config.LoadDataApi.Repository
{
    public class DapperRepository
    {
        public SqlConnection _connection;
        public DapperRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                _connection.Open();
            }
        }
        public async Task InsertGenres(List<Genre> genres)
        {
            try
            {
                _connection.Execute(QueryInsert.InsertGenre, genres);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task InsertTvShows(List<TvShow> tvShows)
        {
            try
            {
                _connection.Execute(QueryInsert.InsertTvShow, tvShows);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task InsertSeasons(List<Season> seasons)
        {
            try
            {
                _connection.Execute(QueryInsert.InsertSeason, seasons);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task InsertEpisodes(List<Episode> episodes)
        {
            try
            {
                _connection.Execute(QueryInsert.InsertEpisode, episodes);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task InsertActors(List<Actor> actors)
        {
            try
            {
                _connection.Execute(QueryInsert.InsertActor, actors);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task InsertTvShowGenres(List<TvShowGenre> tvShowGenres)
        {
            try
            {
                _connection.Execute(QueryInsert.InsertTvShowGenres, tvShowGenres);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task InsertEpisodeActor(List<EpisodeActor> episodeActors)
        {
            try
            {
                _connection.Execute(QueryInsert.InsertEpisodesActors, episodeActors);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
