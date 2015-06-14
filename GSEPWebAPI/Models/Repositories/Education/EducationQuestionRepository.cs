using GSEPWebAPI.App_Start;
using GSEPWebAPI.Helpers.Neo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSEPWebAPI.Models.Repositories.Education
{
    public class EducationQuestionRepository : IEducationQuestionRepository
    {
        Neo4jHelper _neo4jHelper;
        public EducationQuestionRepository()
        {
            _neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
        }
        public Question GetQuestion(string questionID)
        {
            return _neo4jHelper.GetNode<Question>(Constants.LABEL_QUESTION, new KeyString(Constants.KEY_QUESTION, questionID));
        }

        public IEnumerable<Question> GetQuestions(int difficulty, string gameName)
        {
            return _neo4jHelper.GetNodes<Question>(Constants.LABEL_QUESTION, new KeyString[] { new KeyString(Constants.ATTR_GAME_NAME, gameName) }, new KeyInt[] { new KeyInt(Constants.ATTR_DIFFICULTY, difficulty) });
        }

        public IEnumerable<Question> GetQuestions(string hashTag, string gameName)
        {
            return _neo4jHelper.GetNodes<Question>(Constants.LABEL_QUESTION, new KeyString[] { new KeyString(Constants.ATTR_GAME_NAME, gameName) }, null, Constants.LABEL_HASHTAG, new KeyString(Constants.ATTR_HASHTAG, hashTag), Constants.REL_QUESTION_HASHTAG);
        }

        public IEnumerable<Question> GetQuestions(int difficulty, string hashTag, string gameName)
        {
            return _neo4jHelper.GetNodes<Question>(Constants.LABEL_QUESTION, new KeyString[] { new KeyString(Constants.ATTR_GAME_NAME, gameName) }, new KeyInt[] { new KeyInt(Constants.ATTR_DIFFICULTY, difficulty) }, Constants.LABEL_HASHTAG, new KeyString(Constants.ATTR_HASHTAG, hashTag), Constants.REL_QUESTION_HASHTAG);
        }

        public IEnumerable<HashTag> GetHashTags(string questionID)
        {
            return _neo4jHelper.GetNodes<HashTag>(Constants.LABEL_HASHTAG, Constants.LABEL_QUESTION, new KeyString(Constants.KEY_QUESTION, questionID), Constants.REL_QUESTION_HASHTAG);
        }


        public IEnumerable<SharedQuestionModel> GetSharedQuestion(string username)
        {
            IEnumerable<SharedQuestionModel> sharedQuestions = _neo4jHelper.GetNodes<SharedQuestionModel>(
                Constants.LABEL_SHARED_QUESTION,
                Constants.LABEL_USER,
                new KeyString(Constants.KEY_USER, username),
                Constants.REL_USER_SHARED_QUESTION);
            return sharedQuestions;
        }


        public SupportQuestionRelationship SupportSharedQuestion(string username, string sharedID, string answerID)
        {
            SupportQuestionRelationship support = new SupportQuestionRelationship(answerID, false);
            _neo4jHelper.CreateRelationShip(Constants.LABEL_USER,
                                            new KeyString(Constants.KEY_USER, username),
                                            Constants.LABEL_SHARED_QUESTION,
                                            new KeyString(Constants.KEY_SHARED_QUESTION, sharedID),
                                            Constants.REL_USER_SHARED_QUESTION, support);
            return support;
        }

        public SupportQuestionRelationship UpdateSupportSharedQuesiton(string username, string sharedID, string answerID)
        {
            SupportQuestionRelationship support = new SupportQuestionRelationship(answerID, false);
            _neo4jHelper.UpdateRelationShip(Constants.LABEL_USER,
                                            new KeyString(Constants.KEY_USER, username),
                                            Constants.LABEL_SHARED_QUESTION,
                                            new KeyString(Constants.KEY_SHARED_QUESTION, sharedID),
                                            Constants.REL_USER_SHARED_QUESTION, support);            
            return support;
        }

        public SupportQuestionRelationship GetSupportSharedQuestionRel(string username, string sharedID)
        {
            return _neo4jHelper.GetRelationShip<SupportQuestionRelationship>(Constants.LABEL_USER,
                                            new KeyString(Constants.KEY_USER, username),
                                            Constants.LABEL_SHARED_QUESTION,
                                            new KeyString(Constants.KEY_SHARED_QUESTION, sharedID),
                                            Constants.REL_USER_SHARED_QUESTION);
        }

        public IEnumerable<SocialUser> GetUsersSupportSharedQuestion(string shareID)
        {
            return _neo4jHelper.GetNodes<SocialUser>(Constants.LABEL_USER,
                                                        Constants.LABEL_SHARED_QUESTION,
                                                        new KeyString(Constants.KEY_SHARED_QUESTION, shareID),
                                                        Constants.REL_USER_SUPPORT_QUESTION);
        }


        public SupportQuestionRelationship UpdateSupportSharedQuestion(string username, string sharedID, string answerID)
        {
            throw new NotImplementedException();
        }       
    }
}