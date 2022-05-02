using System.Windows;
using Chat.ServiceChatting;
using System.ServiceModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ChatClient
{
    public partial class MainWindow : Window, IServiceChatCallback
    {
        private bool isConnected { get; set; } = false;
        private ServiceChatClient client { get; set; }
        private int Id { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        void ConnectUser()
        {
            // if the user is not logged in
            if (!isConnected)
            {
                // initializing the user
                client = new ServiceChatClient(new InstanceContext(this));
                // connecting the user
                Id = client.Connect(tbUserName.Text);
                // not allowed to change the name
                tbUserName.IsEnabled = false;
                // changing button name
                bDisconCon.Content = "Disconnect";
                // indicating that the user is connected
                isConnected = true;
            }
        }

        void DisconnectUser()
        {
            // if the user is logged in
            if (isConnected)
            {
                // disconnecting the user
                client.Disconnect(Id);
                client = null;
                // allowed to change the name
                tbUserName.IsEnabled = true;
                // changing button name
                bDisconCon.Content = "Connect";
                // indicating that the user is disconnected
                isConnected = false;
            }

        }

        public void CallBackMessage(string message)
        {
            lbChat.Items.Add(message);
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);
        }

        private void Connect_Button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
                DisconnectUser();
            else
                ConnectUser();
        }

        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            //if client is existed
            if (client != null)
                SendMessageFromBuffer();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            //if Enter is pressed and client is existed
            if (e.Key == Key.Enter && client != null)
                SendMessageFromBuffer();
        }

        private void SendMessageFromBuffer()
        {
            // sending message to all of users
            client.SendMessage(tbMessage.Text, Id);
            // clearing the message input buffer
            tbMessage.Clear();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            // if the window is closed, disconnect the user
            DisconnectUser();
        }
    }
}