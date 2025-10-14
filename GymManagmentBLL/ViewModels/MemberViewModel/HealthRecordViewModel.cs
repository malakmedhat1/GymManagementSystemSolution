using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.ViewModels.MemberViewModel
{
    internal class HealthRecordViewModel
    {
        [Required(ErrorMessage = "Height Is Required!!")]
        [Range(0.1, 300, ErrorMessage = "Height Must Be Between 0.1 cm and 300 cm.")]
        public decimal Height { get; set; }

        [Required(ErrorMessage = "Weight Is Required!!")]
        [Range(0.1, 500, ErrorMessage = "Weight Must Be Between 0.1 kg and 500 kg.")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Blood Type Is Required!!")]
        [RegularExpression(@"^(A|B|AB|O)[+-]$", ErrorMessage = "Blood Type Must Be One of the Following: A+, A-, B+, B-, AB+, AB-, O+, O-.")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "Blood Type Must Be Between 2 and 3 Char.")]
        //[RegularExpression(@"^[A-Z0-9+-]+$", ErrorMessage = "Blood Type Must Be Only Uppercase Letters, Digits, '+' and '-'.")]
        public string BloodType { get; set; } = null!;

        public string? Note { get; set; }

        public HealthRecordViewModel HealthRecordViewModels { get; set; } = null!;

    }
}
