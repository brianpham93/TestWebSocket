using System;
using System.Collections.Generic;
namespace GSEPWebAPI.Models.Repositories.Social
{
    public interface ISocialPostRepository
    {
        bool DeletePost(string postID);
        Post EditPost(string postID, string content);
        void Following(string username, string postID);
        IEnumerable<Comment> GetComments(string postID);
        IEnumerable<TimelineAndNewsfeedModel> GetNewsfeed(string username);
        bool LikeUnLikePost(string username, string postID);
        Post Post(string username, string content);
        bool UnFollow(string username, string postID);
        Post ViewPost(string postID);
    }
}
