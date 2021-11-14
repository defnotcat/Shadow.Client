namespace Shadow.Client.Networking.Channels
{
    public interface IMessageChannel<in TIn, in TOut>
    {

        void SendMessage(TOut message);
        void OnMessage(TIn message);

    }
}