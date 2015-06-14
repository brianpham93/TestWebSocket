using GSEPWebAPI.App_Start;
using GSEPWebAPI.Helpers;
using GSEPWebAPI.Helpers.Neo;
using GSEPWebAPI.Models;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSEPWebAPI.Models.Repositories.Social
{
    public class SocialPostRepository : ISocialPostRepository
    {
        public SocialPostRepository() : base() { }
        public Post Post(string username, string content)
        {
            string timeStamp = Constants.TimeStamp();
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            Post post = new Post(timeStamp, content, double.Parse(timeStamp));
            neo4jHelper.CreateNode(post, Constants.LABEL_POST);
            neo4jHelper.CreateRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), Constants.LABEL_POST, new KeyString(Constants.KEY_POST, timeStamp), Constants.REL_USER_POST, default(Object));
            return post;
        }

        public Post EditPost(string postID, string content)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            string timeStamp = Constants.TimeStamp();
            Post post = new Post(postID, content, double.Parse(timeStamp));
            neo4jHelper.UpdateNode(post, Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID));
            return post;

        }

        public Post ViewPost(string postID)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            Post post = neo4jHelper.GetNode<Post>(Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID));
            return post;
        }

        private IEnumerable<Post> GetPosts(string username)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            return neo4jHelper.GetNodes<Post>(Constants.LABEL_POST, Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), Constants.REL_USER_POST);
        }

        public bool DeletePost(string postID)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            // Delete all comment first
            IEnumerable<Comment> listComments = GetComments(postID);
            foreach (var comment in listComments)
            {
                neo4jHelper.DeleteNode(Constants.LABEL_COMMENT, new KeyString(Constants.KEY_COMMENT, comment.CommentID));
            }
            return neo4jHelper.DeleteNode(Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="postID"></param>
        /// <returns>True if like, False if unlike</returns>
        public bool LikeUnLikePost(string username, string postID)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            if (!neo4jHelper.IsRelate(Constants.REL_USER_LIKE_POST, Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID)))
            {
                neo4jHelper.CreateRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID), Constants.REL_USER_LIKE_POST, default(Object));
                return true;
            }
            else
            {
                neo4jHelper.DeleteRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username), Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID), Constants.REL_USER_LIKE_POST);
                return false;
            }

        }

        public IEnumerable<Comment> GetComments(string postID)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            IEnumerable<Comment> results = neo4jHelper.GetNodes<Comment>(Constants.LABEL_COMMENT, Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID), Constants.REL_POST_COMMENT);
            return results.OrderByDescending(comment => comment.CommentID);
        }

        public IEnumerable<TimelineAndNewsfeedModel> GetNewsfeed(string username)
        {
            GraphClient _graphClient;
            _graphClient = new GraphClient(new Uri(Constants.GRAPH_URL));
            _graphClient.Connect();


            List<PostResultModel> posts = GetNewsfeedPosts(username, _graphClient);
            List<ShareQuestionResultModel> questions = GetNewsfeedSharedQuestions(username, _graphClient);
            //List<ShareAchievementResultModel> achievements = GetNewsfeedAchievement(username, _graphClient);

            List<TimelineAndNewsfeedModel> newsfeed = new List<TimelineAndNewsfeedModel>();
            foreach (var item in posts)
            {
                TimelineAndNewsfeedModel feed = new TimelineAndNewsfeedModel(item, null, null, item.Post.Time);
                newsfeed.Add(feed);
            }

            foreach (var item in questions)
            {
                TimelineAndNewsfeedModel feed = new TimelineAndNewsfeedModel(null, item, null, item.Time);
                newsfeed.Add(feed);
            }

            //foreach (var item in achievements)
            //{
            //    TimelineAndNewsfeedModel feed = new TimelineAndNewsfeedModel(null, null, item, item.Time);
            //    newsfeed.Add(feed);
            //}
            return newsfeed.OrderByDescending(item => item.Time);
        }

        private List<ShareQuestionResultModel> GetNewsfeedSharedQuestions(string username, GraphClient _graphClient)
        {            
            //Get shared questions
            string sharedQuestionMatchString = "(user1:" + Constants.LABEL_USER + "), "
                + "(user2:" + Constants.LABEL_USER + "), "
                + "(SharedQuestion:" + Constants.LABEL_SHARED_QUESTION + ") ";

            string sharedQuestionWhereString = "(user1)-[:" + Constants.REL_USER_USER + " {" + Constants.REL_STATUS_KEY
                + ": '" + Constants.REL_STATUS_FRIEND + "'}]-(user2) and "
                + "(user2)-[:" + Constants.REL_USER_SHARED_QUESTION + "]->(SharedQuestion)"
                + " and user1." + Constants.KEY_USER + "='" + username + "'";

            IEnumerable<SharedQuestionModel> friendSharedQuestions = _graphClient.Cypher
                .Match(sharedQuestionMatchString)
                .Where(sharedQuestionWhereString)
                .Return<SharedQuestionModel>("SharedQuestion").Results;

            List<ShareQuestionResultModel> sharedQuestionResults = new List<ShareQuestionResultModel>();
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);

            foreach (var question in friendSharedQuestions)
            {
                Question originQuestion = neo4jHelper.GetNodes<Question>(Constants.LABEL_SHARED_QUESTION, new KeyString(Constants.KEY_SHARED_QUESTION, question.SharedID),
                    Constants.LABEL_QUESTION, Constants.REL_QUESTION_SHARED_QUESTION).FirstOrDefault();

                SocialUser user = neo4jHelper.GetNodes<SocialUser>(Constants.LABEL_SHARED_QUESTION,
                    new KeyString(Constants.KEY_SHARED_QUESTION, question.SharedID),
                    Constants.LABEL_USER, Constants.REL_USER_SHARED_QUESTION).FirstOrDefault();

                ShareQuestionResultModel shareQuestionResult = new ShareQuestionResultModel(originQuestion, user.Username,
                    user.FirstName + " " + user.LastName, question.SharedID, question.Time);
                sharedQuestionResults.Add(shareQuestionResult);
            }

            return sharedQuestionResults;
        }

        private List<PostResultModel> GetNewsfeedPosts(string username, GraphClient _graphClient)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            List<PostResultModel> postResults = new List<PostResultModel>();   
            //Get posts
            IEnumerable<Post> posts = GetPosts(username);                     
            foreach (var item in posts)
            {
                SocialUser userOwner = neo4jHelper.GetNodes<SocialUser>(Constants.LABEL_POST, 
                    new KeyString(Constants.KEY_POST, item.PostID), Constants.LABEL_USER,
                    Constants.REL_USER_POST).FirstOrDefault();
                PostResultModel post = new PostResultModel(item, userOwner.Username, userOwner.FirstName + " " + userOwner.LastName);
                postResults.Add(post);
            }            

            string matchString = "(user1:" + Constants.LABEL_USER + "), " + "(user2:" + Constants.LABEL_USER + "), "
                + "(post:" + Constants.LABEL_POST + ") ";

            string whereString = "(user1)-[:" + Constants.REL_USER_USER + " {" + Constants.REL_STATUS_KEY + ": '" + Constants.REL_STATUS_FRIEND + "'}]-(user2) and " +
                                 "(user2)-[:" + Constants.REL_USER_POST + "]->(post)" +
                                 " and user1." + Constants.KEY_USER + "='" + username + "'";
            IEnumerable<Post> frPosts = _graphClient.Cypher
                .Match(matchString)
                .Where(whereString)
                .Return<Post>("post").Results;

            foreach (var item in frPosts)
            {
                SocialUser userOwner = neo4jHelper.GetNodes<SocialUser>(Constants.LABEL_POST, 
                    new KeyString(Constants.KEY_POST, item.PostID), Constants.LABEL_USER,
                    Constants.REL_USER_POST).FirstOrDefault();
                PostResultModel post = new PostResultModel(item, userOwner.Username, userOwner.FirstName + " " + userOwner.LastName);
                postResults.Add(post);
            }

            return postResults;
        }

        private List<ShareAchievementResultModel> GetNewsfeedAchievement(string username, GraphClient _graphClient)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);

            string sharedAchievementMatchString = "(user1:" + Constants.LABEL_USER + "), "
               + "(user2:" + Constants.LABEL_USER + "), "
               + "(Achievement:" + Constants.LABEL_ACHIEVEMENT + ") ";

            string sharedAchievementWhereString = "(user1)-[:" + Constants.REL_USER_USER + " {" + Constants.REL_STATUS_KEY
                + ": '" + Constants.REL_STATUS_FRIEND + "'}]-(user2) and "
                + "(user2)-[:" + Constants.REL_USER_SHARE_ACHIEVEMENT + "]->(Achievement)"
                + " and user1." + Constants.KEY_USER + "='" + username + "'";

            IEnumerable<Achievement> friendSharedQuestions = _graphClient.Cypher
                .Match(sharedAchievementMatchString)
                .Where(sharedAchievementWhereString)
                .Return<Achievement>("Achievement").Results;

            List<ShareAchievementResultModel> achievementResults = new List<ShareAchievementResultModel>();

            foreach (var item in friendSharedQuestions)
            {
                
                String shareAchievementTime = neo4jHelper.GetRelationShip<String>(
                    Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username),
                    Constants.LABEL_ACHIEVEMENT, new KeyString(Constants.LABEL_ACHIEVEMENT, item.AchievementID),
                    Constants.REL_USER_SHARE_ACHIEVEMENT
                    );
                ShareAchievementResultModel achievementResult = new ShareAchievementResultModel(item, double.Parse(shareAchievementTime));
                achievementResults.Add(achievementResult);
            }
            return achievementResults;
        }

        public void Following(string username, string postID)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            neo4jHelper.CreateRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username),
                                           Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID),
                                           Constants.REL_USER_FOLLOW_POST, default(Object));
        }

        public bool UnFollow(string username, string postID)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            neo4jHelper.DeleteRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username),
                                           Constants.LABEL_POST, new KeyString(Constants.KEY_POST, postID),
                                           Constants.REL_USER_FOLLOW_POST);
            return true;
        }
    }
}