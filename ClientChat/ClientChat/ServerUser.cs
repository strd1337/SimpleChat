using System.ServiceModel;

namespace ClientChat
{
    public class ServerUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // information about connecting the client to the service
        public OperationContext operationContext { get; set; }
    }
}
