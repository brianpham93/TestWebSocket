using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSEPWebAPI.Models.Repositories.Education
{
    public interface IEducationAnswerRepository
    {
         Answer GetAnswer(string answerID);
         IEnumerable<Answer> GetAnswers(string questionID);
         Answer GetAnswer(string questionID, string answerID);
    }
}
