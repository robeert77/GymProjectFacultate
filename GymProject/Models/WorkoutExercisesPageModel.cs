using Microsoft.AspNetCore.Mvc.RazorPages;
using GymProject.Data;
using System.Diagnostics.CodeAnalysis;

namespace GymProject.Models
{
    public class WorkoutExercisesPageModel : PageModel
    {
        public List<AssignedExerciseData> AssignedExerciseDataList;

        public void PopulateAssignedExerciseData(GymProjectContext context, Workout workout)
        {
            var allExercises = context.Exercises.ToList();
            var workoutExercises = workout.WorkoutExercise.ToDictionary(we => we.ExerciseId);

            AssignedExerciseDataList = new List<AssignedExerciseData>();

            foreach (var exercise in allExercises)
            {
                AssignedExerciseDataList.Add(new AssignedExerciseData
                {
                    ExerciseId = exercise.Id,
                    WorkoutExercise = workoutExercises.ContainsKey(exercise.Id)
                        ? workoutExercises[exercise.Id]
                        : new WorkoutExercise {
                            ExerciseId = exercise.Id,
                            Exercise = new Exercise {
                                Title = exercise.Title,
                            },
                            Sets = 0,
                            Repetitions = 0,
                            MaxWeight = 0
                        },
                    Assigned = workoutExercises.ContainsKey(exercise.Id) // Check if the exercise is assigned
                });
            }
        }

        public void UpdateWorkoutExercises(GymProjectContext context, string[] selectedExercises, Dictionary<int, int> workoutExerciseSets, Dictionary<int, int> workoutExerciseReps, Dictionary<int, int> workoutExerciseWeights, Workout workoutToUpdate)
        {
            if (selectedExercises == null)
            {
                workoutToUpdate.WorkoutExercise = new List<WorkoutExercise>();
                return;
            }

            var selectedExercisesHS = new HashSet<String>(selectedExercises);
            var workoutExercises = workoutToUpdate.WorkoutExercise.ToDictionary(e => e.ExerciseId);

            foreach (var exercise in context.Exercises)
            {
                if (selectedExercisesHS.Contains(exercise.Id.ToString()))
                {
                    if (!workoutExercises.ContainsKey(exercise.Id))
                    {
                        workoutToUpdate.WorkoutExercise.Add(
                            new WorkoutExercise {
                                WorkoutId = workoutToUpdate.Id,
                                ExerciseId = exercise.Id,
                                Sets = workoutExerciseSets.ContainsKey(exercise.Id) ? workoutExerciseSets[exercise.Id] : 0,
                                Repetitions = workoutExerciseReps.ContainsKey(exercise.Id) ? workoutExerciseReps[exercise.Id] : 0,
                                MaxWeight = workoutExerciseWeights.ContainsKey(exercise.Id) ? workoutExerciseWeights[exercise.Id] : 0,
                            });
                    }
                    else
                    {
                        var existingWorkoutExercise = workoutExercises[exercise.Id];
                        existingWorkoutExercise.Sets = workoutExerciseSets.ContainsKey(exercise.Id) ? workoutExerciseSets[exercise.Id] : existingWorkoutExercise.Sets;
                        existingWorkoutExercise.Repetitions = workoutExerciseReps.ContainsKey(exercise.Id) ? workoutExerciseReps[exercise.Id] : existingWorkoutExercise.Repetitions;
                        existingWorkoutExercise.MaxWeight = workoutExerciseWeights.ContainsKey(exercise.Id) ? workoutExerciseWeights[exercise.Id] : existingWorkoutExercise.MaxWeight;
                    }
                }
                else if (workoutExercises.ContainsKey(exercise.Id))
                {
                    WorkoutExercise courseToRemove = workoutToUpdate.WorkoutExercise.SingleOrDefault(i => i.ExerciseId == exercise.Id);
                    context.Remove(courseToRemove);
                }
            }
        }
    }
}
