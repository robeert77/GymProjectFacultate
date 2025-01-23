using System.ComponentModel.DataAnnotations;

namespace GymProject.Models
{
    public class WorkoutExercise
    {
        public int Id { get; set; }

        public int WorkoutId { get; set; }

        public Workout Workout { get; set; }

        public int ExerciseId { get; set; }

        public Exercise Exercise { get; set; }

        [Display(Name = "Nr. Of Sets")]
        public int Sets { get; set; }

        [Display(Name = "Repetitions/Set")]
        public int Repetitions { get; set; }

        [Display(Name = "Maximum Weight")]
        public int MaxWeight { get; set; }
    }
}
