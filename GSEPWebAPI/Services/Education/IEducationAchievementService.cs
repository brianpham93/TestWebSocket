using GSEPWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSEPWebAPI.Services.Education
{
    public interface IEducationAchievementService
    {
        Achievement GetAchievement(string achievementID);
        List<Achievement> GetAchievements(String gameName);
        List<Achievement> GetAchievementsByDifficulty(String gameName, int difficulty);
        bool ShareAchievement(String username, String achievementID);
    }
}
