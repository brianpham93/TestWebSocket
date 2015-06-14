using GSEPWebAPI.Models;
using System;
using System.Collections.Generic;
namespace GSEPWebAPI.Services.Social
{
    public interface ISocialPostService
    {
        bool DeletePost(string postID, string username);
        Post EditPost(string postID, string content, string username);
        void Following(string username, string postID);
        IEnumerable<CommentResultModel> GetComments(string postID);
        IEnumerable<TimelineAndNewsfeedModel> GetNewsfeed(string username);
        bool LikeUnLikePost(string username, string postID);
        Post Post(string username, string content);
        bool UnFollow(string username, string postID);
        PostResultModel ViewPost(string postID);
    }
}
