using GymSchedule.Models;
using GymSchedule.RequestBody;

using MongoDB.Driver;

namespace GymSchedule.Services
{
    public interface ISportActivityService
    {
        List<ActivityBody> GetDailySportActivities(DateTime day);
        ActivityBody? GetSportActivityById(string id);
        List<ActivityBody> GetWeeklySportActivities(DateTime day);
        void CreateSportActivity(ActivityBody sportsActivity);
        UpdateResult UpdateSportActivity(string id, ActivityBody sportsActivity);
        UpdateResult SoftRemoveSportActivityById(string id);
        DeleteResult RemoveSportActivityByDate(DateTime day);
        bool CheckAvailableActivitiesGymNumber(DateTime startDate, DateTime endDate, string gymNumber);
    }
}
