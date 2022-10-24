namespace TvShowTracker.Config.LoadDataApi
{
    using Newtonsoft.Json;
    public class GenreResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
    public class GenresResponseJson
    {
        [JsonProperty("genres")]
        public List<GenreResponse> Genres { get; set; }
    }
}