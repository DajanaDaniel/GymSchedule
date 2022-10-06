using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GymSchedule.Models
{
    public class SportsActivity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string ActivityName { get; set; }

        [BsonElement]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string GymNumber { get; set; }

        public string Details { get; set; }

        public bool ToRemove { get; set; }
    }
}
