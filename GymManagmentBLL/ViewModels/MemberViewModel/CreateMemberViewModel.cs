using GymManagmentDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.ViewModels.MemberViewModel
{
    public class CreateMemberViewModel
    {
        [Required(ErrorMessage ="Name is Required!!")]
        [StringLength(50, MinimumLength =2 , ErrorMessage ="Name Must Be Between 2 and 50 Char.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name Must Be Only Letters and Spaces.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is Required!!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(100,MinimumLength =5, ErrorMessage = "Email Must Be Less Than 100 Char.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone is Required!!")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Invalid Phone Number.")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$" , ErrorMessage ="Phone must be a valid egyption phone number")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Birth is Required!!")]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender Is Required!!")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Building Number Is Required!!")]
        [Range(1, 1000, ErrorMessage = "Building Number Must Be Between 0 and 1000.")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Street Is Required!!")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street Must Be Between 2 and 30 Char.")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "City Is Required!!")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City Must Be Between 2 and 30 Char.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City Must Be Only Letters and Spaces.")]
        public string City { get; set; } = null!;

        public HealthRecordViewModel HealthRecordViewModel { get; set; } = null!;

    }
}
