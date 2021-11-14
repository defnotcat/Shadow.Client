namespace Shadow.Client.Models
{
    public class Identity
    {

        public string UserAgent { get; set; }

        public string ShadowAgent { get; set; }

        public string DeviceId { get; set; }

        // todo: remove
        public static Identity DebugIdentity = new()
        {
            UserAgent = "Samsung SM-A405FN",
            ShadowAgent = "Android;arm64-v8a;Samsung SM-A405FN;29(29);3.6.4#306040",
            DeviceId = "53a5d9a1-6ff2-4528-a60f-4b0dbad8f728"
        };
    }
}