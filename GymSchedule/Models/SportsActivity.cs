using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using System.ComponentModel.DataAnnotations;

namespace GymSchedule.Models
{
    public class SportsActivity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        [BsonElement("Name")]
        [MinLength(3)]
        [MaxLength(50)]
        public string ActivityName { get; set; }

        [BsonRequired]
        public DateTime StartDate { get; set; }

        [BsonRequired]
        public DateTime EndDate { get; set; }

        [BsonRequired]
        public string GymNumber { get; set; }

        [MaxLength(1000)]
        public string Details { get; set; }

        public bool ToRemove { get; set; }
    }
}
