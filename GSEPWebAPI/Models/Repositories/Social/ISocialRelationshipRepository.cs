using System;
namespace GSEPWebAPI.Models.Repositories.Social
{
    public interface ISocialRelationshipRepository
    {
        bool CancelFriendRequest(string username, string targetUsername);
        Relationship GetRelationship(string username, string targetUsername);
        bool ReponseRequest(string username, string targetUsername, bool accept);
        Relationship SendFriendRequest(string username, string targetUsername);
        bool Unfriend(string username, string targetUsername);
    }
}
