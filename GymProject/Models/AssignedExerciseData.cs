namespace GymProject.Models
{
    public class AssignedExerciseData
    {
        public int ExerciseId { get; set; }
        public WorkoutExercise WorkoutExercise { get; set; }
        public bool Assigned {  get; set; }
    }
}
