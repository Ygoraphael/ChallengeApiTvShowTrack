using Newtonsoft.Json;
using TvShowTracker.EntityFrameworkPaginateCore;

namespace TvShowTracker.Tests.Controller
{
    internal class TvShowsControllerTests : BaseSetup
    {
        private const string _endPoint = "/v1/TvShows";
        private const string _endPoint_Id0_BadRquest = "/0";
        private const string _endPoint_Id1_Data_Found = "/1";
        private const string _endPoint_Id3_Data_NotFound = "/3";
        private const string _endPoint_Actors = "/Actors";
        private const string _endPoint_Episodes = "/Episodes";
        [Test]
        public async Task GetTvShows_ShouldReturnOkResponse_WhenDataFound()
        {
            SetValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint);
            var Content = result.Content.ReadAsStringAsync().Result;
            var tvShows = JsonConvert.DeserializeObject<Page<TvShowDTO>>(Content);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(tvShows);
            Assert.IsTrue(tvShows.Results.Count() == 2);
        }
        [Test]
        public async Task GetTvShows_ShouldReturnNotFound_WhenDataNotFound()
        {
            SetValidToken();
            await MockData.CreatetvShows(_rebuildApi, false);
            var result = await _httpClient.GetAsync(_endPoint);
            await MockData.CreatetvShows(_rebuildApi, true);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
        [Test]
        public async Task GetTvShows_ShouldReturnUnauthorized_WhenInvalidToken()
        {
            SetInValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint);
            Assert.AreEqual(HttpStatusCode.Unauthorized, result.StatusCode);
        }
        [Test]
        public async Task GetTvShowsById_ShouldReturnOkResponse_WhenDataFound()
        {
            SetValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + _endPoint_Id1_Data_Found);
            var Content = result.Content.ReadAsStringAsync().Result;
            var tvShows = JsonConvert.DeserializeObject<TvShow>(Content);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(tvShows);
        }
        [Test]
        public async Task GetTvShowsById_ShouldReturnNotFound_WhenDataNotFound()
        {
            SetValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + _endPoint_Id3_Data_NotFound);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
        [Test]
        public async Task GetTvShowsById_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            SetValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + _endPoint_Id0_BadRquest);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
        [Test]
        public async Task GetTvShowsById_ShouldReturnUnauthorized_WhenInvalidToken()
        {
            SetInValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + _endPoint_Id1_Data_Found);
            Assert.AreEqual(HttpStatusCode.Unauthorized, result.StatusCode);
        }


        [Test]
        public async Task GetActorsTvShow_ShouldReturnOkResponse_WhenDataFound()
        {
            SetValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + _endPoint_Id1_Data_Found + _endPoint_Actors);
            var Content = result.Content.ReadAsStringAsync().Result;
            var tvShows = JsonConvert.DeserializeObject<TvShow>(Content);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(tvShows);
        }
        [Test]
        public async Task GetActorsTvShow_ShouldReturnNotFound_WhenDataNotFound()
        {
            SetValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + _endPoint_Id3_Data_NotFound + _endPoint_Actors);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
        [Test]
        public async Task GetActorsTvShow_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            SetValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + _endPoint_Id0_BadRquest + _endPoint_Actors);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
        [Test]
        public async Task GetActorsTvShow_ShouldReturnUnauthorized_WhenInvalidToken()
        {
            SetInValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + _endPoint_Id1_Data_Found + _endPoint_Actors);
            Assert.AreEqual(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        [Test]
        public async Task GetEpisodesTvShow_ShouldReturnOkResponse_WhenDataFound()
        {
            SetValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + _endPoint_Id1_Data_Found + _endPoint_Episodes);
            var Content = result.Content.ReadAsStringAsync().Result;
            var tvShows = JsonConvert.DeserializeObject<TvShow>(Content);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(tvShows);
        }
        [Test]
        public async Task GetEpisodesTvShow_ShouldReturnNotFound_WhenDataNotFound()
        {
            SetValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + _endPoint_Id3_Data_NotFound + _endPoint_Episodes);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
        [Test]
        public async Task GetEpisodesTvShow_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            SetValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + _endPoint_Id0_BadRquest + _endPoint_Episodes);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
        [Test]
        public async Task GetEpisodesTvShow_ShouldReturnUnauthorized_WhenInvalidToken()
        {
            SetInValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + _endPoint_Id1_Data_Found + _endPoint_Episodes);
            Assert.AreEqual(HttpStatusCode.Unauthorized, result.StatusCode);
        }


        [Test]
        public async Task GetEpisodeTvShow_ShouldReturnOkResponse_WhenDataFound()
        {
            SetValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + _endPoint_Id1_Data_Found + _endPoint_Episodes);
            var Content = result.Content.ReadAsStringAsync().Result;
            var tvShows = JsonConvert.DeserializeObject<TvShow>(Content);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(tvShows);
        }
        [Test]
        public async Task GetEpisodeTvShow_ShouldReturnNotFound_WhenDataNotFound()
        {
            SetValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + _endPoint_Id3_Data_NotFound + _endPoint_Episodes);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
        [Test]
        [TestCase("/0","/1")]
        [TestCase("/1", "/0")]
        [TestCase("/0", "/0")]
        public async Task GetEpisodeTvShow_ShouldReturnBadRequest_WhenInvalidRequest(string endPointTvShow, string endPointEpisode)
        {
            SetValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + endPointTvShow + _endPoint_Episodes + endPointEpisode);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
        [Test]
        public async Task GetEpisodeTvShow_ShouldReturnUnauthorized_WhenInvalidToken()
        {
            SetInValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + _endPoint_Id1_Data_Found + _endPoint_Episodes + _endPoint_Id1_Data_Found);
            Assert.AreEqual(HttpStatusCode.Unauthorized, result.StatusCode);
        }
    }
}