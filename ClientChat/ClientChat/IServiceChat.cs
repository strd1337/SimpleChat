using System.ServiceModel;


namespace ClientChat
{
    // description of what the service can do
    
    // let the server know about IServiceChatCallBack
    [ServiceContract(CallbackContract = typeof(IServerChatCallBack))]
    public interface IServiceChat
    {
        [OperationContract]
        int Connect(string name);
        [OperationContract]
        void Disconnect(int id);
        // do not wait for a response from the server when sending a message
        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, int id); 
    }

    public interface IServerChatCallBack
    {
        [OperationContract(IsOneWay = true)]
        void CallBackMessage(string msg);
    }
    
}
