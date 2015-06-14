using GSEPWebAPI.Models;
using System;
namespace GSEPWebAPI.Services.Social
{
    public interface ISocialCommentService
    {
        Comment Comment(string username, string postID, string content);
        bool DeleteComment(string commentID, string username);
        Comment EditComment(string commentID, string content, string username);
        CommentResultModel GetComment(string commentID);
        bool LikeUnLikeComment(string username, string commentID);
    }
}
