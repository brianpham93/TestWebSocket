using GSEPWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSEPWebAPI.Services.Education
{
    public interface IEducationQuestionService
    {
         QuestionResultModel GetQuestion(string questionID);
         IEnumerable<QuestionResultModel> GetQuestions(int difficulty, string gameName);
         IEnumerable<QuestionResultModel> GetQuestions(string hashTag, string gameName);
         IEnumerable<QuestionResultModel> GetQuestions(int difficulty, string hashTag, string gameName);
         bool CheckAnswer(string answerID, string questionID);
         IEnumerable<SharedQuestionModel> GetSharedQuestion(string username);
         SupportQuestionRelationship SupportSharedQuestion(string username,string sharedID, string answerID);
         SupportQuestionRelationship UpdateSupportSharedQuestion(string username, string sharedID, string answerID);
         SupportQuestionRelationship GetSupportSharedQuestionRel(string username, string sharedID);
         IEnumerable<SocialUser> GetUserSupportSharedQuestion(string shareID);
    }
}
