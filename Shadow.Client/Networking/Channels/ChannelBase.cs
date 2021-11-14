using System;

namespace Shadow.Client.Networking.Channels
{
    public abstract class ChannelBase : IDisposable
    {
        protected string ChannelName => GetType().Name;
        public string Host { get; protected set; }
        public int PortBase { get; protected set; }
        public int Port { get; protected set; }
        
        protected ChannelBase(string host, int port, int portOffset)
        {
            Host = host;
            PortBase = port;
            Port = port + portOffset;
        }

        protected void Throw(string msg) => throw new InvalidOperationException($"{ChannelName}: {msg}");

        public abstract void Start();
        public abstract void Listen();
        public abstract void Stop();
        public abstract void Send(byte[] data);

        public abstract void OnDataSent(byte[] data);
        public abstract void OnDataReceived(byte[] data);

        public abstract bool IsChannelActive();
        
        public abstract void Dispose();
    }
}