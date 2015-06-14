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
    public class SocialRelationshipRepository : ISocialRelationshipRepository
    {
        public SocialRelationshipRepository() : base() { }
        public Relationship SendFriendRequest(string username, string targetUsername)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            Relationship rel = new Relationship(Constants.REL_STATUS_PENDING);
            neo4jHelper.CreateRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username),
                                           Constants.LABEL_USER, new KeyString(Constants.KEY_USER, targetUsername),
                                           Constants.REL_USER_USER, rel);
            return rel;
        }
        public bool ReponseRequest(string username, string targetUsername, bool accept)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            if (accept)
            {
                Relationship rel = new Relationship(Constants.REL_STATUS_FRIEND);
                neo4jHelper.CreateRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username),
                                           Constants.LABEL_USER, new KeyString(Constants.KEY_USER, targetUsername),
                                           Constants.REL_USER_USER, rel);
                neo4jHelper.UpdateRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, targetUsername),
                                           Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username),
                                           Constants.REL_USER_USER, rel);
            }
            else
            {
                neo4jHelper.DeleteRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, targetUsername),
                                               Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username),
                                               Constants.REL_USER_USER);
            }
            return true;
        }
        

        public Relationship GetRelationship(string username, string targetUsername)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            return neo4jHelper.GetRelationShip<Relationship>(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username),
                                           Constants.LABEL_USER, new KeyString(Constants.KEY_USER, targetUsername),
                                           Constants.REL_USER_USER);
        }

        public bool Unfriend(string username, string targetUsername)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            neo4jHelper.DeleteRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, targetUsername),
                                               Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username),
                                               Constants.REL_USER_USER);
            neo4jHelper.DeleteRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username),
                                           Constants.LABEL_USER, new KeyString(Constants.KEY_USER, targetUsername),                                               
                                           Constants.REL_USER_USER);
            return true;
        }

        public bool CancelFriendRequest(string username, string targetUsername)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);           
            neo4jHelper.DeleteRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username),
                                           Constants.LABEL_USER, new KeyString(Constants.KEY_USER, targetUsername),
                                           Constants.REL_USER_USER);
            return true;
        }
    }
}