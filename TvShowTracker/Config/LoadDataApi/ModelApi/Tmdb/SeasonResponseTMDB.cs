namespace TvShowTracker.Config.LoadDataApi
{
    using Newtonsoft.Json;
    public class SeasonResponseJson
    {
        [JsonProperty("seasons")]
        public List<SeasonResponse> Seasons { get; set; }
    }
    public partial class SeasonResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("season_number")]
        public int SeasonNumber { get; set; }
        [JsonProperty("episode_count")]
        public int EpisodeCount { get; set; }
        [JsonProperty("overview")]
        public string Overview { get; set; }
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }
    }
}