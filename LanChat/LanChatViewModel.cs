using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using LanChat.Network;

namespace LanChat
{
    public class LanChatViewModel : INotifyPropertyChanged
    {
        public DelegateCommand LoginCommand { get; private set; }
        public string Nick
        {
            get { return this.myNick; }
            set
            {
                this.myNick = value;
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string WindowTitle
        {
            get { return this.myWindowTitle; }
            set
            {
                this.myWindowTitle = value;
                this.OnPropertyChanged("WindowTitle");
            }
        }

        public ObservableCollection<ChatMessage> MessageHistory { get; private set; }

        public ObservableCollection<string> ChatParticipants { get; private set; }

        private ChatEngine engine;
        private string myNick;
        private string myWindowTitle;

        public LanChatViewModel()
        {
            LoginCommand = new DelegateCommand(_ => !String.IsNullOrEmpty(this.Nick), Login);
            MessageHistory = new ObservableCollection<ChatMessage>();
            ChatParticipants = new ObservableCollection<string>();

            myWindowTitle = "not logged in";

            CollectionViewSource.GetDefaultView(ChatParticipants).SortDescriptions.Add(new SortDescription());

            this.engine = new ChatEngine();
            this.engine.Initialize();

            this.engine.MessageReceived += EngineOnMessageReceived;
            this.engine.AddKnownParticipant += EngineOnAddKnownParticipant;
        }

        private void EngineOnAddKnownParticipant(object sender, UserEventArgs eventArgs)
        {
            var nick = eventArgs.Nick;
            if (!ChatParticipants.Contains(nick))
                ChatParticipants.Add(nick);
        }

        private void Login(object _)
        {
            engine.Login(this.Nick);
            WindowTitle = "logged as " + this.Nick;
        }

        private void EngineOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            MessageHistory.Add(new ChatMessage
            {
                Message = messageEventArgs.Message,
                Nick = messageEventArgs.Nick
            });
        }

        public void Say(string message)
        {
            this.engine.Send(message);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ChatMessage
    {
        public string Nick { get; set; }
        public string Message { get; set; }
    }
}
