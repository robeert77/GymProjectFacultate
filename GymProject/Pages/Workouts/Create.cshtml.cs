using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GymProject.Data;
using GymProject.Models;
using Microsoft.AspNetCore.Identity;

namespace GymProject.Pages.Workouts
{
    public class CreateModel : WorkoutExercisesPageModel
    {
        private readonly GymProject.Data.GymProjectContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(GymProject.Data.GymProjectContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            var workout = new Workout();
            workout.WorkoutExercise = new List<WorkoutExercise>();

            PopulateAssignedExerciseData(_context, workout);

            return Page();
        }

        [BindProperty]
        public Workout Workout { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string[] selectedExercises, Dictionary<int, int> exerciseSets, Dictionary<int, int> exerciseRepetitions, Dictionary<int, int> exerciseMaxWeight)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Index"); 
            }

            var newWorkout = new Workout {
                UserId = userId,
                Title = Workout.Title,
                Description = Workout.Description,
                StartTime = Workout.StartTime,
                EndTime = Workout.EndTime,
            };

            if (selectedExercises != null)
            {
                newWorkout.WorkoutExercise = new List<WorkoutExercise>();
                foreach (var exercise in selectedExercises)
                {
                    int exerciseId = int.Parse(exercise);
                    var exerciseToAdd = new WorkoutExercise {
                        ExerciseId = exerciseId,
                        Sets = exerciseSets.ContainsKey(exerciseId) ? exerciseSets[exerciseId] : 0,
                        Repetitions = exerciseRepetitions.ContainsKey(exerciseId) ? exerciseRepetitions[exerciseId] : 0,
                        MaxWeight = exerciseMaxWeight.ContainsKey(exerciseId) ? exerciseMaxWeight[exerciseId] : 0
                    };

                    newWorkout.WorkoutExercise.Add(exerciseToAdd);
                }
            }

            Workout = newWorkout;
            _context.Workout.Add(Workout);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
