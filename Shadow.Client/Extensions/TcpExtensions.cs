using System.Net;
using System.Net.Sockets;

namespace Shadow.Client.Extensions
{
    public static class TcpExtensions
    {

        public static (string Host, int Port) GetRemoteAddress(this TcpClient tcpClient)
        {
            var endpoint = tcpClient.Client.RemoteEndPoint as IPEndPoint;
            var ipAddress = endpoint.Address;
            return (ipAddress.ToString(), endpoint.Port);
        }

    }
}