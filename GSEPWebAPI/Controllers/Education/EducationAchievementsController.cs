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
    public class EducationAchievementsController : ApiController
    {
        private IEducationAchievementService _achievementService;
        public EducationAchievementsController() : this(new EducationAchievementService()) { }
        public EducationAchievementsController(IEducationAchievementService achievementService)
        {
            this._achievementService = achievementService;
        }


        [ResponseType(typeof(List<Achievement>))]
        [HttpGet]
        [Route(Constants.API_LINK_ACHIEVEMENT_GET_ACHIEVEMENTS)]
        public IHttpActionResult GetAchievements(String gameName)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_achievementService.GetAchievements(gameName));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }

        [ResponseType(typeof(List<Achievement>))]
        [HttpGet]
        [Route(Constants.API_LINK_ACHIEVEMENT_GET_ACHIEVEMENTS_BY_DIFFICULTY)]
        public IHttpActionResult GetAchievementsByDifficulty(String gameName, int difficulty)
                                                                                                                                                                                                                                        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_achievementService.GetAchievementsByDifficulty(gameName, difficulty));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }

        [ResponseType(typeof(bool))]
        [Route(Constants.API_LINK_ACHIEVEMENT_SHARE_ACHIEVEMENT)]        
        [HttpPost]
        public IHttpActionResult ShareAchievement(String achievementID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (User.Identity.IsAuthenticated)
            {
                return Ok(_achievementService.ShareAchievement(User.Identity.Name, achievementID));
            }
            return Ok(Constants.ERROR_AUTHENTICATE);
        }

        [Route(Constants.API_LINK_ACHIEVEMENT_GET_ACHIEVEMENT)]
        [ResponseType(typeof(Achievement))]
        [HttpGet]
        public IHttpActionResult GetAchievement(String achievementID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (User.Identity.IsAuthenticated)
            {
                return Ok(_achievementService.GetAchievement(achievementID));
            }
            return Ok(Constants.ERROR_AUTHENTICATE);
        }
      
    }
}
