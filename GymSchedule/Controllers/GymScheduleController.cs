using GymSchedule.RequestBody;
using GymSchedule.Services;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<ActivityBody> GetSportsActivityById(string id)
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

            if (activity.Count < 1)
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

            if (activity.Count < 1)
            {
                return NotFound($"Not found activity for this week");
            }

            return activity;
        }

        [Consumes("application/json")]
        [HttpPost("/activities")]
        public ActionResult CreateSportActivity([FromBody] ActivityBody activityBody)
        {
            sportActivityService.CreateSportActivity(activityBody);

            return StatusCode(201);
        }

        [Consumes("application/json")]
        [HttpPut("/activities/{id}")]
        public ActionResult UpdateSportActivity(string id, [FromBody] ActivityBody activityBody)
        {
            var result = sportActivityService.UpdateSportActivity(id, activityBody);
            if (result.ModifiedCount < 1)
            {
                return NotFound("Not found data to update");
            }

            return NoContent();
        }
         
        [HttpDelete("/activities/{id}")]
        public ActionResult UpdateSportActivity(string id)
        {
            var result = sportActivityService.SoftRemoveSportActivityById(id);
            if (result.ModifiedCount < 1)
            {
                return NotFound("Not found data to remove");
            }

            return NoContent();
        }

        [HttpDelete("/activities/{day}")]
        public ActionResult UpdateSportActivity(DateTime day)
        {
            var result = sportActivityService.RemoveSportActivityByDate(day);
            if (result.DeletedCount < 1)
            {
                return NotFound("Not found data to remove");
            }

            return NoContent();
        }
    }
}