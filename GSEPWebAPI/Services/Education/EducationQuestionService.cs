using GSEPWebAPI.App_Start;
using GSEPWebAPI.Models;
using GSEPWebAPI.Models.Repositories.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSEPWebAPI.Services.Education
{
    public class EducationQuestionService : IEducationQuestionService
    {
        private IEducationQuestionRepository _questionRepository;
        private IEducationAnswerRepository _answerRepository;

        public EducationQuestionService()
        {
            _questionRepository = new EducationQuestionRepository();
            _answerRepository = new EducationAnswerRepository();
        }

        public QuestionResultModel GetQuestion(string questionID)
        {
            Question question = _questionRepository.GetQuestion(questionID);
            IEnumerable<Answer> answers = _answerRepository.GetAnswers(questionID);
            IEnumerable<HashTag> hashTags = _questionRepository.GetHashTags(questionID);
            List<AnswerResultModel> answersResult = null;
            List<String> hashTagsResult = null;
            QuestionResultModel result = null;
            if (answers != null)
            {
                answersResult = new List<AnswerResultModel>();
                foreach (var answer in answers)
                {
                    answersResult.Add(new AnswerResultModel(answer.AnswerID, answer.Content));
                }
            }
            if (hashTags != null)
            {
                hashTagsResult = new List<String>();
                foreach (var hashTag in hashTags)
                {
                    hashTagsResult.Add(hashTag.Tag);
                }
            }
            if (question != null)
            {
                result = new QuestionResultModel(questionID, question.Content, question.Difficulty, hashTagsResult, answersResult);
            }
            return result;
        }

        public IEnumerable<QuestionResultModel> GetQuestions(int difficulty, string gameName)
        {
            IEnumerable<Question> questions = _questionRepository.GetQuestions(difficulty, gameName);
            List<QuestionResultModel> result = null;
            if (questions != null)
            {
                result = new List<QuestionResultModel>();
                foreach (var question in questions)
                {
                    string questionID = question.QuestionID;
                    QuestionResultModel questionResult = this.GetQuestion(questionID);
                    if (questionResult != null)
                    {
                        result.Add(questionResult);
                    }
                }
            }
            return result;
        }

        public IEnumerable<QuestionResultModel> GetQuestions(string hashTag, string gameName)
        {
            IEnumerable<Question> questions = _questionRepository.GetQuestions(hashTag, gameName);
            List<QuestionResultModel> result = null;
            if (questions != null)
            {
                result = new List<QuestionResultModel>();
                foreach (var question in questions)
                {
                    string questionID = question.QuestionID;
                    QuestionResultModel questionResult = this.GetQuestion(questionID);
                    if (questionResult != null)
                    {
                        result.Add(questionResult);
                    }
                }
            }
            return result;
        }

        public IEnumerable<QuestionResultModel> GetQuestions(int difficulty, string hashTag, string gameName)
        {
            IEnumerable<Question> questions = _questionRepository.GetQuestions(difficulty, hashTag, gameName);
            List<QuestionResultModel> result = null;
            if (questions != null)
            {
                result = new List<QuestionResultModel>();
                foreach (var question in questions)
                {
                    string questionID = question.QuestionID;
                    QuestionResultModel questionResult = this.GetQuestion(questionID);
                    if (questionResult != null)
                    {
                        result.Add(questionResult);
                    }
                }
            }
            return result;
        }

        public bool CheckAnswer(string questionID, string answerID)
        {
            Answer answer = _answerRepository.GetAnswer(questionID, answerID);
            if (answer == null)
            {
                return false;
            }
            return answer.IsTrue;
        }

        public IEnumerable<SharedQuestionModel> GetSharedQuestion(string username)
        {
            return _questionRepository.GetSharedQuestion(username);
        }


        public SupportQuestionRelationship SupportSharedQuestion(string username, string sharedID, string answerID)
        {
            return _questionRepository.SupportSharedQuestion(username, sharedID, answerID);
        }

        public SupportQuestionRelationship UpdateSupportSharedQuestion(string username, string sharedID, string answerID)
        {
            return _questionRepository.UpdateSupportSharedQuestion(username, sharedID, answerID);
        }

        public SupportQuestionRelationship GetSupportSharedQuestionRel(string username, string sharedID)
        {
            return _questionRepository.GetSupportSharedQuestionRel(username, sharedID);
        }

        public IEnumerable<SocialUser> GetUserSupportSharedQuestion(string shareID)
        {
            return _questionRepository.GetUsersSupportSharedQuestion(shareID);
        }
    }
}