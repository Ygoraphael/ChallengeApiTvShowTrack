namespace TvShowTracker.Config.LoadDataApi
{
    using Newtonsoft.Json;
    public partial class EpisodeActorsResponseJson
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("episodes")]
        public List<EpisodeResponse> Episodes { get; set; }        
    }
    public partial class EpisodeResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("episode_number")]
        public int Chapter { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }        
        [JsonProperty("overview")]
        public string Overview { get; set; }
        [JsonProperty("still_path")]
        public string PosterPath { get; set; }
        [JsonProperty("guest_stars")]
        public List<ActorResponse> Actors { get; set; }
    }
    public partial class ActorResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("character")]
        public string Character { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("profile_path")]
        public string Picture { get; set; }
    }
}

