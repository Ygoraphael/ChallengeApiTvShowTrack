using System.Dynamic;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace TvShowTracker.Tests.Controller
{
    public abstract class BaseSetup
    {
        public RebuildApi _rebuildApi;
        public HttpClient _httpClient;
        public BaseSetup()
        {
            _rebuildApi = new RebuildApi();
            _httpClient = _rebuildApi.CreateClient();
            Task.Run(MockData.CreateAllBase(_rebuildApi, true).Wait);
        }

        public void SetInValidToken()
        {
            _httpClient.SetToken("InValid", "");
        }

        public void SetValidToken()
        {
            var data = new ExpandoObject();
            data.TryAdd(ClaimTypes.NameIdentifier, "1");
            data.TryAdd(ClaimTypes.Email, "teste@mail.com");
            _httpClient.SetFakeBearerToken("SUperUserName", new[] { "Role1", "Role2" }, (object)data);
        }
    }
}