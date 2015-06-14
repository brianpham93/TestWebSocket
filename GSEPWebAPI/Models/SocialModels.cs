using GSEPWebAPI.App_Start;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GSEPWebAPI.Models
{
    #region SocialUser
    public class SocialUser
    {
        [Key]
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Range(1900, 2015,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Year { get; set; }
        [Range(1, 12,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Month { get; set; }
        [Range(1, 31,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Day { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string AvatarURL { get; set; }

        public SocialUser()
            : base()
        {

        }

        public SocialUser(string username, string firstName, string lastName,
            int year, int month, int day, string gender, string country, string avatarURL)
        {
            this.Username = username;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Year = year;
            this.Month = month;
            this.Day = day;
            this.Gender = gender;
            this.Country = country;
            this.AvatarURL = avatarURL;
        }
    }

    public class EditProfileModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string AvatarURL { get; set; }

        public EditProfileModel()
        {

        }

        public EditProfileModel(string firstName, string lastName,
            int year, int month, int day, string gender, string country, string avatarURL)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Year = year;
            this.Month = month;
            this.Day = day;
            this.Gender = gender;
            this.Country = country;
            this.AvatarURL = avatarURL;
        }
    }

    public class UserSearchResult
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Country { get; set; }
        public string AvatarURL { get; set; }
        public UserSearchResult()
            : base()
        {

        }

        public UserSearchResult(string firstName, string lastName, string country, string avatarURL, string username)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Username = username;
            this.Country = country;
            this.AvatarURL = avatarURL;
        }
    }
    #endregion

    #region Comment
    public class Comment
    {
        [Key]
        public string CommentID { get; set; }
        public string Content { get; set; }
        public Double Time { get; set; }
        public Comment()
        {

        }
        public Comment(string commentID, string content, Double time)
        {
            this.CommentID = commentID;
            this.Content = content;
            this.Time = time;
        }
    }

    public class CommentCreateAndEditModel
    {
        public string Content { get; set; }

        public CommentCreateAndEditModel()
        {

        }
        public CommentCreateAndEditModel(string content)
        {
            this.Content = content;
        }
    }

    public class CommentResultModel
    {
        public Comment Comment { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public CommentResultModel() : base() { }
        public CommentResultModel(Comment comment, string username, string fullname)
        {
            this.Comment = comment;
            this.Username = username;
            this.Fullname = fullname;
        }
    }

    #endregion

    #region NewPost
    public class Post
    {
        [Key]
        public string PostID { get; set; }
        public string Content { get; set; }
        public double Time { get; set; }
        public Post()
        {

        }

        public Post(string postID, string content, double time)
        {
            this.PostID = postID;
            this.Content = content;
            this.Time = time;
        }
    }

    public class PostCreateAndEditModel
    {
        public string Content { get; set; }
        public PostCreateAndEditModel() : base() { }
        public PostCreateAndEditModel(string content)
        {
            this.Content = content;
        }
    }

    public class PostResultModel
    {
        public Post Post { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public PostResultModel() : base() { }
        public PostResultModel(Post post, string username, string fullname)
        {
            this.Post = post;
            this.Username = username;
            this.Fullname = fullname;
        }
    }

    public class TimelineAndNewsfeedModel
    {
        public PostResultModel Post { get; set; }
        public ShareQuestionResultModel SharedQuestion { get; set; }
        public ShareAchievementResultModel Achievement { get; set; }
        public double Time { get; set; }

        public TimelineAndNewsfeedModel()
            : base()
        {

        }

        public TimelineAndNewsfeedModel(PostResultModel post, ShareQuestionResultModel sharedQuestion, ShareAchievementResultModel achievement, Double time)
        {
            this.Post = post;
            this.SharedQuestion = sharedQuestion;
            this.Achievement = achievement;
            this.Time = time;
        }

    }
    #endregion

    #region Message
    public class Message
    {
        [Key]
        public string MessageID { get; set; }
        public string Content { get; set; }
        public double Time { get; set; }

        public Message()
        {

        }
        public Message(string messageID, string content, double time)
        {
            this.MessageID = messageID;
            this.Content = content;
            this.Time = time;
        }
    }

    public class SendMessageModel
    {
        public string Content { get; set; }
        public string TargetUsername { get; set; }

        public SendMessageModel()
        {

        }
        public SendMessageModel(string content, string targetUsername)
        {
            this.TargetUsername = targetUsername;
            this.Content = content;
        }
    }

    public class MessageConversation
    {
        public string SendUsername { get; set; }
        public Message Message { get; set; }
        public MessageConversation() : base() { }
        public MessageConversation(string sendUsername, Message message)
        {
            this.SendUsername = sendUsername;
            this.Message = message;
        }
    }
    #endregion

    #region Relationship
    public class Relationship
    {
        public string Status { get; set; }
        public Relationship()
        {

        }

        public Relationship(string status)
        {
            this.Status = status;
        }
    }

    public class RelationshipCreateOrEditModel
    {
        public string TargetUsername { get; set; }
        public RelationshipCreateOrEditModel() : base() { }
        public RelationshipCreateOrEditModel(string targetUsername)
        {
            this.TargetUsername = targetUsername;
        }
    }

    public class ResponseRequestModel
    {
        public string TargetUsername { get; set; }
        public bool Accept { get; set; }
        public ResponseRequestModel() : base() { }
        public ResponseRequestModel(string targetUsername, bool accept)
        {
            this.TargetUsername = targetUsername;
            this.Accept = accept;
        }
    }
    #endregion

    #region Notification
    public class Notification
    {
        public string NotificationID { get; set; }
        public string PostID { get; set; }
        public string CommentID { get; set; }
        public string Title { get; set; }
        public Notification() : base() { }
        public Notification(string notiID, string postID, string commentID, string title)
        {
            this.NotificationID = notiID;
            this.PostID = postID;
            this.CommentID = commentID;
            this.Title = title;
        }
    }

    public class NotificationsResult
    {
        public string NotificationsID { get; set; }
        public IEnumerable<Notification> ListNotification { get; set; }
        public NotificationsResult() : base() { }
        public NotificationsResult(string notificationID, IEnumerable<Notification> listNotifications)
        {
            this.NotificationsID = notificationID;
            this.ListNotification = listNotifications;
        }
    }

    public class NotificationsNeo
    {
        public string NotificationsID { get; set; }
        public string Content { get; set; }
        public NotificationsNeo() : base() { }
        public NotificationsNeo(string notificationsID, string content)
        {
            this.NotificationsID = notificationsID;
            this.Content = content;
        }
    }
    #endregion
}