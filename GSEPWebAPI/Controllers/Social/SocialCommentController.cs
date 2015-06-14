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
    public class SocialCommentController : ApiController
    {
        private ISocialCommentService _commentService;

        public SocialCommentController() : this(new SocialCommentService()) { }
        public SocialCommentController(ISocialCommentService commentService)
        {
            this._commentService = commentService;
        }
        #region Comment to a post
        [ResponseType(typeof(Comment))]
        [HttpPost]
        [Route(Constants.API_LINK_COMMENT_CREATE)]
        public IHttpActionResult Comment(string postID, CommentCreateAndEditModel newComment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_commentService.Comment(User.Identity.Name, postID, newComment.Content));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        #region EditComment
        [ResponseType(typeof(Comment))]
        [HttpPut]
        [Route(Constants.API_LINK_COMMENT_EDIT)]
        public IHttpActionResult EditComment(CommentCreateAndEditModel editComment, string commentID)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_commentService.EditComment(commentID, editComment.Content, User.Identity.Name));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        #region View Comment
        [ResponseType(typeof(CommentResultModel))]
        [HttpGet]
        [Route(Constants.API_LINK_COMMENT_VIEW)]
        public IHttpActionResult ViewComment(string commentID)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_commentService.GetComment(commentID));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        #region DeleteComment
        [ResponseType(typeof(Boolean))]
        [HttpDelete]
        [Route(Constants.API_LINK_COMMENT_DELETE)]
        public IHttpActionResult DeleteComment(string commentID)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {

                    return Ok(_commentService.DeleteComment(commentID, User.Identity.Name));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        #region LikeCommnet
        [ResponseType(typeof(Boolean))]
        [HttpPost]
        [Route(Constants.API_LINK_COMMENT_LIKE_UNLIKE)]
        public IHttpActionResult LikeUnLikeComment(string commentID)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_commentService.LikeUnLikeComment(User.Identity.Name, commentID));
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
