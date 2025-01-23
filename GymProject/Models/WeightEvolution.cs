using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymProject.Models
{
    public class WeightEvolution
    {
        public int Id {  get; set; }

        [Display(Name = "Weight (kg)")]
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Weight { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}
