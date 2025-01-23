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
    public class DeleteModel : PageModel
    {
        private readonly GymProject.Data.GymProjectContext _context;

        public DeleteModel(GymProject.Data.GymProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weightevolution = await _context.WeightEvolution.FindAsync(id);
            if (weightevolution != null)
            {
                WeightEvolution = weightevolution;
                _context.WeightEvolution.Remove(WeightEvolution);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
