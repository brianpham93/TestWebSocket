using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSEPWebAPI.Models.Repositories.Education
{
    public interface IEducationQuestionRepository
    {
         Question GetQuestion(string questionID);
         IEnumerable<Question> GetQuestions(int difficulty, string gameName);
         IEnumerable<Question> GetQuestions(string hashTag, string gameName);
         IEnumerable<Question> GetQuestions(int difficulty, string hashTag, string gameName);        
         IEnumerable<HashTag> GetHashTags(string questionID);
         IEnumerable<SharedQuestionModel> GetSharedQuestion(string username);
         SupportQuestionRelationship SupportSharedQuestion(string username, string sharedID, string answerID);
         SupportQuestionRelationship UpdateSupportSharedQuestion(string username, string sharedID, string answerID);
         SupportQuestionRelationship GetSupportSharedQuestionRel(string username, string sharedID);
         IEnumerable<SocialUser> GetUsersSupportSharedQuestion(string shareID);
    }
}
