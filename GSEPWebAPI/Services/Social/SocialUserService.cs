using GSEPWebAPI.App_Start;
using GSEPWebAPI.Helpers.Neo;
using GSEPWebAPI.Models;
using GSEPWebAPI.Models.Repositories.Social;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSEPWebAPI.Services.Social
{
    public class SocialUserService : ISocialUserService
    {
        private ISocialUserRepository _userRepository;
        public SocialUserService()
        {
            this._userRepository = new SocialUserRepository();
        }

        public void AddNotification(Models.Notification noti, string username)
        {
            throw new NotImplementedException();
        }

        public Models.NotificationsResult GetNotifications(string username)
        {
            throw new NotImplementedException();
        }

        public Models.SocialUser GetProfile(string username)
        {
            return _userRepository.GetProfile(username);             
        }

        public void Register(string username)
        {
            _userRepository.Register(username);
        }

        public IEnumerable<Models.UserSearchResult> SearchUser(string keyword)
        {
            return _userRepository.SearchUser(keyword);
        }

        public Models.SocialUser UpdateProfile(string username, Models.EditProfileModel editInfo)
        {
            return _userRepository.UpdateProfile(username, editInfo);
        }


        public bool ShareQuestion(string username, string questionID)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            IEnumerable<SharedQuestionModel> listShare =  neo4jHelper.GetNodes<SharedQuestionModel>(Constants.LABEL_SHARED_QUESTION, Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), Constants.REL_USER_SHARED_QUESTION,
                Constants.LABEL_QUESTION, new KeyString(Constants.KEY_QUESTION, questionID), Constants.REL_QUESTION_SHARED_QUESTION);
            if (listShare != null && listShare.Count() != 0)
            {
                return false;
            }
            return _userRepository.ShareQuestion(username, questionID);
        }
    }
}