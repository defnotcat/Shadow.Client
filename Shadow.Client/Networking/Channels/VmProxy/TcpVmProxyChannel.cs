using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading;
using Shadow.Client.Models;
using Shadow.Client.Networking.Channels.CtrlV2;
using Shadow.Client.Networking.Messages.VmProxy;
using Shadow.Client.Networking.Messages.VmProxy.In;
using Shadow.Client.Networking.Tcp;

namespace Shadow.Client.Networking.Channels.VmProxy
{
    public class TcpVmProxyChannel : SslTcpChannel, IVmProxyChannel
    {
        private const int PortOffset = 1;

        public TcpVmProxyChannel(VmLocation vmLocation) : base(vmLocation.Host, vmLocation.Port + PortOffset)
        {
        }

        public void SendMessage(VmProxyMessageOut message)
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            message.Write(writer);
            Console.WriteLine(JsonSerializer.Serialize(message));
            Send(stream.ToArray());
        }

        public void OnMessage(VmProxyMessageIn message)
        {
            Console.WriteLine($"\nOnMessage: {message.GetType().FullName}");
            if (message is ShadowStatusMessage)
                new Thread(() =>
                {
                    try
                    {
                        Console.WriteLine("Starting ctrl chan");
                        var ctrl = new TcpCtrlV2Channel(Host, Port + 10);
                        ctrl.Start();
                        Console.WriteLine("SendMessage()");
                        ctrl.SendMessage(null);
                        ctrl.Listen();
                    }
                    catch (Exception exp)
                    { 
                        Console.WriteLine(exp.Message);
                    }
                }).Start();
        }

        public override void OnDataReceived(byte[] data)
        {
            Console.WriteLine(Encoding.Default.GetString(data));
            var message = JsonSerializer.Deserialize<VmProxyMessageIn>(Encoding.Default.GetString(data));
            var messageIdentifier = message.Cmd ?? message.Resp;

            if (!string.IsNullOrEmpty(messageIdentifier) &&
                IVmProxyChannel.TryGetMessageModel(messageIdentifier, out var model))
                OnMessage(model);
        }

        public override void OnDataSent(byte[] data)
        {
            Console.WriteLine($"OnDataSent: ${Encoding.Default.GetString(data)}");
        }

        protected override bool OnSslVerifyCert(object sender, X509Certificate cert, X509Chain chain,
            SslPolicyErrors policy)
        {
            Console.WriteLine($"OnSslVerifyCert: {cert.Subject}");
            return true;
        }

        public int GetMaxMessageSize() => 0x400; // 1024
    }
}