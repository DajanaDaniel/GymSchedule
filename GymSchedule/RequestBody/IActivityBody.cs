namespace GymSchedule.RequestBody
{
    public interface IActivityBody
    {
        public string ActivityName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string GymNumber { get; set; }

        public string Details { get; set; }
    }
}
