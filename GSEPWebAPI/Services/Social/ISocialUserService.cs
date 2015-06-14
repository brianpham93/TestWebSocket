using GSEPWebAPI.Models;
using System;
using System.Collections.Generic;
namespace GSEPWebAPI.Services.Social
{
    public interface ISocialUserService
    {
        void AddNotification(Notification noti, string username);
        NotificationsResult GetNotifications(string username);
        SocialUser GetProfile(string username);
        void Register(string username);
        IEnumerable<UserSearchResult> SearchUser(string keyword);
        SocialUser UpdateProfile(string username, GSEPWebAPI.Models.EditProfileModel editInfo);
        bool ShareQuestion(string username, string questionID);
    }
}
