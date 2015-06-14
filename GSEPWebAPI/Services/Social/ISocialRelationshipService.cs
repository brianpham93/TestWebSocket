using GSEPWebAPI.Models;
using System;
using System.Collections.Generic;
namespace GSEPWebAPI.Services.Social
{
    public interface ISocialRelationshipService
    {
        bool CancelFriendRequest(string username, string targetUsername);
        IEnumerable<Relationship> GetRelationship(string username, string targetUsername);
        bool ReponseRequest(string username, string targetUsername, bool accept);
        Relationship SendFriendRequest(string username, string targetUsername);
        bool Unfriend(string username, string targetUsername);
    }
}
