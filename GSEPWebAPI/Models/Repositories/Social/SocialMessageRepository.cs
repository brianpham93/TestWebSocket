using GSEPWebAPI.App_Start;
using GSEPWebAPI.Helpers;
using GSEPWebAPI.Helpers.Neo;
using GSEPWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSEPWebAPI.Models.Repositories.Social
{
    public class SocialMessageRepository : ISocialMessageRepository
    {
        public SocialMessageRepository() : base() { }
        public Message SendMessage(string sendUsername, string receiveUsername, string content)
        {
            string timeStamp = Constants.TimeStamp();
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            Message mess = new Message(timeStamp, content, double.Parse(timeStamp));
            neo4jHelper.CreateNode(mess, Constants.LABEL_MESSAGE);
            neo4jHelper.CreateRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, sendUsername), Constants.LABEL_MESSAGE, new KeyString(Constants.KEY_MESSAGE, timeStamp), Constants.REL_USER_SEND_MESSAGE, default(Object));
            neo4jHelper.CreateRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, receiveUsername), Constants.LABEL_MESSAGE, new KeyString(Constants.KEY_MESSAGE, timeStamp), Constants.REL_USER_RECEIVE_MESSAGE, default(Object));
            return mess;
        }

        public Message GetMessage(string messageID)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            Message mess = neo4jHelper.GetNode<Message>(Constants.LABEL_MESSAGE, new KeyString(Constants.KEY_MESSAGE, messageID));
            return mess;
        }

        public IEnumerable<Message> GetMessages(string sendUsername, string receiveUsername)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            IEnumerable<Message> results = neo4jHelper.GetNodes<Message>(Constants.LABEL_MESSAGE,
                                                 Constants.LABEL_USER, new KeyString(Constants.KEY_USER, sendUsername), Constants.REL_USER_SEND_MESSAGE,
                                                 Constants.LABEL_USER, new KeyString(Constants.KEY_USER, receiveUsername), Constants.REL_USER_RECEIVE_MESSAGE);
            return results.OrderByDescending(mess => mess.MessageID);            
        }

        public IEnumerable<MessageConversation> GetConversation(string username1, string username2)
        {            
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            IEnumerable<Message> result1 = neo4jHelper.GetNodes<Message>(Constants.LABEL_MESSAGE,
                                                 Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username1), Constants.REL_USER_SEND_MESSAGE,
                                                 Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username2), Constants.REL_USER_RECEIVE_MESSAGE);
            IEnumerable<Message> result2 = neo4jHelper.GetNodes<Message>(Constants.LABEL_MESSAGE,
                                                 Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username2), Constants.REL_USER_SEND_MESSAGE,
                                                 Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username1), Constants.REL_USER_RECEIVE_MESSAGE);
            List<MessageConversation> messageCons = new List<MessageConversation>();
            foreach (var mess in result1)
            {
                MessageConversation messCon = new MessageConversation(username1, mess);
                messageCons.Add(messCon);
            }
            foreach (var mess in result2)
            {
                MessageConversation messCon = new MessageConversation(username2, mess);
                messageCons.Add(messCon);
            }
            return messageCons.OrderByDescending(messCon => messCon.Message.MessageID);            
        }
    }
}