using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagmentDAL.Entities.Enums;



namespace GymManagmentBLL.ViewModels.TrainerViewModel
{
    internal class TrainerViewModel
    {
        public int Id { get; set; }
        public string? Photo { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public Specialties Specialties { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Address { get; set; }
        //public bool IsActive { get; set; }
    }
}
