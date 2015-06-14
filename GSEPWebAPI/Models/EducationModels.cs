using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GSEPWebAPI.Models
{
    public class Question
    {
        public string QuestionID { get; set; }
        public string Content { get; set; }
        public int Difficulty { get; set; }
        public string GameName { get; set; }
        public Question()
            : base()
        {

        }
        public Question(string id, string content, int difficulty, string gameName)
        {
            this.QuestionID = id;
            this.Content = content;
            this.Difficulty = difficulty;
            this.GameName = gameName;
        }
    }

    public class SharedQuestionModel
    {
        public String SharedID { get; set; }
        public Double Time { get; set; }
        public bool IsActive { get; set; }

        public SharedQuestionModel() : base()
        {

        }

        public SharedQuestionModel(String sharedID, double time, bool isActive)
        {
            this.SharedID = sharedID;
            this.Time = time;
            this.IsActive = isActive;
        }


    }

    public class ShareQuestionResultModel
    {
        public Question Question { get; set; }
        public String Username { get; set; }
        public String Fullname { get; set; }
        public String ShareID { get; set; }
        public Double Time { get; set; }
        public ShareQuestionResultModel()
            : base()
        {

        }

        public ShareQuestionResultModel(Question question, String username, String fullname, String shareID, Double time)
        {
            this.Question = question;
            this.Username = username;
            this.Fullname = fullname;
            this.ShareID = shareID;
            this.Time = time;
        }
    }

    public class Answer
    {
        public string AnswerID { get; set; }
        public string Content { get; set; }
        public bool IsTrue { get; set; }
        public Answer()
            : base()
        {

        }
        public Answer(string answerID, string content, bool isTrue)
        {
            this.AnswerID = answerID;
            this.Content = content;
            this.IsTrue = isTrue;
        }
    }

    public class HashTag
    {
        public string GameName { get; set; }
        public string Tag { get; set; }
        public string HashTagID { get; set; }
        public HashTag() : base() { }
        public HashTag(string gameName, string tag)
        {
            this.GameName = gameName;
            this.Tag = tag;
            this.HashTagID = GameName + Tag;
        }
    }

    public class AnswerResultModel
    {
        public string AnswerID { get; set; }
        public string Content { get; set; }
        public AnswerResultModel() { }
        public AnswerResultModel(string answerID, string content)
        {
            this.AnswerID = answerID;
            this.Content = content;
        }
    }

    public class QuestionResultModel
    {
        public string QuestionID { get; set; }
        public string Content { get; set; }
        public int Difficulty { get; set; }
        public List<String> HashTags { get; set; }
        public List<AnswerResultModel> Answers { get; set; }
        public QuestionResultModel() { }
        public QuestionResultModel(string questionID, string content, int difficulty, List<String> hashTag, List<AnswerResultModel> answers)
        {
            this.QuestionID = questionID;
            this.Content = content;
            this.Difficulty = difficulty;
            this.HashTags = hashTag;
            this.Answers = answers;
        }
    }

    public class SupportQuestionRelationship
    {
        public string AnswerID { get; set; }
        public bool IsAwarded { get; set; }
        public SupportQuestionRelationship() { }
        public SupportQuestionRelationship(string answerID, bool isAwarded) {
            this.AnswerID = answerID;
            this.IsAwarded = isAwarded;
        }
    }

    public class SupportQuestionModel
    {
        public string ShareID { get; set; }
        public string AnswerID { get; set; }
        public SupportQuestionModel() { }
        public SupportQuestionModel(string sharedID, string answerID)
        {
            this.ShareID = sharedID;
            this.AnswerID = answerID;
        }
    }

    #region Achievement
    public class Achievement
    {
        public String AchievementID { get; set; }
        public String AchievementName { get; set; }
        public int Difficulty { get; set; }
        public String GameName { get; set; }
        public String IconURL { get; set; }
        public String Description { get; set; }

        public Achievement() : base()
        {

        }

        public Achievement(String achievementID, String achievementName, int difficulty, String gameName, String iconURL, String description)
        {
            this.AchievementID = achievementID;
            this.AchievementName = achievementName;
            this.Difficulty = difficulty;
            this.GameName = gameName;
            this.IconURL = iconURL;
            this.Description = description;
        }
    }

    public class ShareAchievement
    {        
        public ShareAchievement(double time)
        {            
            this.Time = time;
        }
        public double Time { get; set; }
    }

    public class ShareAchievementResultModel
    {
        public Achievement Achievement { get; set; }
        public Double Time { get; set; }
        public ShareAchievementResultModel() : base()
        {

        }

        public ShareAchievementResultModel(Achievement achievement, Double time)
        {
            this.Achievement = achievement;
            this.Time = time;
        }
    }
    #endregion
}