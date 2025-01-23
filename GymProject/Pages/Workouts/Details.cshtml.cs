using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GymProject.Data;
using GymProject.Models;

namespace GymProject.Pages.Workouts
{
    public class DetailsModel : WorkoutExercisesPageModel
    {
        private readonly GymProject.Data.GymProjectContext _context;

        public DetailsModel(GymProject.Data.GymProjectContext context)
        {
            _context = context;
        }

        public Workout Workout { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _context.Workout
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
    }
}
