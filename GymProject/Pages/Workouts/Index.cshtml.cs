using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GymProject.Data;
using GymProject.Models;
using Microsoft.AspNetCore.Identity;

namespace GymProject.Pages.Workouts
{
    public class IndexModel : PageModel
    {
        private readonly GymProject.Data.GymProjectContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(GymProject.Data.GymProjectContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Workout> Workout { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

            Workout = await _context.Workout
                .Where(w => w.UserId == userId)
                .ToListAsync();
        }
    }
}
