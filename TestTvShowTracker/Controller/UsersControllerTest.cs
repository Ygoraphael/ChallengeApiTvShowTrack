using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShowTracker.Tests.Controller
{
    internal class UsersControllerTest : BaseSetup
    {
        private const string _endPoint = "/v1/Users";
        private const string _endPoint_Id0_BadRequest = "/0";
        private const string _endPoint_Id1_Data_Found = "/1";
        private const string _endPoint_Id3_Data_NotFound = "/3";
        [Test]
        public async Task PostUser_ShouldReturnOkResponse_WhenValidRequest()
        {
            var user = new UserPostDTO { Email = "teste@mail.com", Password = "123456" };
            var result = await _httpClient.PostAsJsonAsync(_endPoint, user);
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            Assert.IsNotEmpty(result.Content.ReadAsStringAsync().Result);
        }
        [Test]
        [TestCase("teste@mail.com", "")]
        [TestCase("", "12456")]
        [TestCase("", "")]
        public async Task PostUser_ShouldReturnBadRequest_WhenInvalidRequest(string email, string password)
        {
            UserPostDTO user = null;
            if ((email != "") || (password != ""))
                user = new UserPostDTO { Email = email, Password = password };
            var result = await _httpClient.PostAsJsonAsync(_endPoint, user);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.IsNotEmpty(result.Content.ReadAsStringAsync().Result);
        }
        [Test]
        public async Task GetUserByI_ShouldReturnOkResponse_WhenDataFound()
        {
            HttpResponseMessage result = await _httpClient.GetAsync(_endPoint + _endPoint_Id1_Data_Found);
            var Content = result.Content.ReadAsStringAsync().Result;
            var user = JsonConvert.DeserializeObject<UserGetDTO>(Content);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(user);
            Assert.IsTrue(user.Email == "teste@mail.com");
        }
        [Test]
        public async Task GetUserById_ShouldReturnNotFound_WhenDataNotFound()
        {
            var result = await _httpClient.GetAsync(_endPoint + _endPoint_Id3_Data_NotFound);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
        [Test]
        public async Task GetUserById_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            var result = await _httpClient.GetAsync(_endPoint + _endPoint_Id0_BadRequest);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
