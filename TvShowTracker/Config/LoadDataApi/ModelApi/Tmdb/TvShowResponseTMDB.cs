namespace TvShowTracker.Config.LoadDataApi
{
    using Newtonsoft.Json;
    public class TvShowResponseJson
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("results")]
        public List<TvShowResponse> TvShows { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }
    }
    public class TvShowResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("overview")]
        public string Overview { get; set; }
        [JsonProperty("popularity")]
        public double Popularity { get; set; }
        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }
        [JsonProperty("vote_count")]
        public double VoteCount { get; set; }
        [JsonProperty("genre_ids")]
        public List<int> GenresId { get; set; }
    }
    public class TvShowSave
    {
        public string Name { get; set; }
        public string? Overview { get; set; }
        public double? Popularity { get; set; }
        public double? Vote_average { get; set; }
        public double? vote_count { get; set; }
    }

}