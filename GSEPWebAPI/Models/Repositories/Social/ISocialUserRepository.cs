using System;
using System.Collections.Generic;
namespace GSEPWebAPI.Models.Repositories.Social
{
    public interface ISocialUserRepository
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
