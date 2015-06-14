using GSEPWebAPI.Models;
using System;
using System.Collections.Generic;
namespace GSEPWebAPI.Services.Social
{
    public interface ISocialMessageService
    {
        IEnumerable<MessageConversation> GetConversation(string username1, string username2);
        Message GetMessage(string messageID);
        IEnumerable<Message> GetMessages(string sendUsername, string receiveUsername);
        Message SendMessage(string sendUsername, string receiveUsername, string content);
    }
}
