using TvShowTracker.Config.LoadDataApi.Repository;
using TvShowTracker.Model;
using TvShowTracker.Data;
using AutoMapper;
namespace TvShowTracker.Config.LoadDataApi
{
    public class ApiServiceTMDB : IApiServices
    {
        private string _apiKey;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly DapperRepository _dapperRepository;
        private readonly string _urlGenre;
        private readonly string _urlTvShow;
        private readonly string _urlSeason;
        private readonly string _urlEpsiodeActors;

        List<Genre> _listGenre = new List<Genre>();
        List<Actor> _listActor = new List<Actor>();
        List<TvShow> _listTvShow = new List<TvShow>();
        List<Season> _listSeason = new List<Season>();
        List<Episode> _listEpisode = new List<Episode>();
        List<TvShowGenre> _listTvShowGenres = new List<TvShowGenre>();
        List<EpisodeActor> _listEpisodeActor = new List<EpisodeActor>();
        public ApiServiceTMDB(HttpClient httpClient, IMapper mapper, DapperRepository dapperRepository, IConfiguration config)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient)); ;
            _httpClient.BaseAddress = new Uri(config["ExternalApi:TMDBAPI"]);
            _mapper = mapper;
            _dapperRepository = dapperRepository;
            _apiKey = config["ExternalApi:TMDBKey"];
            _urlGenre = config["ExternalApi:Methods:Genres"];
            _urlTvShow = config["ExternalApi:Methods:TvShows"];
            _urlSeason = config["ExternalApi:Methods:Seasons"];
            _urlEpsiodeActors = config["ExternalApi:Methods:EpisodesActors"];
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
            string getUrl = ReplaceApiKeyUrl(_urlGenre);
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
        public void CleanMemory()
        {
            SaveAlllistEntities();
            ClearAlllistEntities();
            SaveRelations();
            ClearAlllistRelations();
        }
        public async Task GetApiTvShows()
        {
            const int MaxPagesToLoad = 20;
            int PageNumber = 1;
            int TotalPages;
            string getUrl = GetUrlTvShow(PageNumber);
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
                getUrl = GetUrlTvShow(PageNumber);
                CleanMemory();
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
            string getUrl = GetUrlSeasons(tvShowId);
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
            string getUrl = GetUrlEpisodeActors(seasonNumber, tvShowId);
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
        public string ReplaceApiKeyUrl(string url)
        {
            return url.Replace("{api_key}", _apiKey);             
        }
        public string GetUrlTvShow(int PageNumber)
        {
            return ReplaceApiKeyUrl(_urlTvShow).Replace("{PageNumber}", PageNumber.ToString());
        }
        public string GetUrlSeasons(int tvShowId)
        {
            return ReplaceApiKeyUrl(_urlSeason).Replace("{tvShowId}", tvShowId.ToString());
        }
        public string GetUrlEpisodeActors(int seasonNumber, int tvShowId)
        {
            return ReplaceApiKeyUrl(_urlEpsiodeActors)
                    .Replace("{tvShowId}", tvShowId.ToString())
                    .Replace("{SeasonId}", seasonNumber.ToString());
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