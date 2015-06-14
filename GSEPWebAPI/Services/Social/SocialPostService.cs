using GSEPWebAPI.App_Start;
using GSEPWebAPI.Helpers;
using GSEPWebAPI.Helpers.Neo;
using GSEPWebAPI.Models;
using GSEPWebAPI.Models.Repositories.Social;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSEPWebAPI.Services.Social
{
    public class SocialPostService : ISocialPostService
    {
        private ISocialPostRepository _postRepository;
        public SocialPostService()
        {
            this._postRepository = new SocialPostRepository();
        }


        public bool DeletePost(string postID,string username)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            if (neo4jHelper.GetNode<Post>(Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID)) == null)
            {
                throw new Exception(Constants.ERROR_POST_NOT_EXIST);
            }
            if (!neo4jHelper.IsRelate(Constants.REL_USER_POST, Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID)))
            {
                throw new Exception(Constants.ERROR_POST_NOT_RELATE_USER);
            }
            return _postRepository.DeletePost(postID);
        }

        public Post EditPost(string postID, string content, string username)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            if (neo4jHelper.GetNode<Post>(Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID)) == null)
            {
                throw new Exception(Constants.ERROR_POST_NOT_EXIST);
            }
            if (!neo4jHelper.IsRelate(Constants.REL_USER_POST, Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID)))
            {
                throw new Exception(Constants.ERROR_POST_NOT_RELATE_USER);
            }
            return _postRepository.EditPost(postID, content);
        }

        public void Following(string username, string postID)
        {
            _postRepository.Following(username, postID);
        }

        public IEnumerable<CommentResultModel> GetComments(string postID)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            if (neo4jHelper.GetNode<Post>(Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID)) == null)
            {
                throw new Exception(Constants.ERROR_POST_NOT_EXIST);
            }
            IEnumerable<Comment> comments = _postRepository.GetComments(postID);
            List<CommentResultModel> commentResult = new List<CommentResultModel>();
            foreach (var comment in comments)
            {
                SocialUser userOwner = neo4jHelper.GetNodes<SocialUser>(Constants.LABEL_COMMENT, new KeyString(Constants.KEY_COMMENT, comment.CommentID), Constants.LABEL_USER, Constants.REL_USER_COMMENT).FirstOrDefault();
                commentResult.Add(new CommentResultModel(comment, userOwner.Username, userOwner.FirstName + " " + userOwner.LastName));
            }
            return commentResult;
        }

        public IEnumerable<TimelineAndNewsfeedModel> GetNewsfeed(string username)
        {
            //IEnumerable<Post> posts = _postRepository.GetNewsfeed(username);
            //List<PostResultModel> postResult = new List<PostResultModel>();
            //Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            //foreach (var post in posts)
            //{                
            //    SocialUser userOwner = neo4jHelper.GetNodes<SocialUser>(Constants.LABEL_POST, 
            //        new KeyString(Constants.KEY_POST, post.PostID), Constants.LABEL_USER,
            //        Constants.REL_USER_POST).FirstOrDefault();
            //    postResult.Add(new PostResultModel(post, userOwner.Username, userOwner.FirstName + " " + userOwner.LastName));
            //}

            return _postRepository.GetNewsfeed(username);
        }

        public bool LikeUnLikePost(string username, string postID)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            if (neo4jHelper.GetNode<Post>(Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID)) == null)
            {
                throw new Exception(Constants.ERROR_POST_NOT_EXIST);
            }
            bool result = _postRepository.LikeUnLikePost(username, postID);
            if (result)
            {
                // Update timestamp for this post to display in newsfeed
                Post post = _postRepository.ViewPost(postID);
                _postRepository.EditPost(postID, post.Content);
                // Add notification to owner of post
                // Find post owner
                var listUser = neo4jHelper.GetNodes<SocialUser>(Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID), Constants.LABEL_USER, Constants.REL_USER_POST);
                if (listUser != null && listUser.Count() != 0)
                {
                    SocialUser user = listUser.FirstOrDefault();
                    // If user like post is not owner of post then add to notifications
                    if (!user.Username.Equals(username))
                    {
                        SocialUserRepository userRepo = new SocialUserRepository();
                        string title = username + Constants.MESSAGE_NOTIFICATION_LIKE_POST;
                        Notification noti = new Notification(Constants.TimeStamp(), postID, null, title);
                        userRepo.AddNotification(noti, user.Username);
                    }
                }
            }
            return result;
        }

        public Post Post(string username, string content)
        {
            return _postRepository.Post(username, content);
        }

        public bool UnFollow(string username, string postID)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            if (neo4jHelper.GetNode<Post>(Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID)) == null)
            {
                throw new Exception(Constants.ERROR_POST_NOT_EXIST);
            }
            if (!neo4jHelper.IsRelate(Constants.REL_USER_FOLLOW_POST, Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID)))
            {
                throw new Exception(Constants.ERROR_USER_NOT_FOLLOW_POST);
            }
            return _postRepository.UnFollow(username, postID);
        }

        public PostResultModel ViewPost(string postID)
        {
            Post post = _postRepository.ViewPost(postID);
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            SocialUser userOwner = neo4jHelper.GetNodes<SocialUser>(Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID), Constants.LABEL_USER, Constants.REL_USER_POST).FirstOrDefault();
            return new PostResultModel(post, userOwner.Username, userOwner.FirstName + " " + userOwner.LastName);
        }

    }
}