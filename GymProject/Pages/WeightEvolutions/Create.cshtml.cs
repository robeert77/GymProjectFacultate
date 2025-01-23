using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GymProject.Data;
using GymProject.Models;

namespace GymProject.Pages.WeightEvolutions
{
    public class CreateModel : PageModel
    {
        private readonly GymProject.Data.GymProjectContext _context;

        public CreateModel(GymProject.Data.GymProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public WeightEvolution WeightEvolution { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.WeightEvolution.Add(WeightEvolution);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
