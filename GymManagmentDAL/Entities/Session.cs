using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    public class Session : BasicEntity
    {
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        #region RelationShips

        #region Session - Category

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        #endregion

        #region Session - Trainer

        public int SessionTrainerId { get; set; }
        public Trainer SessionTrainer { get; set; } = null!;

        #endregion

        #region MyRegion

        public ICollection<MemberSession> SessionsMember { get; set; } = null!;
        #endregion
        #endregion
    }
}
