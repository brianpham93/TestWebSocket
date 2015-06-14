using GSEPWebAPI.App_Start;
using GSEPWebAPI.Helpers;
using GSEPWebAPI.Models;
using GSEPWebAPI.Services.Social;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace GSEPWebAPI.Controllers.Social
{
    public class SocialRelationshipController : ApiController
    {
        private ISocialRelationshipService _relService;
        public SocialRelationshipController(ISocialRelationshipService relService)
        {
            this._relService = relService;
        }
        public SocialRelationshipController() : this(new SocialRelationshipService()) { }
        #region SendFriendRequest
        [ResponseType(typeof(Relationship))]
        [HttpPost]
        [Route(Constants.API_LINK_RELATIONSHIP_SEND_REQUEST)]
        public IHttpActionResult SendFriendRequest(RelationshipCreateOrEditModel r)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {

                    return Ok(_relService.SendFriendRequest(User.Identity.Name, r.TargetUsername));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        #region ResponseRequest
        [ResponseType(typeof(Boolean))]
        [HttpPost]
        [Route(Constants.API_LINK_RELATIONSHIP_RESPONSE_REQUEST)]
        public IHttpActionResult ResponseRequest(ResponseRequestModel res)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_relService.ReponseRequest(User.Identity.Name, res.TargetUsername, res.Accept));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion


        #region GetRelationship
        [ResponseType(typeof(IEnumerable<Relationship>))]
        [HttpGet]
        [Route(Constants.API_LINK_RELATIONSHIP_GET_RELATIONSHIP)]
        public IHttpActionResult GetRelationship(string username)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {

                    return Ok(_relService.GetRelationship(User.Identity.Name, username));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        #region Unfriend
        [ResponseType(typeof(Boolean))]
        [HttpDelete]
        [Route(Constants.API_LINK_RELATIONSHIP_UNFRIEND)]
        public IHttpActionResult Unfriend(string username)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_relService.Unfriend(User.Identity.Name, username));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion


        #region Cancel Friend Request
        [ResponseType(typeof(Boolean))]
        [HttpDelete]
        [Route(Constants.API_LINK_RELATIONSHIP_CANCEL_REQUEST)]
        public IHttpActionResult CancelRequest(string username)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {

                    return Ok(_relService.CancelFriendRequest(User.Identity.Name, username));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

    }
}
