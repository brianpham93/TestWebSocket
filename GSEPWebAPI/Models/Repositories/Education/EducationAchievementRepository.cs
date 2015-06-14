using GSEPWebAPI.App_Start;
using GSEPWebAPI.Helpers.Neo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSEPWebAPI.Models.Repositories.Education
{
    public class EducationAchievementRepository : IEducationAchievementRepository
    {
        /// <summary>
        /// Get all achievements of a game
        /// </summary>        
        /// <param name="gameName">Name of the game that achievements belonged to</param>
        /// <returns></returns>
        public List<Achievement> GetAchievements(string gameName)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            IEnumerable<Achievement> achievements = neo4jHelper.SearchNodes<Achievement>(Constants.LABEL_ACHIEVEMENT,
                                                                                gameName, new string[] { "GameName" });
            return achievements.ToList();
        }

        /// <summary>
        /// Get all achievements of a game that have desired difficulty
        /// </summary>
        /// <param name="gameName">Name of the game that achievements belong to</param>
        /// <param name="difficulty">Desired difficulty of achievement</param>
        /// <returns></returns>
        public List<Achievement> GetAchievementsByDifficulty(string gameName, int difficulty)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            IEnumerable<Achievement> achievements = neo4jHelper.SearchNodes<Achievement>(Constants.LABEL_ACHIEVEMENT,
                                                     gameName, new string[] { "GameName" });
            List<Achievement> results = new List<Achievement>();
            foreach (var achievement in achievements)
            {
                if (achievement.Difficulty == difficulty)
                {
                    results.Add(achievement);
                }
            }
            return results;
        }

        public List<Achievement> GetUserAchievements(string username, string gameName)
        {

            throw new NotImplementedException();
        }

        public List<Achievement> GetUserAchievementsByDifficulty(string username, string gameName, int difficulty)
        {
            throw new NotImplementedException();
        }


        public bool ShareAchievement(string username, string achievementID)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);

            ShareAchievement shareAchievement = new ShareAchievement(double.Parse(Constants.TimeStamp()));

            neo4jHelper.CreateRelationShip(Constants.LABEL_USER, new KeyString(Constants.KEY_USER, username),
                Constants.LABEL_ACHIEVEMENT, new KeyString(Constants.KEY_ACHIEVEMENT, achievementID),
                Constants.REL_USER_SHARE_ACHIEVEMENT, shareAchievement);
            return true;

        }

        public Achievement GetAchievement(string achievementID)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);
            Achievement achievement = neo4jHelper.GetNode<Achievement>("Achievement", new KeyString(Constants.KEY_ACHIEVEMENT, achievementID));
            return achievement;
        }
    }
}