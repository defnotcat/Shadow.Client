using System;
using System.Collections.Generic;
using System.Linq;
using Shadow.Client.Networking.Messages;
using Shadow.Client.Networking.Messages.VmProxy;
using Shadow.Client.Networking.Messages.VmProxy.In;

namespace Shadow.Client.Networking.Channels.VmProxy
{
    public interface IVmProxyChannel : IMessageChannel<VmProxyMessageIn, VmProxyMessageOut>
    {
        private static readonly List<VmProxyMessageIn> RegisteredMessages = new()
        {
            new GetVersionMessage(),
            new HelloMessage(),
            new ShadowStatusMessage()
        };

        /// <summary>
        /// Get message model from identifiers resp or cmd
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        static bool TryGetMessageModel(string cmd, out VmProxyMessageIn message)
        {
            return (message = RegisteredMessages.FirstOrDefault(x => x.Resp == cmd || x.Cmd == cmd)) != null;
        }
        
    }
}