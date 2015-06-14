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
    public class SocialCommentService : ISocialCommentService
    {
        private SocialCommentRepository _commentRepository;
        public SocialCommentService()
        {
            this._commentRepository = new SocialCommentRepository();
        }
        public Comment Comment(string username, string postID, string content)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            if (neo4jHelper.GetNode<Post>(Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID)) == null)
            {
                throw new Exception(Constants.ERROR_POST_NOT_EXIST);
            }
            // Find post owner
            SocialUser postOwner = null;
            var listUser = neo4jHelper.GetNodes<SocialUser>(Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID), Constants.LABEL_USER, Constants.REL_USER_POST);
            if (listUser != null && listUser.Count() != 0)
            {
                postOwner = listUser.FirstOrDefault();
            }
            else
            {
                throw new Exception(Constants.ERROR_POST_NOT_RELATE_ANY);
            }
            // Comment to post
            Comment comment = null;
            SocialPostRepository postRepo = new SocialPostRepository();
            SocialUserRepository userRepo = new SocialUserRepository();
            // if user is commented is post owner(Don't need to set following and notification to post owner)
            // else check if 2 user is friend, if not do nothing and return Error
            if (postOwner.Username.Equals(username))
            {
                // Comment to post
                comment = _commentRepository.Comment(username, postID, content);
                if (comment == null)
                {
                    return comment;
                }
                // Update timestamp for post to display in newsfeed
                Post post = postRepo.ViewPost(postID);
                postRepo.EditPost(postID, post.Content);
            }
            else
            {
                // Check if 2 user is friend
                Relationship relationship = neo4jHelper.GetRelationShip<Relationship>(
                                        Constants.LABEL_USER, new KeyString(Constants.KEY_USER, postOwner.Username),
                                        Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username),
                                        Constants.REL_USER_USER);
                if (relationship != null && relationship.Status != null && relationship.Status.Equals(Constants.REL_STATUS_FRIEND))
                {
                    // Comment to post
                    comment = _commentRepository.Comment(username, postID, content);
                    if (comment == null)
                    {
                        return comment;
                    }
                    // Update timestamp for post to display in newsfeed
                    Post post = postRepo.ViewPost(postID);
                    postRepo.EditPost(postID, post.Content);

                    // Set user follow this post, only set if post is not follow by user
                    if (!neo4jHelper.IsRelate(Constants.REL_USER_FOLLOW_POST, Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID)))
                    {
                        postRepo.Following(username, postID);
                    }
                    // Add notification to post owner                            
                    string title = username + Constants.MESSAGE_NOTIFICATION_COMMENT_TO_POST;
                    Notification noti = new Notification(Constants.TimeStamp(), postID, null, title);
                    userRepo.AddNotification(noti, postOwner.Username);

                }
                else
                {
                    throw new Exception(Constants.ERROR_TWO_USER_NOT_FRIEND);
                }
            }
            // Add notification to users who following this post (Except user is comment now)
            // Find all users who following this post
            var usersFollowing = neo4jHelper.GetNodes<SocialUser>(Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID), Constants.LABEL_USER, Constants.REL_USER_FOLLOW_POST);
            foreach (var userFollow in usersFollowing)
            {
                if (!userFollow.Username.Equals(username))
                {
                    string title = username + Constants.MESSAGE_NOTIFICATION_COMMENT_TO_POST_FOLLOWING;
                    Notification noti = new Notification(Constants.TimeStamp(), postID, null, title);
                    userRepo.AddNotification(noti, userFollow.Username);
                }
            }
            return comment;
        }

        public bool DeleteComment(string commentID, string username)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            if (neo4jHelper.GetNode<Comment>(Constants.LABEL_COMMENT, new KeyString(Constants.KEY_COMMENT, commentID)) == null)
            {
                throw new Exception(Constants.ERROR_COMMENT_NOT_EXIST);
            }
            if (!neo4jHelper.IsRelate(Constants.REL_USER_COMMENT, Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), Constants.LABEL_COMMENT, new KeyString(Constants.KEY_COMMENT, commentID)))
            {
                throw new Exception(Constants.ERROR_COMMENT_NOT_RELATE_USER);
            }
            return _commentRepository.DeleteComment(commentID);
        }

        public Comment EditComment(string commentID, string content, string username)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            if (neo4jHelper.GetNode<Comment>(Constants.LABEL_COMMENT, new KeyString(Constants.KEY_COMMENT, commentID)) == null)
            {
                throw new Exception(Constants.ERROR_COMMENT_NOT_EXIST);
            }
            if (!neo4jHelper.IsRelate(Constants.REL_USER_COMMENT, Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), Constants.LABEL_COMMENT, new KeyString(Constants.KEY_COMMENT, commentID)))
            {
                throw new Exception(Constants.ERROR_COMMENT_NOT_RELATE_USER);
            }
            return _commentRepository.EditComment(commentID, content);
        }

        public CommentResultModel GetComment(string commentID)
        {
            Comment comment = _commentRepository.GetComment(commentID);
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            SocialUser userOwner = neo4jHelper.GetNodes<SocialUser>(Constants.LABEL_COMMENT, new KeyString(Constants.KEY_COMMENT, comment.CommentID), Constants.LABEL_USER, Constants.REL_USER_COMMENT).FirstOrDefault();
            return new CommentResultModel(comment, userOwner.Username, userOwner.FirstName + " " + userOwner.LastName);

        }

        public bool LikeUnLikeComment(string username, string commentID)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            if (neo4jHelper.GetNode<Post>(Constants.LABEL_COMMENT, new KeyString(Constants.KEY_COMMENT, commentID)) == null)
            {
                throw new Exception(Constants.ERROR_COMMENT_NOT_EXIST);
            }
            bool result = _commentRepository.LikeUnLikeComment(username, commentID);
            if (result)
            {
                // Add notification to owner of post
                // Find comment owner
                var listUser = neo4jHelper.GetNodes<SocialUser>(Constants.LABEL_COMMENT, new KeyString(Constants.KEY_COMMENT, commentID), Constants.LABEL_USER, Constants.REL_USER_COMMENT);
                if (listUser != null && listUser.Count() != 0)
                {
                    SocialUser user = listUser.FirstOrDefault();
                    // If user like post is not owner of post then add to notifications
                    if (!user.Username.Equals(username))
                    {
                        SocialUserRepository userRepo = new SocialUserRepository();
                        string title = username + Constants.MESSAGE_NOTIFICATION_LIKE_COMMENT;
                        Notification noti = new Notification(Constants.TimeStamp(), null, commentID, title);
                        userRepo.AddNotification(noti, user.Username);
                    }
                }
            }
            return result;
        }
    }
}