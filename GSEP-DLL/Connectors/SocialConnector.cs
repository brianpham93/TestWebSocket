using GSEP_DLL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace GSEP_DLL.Connectors
{
    public static class SocialConnector
    {
        
        /// <summary>
        /// Get profile of user who have username = username
        /// </summary>
        /// <param name="token">token of logged in to system</param>
        /// <param name="username">Username cua thang ma muon xem profile (VD: User login la trungnt va muon xem 
        /// profile cua user NguyenThuyAn. Thi token se la token cua trungnt, username se la NguyenThuyAn</param>
        /// <returns></returns>
        public static SocialUser SeeProfile(string token, string username)
        {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_USER_GET_PROFILE + "?username="+username;
            SocialUser result = common.HttpGet<SocialUser>(url, token);
            return result;
        }

        /// <summary>
        /// View personal profile
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static SocialUser SeeProfile(string token)
        {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_USER_GET_PROFILE;
            SocialUser result = common.HttpGet<SocialUser>(url, token);
            return result;
        }

        /// <summary>
        /// Edit personal profile
        /// </summary>
        /// <param name="editInfo">Info needs updating</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static SocialUser EditProfile(EditProfileModel editInfo, string token)
        {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_USER_UPDATE_PROFILE;           
            SocialUser result = common.HttpPut<SocialUser, EditProfileModel>(url, editInfo, token);
            return result;            
        }

        /// <summary>
        /// Search user by a keyword
        /// </summary>
        /// <param name="token"></param>
        /// <param name="keyword">keyword</param>
        /// <returns></returns>
        public static List<UserSearchResult> SearchUser(String token, String keyword)
        {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_USER_SEARCH + "?keyword="+keyword;
            IEnumerable<UserSearchResult> result = common.HttpGet<IEnumerable<UserSearchResult>>(url, token);
            if (result == null)
            {
                return null;
            }
            else
            {
                List<UserSearchResult> listUser = new List<UserSearchResult>();
                listUser.AddRange(result);
                return listUser;
            }            
        }

        /// <summary>
        /// Get personal notifications
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static NotificationsResult GetNotifications(String token) {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_USER_GET_NOTIFICATIONS ;
            NotificationsResult result = common.HttpGet<NotificationsResult>(url, token);
            return result;
        }

        /// <summary>
        /// Send friend request to an user
        /// </summary>
        /// <param name="token"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public static Relationship SendFriendRequest(String token, String username)
        {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_RELATIONSHIP_SEND_REQUEST;
            RelationshipCreateOrEditModel relCreate = new RelationshipCreateOrEditModel(username);
            return common.HttpPost<Relationship,RelationshipCreateOrEditModel>(url,relCreate, token);
        }

        /// <summary>
        /// Response to a friend request from an user
        /// </summary>
        /// <param name="token"></param>
        /// <param name="username">username of requester</param>
        /// <param name="accept">Accept or not</param>
        /// <returns></returns>
        public static Boolean ResponseFriendRequest(String token, String username, bool accept) {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_RELATIONSHIP_RESPONSE_REQUEST;
            ResponseRequestModel res = new ResponseRequestModel(username, accept);
            return common.HttpPost<Boolean, ResponseRequestModel>(url, res, token);
        }

        /// <summary>
        /// View friendship status between player and another user
        /// </summary>
        /// <param name="token"></param>
        /// <param name="username">username of whom player want to see the relationship</param>
        /// <returns></returns>
        public static List<Relationship> SeeFriendship(String token, String username) {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_RELATIONSHIP_GET_RELATIONSHIP + "?username=" + username;
            IEnumerable<Relationship> result = common.HttpGet<IEnumerable<Relationship>>(url, token);
            if (result == null)
            {
                return null;
            }
            else
            {
                List<Relationship> listRel = new List<Relationship>();
                listRel.AddRange(result);                
                return listRel;
            } 
        }

        /// <summary>
        /// Unfriend to an user
        /// </summary>
        /// <param name="token"></param>
        /// <param name="username">username of friend that player wants to unfriend</param>
        /// <returns></returns>
        public static Boolean Unfriend(String token, String username) {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_RELATIONSHIP_UNFRIEND + "?username=" + username;
            return common.HttpDelete(url, token);
        }

        /// <summary>
        /// Cancel a friend request that player sent to another user
        /// </summary>
        /// <param name="token"></param>
        /// <param name="username">username of user that player want to cancel request</param>
        /// <returns></returns>
        public static Boolean CancelRequest(String token, String username)
        {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_RELATIONSHIP_CANCEL_REQUEST + "?username=" + username;
            return common.HttpDelete(url, token);
        }

        /// <summary>
        /// Comment to a specific post
        /// </summary>
        /// <param name="token">Token get from login session</param>
        /// <param name="postID">ID of the post</param>
        /// <param name="comment">CommentCreateAndEditModel object which contains a comment property (String)</param>
        /// <returns></returns>
        public static Comment Comment(String token, String postID, CommentCreateAndEditModel comment) {
            string url = Constant.ApiURL + Constant.API_LINK_COMMENT_CREATE + "?postID="+postID ;
            HttpCommon common = new HttpCommon();
            string postData = "Content=" + comment.Content;
            Comment result = common.HttpPost<Comment, CommentCreateAndEditModel>(url, comment, token);
            return result;
        }

        /// <summary>
        /// Edit a comment that player commented before
        /// </summary>
        /// <param name="token"></param>
        /// <param name="commentID">ID of the comment</param>
        /// <param name="editInfo">New content of the comment</param>
        /// <returns></returns>
        public static Comment Edit(String token, String commentID, CommentCreateAndEditModel editInfo) {
            return null;
        }

        /// <summary>
        /// Delete a comment that player commented before
        /// </summary>
        /// <param name="token"></param>
        /// <param name="commentID">ID of the comment</param>
        /// <returns></returns>
        public static String DeleteComment(String token, String commentID) { return null; }

        /// <summary>
        /// View a specific post by its ID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="postID">ID of the post</param>
        /// <returns></returns>
        public static Post ViewPost(String token, String postID) { return null; }

        /// <summary>
        /// Post a new post
        /// </summary>
        /// <param name="token"></param>
        /// <param name="post">Post content</param>
        /// <returns></returns>
        public static Post Post(string token, PostCreateAndEditModel post)
        {
           
            string url = Constant.ApiURL + Constant.API_LINK_POST_CREATE;
            HttpCommon common = new HttpCommon();
            string postData = "Content=" + post.Content;
            Post result = common.HttpPost<Post, PostCreateAndEditModel>(url, post, token);
            return result;

        }

        /// <summary>
        /// Get Newsfeed. Newsfeed contains only posts of friends. 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static IEnumerable<TimelineAndNewsfeedModel> GetNewsfeed(string token)
        {
            string url = Constant.ApiURL + Constant.API_LINK_POST_GET_NEWSFEED;
            HttpCommon common = new HttpCommon();
            IEnumerable<TimelineAndNewsfeedModel> feeds = common.HttpGet<IEnumerable<TimelineAndNewsfeedModel>>(url, token);
            return feeds;
        }

        /// <summary>
        /// Get all comments of a specific post
        /// </summary>
        /// <param name="token"></param>
        /// <param name="postID">ID of the post</param>
        /// <returns></returns>
        public static List<CommentResultModel> GetComments(string token, string postID)
        {
            string url = Constant.ApiURL + Constant.API_LINK_POST_GET_COMMENTS + "?postID=" + postID ;
            HttpCommon common = new HttpCommon();
            List<CommentResultModel> comments = common.HttpGet<List<CommentResultModel>>(url, token);
            return comments;
        }
        
        /// <summary>
        /// Edit a post that player posted before
        /// </summary>
        /// <param name="token"></param>
        /// <param name="postID">ID of post</param>
        /// <param name="editInfo">New content of post</param>
        /// <returns></returns>
        public static Post EditPost(String token, String postID, PostCreateAndEditModel editInfo)
        {            
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_POST_EDIT+"?postID="+postID ;
            string putData = "Content=" + editInfo.Content;
            Post result = common.HttpPut<Post, PostCreateAndEditModel>(url, editInfo, token);
            return result;
        }

        /// <summary>
        /// Delete a post that player posted before
        /// </summary>
        /// <param name="token"></param>
        /// <param name="postID">ID of the post</param>
        /// <returns></returns>
        public static Boolean DeletePost(String token, String postID)
        {
            string url = Constant.ApiURL + Constant.API_LINK_POST_DELETE + "?postID=" + postID;
            HttpCommon common = new HttpCommon();
            return common.HttpDelete(url, token);
        }
        
        /// <summary>
        /// Like/Unlike a post 
        /// </summary>
        /// <param name="token">Token get from Login session</param>
        /// <param name="postID">ID of the post user want to like</param>
        /// <returns>result</returns>
        public static bool LikePost(String token, String postID)
        {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_POST_LIKE_UNLIKE + "?postID=" + postID;
            bool result = common.HttpPost<bool>(url,token);
            return result;
        }

        /// <summary>
        /// Like a comment that player likes
        /// </summary>
        /// <param name="token"></param>
        /// <param name="commentID">ID of the comment</param>
        /// <returns></returns>
        public static bool LikeComment(String token, String commentID)
        {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_COMMENT_LIKE_UNLIKE + "?commentID=" + commentID;
            bool result = common.HttpPost<bool>(url, token);
            return result;
        }             
    }
}
