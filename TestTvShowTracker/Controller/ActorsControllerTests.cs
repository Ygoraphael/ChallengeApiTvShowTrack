using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowTracker.EntityFrameworkPaginateCore;

namespace TvShowTracker.Tests.Controller
{
    public class ActorsControllerTests : BaseSetup
    {
        private const string _endPoint = "/v1/Actors";
        private const string _endPoint_Id0_BadRequest = "/0";
        private const string _endPoint_Id1_Data_Found = "/1";
        private const string _endPoint_Id3_Data_NotFound = "/3";
        private const string _endPoint_TvShows = "/TvShows";
        [Test]
        public async Task GetActorById_ShouldReturnOkResponse_WhenDataFound()
        {
            SetValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + _endPoint_Id1_Data_Found);
            var Content = result.Content.ReadAsStringAsync().Result;
            var actor = JsonConvert.DeserializeObject<Actor>(Content);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(actor);
            Assert.IsTrue(actor.Id == 1);
        }
        [Test]
        public async Task GetActorById_ShouldReturnNotFound_WhenDataNotFound()
        {
            SetValidToken();
            var result = await _httpClient.GetAsync(_endPoint + _endPoint_Id3_Data_NotFound);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
        [Test]
        public async Task GetActorById_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            SetValidToken();
            var result = await _httpClient.GetAsync(_endPoint + _endPoint_Id0_BadRequest);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
        [Test]
        public async Task GetActors_ShouldReturnOkResponse_WhenDataFound()
        {
            SetValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint);
            var Content = result.Content.ReadAsStringAsync().Result;
            var actors = JsonConvert.DeserializeObject<Page<ActorDTO>>(Content);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(actors);
            Assert.IsTrue(actors.RecordCount == 2);
        }
        [Test]
        public async Task GetActors_ShouldReturnNotFound_WhenDataNotFound()
        {
            SetValidToken();
            await MockData.CreateActors(_rebuildApi, false);
            var result = await _httpClient.GetAsync(_endPoint);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
            await MockData.CreateActors(_rebuildApi, true);
        }
        [Test]
        public async Task GetActorTvShows_ShouldReturnOkResponse_WhenDataFound()
        {
            SetValidToken();
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + _endPoint_Id1_Data_Found +_endPoint_TvShows);
            var Content = result.Content.ReadAsStringAsync().Result;
            var tvShows = JsonConvert.DeserializeObject<Page<TvShowDTO>>(Content);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(tvShows);
            Assert.IsTrue(tvShows.RecordCount == 1);
        }
        [Test]
        public async Task GetActorTvShows_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            SetValidToken();
            var result = await _httpClient.GetAsync(_endPoint + _endPoint_Id0_BadRequest + _endPoint_TvShows);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
        [Test]
        public async Task GetActorTvShows_ShouldReturnNotFound_WhenDataNotFound()
        {
            SetValidToken();
            var result = await _httpClient.GetAsync(_endPoint + _endPoint_Id3_Data_NotFound + _endPoint_TvShows);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
        [Test]
        public async Task GetActorTvShows_ShouldReturnUnauthorized_WhenTokenInvalid()
        {
            SetInValidToken();
            var result = await _httpClient.GetAsync(_endPoint + _endPoint_Id1_Data_Found + _endPoint_TvShows);
            Assert.AreEqual(HttpStatusCode.Unauthorized, result.StatusCode);
        }
    }
}
