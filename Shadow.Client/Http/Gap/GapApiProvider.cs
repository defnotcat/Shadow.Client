using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Shadow.Client.Extensions;
using Shadow.Client.Models;

namespace Shadow.Client.Http.Gap
{
    public class GapApiProvider : HttpApiProvider
    {
        public Identity Identity { get; set; }
        
        public GapSession Session { get; set; }
        
        public GapApiProvider(Identity identity, string host = null) : base(host)
        {
            Identity = identity;
        }

        public override HttpResponseMessage Perform(HttpRequestMessage message)
        {
            message.Headers.UserAgent.ParseAdd(Identity.UserAgent);
            message.Headers.Add("X-Shadow-Agent", Identity.ShadowAgent);
            message.Headers.Add("X-Shadow-Uuid", Identity.DeviceId);
            if (Session != null)
                message.Headers.Add("Authorization", $"Token {Session.Token}");
            return base.Perform(message);  
        }
        
        #region Authentication

        /// <summary>
        /// Get authorization for the gap API from an SsoSession
        /// </summary>
        /// <param name="ssoSession"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="TwoFactorRequiredException"></exception>
        public GapSession Login(SsoSession ssoSession)
        {
            var content = PerformJson<JsonElement>("/shadow/auth_login", out var response, content: new { token = ssoSession.Token });
            if (response.StatusCode != HttpStatusCode.OK) throw new InvalidOperationException($"Failed to obtain authorization for {Host}, the sso session is probably bad");

            var token = content.GetProperty("token").GetString();
            Session = new GapSession { Token = token };

            PerformJson<JsonElement>("/shadow/auth_uuid", out response);
            if (response.StatusCode == HttpStatusCode.PreconditionFailed) throw new TwoFactorRequiredException(null);
            if (response.StatusCode != HttpStatusCode.OK) throw new InvalidOperationException($"Unexpected API response, expected 200 status code and got {response.StatusCode}");
            
            return Session;
        }
        
        /// <summary>
        /// Submit a two factor code to approve the login
        /// </summary>
        /// <param name="twoFactorCode"></param>
        /// <exception cref="TwoFactorRequiredException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void ApproveLogin(string twoFactorCode)
        {
            var content = PerformJson<JsonElement>($"/shadow/client/approval?code={twoFactorCode}", out var response);
            if (response.StatusCode == HttpStatusCode.Forbidden) throw new TwoFactorRequiredException($"error code: {content.GetProperty("err").GetString()}");
            if (response.StatusCode != HttpStatusCode.OK) throw new InvalidOperationException($"Unexpected API response, expected 200 status code and got {response.StatusCode}");
        }

        #endregion

        #region VM
        
        /// <summary>
        /// Get the VM location (address and port)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="VmNotStartedYetException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public VmLocation GetVmLocation()
        {
            var content = PerformJson<JsonElement>("shadow/vm/ip", out var response);
            if (!content.TryGetProperty("msg", out var prop))
                content.TryGetProperty("err", out prop);
            
            if (response.StatusCode >= (HttpStatusCode) 470) throw new VmNotStartedYetException(prop.GetString());
            if (response.StatusCode != HttpStatusCode.OK) throw new InvalidOperationException($"Invalid status code, expected 200, got {response.StatusCode}");

            return response.DeserializeAsAsync<VmLocation>().Result;
        }

        public bool TryGetVmLocation(out VmLocation location)
        {
            location = null;
            try
            {
                location = GetVmLocation();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// Start the VM
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void Start()
        {
            PerformJson<JsonElement>("shadow/vm/start", out var response);
            if (response.StatusCode != HttpStatusCode.OK) throw new InvalidOperationException($"Invalid status code, expected 200, got {response.StatusCode}");
        }

        /// <summary>
        /// Start the VM and wait until it's ready
        /// </summary>
        /// <returns></returns>
        public async Task<VmLocation> StartAsync()
        {
            Start();
            VmLocation location;
            do
            {
                await Task.Delay(2000);
            } while (!TryGetVmLocation(out location));
            return location;
        }

        /// <summary>
        /// Stop the VM
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void Stop()
        {
            PerformJson<JsonElement>("shadow/vm/stop", out var response);
            if (response.StatusCode != HttpStatusCode.OK) throw new InvalidOperationException($"Invalid status code, expected 200, got {response.StatusCode}");
        }

        /// <summary>
        /// Stop the VM and wait until it's off
        /// </summary>
        /// <returns></returns>
        public async Task StopAsync()
        {
            Stop();
            do
            {
                await Task.Delay(3000);
            } while (TryGetVmLocation(out _));
        }
        
        #endregion
            
        /// <summary>
        /// Use the recommended API host given by another Shadow API
        /// </summary>
        /// <param name="email">Session Email</param>
        public void SetRecommendedHost(string email)
        {
            Host = "tinag.shadow.tech:2053";
            var content = PerformJson<JsonElement>($"gap?fmt=json&email={HttpUtility.UrlEncode(email)}", out _, "GET");
            Host = new Uri(content.GetProperty("uri").GetString()).Host;
        }
        
    }
}