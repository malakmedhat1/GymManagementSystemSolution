using GymManagmentDAL.Data.Contexts;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repository.Classes
{
    public class PlanRepository : IPlanRepository
    {
        private readonly GymDBContext _dbContext;

        public PlanRepository(GymDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Plan> GetAll()
        {
            return _dbContext.Plans.ToList();
        }

        public Plan? GetById(int id) => _dbContext.Plans.Find(id);

        public void Update(Plan plan) => _dbContext.Plans.Update(plan);
    }
}
