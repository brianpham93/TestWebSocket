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
    public static class EducationConnector
    {

        /// <summary>
        /// Get question by ID
        /// </summary>
        /// <param name="token">token of login session</param>
        /// <param name="questionID">ID of question</param>
        /// <returns></returns>
        public static QuestionResultModel GetQuestion(string token, string questionID)
        {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_QUESTION_GET_QUESTION_BY_ID + "?" + Constant.PARAM_QUESTIONID + questionID;
            QuestionResultModel result = common.HttpGet<QuestionResultModel>(url, token);
            return result;
        }

        /// <summary>
        /// Get questions of a game by difficulty
        /// </summary>
        /// <param name="token">token of login session</param>
        /// <param name="difficulty">desired difficulty for the questions</param>
        /// <param name="gameName">name of the game</param>
        /// <returns></returns>
        public static List<QuestionResultModel> GetQuestionByDifficulty(String token, int difficulty, string gameName)
        {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_QUESTION_GET_QUESTIONS_BY_DIFFICULTY + "?"
                + Constant.PARAM_DIFFICULTY + difficulty + "&"
                + Constant.PARAM_GAMENAME + gameName;

            List<QuestionResultModel> result = common.HttpGet<List<QuestionResultModel>>(url, token);
            return result;
        }


        public static List<QuestionResultModel> GetQuestionByHashtag(String token, string hashtag, string gameName)
        {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_QUESTION_GET_QUESTIONS_BY_DIFFICULTY + "?"
                + Constant.PARAM_HASHTAG + hashtag + "&"
                + Constant.PARAM_GAMENAME + gameName;

            List<QuestionResultModel> result = common.HttpGet<List<QuestionResultModel>>(url, token);
            return result;
        }

        /// <summary>
        /// Get questions by desired difficulty and hashtag
        /// </summary>
        /// <param name="token">token of login session</param>
        /// <param name="difficulty">desired difficulty for questions</param>
        /// <param name="hashtag">desired hashtag for questions</param>
        /// <param name="gameName">name of the game</param>
        /// <returns></returns>
        public static List<QuestionResultModel> GetQuestionByDificultyAndHashtag(String token, int difficulty, string hashtag, string gameName)
        {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_QUESTION_GET_QUESTIONS_BY_DIFFICULTY + "?"
                + Constant.PARAM_DIFFICULTY + difficulty + "&"
                + Constant.PARAM_HASHTAG + hashtag + "&"
                + Constant.PARAM_GAMENAME + gameName;

            List<QuestionResultModel> result = common.HttpGet<List<QuestionResultModel>>(url, token);
            return result;
        }

        /// <summary>
        /// Check if user's answer of a question is correct or not
        /// </summary>
        /// <param name="token">token of login session</param>
        /// <param name="questionID">ID of question</param>
        /// <param name="answerID">ID of answer that user chose</param>
        /// <returns></returns>
        public static bool CheckAnswer(string token, string questionID, string answerID)
        {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_ANSWER_CHECK_ANSWER + "?"
                + Constant.PARAM_QUESTIONID + questionID + "&"
                + Constant.PARAM_ANSWERID + answerID;                

            bool result = common.HttpGet<bool>(url, token);
            return result;
        }        

        //GET NUMBER OF RIGHT ANSWER
        public static int GetNoOfRightAnswer(String token, String sessionID)
        {
            return 0;
        }

        //GET NUMBER OF WRONG ANSWER
        public static int GetNoOfWrongAnswer(String token, String sessionID)
        {
            return 0;
        }

        public static bool ShareQuestion(string token, string questionID)
        {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_USER_SHARE_QUESTION +"?"+ Constant.PARAM_QUESTIONID + questionID;
            //CreateShareQuestionModel shareQuestion = new CreateShareQuestionModel(questionID);
            return common.HttpPost<bool>(url, token);
            //return common.HttpPost<Relationship, RelationshipCreateOrEditModel>(url, relCreate, token);
        }

        public static List<Achievement> GetAchievements(string token, string gameName)
        {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_ACHIEVEMENT_GET_ACHIEVEMENTS + "?" + Constant.PARAM_GAMENAME + gameName;
            return common.HttpGet<List<Achievement>>(url, token);
        }

        public static List<Achievement> GetAchievementsByDifficulty(string token, string gameName, int difficulty)
        {
            HttpCommon common = new HttpCommon();
            string url = Constant.ApiURL + Constant.API_LINK_ACHIEVEMENT_GET_ACHIEVEMENTS_BY_DIFFICULTY + "?"
                + Constant.PARAM_GAMENAME + gameName
                + "&" + Constant.PARAM_DIFFICULTY + difficulty;
;
            return common.HttpGet<List<Achievement>>(url, token);
        }

        public static bool ShareAchievement(string token, string achievementID)
        {
            HttpCommon common = new HttpCommon();
            String url = Constant.ApiURL + Constant.API_LINK_ACHIEVEMENT_SHARE_ACHIEVEMENT + "?"
                + Constant.PARAM_ACHIEVEMENTID + achievementID;

            return common.HttpPost<bool>(url, token);            
        }

        public static Achievement GetAchievement(string token, string achievementID)
        {
            HttpCommon common = new HttpCommon();
            String url = Constant.ApiURL + Constant.API_LINK_ACHIEVEMENT_GET_ACHIEVEMENT + "?"
                + Constant.PARAM_ACHIEVEMENTID + achievementID;

            return common.HttpGet<Achievement>(url, token);
        }


             

    }
}
