﻿using System;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using Shadow.Client.Models;
using Shadow.Client.Networking.Messages.VmProxy;
using Shadow.Client.Networking.Tcp;

namespace Shadow.Client.Networking.Channels.VmProxy
{
    public class TcpVmProxyChannel : SslTcpChannel, IVmProxyChannel
    {
        private const int PortOffset = 1;

        // To avoid having to cast this to IVMProxyChannel every time
        public IVmProxyChannel VmProxyChannel => this;

        public TcpVmProxyChannel(VmLocation vmLocation) : base(vmLocation.Host, vmLocation.Port + PortOffset) { }

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
        }

        public override void OnDataReceived(byte[] data) => VmProxyChannel.HandleInData(data);

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