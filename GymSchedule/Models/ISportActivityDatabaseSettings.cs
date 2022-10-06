
namespace GymSchedule.Models
{
    public interface ISportActivityDatabaseSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
