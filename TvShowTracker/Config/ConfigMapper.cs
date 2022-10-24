using TvShowTracker.Config.LoadDataApi;
using TvShowTracker.Model;
using AutoMapper;

namespace TvShowTracker.Config
{
    public class ConfigMapper : Profile
    {
        public ConfigMapper()
        {
            //Map to request of the app
            CreateMap<User, UserPostDTO>().ReverseMap();
            CreateMap<User, UserGetDTO>().ReverseMap();
            CreateMap<FavoriteTvShow, FavoriteTvShowDTO>().ReverseMap();
            CreateMap<TvShow, TvShowDTO>().ReverseMap();
            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<Episode, EpisodeDTO>().ReverseMap();
            CreateMap<Genre, GenreDTO>().ReverseMap();
            //Map to import from Api
            CreateMap<Genre, GenreResponse>().ReverseMap();
            CreateMap<Actor, ActorResponse>().ReverseMap();
            CreateMap<Episode, EpisodeResponse>().ReverseMap();
            CreateMap<TvShow, TvShowResponse>().ReverseMap();
            CreateMap<Season, SeasonResponse>().ReverseMap();
        }
    }
}