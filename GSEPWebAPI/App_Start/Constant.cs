using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSEPWebAPI.App_Start
{
    public class Constants
    {
        public const String API_LINK_USER_GET_PROFILE = "Api/User/GetProfile";
        public const String API_LINK_USER_UPDATE_PROFILE = "Api/User/UpdateProfile";
        public const String API_LINK_USER_SEARCH = "Api/User/Search";
        public const String API_LINK_USER_GET_NOTIFICATIONS = "Api/User/GetNotifications";
        public const String API_LINK_USER_SHARE_QUESTION = "Api/User/ShareQuestion";

        public const String API_LINK_POST_CREATE = "Api/Post/NewPost";
        public const String API_LINK_POST_EDIT = "Api/Post/EditPost";
        public const String API_LINK_POST_VIEW = "Api/Post/ViewPost";
        public const String API_LINK_POST_DELETE = "Api/Post/DeletePost";
        public const String API_LINK_POST_LIKE_UNLIKE = "Api/Post/LikeUnlikePost";
        public const String API_LINK_POST_GET_COMMENTS = "Api/Post/GetComments";
        public const String API_LINK_POST_GET_NEWSFEED = "Api/Post/GetNewsfeed";
        public const String API_LINK_POST_UNFOLLOW = "Api/Post/UnfollowPost";

        public const String API_LINK_COMMENT_CREATE = "Api/Comment/NewComment";
        public const String API_LINK_COMMENT_EDIT = "Api/Comment/EditComment";
        public const String API_LINK_COMMENT_VIEW = "Api/Comment/ViewComment";
        public const String API_LINK_COMMENT_DELETE = "Api/Comment/DeleteComment";
        public const String API_LINK_COMMENT_LIKE_UNLIKE = "Api/Comment/LikeUnlikeComment";

        public const String API_LINK_RELATIONSHIP_SEND_REQUEST = "Api/Relationship/SendRequest";
        public const String API_LINK_RELATIONSHIP_RESPONSE_REQUEST = "Api/Relationship/ResponseRequest";
        public const String API_LINK_RELATIONSHIP_GET_RELATIONSHIP = "Api/Relationship/GetRelationship";
        public const String API_LINK_RELATIONSHIP_UNFRIEND = "Api/Relationship/Unfriend";
        public const String API_LINK_RELATIONSHIP_CANCEL_REQUEST = "Api/Relationship/CancelRequest";

        public const String API_LINK_MESSAGE_SEND = "Api/Message/SendMessage";
        public const String API_LINK_MESSAGE_GET_MESSAGE = "Api/Message/GetMessage";
        public const String API_LINK_MESSAGE_GET_CONVERSATION = "Api/Message/GetConversation";

        public const String API_LINK_GET_QUESTION_BY_ID = "Api/Question/GetQuestion";
        public const String API_LINK_GET_QUESTIONS = "Api/Question/GetQuestions";
        public const String API_LINK_CHECK_ANSWER = "Api/Question/CheckAnswer";
        public const String API_LINK_GET_SHARED_QUESTION = "Api/Question/GetSharedQuestion";
        public const String API_LINK_SUPPORT_SHARED_QUESTION = "Api/Question/SupportSharedQuestion";
        public const String API_LINK_UPDATE_SUPPORT_SHARED_QUESTION = "Api/Question/UpdateSupportSharedQuestion";
        public const String API_LINK_GET_USER_SUPPORT_SHARED_QUESTION = "Api/Question/GetUsersSupport";
        public const String API_LINK_GET_SUPPORT_SHARED_QUESTION_REL = "Api/Question/GetSupportSharedQuestionRelationship";

        public const String API_LINK_ACHIEVEMENT_GET_ACHIEVEMENTS = "Api/Achievement/GetAchievements";
        public const String API_LINK_ACHIEVEMENT_GET_ACHIEVEMENTS_BY_DIFFICULTY = "Api/Achievement/GetAchievementsByDifficulty";
        public const String API_LINK_ACHIEVEMENT_SHARE_ACHIEVEMENT = "Api/Achievement/ShareAchievement";
        public const String API_LINK_ACHIEVEMENT_GET_ACHIEVEMENT = "Api/Achievement/GetAchievement";
             

        public static String GRAPH_URL = "http://neo4j:gsep@52.74.208.191:7474/db/data";                                                      
        public static String LABEL_USER = "User";
        public static String LABEL_POST = "Post";
        public static String LABEL_COMMENT = "Commnent";
        public static String LABEL_LIKE = "Like";
        public static String LABEL_MESSAGE = "Message";
        public static String LABEL_NOTIFICATIONS = "Notifications";
        public static String LABEL_QUESTION = "Question";
        public static String LABEL_ANSWER = "ANSWER";
        public static String LABEL_HASHTAG = "HashTag";
        public static String LABEL_SHARED_QUESTION = "SharedQuestion";
        public static String LABEL_ACHIEVEMENT = "Achievement";

        public static String REL_USER_POST = "POST";
        public static String REL_USER_FOLLOW_POST = "FOLLOWING";
        public static String REL_USER_LIKE_POST = "LIKE_POST";
        public static String REL_USER_COMMENT = "COMMENT";
        public static String REL_USER_LIKE_COMMENT = "LIKE_COMMENT";
        public static String REL_USER_NOTIFICATIONS = "HAVE_NOTIFICATIONS";
        public static String REL_POST_COMMENT = "COMMENTED";
        public static String REL_USER_USER = "IN_A_RELATIONSHIP";
        public static String REL_USER_SEND_MESSAGE = "SEND_MESSAGE";
        public static String REL_USER_RECEIVE_MESSAGE = "RECEIVE_MESSAGE";
        public static String REL_QUESTION_ANSWER = "HAS_ANSWER";
        public static String REL_QUESTION_HASHTAG = "HAS_HASHTAG";
        public static String REL_USER_SHARED_QUESTION = "SHARE_QUESTION";
        public static String REL_QUESTION_SHARED_QUESTION = "IS_SHARED";
        public static String REL_USER_SHARE_ACHIEVEMENT = "SHARE_ACHIEVEMENT";
        public static String REL_USER_SUPPORT_QUESTION = "SUPPORT_QUESTION";
        
        
        public static String KEY_USER = "Username";
        public static String KEY_POST = "PostID";
        public static String KEY_COMMENT = "CommentID";
        public static String KEY_MESSAGE = "MessageID";
        public static String KEY_NOTIFICATIONS = "NotificationsID";
        public static String KEY_QUESTION = "QuestionID";
        public static String KEY_ANSWER = "AnswerID";
        public static String KEY_HASHTAG = "HashTagID";
        public static String KEY_SHARED_QUESTION = "SharedID";
        public static String KEY_ACHIEVEMENT = "AchievementID";
             

        public static String ATTR_GAME_NAME = "GameName";
        public static String ATTR_HASHTAG = "Tag";
        public static String ATTR_DIFFICULTY = "Difficulty";

        public static String REL_STATUS_PENDING = "PENDING";
        public static String REL_STATUS_FRIEND = "FRIEND";
        public static String REL_STATUS_KEY = "Status";
        
             
        public static String ERROR_AUTHENTICATE = "Error: User not login";
        public static String ERROR_POST_NOT_EXIST = "Error: Post is not exist";
        public static String ERROR_POST_NOT_RELATE_USER = "Error: Post is not relate to user";
        public static String ERROR_POST_NOT_RELATE_ANY = "Error: Post is not relate to any user";
        public static String ERROR_COMMENT_NOT_EXIST = "Error: Comment is not exist";
        public static String ERROR_COMMENT_NOT_RELATE_USER = "Error: Comment is not relate to user";
        public static String ERROR_TWO_USER_ALREADY_RELATE = "Error: Two user already have relationship";
        public static String ERROR_NOT_HAVE_REQUEST = "Error: Not have request to response";
        public static String ERROR_TWO_USER_ALREADY_FRIEND = "Error: Two user already friend";
        public static String ERROR_TWO_USER_NOT_FRIEND = "Error: Two user not friend";
        public static String ERROR_USER_NOT_FOLLOW_POST = "Error: User is not follow this post";
        public static String ERROR_USER_CAN_NOT_UPDATE_SUPPORT = "Error: User is not allow to update support";

        public static String MESSAGE_DELETE_COMMENT_FAIL = "Delete comment fail";
        public static String MESSAGE_DELETE_COMMENT_SUCCESS = "Delete comment successfully";
        public static String MESSAGE_DELETE_POST_FAIL = "Delete post fail";
        public static String MESSAGE_DELETE_POST_SUCCESS = "Delete post successfully";
        public static String MESSAGE_LIKE_COMMENT_SUCCESS = "Like comment successfully";
        public static String MESSAGE_UNLIKE_COMMENT_SUCCESS = "UnLike comment successfully";
        public static String MESSAGE_LIKE_POST_SUCCESS = "Like post successfully";
        public static String MESSAGE_UNLIKE_POST_SUCCESS = "UnLike post successfully";
        public static String MESSAGE_RESPONSE_REQUEST_SUCCESS = "Response request successfully";
        public static String MESSAGE_UNFRIEND_SUCCESS = "Unfriend successfully";
        public static String MESSAGE_UNFOLLOW_SUCCESS = "Unfollow successfully";

        public static String MESSAGE_NOTIFICATION_LIKE_POST = " is liked your post";
        public static String MESSAGE_NOTIFICATION_LIKE_COMMENT = " is liked your comment";
        public static String MESSAGE_NOTIFICATION_COMMENT_TO_POST = " is commented to your post";
        public static String MESSAGE_NOTIFICATION_COMMENT_TO_POST_FOLLOWING = " is commented to post you are following";

        public static String ErrorException(Exception e)
        {
            return "Error: " + e.Message;
        }

        public static String ErrorUserNotExist(string username)
        {
            return "Error: User " + username + " is not exist";
        }

        public static String TimeStamp()
        {
            return (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds).ToString(); 
        }

        public static String NotificationsIDValue(string username)
        {
            string id = username + TimeStamp();
            return id;
        }
    }
}
