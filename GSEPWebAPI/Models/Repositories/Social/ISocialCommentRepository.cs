using System;
namespace GSEPWebAPI.Models.Repositories.Social
{
    public interface ISocialCommentRepository
    {
        Comment Comment(string username, string postID, string content);
        bool DeleteComment(string commentID);
        Comment EditComment(string commentID, string content);
        Comment GetComment(string commentID);
        bool LikeUnLikeComment(string username, string commentID);
    }
}
