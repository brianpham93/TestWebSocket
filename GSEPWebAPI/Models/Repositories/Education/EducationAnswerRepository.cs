using GSEPWebAPI.App_Start;
using GSEPWebAPI.Helpers.Neo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSEPWebAPI.Models.Repositories.Education
{
    public class EducationAnswerRepository : IEducationAnswerRepository
    {
        Neo4jHelper _neo4jHelper;
        public EducationAnswerRepository()
        {
            _neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
        }
        public Answer GetAnswer(string answerID)
        {
            return _neo4jHelper.GetNode<Answer>(Constants.LABEL_ANSWER, new KeyString(Constants.KEY_ANSWER, answerID));
        }

        public IEnumerable<Answer> GetAnswers(string questionID)
        {
            return _neo4jHelper.GetNodes<Answer>(Constants.LABEL_ANSWER, Constants.LABEL_QUESTION, new KeyString(Constants.KEY_QUESTION, questionID), Constants.REL_QUESTION_ANSWER);
        }


        public Answer GetAnswer(string questionID, string answerID)
        {
            if (!_neo4jHelper.IsRelate(Constants.REL_QUESTION_ANSWER, Constants.LABEL_QUESTION, new KeyString(Constants.KEY_QUESTION, questionID),
                Constants.LABEL_ANSWER, new KeyString(Constants.KEY_ANSWER, answerID)))
            {
                return null;
            }
            return _neo4jHelper.GetNode<Answer>(Constants.LABEL_ANSWER, new KeyString(Constants.KEY_ANSWER, answerID));
        }
    }
}