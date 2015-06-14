using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSEPWebAPI.Models.Repositories.Education
{
    public interface IEducationAchievementRepository
    {
        /// <summary>
        /// Get an Achievement by its ID
        /// </summary>
        /// <param name="achievementID">ID of achievement</param>
        /// <returns></returns>
        Achievement GetAchievement(string achievementID);
        List<Achievement> GetAchievements(String gameName);
        List<Achievement> GetAchievementsByDifficulty(String gameName, int difficulty);
        List<Achievement> GetUserAchievements(String username, String gameName);
        List<Achievement> GetUserAchievementsByDifficulty(String username, String gameName, int difficulty);
        bool ShareAchievement(String username, String achievementID);
    }
}
