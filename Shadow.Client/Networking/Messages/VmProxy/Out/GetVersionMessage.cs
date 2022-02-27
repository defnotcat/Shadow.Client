namespace Shadow.Client.Networking.Messages.VmProxy.Out
{
    public class GetVersionMessage : VmProxyMessageOut
    {

        public GetVersionMessage(int id) : base("get-version", id)
        {
            Id = id;
        }
        
    }
}