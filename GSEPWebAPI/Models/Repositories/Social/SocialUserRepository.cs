using GSEPWebAPI.App_Start;
using GSEPWebAPI.Helpers;
using GSEPWebAPI.Helpers.Neo;
using GSEPWebAPI.Models;
using Neo4jClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSEPWebAPI.Models.Repositories.Social
{
    public class SocialUserRepository : ISocialUserRepository
    {
        public SocialUserRepository() : base() { }
        public void Register(string username)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            var newUser = new SocialUser { Username = username, FirstName = null, LastName = null, Year = 1970, Month = 01, Day = 01, Gender = null, Country = null, AvatarURL = null };
            neo4jHelper.CreateNode(newUser, Constants.LABEL_USER);
            // Create node notifications for user also            
            CreateNotificationsNode(username);
        }

        private NotificationsNeo CreateNotificationsNode(string username)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            NotificationsNeo notifications = new NotificationsNeo(Constants.NotificationsIDValue(username), null);
            neo4jHelper.CreateNode(notifications, Constants.LABEL_NOTIFICATIONS);
            neo4jHelper.CreateRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), Constants.LABEL_NOTIFICATIONS, new KeyString(Constants.KEY_NOTIFICATIONS, notifications.NotificationsID), Constants.REL_USER_NOTIFICATIONS, default(Object));
            return notifications;
        }

        public SocialUser GetProfile(string username)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            SocialUser social = neo4jHelper.GetNode<SocialUser>(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username));
            return social;
        }

        public SocialUser UpdateProfile(string username, EditProfileModel editInfo)
        {
            SocialUser user = new SocialUser { Username = username, FirstName = editInfo.FirstName, LastName = editInfo.LastName, Year = editInfo.Year, Month = editInfo.Month, Day = editInfo.Day, Gender = editInfo.Gender, Country = editInfo.Country, AvatarURL = editInfo.AvatarURL };
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            neo4jHelper.UpdateNode(user, Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username));
            return user;
        }

        public IEnumerable<UserSearchResult> SearchUser(string keyword)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            return neo4jHelper.SearchNodes<UserSearchResult>(Constants.LABEL_USER, keyword, new string[] { "Username", "FirstName", "LastName" });
        }

        public void AddNotification(Notification noti, string username)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            var result = neo4jHelper.GetNodes<NotificationsNeo>(Constants.LABEL_NOTIFICATIONS, Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), Constants.REL_USER_NOTIFICATIONS);
            NotificationsNeo notis;
            if (result == null || result.Count() == 0)
            {
                notis = CreateNotificationsNode(username);
            }
            else
            {
                notis = result.FirstOrDefault();
            }
            if (notis != null)
            {
                List<Notification> listNoti = new List<Notification>();
                if (notis.Content != null)
                {
                    IEnumerable<Notification> objectListNoti = JsonConvert.DeserializeObject<IEnumerable<Notification>>(notis.Content);
                    listNoti.AddRange(objectListNoti);
                }
                if (listNoti != null)
                {
                    if (listNoti.Count() > 100)
                    {
                        listNoti.RemoveAt(listNoti.Count() - 1);
                    }
                }
                listNoti.Insert(0, noti);
                string content = JsonConvert.SerializeObject(listNoti);
                notis.Content = content;
                neo4jHelper.UpdateNode(notis, Constants.LABEL_NOTIFICATIONS, new KeyString(Constants.KEY_NOTIFICATIONS, notis.NotificationsID));
            }

        }

        public NotificationsResult GetNotifications(string username)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            var result = neo4jHelper.GetNodes<NotificationsNeo>(Constants.LABEL_NOTIFICATIONS, Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), Constants.REL_USER_NOTIFICATIONS);
            if (result != null && result.Count() != 0)
            {
                NotificationsNeo notis = result.FirstOrDefault();
                if (notis != null)
                {
                    if (notis.Content != null)
                    {
                        IEnumerable<Notification> objectListNoti = JsonConvert.DeserializeObject<IEnumerable<Notification>>(notis.Content);
                        NotificationsResult notifications = new NotificationsResult(notis.NotificationsID, objectListNoti);
                        return notifications;
                    }
                }
            }
            return null;
        }

        //public string DeleteAccount(string username)
        //{
        //    //todo Code for DeleteAccount
        //    return null;
        //}


        public bool ShareQuestion(string username, string questionID)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            String timestamp = Constants.TimeStamp();
            SharedQuestionModel sharedQuestion = new SharedQuestionModel(timestamp, double.Parse(timestamp), true);
            neo4jHelper.CreateNode(sharedQuestion, Constants.LABEL_SHARED_QUESTION);

            //User - Shared question relationship.
            neo4jHelper.CreateRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), 
                Constants.LABEL_SHARED_QUESTION, new KeyString(Constants.KEY_SHARED_QUESTION, timestamp),
                Constants.REL_USER_SHARED_QUESTION, default(Object));

            //Question - Shared question relationship.
            neo4jHelper.CreateRelationShip(Constants.LABEL_QUESTION, new KeyString(Constants.KEY_QUESTION, questionID),
                Constants.LABEL_SHARED_QUESTION, new KeyString(Constants.KEY_SHARED_QUESTION, timestamp),
                Constants.REL_QUESTION_SHARED_QUESTION, default(Object));                                
            //neo4jHelper.CreateRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), Constants.LABEL_QUESTION, new KeyString(Constants.KEY_QUESTION, questionID), Constants.REL_USER_SHARED_QUESTION, default(Object));
            return true;
        }
    }

}