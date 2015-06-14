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
    public class SocialPostController : ApiController
    {
        private ISocialPostService _postService;

        public SocialPostController() : this(new SocialPostService()) { }

        public SocialPostController(ISocialPostService postService)
        {
            this._postService = postService;
        }
        #region Create new post
        [ResponseType(typeof(Post))]
        [HttpPost]
        [Route(Constants.API_LINK_POST_CREATE)]
        public IHttpActionResult NewPost(PostCreateAndEditModel newPost)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_postService.Post(User.Identity.Name, newPost.Content));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        #region EditPost
        [ResponseType(typeof(Post))]
        [HttpPut]
        [Route(Constants.API_LINK_POST_EDIT)]
        public IHttpActionResult EditPost(PostCreateAndEditModel editPost, string postID)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {                                    
                    return Ok(_postService.EditPost(postID,editPost.Content,User.Identity.Name));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        #region ViewPost
        [ResponseType(typeof(PostResultModel))]
        [HttpGet]
        [Route(Constants.API_LINK_POST_VIEW)]
        public IHttpActionResult ViewPost(string postID)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {                    
                    return Ok(_postService.ViewPost(postID));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        #region DeletePost
        [ResponseType(typeof(Boolean))]
        [HttpDelete]
        [Route(Constants.API_LINK_POST_DELETE)]
        public IHttpActionResult DeletePost(string postID)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {                                       
                    return Ok(_postService.DeletePost(postID,User.Identity.Name));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        #region LikeUnlikePost
        [ResponseType(typeof(Boolean))]
        [HttpPost]
        [Route(Constants.API_LINK_POST_LIKE_UNLIKE)]
        public IHttpActionResult LikeUnLikePost(string postID)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(_postService.LikeUnLikePost(User.Identity.Name, postID));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion


        #region GetComments
        [ResponseType(typeof(IEnumerable<CommentResultModel>))]
        [HttpGet]
        [Route(Constants.API_LINK_POST_GET_COMMENTS)]
        public IHttpActionResult GetComments(string postID)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {                   
                    return Ok(_postService.GetComments(postID));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        #region GetNewsfeed
        [ResponseType(typeof(IEnumerable<PostResultModel>))]
        [HttpGet]
        [Route(Constants.API_LINK_POST_GET_NEWSFEED)]
        public IHttpActionResult GetNewsfeed()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {                    
                    return Ok(_postService.GetNewsfeed(User.Identity.Name));
                }
                return Ok(Constants.ERROR_AUTHENTICATE);
            }
            catch (Exception e)
            {
                return Ok(Constants.ErrorException(e));
            }
        }
        #endregion

        #region Unfollow
        [ResponseType(typeof(Boolean))]
        [HttpPost]
        [Route(Constants.API_LINK_POST_UNFOLLOW)]
        public IHttpActionResult Unfollow(string postID)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (User.Identity.IsAuthenticated)
                {                    
                    return Ok(_postService.UnFollow(User.Identity.Name, postID));
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
