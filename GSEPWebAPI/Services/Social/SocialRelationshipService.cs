using GSEPWebAPI.App_Start;
using GSEPWebAPI.Helpers.Neo;
using GSEPWebAPI.Models;
using GSEPWebAPI.Models.Repositories.Social;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSEPWebAPI.Services.Social
{
    public class SocialRelationshipService : ISocialRelationshipService
    {
        private SocialRelationshipRepository _relRepository;
        public SocialRelationshipService()
        {
            this._relRepository = new SocialRelationshipRepository();
        }
        public bool CancelFriendRequest(string username, string targetUsername)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            if (neo4jHelper.GetNode<SocialUser>(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, targetUsername)) == null)
            {
                throw new Exception(Constants.ErrorUserNotExist(username));
            }

            Relationship relationship = neo4jHelper.GetRelationShip<Relationship>(
                                        Constants.LABEL_USER, new KeyString(Constants.KEY_USER, targetUsername),
                                        Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username),
                                        Constants.REL_USER_USER);
            if (relationship != null)
            {
                if (relationship.Status != null && relationship.Status.Equals(Constants.REL_STATUS_FRIEND))
                {
                    throw new Exception(Constants.ERROR_TWO_USER_ALREADY_FRIEND);
                }
            }            
            return _relRepository.CancelFriendRequest(username, targetUsername);
        }

        public IEnumerable<Relationship> GetRelationship(string username, string targetUsername)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            if (neo4jHelper.GetNode<SocialUser>(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, targetUsername)) == null)
            {
                throw new Exception(Constants.ErrorUserNotExist(targetUsername));
            }            
            Relationship relationship = _relRepository.GetRelationship(username, targetUsername);
            Relationship relationshipReverse = _relRepository.GetRelationship(targetUsername, username);
            List<Relationship> relResult = new List<Relationship>();
            relResult.Add(relationship);
            relResult.Add(relationshipReverse);
            return relResult;
        }

        public bool ReponseRequest(string username, string targetUsername, bool accept)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            if (neo4jHelper.GetNode<SocialUser>(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, targetUsername)) == null)
            {
                throw new Exception(Constants.ErrorUserNotExist(targetUsername));
            }
            Relationship relationship = neo4jHelper.GetRelationShip<Relationship>(
                                        Constants.LABEL_USER, new KeyString(Constants.KEY_USER, targetUsername),
                                        Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username),
                                        Constants.REL_USER_USER);
            if (relationship != null)
            {
                if (relationship.Status != null && relationship.Status.Equals(Constants.REL_STATUS_FRIEND))
                {
                    throw new Exception(Constants.ERROR_TWO_USER_ALREADY_FRIEND);
                }
            }
            else
            {
                throw new Exception(Constants.ERROR_NOT_HAVE_REQUEST);
            }            
            return _relRepository.ReponseRequest(username, targetUsername, accept);
        }

        public Relationship SendFriendRequest(string username, string targetUsername)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            if (neo4jHelper.GetNode<SocialUser>(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, targetUsername)) == null)
            {
                throw new Exception(Constants.ErrorUserNotExist(targetUsername));
            }
            if (neo4jHelper.IsRelate(Constants.REL_USER_USER,
                                     Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username),
                                     Constants.LABEL_USER, new KeyString(Constants.KEY_USER, targetUsername)))
            {
                throw new Exception(Constants.ERROR_TWO_USER_ALREADY_RELATE);
            }
            if (neo4jHelper.IsRelate(Constants.REL_USER_USER,
                                     Constants.LABEL_USER, new KeyString(Constants.KEY_USER, targetUsername),
                                     Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username))
                                     )
            {
                throw new Exception(Constants.ERROR_TWO_USER_ALREADY_RELATE);
            }            
            return _relRepository.SendFriendRequest(username, targetUsername);
        }

        public bool Unfriend(string username, string targetUsername)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            if (neo4jHelper.GetNode<SocialUser>(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, targetUsername)) == null)
            {
                throw new Exception(Constants.ErrorUserNotExist(targetUsername));
            }

            Relationship relationship = neo4jHelper.GetRelationShip<Relationship>(
                                        Constants.LABEL_USER, new KeyString(Constants.KEY_USER, targetUsername),
                                        Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username),
                                        Constants.REL_USER_USER);
            if (relationship != null)
            {
                if (relationship.Status != null && !relationship.Status.Equals(Constants.REL_STATUS_FRIEND))
                {
                    throw new Exception(Constants.ERROR_TWO_USER_NOT_FRIEND);
                }
            }            
            return _relRepository.Unfriend(username, targetUsername);
        }
    }
}