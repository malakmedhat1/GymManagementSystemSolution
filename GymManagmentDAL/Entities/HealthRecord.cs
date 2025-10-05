using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    public class HealthRecord : BasicEntity
    {
        // ID [FK] => ID of Member [PK]
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string BloodType { get; set; } = null!; // controled at frontend
        public string? Note { get; set; }
    }
}
