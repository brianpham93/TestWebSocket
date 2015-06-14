using GSEPWebAPI.App_Start;
using GSEPWebAPI.Helpers.Neo;
using GSEPWebAPI.Models.Repositories.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GSEPWebAPI.Models;

namespace GSEPWebAPI.Services.Education
{
    public class EducationAchievementService : IEducationAchievementService
    {


        private IEducationAchievementRepository _achievementRepository;
        public EducationAchievementService()
        {
            _achievementRepository = new EducationAchievementRepository();
        }
        public List<Models.Achievement> GetAchievements(string gameName)
        {
            return _achievementRepository.GetAchievements(gameName);            
        }

        public List<Models.Achievement> GetAchievementsByDifficulty(string gameName, int difficulty)
        {
            return _achievementRepository.GetAchievementsByDifficulty(gameName, difficulty);
        }


        public bool ShareAchievement(string username, string achievementID)
        {
            Neo4jHelper neo4jHelper = new Neo4jHelper(Constants.GRAPH_URL);

            Object shareAchivement = neo4jHelper.GetRelationShip<Object>(Constants.LABEL_USER,
                new KeyString(Constants.KEY_USER, username), Constants.LABEL_ACHIEVEMENT, 
                new KeyString(Constants.KEY_ACHIEVEMENT, achievementID), Constants.REL_USER_SHARE_ACHIEVEMENT);

            if (shareAchivement != null)
            {
                return false;
            }
            
            return _achievementRepository.ShareAchievement(username, achievementID);
        }

        public Achievement GetAchievement(string achievementID)
        {
            return _achievementRepository.GetAchievement(achievementID);
        }
    }
}