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
    public class IndexModel : PageModel
    {
        private readonly GymProject.Data.GymProjectContext _context;

        public IndexModel(GymProject.Data.GymProjectContext context)
        {
            _context = context;
        }

        public IList<WeightEvolution> WeightEvolution { get;set; } = default!;

        public async Task OnGetAsync()
        {
            WeightEvolution = await _context.WeightEvolution.ToListAsync();
        }
    }
}
