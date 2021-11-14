namespace Shadow.Client.Networking.Messages
{
    public interface IMessageChannel<in TIn, in TOut>
    {

        void SendMessage(TOut message);
        void OnMessage(TIn message);

    }
}