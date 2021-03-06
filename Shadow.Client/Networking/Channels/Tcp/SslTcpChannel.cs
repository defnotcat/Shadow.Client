using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Shadow.Client.Extensions;

namespace Shadow.Client.Networking.Channels.Tcp
{
    public abstract class SslTcpChannel : TcpChannel
    {
        protected SslStream SslStream => Stream as SslStream;
        
        protected SslTcpChannel(string host, int portBase, int portOffset) : base(host, portBase, portOffset) { }

        public virtual bool AuthenticateSslStream()
        {
            try
            {
                SslStream.AuthenticateAsClient(TcpClient.GetRemoteAddress().Host);
                return true;
            }
            catch(Exception exp)
            {
                Console.WriteLine("Ssl Negotiation exp:");
                Console.WriteLine(exp.Message);
                Console.WriteLine(exp.StackTrace);
                return false;
            }
        }

        protected abstract bool OnSslVerifyCert(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors policy);
        
        public override void Start()
        {
            base.Start();
            Stream = new SslStream(Stream, false, OnSslVerifyCert, null);
            if (!AuthenticateSslStream()) Throw("Failed to convert stream to SSL stream");
            Console.WriteLine("Stream upgraded to SSL stream successfully");
        }
    }
}