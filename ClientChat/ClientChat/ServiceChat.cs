using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ClientChat
{
    // defines how our service works
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        private List<ServerUser> users = new List<ServerUser>();
        private int nextId = 0;
        
        // when entering the chat
        public int Connect(string name)
        {
            // create a new user and initialize the appropriate fields
            ServerUser user = new ServerUser()
            {
                Id = nextId++,
                Name = name,
                operationContext = OperationContext.Current
            };

            // informing other users about the appearance of a new user
            SendMessage($" {user.Name} подключился к чату.", -1);
            // and adding user
            users.Add(user);

            return user.Id;
        }
        
        public void Disconnect(int id)
        {
            // looking for a user who wants to log out
            var user = users.FirstOrDefault(x => x.Id == id);

            // if we found one
            if (user != null)
            {
                // removing the user from the chat and
                users.Remove(user);
                // informing other users about the disappearance of the user
                SendMessage($" {user.Name} покинул чат.", -1);
            }
        }

        public void SendMessage(string message, int id)
        {
            // iterating over all users
            foreach (var item in users)
            {
                // creating a response
                StringBuilder answer = new StringBuilder(DateTime.Now.ToShortTimeString());
                var user = users.FirstOrDefault(x => x.Id == id);

                if (user != null)
                    answer.Append($" {user.Name}: ");

                answer.Append(message);

                // sending a message to other users
                item.operationContext.GetCallbackChannel<IServerChatCallBack>().
                    CallBackMessage(answer.ToString());
            }
        }
    }
}
