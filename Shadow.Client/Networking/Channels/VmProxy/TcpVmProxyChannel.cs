using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Shadow.Client.Networking.Channels.Tcp;
using Shadow.Client.Networking.Messages.VmProxy;

namespace Shadow.Client.Networking.Channels.VmProxy
{
    public class TcpVmProxyChannel : SslTcpChannel, IVmProxyChannel
    {
        // To avoid having to cast this to IVMProxyChannel every time
        public IVmProxyChannel VmProxyChannel => this;

        public TcpVmProxyChannel(string host, int portBase) : base(host, portBase, 1) { }

        public void SendMessage(VmProxyMessageOut message) => Send(VmProxyChannel.SerializeMessage(message));

        public void OnMessage(VmProxyMessageIn message)
        {
            Console.WriteLine($"\nOnMessage: {message.GetType().FullName}");
        }

        public override void OnDataReceived(byte[] data)
        {
            VmProxyChannel.HandleInData(data);
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
    }
}