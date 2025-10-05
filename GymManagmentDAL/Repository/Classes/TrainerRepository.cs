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
    internal class TrainerRepository : ITrainerRepository
    {
        private readonly GymDBContext _dbContext;

        public TrainerRepository(GymDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Trainer trainer)
        {
            _dbContext.Trainers.Add(trainer);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var trainer = _dbContext.Trainers.Find(id);
            if (trainer != null)
            {
                _dbContext.Trainers.Remove(trainer);
                return _dbContext.SaveChanges();
            }
            else
                return 0;
        }

        public IEnumerable<Trainer> GetAll()
        {
           return _dbContext.Trainers.ToList();

        }

        public Trainer? GetById(int id) => _dbContext.Trainers.Find(id);

        public int Update(Trainer trainer)
        {
            _dbContext.Trainers.Update(trainer);
            return _dbContext.SaveChanges();
        }
    }
}
