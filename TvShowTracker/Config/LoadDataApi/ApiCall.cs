using Newtonsoft.Json;
using System.Text;
namespace TvShowTracker.Config.LoadDataApi
{
    public static class ApiCall
    {
        public static async Task<T> CallApi<T>(this HttpClient httpClient, string getUrl)
        {
            HttpResponseMessage response = httpClient.GetAsync(getUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                var dados = await response.Content.ReadAsByteArrayAsync();
                return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(dados));
            }
            else throw new ApplicationException($"Something went wrong calling the API: " + $"{response.ReasonPhrase}");
        }
    }
}