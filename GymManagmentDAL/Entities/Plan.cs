using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    public class Plan : BasicEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int DurationDays{ get; set; }
        public bool IsActive { get; set; }

        public ICollection<MemberShip> PlanMembers { get; set; } = null!;
    }
}
