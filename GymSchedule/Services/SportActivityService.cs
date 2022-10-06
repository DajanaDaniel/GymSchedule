using GymSchedule.Models;
using GymSchedule.RequestBody;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;

namespace GymSchedule.Services
{
    public class SportActivityService : ISportActivityService
    {
        private readonly IMongoCollection<SportsActivity> _sportsActivityCollection;

        public SportActivityService(IOptions<SportActivityDatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

            _sportsActivityCollection = mongoDatabase.GetCollection<SportsActivity>(settings.Value.CollectionName);
        }

        public void CreateSportActivity(ActivityBody activity)
        {
            var activityDbObject = new SportsActivity
            {
                ActivityName = activity.ActivityName,
                StartDate = activity.StartDate,
                EndDate = activity.EndDate,
                GymNumber = activity.GymNumber,
                Details = activity.Details,
                ToRemove = false
            };
            _sportsActivityCollection.InsertOne(activityDbObject);
        }
            
        public List<ActivityBody> GetDailySportActivitys(DateTime day)
        {
            var builder = Builders<SportsActivity>.Filter;
            var filter = builder.Gte("StartDate", day)
                & builder.Lte("StartDate", day.AddHours(24))
                & builder.Eq("ToRemove", false);

            var activityList = _sportsActivityCollection.Find(filter).ToList();

            return activityList.Select(activity => new ActivityBody
            {
                ActivityName = activity.ActivityName,
                StartDate = activity.StartDate,
                EndDate = activity.EndDate,
                Details = activity.Details
            }).ToList();
        }
            
        public ActivityBody? GetSportActivityById(string id)
        {
            var builder = Builders<SportsActivity>.Filter;
            var filter = builder.Eq("Id", id) & builder.Eq("ToRemove", false);

            var activity = _sportsActivityCollection.Find(filter).Single();

            return new ActivityBody
            {
                ActivityName = activity.ActivityName,
                StartDate = activity.StartDate,
                EndDate = activity.EndDate,
                GymNumber=activity.GymNumber,  
                Details=activity.Details
            };
        }
            
        public List<ActivityBody> GetWeeklySportActivitys(DateTime day)
        {
            var cuurenDayOfWeek = (int)day.DayOfWeek;
            var monday = day.AddDays(-cuurenDayOfWeek+1);
            var sunday = day.AddDays(-cuurenDayOfWeek+7);

            var builder = Builders<SportsActivity>.Filter;
            var filter = builder.Gte("StartDate", monday)
                & builder.Lte("StartDate", sunday.AddHours(24))
                & builder.Eq("ToRemove", false);

            var activityList = _sportsActivityCollection.Find(filter).ToList();

            return activityList.Select(activity => new ActivityBody
            {
                ActivityName = activity.ActivityName,
                StartDate = activity.StartDate,
                EndDate = activity.EndDate,
                Details = activity.Details
            }).ToList();
        }

        public DeleteResult RemoveSportActivityByDate(DateTime activityDay)
        {
            var builder = Builders<SportsActivity>.Filter;
            var filter = builder.Gte("StartDate", activityDay)
                & builder.Lte("StartDate", activityDay.AddHours(24))
                & builder.Eq("ToRemove", false);

            return _sportsActivityCollection.DeleteMany(filter);
        }

        public UpdateResult SoftRemoveSportActivityById(string id)
        {
            var filter = Builders<SportsActivity>.Filter.Eq("Id", id);
            var update = Builders<SportsActivity>.Update.Set("ToRemove", true);

            return _sportsActivityCollection.UpdateOne(filter, update);
        }

        public UpdateResult UpdateSportActivity(string id, ActivityBody activity)
        {
            var builderFilter = Builders<SportsActivity>.Filter;
            var filter = builderFilter.Eq("Id", id) & builderFilter.Eq("ToRemove", false);

            var update = Builders<SportsActivity>.Update.Set(nameof(SportsActivity.ActivityName), activity.ActivityName)
                .Set(nameof(SportsActivity.StartDate), activity.StartDate)
                .Set(nameof(SportsActivity.EndDate), activity.EndDate)
                .Set(nameof(SportsActivity.GymNumber), activity.GymNumber)
                .Set(nameof(SportsActivity.Details), activity.Details);

            return _sportsActivityCollection.UpdateOne(filter, update);
        }
    }
}
