using GymSchedule.RequestBody;
using GymSchedule.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using System.ComponentModel.DataAnnotations;

namespace GymSchedule.Controllers
{
    [ApiController]
    [Route("api")]
    public class GymScheduleController : ControllerBase
    {
        private readonly IActivityBody activity;
        private readonly ISportActivityService sportActivityService;

        public GymScheduleController(IActivityBody activity, SportActivityService sportActivity)
        {
            this.sportActivityService = sportActivity;
            this.activity = activity;   
        }

        [Produces("application/json")]
        [HttpGet("/activities/{id}")]
        public ActionResult<ActivityBody> GetSportsActivityById([Required(AllowEmptyStrings = false)] string id)
        {
            var activity = sportActivityService.GetSportActivityById(id);
            if (activity == null)
            {
                return NotFound($"Activity with Id: {id} not found");
            }

            return activity;
        }

        [Produces("application/json")]
        [HttpGet("/activities/today")]
        public ActionResult<List<ActivityBody>> GetDailySportActivitys()
        {
            var activity = sportActivityService.GetDailySportActivitys(DateTime.Today);
            if (activity is null || activity.Count < 1)
            {
                return NotFound($"Not found activity for today");
            }
            
            return activity;
        }

        [Produces("application/json")]
        [HttpGet("/activities/week")]
        public ActionResult<List<ActivityBody>> GetWeekSportActivitys()
        {
            var activity = sportActivityService.GetWeeklySportActivitys(DateTime.Today);
            if (activity is null || activity.Count < 1)
            {
                return NotFound($"Not found activity for this week");
            }

            return activity;
        }

        [Consumes("application/json")]
        [HttpPost("/activities")]
        public ActionResult CreateSportActivity([FromBody] ActivityBody activityBody)
        {
            var validateResult = ValidateRequest(activityBody);
            if (validateResult != null)
            {
                return validateResult;
            }

            sportActivityService.CreateSportActivity(activityBody);

            return StatusCode(201);
        }

        [Consumes("application/json")]
        [HttpPut("/activities/{id}")]
        public ActionResult UpdateSportActivity([Required(AllowEmptyStrings = false)] string id, [FromBody] ActivityBody activityBody)
        {
            var validateResult = ValidateRequest(activityBody);
            if (validateResult != null)
            {
                return validateResult;
            }

            var activity = sportActivityService.UpdateSportActivity(id, activityBody);
            if (activity is null || activity.ModifiedCount < 1)
            {
                return NotFound("Not found data to update");
            }

            return NoContent();
        }
         
        [HttpDelete("/activities/{id}")]
        public ActionResult UpdateSportActivity([Required(AllowEmptyStrings = false)] string id)
        {
            var activity = sportActivityService.SoftRemoveSportActivityById(id);
            if (activity is null || activity.ModifiedCount < 1)
            {
                return NotFound("Not found data to remove");
            }

            return NoContent();
        }

        [HttpDelete("/activities/{day}")]
        public ActionResult UpdateSportActivity([Required(AllowEmptyStrings = false)] DateTime day)
        {
            var activity = sportActivityService.RemoveSportActivityByDate(day);
            if (activity is null || activity.DeletedCount < 1)
            {
                return NotFound("Not found data to remove");
            }

            return NoContent();
        }

        private ActionResult? ValidateRequest(ActivityBody activityBody)
        {
            var startDate = activityBody.StartDate;
            var endDate = activityBody.EndDate;

            if(endDate <= startDate)
            {
                return BadRequest("Start date can not be after end date!");
            }

            var gymNumberAvailable = sportActivityService.CheckAvailableActivitiesGymNumber(startDate, endDate, activityBody.GymNumber);
            if (!gymNumberAvailable)
            {
                return BadRequest($"At that time other activity take place in room {activityBody.GymNumber}");
            }

            return null;
        }
    }
}