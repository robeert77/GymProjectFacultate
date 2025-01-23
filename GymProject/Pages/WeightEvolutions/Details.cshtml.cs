using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GymProject.Data;
using GymProject.Models;

namespace GymProject.Pages.WeightEvolutions
{
    public class DetailsModel : PageModel
    {
        private readonly GymProject.Data.GymProjectContext _context;

        public DetailsModel(GymProject.Data.GymProjectContext context)
        {
            _context = context;
        }

        public WeightEvolution WeightEvolution { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weightevolution = await _context.WeightEvolution.FirstOrDefaultAsync(m => m.Id == id);
            if (weightevolution == null)
            {
                return NotFound();
            }
            else
            {
                WeightEvolution = weightevolution;
            }
            return Page();
        }
    }
}
