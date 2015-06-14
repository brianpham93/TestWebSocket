using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GSEPWebAPI.Models;

namespace GSEPWebAPI.Services.Social
{
    public class SocialMessageService : ISocialMessageService
    {
        public IEnumerable<MessageConversation> GetConversation(string username1, string username2)
        {
            throw new NotImplementedException();
        }

        public Message GetMessage(string messageID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetMessages(string sendUsername, string receiveUsername)
        {
            throw new NotImplementedException();
        }

        public Message SendMessage(string sendUsername, string receiveUsername, string content)
        {
            throw new NotImplementedException();
        }
    }
}