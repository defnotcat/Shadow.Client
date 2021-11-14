using RequestTransformer = System.Func<System.Net.Http.HttpRequestMessage, System.Net.Http.HttpRequestMessage>;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Shadow.Client.Extensions;

namespace Shadow.Client.Http
{
    public class HttpApiProvider
    {
        
        public HttpClient HttpClient { get; set; }

        public string Host { get; protected set; }

        public HttpApiProvider(string host)
        {
            Host = host;
        }

        public HttpApiProvider()
        {
        }

        public virtual T PerformJson<T>(string endpoint, out HttpResponseMessage response, string method = "POST",
            object content = null)
        {
            response = Perform(new HttpRequestMessage(new HttpMethod(method), $"https://{Host}/{endpoint}")
            {
                Content = content == null ? null : new StringContent(JsonSerializer.Serialize(content), Encoding.Default, "application/json")
            });
            return response.DeserializeAsAsync<T>().Result;
        }

        public virtual HttpResponseMessage Perform(HttpRequestMessage message)
            => HttpClient.Send(message);

        public virtual async Task<HttpResponseMessage> PerformAsync(HttpRequestMessage message)
            => await HttpClient.SendAsync(message);

        // todo: remove transformers
    }
}