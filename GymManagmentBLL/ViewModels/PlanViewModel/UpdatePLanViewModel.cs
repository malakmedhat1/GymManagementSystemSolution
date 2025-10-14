using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.ViewModels.PlanViewModel
{
    internal class UpdatePLanViewModel
    {
        [Required(ErrorMessage = "Name is Required!!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name Must Be Between 2 and 50 Char.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name Must Be Only Letters and Spaces.")]
        public string PlanName { get; set; } = null!;
        
        [Required(ErrorMessage = "Description is Required!!")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description Must Be Between 10 and 500 Char.")]
        public string Description { get; set; } = null!;
        
        [Required(ErrorMessage = "Price is Required!!")]
        [Range(0.1, 10000, ErrorMessage = "Price Must Be Between 0.1 and 10000.")]
        public decimal Price { get; set; }
        
        [Required(ErrorMessage = "Duration Days is Required!!")]
        [Range(1, 365, ErrorMessage = "Duration Days Must Be Between 1 and 365.")]
        public int DurationDays { get; set; }
    }
}
