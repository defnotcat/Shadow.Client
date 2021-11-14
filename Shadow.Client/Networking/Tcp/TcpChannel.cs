using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace Shadow.Client.Networking.Tcp
{
    public abstract class TcpChannel : ChannelBase
    {
        protected TcpClient TcpClient { get; set; }
        protected Stream Stream { get; set; }
        protected BinaryWriter Writer { get; set; }

        protected TcpChannel(string host, int portBase, int portOffset) : base(host, portBase, portOffset)
        {
        }

        public abstract override void OnDataReceived(byte[] data);
        public abstract override void OnDataSent(byte[] data);

        public override void Send(byte[] data)
        {
            Stream.Write(data);
            OnDataSent(data);
        }

        public override void Stop()
        {
            Console.WriteLine($"Stopping TcpChannel: {ChannelName}");
            TcpClient?.Close();
        }

        public override void Start()
        {
            TcpClient ??= new TcpClient(Host, Port);

            if (!TcpClient.Connected)
                Throw("Failed to start TcpChannel");

            Stream = TcpClient.GetStream();
            Writer = new BinaryWriter(Stream);
        }

        public override void Listen()
        {
            while (true)
            {
                try
                {
                    var recvBuff = new byte[1024];
                    var bytesRead = Stream.Read(recvBuff, 0, recvBuff.Length);
                    OnDataReceived(recvBuff.Take(bytesRead).ToArray());
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                    Console.WriteLine("Exp on listen: " + exp.StackTrace);
                }
            }
        }

        public override bool IsChannelActive()
        {
            // Find a better way to do this as I'm mot sure whether the Connected property is bs or not
            return TcpClient.Connected;
        }

        public override void Dispose()
        {
            Stop();
            TcpClient.Dispose();
            Stream.Dispose();
        }
    }
}