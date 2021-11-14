using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Shadow.Client.Networking.Messages.CtrlV2;
using Shadow.Client.Networking.Tcp;

namespace Shadow.Client.Networking.Channels.CtrlV2
{
    public class TcpCtrlV2Channel : SslTcpChannel, ICtrlV2Channel
    {
        public TcpCtrlV2Channel(string host, int port) : base(host, port, 11) { }

        public override void OnDataReceived(byte[] data)
        {
            Console.WriteLine("Received: " + Convert.ToBase64String(data));
        }

        public override void OnDataSent(byte[] data)
        {
        }

        protected override bool OnSslVerifyCert(object sender, X509Certificate cert, X509Chain chain,
            SslPolicyErrors policy)
        {
            Console.WriteLine("Cert: " + cert.ToString());
            return true;
        }

        public void SendMessage(CtrlV2MessageOut message)
        {
             Send(Convert.FromBase64String("AAEAaxIEGgIKACJVCAISBjMuMTAuORpJQW5kcm9pZDthcm02NC12OGE7U2Ftc3VuZyBTTS1BNDA1Rk47MjkoMjkpOzMuNi40IzMwNjA0MHxTYW1zdW5nIFNNLUE0MDVGTioMCAEaCE9DYXB0dXJl"));
        }

        public void OnMessage(CtrlV2MessageIn message)
        {
        }
    }
}