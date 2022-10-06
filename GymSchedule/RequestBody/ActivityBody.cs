using MongoDB.Bson.Serialization.Attributes;

namespace GymSchedule.RequestBody
{
    public class ActivityBody : IActivityBody
    {
        public string ActivityName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string GymNumber { get; set; }

        public string Details { get; set; }
    }
}
