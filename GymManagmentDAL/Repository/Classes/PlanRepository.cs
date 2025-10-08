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
    internal class PlanRepository : IPlanRepository
    {
        private readonly GymDBContext _dbContext;

        public PlanRepository(GymDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(Plan plan)
        {
            _dbContext.Plans.Add(plan);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var plan = _dbContext.Plans.Find(id);
            if (plan != null)
            {
                _dbContext.Plans.Remove(plan);
                return _dbContext.SaveChanges();
            }
            else
                return 0;
        }

        public IEnumerable<Plan> GetAll()
        {
            return _dbContext.Plans.ToList();
        }

        public Plan? GetById(int id) => _dbContext.Plans.Find(id);

        public int Update(Plan plan)
        {
            _dbContext.Plans.Update(plan);
            return _dbContext.SaveChanges();
        }
    }
}
