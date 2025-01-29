using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymProject.Data;
using GymProject.Models;
using Microsoft.Identity.Client.AppConfig;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.AspNetCore.Identity;

namespace GymProject.Pages.Workouts
{
    public class EditModel : WorkoutExercisesPageModel
    {
        private readonly GymProject.Data.GymProjectContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(GymProject.Data.GymProjectContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Workout Workout { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout =  await _context.Workout
                        .Include(w => w.WorkoutExercise).ThenInclude(w => w.Exercise)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(w => w.Id == id);

            if (workout == null)
            {
                return NotFound();
            }

            PopulateAssignedExerciseData(_context, workout);

            Workout = workout;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedExercises, Dictionary<int, int> exerciseSets, Dictionary<int, int> exerciseRepetitions, Dictionary<int, int> exerciseMaxWeight)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var workoutToUpdate = await _context.Workout
                        .Include(w => w.WorkoutExercise).ThenInclude(w => w.Exercise)
                        .FirstOrDefaultAsync(w => w.Id == id);

            if (workoutToUpdate == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Index");
            }
            workoutToUpdate.UserId = userId;

            if (await TryUpdateModelAsync<Workout>(
                 workoutToUpdate,
                 "Workout",
                 i => i.Title, i => i.Description,
                 i => i.StartTime, i => i.EndTime))
            {
                UpdateWorkoutExercises(_context, selectedExercises, exerciseSets, exerciseRepetitions, exerciseMaxWeight, workoutToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            UpdateWorkoutExercises(_context, selectedExercises, exerciseSets, exerciseRepetitions, exerciseMaxWeight, workoutToUpdate);
            PopulateAssignedExerciseData(_context, workoutToUpdate);

            return Page();
        }

        private bool WorkoutExists(int id)
        {
            return _context.Workout.Any(e => e.Id == id);
        }
    }
}
