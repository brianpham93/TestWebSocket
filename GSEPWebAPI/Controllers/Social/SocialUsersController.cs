using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GSEPWebAPI.Models;
using System.IO;
using GSEPWebAPI.App_Start;
using GSEPWebAPI.Services.Social;

namespace GSEPWebAPI.Controllers
{
    public class SocialUsersController : ApiController
    {
        private ISocialUserService _userService;

        public SocialUsersController(): this(new SocialUserService())
        {}

        public SocialUsersController(ISocialUserService userService)
        {
            this._userService = userService;
        }

        #region ShareQuestion
        /// <summary>
        /// Share a incorrectly answered question to newsfeed to let other help you answer
        /// </summary>
        /// <param name="questionID">ID of the question</param>
        /// <returns></returns>
        [ResponseType(typeof(bool))]
        [HttpPost]
        [Route(Constants.API_LINK_USER_SHARE_QUESTION)]        
        public IHttpActionResult ShareQuestion(string questionID)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_userService.ShareQuestion(User.Identity.Name, questionID));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        #region GetProfile by username
        [ResponseType(typeof(SocialUser))]
        [HttpGet]
        [Route(Constants.API_LINK_USER_GET_PROFILE)]
        public IHttpActionResult GetProfile(string username)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {                    
                    return Ok(_userService.GetProfile(username));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        #region Get profile of logged in user
        [ResponseType(typeof(SocialUser))]
        [HttpGet]
        [Route(Constants.API_LINK_USER_GET_PROFILE)]
        public IHttpActionResult GetProfile()
        {
            return GetProfile(User.Identity.Name);
        }
        #endregion

        #region UpdateProfile
        [ResponseType(typeof(SocialUser))]
        [HttpPut]
        [Route(Constants.API_LINK_USER_UPDATE_PROFILE)]
        public IHttpActionResult UpdateProfile(EditProfileModel editInfo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (User.Identity.IsAuthenticated)
                {                    
                    return Ok(_userService.UpdateProfile(User.Identity.Name, editInfo));                    
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion
        
        #region SearchUser by keyword (search in username, firstname, lastname)
        [ResponseType(typeof(IEnumerable<UserSearchResult>))]
        [HttpGet]
        [Route(Constants.API_LINK_USER_SEARCH)]        
        public IHttpActionResult SearchUser(string keyword)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (User.Identity.IsAuthenticated)
                {                    
                    return Ok(_userService.SearchUser(keyword));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion
        
        #region GetNotification
        [ResponseType(typeof(IEnumerable<NotificationsResult>))]
        [HttpGet]
        [Route(Constants.API_LINK_USER_GET_NOTIFICATIONS)]
        public IHttpActionResult GetNotifications()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (User.Identity.IsAuthenticated)
                {                    
                    return Ok(_userService.GetNotifications(User.Identity.Name));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        //todo code for upload avatar in controller
        #region UploadAvatar
        #endregion


      
    }
}