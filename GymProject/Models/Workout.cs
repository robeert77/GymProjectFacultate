using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GymProject.Models
{
    public class Workout
    {
        public int Id { get; set; }

        public string UserId { get; set; }  
        public IdentityUser User { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Start Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:HH:mm dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:HH:mm dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        public ICollection<WorkoutExercise>? WorkoutExercise { get; set; }

        public string GetDuration()
        {
            var duration = EndTime - StartTime;
            return $"{(duration.Hours > 0 ? duration.Hours + " hour" + (duration.Hours > 1 ? "s" : "") : "")} " +
                   $"{(duration.Minutes > 0 ? duration.Minutes + " minute" + (duration.Minutes > 1 ? "s" : "") : "")}".Trim();
        }
    }
}
