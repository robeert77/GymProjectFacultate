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

namespace GymProject.Pages.WeightEvolutions
{
    public class EditModel : PageModel
    {
        private readonly GymProject.Data.GymProjectContext _context;

        public EditModel(GymProject.Data.GymProjectContext context)
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

            var weightevolution =  await _context.WeightEvolution.FirstOrDefaultAsync(m => m.Id == id);
            if (weightevolution == null)
            {
                return NotFound();
            }
            WeightEvolution = weightevolution;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(WeightEvolution).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeightEvolutionExists(WeightEvolution.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool WeightEvolutionExists(int id)
        {
            return _context.WeightEvolution.Any(e => e.Id == id);
        }
    }
}
