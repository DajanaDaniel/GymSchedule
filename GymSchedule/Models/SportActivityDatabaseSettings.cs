namespace GymSchedule.Models
{
    public class SportActivityDatabaseSettings : ISportActivityDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
