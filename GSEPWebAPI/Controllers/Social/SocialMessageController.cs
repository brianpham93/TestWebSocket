using GSEPWebAPI.App_Start;
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
    public class SocialMessageController : ApiController
    {
        private ISocialMessageService _messageService;
        public SocialMessageController() : this(new SocialMessageService()) { }
        public SocialMessageController(ISocialMessageService messageService)
        {
            this._messageService = messageService;
        }


        #region SendMessage
        [ResponseType(typeof(Message))]
        [HttpPost]
        [Route(Constants.API_LINK_MESSAGE_SEND)]
        public IHttpActionResult SendMessage(SendMessageModel mess)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_messageService.SendMessage(User.Identity.Name, mess.TargetUsername, mess.Content));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        #region GetMessage
        [ResponseType(typeof(Message))]
        [HttpGet]
        [Route(Constants.API_LINK_MESSAGE_GET_MESSAGE)]
        public IHttpActionResult SendMessage(string messageID)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_messageService.GetMessage(messageID));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        #region GetConversation
        [ResponseType(typeof(List<MessageConversation>))]
        [HttpGet]
        [Route(Constants.API_LINK_MESSAGE_GET_CONVERSATION)]
        public IHttpActionResult GetConversation(string targetUsername)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_messageService.GetConversation(User.Identity.Name, targetUsername));
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
