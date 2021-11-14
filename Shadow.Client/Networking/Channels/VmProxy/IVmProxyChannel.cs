using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Shadow.Client.Networking.Messages;
using Shadow.Client.Networking.Messages.VmProxy;
using Shadow.Client.Networking.Messages.VmProxy.In;

namespace Shadow.Client.Networking.Channels.VmProxy
{
    public interface IVmProxyChannel : IMessageChannel<VmProxyMessageIn, VmProxyMessageOut>
    {

        #region Messages registration
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
        /// <param name="modelType"></param>
        /// <returns></returns>
        static bool TryGetMessageModel(string cmd, out Type modelType)
        {
            return (modelType = RegisteredMessages.FirstOrDefault(x => x.Resp == cmd || x.Cmd == cmd)?.GetType()) != null;
        }
        #endregion
        
        public IVmProxyChannel VmProxyChannel { get; }

        void HandleInData(byte[] data)
        {
            var jsonText = Encoding.Default.GetString(data);
            var message = JsonSerializer.Deserialize<VmProxyMessageIn>(jsonText);
            var messageIdentifier = message.Cmd ?? message.Resp;

            if (!string.IsNullOrEmpty(messageIdentifier) && TryGetMessageModel(messageIdentifier, out var model))
                OnMessage((VmProxyMessageIn) JsonSerializer.Deserialize(jsonText, model));
        }

        byte[] SerializeMessage(VmProxyMessageOut message)
        {
            using var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            message.Write(writer);
            return stream.ToArray();
        }
    }
}