namespace TvShowTracker.Tests.Controller
{
    public class LoginControllerTests : BaseSetup
    {
        private const string _endPoint_Login = "/v1/Login";
        [Test]
        public async Task Login_ShouldReturnNotFound_WhenInvalidCredential()
        {
            var user = new UserPostDTO { Email = "teste@mail.com", Password = "wrong1" };
            var result = await _httpClient.PostAsJsonAsync(_endPoint_Login, user);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
        [Test]
        [TestCase("teste@mail.com", "")]
        [TestCase("", "12456")]
        [TestCase("", "")]
        public async Task Login_ShouldReturnBadResquest_WhenInvalidRequest(string email, string password)
        {
            UserPostDTO user = null;
            if ((email != "") || (password != ""))
                user = new UserPostDTO { Email = email, Password = password };
            var result = await _httpClient.PostAsJsonAsync(_endPoint_Login, user);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
        [Test]
        public async Task Login_ShouldReturnOkResponse_WhenValidRequest()
        {
            var user = new UserPostDTO { Email = "teste@mail.com", Password = "123456" };
            var result = await _httpClient.PostAsJsonAsync(_endPoint_Login, user);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotEmpty(result.Content.ReadAsStringAsync().Result);
        }
    }    
}