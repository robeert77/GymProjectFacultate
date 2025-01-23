using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GymProject.Models;

namespace GymProject.Data
{
    public class GymProjectContext : DbContext
    {
        public GymProjectContext (DbContextOptions<GymProjectContext> options)
            : base(options)
        {
        }

        public DbSet<GymProject.Models.Exercise> Exercises { get; set; } = default!;
        public DbSet<GymProject.Models.WeightEvolution> WeightEvolution { get; set; } = default!;
        public DbSet<GymProject.Models.Workout> Workout { get; set; } = default!;
        public DbSet<GymProject.Models.WorkoutExercise> WorkoutExercise { get; set; } = default!;
    }
}
