using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace GymSchedule.RequestBody
{
    public class ActivityBody : IActivityBody
    {
        [MinLength(3)]
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string ActivityName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string GymNumber { get; set; }

        [MaxLength(1000)]
        public string Details { get; set; }
    }
}
