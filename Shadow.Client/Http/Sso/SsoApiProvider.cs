using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using Shadow.Client.Models;

namespace Shadow.Client.Http.Sso
{
    public class SsoApiProvider : HttpApiProvider
    {

        private const string UserAgent = "okhttp/4.9.0";
        private const string DefaultHost = "sso.api-web.shadow.tech";
        
        public Identity Identity { get; set; }
        
        public SsoSession Session { get; set; }

        public SsoApiProvider(Identity identity, string host = DefaultHost) : base(host)
        {
            Identity = identity;
        }

        public override HttpResponseMessage Perform(HttpRequestMessage message)
        {
            // Really no point in using the identity's UserAgent as okhttp is vague enough
            message.Headers.UserAgent.ParseAdd(UserAgent);
            return base.Perform(message);
        }

        /// <summary>
        /// Login with specified credentials (/api/v2/sso/auth/login)
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public SsoSession Login(string email, string password)
        {
            var content = PerformJson<JsonElement>("api/v2/sso/auth/login", out var response, content: new { email, password, device_id = Identity.DeviceId });

            if (response.StatusCode == HttpStatusCode.Forbidden) throw new SsoAuthenticationFailedException($"error code: {content.GetProperty("error").GetString()}");
            if (response.StatusCode != HttpStatusCode.OK) throw new InvalidOperationException($"Unexpected API response, expected 200 status code and got {response.StatusCode}");

            return Session = new SsoSession
            {
                Token = content.GetProperty("token").GetString(),
                Refresh = content.GetProperty("refresh").GetString()
            };
        }
    }
}