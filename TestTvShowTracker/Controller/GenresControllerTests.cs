using Newtonsoft.Json;
using TvShowTracker.EntityFrameworkPaginateCore;

namespace TvShowTracker.Tests.Controller
{
    internal class GenresControllerTests : BaseSetup
    {
        private const string _endPoint = "/v1/Genres";
        private const string _endPoint_Id0_BadRequest = "/0";
        private const string _endPoint_Id1_Data_Found = "/1";
        private const string _endPoint_Id3_Data_NotFound = "/3";
        private const string _endPoint_TvShows = "/TvShows";
        [Test]
        public async Task GetTvShowsGenre_ShouldReturnOkResponse_WhenDataFound()
        {
            SetValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + _endPoint_Id1_Data_Found + _endPoint_TvShows);
            var Content = result.Content.ReadAsStringAsync().Result;
            var tvShows = JsonConvert.DeserializeObject<Page<TvShowDTO>>(Content);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(Content);
            Assert.IsTrue(tvShows.RecordCount == 2);
        }
        [Test]
        public async Task GetTvShowsGenre_ShouldReturnNotFound_WhenDataNotFound()
        {
            SetValidToken();
            var result = await _httpClient.GetAsync(_endPoint + _endPoint_Id3_Data_NotFound + _endPoint_TvShows);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
        [Test]
        public async Task GetTvShowsGenre_ShouldReturnBadRequest_WhenDataNotFound()
        {
            SetValidToken();
            var result = await _httpClient.GetAsync(_endPoint + _endPoint_Id0_BadRequest + _endPoint_TvShows);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
        [Test]
        public async Task GetTvShowsGenre_ShouldReturnUnauthorized_WhenTokenInvalid()
        {
            SetInValidToken();
            var result = await _httpClient.GetAsync(_endPoint + _endPoint_Id1_Data_Found + _endPoint_TvShows);
            Assert.AreEqual(HttpStatusCode.Unauthorized, result.StatusCode);
        }
        [Test]
        public async Task GetGenres_ShouldReturnOkResponse_WhenDataFound()
        {
            SetValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint);
            var Content = result.Content.ReadAsStringAsync().Result;
            var genres = JsonConvert.DeserializeObject<Page<GenreDTO>>(Content);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(Content);
            Assert.IsTrue(genres.RecordCount == 2);
        }
        [Test]
        public async Task GetGenres_ShouldReturnNotFound_WhenDataNotFound()
        {
            SetValidToken();
            await MockData.CreateGenres(_rebuildApi, false);
            var result = await _httpClient.GetAsync(_endPoint);
            await MockData.CreateGenres(_rebuildApi, true);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
        [Test]
        public async Task GetGenres_ShouldReturnUnauthorized_WhenTokenInvalid()
        {
            SetInValidToken();
            var result = await _httpClient.GetAsync(_endPoint);
            Assert.AreEqual(HttpStatusCode.Unauthorized, result.StatusCode);
        }
    }
}
