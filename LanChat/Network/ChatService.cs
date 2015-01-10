using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace LanChat.Network
{
    [ServiceContract]
    public interface IChatService
    {
        [OperationContract(IsOneWay = true)]
        void AnnounceParticipant(string nick);

        [OperationContract(IsOneWay = true)]
        void RequestAnnouncement();

        [OperationContract(IsOneWay = true)]
        void SendMessage(string nick, string message);
    }

    [ServiceBehavior()]
    public class ChatService : IChatService
    {
        public void AnnounceParticipant(string nick)
        {
            var channel = OperationContext.Current.Channel;
            if (channel.RemoteAddress == channel.LocalAddress)
                return;

            Engine.AnnounceParticipant(nick);
        }

        public void RequestAnnouncement()
        {
            var channel = OperationContext.Current.Channel;
            if (channel.RemoteAddress == channel.LocalAddress)
                return;

            Engine.AnnounceSelf();
        }

        public void SendMessage(string nick, string message)
        {
            //var channel = OperationContext.Current.Channel;
            //if (channel.RemoteAddress == channel.LocalAddress)
            //    return;

            Engine.ReceiveMessage(nick, message);
        }

        private static ChatEngine Engine
        {
            get { return OperationContext.Current.Host.Description.Behaviors.Find<ChatEngine>(); }
        }
    }
}
