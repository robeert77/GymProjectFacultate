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

namespace GymProject.Pages.WeightEvolutions
{
    public class CreateModel : PageModel
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
            return Page();
        }

        [BindProperty]
        public WeightEvolution WeightEvolution { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Index");
            }

            WeightEvolution.UserId = userId;

            _context.WeightEvolution.Add(WeightEvolution);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
