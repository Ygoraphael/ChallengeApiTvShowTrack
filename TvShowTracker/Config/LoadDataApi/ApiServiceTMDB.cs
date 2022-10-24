using TvShowTracker.Config.LoadDataApi.Repository;
using TvShowTracker.Model;
using TvShowTracker.Data;
using AutoMapper;
namespace TvShowTracker.Config.LoadDataApi
{
    public class ApiServiceTMDB : IApiServices
    {
        private string _apiKey;
        private readonly DapperRepository _dapperRepository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        List<TvShowGenre> _listTvShowGenres = new List<TvShowGenre>();
        List<TvShow> _listTvShow = new List<TvShow>();
        List<Genre> _listGenre = new List<Genre>();
        List<Season> _listSeason = new List<Season>();
        List<Episode> _listEpisode = new List<Episode>();
        List<Actor> _listActor = new List<Actor>();
        List<EpisodeActor> _listEpisodeActor = new List<EpisodeActor>();
        public ApiServiceTMDB(HttpClient httpClient, IMapper mapper, DapperRepository dapperRepository, IConfiguration config)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient)); ;
            _httpClient.BaseAddress = new Uri(config["ExternalApi:TMDBAPI"]);
            _mapper = mapper;
            _dapperRepository = dapperRepository;
            _apiKey = config["ExternalApi:TMDBKey"];
        }
        public async Task LoapApi()
        {
            try
            {
                GetApiGenres();
                GetApiTvShows();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private async Task InsertGenres()
        {
            await _dapperRepository.InsertGenres(_listGenre);
            _listGenre.Clear();
        }
        public async Task GetApiGenres()
        {
            string _getUrlGenres = "genre/tv/list?api_key={api_key}";
            string getUrl = _getUrlGenres.Replace("{api_key}", _apiKey);
            try
            {
                GenresResponseJson GenresApi = await _httpClient.CallApi<GenresResponseJson>(getUrl);
                foreach (var genre in GenresApi.Genres)
                {
                    _listGenre.Add(_mapper.Map<Genre>(genre));
                }
                await InsertGenres();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task GetApiTvShows()
        {
            const int MaxPagesToLoad = 20;
            int PageNumber = 1;
            int TotalPages;
            string _getUrlTvShows = "trending/tv/day?api_key={api_key}&page={PageNumber}";
            string getUrlNoPage = _getUrlTvShows.Replace("{api_key}", _apiKey);
            string getUrl = getUrlNoPage.Replace("{PageNumber}", PageNumber.ToString());
            do
            {
                try
                {
                    var JsonTvShow = await _httpClient.CallApi<TvShowResponseJson>(getUrl);
                    TotalPages = JsonTvShow.TotalPages;
                    var ListTvShow = JsonTvShow.TvShows;
                    foreach (var tvShow in JsonTvShow.TvShows)
                    {
                        _listTvShow.Add(_mapper.Map<TvShow>(tvShow));
                        CreateRelationTvShowGenres(tvShow.GenresId, tvShow.Id);
                        await GetApiSeason(tvShow.Id);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                PageNumber++;
                getUrl = getUrlNoPage.Replace("{PageNumber}", PageNumber.ToString());
                SaveAlllistEntities();
                ClearAlllistEntities();
                SaveRelations();
                ClearAlllistRelations();
            } while (PageNumber < MaxPagesToLoad && PageNumber < TotalPages);
        }
        private void CreateRelationTvShowGenres(List<int> genreTvShow, int tvShowId)
        {
            foreach (int id in genreTvShow)
            {
                var tvSG = new TvShowGenre()
                {
                    GenreId = id,
                    TvShowId = tvShowId
                };
                _listTvShowGenres.Add(tvSG);
            }
        }
        public async Task GetApiSeason(int tvShowId)
        {
            string _getUrlSeason = "tv/{tvShowId}?api_key={api_key}";
            string getUrl = _getUrlSeason.Replace("{api_key}", _apiKey);
            getUrl = getUrl.Replace("{tvShowId}", tvShowId.ToString());
            try
            {
                var SeasonApi = await _httpClient.CallApi<SeasonResponseJson>(getUrl);
                foreach (var lSeason in SeasonApi.Seasons)
                {
                    var season = _mapper.Map<Season>(lSeason);
                    season.TvShowId = tvShowId;
                    _listSeason.Add(season);
                    await GetApiEpisodesActors(season.Id, season.SeasonNumber, tvShowId);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task GetApiEpisodesActors(int seasonId, int seasonNumber, int tvShowId)
        {
            string _getUrlEpisode = "tv/{tvShowId}/season/{SeasonId}?api_key={api_key}";
            string getUrl = _getUrlEpisode.Replace("{api_key}", _apiKey);
            getUrl = getUrl.Replace("{tvShowId}", tvShowId.ToString());
            getUrl = getUrl.Replace("{SeasonId}", seasonNumber.ToString());
            try
            {
                var EpisodeActorApi = await _httpClient.CallApi<EpisodeActorsResponseJson>(getUrl);
                foreach (var lEpisode in EpisodeActorApi.Episodes)
                {
                    var episode = _mapper.Map<Episode>(lEpisode);
                    episode.SeasonId = seasonId;
                    _listEpisode.Add(episode);
                    foreach (var lActor in lEpisode.Actors)
                    {
                        var actor = _mapper.Map<Actor>(lActor);
                        _listActor.Add(actor);
                        EpisodeActor episodeActors = new EpisodeActor();
                        episodeActors.ActorId = actor.Id;
                        episodeActors.EpisodeId = episode.Id;
                        _listEpisodeActor.Add(episodeActors);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void SaveRelations()
        {
            try
            {
                _dapperRepository.InsertEpisodeActor(_listEpisodeActor);
                _dapperRepository.InsertTvShowGenres(_listTvShowGenres);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void ClearAlllistRelations()
        {
            _listEpisodeActor.Clear();
            _listTvShowGenres.Clear();
        }
        private async Task SaveAlllistEntities()
        {
            try
            {
                await _dapperRepository.InsertTvShows(_listTvShow);
                await _dapperRepository.InsertSeasons(_listSeason);
                await _dapperRepository.InsertEpisodes(_listEpisode);
                await _dapperRepository.InsertActors(_listActor);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void ClearAlllistEntities()
        {
            _listTvShow.Clear();
            _listSeason.Clear();
            _listEpisode.Clear();
            _listActor.Clear();
        }
    }
}