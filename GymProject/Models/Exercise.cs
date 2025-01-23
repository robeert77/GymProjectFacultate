using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymProject.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        [Display(Name = "Exercise Type")]
        public string Title { get; set; }

        [Display(Name = "Additionl Information")]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        [DisplayFormat(DataFormatString = "{0:HH:mm dd.MM.yyyy}")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<WorkoutExercise>? WorkoutExercise { get; set; }
    }
}
