using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Shadow.Client.Http.Gap;
using Shadow.Client.Networking.Channels.VmProxy;
using Shadow.Client.Networking.Messages.VmProxy.Out;

namespace Shadow.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var gap = new GapApiProvider(Keystore.Identity)
            {
                HttpClient = new(new HttpClientHandler
                {
                    Proxy = new WebProxy("http://localhost:1338")
                })
            };
            gap.SetRecommendedHost(Keystore.Credentials.Email);
            gap.Session = Keystore.GapSession;

            if (args.Length > 0 && args[0] == "stop")
            {
                Console.WriteLine("stopping vm");
                await gap.StopAsync();
                Console.WriteLine("vm stopped!");
                return;
            }

            if (!gap.TryGetVmLocation(out var location))
            {
                Console.WriteLine("VM not found, starting");
                location = await gap.StartAsync();
            }
            
            var channel = new TcpVmProxyChannel(location);
            new Thread(() =>
            {
                channel.Start();
                channel.SendMessage(new GetVersionMessage(1));
                channel.SendMessage(new HelloMessage(2, $"{gap.Identity.ShadowAgent}|{gap.Identity.UserAgent}",
                    gap.Session.Token));
                channel.SendMessage(new ShadowStatusMessage(3)
                {
                    Service = "shadow-status",
                    Data = new() { Type = "get_version"}
                });
                channel.Listen();
            }).Start();
            
            Console.WriteLine($"VM available at {location}");

            await Task.Delay(50000);
            Console.ReadLine();
        }
    }
}