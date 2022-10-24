namespace TvShowTracker.Config.LoadDataApi
{
    public interface IApiServices
    {
        Task LoapApi();
        Task GetApiGenres();
        Task GetApiTvShows();
        Task GetApiSeason(int TvShowId);
        Task GetApiEpisodesActors(int TvShowId, int SeasonNumber, int SeasonId);
    }
}
