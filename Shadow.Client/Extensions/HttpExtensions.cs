using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shadow.Client.Extensions
{
    public static class HttpExtensions
    {

        public static async Task<T> DeserializeAsAsync<T>(this HttpResponseMessage response)
        {
            var rawContent = await response.Content.ReadAsStringAsync();
            if (rawContent.Length == 0) rawContent = "{}";
            return JsonSerializer.Deserialize<T>(rawContent);
        }
        
    }
}