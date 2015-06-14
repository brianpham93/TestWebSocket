using GSEPWebAPI.App_Start;
using GSEPWebAPI.Helpers;
using GSEPWebAPI.Helpers.Neo;
using GSEPWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSEPWebAPI.Models.Repositories.Social
{
    public class SocialCommentRepository : ISocialCommentRepository
    {
        public SocialCommentRepository() : base() { }
        public Comment Comment(string username,string postID, string content)
        {
            string timeStamp = Constants.TimeStamp();
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            Comment comment = new Comment(timeStamp, content, double.Parse(timeStamp));
            neo4jHelper.CreateNode(comment, Constants.LABEL_COMMENT);
            neo4jHelper.CreateRelationShip(Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID),Constants.LABEL_COMMENT, new KeyString(Constants.KEY_COMMENT,timeStamp),Constants.REL_POST_COMMENT, default(Object));
            neo4jHelper.CreateRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username),Constants.LABEL_COMMENT, new KeyString(Constants.KEY_COMMENT, timeStamp),Constants.REL_USER_COMMENT, default(Object));
            return comment;
        }

        public Comment EditComment(string commentID, string content)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            string timeStamp = Constants.TimeStamp();
            Comment comment = new Comment(commentID, content, double.Parse(timeStamp));
            neo4jHelper.UpdateNode(comment, Constants.LABEL_COMMENT, new KeyString(Constants.KEY_COMMENT, commentID));
            return comment;
        }

        public Comment GetComment(string commentID)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            Comment comment = neo4jHelper.GetNode<Comment>(Constants.LABEL_COMMENT, new KeyString(Constants.KEY_COMMENT, commentID));
            return comment;
        }

        public bool DeleteComment(string commentID)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            return neo4jHelper.DeleteNode(Constants.LABEL_COMMENT, new KeyString(Constants.KEY_COMMENT, commentID));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="commentID"></param>
        /// <returns></returns>
        public bool LikeUnLikeComment(string username, string commentID)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            if (!neo4jHelper.IsRelate(Constants.REL_USER_LIKE_COMMENT, Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), Constants.LABEL_COMMENT, new KeyString(Constants.KEY_COMMENT, commentID)))
            {
                neo4jHelper.CreateRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), Constants.LABEL_COMMENT, new KeyString(Constants.KEY_COMMENT, commentID), Constants.REL_USER_LIKE_COMMENT, default(Object));
                return true;
            }
            else
            {
                neo4jHelper.DeleteRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), Constants.LABEL_COMMENT, new KeyString(Constants.KEY_COMMENT, commentID), Constants.REL_USER_LIKE_COMMENT);
                return false;
            }

        }
        
    }
}