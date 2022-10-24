using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowTracker.EntityFrameworkPaginateCore;

namespace TvShowTracker.Tests.Controller
{
    internal class FavoriteTvShowControllerTests : BaseSetup
    {
        private const string _endPoint = "/v1/FavoritesTvShows";
        private const string _endPoint_Id0_BadRequest = "/0";
        private const string _endPoint_Id1_Data_Found = "/1";
        private const string _endPoint_Id3_Data_NotFound = "/3";
        [Test]
        public async Task GetFavoriteTvShow_ShouldReturnOkResponse_WhenDataFound()
        {
            SetValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint);
            var Content = result.Content.ReadAsStringAsync().Result;
            var tvShows = JsonConvert.DeserializeObject<Page<TvShowDTO>>(Content);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(tvShows);
            Assert.IsTrue(tvShows.RecordCount == 2);
        }
        [Test]
        public async Task GetFavoriteTvShow_ShouldReturnNotFound_WhenDataNotFound()
        {
            SetValidToken();
            await MockData.CreateFavoriteTvShow(_rebuildApi, false);
            var result = await _httpClient.GetAsync(_endPoint);
            await MockData.CreateFavoriteTvShow(_rebuildApi, true);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
        [Test]
        public async Task GetFavoriteTvShow_ShouldReturnUnauthorized_WhenInValidToken()
        {
            SetInValidToken();
            var result = await _httpClient.GetAsync(_endPoint);
            Assert.AreEqual(HttpStatusCode.Unauthorized, result.StatusCode);
        }
        [Test]
        public async Task PostFavoriteTvShow_ShouldReturnOkResponse_WhenDataFound()
        {
            SetValidToken();
            var data = new FavoriteTvShowDTO() { TvShowId = 1 };
            var result = await _httpClient.PostAsJsonAsync(_endPoint, data);
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }
        [Test]
        public async Task PostFavoriteTvShow_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            SetValidToken();
            var result = await _httpClient.PostAsJsonAsync(_endPoint, 0);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
        [Test]
        public async Task PostFavoriteTvShow_ShouldReturnUnauthorized_WhenInvalidToken()
        {
            SetInValidToken();
            var result = await _httpClient.PostAsJsonAsync(_endPoint, 0);
            Assert.AreEqual(HttpStatusCode.Unauthorized, result.StatusCode);
        }
        [Test]
        public async Task DeleteFavoriteTvShow_ShouldReturnNoContent_WhenDataFound()
        {
            SetValidToken();
            var result = await _httpClient.DeleteAsync(_endPoint + _endPoint_Id1_Data_Found);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }
        [Test]
        public async Task DeleteFavoriteTvShow_ShouldReturnNotFound_WhenDataNotFound()
        {
            SetValidToken();
            var result = await _httpClient.DeleteAsync(_endPoint + _endPoint_Id3_Data_NotFound);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
        [Test]
        public async Task DeleteFavoriteTvShow_ShouldReturnBadRequest_WhenInvalidRequestFound()
        {
            SetValidToken();
            var result = await _httpClient.DeleteAsync(_endPoint + _endPoint_Id0_BadRequest);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
        [Test]
        public async Task DeleteFavoriteTvShow_ShouldReturnUnauthorized_WhenInvalidToken()
        {
            SetInValidToken();
            var result = await _httpClient.DeleteAsync(_endPoint + _endPoint_Id1_Data_Found);
            Assert.AreEqual(HttpStatusCode.Unauthorized, result.StatusCode);
        }
    }
}
