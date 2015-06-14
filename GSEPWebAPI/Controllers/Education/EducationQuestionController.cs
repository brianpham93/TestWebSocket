using GSEPWebAPI.App_Start;
using GSEPWebAPI.Models;
using GSEPWebAPI.Services.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace GSEPWebAPI.Controllers.Education
{
    public class EducationQuestionController : ApiController
    {
        private IEducationQuestionService _questionService;
        public EducationQuestionController() : this(new EducationQuestionService()) { }
        public EducationQuestionController(IEducationQuestionService questionService)
        {
            this._questionService = questionService;
        }

        #region Get Question by ID
        [ResponseType(typeof(QuestionResultModel))]
        [HttpGet]
        [Route(Constants.API_LINK_GET_QUESTION_BY_ID)]
        public IHttpActionResult GetQuestion(string questionID)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_questionService.GetQuestion(questionID));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion


        #region Get Question by difficulty and game name
        [ResponseType(typeof(IEnumerable<QuestionResultModel>))]
        [HttpGet]
        [Route(Constants.API_LINK_GET_QUESTIONS)]
        public IHttpActionResult GetQuestion(int difficulty, string gameName)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_questionService.GetQuestions(difficulty, gameName));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        #region Get Question by hashtag and game name
        [ResponseType(typeof(IEnumerable<QuestionResultModel>))]
        [HttpGet]
        [Route(Constants.API_LINK_GET_QUESTIONS)]
        public IHttpActionResult GetQuestion(string hashTag, string gameName)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_questionService.GetQuestions(hashTag, gameName));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        #region Get Question by difficulty, hastag and game name
        /// <summary>
        /// Get questions by desired difficulty and hashtag
        /// </summary>        
        /// <param name="difficulty">desired difficulty for questions</param>
        /// <param name="hashtag">desired hashtag for questions</param>
        /// <param name="gameName">name of the game</param>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable<QuestionResultModel>))]
        [HttpGet]
        [Route(Constants.API_LINK_GET_QUESTIONS)]
        public IHttpActionResult GetQuestion(int difficulty, string hashTag, string gameName)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_questionService.GetQuestions(difficulty, hashTag, gameName));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        #region Check answer
        [ResponseType(typeof(Boolean))]
        [HttpGet]
        [Route(Constants.API_LINK_CHECK_ANSWER)]
        public IHttpActionResult CheckAnswer(string questionID, string answerID)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_questionService.CheckAnswer(questionID, answerID));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        [ResponseType(typeof(List<SharedQuestionModel>))]
        [HttpGet]
        [Route(Constants.API_LINK_GET_SHARED_QUESTION)]
        public IHttpActionResult GetSharedQuestions()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_questionService.GetSharedQuestion(User.Identity.Name));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }



        [ResponseType(typeof(SupportQuestionRelationship))]
        [HttpPost]
        [Route(Constants.API_LINK_SUPPORT_SHARED_QUESTION)]
        public IHttpActionResult SupportSharedQuestion(SupportQuestionModel support)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_questionService.SupportSharedQuestion(User.Identity.Name,support.ShareID,support.AnswerID));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }

        [ResponseType(typeof(SupportQuestionRelationship))]
        [HttpPut]
        [Route(Constants.API_LINK_UPDATE_SUPPORT_SHARED_QUESTION)]
        public IHttpActionResult UpdateSupportSharedQuestion(SupportQuestionModel support)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_questionService.UpdateSupportSharedQuestion(User.Identity.Name, support.ShareID, support.AnswerID));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }

        [ResponseType(typeof(SupportQuestionRelationship))]
        [HttpGet]
        [Route(Constants.API_LINK_GET_SUPPORT_SHARED_QUESTION_REL)]
        public IHttpActionResult GetSupportSharedQuestionRel(string sharedID)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_questionService.GetSupportSharedQuestionRel(User.Identity.Name,sharedID));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }

        [ResponseType(typeof(SupportQuestionRelationship))]
        [HttpGet]
        [Route(Constants.API_LINK_GET_USER_SUPPORT_SHARED_QUESTION)]
        public IHttpActionResult GetUserSupportSharedQuestion(string sharedID)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_questionService.GetUserSupportSharedQuestion(sharedID));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }

    }
}
